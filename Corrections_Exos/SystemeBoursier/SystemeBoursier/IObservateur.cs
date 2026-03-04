using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemeBoursier
{
    internal interface IObservateur
    {
        void MettreAJour(string codeAction, double prix);
    }
}
