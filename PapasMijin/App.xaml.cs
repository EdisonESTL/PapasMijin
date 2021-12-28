using PapasMijin.Services;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PapasMijin
{
    public partial class App : Application
    {
        static PapasService database;

        public static PapasService Database
        {
            get
            {
                if(database == null)
                {
                    var pat = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    database = new PapasService(Path.Combine(pat, "PapasElMijin.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
