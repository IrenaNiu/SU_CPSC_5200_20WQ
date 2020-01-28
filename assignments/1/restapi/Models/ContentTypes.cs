namespace restapi.Models
{
    public class ContentTypes
    {
        private ContentTypes() {}

        public const string Root = "application/com.my-company.my-product.root+json";

        public const string Timesheet = "application/com.my-company.my-product.timesheet+json";

        public const string Timesheets = "application/com.my-company.my-product.timesheets+json";

        public const string TimesheetLine = "application/com.my-company.my-product.timesheet-line+json";

        public const string TimesheetLines = "application/com.my-company.my-product.timesheet-lines+json";
    
        public const string Transitions = "application/com.my-company.my-product.timesheet-transitions+json";

        public const string Transition = "application/com.my-company.my-product.timesheet-transition+json";
    
        public const string Error = "application/com.my-company.my-product.simple-error+json";

        public const string Cancellation = "application/com.my-company.my-product.timesheet-cancellation+json";
        
        public const string Submittal = "application/com.my-company.my-product.timesheet-submittal+json";
        
        public const string Rejection = "application/com.my-company.my-product.timesheet-rejection+json";
        
        public const string Approval = "application/com.my-company.my-product.timesheet-approval+json";
    }
}