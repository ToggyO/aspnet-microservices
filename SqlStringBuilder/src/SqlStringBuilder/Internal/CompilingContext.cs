using SqlStringBuilder.Interfaces.Common;

namespace SqlStringBuilder.Internal
{
	/// <summary>
	/// SQL compilation context.
	/// Uses for sharing builder instance between compilation methods.
	/// </summary>
	/// <typeparam name="TQuery">Type of SQL builder.</typeparam>
	internal class CompilationContext<TQuery> where TQuery : IBaseQueryStatementBuilder
	{
		/// <summary>
		/// Gets an instance of <see cref="IInternalBaseQueryStatementBuilder{TQuery}"/>.
		/// </summary>
		public IInternalBaseQueryStatementBuilder<TQuery> Builder { get; }

		/// <summary>
		/// Initialize new instance of <see cref="CompilationContext{TQuery}"/>.
		/// </summary>
		/// <param name="builder">instance of SQL builder.</param>
		public CompilationContext(IInternalBaseQueryStatementBuilder<TQuery> builder) => Builder = builder;
	}
}