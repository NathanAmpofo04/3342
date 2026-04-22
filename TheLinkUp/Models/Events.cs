using System.Collections.Generic;

namespace TheLinkUp.Models
{
    public class Event
    {
        private string eventID;
        private string eventName;
        private string eventDate;
        private string venueName;
        private string city;
        private string state;
        private string imageURL;
        private string eventURL;
        private List<YelpPlace> nearbyPlaces;
        private List<Attendee> attendees;

        public string EventID
        {
            get { return eventID; }
            set { eventID = value; }
        }

        public string EventName
        {
            get { return eventName; }
            set { eventName = value; }
        }

        public string EventDate
        {
            get { return eventDate; }
            set { eventDate = value; }
        }

        public string VenueName
        {
            get { return venueName; }
            set { venueName = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public string ImageURL
        {
            get { return imageURL; }
            set { imageURL = value; }
        }

        public string EventURL
        {
            get { return eventURL; }
            set { eventURL = value; }
        }

        public List<YelpPlace> NearbyPlaces
        {
            get { return nearbyPlaces; }
            set { nearbyPlaces = value; }
        }

        public List<Attendee> Attendees
        {
            get { return attendees; }
            set { attendees = value; }
        }
        public Event()
        {
            nearbyPlaces = new List<YelpPlace>();
            attendees = new List<Attendee>();
        }

        public Event(string eventID, string eventName, string eventDate, string venueName,
                     string city, string state, string imageURL, string eventURL)
        {
            this.eventID = eventID;
            this.eventName = eventName;
            this.eventDate = eventDate;
            this.venueName = venueName;
            this.city = city;
            this.state = state;
            this.imageURL = imageURL;
            this.eventURL = eventURL;
            this.nearbyPlaces = new List<YelpPlace>();
            this.attendees = new List<Attendee>();

        }
    }
}