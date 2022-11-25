using API_PROJECT_FP_DAM.Models;
using Microsoft.EntityFrameworkCore;

namespace API_PROJECT_FP_DAM.DataContext
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

    }

}
