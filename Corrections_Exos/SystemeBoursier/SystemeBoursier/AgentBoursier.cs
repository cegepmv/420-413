using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemeBoursier
{
    internal class AgentBoursier : IObservateur
    {
        public void MettreAJour(string codeAction, double prix)
        {
            Console.WriteLine($"Achat/Vente suggéré pour {codeAction} — Nouveau prix {prix} $");
        }
    }
}
