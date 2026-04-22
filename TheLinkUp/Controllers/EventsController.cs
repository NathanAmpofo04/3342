using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;
using TheLinkUp.Models;
using Utilities;
using Microsoft.AspNetCore.SignalR;
using TheLinkUp.Hubs;

namespace TheLinkUp.Controllers
{
    public class EventsController : Controller
    {
        public async Task<IActionResult> DisplayEvents()
        {
            if (HttpContext.Session.GetInt32("MemberID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (HttpContext.Session.GetInt32("MemberID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            List<Event> eventList = new List<Event>();

            string apiKey = "3A8x7o7xgcztbBAjD3uSNWzOblnAHgeV";
            string url = "https://app.ticketmaster.com/discovery/v2/events.json?apikey=" + apiKey + "&city=Philadelphia";

            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);

            JObject data = JObject.Parse(response);

            if (data["_embedded"] != null && data["_embedded"]["events"] != null)
            {
                foreach (var item in data["_embedded"]["events"])
                {
                    Event evt = new Event();

                    evt.EventID = item["id"].ToString();
                    evt.EventName = item["name"].ToString();
                    evt.EventDate = item["dates"]["start"]["localDate"].ToString();
                    evt.EventURL = item["url"].ToString();

                    if (item["images"] != null && item["images"].HasValues)
                    {
                        evt.ImageURL = item["images"][0]["url"].ToString();
                    }
                    else
                    {
                        evt.ImageURL = "https://via.placeholder.com/150";
                    }

                    if (item["_embedded"] != null && item["_embedded"]["venues"] != null)
                    {
                        evt.VenueName = item["_embedded"]["venues"][0]["name"].ToString();
                        evt.City = item["_embedded"]["venues"][0]["city"]["name"].ToString();

                        if (item["_embedded"]["venues"][0]["state"] != null)
                        {
                            evt.State = item["_embedded"]["venues"][0]["state"]["stateCode"].ToString();
                        }
                        else
                        {
                            evt.State = "";
                        }
                    }

                    eventList.Add(evt);
                }
            }

            return View("DisplayEvents", eventList);
        }

        public async Task<IActionResult> ViewEventDetails(string id)
        {
            if (HttpContext.Session.GetInt32("MemberID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (HttpContext.Session.GetInt32("MemberID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            Event evt = new Event();

            string ticketmasterKey = "3A8x7o7xgcztbBAjD3uSNWzOblnAHgeV";
            string ticketmasterUrl = "https://app.ticketmaster.com/discovery/v2/events/" + id + ".json?apikey=" + ticketmasterKey;

            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(ticketmasterUrl);

            JObject item = JObject.Parse(response);

            evt.EventID = item["id"].ToString();
            evt.EventName = item["name"].ToString();
            evt.EventDate = item["dates"]["start"]["localDate"].ToString();
            evt.EventURL = item["url"].ToString();

            if (item["images"] != null && item["images"].HasValues)
            {
                evt.ImageURL = item["images"][0]["url"].ToString();
            }
            else
            {
                evt.ImageURL = "https://via.placeholder.com/300";
            }

            if (item["_embedded"] != null && item["_embedded"]["venues"] != null)
            {
                evt.VenueName = item["_embedded"]["venues"][0]["name"].ToString();
                evt.City = item["_embedded"]["venues"][0]["city"]["name"].ToString();

                if (item["_embedded"]["venues"][0]["state"] != null)
                {
                    evt.State = item["_embedded"]["venues"][0]["state"]["stateCode"].ToString();
                }
                else
                {
                    evt.State = "";
                }
            }

            string yelpKey = "Kq_cZ5HD-VlK5LLar4uOe7rlbPwCJRaArE9v_K5NiJ8UHL-QGw2dK99QwWX8ff1N8xx7JNePr1xPZ896tB3vOPFwCq13D-jA-o8_WKCquWZKqU4BH9e4Xz2cZAXoaXYx";
            string yelpUrl = "https://api.yelp.com/v3/businesses/search?location=" + Uri.EscapeDataString(evt.City) + "&term=restaurants&limit=3";

            HttpClient yelpClient = new HttpClient();
            yelpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + yelpKey);

            string yelpResponse = await yelpClient.GetStringAsync(yelpUrl);
            JObject yelpData = JObject.Parse(yelpResponse);

            if (yelpData["businesses"] != null)
            {
                foreach (var business in yelpData["businesses"])
                {
                    YelpPlace place = new YelpPlace();

                    place.Name = business["name"].ToString();
                    place.Rating = business["rating"].ToString();
                    place.Address = business["location"]["address1"].ToString();

                    if (business["categories"] != null && business["categories"].HasValues)
                    {
                        place.Category = business["categories"][0]["title"].ToString();
                    }
                    else
                    {
                        place.Category = "";
                    }

                    evt.NearbyPlaces.Add(place);
                }
            }

            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandType = CommandType.Text;
            objCommand.CommandText = @"SELECT m.MemberID, m.Username, m.Email
                           FROM EventRSVPs r
                           INNER JOIN Members m ON r.MemberID = m.MemberID
                           WHERE r.EventID = @EventID";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@EventID", evt.EventID);

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);

            foreach (DataRow record in ds.Tables[0].Rows)
            {
                Attendee attendee = new Attendee();

                attendee.MemberID = int.Parse(record["MemberID"].ToString());
                attendee.Username = record["Username"].ToString();
                attendee.Email = record["Email"].ToString();

                evt.Attendees.Add(attendee);
            }

            return View("ViewEventDetails", evt);
        }

