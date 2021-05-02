using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HR.ATS.CrossCutting.Dto.Opening;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HR.ATS.Query.Opening
{
    public class GetAllOpeningsQuery : IRequest<IEnumerable<OpeningDTO>>
    {
        public GetAllOpeningsQuery(string? filter = default)
        {
            Filter = filter?.Trim();
        }

        public string? Filter { get; }
    }

    internal class GetAllOpeningsQueryHandler : IRequestHandler<GetAllOpeningsQuery, IEnumerable<OpeningDTO>>
    {
        private readonly IMongoDatabase _database;

        public GetAllOpeningsQueryHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<IEnumerable<OpeningDTO>> Handle(
            GetAllOpeningsQuery request,
            CancellationToken cancellationToken
        )
        {
            var openingCollection = _database.GetCollection<Domain.Opening.Opening>(nameof(Domain.Opening.Opening));
            var filter = string.IsNullOrWhiteSpace(request.Filter) ? default : request.Filter;
            var result = await openingCollection.AsQueryable()
                                                .Where(
                                                    opening => filter == null ||
                                                               opening.Name.Value.ToLower().Contains(filter.ToLower())
                                                )
                                                .Select(
                                                    opening => new OpeningDTO
                                                    {
                                                        Name = opening.Name.Value,
                                                        Description = opening.Description.Value
                                                    }
                                                )
                                                .ToListAsync(cancellationToken);
            return result;
        }
    }
}