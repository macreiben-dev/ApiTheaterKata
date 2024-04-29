using System.Data.SQLite;

namespace BestTheater.DontTouchMe.Kata;

public static class DatabaseBootstrapper
{
    /***
     * This code is here to store data in the application.
     *
     * It is not to be modified
     */
    public static void OnStart()
    {
        if (!Directory.Exists("./database"))
        {
            Directory.CreateDirectory("./database");
        }

        if (File.Exists("./database/BestestTheater.db"))
        {
            return;
        }

        BootApplicationUser.CreateUsersAndFeed();

        BootShows.CreateShowAndFeed();
        
        BootBookings.CreateBookingsAndFeed();
    }
}