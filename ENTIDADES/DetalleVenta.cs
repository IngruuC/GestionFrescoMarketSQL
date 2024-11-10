﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{

    public class DetalleVenta
    {

            public int Id { get; set; }
            public int VentaId { get; set; }
            public int ProductoId { get; set; }
            public int Cantidad { get; set; }
            public string ProductoNombre { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal Subtotal { get; set; }
            public Venta Venta { get; set; }
            public Producto Producto { get; set; }
        
    }
}

