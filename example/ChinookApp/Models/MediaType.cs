namespace ChinookApp.Models;

// CREATE TABLE [MediaType]
// (
//     [MediaTypeId] INTEGER  NOT NULL,
//     [Name] NVARCHAR(120),
//     CONSTRAINT [PK_MediaType] PRIMARY KEY  ([MediaTypeId])
// );
public class MediaType
{
    public int? MediaTypeId { get; set; }

    public string? Name { get; set; }
}