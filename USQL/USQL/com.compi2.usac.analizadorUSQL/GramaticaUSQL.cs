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
            RegexBasedTerminal flotante = new RegexBasedTerminal("flotante","[0-9]+.[0-9]+");
            RegexBasedTerminal date = new RegexBasedTerminal("date", "(0?[1-9]|[1-2][0-9]|3[0-1])-(0?[1-9]|1[0-2])-[0-9]{4}");
            DataLiteralBase datetime = new DataLiteralBase("datetime", TypeCode.DateTime);
            IdentifierTerminal id = new IdentifierTerminal("id");
            StringLiteral cadenaNormal = TerminalFactory.CreateCSharpString("cadenaNormal");
            StringLiteral cadena = new StringLiteral("cadenaNormal", "\"");
            StringLiteral fechahora = new StringLiteral("datetime", "'");
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
                | numero
                | flotante
                | date
                | datetime
                | verdad
                | falso
                | id
                | cadena;
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
