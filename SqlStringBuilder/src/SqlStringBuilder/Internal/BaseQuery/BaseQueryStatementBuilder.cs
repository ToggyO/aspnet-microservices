using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using SqlStringBuilder.Interfaces.Common;
using SqlStringBuilder.Internal.Components;

namespace SqlStringBuilder.Internal.BaseQuery
{
    /// <inheritdoc cref="IBaseQueryStatementBuilder"/>.
    internal abstract partial class BaseQueryStatementBuilder<TQuery>: AbstractQueryStatementBuilder
	    where TQuery : BaseQueryStatementBuilder<TQuery>
    {
	    /// <summary>
        /// If the query already contains a component for the given component name,
        /// replace it with the specified component. Otherwise, just
        /// add the component.
        /// </summary>
        /// <param name="componentName">Component name.</param>
        /// <param name="component">Instance of component.</param>
        /// <returns></returns>
        internal TQuery AddOrReplaceComponent(string componentName, AbstractComponent component)
        {
            var current= GetComponents<AbstractComponent>(componentName).FirstOrDefault();
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
        internal TQuery AddComponent(string componentName, AbstractComponent component)
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
        internal List<TComponent> GetComponents<TComponent>(string componentName) where TComponent : AbstractComponent
        {
            var components = Components.AsEnumerable()
                .Where(x => x.ComponentName == componentName)
                .Cast<TComponent>();
            return components.ToList();
        }

        // TODO: check
        protected string ExtractAlias(string str)
        {
            string result = Regex.Replace(str, @"[\.\-\\_\s]", string.Empty);
            return string.Concat(
                result.Substring(0, 1).ToUpper(),
                result.Substring(1));
        }
    }
}