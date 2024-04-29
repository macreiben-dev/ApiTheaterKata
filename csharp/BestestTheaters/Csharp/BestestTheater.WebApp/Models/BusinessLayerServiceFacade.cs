namespace BestestTheater.WebApp.Models
{
    public class BusinessLayerServiceFacade
    {
        private static List<BkngData_3> BookedShows { get; set; } = new();

        /// <summary>
        /// Fetch the shows
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Show> FetchShows()
        {
            var TOMORROW = DateTime.Now.AddDays(1);
            var shows = GetAllShows(TOMORROW);
            return shows;
        }

        // =====================================
        
        public static List<Show> GetAllShows(DateTime date)
        {
            var allShows = ShowRepository.AllShows();

            var output = new List<Show>();
            
            foreach (var show in allShows)
            {
                if (show.Date.Day == date.Day && show.Date.Month == date.Month && show.Date.Year == date.Year)
                    output.Add(show);
            }
            
            return output;
        }

        public static void DoBooking(BookingInfo bookingInfo)
        {
            ShowRepository.SaveBooking(bookingInfo);
        }
        
        // =====================================
        
        public List<BkngData_3> GetMyBookings()
        {
            return BookedShows;
        }

        public void Book(Show showBooked)
        {
            var seatsAsStrings = GetSeats();
            var details = seatsAsStrings.Aggregate((prev, next) => prev + ", " + next);
            BookedShows.Add(new BkngData_3 { Title = showBooked.Title, Date = showBooked.Date, Details = details });
        }

        private static List<string> GetSeats()
        {
            var randomSeatSeed = new Random().Next(0, 100);
            var randSeatCount = new Random().Next(1, 5);
            var seatsAsStrings = new List<string>();
            for (var j = 0; j < randSeatCount; j++)
            {
                int randomizedNumber = new Random().Next(randomSeatSeed, randomSeatSeed + 100);

                seatsAsStrings.Add($"Seats {randomizedNumber.ToString("D2")}");
            }

            return seatsAsStrings;
        }
    }
}
