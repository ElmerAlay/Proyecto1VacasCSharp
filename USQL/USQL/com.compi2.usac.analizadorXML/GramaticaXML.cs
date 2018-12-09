using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace USQL.com.compi2.usac.analizadorXML
{
    class GramaticaXML: Grammar
    {
        public GramaticaXML()
            : base(caseSensitive: false)
        {
            #region Expresiones Regulares
            RegexBasedTerminal numero = new RegexBasedTerminal("numero", "[0-9]+");
            IdentifierTerminal id = new IdentifierTerminal("id");
            StringLiteral cadenaNormal = TerminalFactory.CreateCSharpString("cadenaNormal");
            StringLiteral cadena = new StringLiteral("cadenaNormal", "\"");
            #endregion

            #region Terminales
            var dbO = ToTerm("<db>");
            var dbC = ToTerm("</db>");

            var nombreO = ToTerm("<nombre>");
            var nombreC = ToTerm("</nombre>");

            var pathO = ToTerm("<path>");
            var pathC = ToTerm("</path>");

            var procedureO = ToTerm("<procedure>");
            var procedureC = ToTerm("</procedure>");

            var objectO = ToTerm("<object>");
            var objectC = ToTerm("</object>");

            var tableO = ToTerm("<tabla>");
            var tableC = ToTerm("</tabla>");

            var rowO = ToTerm("<rows>");
            var rowC = ToTerm("</rows>");

            var intO = ToTerm("<int>");
            var intC = ToTerm("</int>");

            var boolO = ToTerm("<bool>");
            var boolC = ToTerm("</bool>");

            var textO = ToTerm("<text>");
            var textC = ToTerm("</text>");

            var paramO = ToTerm("<params>");
            var paramC = ToTerm("</params>");

            var srcO = ToTerm("<src>");
            var srcC = ToTerm("</src>");

            var attrO = ToTerm("<attr>");
            var attrC = ToTerm("</attr>");

            var gt = ToTerm("<");
            var lt = ToTerm(">");
            var diagonal = ToTerm("/");
            #endregion

            #region No Terminales
            NonTerminal S = new NonTerminal("S"),
            BDS = new NonTerminal("BDS"),
            BD = new NonTerminal("BD"),
            REGISTROS = new NonTerminal("REGISTROS"),
            REGISTRO = new NonTerminal("REGISTRO"),
            PROCEDURE = new NonTerminal("PROCEDURE"),
            OBJECT = new NonTerminal("OBJECT"),
            TABLE = new NonTerminal("TABLE"),
            CAMPOS = new NonTerminal("CAMPOS"),
            CAMPO = new NonTerminal("CAMPO"),
            ROWS = new NonTerminal("ROWS"),
            ROW = new NonTerminal("ROW"),
            PROCS = new NonTerminal("PROCS"),
            PROC = new NonTerminal("PROC"),
            OBJS = new NonTerminal("OBJS"),
            OBJ = new NonTerminal("OBJ");
            #endregion

            #region Gramatica
            S.Rule = BDS | REGISTROS | ROWS | PROCS | OBJS;

            BDS.Rule = BDS + BD
                | BD;

            BD.Rule = dbO + 
                        nombreO + 
                            cadena + 
                        nombreC +
                        pathO +
                            cadena +
                        pathC +
                      dbC;

            REGISTROS.Rule = REGISTROS + REGISTRO
                | REGISTRO;

            REGISTRO.Rule = PROCEDURE | OBJECT | TABLE;

            PROCEDURE.Rule = procedureO + pathO + cadena + pathC + procedureC;

            OBJECT.Rule = objectO + pathO + cadena + pathC + objectC;

            TABLE.Rule = tableO + 
                            nombreO + cadena + nombreC +
                            pathO + cadena + pathC +
                            rowO + CAMPOS + rowC +
                         tableC;

            CAMPOS.Rule = CAMPOS + CAMPO
                | CAMPO;

            CAMPO.Rule = intO + cadena + intC
                | textO + cadena + textC
                | boolO + cadena + boolC
                | gt + id + lt + cadena + gt + diagonal + id + lt;

            ROWS.Rule = ROWS + ROW
                | ROW;

            ROW.Rule = rowO + CAMPOS + rowC;

            PROCS.Rule = PROCS + PROC
                | PROC;

            PROC.Rule = procedureO +
                            nombreO + cadena + nombreC +
                            paramO + CAMPOS + paramC +
                            srcO + cadena + srcC +
                        procedureC;

            OBJS.Rule = OBJS + OBJ
                | OBJ;

            OBJ.Rule = objectO +
                            nombreO + cadena + nombreC +
                            attrO + CAMPOS + attrC +
                       objectC;
            #endregion

            #region Preferencias
            this.Root = S;
            this.MarkPunctuation("<db>", "</db>", "<nombre>", "</nombre>",
                "<path>", "</path>", "<procedure>", "</procedure>", "<object>", "</object>",
                "<tabla>", "</tabla>", "<rows>", "</rows>", "</int>", "</text>",
                "</bool>", "<", ">", "/", "<params>", "</params>", "<src>", "</src>", "<attr>", "</attr>");
            #endregion
        }
    }
}
