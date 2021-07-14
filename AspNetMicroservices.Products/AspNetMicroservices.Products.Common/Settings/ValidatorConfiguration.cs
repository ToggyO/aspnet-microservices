using System.Linq;
using FluentValidation;

namespace NetREST.DTO
{
    public static class ValidatorConfigurationOverload
    {
        public static void Override()
        {
            ValidatorOptions.Global.PropertyNameResolver = (_, member, _) =>
                member.Name.First().ToString().ToLower() + member.Name.Substring(1);
        }
    }
}