using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HR.ATS.CrossCutting.Dto.Applicant;
using HR.ATS.Domain.Person;
using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HR.ATS.Query.Applicant
{
    public class GetResumeFromLoggedUserQuery : IRequest<ResumeDto?>
    {
    }

    internal class GetResumeFromLoggedUserQueryHandler : IRequestHandler<GetResumeFromLoggedUserQuery, ResumeDto?>
    {
        private readonly HttpContext _context;
        private readonly IMongoDatabase _database;

        public GetResumeFromLoggedUserQueryHandler(IMongoDatabase database, IHttpContextAccessor contextAccessor)
        {
            _database = database;
            _context = contextAccessor.HttpContext;
        }

        public async Task<ResumeDto?> Handle(GetResumeFromLoggedUserQuery request, CancellationToken cancellationToken)
        {
            var userId = _context.GetUserId();
            var personCollection = _database.GetCollection<Person>(nameof(Person));
            var applicantCollection =
                _database.GetCollection<Domain.Applicant.Applicant>(nameof(Domain.Applicant.Applicant));
            var query =
                from person in personCollection.AsQueryable()
                where person.UserId.Value == userId
                join applicant in applicantCollection.AsQueryable() on person.Id equals applicant.PersonReference.Id
                    into applicants
                from singleApplicant in applicants.DefaultIfEmpty()
                select singleApplicant.Resume;

            var result = await query.FirstOrDefaultAsync(cancellationToken);
            if (result is null) return null;

            return new ResumeDto
            {
                Experiences = result.Experiences.Items.Select(
                    e => new ExperienceDto
                    {
                        Company = e.Company,
                        Description = e.Description,
                        PeriodStartDate = e.Period.StartDate,
                        PeriodEndDate = e.Period.EndDate
                    }
                ),
                Introduction = result.Introduction
            };
        }
    }
}