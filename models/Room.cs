using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.models
{
    class Room
    {
        public int Id { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
