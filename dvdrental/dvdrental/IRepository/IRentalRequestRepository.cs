using dvdrental.DTOs.ResponceDtos;
using dvdrental.Entity;

namespace dvdrental.IRepository
{

    public interface IRentalRequestRepository
    {
        Task<RentalRequest> AddRentalRequest(RentalRequest rentalRequest);
        Task<IEnumerable<RentalRequest>> GetAllRentalRequests();


    }

}
