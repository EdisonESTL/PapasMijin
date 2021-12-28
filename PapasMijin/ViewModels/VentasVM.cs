using PapasMijin.Datos;
using System.Collections.ObjectModel;
using PapasMijin.Services;
using System.Text;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using System;

namespace PapasMijin.ViewModels
{
    public class VentasVM : BaseVM
    {
        //private ObservableCollection<Ventas> _venta;
        //public ObservableCollection<Ventas> Venta { get => _venta; set { _venta = value; OnPropertyChanged(); } }
        public ObservableCollection<ListaPapas> Venta { get; set; }

        private double _totalHoy { get; set; }
        public double totalHoy
        {
            get { return _totalHoy; }
            set 
            { 
                _totalHoy = value;
                OnPropertyChanged();
            }
        }
        //public ObservableCollection<GroupedVentas> GroupedVentas { get; set; }
        public ICommand Verv { get; }
        public ICommand BorrarVentas { get; }

        public VentasVM()
        {
            Verv = new Command(Ver);
            Venta = new ObservableCollection<ListaPapas>();
            totalHoy = 0;
            BorrarVentas = new Command(DeleteV);
            //GroupedVentas = new ObservableCollection<GroupedVentas>();
        }

        async private void DeleteV()
        {
            await App.Database.DeleteVentasLista();
        }

        async public void Ver()
        {
            //var ie = await App.Database.GetVentas();

            //var pri = ie[0];

            //var gvt = new GroupedVentas();
            IEnumerable<ListaPapas> ie = await App.Database.GetVentasLista();

            foreach (var f in ie)
            {
                Venta.Add(f);
                totalHoy = totalHoy + f.precio;
                /*if(f.Fecha == pri.Fecha)
                {
                    gvt.Add();
                }*/
            }
        }
    }
}
