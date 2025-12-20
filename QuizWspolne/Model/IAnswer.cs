using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public interface IAnswer
    {
        int Id { get; set; }
        string Tresc { get; set; }
        bool CzyPoprawna { get; set; }
        int QuestionId { get; set; }
    }

}
