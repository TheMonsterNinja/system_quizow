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
        public int WybranaOdpowiedzId { get; set; }

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
            // Po POST zaciągamy dane quizu jeszcze raz, żeby widok miał co wyświetlać
            OnGet(id);

            CzyPokazacWynik = true;

            // Jeżeli nic nie zaznaczono
            if (WybranaOdpowiedzId == 0)
            {
                Komunikat = "Zaznacz odpowiedź zanim klikniesz Sprawdź.";
                return;
            }

            using var context = new QuizContext();
            var odp = context.Odpowiedzi.FirstOrDefault(o => o.Id == WybranaOdpowiedzId);

            if (odp != null && odp.CzyPoprawna)
                Komunikat = "Dobrze!";
            else
                Komunikat = "Źle!";
        }
    }
}
