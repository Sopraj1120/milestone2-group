using dvdrental.DTOs;
using dvdrental.DTOs.ResponceDtos;
using dvdrental.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class RentalRequestController : ControllerBase
{
    private readonly IRentalRequestService _rentalRequestService;

    public RentalRequestController(IRentalRequestService rentalRequestService)
    {
        _rentalRequestService = rentalRequestService;
    }

    // POST api/rentalrequest
    [HttpPost]
    public async Task<ActionResult<RentalResponceDto>> CreateRentalRequest([FromBody] RentalRequestDto rentalRequestDto)
    {
        try
        {
            if (rentalRequestDto == null)
            {
                return BadRequest("Rental request cannot be null.");
            }

            // Creating the rental request
            var rentalRequest = await _rentalRequestService.CreateRentalRequest(rentalRequestDto);

            // Return the response
            return Ok(rentalRequest);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET api/rentalrequest
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RentalResponceDto>>> GetAllRentalRequests()
    {
        try
        {
            var rentalRequests = await _rentalRequestService.GetAllRentalRequests();
            return Ok(rentalRequests);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    
}
