﻿'Generated by the GOLD Parser Builder

Option Explicit On
Option Strict Off

Imports System.IO
Imports System.Windows.Forms;


Module MyParser
    Private Parser As New GOLD.Parser

    Private Enum SymbolIndex
        [Eof] = 0                                 ' (EOF)
        [Error] = 1                               ' (Error)
        [Whitespace] = 2                          ' Whitespace
        [Comma] = 3                               ' ','
        [Colon] = 4                               ' ':'
        [Lbracket] = 5                            ' '['
        [Rbracket] = 6                            ' ']'
        [Cadena] = 7                              ' cadena
        [Col] = 8                                 ' col
        [Datos] = 9                               ' datos
        [Error2] = 10                             ' error
        [Fila] = 11                               ' fila
        [Instruccion] = 12                        ' instruccion
        [Lenguaje] = 13                           ' lenguaje
        [Msg] = 14                                ' msg
        [Numero] = 15                             ' numero
        [Otro] = 16                               ' otro
        [Paq_error] = 17                          ' 'paq_error'
        [Paquete] = 18                            ' paquete
        [Reporte] = 19                            ' reporte
        [Tipo] = 20                               ' tipo
        [Usql] = 21                               ' usql
        [Validar] = 22                            ' validar
        [Error_paq] = 23                          ' <ERROR_PAQ>
        [Lcads] = 24                              ' <LCADS>
        [Login_rec] = 25                          ' <LOGIN_REC>
        [Reporte_rec] = 26                        ' <REPORTE_REC>
        [S] = 27                                  ' <S>
        [Tipo2] = 28                              ' <TIPO>
        [Usql_rec] = 29                           ' <USQL_REC>
    End Enum

    Private Enum ProductionIndex
        [S] = 0                                   ' <S> ::= <ERROR_PAQ>
        [S2] = 1                                  ' <S> ::= <REPORTE_REC>
        [S3] = 2                                  ' <S> ::= <USQL_REC>
        [S4] = 3                                  ' <S> ::= <LOGIN_REC>
        [Error_paq_Lbracket_Paquete_Colon_Paq_error_Comma_Tipo_Colon_Comma_Msg_Colon_Cadena_Comma_Datos_Colon_Lbracket_Error_Colon_Cadena_Comma_Instruccion_Colon_Cadena_Comma_Col_Colon_Numero_Comma_Fila_Colon_Numero_Rbracket_Rbracket] = 4 ' <ERROR_PAQ> ::= '[' paquete ':' 'paq_error' ',' tipo ':' <TIPO> ',' msg ':' cadena ',' datos ':' '[' error ':' cadena ',' instruccion ':' cadena ',' col ':' numero ',' fila ':' numero ']' ']'
        [Tipo_Lenguaje] = 5                       ' <TIPO> ::= lenguaje
        [Tipo_Otro] = 6                           ' <TIPO> ::= otro
        [Reporte_rec_Lbracket_Paquete_Colon_Reporte_Comma_Datos_Colon_Cadena_Rbracket] = 7 ' <REPORTE_REC> ::= '[' paquete ':' reporte ',' datos ':' cadena ']'
        [Usql_rec_Lbracket_Paquete_Colon_Usql_Comma_Datos_Colon_Lbracket_Rbracket_Rbracket] = 8 ' <USQL_REC> ::= '[' paquete ':' usql ',' datos ':' '[' <LCADS> ']' ']'
        [Lcads_Lbracket_Cadena_Rbracket] = 9      ' <LCADS> ::= <LCADS> '[' cadena ']'
        [Lcads_Lbracket_Cadena_Rbracket2] = 10    ' <LCADS> ::= '[' cadena ']'
        [Login_rec_Lbracket_Validar_Colon_Numero_Comma_Datos_Colon_Cadena_Rbracket] = 11 ' <LOGIN_REC> ::= '[' validar ':' numero ',' datos ':' cadena ']'
    End Enum

    Public Program As Object     'You might derive a specific object

    Public Sub Setup()
        'This procedure can be called to load the parse tables. The class can
        'read tables using a BinaryReader.
        
        Parser.LoadTables(Path.Combine(Application.StartupPath, "grammar.egt"))
    End Sub
    
    Public Function Parse(ByVal Reader As TextReader) As Boolean
        'This procedure starts the GOLD Parser Engine and handles each of the
        'messages it returns. Each time a reduction is made, you can create new
        'custom object and reassign the .CurrentReduction property. Otherwise, 
        'the system will use the Reduction object that was returned.
        '
        'The resulting tree will be a pure representation of the language 
        'and will be ready to implement.

        Dim Response As GOLD.ParseMessage
        Dim Done as Boolean                  'Controls when we leave the loop
        Dim Accepted As Boolean = False      'Was the parse successful?

        Accepted = False    'Unless the program is accepted by the parser

        Parser.Open(Reader)
        Parser.TrimReductions = False  'Please read about this feature before enabling  

        Done = False
        Do Until Done
            Response = Parser.Parse()

            Select Case Response              
                Case GOLD.ParseMessage.LexicalError
                    'Cannot recognize token
                    Done = True

                Case GOLD.ParseMessage.SyntaxError
                    'Expecting a different token
                    Done = True

                Case GOLD.ParseMessage.Reduction
                    'Create a customized object to store the reduction
                    .CurrentReduction = CreateNewObject(Parser.CurrentReduction)

                Case GOLD.ParseMessage.Accept
                    'Accepted!
                    'Program = Parser.CurrentReduction  'The root node!                 
                    Done = True
                    Accepted = True

                Case GOLD.ParseMessage.TokenRead
                    'You don't have to do anything here.

                Case GOLD.ParseMessage.InternalError
                    'INTERNAL ERROR! Something is horribly wrong.
                    Done = True

                Case GOLD.ParseMessage.NotLoadedError
                    'This error occurs if the CGT was not loaded.                   
                    Done = True

                Case GOLD.ParseMessage.GroupError 
                    'COMMENT ERROR! Unexpected end of file
                    Done = True
            End Select
        Loop

        Return Accepted
    End Function

    Private Function CreateNewObject(Reduction as GOLD.Reduction) As Object
        Dim Result As Object = Nothing

        With Reduction
            Select Case .Parent.TableIndex                        
                Case ProductionIndex.S                 
                    ' <S> ::= <ERROR_PAQ> 

                Case ProductionIndex.S2                 
                    ' <S> ::= <REPORTE_REC> 

                Case ProductionIndex.S3                 
                    ' <S> ::= <USQL_REC> 

                Case ProductionIndex.S4                 
                    ' <S> ::= <LOGIN_REC> 

                Case ProductionIndex.Error_paq_Lbracket_Paquete_Colon_Paq_error_Comma_Tipo_Colon_Comma_Msg_Colon_Cadena_Comma_Datos_Colon_Lbracket_Error_Colon_Cadena_Comma_Instruccion_Colon_Cadena_Comma_Col_Colon_Numero_Comma_Fila_Colon_Numero_Rbracket_Rbracket                 
                    ' <ERROR_PAQ> ::= '[' paquete ':' 'paq_error' ',' tipo ':' <TIPO> ',' msg ':' cadena ',' datos ':' '[' error ':' cadena ',' instruccion ':' cadena ',' col ':' numero ',' fila ':' numero ']' ']' 

                Case ProductionIndex.Tipo_Lenguaje                 
                    ' <TIPO> ::= lenguaje 

                Case ProductionIndex.Tipo_Otro                 
                    ' <TIPO> ::= otro 

                Case ProductionIndex.Reporte_rec_Lbracket_Paquete_Colon_Reporte_Comma_Datos_Colon_Cadena_Rbracket                 
                    ' <REPORTE_REC> ::= '[' paquete ':' reporte ',' datos ':' cadena ']' 

                Case ProductionIndex.Usql_rec_Lbracket_Paquete_Colon_Usql_Comma_Datos_Colon_Lbracket_Rbracket_Rbracket                 
                    ' <USQL_REC> ::= '[' paquete ':' usql ',' datos ':' '[' <LCADS> ']' ']' 

                Case ProductionIndex.Lcads_Lbracket_Cadena_Rbracket                 
                    ' <LCADS> ::= <LCADS> '[' cadena ']' 

                Case ProductionIndex.Lcads_Lbracket_Cadena_Rbracket2                 
                    ' <LCADS> ::= '[' cadena ']' 

                Case ProductionIndex.Login_rec_Lbracket_Validar_Colon_Numero_Comma_Datos_Colon_Cadena_Rbracket                 
                    ' <LOGIN_REC> ::= '[' validar ':' numero ',' datos ':' cadena ']' 

            End Select
        End With     

        Return Result
    End Function
End Module
