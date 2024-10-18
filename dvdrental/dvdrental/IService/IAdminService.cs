using dvdrental.DTOs.RequestDtos;
using dvdrental.DTOs.ResponceDtos;

namespace dvdrental.IService
{
    public interface IAdminService
    {
        Task<AdminResponceDto> CreateAdminAsync(AdminRequestDto requestDto);
    }
}
