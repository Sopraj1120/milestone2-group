using dvdrental.DTOs.RequestDtos;
using dvdrental.DTOs.ResponceDtos;

namespace dvdrental.IService
{
    public interface IMoviesService
    {
        Task<MovieResponseDto> AddMovie(MoviesRequestDto movieRequest);
        Task<List<MovieResponseDto>> GetAllMovies();
        Task<MovieResponseDto> GetMovieById(int id);
        Task<bool> UpdateMovie(MoviesRequestDto movieRequest, int id);
        Task<bool> DeleteMovie(int id);
        Task<List<MovieResponseDto>> GetMoviesByCategory(int categoryId);


    }
}
