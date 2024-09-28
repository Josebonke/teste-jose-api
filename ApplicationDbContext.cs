using Microsoft.EntityFrameworkCore;
using teste_jose_api.identity;


namespace teste_jose_api
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}
         DbSet<Model.LoginModel.UsuarioGrid> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Personalização de tabelas e propriedades, se necessário
            builder.Entity<AppUsuario>(entity => {
                entity.ToTable("Usuario");
            });

            builder.Entity<ApplicationRole>(entity => {
                entity.ToTable("Roles");
            });

            // Defina outras personalizações, como tabelas de claims
        }
    }
}
