using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Web.Middlewere;

public sealed class DomainExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception ex, CancellationToken ct)
    {
        var (status, title) = ex switch
        {
            BookingConflictException => (StatusCodes.Status409Conflict, "Конфликт бронирования"),
            DomainException => (StatusCodes.Status400BadRequest, "Нарушение правил домена"),
            _ => (0, "")
        };

        if (status == 0) return false;

        httpContext.Response.StatusCode = status;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = ex.Message
        }, ct);

        return true;
    }
}
