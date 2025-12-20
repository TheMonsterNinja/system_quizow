using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Model;

namespace QuizWpf
{
    public partial class MainWindow : Window
    {
        private MenedzerQuizow<Quiz> _menedzer = new MenedzerQuizow<Quiz>();
        public MainWindow()
        {
            InitializeComponent();
            ZaladujQuizyZBazy();
        }

        private void PrzyciskDodaj_Click(object sender, RoutedEventArgs e)
        {
            var tytul = PoleTytulQuizu.Text;

            if (string.IsNullOrWhiteSpace(tytul))
            {
                MessageBox.Show("Podaj tytuł quizu.");
                return;
            }

            var nowyQuiz = new Quiz
            {
                Tytul = tytul
            };

            // C z CRUD (Create)
            using (var context = new QuizContext())
            {
                context.Quizy.Add(nowyQuiz);
                context.SaveChanges();
            }

            PoleTytulQuizu.Clear();
            ZaladujQuizyZBazy();
        }

        private void PrzyciskOdswiez_Click(object sender, RoutedEventArgs e)
        {
            ZaladujQuizyZBazy();
        }

        private void PrzyciskUsun_Click(object sender, RoutedEventArgs e)
        {
            // 1. Pobierz zaznaczony quiz z listy
            var zaznaczonyQuiz = ListaQuizow.SelectedItem as Quiz;

            if (zaznaczonyQuiz == null)
            {
                MessageBox.Show("Najpierw zaznacz quiz, który chcesz usunąć.");
                return;
            }

            // 2. Potwierdzenie (opcjonalne, ale ładnie wygląda)
            var wynik = MessageBox.Show(
                $"Czy na pewno chcesz usunąć quiz: \"{zaznaczonyQuiz.Tytul}\"?",
                "Potwierdzenie",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (wynik != MessageBoxResult.Yes)
                return;

            // 3. Usunięcie z bazy (D z CRUD)
            using (var context = new QuizContext())
            {
                var quizZBazy = context.Quizy.FirstOrDefault(q => q.Id == zaznaczonyQuiz.Id);
                if (quizZBazy != null)
                {
                    context.Quizy.Remove(quizZBazy);
                    context.SaveChanges();
                }
            }

            // 4. Odśwież listę
            ZaladujQuizyZBazy();
        }

        private void PrzyciskZapiszZmiany_Click(object sender, RoutedEventArgs e)
        {
            var zaznaczonyQuiz = ListaQuizow.SelectedItem as Quiz;

            if (zaznaczonyQuiz == null)
            {
                MessageBox.Show("Najpierw zaznacz quiz, który chcesz zmienić.");
                return;
            }

            var nowyTytul = PoleTytulQuizu.Text;

            if (string.IsNullOrWhiteSpace(nowyTytul))
            {
                MessageBox.Show("Tytuł nie może być pusty.");
                return;
            }

            using (var context = new QuizContext())
            {
                // Pobieramy ten sam quiz z bazy po Id
                var quizZBazy = context.Quizy.FirstOrDefault(q => q.Id == zaznaczonyQuiz.Id);

                if (quizZBazy != null)
                {
                    quizZBazy.Tytul = nowyTytul;
                    context.SaveChanges();
                }
            }

            // Po zapisaniu odświeżamy listę
            ZaladujQuizyZBazy();
        }


        private void ZaladujQuizyZBazy()
        {
            using (var context = new QuizContext())
            {
                var quizyZBazy = context.Quizy
                    .OrderBy(q => q.Tytul)
                    .ToList();

                // Tworzymy nowy menedżer i ładujemy do niego wszystkie quizy
                _menedzer = new MenedzerQuizow<Quiz>();
                foreach (var quiz in quizyZBazy)
                {
                    _menedzer.DodajQuiz(quiz);
                }
            }

            // Po załadowaniu z bazy od razu pokazujemy pełną listę,
            // uwzględniając ewentualny tekst w polu "Szukaj"
            OdswiezListeZFiltracja();
        }

        private void OdswiezListeZFiltracja()
        {
            if (_menedzer == null)
                return;

            var filtr = PoleSzukaj.Text;

            if (string.IsNullOrWhiteSpace(filtr))
            {
                // Bez filtra – wszystkie quizy
                ListaQuizow.ItemsSource = _menedzer
                    .PobierzWszystkie()
                    .ToList();
            }
            else
            {
                // Z filtrem – tu wykorzystujemy LINQ z Menedzera
                ListaQuizow.ItemsSource = _menedzer
                    .SzukajPoFragmencieTytulu(filtr)
                    .ToList();
            }
        }
        private void PoleSzukaj_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            OdswiezListeZFiltracja();
        }

        private void ListaQuizow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var zaznaczonyQuiz = ListaQuizow.SelectedItem as Quiz;

            if (zaznaczonyQuiz != null)
            {
                // Przepisujemy tytuł do pola tekstowego,
                // żeby można było go łatwo edytować
                PoleTytulQuizu.Text = zaznaczonyQuiz.Tytul;
            }
        }

    }
}