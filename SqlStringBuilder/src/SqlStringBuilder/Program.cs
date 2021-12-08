using System;

namespace SqlStringBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlStringBuilder
                .UseSelect()
                .From("kek")
                .Select("df", "sdf", "df");
        }
    }
}
