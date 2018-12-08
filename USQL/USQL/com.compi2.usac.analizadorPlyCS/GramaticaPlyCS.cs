using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace USQL.com.compi2.usac.analizadorPlyCS
{
    class GramaticaPlyCS : Grammar
    {
        public GramaticaPlyCS()
            : base(caseSensitive: false)
        {
            #region Expresiones Regulares
            RegexBasedTerminal numero = new RegexBasedTerminal("numero", "[0-9]+");
            IdentifierTerminal id = new IdentifierTerminal("id");
            StringLiteral cadenaSinComillas = TerminalFactory.CreateCSharpString("cadenaSinComillas");
            StringLiteral cadena = new StringLiteral("cadena", "\"");
            #endregion

            #region Terminales
            #endregion

            #region No Terminales
            #endregion

            #region Gramatica
            #endregion

            #region Preferencias
            #endregion
        }
    }
}
