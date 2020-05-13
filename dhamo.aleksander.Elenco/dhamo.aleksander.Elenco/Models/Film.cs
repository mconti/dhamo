using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace dhamo.aleksander.Elenco.Models
{
    public class Film
    {
        public string Titolo { get; set; }
        public string Link { get; set; }
        public string Immagine { get; set; }
        public string Genere { get; set; }

        public Film()
        {

        }

    }
    public class Films : ObservableCollection<Film>
    {
        public Films()
        {
        }

        public void Load()
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string fileName = Path.Combine(path, "films.csv");

                if (File.Exists(fileName))
                {
                    StreamReader sr = new StreamReader(fileName);
                    while (!sr.EndOfStream)
                    {
                        string riga = sr.ReadLine();
                        string[] colonne = riga.Split(';');
                        Film f = new Film { Titolo = colonne[0], Immagine = colonne[1], Link = colonne[2] };
                        Add(f);
                    }
                    sr.Close();
                }
            }
            catch (Exception errore) { }

        }
        public void Save()
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string fileName = Path.Combine(path, "films.csv");

                StreamWriter sw = new StreamWriter(fileName);

                foreach (Film f in this)
                {
                    sw.WriteLine($"{f.Titolo};{f.Immagine};{f.Link}");
                }
                sw.Close();
            }
            catch (Exception errore) { }

        }
    }
}
