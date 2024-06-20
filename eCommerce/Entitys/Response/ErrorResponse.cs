using System.Collections.Generic;

namespace eCommerce.Entitys.Response
{
    public class ErrorResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ErrorResponse(string errorMessage) : this(new List<string>() { errorMessage }) { }

        public ErrorResponse(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
