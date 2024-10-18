namespace dvdrental.DTOs.ResponceDtos
{
    public class MovieResponseDto : MovieDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
