namespace TheLinkUp.Models
{
    public class RSVP
    {
        private int rsvpId;
        private int memberID;
        private string eventID;
        private string eventName;
        private string eventDate;
        private string venueName;
        private string city;
        private string state;

        public int RSVPId
        {
            get { return rsvpId; }
            set { rsvpId = value; }
        }

        public int MemberID
        {
            get { return memberID; }
            set { memberID = value; }
        }

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

        public RSVP()
        {
        }

        public RSVP(int rsvpId, int memberID, string eventID, string eventName,
                    string eventDate, string venueName, string city, string state)
        {
            this.rsvpId = rsvpId;
            this.memberID = memberID;
            this.eventID = eventID;
            this.eventName = eventName;
            this.eventDate = eventDate;
            this.venueName = venueName;
            this.city = city;
            this.state = state;
        }
    }
}