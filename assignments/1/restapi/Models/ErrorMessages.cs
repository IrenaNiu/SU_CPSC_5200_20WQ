namespace restapi.Models
{
    public class InvalidStateError
    {
        public int ErrorCode { get => 100; }

        public string Message { get => "Transition not valid for current state"; }
    }

    public class EmptyTimecardError
    {
        public int ErrorCode { get => 101; }

        public string Message { get => "Unable to submit timecard with no lines"; }
    }

    public class MissingTransitionError
    {
        public int ErrorCode { get => 102; }

        public string Message { get => "No state transition of requested type present in timecard"; }
    }

    public class InvalidIdentityError
    {
        public int ErrorCode { get => 103; }

        public string Message { get => "The user account attempting the transition has no rights to perform that transition"; }
    }

    public class NoEmployeeFound
    {
        public int ErrorCode { get => 104; }

        public string Message { get => "Unable to find an employee record"; }
    }

    public class NoManagerFound
    {
        public int ErrorCode { get => 105; }

        public string Message { get => "Unable to find the employee's manager record"; }
    }

    public class EmployeeInactive
    {
        public int ErrorCode { get => 105; }

        public string Message { get => "Caller is not currently an active employee"; }
    }
}