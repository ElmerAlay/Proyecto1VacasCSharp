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
            RegexBasedTerminal flotante = new RegexBasedTerminal("flotante","([0-9]+)\\.([0-9]+)");
            RegexBasedTerminal fecha = new RegexBasedTerminal("fecha", "(0?[1-9]|[1-2][0-9]|3[0-1])-(0?[1-9]|1[0-2])-[0-9]{4}");
            DataLiteralBase fechahora = new DataLiteralBase("fechahora", TypeCode.DateTime);
            IdentifierTerminal id = new IdentifierTerminal("id");
            RegexBasedTerminal variable = new RegexBasedTerminal("variable", "@([a-zA-ZñÑ])([a-zA-ZñÑ0-9_])*");
            StringLiteral cadenaNormal = TerminalFactory.CreateCSharpString("cadenaNormal");
            StringLiteral cadena = new StringLiteral("cadenaNormal", "\"");
            StringLiteral stringfechahora = new StringLiteral("fechahora","'");
            StringLiteral stringfecha = new StringLiteral("fecha", "'");
            #endregion

            #region TERMINALES
            var plus = ToTerm("+");
            var minus = ToTerm("-");
            var mult = ToTerm("*");
            var div = ToTerm("/");
            var pot = ToTerm("^");

            var gt = ToTerm(">");
            var lt = ToTerm("<");
            var gte = ToTerm(">=");
            var lte = ToTerm("<=");
            var eq = ToTerm("==");
            var neq = ToTerm("!=");

            var and = ToTerm("&&");
            var or = ToTerm("||");
            var not = ToTerm("!");

            var texto = ToTerm("text");
            var integer = ToTerm("integer");
            var doble = ToTerm("double");
            var boolean = ToTerm("boolean");
            var tipodate = ToTerm("date");
            var datetime = ToTerm("datetime");

            var declarar = ToTerm("declarar");

            var coma = ToTerm(",");
            var puntoycoma = ToTerm(";");
            var asign = ToTerm("=");

            var token_If = ToTerm("si");
            var token_else = ToTerm("else");

            var llave_o = ToTerm("{");
            var llave_c = ToTerm("}");
            var par_o = ToTerm("(");
            var par_c = ToTerm(")");

            var retornar = ToTerm("retornar");
            var detener = ToTerm("detener");

            var mientras = ToTerm("mientras");
            #endregion

            #region NO TERMINALES
            NonTerminal S = new NonTerminal("S"),
            E = new NonTerminal("E"),
            IF = new NonTerminal("IF"),
            WHILE = new NonTerminal("WHILE"),
            SENTS = new NonTerminal("SENTS"),
            SENT = new NonTerminal("SENT"),
            TIPO = new NonTerminal("TIPO"),
            LVAR = new NonTerminal("LVAR"),
            DEC = new NonTerminal("DEC");
            #endregion

            #region GRAMATICA
            S.Rule = DEC 
                | E
                | IF
                | WHILE;

            DEC.Rule = declarar + LVAR + TIPO + puntoycoma
                | declarar + LVAR + TIPO + asign + E + puntoycoma;

            LVAR.Rule = LVAR + coma + variable
                | variable;

            TIPO.Rule = texto | integer | doble | boolean | tipodate | datetime;
            
            
            IF.Rule = token_If + par_o + E + par_c + llave_o + SENTS + llave_c
                | token_If + par_o + E + par_c + llave_o + SENTS + llave_c + token_else + llave_o + SENTS + llave_c;

            SENTS.Rule = SENTS + SENT
                | SENT;

            SENT.Rule = IF | E | retornar | detener;

            WHILE.Rule = mientras + par_o + E + par_c + llave_o + SENTS + llave_c;



            E.Rule = E + plus + E
                | E + minus + E
                | E + mult + E
                | E + div + E
                | E + pot + E
                | E + gt + E
                | E + lt + E
                | E + gte + E
                | E + lte + E
                | E + eq + E
                | E + neq + E
                | E + and + E
                | E + or + E
                | par_o + E + par_c
                | minus + E
                | not + E
                | cadena
                | numero
                | flotante
                | stringfecha
                | id
                | variable;
            #endregion

            #region PREFERENCIAS
            this.Root = S;
            this.RegisterOperators(5, Associativity.Left, or);
            this.RegisterOperators(10, Associativity.Left, and);
            this.RegisterOperators(15, Associativity.Left, gt, lt, gte, lte, eq, neq);
            this.RegisterOperators(20, Associativity.Left, plus, minus);
            this.RegisterOperators(30, Associativity.Left, mult, div);

            this.RegisterOperators(10, Associativity.Right, not);
            this.RegisterOperators(20, Associativity.Right, pot);
            #endregion
        }
    }
}
