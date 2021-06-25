using KittensApi.Controllers.Requests;
using KittensApi.Controllers.Responses;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;

namespace KittensApi.Controllers
{
    [Route("kittens")]
    [ApiController]
    public class KittensStorageController : ControllerBase
    {
        [HttpPost]
        public void Create(KittenCreateRequest request)
        {

        }

        [HttpGet]
        public IEnumerable<KittenGetResponse> Get()
        {
            return Array.Empty<KittenGetResponse>();
        }
    }
}
