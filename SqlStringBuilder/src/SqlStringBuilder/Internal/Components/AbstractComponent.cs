namespace SqlStringBuilder.Internal.Components
{
    /// <summary>
    /// Base SQL statement component.
    /// </summary>
    internal abstract class AbstractComponent
    {
        /// <summary>
        /// Component type e.g. "from", "select", "where".
        /// </summary>
        public string ComponentName { get; set; }
    }
}