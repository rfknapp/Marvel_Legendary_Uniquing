using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MarvelLegendary.Enums;
using MarvelLegendary.Helpers;
using MarvelLegendary.Tools;
using Microsoft.Data.Sqlite;

namespace MarvelLegendary
{
    public class Card
    {
        public int CardId { get; set; }
        public string CardName { get; set; }
        public int CardType { get; set; }
        public int SetId { get; set; }
    }

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

        public static List<Card> GetCardRelationships(CardType typeEnum, Card card)
        {
            //This will return the CardId for the card that combinations are queried for
            var command = GetConnection().CreateCommand();
            command.CommandText =
                @"SELECT CardId
                  FROM Card
                  WHERE CardName = $name
                    AND SetId = $setId";

            command.Parameters.AddWithValue("$name", card.CardName);
            command.Parameters.AddWithValue("$setId", card.SetId);

            var cardId = RunCommandScalar<int>(command);

            //This will return all cards, that match typeEnum, that were played with the card that was queried for
            command.Parameters.Clear();
            command = GetConnection().CreateCommand();
            command.CommandText =
                @"SELECT c.CardId,
                         c.CardName,
                         c.SetId,
                         c.CardType
                  FROM CardRelationship cr
                  INNER JOIN Card c
                      ON c.CardId = CASE
                                      WHEN cr.Card1Id = $cardId THEN cr.Card2Id
                                      ELSE cr.Card1Id
                                    END
                  WHERE (cr.Card1Id = $cardId OR cr.Card2Id = $cardId)
                    AND c.CardType = $cardTypeId;";

            command.Parameters.AddWithValue("$cardId", cardId);
            command.Parameters.AddWithValue("$cardTypeId", (int)typeEnum);
            
            var cardsPlayedWithQuery = RunCommand(command);
            var cardNames = cardsPlayedWithQuery.Select(c => c.CardName).ToList();

            //This returns List<string> but needs to return List<Card>

            return cardsPlayedWithQuery;
            //return cardNames;
        }

        public static List<Card> RunCommand(SqliteCommand command)
        {
            var cards = new List<Card>();
            command.Connection.Open();

            try
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cards.Add(new Card
                        {
                            CardId = reader.GetInt32(reader.GetOrdinal("CardId")),
                            CardName = reader.GetString(reader.GetOrdinal("CardName")),
                            SetId = reader.GetInt32(reader.GetOrdinal("SetId")),
                            CardType = reader.GetInt32(reader.GetOrdinal("CardType"))
                        });
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
