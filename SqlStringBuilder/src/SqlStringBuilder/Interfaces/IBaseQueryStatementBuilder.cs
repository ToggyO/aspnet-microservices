namespace SqlStringBuilder.Interfaces
{
    /// <summary>
    /// Base SQL statement builder.
    /// </summary>
    public interface IBaseQueryStatementBuilder
    {
        string Build();
    }
}