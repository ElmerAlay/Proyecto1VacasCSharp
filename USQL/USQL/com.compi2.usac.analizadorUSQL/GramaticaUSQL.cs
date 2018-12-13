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
            StringLiteral cadenaNormal = TerminalFactory.CreateCSharpString("cadenaNormal");
            StringLiteral cadena = new StringLiteral("cadenaNormal", "\"");
            #endregion

            #region TERMINALES
            var plus = ToTerm("+");
            var minus = ToTerm("-");
            var mult = ToTerm("*");
            var div = ToTerm("/");

            var token_If = ToTerm("if");
            var token_else = ToTerm("else");

            var llave_o = ToTerm("{");
            var llave_c = ToTerm("}");
            var par_o = ToTerm("(");
            var par_c = ToTerm(")");

            var verdad = ToTerm("true");
            var falso = ToTerm("false");

            var mientras = ToTerm("while");
            #endregion

            #region NO TERMINALES
            NonTerminal S = new NonTerminal("S"),
            E = new NonTerminal("E"),
            IF = new NonTerminal("IF"),
            WHILE = new NonTerminal("WHILE"),
            COND = new NonTerminal("COND"),
            SENTS = new NonTerminal("SENTS"),
            SENT = new NonTerminal("SENT");
            #endregion

            #region GRAMATICA
            S.Rule = E
                | IF;

            IF.Rule = token_If + par_o + COND + par_c + llave_o + SENTS + llave_c
                | token_If + par_o + COND + par_c + llave_o + SENTS + llave_c + token_else + llave_o + SENTS + llave_c;

            COND.Rule = verdad | falso;

            SENTS.Rule = SENTS + SENT
                | SENT;

            SENT.Rule = IF | E;

            WHILE.Rule = mientras + par_o + COND + par_c + llave_o + SENTS + llave_c;

            E.Rule = E + plus + E
                | E + minus + E
                | E + mult + E
                | E + div + E
                | ToTerm("(") + E + ToTerm(")")
                | numero
                | id
                | cadena;
            #endregion

            #region PREFERENCIAS
            this.Root = S;
            this.RegisterOperators(20, Associativity.Left, plus, minus);
            this.RegisterOperators(30, Associativity.Left, mult, div);
            #endregion
        }
    }
}
