using Alyio.Extensions.Dapper.Sqlite.Tests.Models;

using Dapper;

namespace Alyio.Extensions.Dapper.Sqlite.Tests.Repositories
{
    // CREATE TABLE [Genre]
    // (
    //     [GenreId] INTEGER  NOT NULL,
    //     [Name] NVARCHAR(120),
    //     CONSTRAINT [PK_Genre] PRIMARY KEY  ([GenreId])
    // );
    ///
    public class GenreRepository : Repository<Genre, int>, IGenreRepository
    {
        public GenreRepository(IStoreService<Genre, int> storeService) : base(storeService)
        {
        }

        public async Task<Genre?> SelectByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var parameters = new DynamicParameters();
            parameters.Add(nameof(Genre.Name), name);
            var genres = await Store.QueryAsync<Genre>(nameof(SelectByNameAsync), parameters, cancellationToken).ConfigureAwait(false);
            return genres.FirstOrDefault();
        }

        public Task<(int totalCount, IEnumerable<Genre>)> PageSelectAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return Store.PageQueryAsync<Genre>(nameof(SelectAllAsync), pageNumber, pageSize, cancellationToken);
        }
    }
}