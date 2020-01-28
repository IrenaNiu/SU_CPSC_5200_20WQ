using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using restapi.Models;

namespace restapi.Controllers
{
    public class RootController : Controller
    {
        // GET api/values
        [Route("~/")]
        [HttpGet]
        [Produces(ContentTypes.Root)]
        [ProducesResponseType(typeof(IDictionary<ApplicationRelationship, object>), 200)]
        public IDictionary<ApplicationRelationship, object> Get()
        {
            return new Dictionary<ApplicationRelationship, object>()
            {
                {
                    ApplicationRelationship.Timesheets, new List<DocumentLink>()
                    {
                        new DocumentLink()
                        {
                            Method = Method.Get,
                            Type = ContentTypes.Timesheets,
                            Relationship = DocumentRelationship.Timesheets,
                            Reference = "/timesheets"
                        },

                        //
                        // this is how we expose a new application-level endpoint, in this case
                        // we're adding a POST to /timesheets of timesheet content type
                        //
                        new DocumentLink()
                        {
                            Method = Method.Post,
                            Type = ContentTypes.Timesheet,
                            Relationship = DocumentRelationship.CreateTimesheet,
                            Reference = "/timesheets"
                        }
                    }
                },
                {
                    ApplicationRelationship.Employees, new List<DocumentLink>()
                    {
                        new DocumentLink()
                        {
                            Method = Method.Get,
                            Type = ContentTypes.Employees,
                            Relationship = DocumentRelationship.Employees,
                            Reference = "/employees"
                        },

                        new DocumentLink()
                        {
                            Method = Method.Post,
                            Type = ContentTypes.Employees,
                            Relationship = DocumentRelationship.CreateEmployee,
                            Reference = "/employees"
                        }
                    }
                },
                {
                    //
                    // and, we need to bump the timesheet version, we can argue whether
                    // this is considered a breaking change or not. i'm saying it's not
                    // in this case because all of our existing clients will continue
                    // to work without fail
                    //
                    ApplicationRelationship.Version, "0.2"
                }
            };
        }
    }
}
