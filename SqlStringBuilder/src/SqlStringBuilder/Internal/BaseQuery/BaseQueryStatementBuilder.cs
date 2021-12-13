using System.Text.RegularExpressions;

using SqlStringBuilder.Interfaces.Common;

namespace SqlStringBuilder.Internal.BaseQuery
{
    /// <inheritdoc cref="IBaseQueryStatementBuilder"/>.
    internal abstract partial class BaseQueryStatementBuilder<TQuery> : AbstractQueryStatementBuilder<TQuery>
	    where TQuery : BaseQueryStatementBuilder<TQuery>
    {
	    private bool _orFlag = false;

	    private bool _notFlag = false;

	    /// <summary>
	    /// Indicates whether next condition component as "AND" statement.
	    /// </summary>
	    /// <returns></returns>
	    public TQuery And()
        {
	        _orFlag = false;
	        return (TQuery)this;
        }

	    /// <summary>
	    /// Indicates whether next condition component as "AND" statement.
	    /// </summary>
	    /// <returns></returns>
        public TQuery Or()
        {
	        _orFlag = true;
	        return (TQuery)this;
        }

	    /// <summary>
	    /// Indicates whether next condition component as "NOT" statement.
	    /// </summary>
	    /// <param name="flag">Whether to include "NOT" condition.</param>
	    /// <returns></returns>
        public TQuery Not(bool flag = true)
        {
	        _notFlag = flag;
	        return (TQuery)this;
        }

	    /// <summary>
	    /// Retrieves "OR" flag value and resets it.
	    /// </summary>
	    /// <returns></returns>
        internal bool GetOr()
        {
	        bool val = _orFlag;
	        // Reset the flag
	        _orFlag = false;
	        return val;
        }

	    /// <summary>
	    /// Retrieves "NOT" flag value and resets it.
	    /// </summary>
	    /// <returns></returns>
        internal bool GetNot()
        {
	        bool val = _notFlag;
	        // Reset the flag
	        _notFlag = false;
	        return val;
        }

        // TODO: check
        protected string ExtractAlias(string str)
        {
            string result = Regex.Replace(str, @"[\.\-\\_\s]", string.Empty);
            return string.Concat(
                result.Substring(0, 1).ToUpper(),
                result.Substring(1));
        }
    }
}