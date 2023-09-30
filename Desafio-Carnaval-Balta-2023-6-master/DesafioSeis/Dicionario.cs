namespace DesafioSeis
{
    public class Dicionario
    {
        public Dictionary<char, char> Caracteres { get; set; }
        public Dicionario()
        {
            Caracteres = DicionarioCriptografia();
        }

        public Dictionary<char, char> DicionarioCriptografia()
        {
            var dic = new Dictionary<char, char>()
            {
                {'q', '/' },{'w', '~' },{'e', '´' },{'r', ';' },{'t', 'ç' },{'y', 'p' },{'u', '.' },{'i', 'l' },{'o', 'o' },{'p', ',' },{'[', 'k' },{'{', 'i' },
                {'a', 'm' },{'s', 'j' },{'d', 'u' },{'f', 'n' },{'g', 'h' },{'h', 'y' },{'j', 'a' },{'k', 'q' },{'l', '!' },{'ç', '@' },{']', '#' },{'}', 'z' },
                {'z', '$' },{'x', '%' },{'c', '¨' },{'v', '&' },{'b', '*' },{'n', '(' },{'m', ')' },{',', '-' },{'.', '=' },{';', '{' },{'/', '}' },{'?', '\\' },
                {'1', 'b' },{'2', 'g' },{'3', 't' },{'4', 'v' },{'5', 'f' },{'6', 'r' },{'7', 'c' },{'8', 'd' },{'9', 'e' },{'0', 'x' },{'-', 's' },{'=', 'w' },
            };

            return dic;
        }
    }
}
