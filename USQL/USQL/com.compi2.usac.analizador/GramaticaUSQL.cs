using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace USQL.com.compi2.usac.analizador
{
    class GramaticaUSQL : Grammar
    {

        public GramaticaUSQL() : base(caseSensitive : false)
        {
            #region EXPRESIONES REGULARES
            RegexBasedTerminal numero = new RegexBasedTerminal("numero", "[0-9]+");
            IdentifierTerminal id = new IdentifierTerminal("id");
            #endregion

            #region TERMINALES
            var plus = ToTerm("+");
            var minus = ToTerm("-");
            var mult = ToTerm("*");
            var div = ToTerm("/");
            #endregion

            #region NO TERMINALES
            NonTerminal S = new NonTerminal("S"),
            E = new NonTerminal("E");
            #endregion

            #region GRAMATICA
            S.Rule = E;
            E.Rule = E + plus + E
                | E + minus + E
                | E + mult + E
                | E + div + E
                | numero
                | id;
            #endregion

            #region PREFERENCIAS
            this.Root = S;
            #endregion
        }
    }
}
