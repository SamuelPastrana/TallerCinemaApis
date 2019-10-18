﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerCinemaApi.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TallerCinemaApi.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResumenPage : ContentPage
	{
		public ResumenPage (Pelicula pelicula, Funcion funcion, int cantidad)
		{
			InitializeComponent ();
            gridPelicula.BindingContext = pelicula;
            gridFuncion.BindingContext = funcion;
            Cantidad.Text = cantidad.ToString();

            TotalPagar.Text = (cantidad * funcion.Precio).ToString();
        }

        private async void Finalizar_Click(object sender, EventArgs e)
        {
            await DisplayAlert("¡Felicidades!", "La Boleta Se Ha Comprado Exitosamente", "OK");
        }
    }
}