namespace BestTheater.DontTouchMe.Kata;

public static class BootApplicationUser
{
    internal   static void CreateUsersAndFeed()
    {
        using var connection = ConnectionFactory.CreateConnection();
        connection.Open();

        {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "CREATE TABLE AppUsers (Id INTEGER PRIMARY KEY AUTOINCREMENT, LoginName TEXT, Email TEXT, Password TEXT) ";

            cmd.ExecuteNonQuery();
        }
        
        {
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
            INSERT INTO AppUsers (LoginName, Email, Password) VALUES 
            ('User1', 'user1@example.com', 'password'), 
            ('User2', 'user2@example.com', 'password'), 
            ('User3', 'user3@example.com', 'password')";

            cmd.ExecuteNonQuery();

            connection.Close();
        }
    }
}