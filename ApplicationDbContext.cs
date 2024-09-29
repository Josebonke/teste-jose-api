﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using teste_jose_api.identity;


namespace teste_jose_api
{
    public class ApplicationDbContext: IdentityDbContext<AppUsuario>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}
         

        public DbSet<Model.LoginModel.UsuarioGrid> Usuarios { get; set; }

    }
}
