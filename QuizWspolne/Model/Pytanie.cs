using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class Pytanie : IQuestion
    {
        public int Id { get; set; }
        public string Tresc { get; set; }

        // Klucz obcy do Quizu
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        // Nawigacja do odpowiedzi
        public List<Odpowiedz> Odpowiedzi { get; set; } = new();
    }

}
