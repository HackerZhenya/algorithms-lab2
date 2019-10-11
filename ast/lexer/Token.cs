namespace algorithms_lab2.ast.lexer
{
    public class Token
    {
        public TokenType Type { get; }
        public string Value { get; }

        public Token(TokenType type, string value = null)
        {
            Type = type;
            Value = value;
        }

        public Token(TokenType type, char value) : this(type, value.ToString())
        {
        }

        public override bool Equals(object obj)
        {
            return obj is Token token &&
                   token.Type == Type &&
                   (
                       token.Type != TokenType.Number &&
                       token.Type != TokenType.Word ||
                       token.Value == Value
                   );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Type * 397) ^ (Value != null ? Value.GetHashCode() : 0);
            }
        }

        public override string ToString() =>
            Value == null ? $"[{Type.ToString()}]" : $"[{Type.ToString()} \"{Value}\"]";
    }
}