namespace dvdrental.DTOs
{
    public class RentalRequestDto
    {
        public int MovieId { get; set; }
        public int CustomerId { get; set; }
        public DateTime RentDate { get; set; } = DateTime.Now;
        public DateTime ReturnDate { get; set; }
        public IFormFile? imagefile { get; set; }

    }
}
