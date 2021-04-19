using MeetingRoomBooking.models;
using MeetingRoomBooking.services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeetingRoomBooking
{
    public partial class Booking : Form
    {
        public Booking()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RoomService roomService = new RoomService();
            roomService.saveRooms();

            var rooms = roomService.getAllRooms();
            comboBox1.DataSource = rooms;

            //Bind the Display member and Value member to the data source
            comboBox1.DisplayMember = "Id";
            comboBox1.ValueMember = "Id";

            dateTimePicker1.MinDate = DateTime.Today;

            dateTimePicker1.CustomFormat = "yyyy/mm/dd HH:mm:ss";
            dateTimePicker2.CustomFormat = "yyyy/mm/dd HH:mm:ss";

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            //var temp = new List<models.Room>();
            //temp.Add(new Room()
            //{
            //    Id = 1,
            //    Bookings = new List<models.Booking>()
            //});
            //temp.Add(new Room()
            //{
            //    Id = 2,
            //    Bookings = new List<models.Booking>()
            //});


            // roomService.InitializeRooms("localhost", "rooms", temp);
        }

        private void start_date_time_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //call logic 
            BookingService bookingService = new BookingService();

            var startDateTime = dateTimePicker1.Value;
            var endDateTime = dateTimePicker2.Value;

            int roomNo;
            Int32.TryParse(comboBox1.SelectedValue.ToString(), out roomNo);
            
            bool isVal = bookingService.IsAvailable(roomNo, startDateTime, endDateTime);
            if (!isVal)
            {
                string message = "Meeting Room is unavailable! Please choose another date or time.";
                string title = "Error";
                MessageBox.Show(message, title);                
            }
            else
            {
                string message = "Meeting Room Booking is successful.";
                string title = "Success";
                MessageBox.Show(message, title);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ManageBooking f = new ManageBooking();
            f.ShowDialog();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            var startDate = dateTimePicker1.Value;
            var endDate = dateTimePicker2.Value;

            if(endDate < startDate)
            {
                string message = "End Date must be greater than Start Date";
                string title = "Error";
                MessageBox.Show(message, title);
                dateTimePicker2.Value = dateTimePicker1.Value;
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
