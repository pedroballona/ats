using System.Threading.Tasks;
using HR.ATS.Domain.Common;

namespace HR.ATS.Domain.Person
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<Person?> CreatePersonIfUserDoesntExist(Person person);
        Task<Person?> GetLoggedPerson();
    }
}