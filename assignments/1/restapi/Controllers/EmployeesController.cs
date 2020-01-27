using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using restapi.Models;

namespace restapi.Controllers
{
    [Route("[controller]")]
    public class EmployeesController : ControllerWithIdentity
    {
        private readonly EmployeesRepository repository;

        private readonly ILogger logger;

        public EmployeesController(ILogger<EmployeesController> logger)
        {
            repository = new EmployeesRepository();
            this.logger = logger;
        }

        [HttpGet]
        [Produces(ContentTypes.Employees)]
        [ProducesResponseType(typeof(IEnumerable<Person>), 200)]
        public IEnumerable<Person> GetAll()
        {
            return repository
                .All
                .OrderBy(t => t.Name);
        }

        [HttpGet("{employeeId:int}")]
        [Produces(ContentTypes.Employee)]
        [ProducesResponseType(typeof(Person), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetOne(int employeeId)
        {
            Person employee = repository.Find(employeeId);

            if (employee != null)
            {
                return Ok(employee);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Produces(ContentTypes.Employee)]
        [ProducesResponseType(typeof(Person), 200)]
        public Person Create([FromBody] Person employee)
        {
            repository.Add(employee);

            return employee;
        }

        [HttpGet("{employeeId:int}/reports")]
        [Produces(ContentTypes.Employees)]
        [ProducesResponseType(typeof(IList<Person>), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetReports(int employeeId)
        {
            return StatusCode(501, "Function not implemented");
        }

        [HttpGet("{employeeId:int}/organization")]
        [Produces(ContentTypes.Employees)]
        [ProducesResponseType(typeof(IList<Person>), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetOrganization(int employeeId)
        {
            return StatusCode(501, "Function not implemented");
        }

        [HttpGet("{employeeId:int}/manager")]
        [Produces(ContentTypes.Employees)]
        [ProducesResponseType(typeof(IList<Person>), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetManager(int employeeId)
        {
            return StatusCode(501, "Function not implemented");
        }
    }
}
