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
            StringLiteral cadena = new StringLiteral("cadenaSinComillas", "\"");
            #endregion

            #region Terminales
            var llaveO = ToTerm("[");
            var llaveC = ToTerm("]");
            var validar = ToTerm("validar");
            var login = ToTerm("login");
            var dosPuntos = ToTerm(":");
            var colon = ToTerm(",");

            var paquete = ToTerm("paquete");
            var fin = ToTerm("fin");

            var paq_error = ToTerm("paq_error");
            var tipo = ToTerm("tipo");
            var msg = ToTerm("msg");
            var datos = ToTerm("datos");
            var error = ToTerm("error");
            var instruccion = ToTerm("instruccion");
            var col = ToTerm("col");
            var fila = ToTerm("fila");
            var lenguaje = ToTerm("lenguaje");
            var otro = ToTerm("otro");

            var usql = ToTerm("usql");
            var reporte = ToTerm("reporte");
            #endregion

            #region No Terminales
            NonTerminal S = new NonTerminal("S"),
            LOGIN = new NonTerminal("LOGIN"),
            LOGOUT = new NonTerminal("LOGOUT"),
            ERROR_PAQ = new NonTerminal("ERROR_PAQ"),
            TIPO_ERR = new NonTerminal("TIPO_ERR"),
            USQL = new NonTerminal("USQL"),
            REPORTE = new NonTerminal("REPORTE"),
            REPORTE_REC = new NonTerminal("REPORTE_REC"),
            USQL_REC = new NonTerminal("USQL_REC"),
            LCADENAS = new NonTerminal("LCADENAS"),
            LOGIN_REC = new NonTerminal("LOGIN_REC");
            #endregion

            #region Gramatica
            S.Rule = LOGIN
                | LOGOUT
                | ERROR_PAQ
                | USQL
                | REPORTE
                | REPORTE_REC
                | USQL_REC
                | LOGIN_REC;
            
            LOGIN.Rule = llaveO + 
                            validar + dosPuntos + numero + colon + 
                            login + dosPuntos + llaveO + cadena + llaveC +
                         llaveC;
            
            LOGOUT.Rule = llaveO + paquete + dosPuntos + fin + llaveC;

            ERROR_PAQ.Rule = llaveO +
                                paquete + dosPuntos + paq_error + colon +
                                tipo + dosPuntos + TIPO_ERR + colon +
                                msg + dosPuntos + cadena + colon +
                                datos + dosPuntos +
                                llaveO +
                                    error + dosPuntos + cadena + colon +
                                    instruccion + dosPuntos + cadena + colon +
                                    col + dosPuntos + numero + colon +
                                    fila + dosPuntos + numero +
                                llaveC +
                             llaveC;

            TIPO_ERR.Rule = lenguaje
                | otro;

            USQL.Rule = llaveO + 
                                paquete + dosPuntos + usql + colon +
                                instruccion + dosPuntos + cadena +
                               llaveC;

            REPORTE.Rule = llaveO +
                                paquete + dosPuntos + reporte + colon +
                                instruccion + dosPuntos + cadena +
                           llaveC;

            REPORTE_REC.Rule = llaveO +
                                    paquete + dosPuntos + reporte + colon +
                                    datos + dosPuntos + cadena +
                               llaveC;

            USQL_REC.Rule = llaveO +
                                paquete + dosPuntos + usql + colon +
                                datos + dosPuntos + llaveO + LCADENAS + llaveC +
                            llaveC;

            LCADENAS.Rule = LCADENAS + llaveO + cadena + llaveC
                | llaveO + cadena + llaveC;

            LOGIN_REC.Rule = llaveO +
                                validar + dosPuntos + numero + colon +
                                datos + dosPuntos + cadena +
                            llaveC;
            #endregion

            #region Preferencias
            this.Root = S;
            this.MarkPunctuation("[", "]",",",":","login","validar","paquete","tipo","msg","datos",
                "error","instruccion","col","fila","paq_error","usql","reporte");
            #endregion
        }
    }
}
