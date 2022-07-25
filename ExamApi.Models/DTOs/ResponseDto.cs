namespace ExamApi.Models.DTOs;

public class ResponseDto
{
    public bool Success { get; set; }
    public string? Message { get; set; }

    public ResponseDto()
    {
        
    }

    public ResponseDto(bool success, string? message = null)
    {
        Success = success;
        Message = message;
    }
}