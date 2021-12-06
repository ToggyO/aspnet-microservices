using System.Text;
using System.Text.RegularExpressions;

using SqlStringBuilder.Interfaces;

namespace SqlStringBuilder.Internal
{
    /// <inheritdoc cref="IBaseQueryStatementBuilder"/>.
    public abstract class BaseQueryStatementBuilder : IBaseQueryStatementBuilder
    {
        protected StringBuilder Builder;

        public string Build()
        {
            return Builder.ToString();
        }

        protected string ExtractAlias(string str)
        {
            string result = Regex.Replace(str, @"[\.\-\\_\s]", string.Empty);
            return string.Concat(
                result.Substring(0, 1).ToUpper(),
                result.Substring(1));
        }
    }
}