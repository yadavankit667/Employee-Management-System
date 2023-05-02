namespace EMS.Models
{
    public class CommonResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public dynamic? Result { get; set; }
    }
}
