﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Abstractions.Models;
using BusinessLayer.Abstractions.Services;
using DataLayer.Abstractions.Filters;
using DataLayer.Abstractions.Repositories;
using KittenData = DataLayer.Abstractions.Entities.Kitten;
using MapsterMapper;

namespace BusinessLayer.Services
{
    public class KittensService : Service<Kitten, KittenData, int, IKittensRepository>, IKittensService
    {
        public KittensService(IKittensRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<IReadOnlyCollection<Kitten>> GetPage(int page, int pageSize)
        {
            var data = await Repository.GetFiltered(new PageFilter() { Page = page, Size = pageSize });
            return data.Select(Mapper.Map<Kitten>).ToList();
        }

        public async Task<IReadOnlyCollection<Kitten>> SearchFor(KittenSearchData searchData)
        {
            var data = await Repository.GetFiltered(searchFilterData: Mapper.Map<KittenSearchFilterData>(searchData));
            return data.Select(Mapper.Map<Kitten>).ToList();
        }

        public async Task<IReadOnlyCollection<Kitten>> GetPageFromSearch(int page, int pageSize, KittenSearchData searchData)
        {
            var data = await Repository.GetFiltered(new PageFilter() { Page = page, Size = pageSize }, Mapper.Map<KittenSearchFilterData>(searchData));
            return data.Select(Mapper.Map<Kitten>).ToList();
        }
    }
}