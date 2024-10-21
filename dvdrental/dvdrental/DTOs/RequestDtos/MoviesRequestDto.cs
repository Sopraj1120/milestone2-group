namespace dvdrental.DTOs.RequestDtos
{
    public class MoviesRequestDto : MovieDto
    {
        public IFormFile File { get; set; }
    }
}
