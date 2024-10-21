namespace dvdrental.Entity
{
    public class Movies
    {
        public int Id { get; set; }


        public string Title { get; set; }

        public string Description { get; set; }


        public int Copies { get; set; }


        public int CategoryId { get; set; }


        public string CategoryName { get; set; }

        public string Image { get; set; }

        public bool IsDeleted { get; set; }

        public IFormFile File { get; set; }
    }
}
