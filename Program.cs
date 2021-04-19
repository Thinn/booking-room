using MeetingRoomBooking.models;
using MeetingRoomBooking.services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeetingRoomBooking
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Booking());



            RoomService roomService = new RoomService();

            var temp = new List<models.Room>();
            temp.Add(new Room()
            {
                Id = 1,
                Bookings = new List<models.Booking>()
            });
            temp.Add(new Room()
            {
                Id = 2,
                Bookings = new List<models.Booking>()
            });

           // roomService.InitializeRooms("localhost", "rooms", temp);

           // var list = roomService.GetAll("localhost", "rooms");
           // Console.WriteLine(list);
        }
    }
}
