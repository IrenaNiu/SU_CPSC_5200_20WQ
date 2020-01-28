using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using restapi.Models;

namespace restapi.Controllers
{
    [Route("[controller]")]
    public class TimesheetsController : ControllerWithIdentity
    {
        private readonly TimesheetsRepository timesheetRepository;

        private readonly EmployeesRepository employeeRepository;

        private readonly ILogger logger;

        public TimesheetsController(ILogger<TimesheetsController> logger)
        {
            timesheetRepository = new TimesheetsRepository();
            employeeRepository = new EmployeesRepository();

            this.logger = logger;
        }

        [HttpGet]
        [Produces(ContentTypes.Timesheets)]
        [ProducesResponseType(typeof(IEnumerable<Timecard>), 200)]
        public IEnumerable<Timecard> GetAll()
        {
            return timesheetRepository
                .All
                .OrderBy(t => t.Opened);
        }

        [HttpGet("{id:guid}")]
        [Produces(ContentTypes.Timesheet)]
        [ProducesResponseType(typeof(Timecard), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetOne(Guid id)
        {
            logger.LogInformation($"Looking for timesheet {id}");

            Timecard timecard = timesheetRepository.Find(id);

            if (timecard != null)
            {
                return Ok(timecard);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Produces(ContentTypes.Timesheet)]
        [ProducesResponseType(typeof(Timecard), 200)]
        public Timecard Create([FromBody] DocumentPerson person)
        {
            logger.LogInformation($"Creating timesheet for {person.ToString()}");

            var timecard = new Timecard(person.Id);

            var entered = new Entered() { Person = person.Id };

            timecard.Transitions.Add(new Transition(entered));

            timesheetRepository.Add(timecard);

            return timecard;
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Delete(Guid id)
        {
            logger.LogInformation($"Looking for timesheet {id}");

            Timecard timecard = timesheetRepository.Find(id);

            if (timecard == null)
            {
                return NotFound();
            }

            if (timecard.CanBeDeleted() == false)
            {
                return StatusCode(409, new InvalidStateError() { });
            }

            timesheetRepository.Delete(id);

            return Ok();
        }

        [HttpGet("{id:guid}/lines")]
        [Produces(ContentTypes.TimesheetLines)]
        [ProducesResponseType(typeof(IEnumerable<TimecardLine>), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetLines(Guid id)
        {
            logger.LogInformation($"Looking for timesheet {id}");

            Timecard timecard = timesheetRepository.Find(id);

            if (timecard != null)
            {
                var lines = timecard.Lines
                    .OrderBy(l => l.WorkDate)
                    .ThenBy(l => l.Recorded);

                // in this case we need to walk over the lines to set their
                // up-to-date timecard status (timecard itself doesn't carry
                // a current status and it's not possible for the lines to 
                // have a persistent status, so...)
                foreach (var line in lines)
                {
                    line.TimecardStatus = timecard.Status;
                }

                return Ok(lines);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("{id:guid}/lines")]
        [Produces(ContentTypes.TimesheetLine)]
        [ProducesResponseType(typeof(TimecardLine), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(InvalidStateError), 409)]
        public IActionResult AddLine(Guid id, [FromBody] DocumentLine documentLine)
        {
            logger.LogInformation($"Looking for timesheet {id}");

            Timecard timecard = timesheetRepository.Find(id);

            if (timecard != null)
            {
                if (timecard.Status != TimecardStatus.Draft)
                {
                    return StatusCode(409, new InvalidStateError() { });
                }

                if (CallerIdentity != timecard.Employee)
                {
                    return StatusCode(400, new InvalidIdentityError() { });
                }

                var annotatedLine = timecard.AddLine(documentLine);

                timesheetRepository.Update(timecard);

                return Ok(annotatedLine);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{timecardId:guid}/lines/{lineId:guid}")]
        public IActionResult GetLine(Guid timecardId, Guid lineId)
        {
            Timecard timecard = timesheetRepository.Find(timecardId);

            if (timecard == null)
            {
                logger.LogInformation($"timecard {timecardId} wasn't found");
                return NotFound();
            }

            if (timecard.HasLine(lineId) == false)
            {
                // this might be better served by using some other 4xx error
                // because there's actually a problem with both the resource
                // we're updating and the request
                logger.LogInformation($"no matching lines found in collection");
                return NotFound();
            }

            var line = timecard.Lines.First(l => l.UniqueIdentifier == lineId);
            if (line == null)
            {
                logger.LogInformation($"line {lineId} not found in timecard {timecardId}");
                return NotFound();
            }

            // set the line's current status to the current timecard status
            // (timecard itself doesn't carry a current status and it's not 
            // possible for the lines to have a persistent status, so...)
            line.TimecardStatus = timecard.Status;

            return Ok(line);
        }

        [HttpPost("{timecardId:guid}/lines/{lineId:guid}")]
        public IActionResult ReplaceLine(Guid timecardId, Guid lineId, [FromBody] DocumentLine timecardLine)
        {
            Timecard timecard = timesheetRepository.Find(timecardId);

            if (timecard == null)
            {
                return NotFound();
            }

            if (timecard.HasLine(lineId) == false)
            {
                // this might be better served by using some other 4xx error
                // because there's actually a problem with both the resource
                // we're updating and the request
                return NotFound();
            }

            if (CallerIdentity != timecard.Employee)
            {
                return StatusCode(400, new InvalidIdentityError() { });
            }

            var result = timecard.ReplaceLine(lineId, timecardLine);

            return Ok(result);
        }

        [HttpPatch("{timecardId:guid}/lines/{lineId:guid}")]
        public IActionResult UpdateLine(Guid timecardId, Guid lineId, [FromBody] dynamic timecardLine)
        {
            Timecard timecard = timesheetRepository.Find(timecardId);

            if (timecard == null)
            {
                return NotFound();
            }

            if (timecard.HasLine(lineId) == false)
            {
                // this might be better served by using some other 4xx error
                // because there's actually a problem with both the resource
                // we're updating and the request
                return NotFound();
            }

            if (CallerIdentity != timecard.Employee)
            {
                return StatusCode(400, new InvalidIdentityError() { });
            }

            var result = timecard.ReplaceLine(lineId, timecardLine);

            return Ok(result);
        }

        [HttpGet("{id:guid}/transitions")]
        [Produces(ContentTypes.Transitions)]
        [ProducesResponseType(typeof(IEnumerable<Transition>), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetTransitions(Guid id)
        {
            logger.LogInformation($"Looking for timesheet {id}");

            Timecard timecard = timesheetRepository.Find(id);

            if (timecard != null)
            {
                return Ok(timecard.Transitions);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("{id:guid}/submittal")]
        [Produces(ContentTypes.Transition)]
        [ProducesResponseType(typeof(Transition), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(InvalidStateError), 409)]
        [ProducesResponseType(typeof(EmptyTimecardError), 409)]
        public IActionResult Submit(Guid id, [FromBody] Submittal submittal)
        {
            logger.LogInformation($"Looking for timesheet {id}");

            Timecard timecard = timesheetRepository.Find(id);

            if (timecard != null)
            {
                if (timecard.Status != TimecardStatus.Draft)
                {
                    return StatusCode(409, new InvalidStateError() { });
                }

                if (timecard.Lines.Count < 1)
                {
                    return StatusCode(409, new EmptyTimecardError() { });
                }

                if (CallerIdentity != timecard.Employee)
                {
                    return StatusCode(400, new InvalidIdentityError() { });
                }

                var transition = new Transition(submittal, TimecardStatus.Submitted);

                logger.LogInformation($"Adding submittal {transition}");

                timecard.Transitions.Add(transition);

                timesheetRepository.Update(timecard);

                return Ok(transition);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id:guid}/submittal")]
        [Produces(ContentTypes.Transition)]
        [ProducesResponseType(typeof(Transition), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(MissingTransitionError), 409)]
        public IActionResult GetSubmittal(Guid id)
        {
            logger.LogInformation($"Looking for timesheet {id}");

            Timecard timecard = timesheetRepository.Find(id);

            if (timecard != null)
            {
                if (timecard.Status == TimecardStatus.Submitted)
                {
                    var transition = timecard.Transitions
                                        .Where(t => t.TransitionedTo == TimecardStatus.Submitted)
                                        .OrderByDescending(t => t.OccurredAt)
                                        .FirstOrDefault();

                    return Ok(transition);
                }
                else
                {
                    return StatusCode(409, new MissingTransitionError() { });
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("{id:guid}/returns")]
        [Produces(ContentTypes.Transition)]
        [ProducesResponseType(typeof(Transition), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(InvalidStateError), 409)]
        public IActionResult ReturnToDraft(Guid id, [FromBody] Return reopen)
        {
            logger.LogInformation($"Looking for timesheet {id}");

            Timecard timecard = timesheetRepository.Find(id);

            if (timecard != null)
            {
                if (timecard.Status != TimecardStatus.Submitted)
                {
                    return StatusCode(409, new InvalidStateError() { });
                }

                //
                // this is an example of how you might solve the validation
                // requirement. however, this code is so integral to the overall
                // business requirements, and is called so often, that it
                // belongs in a lower-layer service
                //
                var employee = GetEmployee(timecard.Employee);
                if (employee == null)
                {
                    return StatusCode(400, new NoEmployeeFound() { });
                }
                if (employee.Status == PersonStatus.PastEmployee)
                {
                    return StatusCode(400, new EmployeeInactive() { });
                }

                var manager = GetManagerFor(timecard.Employee);
                if (manager == null)
                {
                    return StatusCode(400, new NoManagerFound() { });
                }

                if (CallerIdentity != manager.EmployeeId)
                {
                    return StatusCode(400, new InvalidIdentityError() { });
                }

                var transition = new Transition(reopen, TimecardStatus.Draft);

                logger.LogInformation($"Adding reopen {transition}");

                timecard.Transitions.Add(transition);

                timesheetRepository.Update(timecard);

                return Ok(transition);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id:guid}/returns")]
        [Produces(ContentTypes.Transition)]
        [ProducesResponseType(typeof(Transition), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(MissingTransitionError), 409)]
        public IActionResult GetReturn(Guid id)
        {
            logger.LogInformation($"Looking for timesheet {id}");

            Timecard timecard = timesheetRepository.Find(id);

            if (timecard != null)
            {
                if (timecard.Status == TimecardStatus.Draft)
                {
                    var transition = timecard.Transitions
                                        .Where(t => t.TransitionedTo == TimecardStatus.Draft)
                                        .OrderByDescending(t => t.OccurredAt)
                                        .FirstOrDefault();

                    return Ok(transition);
                }
                else
                {
                    return StatusCode(409, new MissingTransitionError() { });
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("{id:guid}/cancellation")]
        [Produces(ContentTypes.Transition)]
        [ProducesResponseType(typeof(Transition), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(InvalidStateError), 409)]
        [ProducesResponseType(typeof(EmptyTimecardError), 409)]
        public IActionResult Cancel(Guid id, [FromBody] Cancellation cancellation)
        {
            logger.LogInformation($"Looking for timesheet {id}");

            Timecard timecard = timesheetRepository.Find(id);

            if (timecard != null)
            {
                if (timecard.Status != TimecardStatus.Draft && timecard.Status != TimecardStatus.Submitted)
                {
                    return StatusCode(409, new InvalidStateError() { });
                }

                var transition = new Transition(cancellation, TimecardStatus.Cancelled);

                logger.LogInformation($"Adding cancellation transition {transition}");

                timecard.Transitions.Add(transition);

                timesheetRepository.Update(timecard);

                return Ok(transition);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id:guid}/cancellation")]
        [Produces(ContentTypes.Transition)]
        [ProducesResponseType(typeof(Transition), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(MissingTransitionError), 409)]
        public IActionResult GetCancellation(Guid id)
        {
            logger.LogInformation($"Looking for timesheet {id}");

            Timecard timecard = timesheetRepository.Find(id);

            if (timecard != null)
            {
                if (timecard.Status == TimecardStatus.Cancelled)
                {
                    var transition = timecard.Transitions
                                        .Where(t => t.TransitionedTo == TimecardStatus.Cancelled)
                                        .OrderByDescending(t => t.OccurredAt)
                                        .FirstOrDefault();

                    return Ok(transition);
                }
                else
                {
                    return StatusCode(409, new MissingTransitionError() { });
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("{id:guid}/rejection")]
        [Produces(ContentTypes.Transition)]
        [ProducesResponseType(typeof(Transition), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(InvalidStateError), 409)]
        [ProducesResponseType(typeof(EmptyTimecardError), 409)]
        public IActionResult Reject(Guid id, [FromBody] Rejection rejection)
        {
            logger.LogInformation($"Looking for timesheet {id}");

            Timecard timecard = timesheetRepository.Find(id);

            if (timecard != null)
            {
                if (timecard.Status != TimecardStatus.Submitted)
                {
                    return StatusCode(409, new InvalidStateError() { });
                }

                //
                // this is an example of how you might solve the validation
                // requirement. however, this code is so integral to the overall
                // business requirements, and is called so often, that it
                // belongs in a lower-layer service
                //
                var employee = GetEmployee(timecard.Employee);
                if (employee == null)
                {
                    return StatusCode(400, new NoEmployeeFound() { });
                }
                if (employee.Status == PersonStatus.PastEmployee)
                {
                    return StatusCode(400, new EmployeeInactive() { });
                }

                var manager = GetManagerFor(timecard.Employee);
                if (manager == null)
                {
                    return StatusCode(400, new NoManagerFound() { });
                }

                if (CallerIdentity != manager.EmployeeId)
                {
                    return StatusCode(400, new InvalidIdentityError() { });
                }

                var transition = new Transition(rejection, TimecardStatus.Rejected);

                logger.LogInformation($"Adding rejection transition {transition}");

                timecard.Transitions.Add(transition);

                timesheetRepository.Update(timecard);

                return Ok(transition);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id:guid}/rejection")]
        [Produces(ContentTypes.Transition)]
        [ProducesResponseType(typeof(Transition), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(MissingTransitionError), 409)]
        public IActionResult GetRejection(Guid id)
        {
            logger.LogInformation($"Looking for timesheet {id}");

            Timecard timecard = timesheetRepository.Find(id);

            if (timecard != null)
            {
                if (timecard.Status == TimecardStatus.Rejected)
                {
                    var transition = timecard.Transitions
                                        .Where(t => t.TransitionedTo == TimecardStatus.Rejected)
                                        .OrderByDescending(t => t.OccurredAt)
                                        .FirstOrDefault();

                    return Ok(transition);
                }
                else
                {
                    return StatusCode(409, new MissingTransitionError() { });
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("{id:guid}/approval")]
        [Produces(ContentTypes.Transition)]
        [ProducesResponseType(typeof(Transition), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(InvalidStateError), 409)]
        [ProducesResponseType(typeof(EmptyTimecardError), 409)]
        public IActionResult Approve(Guid id, [FromBody] Approval approval)
        {
            logger.LogInformation($"Looking for timesheet {id}");

            Timecard timecard = timesheetRepository.Find(id);

            if (timecard != null)
            {
                if (timecard.Status != TimecardStatus.Submitted)
                {
                    return StatusCode(409, new InvalidStateError() { });
                }

                //
                // this is an example of how you might solve the validation
                // requirement. however, this code is so integral to the overall
                // business requirements, and is called so often, that it
                // belongs in a lower-layer service
                //
                var employee = GetEmployee(timecard.Employee);
                if (employee == null)
                {
                    return StatusCode(400, new NoEmployeeFound() { });
                }
                if (employee.Status == PersonStatus.PastEmployee)
                {
                    return StatusCode(400, new EmployeeInactive() { });
                }

                var manager = GetManagerFor(timecard.Employee);
                if (manager == null)
                {
                    return StatusCode(400, new NoManagerFound() { });
                }

                if (CallerIdentity != manager.EmployeeId)
                {
                    return StatusCode(400, new InvalidIdentityError() { });
                }

                var transition = new Transition(approval, TimecardStatus.Approved);

                logger.LogInformation($"Adding approval transition {transition}");

                timecard.Transitions.Add(transition);

                timesheetRepository.Update(timecard);

                return Ok(transition);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id:guid}/approval")]
        [Produces(ContentTypes.Transition)]
        [ProducesResponseType(typeof(Transition), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(MissingTransitionError), 409)]
        public IActionResult GetApproval(Guid id)
        {
            logger.LogInformation($"Looking for timesheet {id}");

            Timecard timecard = timesheetRepository.Find(id);

            if (timecard != null)
            {
                if (timecard.Status == TimecardStatus.Approved)
                {
                    var transition = timecard.Transitions
                                        .Where(t => t.TransitionedTo == TimecardStatus.Approved)
                                        .OrderByDescending(t => t.OccurredAt)
                                        .FirstOrDefault();

                    return Ok(transition);
                }
                else
                {
                    return StatusCode(409, new MissingTransitionError() { });
                }
            }
            else
            {
                return NotFound();
            }
        }

        private Person GetEmployee(int employeeId)
        {
            // get the employee
            var employee = employeeRepository.Find(employeeId);

            return employee;
        }

        private Person GetManagerFor(int employeeId)
        {
            // get the employee
            var employee = employeeRepository.Find(employeeId);
            if (employee == null)
            {
                return null;
            }

            // get the manager
            var manager = employeeRepository.Find(employee.ManagerId);

            return manager;
        }
    }
}
