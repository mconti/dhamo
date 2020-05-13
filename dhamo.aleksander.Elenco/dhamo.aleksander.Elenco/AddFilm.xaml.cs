using dhamo.aleksander.Elenco.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static dhamo.aleksander.Elenco.Models.Film;

namespace dhamo.aleksander.Elenco
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFilm
    {
        public string genere { get; set; }

        Films Archivio { get; set; }

        MainPage finestra;

        public AddFilm()
        {
            InitializeComponent();
        }

        //riceve l'archio dalla MainPage e lo aggiorna
        public AddFilm(Films ArchivioFilm)
        {
            InitializeComponent();
            Archivio = ArchivioFilm;
        }

        //scegli la categoria del film
        private async void Genere_Clicked(object sender, EventArgs e)
        {
            genere = await DisplayActionSheet("Scegli il genere del film", "Annulla", "", "Azione", "Comico", "Fantasy");
        }

        //verifica se abbiamo inserito i dati e poi attiva il bottone
        private void Film_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(edtNewFilm.Text) && !string.IsNullOrEmpty(edtNewLink.Text))
            {
                Aggiungi.IsEnabled = true;
            }
            else if (string.IsNullOrEmpty(edtNewFilm.Text) || string.IsNullOrEmpty(edtNewLink.Text))
                Aggiungi.IsEnabled = false;
        }


        private async void Aggiungi_Clicked(object sender, EventArgs e)
        {
            AggiungiFilm();
            finestra = new MainPage(Archivio);
            await DisplayAlert("AVVISO", "Film aggiunto", "", "Chiudi");
            //chiude il Popup
            await PopupNavigation.Instance.PopAsync(true);
        }

        //in base al genere del film scelto inserisce il film nella classe
        public void AggiungiFilm()
        {
            if (genere == "Azione")
            {
                Archivio.Add(new Film { Titolo = edtNewFilm.Text, Genere = genere, Immagine = "azione.png", Link = edtNewLink.Text });
            }
            else if (genere == "Comico")
            {
                Archivio.Add(new Film { Titolo = edtNewFilm.Text, Genere = genere, Immagine = "commedia.png", Link = edtNewLink.Text });
            }
            else if (genere == "Fantasy")
            {
                Archivio.Add(new Film { Titolo = edtNewFilm.Text, Genere = genere, Immagine = "fantasy.png", Link = edtNewLink.Text });
            }
        }
    }
}