namespace API.Utilities.Handlers
{
    public class ResponseValidationHandler
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string [] Errors { get; set; } //ini akan memunculkan error lebih dari 1
    }
}
