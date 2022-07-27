using System.Data;

namespace Alyio.Extensions.Dapper.Sqlite.Tests
{
    public class SqlDefinitionTest
    {
        [Theory]
        [InlineData("selectbyid", 60, "select * from sometable where id = @id", CommandType.Text)]
        [InlineData("selectbyname", 120, "select * from sometable where name = @name order by name desc", CommandType.Text)]
        public void TestToString(string id, int timeout, string text, CommandType type)
        {
            // $"Id: {Id}, CommandTimeout: {CommandTimeout}, CommandText: {CommandText}, CommandType: {CommandType}"
            var selDef = new SelectDefinition
            {
                Id = id,
                CommandTimeout = timeout,
                CommandText = text,
                CommandType = type,
            };

            var exptected = $"Id: {id}, CommandTimeout: {timeout}, CommandText: {text}, CommandType: {type}";

            Assert.Equal(exptected, selDef.ToString());
        }
    }
}
