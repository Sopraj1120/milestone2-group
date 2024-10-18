﻿namespace dvdrental.Entity
{
    public class Customer
    {
        
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
            public string Password { get; set; } 
            public string MobileNo { get; set; }
            public string NicNo { get; set; }
            public string UserName { get; set; }
            public bool IsActive { get; set; } = false; 

        public ICollection<RentalRequest> RentalRequests { get; set; }
        public string Name { get; internal set; }
    }
}
