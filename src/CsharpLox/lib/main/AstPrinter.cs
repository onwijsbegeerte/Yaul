using System.Text;

namespace main
{
    public class AstPrinter : Expr.Visitor<string>
    {
       public string print(Expr expr) {
            return expr.accept(this);
        }
        
        public string visitBinaryExpr(Expr.Binary expr)
        {
            return parenthesize(expr.TokenOperator.Lexeme, expr.Left, expr.Right);
        }

        public string visitGroupingExpr(Expr.Grouping expr)
        {
            return parenthesize("Grouping", expr.Expression);
        }

        public string visitLiteralExpr(Expr.Literal expr)
        {
            return expr.Value == null ? "nill" : parenthesize(expr.Value.ToString());
        }

        public string visitUnaryExpr(Expr.Unary expr)
        {
            return parenthesize(expr.TokenOperator.Lexeme, expr.Right);
        }
        
        private string parenthesize(string name, params Expr[] expressions)
        {
            var builder = new StringBuilder();

            builder.Append("(").Append(name);
            foreach (var expression in expressions)
            {
                builder.Append(" ");
                builder.Append(expression.accept(this));
            }
            builder.Append(")");
              
            
            return builder.ToString();
        }
       
    }
    
    
}