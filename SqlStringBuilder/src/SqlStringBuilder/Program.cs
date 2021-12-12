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
                .From("kek")
                .Select("df", "sdf", "df");

            var compiler = new Compiler();
            var result = compiler.Compile(builder);

            Console.WriteLine(result.Sql);
        }
    }
}
