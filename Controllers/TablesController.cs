using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NodeCMBAPI.Models;
using NodeCMBAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NodeCMBAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : Controller
    {
        private readonly ITablesService _tablelService;
        public TablesController(ITablesService tablelService)
        {
            _tablelService = tablelService;
        }
        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var records = _tablelService.GetTables();

            if (records != null)
                if (records.Count > 0)
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

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var records = _tablelService.GetById(id);
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
        [HttpPost("AddTables")]
        public ActionResult<string> AddTables(Tables table)
        {
            var status = _tablelService.AddTables(table);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (status.Equals("1"))
            {
                dic.Add("status", "1");
                dic.Add("message", "Successful");
                dic.Add("data", null);
                return Ok(dic);
            }
            else
            {
                dic.Add("status", "0");
                dic.Add("message", status);
                dic.Add("data", null);
                return Ok(dic);
            }
        }

        [HttpPost("UpdateTables")]
        public ActionResult<string> UpdateTables(Tables table)
        {
            var status = _tablelService.UpdateTables(table);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (status.Equals("1"))
            {
                var updatedHotel = _tablelService.GetById(table.ID);
                dic.Add("status", "1");
                dic.Add("message", "Successful");
                dic.Add("data", updatedHotel);
                return Ok(dic);
            }
            else
            {
                dic.Add("status", "0");
                dic.Add("message", status);
                dic.Add("data", null);
                return Ok(dic);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
