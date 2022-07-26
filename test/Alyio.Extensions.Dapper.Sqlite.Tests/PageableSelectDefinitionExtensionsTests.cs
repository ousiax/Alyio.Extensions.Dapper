namespace Alyio.Extensions.Dapper.Sqlite.Tests
{
    public class PageableSelectDefinitionExtensionsTests
    {
        [Fact]
        public Task TestThrowArgumentExceptionAsync()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                return new SelectDefinition().AsPageable(0, -5);
            });

            Assert.Equal("The pageSize must be greater than zero.", exception.Message);

            return Task.CompletedTask;
        }

        [Fact]
        public Task TestThrowInvalidCastExceptionAsync()
        {
            var exception = Assert.Throws<InvalidCastException>(() =>
            {
                return new SelectDefinition { CommandType = System.Data.CommandType.StoredProcedure }.AsPageable(0, 5);
            });

            Assert.Contains("must be CommandType.Text", exception.Message);

            return Task.CompletedTask;
        }
    }
}
