using System;
using System.Threading;
using System.Threading.Tasks;
using HR.ATS.Domain.Opening;
using MediatR;

namespace HR.ATS.Command.Opening
{
    public class DeleteOpeningCommand: IRequest<Unit>
    {
        public DeleteOpeningCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }

    public class DeleteOpeningCommandHandler : IRequestHandler<DeleteOpeningCommand, Unit>
    {
        private readonly IOpeningRepository _openingRepository;

        public DeleteOpeningCommandHandler(IOpeningRepository openingRepository)
        {
            _openingRepository = openingRepository;
        }
        public async Task<Unit> Handle(DeleteOpeningCommand request, CancellationToken cancellationToken)
        {
            await _openingRepository.DeleteAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}