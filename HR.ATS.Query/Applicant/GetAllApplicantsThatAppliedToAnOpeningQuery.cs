using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HR.ATS.CrossCutting.Dto.Applicant;
using HR.ATS.Domain.Opening;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HR.ATS.Query.Applicant
{
    public class GetAllApplicantsThatAppliedToAnOpeningQuery : IRequest<IEnumerable<SimpleApplicantDto>>
    {
        public GetAllApplicantsThatAppliedToAnOpeningQuery(Guid openingId, string? filter)
        {
            OpeningId = openingId;
            Filter = filter?.Trim();
        }

        public Guid OpeningId { get; }
        public string? Filter { get; }
    }

    public class GetAllApplicantsThatAppliedToAnOpeningQueryHandler : IRequestHandler<
        GetAllApplicantsThatAppliedToAnOpeningQuery, IEnumerable<SimpleApplicantDto>>
    {
        private readonly IMongoDatabase _database;

        public GetAllApplicantsThatAppliedToAnOpeningQueryHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<IEnumerable<SimpleApplicantDto>> Handle(
            GetAllApplicantsThatAppliedToAnOpeningQuery request,
            CancellationToken cancellationToken
        )
        {
            var applicantCollection =
                _database.GetCollection<Domain.Applicant.Applicant>(nameof(Domain.Applicant.Applicant));
            var applicationCollection = _database.GetCollection<Application>(nameof(Application));
            var hasFilter = !string.IsNullOrWhiteSpace(request.Filter);
            var applicantQuery = applicantCollection.AsQueryable();
            var query =
                from application in applicationCollection.AsQueryable()
                where application.OpeningId == request.OpeningId
                join applicant in applicantQuery on application.ApplicantId equals applicant.Id into applicants
                from selectedApplicant in applicants
                select new SimpleApplicantDto
                {
                    Name = selectedApplicant.PersonReference.Name.Value,
                    Id = selectedApplicant.Id
                };
            if (hasFilter)
                query = query.Where(a => a.Name!.ToLower().Contains(request.Filter!));
            var result = await query.ToListAsync(cancellationToken);
            return result;
        }
    }
}