namespace CodingTracker.Model
{
    public class CodingSession
    {
        public int Id { get; private set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration
        {
            get
            {
                return EndTime - StartTime;
            }
        }

        public CodingSession(DateTime startTime, DateTime endTime)
        {

            StartTime = startTime;
            EndTime = endTime;

        }

        // Constructor that includes Id (useful when retrieving data from the database)
        public CodingSession(int id, DateTime startTime, DateTime endTime)
        {
            Id = id;  // Will be populated by the database when retrieved
            StartTime = startTime;
            EndTime = endTime;
        }

        // Empty constructor for use with Dapper (it requires parameterless constructor)
        public CodingSession()
        {
        }

    }
}
