using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ConsoleApp1.Models
{
    public class Odpowiedz : INotifyPropertyChanged
    {
        private string _tekst;
        private bool _czyPoprawna;

        public string Tekst { get => _tekst; set { _tekst = value; OnPropertyChanged(); } }
        public bool CzyPoprawna { get => _czyPoprawna; set { _czyPoprawna = value; OnPropertyChanged(); } }

        public Odpowiedz() { }
        public Odpowiedz(string tekst, bool czyPoprawna) { Tekst = tekst; CzyPoprawna = czyPoprawna; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public class Pytanie : INotifyPropertyChanged
    {
        private string _tresc;
        public string Tresc { get => _tresc; set { _tresc = value; OnPropertyChanged(); } }
        public ObservableCollection<Odpowiedz> Odpowiedzi { get; set; } = new ObservableCollection<Odpowiedz>();

        public Pytanie() { }
        public Pytanie(string tresc) { Tresc = tresc; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public class Quiz : INotifyPropertyChanged
    {
        private string _tytul;
        public string Tytul { get => _tytul; set { _tytul = value; OnPropertyChanged(); } }
        public ObservableCollection<Pytanie> Pytania { get; set; } = new ObservableCollection<Pytanie>();

        public Quiz() { }
        public Quiz(string tytul) { Tytul = tytul; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    // Prosty generyczny repozytorium
    public class Repozytorium<T>
    {
        private readonly ObservableCollection<T> dane = new ObservableCollection<T>();
        public void Dodaj(T element) => dane.Add(element);
        public ObservableCollection<T> PobierzWszystko() => dane;
    }
}