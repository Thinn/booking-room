using MeetingRoomBooking.models;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.services
{
    class RoomService
    {
        public static List<models.Room> rooms = new List<models.Room>();

        public void saveRooms()
        {

            rooms.Add(new Room()
            {
                Id = 1,
                Bookings = new List<models.Booking>()
            });
            rooms.Add(new Room()
            {
                Id = 2,
                Bookings = new List<models.Booking>()
            });
        }

        public List<models.Room> getAllRooms()
        {
            return rooms;
        }


      
        public bool InitializeRooms(string host, string key, List<Room> rooms)
        {
            using (var objRedisClient = new RedisClient(host))
            {
                if (objRedisClient.Get<List<Room>>(key) == null)
                {
                    return objRedisClient.Set<List<Room>>(key, rooms);
                }
                else
                {
                    return false;
                }
            }
        }

        public List<Room> GetAll(string host, string key)
        {
            using (var objRedisClient = new RedisClient(host))
            {
                return objRedisClient.Get<List<Room>>(key);
            }
        }
    }
}