        [HttpPost]
        public async Task<IActionResult> RSVPToEvent(string eventID, string eventName, string eventDate,
                                  string venueName, string city, string state)
        {
            if (HttpContext.Session.GetInt32("MemberID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int memberID = HttpContext.Session.GetInt32("MemberID").Value;

            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandType = CommandType.Text;
            objCommand.CommandText = "SELECT COUNT(*) AS RSVPCount FROM EventRSVPs WHERE MemberID = @MemberID AND EventID = @EventID";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@MemberID", memberID);
            objCommand.Parameters.AddWithValue("@EventID", eventID);

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);

            int count = int.Parse(ds.Tables[0].Rows[0]["RSVPCount"].ToString());

            if (count > 0)
            {
                TempData["RSVPMessage"] = "You already RSVP'd to this event.";
                return RedirectToAction("ViewEventDetails", new { id = eventID });
            }

            objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "AddEventRSVP";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@MemberID", memberID);
            objCommand.Parameters.AddWithValue("@EventID", eventID);
            objCommand.Parameters.AddWithValue("@EventName", eventName);
            objCommand.Parameters.AddWithValue("@EventDate", eventDate);
            objCommand.Parameters.AddWithValue("@VenueName", venueName);
            objCommand.Parameters.AddWithValue("@City", city);
            objCommand.Parameters.AddWithValue("@State", state);

            objDB.DoUpdateUsingCmdObj(objCommand);

            TempData["RSVPMessage"] = "RSVP saved successfully.";
            await hubContext.Clients.All.SendAsync("ReceiveRSVPUpdate", "A new attendee has joined this event.");
            return RedirectToAction("ViewEventDetails", new { id = eventID });
        }

        public IActionResult MyRSVPs()
        {

            if (HttpContext.Session.GetInt32("MemberID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int memberID = HttpContext.Session.GetInt32("MemberID").Value;

            List<RSVP> rsvpList = new List<RSVP>();

            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandType = CommandType.Text;
            objCommand.CommandText = @"SELECT RSVPId, MemberID, EventID, EventName, EventDate, VenueName, City, State
                               FROM EventRSVPs
                               WHERE MemberID = @MemberID";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@MemberID", memberID);

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);

            foreach (DataRow record in ds.Tables[0].Rows)
            {
                RSVP rsvp = new RSVP();

                rsvp.RSVPId = int.Parse(record["RSVPId"].ToString());
                rsvp.MemberID = int.Parse(record["MemberID"].ToString());
                rsvp.EventID = record["EventID"].ToString();
                rsvp.EventName = record["EventName"].ToString();
                rsvp.EventDate = record["EventDate"].ToString();
                rsvp.VenueName = record["VenueName"].ToString();
                rsvp.City = record["City"].ToString();
                rsvp.State = record["State"].ToString();

                rsvpList.Add(rsvp);
            }

            return View("MyRSVPs", rsvpList);
        }

        private readonly IHubContext<EventHub> hubContext;

        public EventsController(IHubContext<EventHub> hubContext)
        {
            this.hubContext = hubContext;
        }
    }
}