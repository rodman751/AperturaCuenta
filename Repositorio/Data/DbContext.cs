﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entidades;
using Microsoft.Data.SqlClient;


public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbContext (DbContextOptions<DbContext> options)
        : base(options)
    {
    }

  
    public DbSet<Entidades.Usuario> Usuario { get; set; } = default!;

    public async Task<List<Usuario>> EjecutarProcedimientoAlmacenado()
    {
        return await Usuario.FromSqlRaw("EXEC CrearTablaTemporalUsuarios").ToListAsync();
    }
 



    public DbSet<Entidades.CuentaApertura.CuentaUsuario> CuentaUsuario { get; set; } = default!;
    public DbSet<Entidades.CuentaApertura.RegistrosAuditoria> RegistrosAuditoria { get; set; } = default!;
    public DbSet<Entidades.CuentaApertura.Imagenes> Imagenes { get; set; } = default!;

}
