using System;
using Microsoft.EntityFrameworkCore;

namespace VendorApp.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}