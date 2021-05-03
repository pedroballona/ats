using System.Threading;
using System.Threading.Tasks;
using HR.ATS.CrossCutting.Dto.Opening;
using HR.ATS.Domain.Opening;
using MediatR;

namespace HR.ATS.Command.Opening
{
    public class CreateOpeningCommand : IRequest<OpeningDto>
    {
        public CreateOpeningCommand(OpeningDto opening)
        {
            Opening = opening;
        }

        public OpeningDto Opening { get; }
    }

    internal class CreateOpeningCommandHandler : IRequestHandler<CreateOpeningCommand, OpeningDto>
    {
        private readonly IOpeningRepository _openingRepository;

        public CreateOpeningCommandHandler(IOpeningRepository openingRepository)
        {
            _openingRepository = openingRepository;
        }

        public async Task<OpeningDto> Handle(CreateOpeningCommand request, CancellationToken cancellationToken)
        {
            var opening = new Domain.Opening.Opening(request.Opening.Name!, request.Opening.Description!, true);
            await _openingRepository.CreateAsync(opening, cancellationToken);
            return request.Opening;
        }
    }
}