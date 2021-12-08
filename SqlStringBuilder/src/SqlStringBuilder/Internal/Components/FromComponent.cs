namespace SqlStringBuilder.Internal.Components
{
    /// <summary>
    /// Represents a "from" component.
    /// </summary>
    internal class FromComponent : AbstractComponent
    {
        private readonly string _alias;

        /// <summary>
        /// Table name.
        /// </summary>
        public string Table { get; init; }

        /// <summary>
        /// Table alias.
        /// </summary>
        public string Alias
        {
            get => _alias;
            init => _alias = value ?? Table;
        }
    }
}