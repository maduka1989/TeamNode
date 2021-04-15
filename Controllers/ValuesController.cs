using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NodeCMBAPI.Services;
using NodeCMBAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace NodeCMBAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IDataService _dataService;
        public ValuesController(IDataService dataService)
        {
            _dataService = dataService;
        }
        [AllowAnonymous]
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            Dictionary<string, object> dic = new Dictionary<string, object>();
            //var records = _dataService.GetFoodPortion();
            //if (records != null)
            //    if (records.Count > 0)
            //    {
            //        dic.Add("status", "1");
            //        dic.Add("message", "Successful");
            //        dic.Add("data", records);
            //        return Ok(dic);
            //    }

            //dic.Add("status", "0");
            //dic.Add("message", "Unsuccessful");
            //dic.Add("data", null);
            //return Ok(dic);

            dic.Add("status", "1");
            dic.Add("message", "Successful");
            dic.Add("data", "test");
            return Ok(dic);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var records = _dataService.GetFoodPortionById(id);
            if (records != null)                
            {
                dic.Add("status", "1");
                dic.Add("message", "Successful");
                dic.Add("data", records);
                return Ok(dic);
            }

            dic.Add("status", "0");
            dic.Add("message", "Unsuccessful");
            dic.Add("data", null);
            return Ok(dic);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
