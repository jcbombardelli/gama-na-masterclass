using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

namespace MasterclassDapper.Db
{
    public static class Seed
    {

        public static bool isSeeded = false;
        public static void CreateDb(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
            var dbFilePath = configuration.GetValue<string>("DBInfo:DbFilePath");
            if (!File.Exists(dbFilePath))
            {
                IDbConnection _dbConnection = new SqliteConnection(connectionString);
                _dbConnection.Open();

                var result = _dbConnection.Execute(@"
                    CREATE TABLE IF NOT EXISTS cities (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        name NVARCHAR(255) NOT NULL,
                        uf CHAR(2) NOT NULL
                    )");

                _dbConnection.Close();
            }
            else {
                isSeeded = true;
            }
        }

        public static void SeedDb(IConfiguration configuration)
        {
            if(!isSeeded){
                var connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
                var dbFilePath = configuration.GetValue<string>("DBInfo:DbFilePath");

                IDbConnection _dbConnection = new SqliteConnection(connectionString);
                _dbConnection.Open();
                _dbConnection.Execute("INSERT INTO cities (name, uf) VALUES('São Paulo', 'SP')");
                _dbConnection.Close();

                isSeeded = true;
            }

        }
    }
}