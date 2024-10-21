using dvdrental.DTOs;
using dvdrental.DTOs.RequestDtos;
using dvdrental.DTOs.ResponceDtos;

namespace dvdrental.IService
{
    public interface IRentalRequestService
    {
        Task<RentalResponceDto> CreateRentalRequest(RentalRequestDto rentalRequestDto);
        Task<IEnumerable<RentalResponceDto>> GetAllRentalRequests();

    }
}
