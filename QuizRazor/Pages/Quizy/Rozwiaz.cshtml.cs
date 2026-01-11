using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WpfApp1.Model;

namespace QuizRazor.Pages.Quiz
{
    public class RozwiazModel : PageModel
    {
        public WpfApp1.Model.Quiz? WybranyQuiz { get; set; }

        [BindProperty]
        public Dictionary<int, int> Wybor { get; set; } = new();

        public bool CzyPokazacWynik { get; set; }
        public string Komunikat { get; set; } = "";

        public void OnGet(int id)
        {
            using var context = new QuizContext();

            WybranyQuiz = context.Quizy
                .Include(q => q.Pytania)
                    .ThenInclude(p => p.Odpowiedzi)
                .FirstOrDefault(q => q.Id == id);
        }

        public void OnPost(int id)
        {
            OnGet(id);
            CzyPokazacWynik = true;

            if (WybranyQuiz == null)
            {
                Komunikat = "Nie znaleziono quizu.";
                return;
            }


            // jeśli nic nie zaznaczono
            if (Wybor == null || Wybor.Count == 0)
            {
                Komunikat = "Zaznacz odpowiedzi zanim klikniesz Sprawdź.";
                return;
            }

            using var context = new QuizContext();

            // pobieramy poprawne odpowiedzi dla quizu (szybko i prosto)
            var poprawneIds = context.Odpowiedzi
                .Where(o => o.CzyPoprawna)
                .Select(o => o.Id)
                .ToHashSet();

            int poprawne = 0;
            int wszystkie = WybranyQuiz.Pytania.Count;

            foreach (var pyt in WybranyQuiz.Pytania)
            {
                // jeśli użytkownik nie odpowiedział na to pytanie – traktujemy jako błędne
                if (!Wybor.TryGetValue(pyt.Id, out int wybraneOdpId))
                    continue;

                if (poprawneIds.Contains(wybraneOdpId))
                    poprawne++;
            }

            Komunikat = $"Wynik: {poprawne}/{wszystkie}";
        }

    }
}
