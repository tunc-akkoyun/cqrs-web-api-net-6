namespace Store.Presentation.Models;

public record ErrorResponse(string Message, DetailedErrorResponse[] Errors);

public record DetailedErrorResponse(string PropertyName, string ErrorMessage);