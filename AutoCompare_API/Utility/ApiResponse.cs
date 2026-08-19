using System.Net;

namespace AutoCompare_API.Utility
{
    // To standardize the API responses across the application
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; } = [];
        public object? Result { get; set; }
    }
}
