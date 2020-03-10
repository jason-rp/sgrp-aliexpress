using System;
using System.Linq;
using SGRP.Aliexpress.Data;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new DesignTimeDbContextFactory().CreateDbContext())
            {
                var releases = context.Users.Take(5);

            }

            Console.WriteLine("\r\nPress any key to continue ...");
            Console.Read();
        }
    }
}
