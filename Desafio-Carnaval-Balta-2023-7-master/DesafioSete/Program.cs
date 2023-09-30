Console.Clear();
Console.WriteLine("Informe o número de linhas da pirâmide na apresentação da série de Fibonacci:");
var maximo = Int32.TryParse(Console.ReadLine(), out int result) ? result : 50;
int n1 = 0, n2 = 1,  n3;
Console.Clear();
var listaNumeros = new List<int>();

for (int i = 0; i < maximo; i++)
{
    n3 = n1 + n2;
    listaNumeros.Add(n3);

    for (int j = 0; j < listaNumeros.Count; j++)
    {
        Console.Write(" {0}", listaNumeros[j]);
    }
    Console.WriteLine();
    n1 = n2;
    n2 = n3;
}