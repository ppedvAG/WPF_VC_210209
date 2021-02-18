using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Personendatenbank
{
    /// <summary>
    /// Interaction logic for Db_Uebersicht.xaml
    /// </summary>
    public partial class Db_Uebersicht : Window
    {
        public ObservableCollection<Person> Personenliste { get; set; } = new ObservableCollection<Person>()
        {
            new Person(){Vorname="Rainer", Nachname="Zufall", Geburtsdatum=new DateTime(1987, 5, 13), Verheiratet=true, Lieblingsfarbe=Colors.DarkSeaGreen, Geschlecht=Gender.Männlich, Kinder=2},
            new Person(){Vorname="Anna", Nachname="Nass", Geburtsdatum=new DateTime(1974, 11, 29), Verheiratet=false, Lieblingsfarbe=Colors.LightBlue, Geschlecht=Gender.Weiblich, Kinder=0}
        };

        public Db_Uebersicht()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        private void Mei_Beenden_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Btn_Loeschen_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Soll diese Person wirklich gelöscht werden?", "Person löschen?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                Personenliste.Remove(Dgd_Personen.SelectedItem as Person);
        }

        private void Btn_Neu_Click(object sender, RoutedEventArgs e)
        {
            PersonenDialog dialog = new PersonenDialog();

            if (dialog.ShowDialog() == true)
            {
                Personenliste.Add(dialog.DataContext as Person);
            }
        }

        private void Btn_Aendern_Click(object sender, RoutedEventArgs e)
        {
            if (Dgd_Personen.SelectedItem is Person)
            {
                PersonenDialog dialog = new PersonenDialog();

                dialog.DataContext = new Person(Dgd_Personen.SelectedItem as Person);

                dialog.Title = (dialog.DataContext as Person).Vorname + " " + (dialog.DataContext as Person).Nachname;

                if (dialog.ShowDialog() == true)
                    Personenliste[Dgd_Personen.SelectedIndex] = (dialog.DataContext as Person);
            }
        }

        private void ChangeLanguage(object sender, RoutedEventArgs e)
        {
            switch ((sender as MenuItem).Name)
            {
                case "Mei_Deutsch":
                    if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-DE");
                        ReloadWindow();

                        File.WriteAllText("settings.txt", "language=de-DE");
                    }
                    break;
                case "Mei_Englisch":
                    if (Thread.CurrentThread.CurrentUICulture.Name != "en-US")
                    {
                        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                        ReloadWindow();

                        File.WriteAllText("settings.txt", "language=en-US");
                    }
                    break;
            }
        }

        private void ReloadWindow()
        {
            new Db_Uebersicht().Show();
            this.Close();
        }
    }
}
