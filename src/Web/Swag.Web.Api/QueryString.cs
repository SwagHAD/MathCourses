using System.Text;
using SharedKernel.Contracts.Pagination;

namespace Swag.Web.Api
{
    internal static class QueryString
    {
        /// <summary>path?Page=..&amp;PageSize=..&amp;Filter.X=.. — пустые значения пропускаются.</summary>
        public static string ForPage<TFilter>(string path, PageRequest<TFilter> request)
            where TFilter : class, IQueryFilter, new()
        {
            IEnumerable<KeyValuePair<string, string?>> parameters =
            [
                new("Page", request.Page.ToString()),
                new("PageSize", request.PageSize.ToString()),
                new("SortBy", request.SortBy),
                new("Descending", request.Descending ? "true" : null),
                .. request.Filter.ToQuery()
            ];

            var builder = new StringBuilder(path);
            var separator = '?';
            foreach (var (key, value) in parameters)
            {
                if (string.IsNullOrEmpty(value))
                    continue;
                builder.Append(separator).Append(Uri.EscapeDataString(key))
                       .Append('=').Append(Uri.EscapeDataString(value));
                separator = '&';
            }
            return builder.ToString();
        }
    }
}
