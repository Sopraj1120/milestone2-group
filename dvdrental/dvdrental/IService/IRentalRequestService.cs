using dvdrental.DTOs;
using dvdrental.DTOs.RequestDtos;
using dvdrental.DTOs.ResponceDtos;
using dvdrental.Entity;

namespace dvdrental.IService
{
    public interface IRentalRequestService
    {
        //Task<RentalResponceDto> CreateRentalRequest(RentalRequestDto rentalRequestDto);
        Task<IEnumerable<RentalRequest>> GetAllRental();
        Task<RentalRequest> GetByIdRental(int id);
        Task<int> AddRentalRequest(RentalRequestDto rentalRequestDto);

    }
}
