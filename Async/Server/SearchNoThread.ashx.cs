﻿using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Server
{
    public class SearchNoThread : HttpTaskAsyncHandler
    {
        public async override Task ProcessRequestAsync(HttpContext context)
        {
            var searchTasks = Enumerable.Range(0, 3).Select(async index => await FileReader.SearchInNoThreadAsync("cip-cip")).ToList();
            await Task.WhenAll(searchTasks);
            context.Response.ContentType = "text/plain";
            context.Response.Write(searchTasks.First().Result);
        }
    }
}