using System;
using System.Threading;
using System.Threading.Tasks;
using HR.ATS.Domain.Opening;
using HR.ATS.Domain.Opening.Events;
using MediatR;

namespace HR.ATS.Command.Opening
{
    public class DeleteOpeningCommand : IRequest<Unit>
    {
        public DeleteOpeningCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }

    public class DeleteOpeningCommandHandler : IRequestHandler<DeleteOpeningCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IOpeningRepository _openingRepository;

        public DeleteOpeningCommandHandler(IOpeningRepository openingRepository, IMediator mediator)
        {
            _openingRepository = openingRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteOpeningCommand request, CancellationToken cancellationToken)
        {
            await _openingRepository.DeleteAsync(request.Id, cancellationToken);
            await _mediator.Publish(new OpeningDeletedEvent(request.Id), cancellationToken);
            return Unit.Value;
        }
    }
}