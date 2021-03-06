using System;
using System.Collections.Generic;
using System.Text;
using Compi_I_Project.Core.Interfaces;
using Compi_I_Project.Core.Models;

namespace Compi_I_Project.Lexer
{
    public class Scanner : IScanner
    {
        private Input input;
        private readonly ILogger logger;
        private readonly Dictionary<string, TokenType> keywords;

        public Scanner(Input input, ILogger logger)
        {
            this.logger = logger;
            this.input = input;
            this.keywords = getKeywords();
        }

        private Dictionary<string, TokenType> getKeywords() {
            return new Dictionary<string, TokenType>
            {
                ["if"] = TokenType.KwIf,
                ["else"] = TokenType.KwElse,
                ["for"] = TokenType.KwFor,
                ["while"] = TokenType.KwWhile,
                ["end"] = TokenType.KwEnd,
                //Poner mas palabras reservadas...
                ["in"] = TokenType.KwIn,
                ["do"] = TokenType.KwdDo,
                ["module"] = TokenType.KwModule,
                ["elsif"] = TokenType.KwElsif,
                ["next"] = TokenType.KwNext,
                ["alias"] = TokenType.KwAlias,
                ["nil"] = TokenType.kwNil,
                ["false"] = TokenType.KwFalse,
                ["super"] = TokenType.KwSuper,
                ["not"] = TokenType.KwNot,
                ["ensure"] = TokenType.KwEnsure,
                ["then"] = TokenType.KwThen,
                ["or"] = TokenType.OpLogicalOr,
                ["and"] = TokenType.OpLogicalAnd,
                ["until"] = TokenType.KwUntil,
                ["break"] = TokenType.KwBreak,
                ["when"] = TokenType.KwWhen,
                ["case"] = TokenType.KwCase,
                ["rescue"] = TokenType.KwRescue,
                ["true"] = TokenType.KwTrue,
                ["class"] = TokenType.KwClass,
                ["retry"] = TokenType.KwRetry,
                ["undef"] = TokenType.KwUndef,
                ["defined?"] = TokenType.KwUndef,
                ["def"] = TokenType.KwDef,
                ["return"] = TokenType.KwReturn,
                ["unless"] = TokenType.KwUnless,
                ["self"] = TokenType.KwSelf,
                ["puts"] = TokenType.KwPuts
            };
        }

        public Token GetNextToken()
        {
            var lexeme = new StringBuilder();
            var currentChar = this.GetNextChar();

            while (currentChar != '\0')
            {

                if (char.IsWhiteSpace(currentChar) || currentChar == '\n')
                {
                    currentChar = this.GetNextChar();
                    continue;
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

                else if (char.IsDigit(currentChar))
                {
                    lexeme.Append(currentChar);
                    currentChar = PeekNextChar();
                    while (char.IsDigit(currentChar))
                    {
                        currentChar = GetNextChar();
                        lexeme.Append(currentChar);
                        currentChar = PeekNextChar();
                    }

                    if (currentChar == '.')
                    {
                        currentChar = GetNextChar();
                        lexeme.Append(currentChar);
                        currentChar = PeekNextChar();
                        while (char.IsDigit(currentChar))
                        {
                            currentChar = GetNextChar();
                            lexeme.Append(currentChar);
                            currentChar = PeekNextChar();
                        }
                    }

                    return BuildToken(lexeme.ToString(), TokenType.Number);
                }

                switch (currentChar)
                {
                    case '\0':
                        return BuildToken("\0", TokenType.OpEOF);
                    case '+':
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.OpPlus);
                    case '-':
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.OpMinus);
                    case '*':
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.OpMul);
                    case '/':
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.OpDiv);
                    case '%':
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.OpMod);
                    case '<':
                        lexeme.Append(currentChar);
                        var nextChar = this.PeekNextChar();
                        if (nextChar == '=')
                        {
                            currentChar = this.GetNextChar();
                            lexeme.Append(currentChar);
                            return BuildToken(lexeme.ToString(), TokenType.OpLessOrEqualThan);
                        }
                        return BuildToken(lexeme.ToString(), TokenType.OpLessThan);
                    case '>':
                        lexeme.Append(currentChar);
                        nextChar = this.PeekNextChar();
                        if (nextChar == '=')
                        {
                            currentChar = this.GetNextChar();
                            lexeme.Append(currentChar);
                            return BuildToken(lexeme.ToString(), TokenType.OpGreaterOrEqualThan);
                        }
                        return BuildToken(lexeme.ToString(), TokenType.OpGreaterThan);
                    case ':':
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.OpColon);
                    case ';':
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.OpSemicolon);
                    case '(':
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.OpLeftParens);
                    case ')':
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.OpRightParens);
                    case '[':
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.OpLeftBracket);
                    case ']':
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.OpRightBracket);
                    case '{':
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.OpLeftBrace);
                    case '}':
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.OpRightBrace);
                    case ',':
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.OpComma);
                    case '=':
                        lexeme.Append(currentChar);
                        nextChar = this.PeekNextChar();
                        if (nextChar == '=')
                        {
                            currentChar = this.GetNextChar();
                            lexeme.Append(currentChar);
                            return BuildToken(lexeme.ToString(), TokenType.OpEqual);
                        }
                        return BuildToken(lexeme.ToString(), TokenType.OpAssignation);
                    case '\'':
                        lexeme.Append(currentChar);
                        currentChar = GetNextChar();
                        while (currentChar != '\'')
                        {
                            lexeme.Append(currentChar);
                            currentChar = GetNextChar();
                        }
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.OpStringLiteral);

                    case '&':
                        lexeme.Append(currentChar);
                        currentChar = GetNextChar();
                        if (currentChar == '&')
                        {
                            lexeme.Append(currentChar);
                            return BuildToken(lexeme.ToString(), TokenType.OpLogicalAnd);
                        }
                        lexeme.Clear();
                        logger.Error($"Expected & but {currentChar} was found, line ine: {this.input.Position.Line} and column: {this.input.Position.Column}");
                        continue;
                    case '|':
                        lexeme.Append(currentChar);
                        currentChar = GetNextChar();
                        if (currentChar == '|')
                        {
                            lexeme.Append(currentChar);
                            return BuildToken(lexeme.ToString(), TokenType.OpLogicalOr);
                        }
                        lexeme.Clear();
                        logger.Error($"Expected | but {currentChar} was found, line ine: {this.input.Position.Line} and column: {this.input.Position.Column}");
                        continue;
                    default:
                        logger.Error($"Invalid token '{currentChar}' was found, line ine: {this.input.Position.Line} and column: {this.input.Position.Column}");
                        return BuildToken(lexeme.ToString(), TokenType.Unknown);
                }
            }

            return new Token
            {
                TokenType = TokenType.OpEOF,
                Lexeme = lexeme.ToString(),
                Column = this.input.Position.Column,
                Line = this.input.Position.Line
            };
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
