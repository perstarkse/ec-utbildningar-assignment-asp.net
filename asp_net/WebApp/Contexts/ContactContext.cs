﻿using Microsoft.EntityFrameworkCore;
using WebApp.Models.Entities;

namespace WebApp.Contexts
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ContactMessageEntity> RecievedMessages { get; set; }
    }
}
