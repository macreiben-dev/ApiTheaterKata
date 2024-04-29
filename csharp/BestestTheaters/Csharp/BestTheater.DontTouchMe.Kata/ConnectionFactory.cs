using System.Data.SQLite;

namespace BestTheater.DontTouchMe.Kata;

public static class ConnectionFactory
{
    internal static SQLiteConnection CreateConnection()
    {
        return new SQLiteConnection("Data Source=./database/BestestTheater.db");
    }
}