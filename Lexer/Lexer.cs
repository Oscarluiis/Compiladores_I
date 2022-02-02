using System;
using System.Collections.Generic;
using System.Text;
using Compi_Project.Core.Interfaces;
using Compi_Project.Core.Models;

namespace Compi_Project.Lexer
{
    public class Lexer
    {
        private Input input;
        private readonly ILogger logger;
        private readonly Dictionary<string, TokenType> keywords;

       

        public Lexer(Input input, ILogger logger)
        {
            this.logger = logger;
            this.input = input;
            
        }

        public Token GetNextToken()
        {
            var lexeme = new StringBuilder();
            var currentChar = this.GetNextChar();

            while (currentChar != '\0')
            {
                if (char.IsWhiteSpace (currentChar) || currentChar == '\n')
                {
                    currentChar = this.GetNextChar();
                }

                if (char.IsLetter(currentChar))
                {
                    lexeme.Append(currentChar);
                    currentChar = this.PeekNextChar();
                    while (char.IsLetterOrDigit(currentChar))
                    {
                        currentChar = this.GetNextChar();
                        lexeme.Append(currentChar);
                        currentChar = this.PeekNextChar();
                    }

                    var tokenLexeme = lexeme.ToString();
                    if (this.keywords.ContainsKey(tokenLexeme))
                    {
                        return BuildToken(tokenLexeme, this.keywords[tokenLexeme]);
                    }

                    return BuildToken(tokenLexeme, TokenType.Identifier);
                }

            }






            return new Token { TokenType = TokenType.Unknown, Lexeme = lexeme.ToString(),
                Column = this.input.Position.Column, Line = this.input.Position.Line } ;
        }

        private char GetNextChar()
        {
            var next = input.NextChar();
            input = next.Reminder;
            return next.Value;
        }

        private char PeekNextChar()
        {
            var next = input.NextChar();
            return next.Value;
        }

        private Token BuildToken(string lexeme, TokenType tokenType)
        {
            return new Token
            {
                Column = this.input.Position.Column,
                Line = this.input.Position.Line,
                Lexeme = lexeme,
                TokenType = tokenType,
            };
        }
    }

}
