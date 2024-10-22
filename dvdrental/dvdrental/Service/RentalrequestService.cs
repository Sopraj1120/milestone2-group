using dvdrental.DTOs;
using dvdrental.DTOs.ResponceDtos;
using dvdrental.Entity;
using dvdrental.IRepository;
using dvdrental.IService;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace dvdrental.Service
{
    public class RentalRequestService : IRentalRequestService
    {
        private readonly IRentalRequestRepository _rentalRequestRepository;

        public RentalRequestService(IRentalRequestRepository rentalRequestRepository)
        {
            _rentalRequestRepository = rentalRequestRepository;
        }

        public async Task<IEnumerable<RentalRequest>> GetAllRental()
        {
            return await _rentalRequestRepository.GetAllRental();
        }

        public async Task<RentalRequest> GetByIdRental(int id)
        {
            return await _rentalRequestRepository.GetByIdRentalRequest(id);
        }

        public async Task<int> AddRentalRequest(RentalRequestDto rentalRequestDto)
        {
            var rentalRequest = new RentalRequest
            {
                MovieId = rentalRequestDto.MovieId,
                CustomerId = rentalRequestDto.CustomerId,
                RentDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(7),
                Status = RentalRequest.RentalStatus.Pending
            };

            return await _rentalRequestRepository.AddRentalRequest(rentalRequest);
        }

        //public async Task<RentalResponceDto> CreateRentalRequest(RentalRequestDto rentalRequestDto)
        //{
        //    // Create the RentalRequest entity from the DTO
        //    var rentalRequest = new RentalRequest
        //    {
        //        MovieId = rentalRequestDto.MovieId,
        //        CustomerId = rentalRequestDto.CustomerId,
        //        // Assign return date to 7 days later
        //        Status = RentalRequest.RentalStatus.Pending, // Default status
        //        MovieAvailableCopies = 1 // Set this based on your business logic
        //    };

        //    // Call the repository to add the rental request
        //    var createdRentalRequest = await _rentalRequestRepository.AddRentalRequest(rentalRequest);

        //    // Create the response DTO
        //    var responseDto = new RentalResponceDto
        //    {
        //        Id = createdRentalRequest.Id,
        //        MovieId = createdRentalRequest.MovieId,
        //        CustomerId = createdRentalRequest.CustomerId,

        //        Status = createdRentalRequest.Status.ToString(),
        //        MovieTitle = "Sample Movie Title", // Replace with actual movie title fetching logic
        //        MovieAvailableCopies = createdRentalRequest.MovieAvailableCopies,
        //        CustomerName = "Sample Customer Name" // Replace with actual customer name fetching logic
        //    };

        //    return responseDto;
        //}

        //public async Task<IEnumerable<RentalResponceDto>> GetAllRentalRequests()
        //{
        //    var rentalRequests = await _rentalRequestRepository.GetAllRentalRequests();
        //    var responseDtos = new List<RentalResponceDto>();

        //    foreach (var rentalRequest in rentalRequests)
        //    {
        //        responseDtos.Add(new RentalResponceDto
        //        {
        //            Id = rentalRequest.Id,
        //            MovieId = rentalRequest.MovieId,
        //            CustomerId = rentalRequest.CustomerId,

        //            Status = rentalRequest.Status.ToString(),
        //            MovieTitle = "Sample Movie Title", // Replace with actual movie title fetching logic
        //            MovieAvailableCopies = rentalRequest.MovieAvailableCopies,
        //            CustomerName = "Sample Customer Name" // Replace with actual customer name fetching logic
        //        });
        //    }
        //    return responseDtos;
        //}
    }
}
