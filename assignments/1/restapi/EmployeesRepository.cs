using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using restapi.Models;

namespace restapi
{
    public class EmployeesRepository
    {
        const string DATABASE_FILE = "filename=employees.db;mode=exclusive";

        static EmployeesRepository()
        {
            using (var database = new LiteDatabase(DATABASE_FILE))
            {
                var employees = database.GetCollection<Person>("employees");

                employees.EnsureIndex(t => t.EmployeeId);
            }
        }


        public IEnumerable<Person> All
        {
            get
            {
                using (var database = new LiteDatabase(DATABASE_FILE))
                {
                    var employees = database.GetCollection<Person>("employees");

                    return employees.Find(Query.All()).ToList();
                }
            }
        }

        public Person Find(int id)
        {
            Person employee = null;

            using (var database = new LiteDatabase(DATABASE_FILE))
            {
                var employees = database.GetCollection<Person>("employees");

                employee = employees
                    .FindOne(t => t.EmployeeId == id);
            }

            return employee;
        }

        public void Add(Person employee)
        {
            using (var database = new LiteDatabase(DATABASE_FILE))
            {
                var employees = database.GetCollection<Person>("employees");

                employees.Insert(employee);
            }
        }

        public void Update(Person employee)
        {
            using (var database = new LiteDatabase(DATABASE_FILE))
            {
                var employees = database.GetCollection<Person>("employees");

                employees.Update(employee);
            }
        }

        public void Delete(int id)
        {
            using (var database = new LiteDatabase(DATABASE_FILE))
            {
                var employees = database.GetCollection<Person>("employees");

                employees.Delete(t => t.EmployeeId == id);
            }
        }
    }
}