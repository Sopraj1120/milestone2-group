namespace dvdrental.DTOs.ResponceDtos
{
    public class CustomerResponseDto : CustomerDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }

}
