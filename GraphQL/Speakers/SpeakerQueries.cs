using System.Linq;
using HotChocolate;
using ConferencePlanner.GraphQL.Data;
using Microsoft.EntityFrameworkCore;
using ConferencePlanner.GraphQL.DataLoader;

namespace ConferencePlanner.GraphQL
{
    [ExtendObjectType("Query")]
    public class SpeakerQueries
    {
        [UseApplicationDbContext]
        [UsePaging]
        public Task<List<Speaker>> GetSpeakers(
            [ScopedService] ApplicationDbContext context) =>
            context.Speakers.OrderBy(t => t.Name).ToListAsync();

        public Task<Speaker> GetSpeakerByIdAsync(
            [ID(nameof(Speaker))]int id,
            SpeakerByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
            dataLoader.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Speaker>> GetSpeakersByIdAsync(
            [ID(nameof(Speaker))]int[] ids,
            SpeakerByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
            await dataLoader.LoadAsync(ids, cancellationToken);
    }
}