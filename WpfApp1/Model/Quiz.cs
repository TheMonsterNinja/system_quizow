using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class Quiz : IQuiz
    {
        public int Id { get; set; }
        public string Tytul { get; set; }

        // Nawigacja do pytań (relacja 1..n)
        public List<Pytanie> Pytania { get; set; } = new();
    }
}
