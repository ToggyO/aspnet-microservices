using SqlStringBuilder.Interfaces.Common;

namespace SqlStringBuilder.Internal
{
	// TODO: Add description.
	internal class CompilationContext<TQuery> where TQuery : IBaseQueryStatementBuilder
	{
		public IInternalBaseQueryStatementBuilder<TQuery> Builder { get; init; }

		public CompilationContext(IInternalBaseQueryStatementBuilder<TQuery> builder)
		{
			Builder = builder;
		}
	}
}