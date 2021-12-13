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
	internal abstract class AbstractQueryStatementBuilder<TQuery> : IInternalBaseQueryStatementBuilder<TQuery>
		where TQuery : AbstractQueryStatementBuilder<TQuery>
	{
		private LinkedList<AbstractComponent> _components = new ();

		LinkedList<AbstractComponent> IInternalBaseQueryStatementBuilder<TQuery>.Components => _components;

		/// <inheritdoc cref="IInternalBaseQueryStatementBuilder{TQuery}.Components"/>.
		internal LinkedList<AbstractComponent> Components => _components;

		/// <inheritdoc cref="IBaseQueryStatementBuilder.QueryType"/>.
		public SqlStatementTypes QueryType { get; init; }

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

		/// <inheritdoc cref="IInternalBaseQueryStatementBuilder{TQuery}.AddOrReplaceComponent"/>.
		public TQuery AddOrReplaceComponent(string componentName, AbstractComponent component)
        {
            var current = GetComponents<AbstractComponent>(componentName).FirstOrDefault();
            if (current is not null)
                Components.Remove(current);

            return AddComponent(componentName, component);
        }

		/// <inheritdoc cref="IInternalBaseQueryStatementBuilder{TQuery}.AddComponent"/>.
		public TQuery AddComponent(string componentName, AbstractComponent component)
        {
            component.ComponentName = componentName;
            Components.AddLast(component);
            return (TQuery)this;
        }

		/// <inheritdoc cref="IInternalBaseQueryStatementBuilder{TQuery}.GetComponents{TComponent}"/>.
		public List<TComponent> GetComponents<TComponent>(string componentName) where TComponent : AbstractComponent
        {
            var components = Components.AsEnumerable()
                .Where(x => x.ComponentName == componentName)
                .Cast<TComponent>();
            return components.ToList();
        }

		/// <inheritdoc cref="IInternalBaseQueryStatementBuilder{TQuery}.GetComponent{TComponent}"/>.
		public TComponent GetComponent<TComponent>(string componentName) where TComponent : AbstractComponent
			=> GetComponents<TComponent>(componentName).FirstOrDefault();

		/// <inheritdoc cref="IInternalBaseQueryStatementBuilder{TQuery}.HasComponent{TComponent}"/>.
		public bool HasComponent<TComponent>(string componentName) where TComponent : AbstractComponent
	        => GetComponents<TComponent>(componentName).Any();
	}
}