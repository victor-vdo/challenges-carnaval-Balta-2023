namespace DesafioSeis
{
    public class Cryptography
    {
        public string Encrypt(string value)
        {
            var dic = new Dicionario();

            var result = "";
            foreach (var character in value)
            {
                result += dic.Caracteres.TryGetValue(character, out char val) ? val : Char.MinValue;
            }
            
            return result;
        }

        public string Decrypt(string value)
        {
            var dic = new Dicionario();
            var result = "";
            foreach (var character in value)
            {
                result += dic.Caracteres.Where(c => c.Value == character).FirstOrDefault().Key;
            }
            return result;
        }
    }
}
