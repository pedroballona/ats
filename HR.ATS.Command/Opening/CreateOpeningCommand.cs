using System.Threading;
using System.Threading.Tasks;
using HR.ATS.CrossCutting.Dto.Opening;
using HR.ATS.Domain.Opening;
using MediatR;

namespace HR.ATS.Command.Opening
{
    public class CreateOpeningCommand : IRequest<OpeningDTO>
    {
        public CreateOpeningCommand(OpeningDTO opening)
        {
            Opening = opening;
        }

        public OpeningDTO Opening { get; }
    }

    internal class CreateOpeningCommandHandler : IRequestHandler<CreateOpeningCommand, OpeningDTO>
    {
        private readonly IOpeningRepository _openingRepository;

        public CreateOpeningCommandHandler(IOpeningRepository openingRepository)
        {
            _openingRepository = openingRepository;
        }

        public async Task<OpeningDTO> Handle(CreateOpeningCommand request, CancellationToken cancellationToken)
        {
            var opening = new Domain.Opening.Opening(request.Opening.Name, request.Opening.Description, true);
            await _openingRepository.CreateAsync(opening, cancellationToken);
            return request.Opening;
        }
    }
}