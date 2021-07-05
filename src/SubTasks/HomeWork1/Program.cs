using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;
using System.Collections.Immutable;

const string PathToFile = "result.json";

try
{
    var posts = await PostsLoaderService.LoadPostsAsync(4, 13);

    SaveToFileHandler.SaveToFile(PathToFile, posts.ToImmutableArray());

    Console.WriteLine("Posts loaded and saved to file result.txt");

    if (false)
    {
        foreach (var post in posts)
            Console.WriteLine(post);
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

// Helpers

static class PostsLoaderService
{
    private static readonly HttpClient Client;
    private static readonly JsonSerializerOptions Options;
    private static readonly Lazy<ConcurrentBag<int>> FailLoad;

    static PostsLoaderService()
    {
        Options = new(JsonSerializerDefaults.Web);
        FailLoad = new();
        Client = new();
        Client.DefaultRequestHeaders.Accept.Add(new("application/json"));
    }

    public static async Task<IEnumerable<Post>> LoadPostsAsync(int from, int to)
    {
        if (from > to || from < 1 || to < 1)
            throw new InvalidOperationException("From must be lower then To, and both must be greater than 1");
        var toAwait = Enumerable.Range(from, to).Select(GetPost).ToArray();
        var tasks = Task.WhenAll(toAwait);
        try
        {
            await tasks;
        }
        catch (Exception) { }

        if(FailLoad.IsValueCreated && FailLoad.Value is { } bag && !bag.IsEmpty)
        {
            Console.WriteLine($"Fail to load posts: { string.Join(", ", bag.OrderBy(x=>x))}");
        }

        return toAwait.Where(x => !x.IsFaulted).Select(x => x.Result);
    }
    
    private static async Task<Post> GetPost(int id)
    {
        try
        {
            if (id is 5 or 6) throw new InvalidOperationException("Fail");
            var json = await Client.GetStringAsync($"https://jsonplaceholder.typicode.com/posts/{id}");
            return JsonSerializer.Deserialize<Post>(json, Options);
        }
        catch
        {
            FailLoad.Value.Add(id);
            throw;
        }
    }
}

static class SaveToFileHandler
{
    public static void SaveToFile(string pathToFile, ImmutableArray<Post> data)
    {
        using var stream = GetFileStream(pathToFile);
        using var writer = new StreamWriter(stream) { AutoFlush = true };
        
        for (int i = 0; i < data.Length; i++)
        {
            var item = data[i];
            var content = $"{item.UserId}\r\n{item.Id}\r\n{item.Title}\r\n{item.Body}";
            if(i == data.Length - 1) writer.Write($"{content}");
            else writer.WriteLine($"{content}\r\n");
        } 
    }

    private static Stream GetFileStream(string pathToFile)
    {
        return File.Create(pathToFile);
    }
}

record Post(int UserId, int Id, string Title, string Body)
{
    public override string ToString()
    {
        return $@"
================================
Post {Id} from user {{{UserId}}}
  Title: 
{Title}
        
  Body: 
{Body}
===============================";
    }
}