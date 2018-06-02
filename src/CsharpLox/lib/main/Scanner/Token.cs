namespace main
{
    public class Token
    {
        public Token()
        {

        }
        public Token(TokenType tokenType, string lexeme, object literal, int line)
        {
            this.TokenType = tokenType;
            this.Lexeme = lexeme;
            this.Literal = literal;
            this.Line = line;
        }

        public override string ToString(){
            return $"Type: {TokenType} Literal: {Literal} Line: {Line}";
        }
        public TokenType TokenType { get; set; }
        public string Lexeme { get; set; }
        public object Literal { get; set; }
        public string Value { get; set; }
        public int Line { get; set; }
    }
}