namespace main
{
    public abstract class Expr
    {
        public abstract R accept<R>(Visitor<R> visitor);

        public interface Visitor<R>
        {
            R visitBinaryExpr(Binary expr);
            R visitGroupingExpr(Grouping expr);
            R visitLiteralExpr(Literal expr);
            R visitUnaryExpr(Unary expr);
        }

        public class Binary : Expr
        {
            public Binary(Expr Left, Token TokenOperator, Expr Right)
            {
                this.Left = Left;
                this.TokenOperator = TokenOperator;
                this.Right = Right;
            }

            public override R accept<R>(Visitor<R> visitor)
            {
                return visitor.visitBinaryExpr(this);
            }

            public Expr Left { get; set; }
            public Token TokenOperator { get; set; }
            public Expr Right { get; set; }
        }

        public class Grouping : Expr
        {
            public Grouping(Expr Expression)
            {
                this.Expression = Expression;
            }

            public override R accept<R>(Visitor<R> visitor)
            {
                return visitor.visitGroupingExpr(this);
            }

            public Expr Expression { get; set; }
        }

        public class Literal : Expr
        {
            public  Literal(object Value)
            {
                this.Value = Value;
            }

            public override R accept<R>(Visitor<R> visitor)
            {
                return visitor.visitLiteralExpr(this);
            }

            public object Value { get; set; }
        }

        public class Unary : Expr
        {
            public  Unary(Token TokenOperator, Expr Right)
            {
                this.TokenOperator = TokenOperator;
                this.Right = Right;
            }

            public override R accept<R>(Visitor<R> visitor)
            {
                return visitor.visitUnaryExpr(this);
            }

            public Token TokenOperator { get; set; }
            public Expr Right { get; set; }
        }
    }

    
}