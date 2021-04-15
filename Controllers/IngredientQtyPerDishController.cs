using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NodeCMBAPI.Models;
using NodeCMBAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NodeCMBAPI.ControllersBase
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientQtyPerDishController : Controller
    {
        private readonly IIngredientQtyPerDishService _iqpdService;
        public IngredientQtyPerDishController(IIngredientQtyPerDishService iqpdService)
        {
            _iqpdService = iqpdService;
        }
        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var records = _iqpdService.GetIngredientQtyPerDish();

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
            var records = _iqpdService.GetById(id);
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

        [HttpPost("AddIngredientQtyPerDish")]
        public ActionResult<string> AddIngredientQtyPerDish(Ingredient_Qty_Per_Dish iqpd)
        {
            var status = _iqpdService.AddIngredientQtyPerDish(iqpd);
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

        [HttpPost("UpdateIngredientQtyPerDish")]
        public ActionResult<string> UpdateIngredientQtyPerDish(Ingredient_Qty_Per_Dish iqpd)
        {
            var status = _iqpdService.UpdateIngredientQtyPerDish(iqpd);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (status.Equals("1"))
            {
                var updatediqpd = _iqpdService.GetById(iqpd.ID);
                dic.Add("status", "1");
                dic.Add("message", "Successful");
                dic.Add("data", updatediqpd);
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
