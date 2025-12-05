using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ConsoleApp1.Helpers;
using ConsoleApp1.Models;
using System.Linq;
using System.Windows.Input;

namespace ConsoleApp1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // Kolekcje i repozytorium
        private readonly Repozytorium<Quiz> repo = new Repozytorium<Quiz>();
        public ObservableCollection<Quiz> Quizy => repo.PobierzWszystko();

        // Nowe elementy (bindowane do pól tekstowych)
        public string NowyTytul { get; set; } = string.Empty;
        public string NowePytanieTresc { get; set; } = string.Empty;
        public string NowaOdpowiedzTekst { get; set; } = string.Empty;
        public bool NowaOdpowiedzCzyPoprawna { get; set; } = false;

        // Wybrane
        private Quiz? _wybranyQuiz;
        public Quiz? WybranyQuiz { get => _wybranyQuiz; set { _wybranyQuiz = value; OnPropertyChanged(); OnPropertyChanged(nameof(WybranePytania)); } }
        public ObservableCollection<Pytanie> WybranePytania => WybranyQuiz?.Pytania ?? new ObservableCollection<Pytanie>();

        private Pytanie? _wybranePytanie;
        public Pytanie? WybranePytanie { get => _wybranePytanie; set { _wybranePytanie = value; OnPropertyChanged(); OnPropertyChanged(nameof(WybraneOdpowiedzi)); } }
        public ObservableCollection<Odpowiedz> WybraneOdpowiedzi => WybranePytanie?.Odpowiedzi ?? new ObservableCollection<Odpowiedz>();

        private Odpowiedz? _wybranaOdpowiedz;
        public Odpowiedz? WybranaOdpowiedz { get => _wybranaOdpowiedz; set { _wybranaOdpowiedz = value; OnPropertyChanged(); } }

        // Komendy
        public ICommand DodajQuizCommand { get; }
        public ICommand DodajPytanieCommand { get; }
        public ICommand DodajOdpowiedzCommand { get; }

        // Tryb quizu
        public ICommand RozpocznijQuizCommand { get; }
        public ICommand NastepnePytanieCommand { get; }
        public ICommand ZakonczQuizCommand { get; }

        private int currentQuestionIndex;
        private int score;

        public Pytanie BiezacePytanie => (WybranyQuiz != null && WybranyQuiz.Pytania.Count > currentQuestionIndex && currentQuestionIndex >= 0)
            ? WybranyQuiz.Pytania[currentQuestionIndex]
            : new Pytanie("") ;

        public ObservableCollection<Odpowiedz> BiezaceOdpowiedzi => new ObservableCollection<Odpowiedz>(BiezacePytanie?.Odpowiedzi ?? Enumerable.Empty<Odpowiedz>());
        private Odpowiedz? _wybranaOdpowiedzDoQuizu;
        public Odpowiedz? WybranaOdpowiedzDoQuizu { get => _wybranaOdpowiedzDoQuizu; set { _wybranaOdpowiedzDoQuizu = value; OnPropertyChanged(); } }

        public string StatusQuizu { get; set; } = "Nie rozpoczêto";
        public string WynikTekst { get; set; } = "0 / 0";

        public MainViewModel()
        {
            // przyk³adowe dane (opcjonalne)
            var q = new Quiz("Przyk³adowy quiz");
            var p = new Pytanie("Ile wynosi 2+2?");
            p.Odpowiedzi.Add(new Odpowiedz("3", false));
            p.Odpowiedzi.Add(new Odpowiedz("4", true));
            p.Odpowiedzi.Add(new Odpowiedz("5", false));
            q.Pytania.Add(p);
            repo.Dodaj(q);

            DodajQuizCommand = new RelayCommand(_ => DodajQuiz(), _ => !string.IsNullOrWhiteSpace(NowyTytul));
            DodajPytanieCommand = new RelayCommand(_ => DodajPytanie(), _ => WybranyQuiz != null && !string.IsNullOrWhiteSpace(NowePytanieTresc));
            DodajOdpowiedzCommand = new RelayCommand(_ => DodajOdpowiedz(), _ => WybranePytanie != null && !string.IsNullOrWhiteSpace(NowaOdpowiedzTekst));

            RozpocznijQuizCommand = new RelayCommand(_ => RozpocznijQuiz(), _ => WybranyQuiz != null && WybranyQuiz.Pytania.Any());
            NastepnePytanieCommand = new RelayCommand(_ => NastepnePytanie(), _ => WybranyQuiz != null && currentQuestionIndex >= 0);
            ZakonczQuizCommand = new RelayCommand(_ => ZakonczQuiz(), _ => currentQuestionIndex >= 0);
        }

        private void DodajQuiz()
        {
            var quiz = new Quiz(NowyTytul.Trim());
            repo.Dodaj(quiz);
            NowyTytul = string.Empty;
            OnPropertyChanged(nameof(NowyTytul));
            OnPropertyChanged(nameof(Quizy));
        }

        private void DodajPytanie()
        {
            if (WybranyQuiz == null) return;
            var p = new Pytanie(NowePytanieTresc.Trim());
            WybranyQuiz.Pytania.Add(p);
            NowePytanieTresc = string.Empty;
            OnPropertyChanged(nameof(NowePytanieTresc));
            OnPropertyChanged(nameof(WybranePytania));
        }

        private void DodajOdpowiedz()
        {
            if (WybranePytanie == null) return;
            var o = new Odpowiedz(NowaOdpowiedzTekst.Trim(), NowaOdpowiedzCzyPoprawna);
            WybranePytanie.Odpowiedzi.Add(o);
            NowaOdpowiedzTekst = string.Empty;
            NowaOdpowiedzCzyPoprawna = false;
            OnPropertyChanged(nameof(NowaOdpowiedzTekst));
            OnPropertyChanged(nameof(NowaOdpowiedzCzyPoprawna));
            OnPropertyChanged(nameof(WybraneOdpowiedzi));
        }

        private void RozpocznijQuiz()
        {
            if (WybranyQuiz == null || !WybranyQuiz.Pytania.Any()) return;
            currentQuestionIndex = 0;
            score = 0;
            WybranaOdpowiedzDoQuizu = null;
            StatusQuizu = $"Pytanie {currentQuestionIndex + 1} / {WybranyQuiz.Pytania.Count}";
            WynikTekst = $"{score} / {WybranyQuiz.Pytania.Count}";
            OnPropertyChanged(nameof(BiezacePytanie));
            OnPropertyChanged(nameof(BiezaceOdpowiedzi));
            OnPropertyChanged(nameof(StatusQuizu));
            OnPropertyChanged(nameof(WynikTekst));
        }

        private void NastepnePytanie()
        {
            if (WybranyQuiz == null) return;
            // sprawdŸ odpowiedŸ
            if (WybranaOdpowiedzDoQuizu != null && WybranaOdpowiedzDoQuizu.CzyPoprawna)
                score++;

            // przejdŸ do nastêpnego
            currentQuestionIndex++;
            if (currentQuestionIndex >= WybranyQuiz.Pytania.Count)
            {
                // koniec
                StatusQuizu = "Koniec quizu";
                WynikTekst = $"{score} / {WybranyQuiz.Pytania.Count}";
                OnPropertyChanged(nameof(StatusQuizu));
                OnPropertyChanged(nameof(WynikTekst));
                // zresetuj indeks aby nie dopuœciæ do kolejnych nextów bez restartu
                currentQuestionIndex = -1;
                OnPropertyChanged(nameof(BiezacePytanie));
                OnPropertyChanged(nameof(BiezaceOdpowiedzi));
                WybranaOdpowiedzDoQuizu = null;
                return;
            }

            WybranaOdpowiedzDoQuizu = null;
            StatusQuizu = $"Pytanie {currentQuestionIndex + 1} / {WybranyQuiz.Pytania.Count}";
            WynikTekst = $"{score} / {WybranyQuiz.Pytania.Count}";
            OnPropertyChanged(nameof(BiezacePytanie));
            OnPropertyChanged(nameof(BiezaceOdpowiedzi));
            OnPropertyChanged(nameof(StatusQuizu));
            OnPropertyChanged(nameof(WynikTekst));
        }

        private void ZakonczQuiz()
        {
            if (WybranyQuiz == null) return;
            currentQuestionIndex = -1;
            StatusQuizu = "Zakoñczono";
            WynikTekst = $"{score} / {WybranyQuiz.Pytania.Count}";
            OnPropertyChanged(nameof(StatusQuizu));
            OnPropertyChanged(nameof(WynikTekst));
            OnPropertyChanged(nameof(BiezacePytanie));
            OnPropertyChanged(nameof(BiezaceOdpowiedzi));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? nm = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nm));
    }
}