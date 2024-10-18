using dvdrental.DTOs.ResponceDtos;
using dvdrental.Entity;

namespace dvdrental.IRepository
{
    public interface IRentalRequestRepository
    {
        Task<RentalRequest> AddRentalRequest(RentalRequest rentalRequest);
        //Task NotifyAdminAboutRentalRequest(RentalRequest rentalRequest);
        Task<bool> AcceptRentalRequest(int id, bool isAccepted);
        //Task NotifyCustomerAboutRentalStatus(int rentalRequestId, string status);
        //Task<bool> ReturnRentalRequest(int id);
        //Task NotifyCustomerAboutReturn(int rentalRequestId);
        Task<List<RentalResponceDto>> GetAllRentalRequests();
        Task<List<RentalResponceDto>> GetRentalRequestById(int id);

     
        Task<List<RentalResponceDto>> GetRentalsByCustomerId(int customerId);
       
      



    }
}
