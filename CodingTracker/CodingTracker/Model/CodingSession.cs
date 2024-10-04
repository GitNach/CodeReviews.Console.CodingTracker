using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Model
{
    public class CodingSession
    {
        private int Id;
        private DateTime StartTime;
        private DateTime EndTime;
        private TimeSpan Duration { 
            get 
            { 
                return EndTime - StartTime; 
            } 
        }

        public CodingSession(int id, DateTime startTime, DateTime endTime)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            
        }

    }
}
