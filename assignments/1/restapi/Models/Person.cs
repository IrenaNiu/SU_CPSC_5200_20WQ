using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using restapi.Helpers;

namespace restapi.Models
{
    public class Person
    {
        public Person() { }

        [JsonIgnore]
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonIgnore]
        [JsonProperty("_self")]
        public string Self { get => $"/employees/{EmployeeId}"; }

        public string Name { get; set; }

        public int ManagerId { get; set; }

        [JsonProperty("id")]
        public int EmployeeId { get; set; }

        public PersonStatus Status { get; set; }

        [JsonProperty("actions")]
        public IList<ActionLink> Actions { get => GetActionLinks(); }

        [JsonProperty("documentation")]
        public IList<DocumentLink> Documents { get => GetDocumentLinks(); }

        public string Version { get; set; } = "timecard-0.2";

        private IList<ActionLink> GetActionLinks()
        {
            var links = new List<ActionLink>();

            switch (Status)
            {
                case PersonStatus.CurrentEmployee:
                    // should consider option to update
                    // should consider option to terminate
                    break;

                case PersonStatus.PastEmployee:
                    // consider option to re-hire
                    break;
            }

            return links;
        }

        private IList<DocumentLink> GetDocumentLinks()
        {
            var links = new List<DocumentLink>();

            // i might consider options to expose
            //   * direct reports of this employee (team)
            links.Add(new DocumentLink()
            {
                Method = Method.Get,
                Type = ContentTypes.Employees,
                Relationship = DocumentRelationship.Reports,
                Reference = $"/employees/{EmployeeId}/reports"
            });

            //   * indirect reports of this employee (organizaation)
            links.Add(new DocumentLink()
            {
                Method = Method.Get,
                Type = ContentTypes.Employees,
                Relationship = DocumentRelationship.Organization,
                Reference = $"/employees/{EmployeeId}/organization"
            });

            //   * link to manager of this employee
            links.Add(new DocumentLink()
            {
                Method = Method.Get,
                Type = ContentTypes.Employee,
                Relationship = DocumentRelationship.Manager,
                Reference = $"/employees/{EmployeeId}/manager"
            });

            // or directly to the manager
            links.Add(new DocumentLink()
            {
                Method = Method.Get,
                Type = ContentTypes.Employee,
                Relationship = DocumentRelationship.Manager,
                Reference = $"/employees/{ManagerId}"
            });

            return links;
        }

        public override string ToString()
        {
            return PublicJsonSerializer.SerializeObjectIndented(this);
        }
    }
}