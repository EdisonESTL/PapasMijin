using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using PapasMijin.Datos;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace PapasMijin.Services
{
    public class PapasService
    {
        private SQLiteAsyncConnection db;
        public PapasService(string databasePath)
        {
            db = new SQLiteAsyncConnection(databasePath);
            db.CreateTableAsync<Ventas>().Wait();
            db.CreateTableAsync<Ventas2>().Wait();
            db.CreateTableAsync<Comida>().Wait();
            db.CreateTableAsync<Precios>().Wait();
            db.CreateTableAsync<ListaPapas>().Wait();
        }
        public async Task<int> AddVentasLista(ListaPapas ventas)
        {
            if (ventas.Id != 0)
            {
                return await db.UpdateAsync(ventas);
            }
            else
            {
                return await db.InsertAsync(ventas);

            }
        }
        public async Task<int> AddVentas(Ventas ventas)
        {
            if(ventas.Id != 0)
            {
                return await db.UpdateAsync(ventas);
            }
            else
            {
                return await db.InsertAsync(ventas);

            }
        }

        public async Task<int> AddVentas2(Ventas2 ventas)
        {
            if (ventas.IdVentas != 0)
            {
                return await db.UpdateAsync(ventas);
            }
            else
            {
                return await db.InsertAsync(ventas);
            }
        }

        public async Task<int> AddComida(Comida comida)
        {
            if(comida.Id != 0)
            {
                return await db.UpdateAsync(comida);
            }
            else
            {
                return await db.InsertAsync(comida);
            }
        }

        public async Task<List<Comida>> GetComida()
        {
            return await db.Table<Comida>().ToListAsync();
        }
        public async Task<List<Ventas>> GetVentas()
        {
            return await db.Table<Ventas>().ToListAsync();
            
        }

        public async Task<List<ListaPapas>> GetVentasLista()
        {
            return await db.Table<ListaPapas>().ToListAsync();

        }

        public async Task<int> DeleteVentasLista()
        {
            return await db.Table<ListaPapas>().DeleteAsync();
        }
        public async Task<List<Precios>> GetPrecios()
        {
            return await db.Table<Precios>().ToListAsync();
        }

        public async Task<int> AddPrecios(Precios precios)
        {
            if (precios.Id != 0)
            {
                return await db.UpdateAsync(precios);
            }
            else
            {
                return await db.InsertAsync(precios);
            }
        }

        public async Task<List<ListaPrecios>> ListaPrecios()
        {
            string query = "SELECT Comida.Id, Comida.Nombre, Precios.Precio FROM Comida INNER JOIN Precios ON Comida.Id = Precios.IdComida";
            var lst = await db.QueryAsync<ListaPrecios>(query);
            return lst.ToList();
        }
    }
}
