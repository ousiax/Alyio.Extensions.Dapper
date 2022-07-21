using ChinookApp.Models;

using Dapper.Extensions.DataAccess;

namespace ChinookApp.Repositories;

// CREATE TABLE [Genre]
// (
//     [GenreId] INTEGER  NOT NULL,
//     [Name] NVARCHAR(120),
//     CONSTRAINT [PK_Genre] PRIMARY KEY  ([GenreId])
// );
public interface IGenreRepository : IRepository<Genre, int>
{
    Task<Genre> SelectByNameAsync(string name, CancellationToken cancellationToken = default);
}