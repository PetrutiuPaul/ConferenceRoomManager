using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConferenceRoomManager.Models
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string NoPerson { get; set; }
        public string Temperature { get; set; }
        public string Humidity { get; set; }
    }
}