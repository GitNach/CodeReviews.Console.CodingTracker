﻿using CodingTracker.Model;
using Dapper;
using System.Configuration;
using Microsoft.Data.Sqlite;

namespace CodingTracker.Controller
{
    public static class DatabaseController
    {
        private static readonly string _connectionString = ConfigurationManager.AppSettings["ConnectionString"]
            ?? throw new InvalidOperationException("ConnectionString not found in app settings.");
        private static readonly string _databaseFile = ConfigurationManager.AppSettings["DatabaseFile"]
            ?? throw new InvalidOperationException("Database file not found in app settings.");

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

        public static void DeleteSession(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var deleteCmd = "DELETE FROM CodingSession WHERE Id = @Id;";
                connection.Execute(deleteCmd, new { Id = id });
            }

        }

        public static bool DoesSessionExist(int sessionId)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                string query = "SELECT COUNT(1) FROM CodingSession WHERE Id = @Id";
                int count = connection.ExecuteScalar<int>(query, new { Id = sessionId });
                return count > 0;
            }

        }
    }
}
