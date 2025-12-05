using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    // ===== INTERFEJSY =====

    public interface IOdpowiedz
    {
        string Tekst { get; set; }
        bool CzyPoprawna { get; set; }
    }

    public interface IPytanie
    {
        string Tresc { get; set; }
        List<IOdpowiedz> Odpowiedzi { get; set; }
    }

    public interface IQuiz
    {
        string Tytul { get; set; }
        List<IPytanie> Pytania { get; set; }
    }

    // ===== KLASY =====

    public class Odpowiedz : IOdpowiedz
    {
        public string Tekst { get; set; } = string.Empty;
        public bool CzyPoprawna { get; set; } = false;

        public Odpowiedz() { }

        public Odpowiedz(string tekst, bool czyPoprawna)
        {
            Tekst = tekst;
            CzyPoprawna = czyPoprawna;
        }
    }

    public class Pytanie : IPytanie
    {
        public string Tresc { get; set; } = string.Empty;
        public List<IOdpowiedz> Odpowiedzi { get; set; } = new List<IOdpowiedz>();

        public Pytanie() { }

        public Pytanie(string tresc)
        {
            Tresc = tresc;
            Odpowiedzi = new List<IOdpowiedz>();
        }
    }

    public class Quiz : IQuiz
    {
        public string Tytul { get; set; } = string.Empty;
        public List<IPytanie> Pytania { get; set; } = new List<IPytanie>();

        public Quiz() { }

        public Quiz(string tytul)
        {
            Tytul = tytul;
            Pytania = new List<IPytanie>();
        }
    }

    // ===== GENERYKI =====

    // Prosty generyczny magazyn danych
    public class Repozytorium<T>
    {
        private readonly List<T> dane = new List<T>();

        public void Dodaj(T element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));
            dane.Add(element);
        }

        // Zwracamy IReadOnlyList, aby nie ujawniać wewnętrznej listy i uniknąć niezamierzonych modyfikacji
        public IReadOnlyList<T> PobierzWszystko()
        {
            return dane.AsReadOnly();
        }
    }

    // Punkt wejścia dla aplikacji konsolowej — wystarczy do szybkiego testu
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Uruchomiono program testowy.");

            var quiz = new Quiz("Testowy quiz");
            var pyt = new Pytanie("Ile to 1+1?");
            pyt.Odpowiedzi.Add(new Odpowiedz("1", false));
            pyt.Odpowiedzi.Add(new Odpowiedz("2", true));
            quiz.Pytania.Add(pyt);

            Console.WriteLine($"Quiz: {quiz.Tytul}");
            foreach (var p in quiz.Pytania)
            {
                Console.WriteLine($"Pytanie: {p.Tresc}");
                foreach (var o in p.Odpowiedzi)
                {
                    // o jest typu IOdpowiedz, ale implementacja to Odpowiedz — wypisujemy tekst
                    Console.WriteLine($" - {o.Tekst} {(o.CzyPoprawna ? "(poprawna)" : "")}");
                }
            }

            Console.WriteLine("Naciśnij Enter aby zakończyć...");
            Console.ReadLine();
        }
    }
}
