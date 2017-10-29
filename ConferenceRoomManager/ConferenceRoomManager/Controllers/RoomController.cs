using ConferenceRoomManager.DAL;
using ConferenceRoomManager.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ConferenceRoomManager.Controllers
{
    [Authorize]
    public class RoomController : Controller
    {
        // GET: Room
        public ActionResult Index()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            List<RoomViewModel> allRooms = new List<RoomViewModel>();
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://10.5.5.25:5000/get_movement");
                var rooms = json.Split(';');
                foreach (var room in rooms)
                {
                    if (room.Length > 0)
                    {
                        var param = room.Split(',');
                        string filter = param[0];
                        var r = unitOfWork.RoomRepository.Get(x => x.Name == filter).FirstOrDefault();
                        string h;
                        if (param[1] == "1")
                        {
                            if (Int32.Parse(param[2]) > 1)
                            {
                                h = param[2];
                            }
                            else
                            {
                                h = "1";
                            }
                        }
                        else
                        {
                            h = "0";
                          
                        }
                        allRooms.Add(new RoomViewModel
                        {
                            Name = param[0],
                            Status = param[1] == "1" ? true : false,
                            Id = r.Id,
                            NoPerson = h,
                            Humidity = String.Format("{0:0.00}", float.Parse(param[3], CultureInfo.InvariantCulture.NumberFormat)) ,
                            Temperature = String.Format("{0:0.00}", float.Parse(param[4], CultureInfo.InvariantCulture.NumberFormat))
                        });
                    }
                }
            }
            allRooms.Sort((x, y) => x.Name.CompareTo(y.Name));
            return View(allRooms);
        }

        public ActionResult AllRooms(string search,bool available)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            List<RoomViewModel> allRooms = new List<RoomViewModel>();
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://10.5.5.25:5000/get_movement");
                var rooms = json.Split(';');
                foreach (var room in rooms)
                {
                    if (room.Length > 0)
                    {
                        var param = room.Split(',');
                        string filter = param[0];
                        var r = unitOfWork.RoomRepository.Get(x => x.Name == filter).FirstOrDefault();
                        if (search != null)
                        {
                            r = unitOfWork.RoomRepository.Get(x => x.Name == filter && x.Name.Contains(search)).FirstOrDefault();
                        }
                        string h;
                        if (param[1] == "1")
                        {
                            if (Int32.Parse(param[2]) > 1)
                            {
                                h = param[2];
                            }
                            else
                            {
                                h = "1";
                            }
                        }
                        else
                        {
                            h = "0";
                        }
                        if (r != null)
                        {
                            if(available == true && param[1] == "0" || available == false)
                            allRooms.Add(new RoomViewModel
                            {
                                Name = param[0],
                                Status = param[1] == "1" ? true : false,
                                Id = r.Id,
                                NoPerson = h,
                                Humidity = String.Format("{0:0.00}", float.Parse(param[3], CultureInfo.InvariantCulture.NumberFormat)),
                                Temperature = String.Format("{0:0.00}", float.Parse(param[4], CultureInfo.InvariantCulture.NumberFormat))
                            });
                        }
                    }
                }
            }
            allRooms.Sort((x, y) => x.Name.CompareTo(y.Name));
            return View(allRooms);
        }
    }
}