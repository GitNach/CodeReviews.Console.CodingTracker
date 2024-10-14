using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Controller
{
    public static class DatabaseController
    {
        private static string _connectionString = $"Data Source={DATABASEFILE};Version=3;";
        private const string DATABASEFILE = "CodingTrackerDatabase.sqlite";

        public static void CreateDatabase()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var tableCmd = @"CREATE TABLE IF NOT EXISTS CodingSession (
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    StartTime TEXT,
                                    EndTime TEXT,
                                    Duration TEXT);";
                connection.Execute(tableCmd);
            }

        }
    }
}
