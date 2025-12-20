using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class MenedzerQuizow<TQuiz> where TQuiz : IQuiz
    {
        private readonly List<TQuiz> _quizy = new();

        public void DodajQuiz(TQuiz quiz)
        {
            _quizy.Add(quiz);
        }

        public IEnumerable<TQuiz> PobierzWszystkie()
        {
            return _quizy;
        }

        // Tu jest i generyk, i LINQ
        public IEnumerable<TQuiz> SzukajPoFragmencieTytulu(string fragment)
        {
            return _quizy
                .Where(q => q.Tytul != null && q.Tytul.Contains(fragment));
        }
    }
}
