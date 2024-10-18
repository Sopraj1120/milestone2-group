using dvdrental.DTOs;
using dvdrental.DTOs.RequestDtos;
using dvdrental.DTOs.ResponceDtos;
using dvdrental.Entity;
using dvdrental.IRepository;
using dvdrental.IService;
using Microsoft.AspNetCore.Http.HttpResults;

namespace dvdrental.Service
{
    public class RentalrequestService : IRentalRequestService
    {
        private readonly IRentalRequestRepository _rentalRequestsRepository;

     

        public RentalrequestService(IRentalRequestRepository rentalRequestsRepository)
        {
            _rentalRequestsRepository = rentalRequestsRepository;
        }
                                                     
        public async Task<bool> AcceptRentalRequest(int id, bool isAccepted)
        {
            var data = await _rentalRequestsRepository.AcceptRentalRequest(id, isAccepted);
                return data;
        }

        public async Task<RentalResponceDto> AddRentalRequest(RentalRequestDto rentalRequestDto)
        {
            var rentalRequest = new RentalRequest
            {
                MovieId = rentalRequestDto.MovieId,
                CustomerId = rentalRequestDto.CustomerId,
                RentDate = rentalRequestDto.RentDate,
                ReturnDate = rentalRequestDto.ReturnDate,
                imagefile = rentalRequestDto.imagefile
                //MovieImageType = rentalRequestDto.MovieImageType,
            };

            var addedRentalRequest = await _rentalRequestsRepository.AddRentalRequest(rentalRequest);

            // Map the RentalRequest to RentalResponseDto
            var rentalResponseDto = new RentalResponceDto
            {
                Id = addedRentalRequest.Id, // Ensure RentalRequest has an Id property
                MovieId = addedRentalRequest.MovieId,
                CustomerId = addedRentalRequest.CustomerId,
                RentDate = addedRentalRequest.RentDate,
                ReturnDate = addedRentalRequest.ReturnDate,
                imagefile=addedRentalRequest.imagefile
                //MovieImage = (byte[])addedRentalRequest.MovieImage,
                //MovieImageType = (string)addedRentalRequest.MovieImageType
            };

            return rentalResponseDto;

        }

        public async Task<List<RentalResponceDto>> GetAllRentalRequests()
        {
            var data=await _rentalRequestsRepository.GetAllRentalRequests();
            return data;
        }



        public async Task<List<RentalResponceDto>> GetRentalRequestById(int id)
        {
            var data = await _rentalRequestsRepository.GetRentalRequestById(id);
            return data;

        }


        public async Task<List<RentalResponceDto>> GetRentalsByCustomerId(int customerId)
        {
            var data=await _rentalRequestsRepository.GetRentalsByCustomerId(customerId);
            return data;
        }

       

        public Task<bool> ReturnRentalRequest(int id)
        {
            throw new NotImplementedException();
        }

    }

}
