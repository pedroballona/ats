using System.Threading.Tasks;
using HR.ATS.Domain.Person;
using HR.ATS.Infrastructure.Repository.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Tnf.Runtime.Session;

namespace HR.ATS.Infrastructure.Repository
{
    internal class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public PersonRepository(IMongoDatabase database, IHttpContextAccessor contextAccessor) : base(database)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task<Person?> CreatePersonIfUserDoesntExist(Person person)
        {
            var exists = await Collection.AsQueryable().AnyAsync(p => p.UserId.Value == person.UserId.Value);

            if (!exists) return await CreateAsync(person);

            return null;
        }

        public async Task<Person?> GetLoggedPerson()
        {
            var userId = _contextAccessor.HttpContext.GetUserId();
            if (userId is null)
                return null;
            var person = await Collection.AsQueryable().Where(p => p.UserId.Value == userId).FirstOrDefaultAsync();
            return person;
        }
    }
}