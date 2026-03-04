using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemeBoursier
{
    internal class Action
    {
        private string _codeAction;
        private double _prix;

        public double Prix
        {
            get => _prix;
            set
            {
                double ancienPrix = _prix;
                _prix = value;

                if ((Math.Abs(_prix - ancienPrix) / ancienPrix) >= 0.02) // variation de 2%
                {
                    Notifier();
                }
            }
        }

        private List<IObservateur> _observateurs;

        public Action(string codeAction, double prix)
        {
            _codeAction = codeAction;
            _prix = prix;
            _observateurs = new List<IObservateur>();
        }

        public void Ajouter(IObservateur observateur) 
        {
            if (!_observateurs.Contains(observateur))
            {
                _observateurs.Add(observateur);
            }
        }

        public void Enlever(IObservateur observateur)
        {
            _observateurs.Remove(observateur);
        }

        private void Notifier()
        {
            foreach (IObservateur observateur in _observateurs)
            {
                observateur.MettreAJour(_codeAction, _prix);
            }
        }

    }
}
