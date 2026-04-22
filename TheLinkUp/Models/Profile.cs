namespace TheLinkUp.Models
{
    public class Profile
    {
        private int memberID;
        private string fullName;
        private int age;
        private string height;
        private int weight;
        private string occupation;
        private string phone;
        private string address;
        private string city;
        private string state;
        private string photoURL;
        private string description;
        private string goals;
        private string commitmentType;
        private string favoriteMovie;
        private string favoriteRestaurant;
        private string favoriteBook;
        private string favoritePoem;
        private string favoriteQuote;
        private bool isVisible;

        public int MemberID
        {
            get { return memberID; }
            set { memberID = value; }
        }

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public string Height
        {
            get { return height; }
            set { height = value; }
        }

        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public string Occupation
        {
            get { return occupation; }
            set { occupation = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
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

        public string PhotoURL
        {
            get { return photoURL; }
            set { photoURL = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Goals
        {
            get { return goals; }
            set { goals = value; }
        }

        public string CommitmentType
        {
            get { return commitmentType; }
            set { commitmentType = value; }
        }

        public string FavoriteMovie
        {
            get { return favoriteMovie; }
            set { favoriteMovie = value; }
        }

        public string FavoriteRestaurant
        {
            get { return favoriteRestaurant; }
            set { favoriteRestaurant = value; }
        }

        public string FavoriteBook
        {
            get { return favoriteBook; }
            set { favoriteBook = value; }
        }

        public string FavoritePoem
        {
            get { return favoritePoem; }
            set { favoritePoem = value; }
        }

        public string FavoriteQuote
        {
            get { return favoriteQuote; }
            set { favoriteQuote = value; }
        }

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        public Profile()
        {
        }

        public Profile(int memberID, string fullName, int age, string height, int weight,
                       string occupation, string phone, string address, string city,
                       string state, string photoURL, string description, string goals,
                       string commitmentType, string favoriteMovie, string favoriteRestaurant,
                       string favoriteBook, string favoritePoem, string favoriteQuote, bool isVisible)
        {
            this.memberID = memberID;
            this.fullName = fullName;
            this.age = age;
            this.height = height;
            this.weight = weight;
            this.occupation = occupation;
            this.phone = phone;
            this.address = address;
            this.city = city;
            this.state = state;
            this.photoURL = photoURL;
            this.description = description;
            this.goals = goals;
            this.commitmentType = commitmentType;
            this.favoriteMovie = favoriteMovie;
            this.favoriteRestaurant = favoriteRestaurant;
            this.favoriteBook = favoriteBook;
            this.favoritePoem = favoritePoem;
            this.favoriteQuote = favoriteQuote;
            this.isVisible = isVisible;
        }
    }
}