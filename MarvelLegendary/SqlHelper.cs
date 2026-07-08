using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MarvelLegendary.Enums;
using MarvelLegendary.Tools;
using Microsoft.Data.Sqlite;

namespace MarvelLegendary
{
    public static class SqlHelper
    {
        private static readonly string ConnectionString = $"Data Source={DatabasePaths.DatabasePath}";

        static SqlHelper()
        {
            SQLitePCL.Batteries_V2.Init();
        }

        public static SqliteConnection GetConnection()
        {
            return new SqliteConnection(ConnectionString);
        }

        public static void SetupDatabase()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var pragma = connection.CreateCommand())
                {
                    pragma.CommandText = "PRAGMA foreign_keys = ON;";
                    pragma.ExecuteNonQuery();
                }

                CreateTables(connection);
            }
        }

        private static void CreateTables(SqliteConnection connection)
        {
            var sql = @"
                CREATE TABLE IF NOT EXISTS Card
                (
                    CardId      INTEGER PRIMARY KEY,
                    CardName    TEXT NOT NULL,
                    CardType    INTEGER NOT NULL,
                    SetId       INTEGER NOT NULL,
                    Enabled     INTEGER NOT NULL DEFAULT 1,

                    CHECK (CardType IN (1,2,3,4,5))
                );

                CREATE INDEX IF NOT EXISTS IX_Card_CardType
                    ON Card(CardType);

                CREATE TABLE IF NOT EXISTS Game
                (
                    
                    GameId          INTEGER PRIMARY KEY AUTOINCREMENT,
                    GamePlayDate    TEXT NOT NULL,
                    PlayerCount     INTEGER,
                    GameSuccess     INTEGER NOT NULL CHECK (GameSuccess IN (0,1)),
                    Notes           TEXT
                );

                CREATE TABLE IF NOT EXISTS GameCard
                (
                    GameId      INTEGER NOT NULL,
                    CardId      INTEGER NOT NULL,

                    PRIMARY KEY (GameId, CardId),

                    FOREIGN KEY (GameId) REFERENCES Game(GameId),
                    FOREIGN KEY (CardId) REFERENCES Card(CardId)
                );
                
                CREATE INDEX IF NOT EXISTS IX_GameCard_CardId
                    ON GameCard(CardId);

                CREATE TABLE IF NOT EXISTS CardRelationship
                (
                    Card1Id        INTEGER NOT NULL,
                    Card2Id        INTEGER NOT NULL,
                    TimesPlayed    INTEGER NOT NULL DEFAULT 1,

                    PRIMARY KEY (Card1Id, Card2Id),

                    FOREIGN KEY (Card1Id) REFERENCES Card(CardId),
                    FOREIGN KEY (Card2Id) REFERENCES Card(CardId),

                    CHECK (Card1Id < Card2Id)
                );

                CREATE INDEX IF NOT EXISTS IX_CardRelationship_Card1
                    ON CardRelationship(Card1Id);

                CREATE INDEX IF NOT EXISTS IX_CardRelationship_Card2
                    ON CardRelationship(Card2Id);";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }
        
        public static List<string> GetList(string sqlString)
        {
            var returnList = new List<string>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sqlString;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var name = reader.GetString(0);
                            returnList.Add(name);
                        }
                    }
                }
            }

            return returnList;
        }

        public static T RunCommandScalar<T>(SqliteCommand command)
        {
            command.Connection.Open();

            try
            {
                var result = command.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    return default(T);

                return (T)Convert.ChangeType(result, typeof(T));
            }
            finally
            {
                command.Connection.Close();
            }
        }

        public static List<SchemeInfo> RunCommand(SqliteCommand command)
        {
            var cards = new List<SchemeInfo>();
            command.Connection.Open();

            try
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var schemeName = reader.GetString(reader.GetOrdinal("CardName"));
                        var setName = (Set)reader.GetInt32(reader.GetOrdinal("SetId"));

                        var card = SchemeRepository.All.Where(s => s.SchemeName == schemeName && s.SetName == setName).FirstOrDefault();

                        cards.Add(card);
                    }
                }
            }
            finally
            {
                command.Connection.Close();
            }

            return cards;
        }
    }
}
