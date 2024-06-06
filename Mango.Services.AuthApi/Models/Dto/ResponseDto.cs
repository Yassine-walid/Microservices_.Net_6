namespace Mango.Services.AuthApi.Models.Dto
{
    public class ResponseDto
    {
        public Object? Result { get; set; }
        public bool? IsSuccess { get; set; } = true;
        public String? Message { get; set; } = string.Empty;
    }
}
