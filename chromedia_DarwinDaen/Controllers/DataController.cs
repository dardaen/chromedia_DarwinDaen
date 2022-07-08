using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft;

namespace chromedia_DarwinDaen.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<DataController> _logger;

        public DataController(ILogger<DataController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int limit = 1)
        {
            var response = new ResponseData();

            var res = await GetAPIData();

            //get top article base on limit
            var sortedRes = res.OrderByDescending(x => x.num_comments)?.Take(limit)?.ToList();

            if (sortedRes != null)
            {
                var responseTitles = new  List<string>();
                foreach (var item in sortedRes)
                {
                    var takenTitle = string.IsNullOrEmpty(item.title) ? (string.IsNullOrEmpty(item.story_title) ?  null : item.story_title) : item.title;
                    if (takenTitle != null)
                    {
                        responseTitles.Add(takenTitle);
                    }
                
                }

                return Ok(responseTitles);
                
            }


            return Ok("No Result Found");
        }



        public async Task<List<DetailedData>> GetAPIData()
        {
            var result = new List<DetailedData>();


            for (int i = 1; i <= 5; i++)
            {
                var client = new RestClient($"https://jsonmock.hackerrank.com/api/articles");

                var request = new RestRequest()
                    .AddParameter("page", i);

                request.AddHeader("Content-Type", "application/json");

                var response = await client.GetAsync(request);

                ResponseData jsonContent = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseData>(response.Content);

                result.AddRange(jsonContent.data);
            }

            return result;

        }
    }
}
