using MeetingRoomBooking.models;
using MeetingRoomBooking.services;
using System;
using System.Collections;
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
    public partial class ManageBooking : Form
    {
        BindingList<models.Booking> bookingBindList;
        public ManageBooking()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
             ComboBox comboBox = (ComboBox)sender;
            var selectedRoom =((models.Room)room_list.SelectedValue).Id;
               
            BookingService bookingService = new BookingService();
            List<models.Booking> bookingList = bookingService.getBookingsByRoomNo(selectedRoom);

            bookingBindList = new BindingList<models.Booking>();

            foreach (models.Booking var in bookingList)
            {
                bookingBindList.Add(var);
            }
            dataGridView1.DataSource = bookingBindList;

        }

        //private static string DisplayFactory(Person p)
        //{
        //    return "First Name" + p.FirstName;
        //}

        private void ManageBooking_Load(object sender, EventArgs e)
        {
            RoomService roomService = new RoomService();
            var list = roomService.getAllRooms();
            room_list.DataSource = list;

            //Bind the Display member and Value member to the data source
            room_list.DisplayMember = "Id";
           // room_list.ValueMember = "Id";

          //  room_list.SelectedItem = list[0].Id;
        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {            
            var bookingId = ((models.Booking)dataGridView1.SelectedRows[0].DataBoundItem).Id;
            var roomId = ((models.Room)room_list.SelectedValue).Id;

            BookingService bookingService = new BookingService();
            bookingService.DeleteBooking(bookingId, roomId);
            bookingBindList.RemoveAt(dataGridView1.SelectedRows[0].Index);

        }
    }
}
