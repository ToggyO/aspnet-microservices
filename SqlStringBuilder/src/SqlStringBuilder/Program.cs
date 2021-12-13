using System;

using SqlStringBuilder.Compilers;

namespace SqlStringBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
	        var builder = SqlStringBuilder
		        .CreateSelectStatement()
		        .From("users")
		        .Select("users.id", "users.name as username", "users.age")
		        .Where("users.id", ">", 1);

            var compiler = new Compiler();
            var result = compiler.Compile(builder);

            Console.WriteLine(result.Sql);
        }
    }
}
