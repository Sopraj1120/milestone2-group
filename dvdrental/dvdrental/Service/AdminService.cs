﻿using dvdrental.DTOs.RequestDtos;
using dvdrental.DTOs.ResponceDtos;
using dvdrental.Entity;
using dvdrental.IRepository;
using dvdrental.IService;
using System.Security.Cryptography;
using System.Text;

namespace dvdrental.Service
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<AdminResponceDto> CreateAdminAsync(AdminRequestDto requestDto)
        {
            try
            {
                
                if (requestDto.Password != requestDto.ConfirmPassword)
                {
                    throw new ArgumentException("Password and Confirm Password do not match.");
                }

                
               

                var admin = new Admin
                {
                    Email = requestDto.Email,
                    Password = requestDto.Password,
                };

               
                var createdAdmin = await _adminRepository.AddAdminAsync(admin);

               
                return new AdminResponceDto
                {
                    Id = createdAdmin.Id,
                    AdminId = createdAdmin.AdminId,
                    Email = createdAdmin.Email,
                    Password = createdAdmin.Password
                };
            }
            catch (ArgumentException ex)
            {
              
                Console.WriteLine($"Validation Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
              
                Console.WriteLine($"An error occurred while creating the admin: {ex.Message}");
                throw;
            }
        }

       
    }
}
