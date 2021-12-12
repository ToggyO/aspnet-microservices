namespace SqlStringBuilder.Internal.Components
{
	/// <summary>
	/// Represents an abstract "from" component.
	/// </summary>
	internal abstract class AbstractFrom : AbstractComponent
	{
		protected string _alias;

        /// <summary>
        /// Table name.
        /// </summary>
        public string Table { get; init; }

        /// <summary>
        /// Try to extract the Alias for the current component.
        /// </summary>
        /// <returns></returns>
        public virtual string Alias { get => _alias; set => _alias = value; }
	}

    /// <summary>
    /// Represents a "from" component.
    /// </summary>
    internal class FromComponent : AbstractFrom
    {
        /// <inheritdoc cref="AbstractFrom.Alias"/>.
        public override string Alias
        {
            get => _alias;
            set => _alias = value ?? Table;
        }
    }
}