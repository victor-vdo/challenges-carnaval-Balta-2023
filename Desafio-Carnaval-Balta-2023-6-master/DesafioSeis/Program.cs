using DesafioSeis;

Console.WriteLine("Digite uma opção: ");
Retorna:
Console.WriteLine("1 - Encriptar");
Console.WriteLine("2 - Decriptar");
var opcao = Int32.TryParse(Console.ReadLine(), out int result) ? result : 0;
string valor = String.Empty;
string resultado = String.Empty;

switch (opcao)
{
    case 1:
        Console.WriteLine("Digite um valor para encriptar:");
        valor = Console.ReadLine();
        Console.WriteLine("Encriptando...");
        var crypt = new Cryptography();
        resultado = crypt.Encrypt(valor);
        Console.WriteLine("Valor: " + resultado);
        break;
    case 2:
        Console.WriteLine("Digite um valor para decriptar:");
        valor = Console.ReadLine();
        Console.WriteLine("Encriptando...");
        var decrypt = new Cryptography();
        resultado = decrypt.Encrypt(valor);
        Console.WriteLine("Valor: " + resultado);
        break;
    default:
        Console.WriteLine("Opção inválida!");
        Console.WriteLine("Digite novamente:");
        goto Retorna;
        break;
}