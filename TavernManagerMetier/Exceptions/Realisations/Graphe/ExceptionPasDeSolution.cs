using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TavernManagerMetier.Exceptions.Realisations.GestionTaverne
{
    /// <summary>
    /// Exception levé si la taverne n'admet pas de solution
    /// </summary>
    public class ExceptionPasDeSolution : ExceptionGestionTaverne
    {
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public ExceptionPasDeSolution() : base("La taverne dans laquelle vous essayez de travailler n'admet pas de solution.")
        {

        }
    }
}
