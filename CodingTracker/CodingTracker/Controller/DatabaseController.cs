using CodingTracker.Model;
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
        private static string _connectionString = $"Data Source={DATABASEFILE};";
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

        public static void InsertSession(CodingSession session)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var insertCmd = @"INSERT INTO CodingSession (StartTime, EndTime, Duration)
                                 VALUES (@StartTime, @EndTime, @Duration);";
                connection.Execute(insertCmd, session);
            }
        }

        public static List<CodingSession> GetSessions() 
        { 
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<CodingSession>("SELECT * FROM CodingSession;").ToList();
            }
        }

        public static void UpdateSession(CodingSession session)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var updateCmd = @"UPDATE CodingSession 
                                  SET StartTime = @StartTime, 
                                      EndTime = @EndTime, 
                                      Duration = @Duration 
                                  WHERE Id = @Id;";
                connection.Execute(updateCmd, session);
            }
        }

    }
}
