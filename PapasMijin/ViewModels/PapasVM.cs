using PapasMijin.Datos;
using PapasMijin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace PapasMijin.ViewModels
{
    public class PapasVM : BaseVM
    {
        public ICommand SumarSalchipapa { get; }
        public ICommand SumarPapa { get; }
        public ICommand SumarHorchata { get; }
        public ICommand SumarPapiPollo { get; }
        public ICommand Total { get; }
        public ICommand Cancelar { get; }
        public ObservableCollection<Ventas> ListaVentas { get; set; }
        public ObservableCollection<Ventas2> Ventas { get; set; }

        public ObservableCollection<ListaPapas> IngresoComida { get; set; }

        public ObservableCollection<ListaPrecios> btnPri { get; set; }

        private ListaPrecios _lista { get; set; }
        public ListaPrecios lista 
        { 
            get { return _lista; }
            
            set 
            { 
                if(_lista != value)
                {
                    _lista = value;
                    ItemSeleccionado();
                }
            }
        }

        public int Cantidad { get; set; }
        public PapasVM()
        {
            SumarSalchipapa = new Command(AddSalchipapa);
            SumarPapa = new Command(AddPapa);
            SumarHorchata = new Command(AddHorchata);
            SumarPapiPollo = new Command(AddPapiPollo); 
            Total = new Command(TotalVenta);
            Cancelar = new Command(CancelarVenta);
            ListaVentas = new ObservableCollection<Ventas>();
            Ventas = new ObservableCollection<Ventas2>();
            IngresoComida = new ObservableCollection<ListaPapas>();
            btnPri = new ObservableCollection<ListaPrecios>();
            Cantidad = 1;
            MostrarMenu();
        }

        private void ItemSeleccionado()
        {
            ListaPapas re = new ListaPapas();
            re.fecha = DateTime.Now;
            re.nombre = lista.nombre;
            re.precio = lista.precio;
            re.cantidad = Cantidad;

            count = count + (re.precio * re.cantidad);
            CountDisplay = count + "$";
            IngresoComida.Add(re);
        }

        private async void MostrarMenu()
        {
            var t = await App.Database.ListaPrecios();
            foreach(var g in t)
            {
                btnPri.Add(g);
            }
        }

        double count = 0;

        string countDisplay = "tu pide";
        public string CountDisplay
        {
            get => countDisplay;
            set
            {
                if (value == countDisplay)
                    return;
                countDisplay = value;
                OnPropertyChanged();
            }
        }

        void AddSalchipapa()
        {
            string Comida = "salchipapa";
            Precios salchi = new Precios();
            var preciossalchi = salchi.Precio;
            ListaPapas Nombre = new ListaPapas() { nombre = Comida, cantidad = Cantidad, precio = preciossalchi };
            //Ventas obj = new Ventas() { Fecha = DateTime.Today, Comida = "Salchipapa", Precio = Precios.Salchipapa };
            //Ventas2 obj2 = new Ventas2() { Fecha = DateTime.Today, ComidaId = "1", NVendido = "1" };
            //count = count + Precios.Salchipapa;
            CountDisplay = count + " $";
            IngresoComida.Add(Nombre);
            //ListaVentas.Add(Nombre);
        }

        void AddPapa()
        {
            //Ventas obj = new Ventas() { Fecha = DateTime.Today, Comida = "Papa", Precio = Precios.Papa };
            //count = count + Precios.Papa;
            CountDisplay = count + " $";
            //ListaVentas.Add(obj);
        }
        void AddHorchata()
        {
            /*Ventas obj = new Ventas() { Fecha = DateTime.Today, Comida = "Horchata", Precio = Precios.Horchata };
            count = count + Precios.Horchata;
            CountDisplay = count + " $";
            ListaVentas.Add(obj);*/
        }
        void AddPapiPollo()
        {
            /*Ventas obj = new Ventas() { Fecha = DateTime.Today, Comida = "Papipollo", Precio = Precios.PapiPollo };
            count = count + Precios.PapiPollo;
            CountDisplay = count + " $";
            ListaVentas.Add(obj);*/
        }

        async void TotalVenta()
        {
            //Ventas2 ventas2 = new Ventas2();
            //ListaPapas n = new ListaPapas();
            foreach (var f in IngresoComida)
            {
                await App.Database.AddVentasLista(f);
            }
            CountDisplay = "su Total es= " + count;
            /*foreach(var t in ListaVentas)
            {
                await App.Database.AddVentas(t);
                //await App.Database.AddVentas2(t);
            }*/
            await Task.Delay(2000);
            //ListaVentas.Clear();
            CancelarVenta();
        }

        void CancelarVenta()
        {
            count = 0;
            CountDisplay = "tu pide";
            //ListaVentas.Clear();
            IngresoComida.Clear();
            //Vt.Clear();
        }

    }
}
