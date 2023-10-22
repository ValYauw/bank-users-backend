using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BNI_Users_backend.Data;
using BNI_Users_backend.Models;
using System;
using System.Linq;

namespace BNI_Users_backend.Seed
{
    public static class SeedUsers
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new UserContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<UserContext>>()))
            {
                // Look for any user.
                if (context.User.Any())
                {
                    return;   // DB has been seeded
                }

                context.User.AddRange(
                    new User
                    {
                        fName = "John",
                        lName = "Doe",
                        telephone = "0811111111",
                        email = "john.doe@mail.com",
                        dateOfBirth = DateTime.Parse("1990-01-01"),
                        description = "A businessman",
                        questionSecondAuthentication = "Apakah nama sekolah pertama anda?"
                    },
                    new User
                    {
                        fName = "Jane",
                        lName = "Doe",
                        telephone = "0811111112",
                        email = "jane.doe@mail.com",
                        dateOfBirth = DateTime.Parse("1985-12-26"),
                        description = "A CEO",
                        questionSecondAuthentication = "Nama binatang peliharaan pertama anda adalah?"
                    },
                    new User
                    {
                        fName = "Jack",
                        lName = "Bowers",
                        telephone = "0811111113",
                        email = "jack.bowers@mail.com",
                        dateOfBirth = DateTime.Parse("2000-11-05"),
                        description = "Fresh graduate.",
                        questionSecondAuthentication = "Apakah warna kesukaan anda?"
                    },
                    new User
                    {
                        fName = "Susilo",
                        lName = "Bayu",
                        telephone = "0811111114",
                        email = "susilo@mail.com",
                        dateOfBirth = DateTime.Parse("2001-04-21"),
                        description = "Entrepreneur.",
                        questionSecondAuthentication = "Apakah nama sekolah pertama anda?"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}