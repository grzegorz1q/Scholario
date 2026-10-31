using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Entities
{
    public enum GradeWeight
    {
        [Display(Name = "Sprawdzian")]
        Sprawdzian = 5,
        [Display(Name = "Kartkówka")]
        Kartkówka = 4,
        [Display(Name = "Odpowiedź")]
        Odpowiedź = 3,
        [Display(Name = "Praca domowa")]
        PracaDomowa = 2,
        [Display(Name = "Praca dodatkowa")]
        PracaDodatkowa = 1
    }
}
