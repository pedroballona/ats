using System.Threading;
using System.Threading.Tasks;
using HR.ATS.Domain.Opening;
using HR.ATS.Domain.Opening.Events;
using MediatR;

namespace HR.ATS.Command.Opening
{
    public class OpeningDeletedEventHandler: INotificationHandler<OpeningDeletedEvent>
    {
        private readonly IApplicationRepository _applicationRepository;

        public OpeningDeletedEventHandler(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }
        public async Task Handle(OpeningDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _applicationRepository.DeleteAllApplicationsFromOpening(notification.OpeningId);
        }
    }
}