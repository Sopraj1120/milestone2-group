namespace dvdrental.DTOs.ResponceDtos
{
    public class RentalResponceDto : RentalRequestDto
    {
        public int Id { get; set; }

        public string MovieTitle { get; set; }
        public int MovieAvailableCopies { get; set; }
        public string CustomerName { get; set; }

        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Image { get; set; }
         
        public string Status { get; set; }
    }
}
