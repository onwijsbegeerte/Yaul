namespace main.Parser
{
    public abstract class Expr
    {
    }

    public class Binary : Expr
    {
        Binary(Expr Left, Token TokenOperator, Expr Right)
        {
            this.Left = Left;
            this.TokenOperator = TokenOperator;
            this.Right = Right;
        }

        public Expr Left { get; set; }
        public Token TokenOperator { get; set; }
        public Expr Right { get; set; }
    }

    public class Grouping : Expr
    {
        Grouping(Expr Expression)
        {
            this.Expression = Expression;
        }

        public Expr Expression { get; set; }
    }

    public class Literal : Expr
    {
        Literal(object Value)
        {
            this.Value = Value;
        }

        public object Value { get; set; }
    }

    public class Unary : Expr
    {
        Unary(Token TokenOperator, Expr Right)
        {
            this.TokenOperator = TokenOperator;
            this.Right = Right;
        }

        public Token TokenOperator { get; set; }
        public Expr Right { get; set; }
    }
}