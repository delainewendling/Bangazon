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
    public class CustomersController : Controller
    {
        //entity framework allows you to interact with your database after you've set up the database structure
        private BangazonContext context;
        public CustomersController(BangazonContext ctx)
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
            //The left side of this expression is written in LINQ 
            //You could say: select customer.FirstName to drill down to what you want.  
            IQueryable<object> customers = from customer in context.Customer select customer;

            if (customers == null)
            {
                //NotFound() is a built in method that returns a correct 404
                return NotFound();
            }
            //Http response with status code 200 with the appropriate headers and send it back to the client. Converts customers to json and responds. 
            return Ok(customers);

        }

        //Will also handle GET but will look for an id to give you back one item
        // GET api/values/5
        //A JSON files gets put out onto the webpage even though we're feeding it a C# string array
        //Naming this entire process of constructing an Http response and reference that entire process using GetCustomer
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //Only want a single customer
                Customer customer = context.Customer.Single(m => m.CustomerId == id);

                if (customer == null)
                {
                    return NotFound();
                }
                
                return Ok(customer);
            }
            catch (System.InvalidOperationException)
            {
                return NotFound();
            }

        }
        //POST requests use this method
        //POST api/values
        //Anything with [] is a decorator
        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            //this checks to see that all required methods are present
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Adds it to the staging area but doesn't save it - it's being added to the Bangazon Context file
            context.Customer.Add(customer);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            //You get the user back after posting by using this method
            return CreatedAtRoute("GetCustomer", new { id = customer.CustomerId }, customer);
        }
        //PUT requests use this method 
        // PUT customers/5
        [HttpPut("{id}")]
        //The [FromBody] tells you to look inside the body of the request for the information. 
        public IActionResult Put([FromRoute]int id, [FromBody] Customer newCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newCustomer.CustomerId != id)
            {
                return NotFound();
            }
            // context.Entry(newCustomer).State = EntityState.Modified;
            // newCustomer.LastUpdated = DateTime.Now;
            context.Customer.Update(newCustomer);
            context.SaveChanges();
   
            //You get the user back after posting by using this method
            return CreatedAtRoute("GetCustomer", new { id = newCustomer.CustomerId }, newCustomer);
        }

        // DELETE customers/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Customer customer = context.Customer.Single(m => m.CustomerId == id);

                if (customer == null)
                {
                    return NotFound();
                }
                context.Customer.Remove(customer);
                context.SaveChanges();
                return Ok(customer);
            }
            catch (System.InvalidOperationException)
            {
                return NotFound();
            }
        }
        private bool CustomerExists(int id)
        {
            return context.Customer.Count(e => e.CustomerId == id) > 0;
        }
    }
}
