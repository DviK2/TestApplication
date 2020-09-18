using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestApplication.Services;

namespace TestApplication.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class NumbersController : ControllerBase
    {
        public NumbersService NumbersService { get; }

        public NumbersController(NumbersService numbersService)
        {
            NumbersService = numbersService;
        }

        [HttpPost]
        public JsonResult GetSquaredSum([FromBody] NumbersModel model)
        {
            int result = NumbersService.GetSquaredSum(model.Numbers);

            return new JsonResult(result);
        }
    }

    public class NumbersModel
    {
        [JsonProperty("numbers")]
        public List<int> Numbers { get; set; }
        public string Number { get; set; }
    }
}
