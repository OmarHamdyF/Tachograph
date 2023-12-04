using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Contexts.seed;
public static class ModelBuilderExtensions
{
    public static void DriversSeed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Driver>().HasData(
              new
              {
                  Id = "Driver A",
                  FirstName = "Omar",
                  LastName = "Hamdy"
              },
              new
              {
                  Id = "Driver B",
                  FirstName = "Ahmed",
                  LastName = "Hamdy"
              }
          );

    }

}

