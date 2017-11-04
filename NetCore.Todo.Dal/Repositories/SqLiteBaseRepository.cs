using System;
using Microsoft.Data.Sqlite;

namespace Todoer.Dal.Repositories
{
    public class SqLiteBaseRepository
    {
        public static string DbFile = @"C:\temp\TodoDB.sqlite";

        public static SqliteConnection SimpleDbConnection()
        {
            return new SqliteConnection("Data Source=" + DbFile);
        }
    }
}