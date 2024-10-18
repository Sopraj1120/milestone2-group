using dvdrental.DTOs.RequestDtos;
using dvdrental.DTOs.ResponceDtos;

namespace dvdrental.IService
{
    public interface ICustomerService
    {
        Task<CustomerResponseDto> AddCustomer(CustomerRequestDto customer);
        Task<IEnumerable<CustomerResponseDto>> GetAllCustomers();
        Task<CustomerResponseDto> GetCustomerById(int id);
        Task<CustomerResponseDto> UpdateCustomer(int id, CustomerRequestDto customer);
        Task<bool> DeleteCustomer(int id);
    }

}
