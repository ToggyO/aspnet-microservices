namespace SqlStringBuilder.Internal.Components
{
    /// <summary>
    /// Represents a comparison between a column and a value.
    /// </summary>
    internal class ConditionComponent<TValue> : AbstractComponent
    {
        /// <summary>
        /// "OR" operator indicator.
        /// </summary>
        public bool IsOr { get; set; } = false;

        /// <summary>
        /// "NOT" operator indicator.
        /// </summary>
        public bool IsNot { get; set; } = false;

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
}