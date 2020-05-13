using dhamo.aleksander.Elenco.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using static dhamo.aleksander.Elenco.Models.Film;

namespace dhamo.aleksander.Elenco
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        int index;
        Films ArchivioDeiFilm { get; set; }

        public MainPage()
        {
            InitializeComponent();
            ArchivioDeiFilm = new Films();
            ArchivioDeiFilm.Load();
            listaFilm.ItemsSource = ArchivioDeiFilm;
        }

        //Riceve l'archivio aggiornato
        public MainPage(Films pass)
        {
            InitializeComponent();
            this.ArchivioDeiFilm = pass;
        }


        //apre il popup
        [Obsolete]
        private void NewFilm_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new AddFilm(ArchivioDeiFilm));
        }

        //aggiorna la ListView
        private void listaImpegni_Refreshing(object sender, EventArgs e)
        {
            listaFilm.ItemsSource = null;
            listaFilm.ItemsSource = ArchivioDeiFilm;
            //indica che l'aggiornamento è terminato
            listaFilm.IsRefreshing = false;
        }


        //ricava l'indice dell'Item selezionato
        private void listaImpegni_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            index = (listaFilm.ItemsSource as System.Collections.ObjectModel.ObservableCollection<Film>).IndexOf(e.Item as Film);
        }

        private void Salva_Clicked(object sender, EventArgs e)
        {
            ArchivioDeiFilm.Save();
        }

        private void rimuovi_Clicked(object sender, EventArgs e)
        {
            //ArchivioDeiFilm
            if (index >= 0)
            {
                DisplayAlert("Avviso", "Film rimosso", "", "Chiudi");
                ArchivioDeiFilm.RemoveAt(index);
                listaFilm.ItemsSource = null;
                listaFilm.ItemsSource = ArchivioDeiFilm;
                ArchivioDeiFilm.Save();
                index = -1;
            }
            else
            {
                DisplayAlert("Avviso", "Seleziona il film poi schiaccia il bottone", "", "Chiudi");
            }
        }

        //apre il video
        private async void openVideo_Clicked(object sender, EventArgs e)
        {
            ImageButton m = sender as ImageButton;
            if (m != null)
            {
                // Anche CommandParameter è un object e serve un cast a Film
                Film f = m.CommandParameter as Film;

                if (f != null)
                {
                    await Browser.OpenAsync(f.Link, BrowserLaunchMode.SystemPreferred);
                }
            }
        }
    }
}
