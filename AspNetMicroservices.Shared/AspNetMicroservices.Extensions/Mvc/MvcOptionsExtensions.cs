using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AspNetMicroservices.Extensions.Mvc
{
    /// <summary>
    /// Provides an extension methods for <see cref="MvcOptions"/> class.
    /// </summary>
    public static class MvcOptionsExtensions
    {
        /// <summary>
        /// Extension method adds route prefix for every controller.
        /// </summary>
        /// <param name="options">Instance of <see cref="MvcOptions"/></param>
        /// <param name="routeAttribute">Instance of object, that implements <see cref="IRouteTemplateProvider"/></param>
        public static void UseGlobalRoutePrefix(this MvcOptions options, IRouteTemplateProvider routeAttribute)
            => options.Conventions.Add(new RoutePrefixConvention(routeAttribute));

        /// <summary>
        /// Extension method adds route prefix for every controller.
        /// </summary>
        /// <param name="options">Instance of <see cref="MvcOptions"/></param>
        /// <param name="prefix">String route prefix</param>
        public static void UseGlobalRoutePrefix(this MvcOptions options, string prefix)
            => options.UseGlobalRoutePrefix(new RouteAttribute(prefix));
    }

    /// <summary>
    /// Represents an instance of <see cref="IApplicationModelConvention"/>
    /// </summary>
    public class RoutePrefixConvention : IApplicationModelConvention
    {
        /// <summary>
        /// Initialize new instance of <see cref="RoutePrefixConvention"/>
        /// </summary>
        /// <param name="route">Instance of object, that implements <see cref="IRouteTemplateProvider"/></param>
        public RoutePrefixConvention(IRouteTemplateProvider route)
        {
            _routePrefix = new AttributeRouteModel(route);
        }

        /// <summary>
        /// Instance of <see cref="AttributeRouteModel"/>
        /// </summary>
        private readonly AttributeRouteModel _routePrefix;

        /// <inheritdoc cref="IApplicationModelConvention"/>
        public void Apply(ApplicationModel application)
        {
            foreach (var selector in application.Controllers.SelectMany(c => c.Selectors))
            {
                if (selector.AttributeRouteModel != null)
                {
                    selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(
                        _routePrefix, selector.AttributeRouteModel);
                }
                else
                {
                    selector.AttributeRouteModel = _routePrefix;
                }
            }
        }
    }
}