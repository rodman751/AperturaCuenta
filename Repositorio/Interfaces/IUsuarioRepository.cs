﻿using Entidades;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Interfaces
{
    public interface IUsuarioRepository
    {

        Task<List<Usuario>> EjecutarProcedimientoAlmacenado();
        Task GuardarUsuario(CuentaUsuario cuenta);
    }
}
