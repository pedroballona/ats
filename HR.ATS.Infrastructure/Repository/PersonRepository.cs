using System.Threading.Tasks;
using HR.ATS.Domain.Person;
using HR.ATS.Infrastructure.Repository.Common;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HR.ATS.Infrastructure.Repository
{
    internal class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(IMongoDatabase database) : base(database)
        {
        }

        public async Task<Person?> CreatePersonIfUserDoesntExist(Person person)
        {
            var exists = await Collection.AsQueryable().AnyAsync(p => p.UserId == person.UserId);

            if (!exists) return await CreateAsync(person);

            return null;
        }
    }
}