namespace SqlStringBuilder.Common
{
    /// <summary>
    /// Table name parameter.
    /// </summary>
    public class FromTableParameter
    {
        /// <summary>
        /// Table name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Table sql alias.
        /// </summary>
        public string Alias { get; set; }
    }
}