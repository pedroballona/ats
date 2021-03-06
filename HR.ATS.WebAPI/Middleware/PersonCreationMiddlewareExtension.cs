using System.Linq;
using System.Threading.Tasks;
using HR.ATS.Command.Person;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace HR.ATS.WebAPI.Middleware
{
    public static class PersonCreationMiddlewareExtension
    {
        public static IApplicationBuilder UseAutomaticPersonCreation(this IApplicationBuilder app)
        {
            app.UseMiddleware<PersonCreationMiddleware>();
            return app;
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class PersonCreationMiddleware
        {
            private readonly RequestDelegate _next;

            public PersonCreationMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task Invoke(
                HttpContext httpContext,
                IMediator mediator,
                IMemoryCache memoryCache
            )
            {
                var userId = httpContext.GetUserId();
                if (userId is null)
                {
                    await _next(httpContext);
                    return;
                }

                var key = "Has_PersonCreatedFor_" + userId;
                if (!memoryCache.TryGetValue(key, out bool _))
                {
                    var name = httpContext.User.Claims.Where(c => c.Type == "name").Select(c => c.Value).Last();
                    var email = httpContext.User.Claims.Where(c => c.Type == "email").Select(c => c.Value).Last();
                    var personCommand = new CreatePersonWhenUserDoesntExistsCommand(name, email, userId.Value);
                    await mediator.Send(personCommand);
                    memoryCache.Set(key, true);
                }

                await _next(httpContext);
            }
        }
    }
}