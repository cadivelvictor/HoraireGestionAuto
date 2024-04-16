using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoThermometreLigne5
{
    public class Arret
    {
        public string Nom { get; private set; }
        public bool EstCorrespondance { get; private set; }
        public bool EstPoleEchange { get; private set; }
        public List<String> Correspondances { get; private set; }

        public Arret(string nom, bool estCorrespondance, bool estPoleEchange, List<String> correspondances)
        {
            Nom = nom;
            EstCorrespondance = estCorrespondance;
            EstPoleEchange = estPoleEchange;
            Correspondances = correspondances;
        }
    }
}
