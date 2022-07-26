using System.Data;
using System.Globalization;

namespace Alyio.Extensions.Dapper
{
    /// <summary>
    /// Extension methods for <see cref="SelectDefinition"/>.
    /// </summary>
    internal static class PageableSelectDefinitionExtensions
    {
        private static readonly string PageableTextForamt = "SELECT COUNT(*) FROM ({0}) original_query; {0} LIMIT {1} OFFSET {2};";

        /// <summary>
        /// Converts a <see cref="SelectDefinition"/> to a pageable instance.
        /// </summary>
        /// <param name="def">The <see cref="SelectDefinition"/>.</param>
        /// <param name="pageNumber">The page number based-zero. If the page number is less than zero, it will be reset to zero.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>A pageable select sql definition to this instance.</returns>
        /// <exception cref="InvalidCastException">The command type of <paramref name="def"/> must be <see cref="CommandType.Text"/>.</exception>
        /// <exception cref="ArgumentException">The page size must be greater than zero.</exception>
        public static SelectDefinition AsPageable(this SelectDefinition def, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                pageNumber = 0;
            }

            if (pageSize <= 0)
            {
                throw new ArgumentException($"The {nameof(pageSize)} must be greater than zero.");
            }

            if (def.CommandType != CommandType.Text)
            {
                throw new InvalidCastException($"The command type of {nameof(def)} must be CommandType.Text");
            }

            var pageable = new SelectDefinition
            {
                Id = def.Id,
                CommandText = string.Format(CultureInfo.InvariantCulture, PageableTextForamt, def.CommandText, pageSize, pageNumber * pageSize),
                CommandTimeout = def.CommandTimeout,
                CommandType = def.CommandType,
                IdName = def.IdName,
                OpenMode = def.OpenMode,
            };

            return pageable;
        }
    }
}
