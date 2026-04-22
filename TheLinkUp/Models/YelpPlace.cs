namespace TheLinkUp.Models
{
    public class YelpPlace
    {
        private string name;
        private string rating;
        private string address;
        private string category;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Rating
        {
            get { return rating; }
            set { rating = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public YelpPlace()
        {
        }

        public YelpPlace(string name, string rating, string address, string category)
        {
            this.name = name;
            this.rating = rating;
            this.address = address;
            this.category = category;
        }
    }
}