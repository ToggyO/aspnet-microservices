using System.Collections.Generic;
using System.Linq;

using SqlStringBuilder.Common;
using SqlStringBuilder.Interfaces.Common;
using SqlStringBuilder.Internal.Components;
using SqlStringBuilder.Internal.Enums;

namespace SqlStringBuilder.Internal
{
	/// <summary>
	/// Represents abstract SQL statement builder
	/// </summary>
	internal abstract class AbstractQueryStatementBuilder<TQuery> : IBaseQueryStatementBuilder,
		IInternalBaseQueryStatementBuilder<TQuery>
		where TQuery : AbstractQueryStatementBuilder<TQuery>
	{
		private LinkedList<AbstractComponent> _components = new ();

		LinkedList<AbstractComponent> IInternalBaseQueryStatementBuilder<TQuery>.Components => _components;

		/// <inheritdoc cref="IBaseQueryStatementBuilder.QueryType"/>.
		public SqlStatementTypes QueryType { get; init; }

		/// <summary>
		/// Set of components. Components represents parts of SQL statement.
		/// </summary>
		internal LinkedList<AbstractComponent> Components => _components;

		/// <inheritdoc cref="IBaseQueryStatementBuilder.Build"/>.
		public SqlResult Build()
		{
			return new SqlResult();
		}

		/// <inheritdoc cref="IBaseQueryStatementBuilder.RawSql"/>.
		public string RawSql(string sql)
		{
			return string.Empty;
		}

		/// <summary>
        /// If the query already contains a component for the given component name,
        /// replace it with the specified component. Otherwise, just
        /// add the component.
        /// </summary>
        /// <param name="componentName">Component name.</param>
        /// <param name="component">Instance of component.</param>
        /// <returns></returns>
		public TQuery AddOrReplaceComponent(string componentName, AbstractComponent component)
        {
            var current = GetComponents<AbstractComponent>(componentName).FirstOrDefault();
            if (current is not null)
                Components.Remove(current);

            return AddComponent(componentName, component);
        }

        /// <summary>
        /// Add a component to the query.
        /// </summary>
        /// <param name="componentName">Component name.</param>
        /// <param name="component">Instance of component.</param>
        /// <returns></returns>
        public TQuery AddComponent(string componentName, AbstractComponent component)
        {
            component.ComponentName = componentName;
            Components.AddLast(component);
            return (TQuery)this;
        }

        /// <summary>
        /// Get the list of components for a given component name.
        /// </summary>
        /// <param name="componentName">Component name.</param>
        /// <typeparam name="TComponent">Type of component, based on <see cref="AbstractComponent"/>.</typeparam>
        /// <returns></returns>
        public List<TComponent> GetComponents<TComponent>(string componentName) where TComponent : AbstractComponent
        {
            var components = Components.AsEnumerable()
                .Where(x => x.ComponentName == componentName)
                .Cast<TComponent>();
            return components.ToList();
        }

        /// <summary>
        /// Get single component for a given component name.
        /// </summary>
        /// <param name="componentName">Component name.</param>
        /// <typeparam name="TComponent">Type of component, based on <see cref="AbstractComponent"/>.</typeparam>
        /// <returns></returns>
        public TComponent GetComponent<TComponent>(string componentName) where TComponent : AbstractComponent
			=> GetComponents<TComponent>(componentName).FirstOrDefault();

        /// <summary>
        /// Return whether the query has a component.
        /// </summary>
        /// <param name="componentName">Component name.</param>
        /// <typeparam name="TComponent">Type of component, based on <see cref="AbstractComponent"/>.</typeparam>
        /// <returns></returns>
        public bool HasComponent<TComponent>(string componentName) where TComponent : AbstractComponent
	        => GetComponents<TComponent>(componentName).Any();
	}
}