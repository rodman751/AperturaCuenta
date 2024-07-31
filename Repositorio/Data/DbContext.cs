using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entidades;
using Entidades.CuentaApertura;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbContext (DbContextOptions<DbContext> options)
        : base(options)
    {
    }

    public DbSet<Entidades.DatosDactilares> DatosDactilares { get; set; } = default!;
    //public DbSet<Entidades.Usuario> Usuario { get; set; } = default!;
    //public DbSet<Entidades.DireccionMapa> DireccionMapa { get; set; } = default!;
    //public DbSet<Entidades.InformacionPersonal> InformacionPersonal { get; set; } = default!;

    //public DbSet<Entidades.OTP> OTP { get; set; } = default!;
    public DbSet<Entidades.CuentaApertura.CombinedData> CombinedData { get; set; } = default!;

    

}
