namespace Alyio.Extensions.Dapper.Sqlite.Tests.Models
{
    // CREATE TABLE [Customer]
    // (
    //     [CustomerId] INTEGER  NOT NULL,
    //     [FirstName] NVARCHAR(40)  NOT NULL,
    //     [LastName] NVARCHAR(20)  NOT NULL,
    //     [Company] NVARCHAR(80),
    //     [Address] NVARCHAR(70),
    //     [City] NVARCHAR(40),
    //     [State] NVARCHAR(40),
    //     [Country] NVARCHAR(40),
    //     [PostalCode] NVARCHAR(10),
    //     [Phone] NVARCHAR(24),
    //     [Fax] NVARCHAR(24),
    //     [Email] NVARCHAR(60)  NOT NULL,
    //     [SupportRepId] INTEGER,
    //     CONSTRAINT [PK_Customer] PRIMARY KEY  ([CustomerId]),
    //     FOREIGN KEY ([SupportRepId]) REFERENCES [Employee] ([EmployeeId]) 
    // 		ON DELETE NO ACTION ON UPDATE NO ACTION
    // );
    public class Customer
    {
        public int? CustomerId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Company { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

        public string? PostalCode { get; set; }

        public string? Phone { get; set; }

        public string? Fax { get; set; }

        public string? Email { get; set; }

        public string? SupportRepId { get; set; }
    }
}