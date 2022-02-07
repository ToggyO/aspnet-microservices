using System;
using System.Collections.Generic;
using System.Reflection;

using Dapper;

namespace AspNetMicroservices.Auth.DataAccess.Mapping
{
	/// <summary>
	/// Implements custom property mapping by user provided criteria (usually presence of some custom attribute with column to member mapping)
	/// and cache list of properties info.
	/// </summary>
	public class CustomPropertyTypeMapWithCachedProperties : SqlMapper.ITypeMap
	{
		private readonly Type _type;

		private readonly List<PropertyInfo> _cachedProperties = new ();

        private readonly Func<Type, IEnumerable<PropertyInfo>, string, PropertyInfo> _propertySelector;

        /// <summary>
        /// Creates custom property mapping.
        /// </summary>
        /// <param name="type">Target entity type.</param>
        /// <param name="selector">
        /// Property selector based on target type, set of property info of type
        /// and DataReader column name.
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
		public CustomPropertyTypeMapWithCachedProperties(Type type,
            Func<Type, IEnumerable<PropertyInfo>, string, PropertyInfo> selector)
		{
			_type = type ?? throw new ArgumentNullException(nameof(type));
            _propertySelector = selector ?? throw new ArgumentNullException(nameof(selector));
            _cachedProperties.AddRange(type.GetProperties(BindingFlags.Instance | BindingFlags.Public));
        }

        /// <summary>
        /// Always returns default constructor.
        /// </summary>
        /// <param name="names">DataReader column names.</param>
        /// <param name="types">DataReader column types.</param>
        /// <returns>Default constructor</returns>
		public ConstructorInfo FindConstructor(string[] names, Type[] types)
            => _type.GetConstructor(Array.Empty<Type>());

        /// <summary>
        /// Always returns null.
        /// </summary>
        /// <returns></returns>
        public ConstructorInfo FindExplicitConstructor() => null;

        /// <summary>
        /// Not implemented as far as default constructor used for all cases.
        /// </summary>
        /// <param name="constructor"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
		public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
		{
			throw new NotImplementedException();
		}

        /// <summary>
        /// Returns property based on selector strategy.
        /// </summary>
        /// <param name="columnName">DataReader column name</param>
        /// <returns>Property member map</returns>
		public SqlMapper.IMemberMap GetMember(string columnName)
		{
            var prop = _propertySelector(_type, _cachedProperties, columnName);
            return prop is not null ? new CustomMemberMap(columnName, prop) : null ;
		}
	}
}