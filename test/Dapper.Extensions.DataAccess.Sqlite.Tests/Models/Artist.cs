namespace Dapper.Extensions.DataAccess.Sqlite.Tests.Models;

// CREATE TABLE [Artist]
// (
//     [ArtistId] INTEGER  NOT NULL,
//     [Name] NVARCHAR(120),
//     CONSTRAINT [PK_Artist] PRIMARY KEY  ([ArtistId])
// );
public class Artist
{
    public int? ArtistId { get; set; }

    public string? Name { get; set; }
}