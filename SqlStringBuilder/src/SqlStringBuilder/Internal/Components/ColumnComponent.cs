namespace SqlStringBuilder.Internal.Components
{
	internal class AbstractColumn : AbstractComponent {}

	internal class SelectComponent : AbstractColumn
	{
		public string Name { get; set; }
	}
}