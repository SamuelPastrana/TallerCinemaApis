using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TallerCinemaApi.Models;
using TallerCinemaApi.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TallerCinemaApi.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

        private async void Ingresar_Click(object sender, EventArgs e)
        {
            var usuario = Usuario.Text;
            var password = Password.Text;

            if (string.IsNullOrEmpty(usuario))
            {
               await DisplayAlert("Validacion", AppResources.ValidacionUsuario, "Aceptar");
                Usuario.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Validacion", AppResources.ValidacionContraseña, "Aceptar");
                Usuario.Focus();
                return;
            }

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://misapis.azurewebsites.net");

            var autenticacion = new Autenticacion
            {
                Usuario = usuario,
                Password = password
            };

            String json = JsonConvert.SerializeObject(autenticacion);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = await client.PostAsync("/api/Seguridad", content);

            if (request.IsSuccessStatusCode)
            {
                var responseJson = await request.Content.ReadAsStringAsync();
                var respuesta = JsonConvert.DeserializeObject<Respuesta>(responseJson);

                if (respuesta.EsPermitido)
                {
                   await Navigation.PushAsync(new CarteleraPage());
                }
                else
                {
                    await DisplayAlert("¡Lo sentimos!", respuesta.Mensaje, "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Error!", "Ha Sucedido Un Problema De Comunicacion", "OK");
            }
        }

    }
}