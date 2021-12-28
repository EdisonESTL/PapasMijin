using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using PapasMijin.Datos;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace PapasMijin.ViewModels
{
    public class PreciosVM : BaseVM
    {
        
        string _comida ;
        public string comida
        {
            get => _comida;
            set
            {
                if (value == _comida)
                    return;
                _comida = value;
                OnPropertyChanged();
            }
        }

        double _precio;
        public double Precio
        {
            get => _precio;
            set {
                if (value == _precio)
                    return;
                _precio = value;
                OnPropertyChanged();
            }
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        public ICommand Guardar { get; }
        public ICommand Cancelar { get; }

        public ObservableCollection<ListaPrecios> ListaVer { get; set; }
        

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true;

                    Show();

                    IsRefreshing = false;
                });
            }
        }
        public PreciosVM()
        {
            Guardar = new Command(Save); 
            Cancelar = new Command(Cancel);
            ListaVer = new ObservableCollection<ListaPrecios>();
        }

        private void Cancel()
        {
            comida = "";
            Precio = 0;
        }

        private async void Save()
        {
            Comida come = new Comida();
            come.Nombre = comida;
            await App.Database.AddComida(come);

            List<Comida> cc = await App.Database.GetComida();
            Precios pri = new Precios();
            pri.IdComida = come.Id;
            pri.Precio = Precio;
            await App.Database.AddPrecios(pri);
            Cancel();
            Show();
        }

        private async void Show()
        {
            List<ListaPrecios> op = await App.Database.ListaPrecios();

            foreach(var t in op)
            {
                ListaVer.Add(t);
            }
        }
    }
}
