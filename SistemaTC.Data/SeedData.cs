using Microsoft.EntityFrameworkCore;
using static SistemaTC.Core.General;

namespace SistemaTC.Data;
public static class SeedData
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entities.Role>(entity =>
        {
            entity.HasData(
                    new Entities.Role
                    {
                        RoleId = RolesList[Roles.Administrator],
                        Code = Roles.Administrator,
                        Name = "Administrador",
                        CreatedBy = SystemUser,
                        Created = new DateTime(2024, 5, 11, 0, 36, 0)
                    }
                );
        });
    }
}
