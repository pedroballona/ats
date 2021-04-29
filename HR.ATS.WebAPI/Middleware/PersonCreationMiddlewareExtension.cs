using System.Linq;
using System.Threading.Tasks;
using HR.ATS.Domain.Person;
using HR.ATS.Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace HR.ATS.WebAPI.Middleware
{
    public static class PersonCreationMiddlewareExtension
    {
        public static IApplicationBuilder UseAutomaticPersonCreation(this IApplicationBuilder app)
        {
            app.UseMiddleware<PersonCreationMiddleware>();
            return app;
        }

        private class PersonCreationMiddleware
        {
            private readonly RequestDelegate _next;

            public PersonCreationMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task Invoke(HttpContext httpContext, IPersonRepository personRepository)
            {
                var userId = httpContext.GetUserId();
                if (userId.HasValue)
                {
                    var name = httpContext.User.Claims.Where(c => c.Type == "name").Select(c => c.Value).Last();
                    var email = httpContext.User.Claims.Where(c => c.Type == "email").Select(c => c.Value).Last();
                    var person = new Person(name, email, userId.Value);
                    await personRepository.CreatePersonIfUserDoesntExist(person);
                }

                await _next(httpContext);
            }
        }
    }
}