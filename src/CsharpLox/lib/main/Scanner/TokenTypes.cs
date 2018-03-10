namespace main
{
    public enum TokenType{
        COMMA,
        DOT,
        MINUS,
        PLUS,
        SEMICOLON,
        STAR,
        L_PARAM,
        R_PARAM,
        L_BRACE,
        R_BRACE,
        EOF,
        BANG,
        BANG_EQUAL,
        EQUAL_EQUAL,
        LESS_EQUAL,
        GREATER_EQUAL,
        EQUAL,
        GREATER,
        LESS,
        NEWLINE,
        SLASH,
        STRING,
        IDENTIFIER, NUMBER,

        // Keywords.
        AND, CLASS, ELSE, FALSE, FUN, FOR, IF, NIL, OR,
        PRINT, RETURN, SUPER, THIS, TRUE, VAR, WHILE
    }
}