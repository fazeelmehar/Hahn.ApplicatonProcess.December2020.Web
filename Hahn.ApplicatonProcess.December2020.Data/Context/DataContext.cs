using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Hahn.ApplicatonProcess.December2020.Data.Context
{
    public class DataContext : DbContext
    {
        public static IConfiguration _Configuration;
        public DbSet<Applicant> Applicant { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applicant>().ToTable("Applicant");
            ModelConfiguration(modelBuilder);
        }

        private void ModelConfiguration(ModelBuilder _modelBuilder)
        {
            _modelBuilder.Entity<Applicant>().Property(ug => ug.ID).IsRequired(true);
            _modelBuilder.Entity<Applicant>().Property(ug => ug.Name).IsRequired(true).HasColumnType("nvarchar(50)");
            _modelBuilder.Entity<Applicant>().Property(ug => ug.FamilyName).IsRequired(true).HasColumnType("nvarchar(50)");
            _modelBuilder.Entity<Applicant>().Property(ug => ug.Address).IsRequired(true).HasColumnType("nvarchar(50)");
            _modelBuilder.Entity<Applicant>().Property(ug => ug.Age).IsRequired(true);
            _modelBuilder.Entity<Applicant>().Property(ug => ug.Hired).IsRequired(true);
            _modelBuilder.Entity<Applicant>().Property(ug => ug.EmailAdress).IsRequired(true).HasColumnType("nvarchar(75)");
            _modelBuilder.Entity<Applicant>().Property(ug => ug.CountryOfOrigin).IsRequired(true).HasColumnType("nvarchar(75)");
        }
    }
}
