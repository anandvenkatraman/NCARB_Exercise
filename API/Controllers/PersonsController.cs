using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TestApi1.Models;
using System.Web.Script.Serialization;
using System.Diagnostics;
using TestApi1.Caching;

namespace TestApi1.Controllers
{
    public class PersonsController : ApiController
    {

        // GET api/persons
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return WebApiCache.Persons.OrderByDescending(p => p.Id);
        }

        // GET api/persons/1
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var person = WebApiCache.Persons.FirstOrDefault(p => (p.Id == id));
            return Ok(person);
            
        }

        // POST api/persons
        [HttpPost]
        public IHttpActionResult Post([FromBody] Person v)
        {
            if (WebApiCache.Persons.Where(p => p.Id == v.Id).Count() == 0) WebApiCache.AddPerson(v);
            else
            {
                var p = WebApiCache.Persons.FirstOrDefault(o => o.Id == v.Id);
                WebApiCache.UpdatePerson(p, v);
                printPerson(p, v);
            }
            return Ok();
        }

        // DELETE api/persons/id
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            WebApiCache.RemovePerson(id);
            return Ok();
        }

        // PUT api/persons/deleteall
        [Route("api/persons/DeleteAll")]
        [HttpPut]
        public IHttpActionResult DeleteAll()
        {
            WebApiCache.ClearPersons();
            return Ok();
        }

        // PUT api/persons/generatedata
        [Route("api/persons/GenerateData")]
        [HttpPut]
        public IHttpActionResult GenerateData()
        {
            WebApiCache.GeneratePersonsData();
            return Ok();
        }

        void printPerson(Person p, Person v)
        {
            var jsr = new JavaScriptSerializer();
            Debug.WriteLine("------------------");
            Debug.WriteLine("Person Object Old Values - " + jsr.Serialize(p));
            Debug.WriteLine("Person Object New Values - " + jsr.Serialize(v));
            Debug.WriteLine("------------------");

        }
    }
}
