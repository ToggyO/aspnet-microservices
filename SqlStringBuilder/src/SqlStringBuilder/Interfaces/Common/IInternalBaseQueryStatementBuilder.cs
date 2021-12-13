using System.Collections.Generic;

using SqlStringBuilder.Internal.Components;

namespace SqlStringBuilder.Interfaces.Common
{
	/// <summary>
	/// Contains set of methods fot project internal usage
	/// </summary>
	/// <typeparam name="TQuery">Type of SQL builder.</typeparam>
	internal interface IInternalBaseQueryStatementBuilder<out TQuery> : IBaseQueryStatementBuilder
		where TQuery : IBaseQueryStatementBuilder
	{
		/// <summary>
		/// Set of components. Components represents parts of SQL statement.
		/// </summary>
		internal LinkedList<AbstractComponent> Components { get; }

		/// <summary>
		/// If the query already contains a component for the given component name,
		/// replace it with the specified component. Otherwise, just
		/// add the component.
		/// </summary>
		/// <param name="componentName">Component name.</param>
		/// <param name="component">Instance of component.</param>
		/// <returns></returns>
		TQuery AddOrReplaceComponent(string componentName, AbstractComponent component);

		/// <summary>
		/// Add a component to the query.
		/// </summary>
		/// <param name="componentName">Component name.</param>
		/// <param name="component">Instance of component.</param>
		/// <returns></returns>
		TQuery AddComponent(string componentName, AbstractComponent component);

		/// <summary>
		/// Get the list of components for a given component name.
		/// </summary>
		/// <param name="componentName">Component name.</param>
		/// <typeparam name="TComponent">Type of component, based on <see cref="AbstractComponent"/>.</typeparam>
		/// <returns></returns>
		List<TComponent> GetComponents<TComponent>(string componentName) where TComponent : AbstractComponent;

		/// <summary>
		/// Get single component for a given component name.
		/// </summary>
		/// <param name="componentName">Component name.</param>
		/// <typeparam name="TComponent">Type of component, based on <see cref="AbstractComponent"/>.</typeparam>
		/// <returns></returns>
		TComponent GetComponent<TComponent>(string componentName) where TComponent : AbstractComponent;

		/// <summary>
		/// Return whether the query has a component.
		/// </summary>
		/// <param name="componentName">Component name.</param>
		/// <typeparam name="TComponent">Type of component, based on <see cref="AbstractComponent"/>.</typeparam>
		/// <returns></returns>
		bool HasComponent<TComponent>(string componentName) where TComponent : AbstractComponent;
	}
}