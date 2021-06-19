using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

const string PathToFile = "result.json";
const string ApiUrl = "https://jsonplaceholder.typicode.com";

await WorkAsync(ApiUrl, PathToFile, true);


static async Task WorkAsync(string apiUrl, string pathToFile, bool printResult)
{
    PostsLoaderService.InitializeClient(apiUrl);
    var posts = await PostsLoaderService.LoadPostsAsync(10);

    await SaveToFileHandler.SaveToFileAsync(pathToFile, posts);

    if (printResult)
    {
        foreach (var post in posts)
            Console.WriteLine(post);
    }
}

// Helpers

static class PostsLoaderService
{
    private static HttpClient Client;
    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web);

    public static void InitializeClient(string baseApiUrl)
    {
        Client = new HttpClient() { BaseAddress = new Uri(baseApiUrl) };
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public static async Task<IEnumerable<Post>> LoadPostsAsync(int count = 1)
    {
        var tasks = Enumerable
            .Range(1, count)
            .Select(id => Client.GetStringAsync($"posts/{id}")
            .ContinueWith(x => JsonSerializer.Deserialize<Post>(x.Result, Options)));
        await Task.WhenAll(tasks);
        return tasks.Select(x => x.Result);
    }
}

static class SaveToFileHandler
{
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

    public static async Task SaveToFileAsync<T>(string pathToFile, IEnumerable<T> data)
    {
        using var stream = GetFileStream(pathToFile);
        await JsonSerializer.SerializeAsync(stream, data, Options);
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