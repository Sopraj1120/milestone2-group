using dvdrental.DTOs.ResponceDtos;
using dvdrental.Entity;

namespace dvdrental.IRepository
{

    public interface IRentalRequestRepository
    {
        Task<IEnumerable<RentalRequest>> GetAllRental();
        Task<RentalRequest> GetByIdRentalRequest(int id);
        Task<int> AddRentalRequest(RentalRequest rentalRequest);


    }

}
