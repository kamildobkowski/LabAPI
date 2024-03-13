using System.Diagnostics;
using LabAPI.Domain.Exceptions;

namespace LabAPI.Api.Middlewares;

public class ErrorHandlingMiddleware : IMiddleware
{
	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next.Invoke(context);
		}
		catch (NotFoundException e)
		{
			context.Response.StatusCode = StatusCodes.Status404NotFound;
			await context.Response.WriteAsync(e.Message);
			Debug.WriteLine(e.Message);
			Console.WriteLine(e.Message);
		}
		catch (Exception e)
		{
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;
			await context.Response.WriteAsync(e.Message);
			Debug.WriteLine(e.Message);
			Console.WriteLine(e.Message);
		}
	}
}