using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HR.ATS.CrossCutting.Dto.Applicant;
using HR.ATS.Domain.Applicant;
using HR.ATS.Domain.Person;
using MediatR;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HR.ATS.Query
{
    public class GetResumeFromLoggedUserQuery : IRequest<ResumeDTO?>
    {
    }

    public class GetResumeFromLoggedUserQueryHandler : IRequestHandler<GetResumeFromLoggedUserQuery, ResumeDTO?>
    {
        private readonly IMongoDatabase _database;
        private readonly HttpContext _context;

        public GetResumeFromLoggedUserQueryHandler(IMongoDatabase database, IHttpContextAccessor contextAccessor)
        {
            _database = database;
            _context = contextAccessor.HttpContext;
        }

        public async Task<ResumeDTO?> Handle(GetResumeFromLoggedUserQuery request, CancellationToken cancellationToken)
        {
            var userId = _context.GetUserId();
            var personCollection = _database.GetCollection<Person>(nameof(Person));
            var applicantCollection = _database.GetCollection<Applicant>(nameof(Applicant));
            var query =
                from person in personCollection.AsQueryable()
                where person.UserId.Value == userId
                join applicant in applicantCollection.AsQueryable() on person.Id equals applicant.PersonReference.Id
                    into applicants
                from singleApplicant in applicants.DefaultIfEmpty()
                select singleApplicant.Resume;

            var result = await query.FirstOrDefaultAsync(cancellationToken: cancellationToken);
            if (result is null)
            {
                return null;
            }

            return new ResumeDTO
            {
                Experiences = result.Experiences.Items.Select(
                    e => new ExperienceDTO
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