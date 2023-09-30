Console.BackgroundColor = ConsoleColor.DarkBlue;

double valorGasolina = 0, valorAlcool = 0;
Entrada entrada = new Entrada(EntradaDado);
entrada("Informe o valor da gasolina:", out valorGasolina);
entrada("Informe o valor do álcool:", out valorAlcool);

var relacao = (valorAlcool / valorGasolina) * 100;
var valorLimite = 72.0;

Saida saida = new Saida(SaidaDado);
saida(relacao, valorLimite);

double EntradaDado(string mensagem, out double valor)
{
    Console.WriteLine(mensagem);
    valor = Double.TryParse(Console.ReadLine(), out double result) ? result : Double.Epsilon;
    return valor;
}

void SaidaDado(double relacao, double valorLimite)
{
    if (relacao <= valorLimite)
        Console.WriteLine("Abasteça seu carro com álcool");
    else
        Console.WriteLine("Abasteça seu carro com gasolina");
}

public delegate double Entrada(string mensagem, out double valor);
public delegate void Saida(double relacao, double valorLimite);