namespace SqlStringBuilder.Internal.Components
{
	/// <summary>
	/// Represents an abstract "select" component.
	/// </summary>
	internal class AbstractColumn : AbstractComponent {}

	/// <summary>
	/// Represents a "select" component. 
	/// </summary>
	internal class Column : AbstractColumn
	{
		public string Name { get; set; }
	}
}