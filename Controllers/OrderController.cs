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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _Service;
        public OrderController(IOrderService Service)
        {
            _Service = Service;
        }
        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var records = _Service.GetOrder();

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
            var records = _Service.GetById(id);
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

        [HttpPost("AddOrder")]
        public ActionResult<string> AddOrder(Order order)
        {
            var status = _Service.AddOrder(order);
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

        [HttpPost("UpdateOrder")]
        public ActionResult<string> UpdateOrder(Order order)
        {
            var status = _Service.UpdateOrder(order);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (status.Equals("1"))
            {
                var updatedOrder = _Service.GetById(order.ID);
                dic.Add("status", "1");
                dic.Add("message", "Successful");
                dic.Add("data", updatedOrder);
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
