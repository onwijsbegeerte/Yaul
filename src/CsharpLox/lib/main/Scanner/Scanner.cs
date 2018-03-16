using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace main
{
    public interface IScanner
    {
        List<Token> tokens();
    }


    public class Scanner : IScanner
    {
        private bool isAtEnd() => Current >= Source.Length;
        public string Source { get; private set; }
        public int Line { get; private set; }
        private int Current { get; set; }
        private int Start { get; set; }
        public List<Token> Tokens { get; set; }

        private static Dictionary<string, TokenType> Keywords = new Dictionary<string, TokenType>()
        {
            {"and", TokenType.AND},
            {"class", TokenType.CLASS},
            {"else", TokenType.ELSE},
            {"false", TokenType.FALSE},
            {"for", TokenType.FOR},
            {"fun", TokenType.FUN},
            {"if", TokenType.IF},
            {"nil", TokenType.NIL},
            {"or", TokenType.OR},
            {"print", TokenType.PRINT},
            {"return", TokenType.RETURN},
            {"super", TokenType.SUPER},
            {"this", TokenType.THIS},
            {"true", TokenType.TRUE},
            {"var", TokenType.VAR},
            {"while", TokenType.WHILE}
        };

        private Lox Lox { get; set; }


        public Scanner(string source)
        {
            Source = source;
            Tokens = new List<Token>();
        }

        public List<Token> tokens()
        {
            if (Source.Length == 0)
                return Tokens;

            Start = 0;
            Current = 0;
            Line = 1;

            while (!isAtEnd())
            {
                Start = Current;
                ScanToken();
            }

            Tokens.Add(new Token(TokenType.EOF, "", null, Line) { });
            return Tokens;
        }


        private void ScanToken()
        {
            char c = advance();
            switch (c)
            {
                case '(':
                    addToken(TokenType.L_PARAM);
                    break;
                case ')':
                    addToken(TokenType.R_PARAM);
                    break;
                case '{':
                    addToken(TokenType.L_BRACE);
                    break;
                case '}':
                    addToken(TokenType.R_BRACE);
                    break;
                case ',':
                    addToken(TokenType.COMMA);
                    break;
                case '.':
                    addToken(TokenType.DOT);
                    break;
                case '-':
                    addToken(TokenType.MINUS);
                    break;
                case '+':
                    addToken(TokenType.PLUS);
                    break;
                case ';':
                    addToken(TokenType.SEMICOLON);
                    break;
                case '*':
                    addToken(TokenType.STAR);
                    break;
                case '"':
                    extractString();
                    break;
                case '!':
                    addToken(match('=') ? TokenType.BANG_EQUAL : TokenType.BANG);
                    break;
                case '=':
                    addToken(match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL);
                    break;
                case '>':
                    addToken(match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER);
                    break;
                case '<':
                    addToken(match('=') ? TokenType.LESS_EQUAL : TokenType.LESS);
                    break;
                case '/':
                    if ((match('/')))
                    {
                        while (peek() != '\n' && !isAtEnd())
                        {
                            advance();
                        }
                    }
                    else if (match('*'))
                    {
                        comment();
                    }
                    else
                    {
                        addToken(TokenType.SLASH);
                    }

                    break;
                case ' ':
                case '\r':
                case '\t':

                    break;
                case '\n':
                    Line++;
                    
                    break;
                default:
                    if (char.IsDigit(c))
                    {
                        number();
                    }
                    else if(char.IsLetter(c))
                    {
                        _identifier();
                    }
                    ErrorLogger.Error(Line, "Unexpected character.");
                    
                    break;
            }
        }

        private void comment()
        {
            while (peek() != '*' && peekNext() != '/')
            {
                advance();
            }

            if (peek() == '*' && peekNext() == '/')
            {
                addToken(TokenType.COMMENT, Source.Substring(Start + 2, Current - 2));
            }
            else
            {
                ErrorLogger.Error(Line, "Unterminated comment");
            }
        }

        private void _identifier()
        {
            while (char.IsLetterOrDigit(peek()))
            {
                advance();
            }
            
            string text = Source.Substring(Start, Current);

            addToken(Keywords.ContainsKey(text) ? Keywords[text] : TokenType.IDENTIFIER);
        }

        private void number()
        {
            while (char.IsDigit(peek()))
            {
                advance();
            }

            while (peek() == '.' && char.IsDigit(peekNext()))
            {
                advance();

                while (char.IsDigit(peek()))
                {
                    advance();
                }
            }

            addToken(TokenType.NUMBER, double.Parse(Source.Substring(Start, Current)));
        }

        private void extractString()
        {
            while (peek() != '"' && !isAtEnd())
            {
                if (peek() == '\n') Line++;
                advance();
            }

            if (isAtEnd())
            {
                ErrorLogger.Error(Line, "Unterminated string");
                return;
            }

            advance();

            string value = Source.Substring(Start + 1, Current - 2);
            addToken(TokenType.STRING, value);
        }

        private char peekNext()
        {
            if (isAtEnd()) return '\0';
            return Source[Current + 1];
        }

        private char peek()
        {
            if (isAtEnd()) return '\0';
            return Source[Current];
        }

        private bool match(char expected)
        {
            if (isAtEnd()) return false;

            if (this.Source[Current] != expected) return false;

            Current++;

            return true;
        }

        private char advance()
        {
            Current++;
            return Source[Current - 1];
        }

        private void addToken(TokenType type)
        {
            addToken(type, null);
        }

        private void addToken(TokenType type, Object literal)
        {
            String text = Source.Substring(Start, Current);
            Tokens.Add(new Token(type, text, literal, Line));
        }
    }
}