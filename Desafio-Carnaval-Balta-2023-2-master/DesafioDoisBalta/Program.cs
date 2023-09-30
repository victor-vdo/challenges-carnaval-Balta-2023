Console.BackgroundColor = ConsoleColor.DarkBlue;
Console.WriteLine("Informe sua altura:");
var altura = Double.TryParse(Console.ReadLine(), out double result) ?result: Double.Epsilon;
Console.WriteLine("Informe seu peso:");
var peso = Double.TryParse(Console.ReadLine(), out double res) ? res : Double.Epsilon;
var imc = peso / (altura*altura);
var risco = "Nenhum";

Dictionary<double, (string,string)> dicImc = new Dictionary<double, (string,string)>()
{
    {16, ("Muito abaixo do peso", String.Empty) },
    {17,("Abaixo do peso", String.Empty) },
    {18.5,("Peso normal", String.Empty) },
    {25,("Acima do peso", String.Empty) },
    {30,("Obesidade", "I" )},
    {35,("Obesidade", "II" )},
    {40,("Obesidade", "III") }
};

(string status, string grau) tuple = (String.Empty, String.Empty);

tuple.status = dicImc
    .OrderBy(e => Math.Abs(e.Key - imc))
    .Where(x => x.Key <= imc)
    .FirstOrDefault()
    .Value.Item1;

tuple.grau = dicImc
    .OrderBy(e => Math.Abs(e.Key - imc))
    .Where(x => x.Key <= imc)
    .FirstOrDefault()
    .Value.Item2;

Console.WriteLine("Seu IMC é " + imc);

if(tuple.grau != String.Empty && tuple.status.Equals("Obesidade"))
{
    Console.WriteLine("Sobrepeso");
    Console.WriteLine("Obesidade Grau " + tuple.grau);
    risco = "Aumentado";
}
Console.WriteLine("Risco: "+risco);