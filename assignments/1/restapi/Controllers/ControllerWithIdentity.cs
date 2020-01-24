using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace restapi.Controllers
{
    public abstract class ControllerWithIdentity : Controller
    {
        public ControllerWithIdentity()
        {
        }

        // this is an example only, for the asp.net framework you'd want
        // to wire up the built-in identity from the controller class, but
        // this works to demonstrate one way to handle things
        protected int CallerIdentity
        {
            get
            {
                var hasIdentity = Request.Headers.ContainsKey("x-timesheet-user");
                if (hasIdentity == false)
                {
                    throw new ArgumentException("missing http header x-timesheet-user", "x-timesheet-user");
                }

                var identityHeader = Request.Headers["x-timesheet-user"].FirstOrDefault();
                if (string.IsNullOrEmpty(identityHeader) == true)
                {
                    throw new ArgumentException("missing http header x-timesheet-user value", "x-timesheet-user");
                }

                var identity = 0;
                if (int.TryParse(identityHeader, out identity) == false)
                {
                    throw new ArgumentException("unable to parse x-timesheet-user", "x-timesheet-user");
                }

                return identity;
            }
        }

        //
        // we're going to use an HTTP header to pass in the "caller's identity" and
        // remove it from the documents. that way we don't need to pass in data that
        // doesn't make any sense in a document (the "who-done-it" piece), and move
        // those to an out-of-band mechanism
        private int GetCallerIdentity()
        {
            var hasIdentity = Request.Headers.ContainsKey("x-timesheet-user");
            if (hasIdentity == false)
            {
                throw new ArgumentException("missing http header x-timesheet-user", "x-timesheet-user");
            }

            var identityHeader = Request.Headers["x-timesheet-user"].FirstOrDefault();
            if (string.IsNullOrEmpty(identityHeader) == true)
            {
                throw new ArgumentException("missing http header x-timesheet-user value", "x-timesheet-user");
            }

            var identity = 0;
            if (int.TryParse(identityHeader, out identity) == false)
            {
                throw new ArgumentException("unable to parse x-timesheet-user", "x-timesheet-user");
            }

            return identity;
        }
    }
}
