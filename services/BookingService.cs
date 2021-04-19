using MeetingRoomBooking.models;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.services
{
    class BookingService
    {
        //Key  => RoomNo
        public static IDictionary<int, List<models.Booking>> RoomBookingList = new Dictionary<int, List<models.Booking>>();

        public bool IsAvailable(int roomNo, DateTime startDateTime, DateTime endDateTime)
        {
            bool isVal = true;
            List<models.Booking> Booking = new List<models.Booking>();

            if (!RoomBookingList.TryGetValue(roomNo, out Booking))
            {
                Booking = new List<models.Booking>();
               
            }
            foreach (models.Booking val in Booking)
            {
                if (startDateTime >= val.StartDateTime && startDateTime <= val.EndDateTime)
                {
                    isVal = false;
                }
                else if (startDateTime <= val.StartDateTime)
                {
                    if (endDateTime >= val.StartDateTime || endDateTime >= val.EndDateTime)

                    {
                        isVal = false;
                    }
                }
            }


            if (isVal)
            {
                //set booking
                Booking.Add(new models.Booking
                {
                    StartDateTime = startDateTime,
                    EndDateTime = endDateTime,
                    Id = Guid.NewGuid().ToString() //Booking.Count+1
                });
                
                RoomBookingList[roomNo] =  Booking;

            }
            
            return isVal;
        }

        /// <summary>  
        /// To Save Key Value Pair in Redis DB  
        /// </summary>  
        /// <param name="host">Redis Host Name</param>  
        /// <param name="key">Key as string</param>  
        /// <param name="value">Value as string</param>  
        /// <returns></returns>  
        public bool Save(string host, string key, string value)
        {
            using (var objRedisClient = new RedisClient(host))
            {
                if (objRedisClient.Get<string>(key) == null)
                {
                    return objRedisClient.Set<String>(key, value);
                }
                else
                {
                    return false;
                }
            }
        }

        public List<models.Booking> getBookingsByRoomNo(int roomNo)
        {
            List<models.Booking> Booking = new List<models.Booking>();

            if (!RoomBookingList.TryGetValue(roomNo, out Booking))
            {
                Booking = new List<models.Booking>();

            }

            return Booking;

        }

        public void DeleteBooking(string id, int roomId)
        {
            List<models.Booking> Booking = new List<models.Booking>();

            if (RoomBookingList.TryGetValue(roomId, out Booking))
            {
                int index = Booking.FindIndex(a => a.Id == id);
                Booking.RemoveAt(index);

                RoomBookingList[roomId] = Booking;
            }
        }
    }
}
