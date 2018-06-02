using System;
using System.Collections.Generic;

namespace main
{
    public class Parser
    {
        private int current = 0;
        private List<Token> Tokens { get; set; }
        

        public Parser(List<Token> tokens)
        {
            this.Tokens = tokens;
        }
        
        private Expr expression() {
            return equality();
        }

        private Expr equality() {
            Expr expr = comparison();

            while (match(TokenType.BANG_EQUAL, TokenType.EQUAL_EQUAL)) {
                Token _operator = previous();
                Expr right = comparison();
                expr = new Expr.Binary(expr, _operator, right);
            }

            return expr;
        }
        
        private bool match(params TokenType[] types)
        {
            foreach (var type in types)
            {
                if (check(type))
                {
                    advance();
                    return true;
                }
            }

            return false;
        }
        
        private Expr comparison() {
            Expr expr = addition();

            while (match(TokenType.GREATER, TokenType.GREATER_EQUAL, TokenType.LESS, TokenType.LESS_EQUAL)) {
                Token _operator = previous();
                Expr right = addition();
                expr = new Expr.Binary(expr, _operator, right);
            }

            return expr;
        }
        
        
        private bool check(TokenType tokenType) {
            if (isAtEnd()) return false;
            return peek().TokenType == tokenType;
        }
        
        private Token advance() {
            if (!isAtEnd()) current++;
            return previous();
        }
        
        private bool isAtEnd() {
            return peek().TokenType == TokenType.EOF;
        }

        private Token peek() {
            return Tokens[current];
        }

        private Token previous() {
            return Tokens[current - 1];
        }
        
        private Expr addition() {
            Expr expr = multiplication();

            while (match(TokenType.MINUS, TokenType.PLUS)) {
                Token _operator = previous();
                Expr right = multiplication();
                expr = new Expr.Binary(expr, _operator, right);
            }

            return expr;
        }

        private Expr multiplication() {
            Expr expr = unary();

            while (match(TokenType.SLASH, TokenType.STAR)) {
                Token _operator = previous();
                Expr right = unary();
                expr = new Expr.Binary(expr, _operator, right);
            }

            return expr;
        }
        
        private Expr unary() {
            if (match(TokenType.BANG, TokenType.MINUS)) {
                Token _operator = previous();
                Expr right = unary();
                return new Expr.Unary(_operator, right);
            }

            return primary();
        }
        
        private Expr primary() {
            if (match(TokenType.FALSE)) return new Expr.Literal(false);
            if (match(TokenType.TRUE)) return new Expr.Literal(true);
            if (match(TokenType.NIL)) return new Expr.Literal(null);

            if (match(TokenType.NUMBER, TokenType.STRING)) {
                return new Expr.Literal(previous().Literal);
            }

            if (match(TokenType.L_PARAM)) {
                Expr expr = expression();
                consume(TokenType.R_PARAM, "Expect ')' after expression.");
                return new Expr.Grouping(expr);
            }

            return null;
        }
        
        private Token consume(TokenType type, string message) {
            if (check(type)) return advance();

             error(peek(), message);

            return null;
        }
        
        private Exception error(Token token, string message) {
            Lox.error(token, message);
            return new Exception();
        }
    }
}