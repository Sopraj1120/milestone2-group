namespace dvdrental.Entity
{
    public class RentalRequest
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int CustomerId { get; set; }

        public DateTime RentDate { get; set; } 
        public DateTime ReturnDate { get; set; }

        public int MovieAvailableCopies { get; set; }

        public string ? ImagePath { get; set; }
        public string ? MovieImageType { get; set; }

        public RentalStatus Status { get; set; } = RentalStatus.Pending;
        public IFormFile? imagefile { get; set; }


        public enum RentalStatus
        {
            Pending,
            Approved,
            Rejected,
            Returned
        }
        public Movies ? Movie { get; set; }
        public Customer ? Customer { get; set; }

       
     
        

    }
}
