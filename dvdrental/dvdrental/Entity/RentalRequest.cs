namespace dvdrental.Entity
{
    public class RentalRequest
    {
        public int Id { get; set; }

        public int MovieId { get; set; }


        public Movies Movie { get; set; }


        public int MoviesAvailableCopies { get; set; }

     
        public int CustomerId { get; set; }

      
        public Customer Customer { get; set; }

       
        public RentalStatus Status { get; set; } = RentalStatus.Pending;

        public DateTime RentDate { get; set; } = DateTime.Now;
        public DateTime ReturnDate { get; set; }

        public IFormFile? imagefile { get; set; }

    
    //public object MovieImageType { get;  set; }
    //public object MovieImage { get;  set; }


    public enum RentalStatus
        {
            Pending,
            Approved,
            Rejected,
            Returned
        }
    }
}
