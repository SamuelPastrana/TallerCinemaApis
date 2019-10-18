using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TallerCinemaApi.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TallerCinemaApi.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CarteleraPage : ContentPage
	{
		public CarteleraPage ()
		{
			InitializeComponent ();
            CargarPeliculas();
		}

        private async void CargarPeliculas()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://misapis.azurewebsites.net");

            var request = await client.GetAsync("/api/Cartelera");

            if (request.IsSuccessStatusCode)
            {
                var responseJson = await request.Content.ReadAsStringAsync();
                var listado = JsonConvert.DeserializeObject<List<Pelicula>>(responseJson);
                listPeliculas.ItemsSource = listado;
            }
            else
            {
                await DisplayAlert("Error!", "Ha Sucedido Un Problema De Comunicacion", "OK");
            }
        }

        private async void Pelicula_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            var pelicula = (Pelicula)e.SelectedItem;
            await Navigation.PushAsync(new FuncionesPage(pelicula));
        }
    }
}