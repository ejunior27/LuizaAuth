using Newtonsoft.Json;

namespace Domain.LuizaAuth.DTOs
{
    public class ErrorResponseDto
    {
        public string Error { get; set; }

        public ErrorResponseDto(string error)
        {
            Error = error;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(new { error = Error });
        }
    }
}
