namespace ChinookApp.Models;

// CREATE TABLE [Genre]
// (
//     [GenreId] INTEGER  NOT NULL,
//     [Name] NVARCHAR(120),
//     CONSTRAINT [PK_Genre] PRIMARY KEY  ([GenreId])
// );
public class Genre
{
    public int? GenreId { get; set; }

    public string? Name { get; set; }
}