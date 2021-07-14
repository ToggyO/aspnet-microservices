using System.Linq;

using FluentValidation;

namespace AspNetMicroservices.Products.Common.Settings
{
	/// <summary>
	/// Global validation configuration overload for <see cref="FluentValidation"/>.
	/// </summary>
    public static class ValidatorConfigurationOverload
    {
	    /// <summary>
	    /// Override validation configuration.
	    /// </summary>
        public static void Override()
        {
            ValidatorOptions.Global.PropertyNameResolver = (_, member, _) =>
                member.Name.First().ToString().ToLower() + member.Name.Substring(1);
        }
    }
}