namespace SqlStringBuilder.Internal.Components
{
	/// <summary>
	/// Abstract condition
	/// </summary>
	internal abstract class AbstractCondition : AbstractComponent
	{
		/// <summary>
		/// "OR" operator indicator.
		/// </summary>
		public bool IsOr { get; set; } = false;

		/// <summary>
		/// "NOT" operator indicator.
		/// </summary>
		public bool IsNot { get; set; } = false;
	}

    /// <summary>
    /// Represents a comparison between a column and a value.
    /// </summary>
    internal class BasicCondition<TValue> : AbstractCondition
    {
	    /// <summary>
        /// Column name.
        /// </summary>
        public string Column { get; set; }

        /// <summary>
        /// Comparison operator.
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Value to compare with.
        /// </summary>
        public TValue Value { get; set; }
    }

    internal class NullCondition : AbstractCondition
    {
	    /// <summary>
	    /// Column name.
	    /// </summary>
	    public string Column { get; set; }
    }
}