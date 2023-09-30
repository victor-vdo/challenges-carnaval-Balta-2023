Console.BackgroundColor = ConsoleColor.DarkBlue;
double troco = 0, venda = 0, pagamento = 0, moedasTroco = 0;
int falta = 0, trocoNotas = 0;
List<int> listaNotas = new List<int>()
{
    1,2,5,10, 20, 50, 100, 200
};

List<int> quantidadeNotas = new List<int>();
Console.WriteLine("Informe o valor final da venda:");
venda = Double.TryParse(Console.ReadLine(), out double resVenda) ? resVenda : Double.Epsilon;
Console.WriteLine("Informe o pagamento:");
pagamento = Double.TryParse(Console.ReadLine(), out double resPagamento) ? resPagamento : Double.Epsilon;

troco = pagamento - venda;
trocoNotas = (int)troco;
moedasTroco = troco - trocoNotas;
falta = trocoNotas;

SeparaNotas(out quantidadeNotas);
ContaNotas(quantidadeNotas);

#region Métodos
void SeparaNotas(out List<int> qtdNotas)
{
    qtdNotas = new List<int>();
    do
    {
    Retorna:
        foreach (var nota in listaNotas.OrderByDescending(o => o))
        {
            if (falta >= nota)
            {
                falta -= nota;
                
                switch (nota)
                {
                    case 200:
                        qtdNotas.Add(200);
                        if (falta >= 200)
                            goto Retorna;
                        break;
                    case 100:
                        qtdNotas.Add(100);
                        if (falta >= 100)
                            goto Retorna;
                        break;
                    case 50:
                        qtdNotas.Add(50);
                        if (falta >= 50)
                            goto Retorna;
                        break;
                    case 20:
                        qtdNotas.Add(20);
                        if (falta >= 20)
                            goto Retorna;
                        break;
                    case 10:
                        qtdNotas.Add(10);
                        if (falta > 10)
                            goto Retorna;
                        break;
                    case 5:
                        qtdNotas.Add(5);
                        if (falta >= 5)
                            goto Retorna;
                        break;
                    case 2:
                        qtdNotas.Add(2);
                        if (falta >= 2)
                            goto Retorna;
                        break;
                    case 1:
                        qtdNotas.Add(1);
                        if (falta >= 1)
                            goto Retorna;
                        break;
                    default:
                        break;
                }
            }
        }
    } while (falta != 0);
}

void ContaNotas(List<int> quantidadeNotas)
{
    var grupo = quantidadeNotas.GroupBy(g => g);
    Console.WriteLine("Troco: ");
    foreach (var grp in grupo)
    {
        PrintaConsole(grp.Count(), grp.Key.ToString());
    }
    if (moedasTroco != 0)
        Console.WriteLine("Resta um saldo de {0} centavos", moedasTroco);
}

void PrintaConsole(int quantidade, string valor)
{
    if(quantidade != 0)
    {
        Console.WriteLine(quantidade + " notas de " + valor);
    }
}
#endregion