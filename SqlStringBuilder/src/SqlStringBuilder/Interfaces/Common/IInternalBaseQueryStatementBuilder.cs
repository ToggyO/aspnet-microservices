using System.Collections.Generic;

using SqlStringBuilder.Internal.Components;

namespace SqlStringBuilder.Interfaces.Common
{
	internal interface IInternalBaseQueryStatementBuilder<out TQuery>
		where TQuery : IBaseQueryStatementBuilder
	{
		internal LinkedList<AbstractComponent> Components { get; }

		TQuery AddOrReplaceComponent(string componentName, AbstractComponent component);

		TQuery AddComponent(string componentName, AbstractComponent component);

		List<TComponent> GetComponents<TComponent>(string componentName) where TComponent : AbstractComponent;

		TComponent GetComponent<TComponent>(string componentName) where TComponent : AbstractComponent;

		bool HasComponent<TComponent>(string componentName) where TComponent : AbstractComponent;
	}
}