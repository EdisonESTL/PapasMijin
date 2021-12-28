using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQLite;

namespace PapasMijin.Datos
{
    public class Ventas 
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Comida { get; set; }
        public double Precio { get; set; }
    }

    //La clase precio => Modelo Precio(Price) se crea en caso de:
    //varios precios un mismo producto.
    public class Precios
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int IdComida { get; set; }
        //Campo Fecha registro con el objetivo de saber desde cuando entro en vigencia el precio
        //public double FechaRegistro { get; set; }
        public double Precio { get; set; }
        //Campo Ingreso creado con el objetivo de almacenar el valor total de ventas a ese precio
        //public double Ingreso { get; set; }
    }
    public class Comida
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        //public double TotalComidaVendida { get; set; }
    }

    public class Ventas2
    {
        [PrimaryKey, AutoIncrement]
        public int IdVentas { get; set; }
        public DateTime Fecha { get; set; }

        [Indexed]
        public string ComidaId { get; set; }
        public string NVendido { get; set; }
    }

    public class ListaPapas
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime fecha { get; set; }
        public string nombre { get; set; }
        public int cantidad { get; set; }
        public double precio { get; set; }
    }

    public class ListaPrecios
    {
        public string nombre { get; set; }
        public double precio { get; set; }
    }
    /*public class GroupedVentas : ObservableCollection<Ventas>
    {
        public DateTime FechaGrupo { get; set; }
        public string nombre { get; set; }
    }*/
}
