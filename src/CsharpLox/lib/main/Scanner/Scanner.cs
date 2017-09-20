using System.Collections.Generic;

namespace main
{
    public class Scanner
    {
        public Scanner(string source)
        {
            Source = source;
        }

        public string Source { get; private set; }
        public List<Token> tokens()
        {
            var tokens = new List<Token>();
            foreach (var character in Source)
            {
                tokens.Add(new Token() { Value = character.ToString() });
            }
            return tokens;
        }
    }
}