using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bangazon.Data;
using Bangazon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace BangazonAPI.Controllers
{
    //Looks at the name of the class and makes the name of the route match the name of the class. api/customers OR you can modify the route.
    [Route("[controller]")]
    //This tells the api to produce json only in all of its responses
    [Produces("application/json")]
    public class OrdersController : Controller
    {
        //entity framework allows you to interact with your database after you've set up the database structure
        private BangazonContext context;
        public OrdersController(BangazonContext ctx)
        {
            context = ctx;
        }
        //When you make a GET request. It will magically call this method for you.
        // GET api/customer
        [HttpGet]
        //I want to perform an action against the database and return a result. 
        public IActionResult Get()
        {
            //I want to query things from this object
            //context is the interface to the database so context.Customer is the table
            //select customer means select everything from the table. 
            IQueryable<object> orders = from order in context.Order select order;

            if (orders == null)
            {
                //NotFound() is a built in method that returns a correct 404
                return NotFound();
            }
            //Http response with status code 200 with the appropriate headers and send it back to the client. Converts customers to json and responds. 
            return Ok(orders);

        }

        //Will also handle GET but will look for an id to give you back one item
        // GET api/values/5
        //A JSON files gets put out onto the webpage even though we're feeding it a C# string array
        //Naming this entire process of constructing an Http response and reference that entire process using GetCustomer
        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //Only want a single customer
                Order order = context.Order.Single(m => m.OrderId == id);

                if (order == null)
                {
                    return NotFound();
                }
                
                return Ok(order);
            }
            catch (System.InvalidOperationException)
            {
                return NotFound();
            }


        }
        //POST requests use this method
        // POST api/values
        //Anything wil [] is a decorator
        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            //this checks to see that all required methods are present
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Adds it to the staging area but doesn't save it 
            context.Order.Add(order);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.OrderId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            //You get the user back after posting by using this method
            return CreatedAtRoute("GetOrder", new { id = order.OrderId }, order);
        }
        //PUT requests use this method 
        // PUT api/values/5
        [HttpPut("{id}")]
        //The [FromBody] tells you to look inside the body of the request for the id. 
        public IActionResult Put(int id, [FromBody]Order newOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newOrder.OrderId != id)
            {
                return NotFound();
            }
            // context.Entry(newCustomer).State = EntityState.Modified;
            // newCustomer.LastUpdated = DateTime.Now;
            context.Order.Update(newOrder);
            context.SaveChanges();
   
            //You get the user back after posting by using this method
            return CreatedAtRoute("GetOrder", new { id = newOrder.OrderId }, newOrder);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
          try
            {
                Order order = context.Order.Single(m => m.OrderId == id);

                if (order == null)
                {
                    return NotFound();
                }
                context.Order.Remove(order);
                context.SaveChanges();
                return Ok(order);
            }
            catch (System.InvalidOperationException)
            {
                return NotFound();
            }
        }
        private bool OrderExists(int id)
        {
            return context.Order.Count(e => e.OrderId == id) > 0;
        }
    }
}
