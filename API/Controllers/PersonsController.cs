using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TestApi1.Models;
using System.Web.Script.Serialization;
using System.Diagnostics;
namespace TestApi1.Controllers
{
    public class PersonsController : ApiController
    {

        static Person[] persons = new Person[]
        {
            new Person { Id = 1, FirstName = "John", LastName = "Smith", JobTitle = "COO"},
            new Person { Id = 2, FirstName = "Jane", LastName = "Doe", JobTitle = "CFO"},
        };

        // GET api/persons
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return persons;
        }

        // GET api/persons/1
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var person = persons.FirstOrDefault((p) => p.Id == id);
            return Ok(person);
            
        }

        // POST api/persons
        [HttpPost]
        public IHttpActionResult Post([FromBody] Person v)
        {
            var person = persons.FirstOrDefault((p) => p.Id == v.Id);
            printPerson(person,v);
            person.FirstName = v.FirstName;
            person.LastName = v.LastName;
            person.JobTitle = v.JobTitle;
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
