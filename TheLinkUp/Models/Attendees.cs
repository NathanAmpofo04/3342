namespace TheLinkUp.Models
{
    public class Attendee
    {
        private int memberID;
        private string username;
        private string email;

        public int MemberID
        {
            get { return memberID; }
            set { memberID = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public Attendee()
        {
        }

        public Attendee(int memberID, string username, string email)
        {
            this.memberID = memberID;
            this.username = username;
            this.email = email;
        }
    }
}