namespace GreenUp.Web.Mvc.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public int Status { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
    }
}
