using ChinookApp.Models;

using Alyio.Extensions.Dapper;

namespace ChinookApp.Repositories
{
    // CREATE TABLE [Genre]
    // (
    //     [GenreId] INTEGER  NOT NULL,
    //     [Name] NVARCHAR(120),
    //     CONSTRAINT [PK_Genre] PRIMARY KEY  ([GenreId])
    // );
    public interface IGenreRepository : IRepository<Genre, int>
    {
        Task<Genre?> SelectByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<(int pageCount, IEnumerable<Genre> resultSet)> SelectPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    }
}