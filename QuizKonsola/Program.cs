using System;
using System.Linq;
using WpfApp1.Model;

namespace QuizKonsola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== QUIZ - KONSOLA (dodawanie pytań) ===");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1) Pokaż quizy");
                Console.WriteLine("2) Dodaj pytanie + 3 odpowiedzi do quizu");
                Console.WriteLine("0) Wyjście");
                Console.Write("Wybór: ");

                var wybor = Console.ReadLine();

                if (wybor == "0") break;

                if (wybor == "1")
                {
                    PokazQuizy();
                }
                else if (wybor == "2")
                {
                    DodajPytanieZOdopwiedziami();
                }
                else
                {
                    Console.WriteLine("Nieznana opcja.");
                }
            }
        }

        static void PokazQuizy()
        {
            using var context = new QuizContext();

            var quizy = context.Quizy
                .OrderBy(q => q.Id)
                .ToList();

            if (quizy.Count == 0)
            {
                Console.WriteLine("Brak quizów w bazie. Dodaj quiz w WPF.");
                return;
            }

            Console.WriteLine("--- Quizy ---");
            foreach (var q in quizy)
            {
                Console.WriteLine($"ID: {q.Id} | Tytuł: {q.Tytul}");
            }
        }

        static void DodajPytanieZOdopwiedziami()
        {
            using var context = new QuizContext();

            // 1) pokaż quizy
            var quizy = context.Quizy.OrderBy(q => q.Id).ToList();
            if (quizy.Count == 0)
            {
                Console.WriteLine("Brak quizów. Dodaj najpierw quiz w WPF.");
                return;
            }

            Console.WriteLine("--- Wybierz quiz ---");
            foreach (var q in quizy)
                Console.WriteLine($"ID: {q.Id} | {q.Tytul}");

            Console.Write("Podaj ID quizu: ");
            if (!int.TryParse(Console.ReadLine(), out int quizId))
            {
                Console.WriteLine("Złe ID.");
                return;
            }

            var quiz = context.Quizy.FirstOrDefault(q => q.Id == quizId);
            if (quiz == null)
            {
                Console.WriteLine("Nie ma quizu o takim ID.");
                return;
            }

            // 2) pytanie
            Console.Write("Wpisz treść pytania: ");
            var trescPytania = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(trescPytania))
            {
                Console.WriteLine("Treść pytania nie może być pusta.");
                return;
            }

            var pytanie = new Pytanie
            {
                Tresc = trescPytania,
                QuizId = quiz.Id
            };

            context.Pytania.Add(pytanie);
            context.SaveChanges(); // żeby pytanie dostało Id

            // 3) odpowiedzi
            Console.WriteLine("Dodajemy 3 odpowiedzi:");
            Console.Write("Odpowiedź 1: ");
            var o1 = Console.ReadLine();

            Console.Write("Odpowiedź 2: ");
            var o2 = Console.ReadLine();

            Console.Write("Odpowiedź 3: ");
            var o3 = Console.ReadLine();

            Console.Write("Która odpowiedź jest poprawna? (1/2/3): ");
            var poprawna = Console.ReadLine();

            int poprawnaNr = 0;
            int.TryParse(poprawna, out poprawnaNr);

            context.Odpowiedzi.Add(new Odpowiedz { Tresc = o1 ?? "", CzyPoprawna = (poprawnaNr == 1), Pytanie = pytanie });
            context.Odpowiedzi.Add(new Odpowiedz { Tresc = o2 ?? "", CzyPoprawna = (poprawnaNr == 2), Pytanie = pytanie });
            context.Odpowiedzi.Add(new Odpowiedz { Tresc = o3 ?? "", CzyPoprawna = (poprawnaNr == 3), Pytanie = pytanie });


            context.SaveChanges();
            Console.WriteLine($"ID nowego pytania: {pytanie.Id}");


            Console.WriteLine("Dodano pytanie i odpowiedzi do quizu.");
        }
    }
}
