namespace ChinookApp.Models;

/// <summary>
/// CREATE TABLE [Album]
/// (
///     [AlbumId] INTEGER  NOT NULL,
///     [Title] NVARCHAR(160)  NOT NULL,
///     [ArtistId] INTEGER  NOT NULL,
///     CONSTRAINT [PK_Album] PRIMARY KEY  ([AlbumId]),
///     FOREIGN KEY ([ArtistId]) REFERENCES [Artist] ([ArtistId]) 
/// 		ON DELETE NO ACTION ON UPDATE NO ACTION
/// );
/// </summary>
public class Album
{
    public int? AlbumId { get; set; }
    public string? Title { get; set; }
    public int ArtistId { get; set; }
}