using System;
using System.Collections.Generic;

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
    public string Tekst { get; set; }
    public bool CzyPoprawna { get; set; }

    public Odpowiedz(string tekst, bool czyPoprawna)
    {
        Tekst = tekst;
        CzyPoprawna = czyPoprawna;
    }
}

public class Pytanie : IPytanie
{
    public string Tresc { get; set; }
    public List<IOdpowiedz> Odpowiedzi { get; set; }

    public Pytanie(string tresc)
    {
        Tresc = tresc;
        Odpowiedzi = new List<IOdpowiedz>();
    }
}

public class Quiz : IQuiz
{
    public string Tytul { get; set; }
    public List<IPytanie> Pytania { get; set; }

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
    private List<T> dane = new List<T>();

    public void Dodaj(T element)
    {
        dane.Add(element);
    }

    public List<T> PobierzWszystko()
    {
        return dane;
    }
}
