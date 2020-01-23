using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using Microsoft.Extensions.Caching.Distributed;
using restapi.Models;

namespace restapi
{
    public class TimesheetsRepository
    {
        const string DATABASE_FILE = "filename=timesheets.db;mode=exclusive";

        static TimesheetsRepository()
        {
            using (var database = new LiteDatabase(DATABASE_FILE))
            {
                var timesheets = database.GetCollection<Timecard>("timesheets");

                timesheets.EnsureIndex(t => t.UniqueIdentifier);
            }
        }


        public IEnumerable<Timecard> All
        {
            get
            {
                using (var database = new LiteDatabase(DATABASE_FILE))
                {
                    var timesheets = database.GetCollection<Timecard>("timesheets");

                    return timesheets.Find(Query.All()).ToList();
                }
            }
        }

        public Timecard Find(Guid id)
        {
            Timecard timecard = null;

            using (var database = new LiteDatabase(DATABASE_FILE))
            {
                var timesheets = database.GetCollection<Timecard>("timesheets");

                timecard = timesheets
                    .FindOne(t => t.UniqueIdentifier == id);
            }

            return timecard;
        }

        public void Add(Timecard timecard)
        {
            using (var database = new LiteDatabase(DATABASE_FILE))
            {
                var timesheets = database.GetCollection<Timecard>("timesheets");

                timesheets.Insert(timecard);
            }
        }

        public void Update(Timecard timecard)
        {
            using (var database = new LiteDatabase(DATABASE_FILE))
            {
                var timesheets = database.GetCollection<Timecard>("timesheets");

                timesheets.Update(timecard);
            }
        }

        public void Delete(Guid id)
        {
            using (var database = new LiteDatabase(DATABASE_FILE))
            {
                var timesheets = database.GetCollection<Timecard>("timesheets");

                timesheets.Delete(t => t.UniqueIdentifier == id);
            }
        }
    }
}