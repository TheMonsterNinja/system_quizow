using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using WpfApp1.Model;

namespace QuizRazor.Pages.Quizy
{
    public class IndexModel : PageModel
    {
        public List<WpfApp1.Model.Quiz> ListaQuizow { get; set; } = new();


        public void OnGet()
        {
            using var context = new QuizContext();
            ListaQuizow = context.Quizy.OrderBy(q => q.Tytul).ToList();
        }
    }
}
