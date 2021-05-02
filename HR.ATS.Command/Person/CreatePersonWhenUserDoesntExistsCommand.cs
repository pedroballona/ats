using System.Threading;
using System.Threading.Tasks;
using HR.ATS.Domain.Person;
using MediatR;

namespace HR.ATS.Command.Person
{
    public class CreatePersonWhenUserDoesntExistsCommand : IRequest<Unit>
    {
        public CreatePersonWhenUserDoesntExistsCommand(
            string name,
            string email,
            long userId
        )
        {
            Name = name;
            Email = email;
            UserId = userId;
        }

        public string Name { get; }
        public string Email { get; }
        public long UserId { get; }
    }

    internal class
        CreatePersonWhenUserDoesntExistsCommandHandler : IRequestHandler<CreatePersonWhenUserDoesntExistsCommand, Unit>
    {
        private readonly IPersonRepository _personRepository;

        public CreatePersonWhenUserDoesntExistsCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Unit> Handle(
            CreatePersonWhenUserDoesntExistsCommand request,
            CancellationToken cancellationToken
        )
        {
            var person = new Domain.Person.Person(request.Name, request.Email, request.UserId);
            await _personRepository.CreatePersonIfUserDoesntExist(person);
            return Unit.Value;
        }
    }
}