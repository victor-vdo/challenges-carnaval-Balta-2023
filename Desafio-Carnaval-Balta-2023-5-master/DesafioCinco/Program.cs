using DesafioCinco;

Console.WriteLine("Informe um valor:");
var valor = Double.TryParse(Console.ReadLine(), out double result) ? result : Double.Epsilon;
if (valor > Int32.MaxValue)
{
    Console.WriteLine("Número grande demais! Não é possível obter os valor por extenso.");
    Environment.Exit(1);
}
var tuple = SeparaCasasDecimais(valor);
var valorInteiro = (int)valor;
ExtendeValorInteiro(valorInteiro);

#region Métodos

(int valorInteiro, double valorFracionado) SeparaCasasDecimais(double valor)
{
    int valorInteiro = (int)valor;
    double valorFracionado = valor - valorInteiro;
    return (valorInteiro, valorFracionado);
}

void ExtendeValorInteiro(int valorInteiro)
{
    var dicDecimais = new Dictionary<int, string>()
    {
        { 0, "zero"},{ 1, "um"},{ 2, "dois"},{ 3, "três"},{ 4, "quatro"},{ 5, "cinco"},{ 6, "seis"},{ 7, "sete"},{ 8, "oito"},{ 9, "nove"}
    };

    var dicPrimeiraDezena = new Dictionary<int, string>()
    {
        { 11, "onze"}, { 12, "doze"},{ 13, "treze"},{ 14, "quatorze"},{ 15, "quinze"},{ 16, "dezesseis"},{ 17, "dezessete"},{ 18, "dezoito"},{ 19, "dezenove"}
    };

    var dicDezenas = new Dictionary<int, string>()
    {
        { 10, "dez"}, { 20, "vinte"},{ 30, "trinta"},{ 40, "quarenta"},{ 50, "cinquenta"},{ 60, "sessenta"},{ 70, "setenta"},{ 80, "oitenta"},{ 90, "noventa"}
    };

    var dicCentenas = new Dictionary<int, string>()
    {
        { 100, "cem"},{ 300, "trezentos"},{ 400, "quatrocentos"},{ 500, "quinhentos"},{ 600, "seiscentos"},{ 700, "setecentos"},{ 800, "oitocentos"}, { 900, "novecentos" }
    };

    var dicInfinitos = new Dictionary<double, string>()
    {
        { 1000, "mil"}, {1000000, "milhão"},  {1000000000, "bilhão"}, {1000000000000, "trilhão"}, {1000000000000000, "quadrilhão"},
        {1000000000000000000 , "quintilhão" }

    };

    var tamanho = valorInteiro.ToString().Length;
    int tamanhoPrimeiraCentena = SeparaMilharesTamanho(tamanho);
    string valor = String.Empty;
    if(valorInteiro.ToString().Length > 3)
    {
        valor = RetornaValorPrimeiraCentena(tamanhoPrimeiraCentena, dicDecimais, dicDezenas, dicCentenas);
        valor = RetornaValorTipoDezena(tamanho, valor, dicDecimais, dicDezenas, dicCentenas, dicInfinitos);
    }

    var tamanhoResto = tamanho - tamanhoPrimeiraCentena;
    var primeiraCentena = RetornaPrimeiraCentena(tamanhoPrimeiraCentena);
    
    var centenasRestantes = RetornaCentenasRestantes(tamanhoPrimeiraCentena, tamanhoResto, valorInteiro);
    int centenasRestantesTamanho = centenasRestantes.Length;
    var tamanhoRestante = 0;
    do
    {
        if (centenasRestantesTamanho > 3)
        {
            tamanhoRestante = SeparaMilharesTamanho(centenasRestantesTamanho);
            valor += RetornaValorCentena(tamanhoRestante, centenasRestantes, dicDecimais, dicDezenas, dicCentenas, dicPrimeiraDezena);
            if (valor.Contains("cem e "))
                valor = valor.Replace("cem e ", "cento e ");
            valor = RetornaValorTipoDezena(centenasRestantesTamanho, valor, dicDecimais, dicDezenas, dicCentenas, dicInfinitos);
            centenasRestantesTamanho -= 3;
        }  
    } while (centenasRestantesTamanho > 3);

    if(centenasRestantesTamanho <=3)
    {
        var valorUltimaCentena = RetornaValorUltimaCentena(valorInteiro).Replace("\0", "");
        tamanhoRestante = valorUltimaCentena.Length;
        valor += RetornaValorCentena(tamanhoRestante, valorUltimaCentena, dicDecimais, dicDezenas, dicCentenas, dicPrimeiraDezena);
    }

    Console.WriteLine("O valor é: " + valor);
}

string RetornaValorUltimaCentena(int valorInteiro)
{
    var tamanhoInteiro = valorInteiro.ToString().Length;
    var valor = String.Empty;
    var ultimoDigito        = valorInteiro.ToString().Length > 0 ? valorInteiro.ToString()[tamanhoInteiro - 1] : Char.MinValue;
    var penultimoDigito     = valorInteiro.ToString().Length > 1 ? valorInteiro.ToString()[tamanhoInteiro - 2] : Char.MinValue;
    var antePenultimoDigito = valorInteiro.ToString().Length > 2 ? valorInteiro.ToString()[tamanhoInteiro - 3] : Char.MinValue;

    valor = antePenultimoDigito.ToString() + penultimoDigito.ToString() + ultimoDigito.ToString();
    return valor;
}

string RetornaValorCentena(int centena,string valorCentena,  Dictionary<int, string> dicDecimais, Dictionary<int, string> dicDezenas, Dictionary<int, string> dicCentenas, Dictionary<int, string> dicPrimeiraDezena)
{
    var primeirosDigitos = String.Empty;
    var primeiraDezena = valorCentena.Length > 0 ? valorCentena[0].ToString() : String.Empty;
    var segundaDezena  = valorCentena.Length > 1 ? valorCentena[1].ToString() : String.Empty;
    var terceiraDezena = valorCentena.Length > 2 ? valorCentena[2].ToString() : String.Empty;
    string valor = "";
    switch (centena)
    {
        case 0:
            break;

        case 1:
            primeirosDigitos = primeiraDezena;
            valor = dicDecimais[Int32.TryParse(primeiraDezena, out int unitario) ? unitario : Int32.MinValue];
            break;

        case 2:
            primeirosDigitos = primeiraDezena
                + segundaDezena;
            var dezenaDois = dicDezenas.Where(x => x.Key.ToString().StartsWith(primeiraDezena)).FirstOrDefault().Value;
            var decimalDois = dicDecimais.Where(x => x.Key.ToString().StartsWith(segundaDezena)).FirstOrDefault().Value;
            if (decimalDois.Contains("zero"))
                return dezenaDois.ToString();
            
            valor = dezenaDois?.ToString()
                + " e "
                + decimalDois;

            if (segundaDezena.StartsWith("1") && terceiraDezena != "0")
                valor = dicPrimeiraDezena
                    .Where(c => c.Key.ToString()
                    .StartsWith(primeiraDezena + segundaDezena))
                    .FirstOrDefault().Value;

            break;

        case 3:
            primeirosDigitos = primeiraDezena
                 + segundaDezena + terceiraDezena;

            var centenaTres = dicCentenas.Where(x => x.Key.ToString().StartsWith(primeiraDezena)).FirstOrDefault().Value;
            var dezenaTres = dicDezenas.Where(x => x.Key.ToString().StartsWith(segundaDezena)).FirstOrDefault().Value;
            var decimalTres = dicDecimais.Where(x => x.Key.ToString().StartsWith(terceiraDezena)).FirstOrDefault().Value;
            if (decimalTres.Contains("zero"))
                decimalTres = String.Empty;

            valor = centenaTres?.ToString()
                + " e "
                + dezenaTres
                + " e "
                + decimalTres;

            if (segundaDezena.StartsWith("1") && terceiraDezena != "0")
                valor = 
                    dicCentenas.Where(x => x.Key.ToString().StartsWith(primeiraDezena)).FirstOrDefault().Value +
                    " e " +
                    dicPrimeiraDezena
                    .Where(c => c.Key.ToString()
                    .StartsWith(segundaDezena + terceiraDezena))
                    .FirstOrDefault().Value;
            if (valor.Contains("cem e"))
                valor = valor.Replace("cem e","cento e");
            break;

        default:
            break;
    }
    return valor;
}

string RetornaCentenasRestantes(int tamanhoPrimeiraCentena, int tamanhoResto, int valorInteiro)
{
    var primeiraCentena = RetornaPrimeiraCentena(tamanhoPrimeiraCentena, false);
    var textoValorInteiro = valorInteiro.ToString();
    var valor = "";
    for (int i = tamanhoPrimeiraCentena; i < textoValorInteiro.Length; i++)
    {
        valor += textoValorInteiro[i].ToString();
    }

    return valor;
}

string RetornaPrimeiraCentena(int tamanhoPrimeiraCentena, bool formatada = false)
{
    var valor = String.Empty;
    var primeiraDezena = valorInteiro.ToString().Length > 0 ? valorInteiro.ToString()[0].ToString() : String.Empty;
    var segundaDezena  = valorInteiro.ToString().Length > 1 ? valorInteiro.ToString()[1].ToString() : String.Empty;
    var terceiraDezena = valorInteiro.ToString().Length > 2 ? valorInteiro.ToString()[2].ToString() : String.Empty;
    if (formatada)
    {
        switch (tamanhoPrimeiraCentena)
        {
            case 0:
                valor = "000";
                break;
            case 1:
                valor = $"00{primeiraDezena}";
                break;
            case 2:
                valor = $"0{primeiraDezena}{segundaDezena}";
                break;
            case 3:
                valor = $"0{primeiraDezena}{segundaDezena}{terceiraDezena}";
                break;
            default:
                valor = "000";
                break;
        }
        return valor; 
    }
    else
    {
        switch (tamanhoPrimeiraCentena)
        {
            case 0:
                valor = "0";
                break;
            case 1:
                valor = $"{primeiraDezena}";
                break;
            case 2:
                valor = $"{primeiraDezena}{segundaDezena}";
                break;
            case 3:
                valor = $"{primeiraDezena}{segundaDezena}{terceiraDezena}";
                break;
            default:
                valor = "0";
                break;
        }
        return valor;
    }

}

int SeparaMilharesTamanho(int tamanho)
{
    do
    {
        if (tamanho > 3)
            tamanho -= 3;
    } while (tamanho > 3);
    return tamanho; 
}

string RetornaTipoDezena(int tamanho)
{
    string tipoDezena = String.Empty;
    if (tipoDezena == String.Empty)
        tipoDezena = tamanho == (int)ETamanho.Decimais ? "isDecimal" : String.Empty;
    if (tipoDezena == String.Empty)
        tipoDezena = tamanho == (int)ETamanho.Dezenas ? "isDezena" : String.Empty;
    if (tipoDezena == String.Empty)
        tipoDezena = tamanho == (int)ETamanho.Centenas ? "isCentena" : String.Empty;
    if (tipoDezena == String.Empty)
        tipoDezena = tamanho >= (int)ETamanho.Milhares && tamanho < (int)ETamanho.Milhoes ? "isMilhar" : String.Empty;
    if (tipoDezena == String.Empty)
        tipoDezena = tamanho >= (int)ETamanho.Milhoes && tamanho < (int)ETamanho.Bilhoes ? "isMilhoes" : String.Empty;
    if (tipoDezena == String.Empty)
        tipoDezena = tamanho >= (int)ETamanho.Bilhoes && tamanho < (int)ETamanho.Trilhoes ? "isBilhoes" : String.Empty;
    if (tipoDezena == String.Empty)
        tipoDezena = tamanho >= (int)ETamanho.Trilhoes && tamanho < (int)ETamanho.Quadrilhoes ? "isTrilhoes" : String.Empty;
    if (tipoDezena == String.Empty)
        tipoDezena = tamanho >= (int)ETamanho.Quadrilhoes && tamanho < (int)ETamanho.Quintilhoes ? "isQuadrilhoes" : String.Empty;
    if (tipoDezena == String.Empty)
        tipoDezena = tamanho >= (int)ETamanho.Quintilhoes ? "isQuintilhoes" : String.Empty;
    return tipoDezena;
}

string RetornaValorPrimeiraCentena(int centena, Dictionary<int, string> dicDecimais, Dictionary<int, string> dicDezenas, Dictionary<int, string> dicCentenas)
{
    var primeiraDezena = valorInteiro.ToString().Length > 0 ? valorInteiro.ToString()[0].ToString() : String.Empty;
    var segundaDezena  = valorInteiro.ToString().Length > 1 ? valorInteiro.ToString()[1].ToString() : String.Empty;
    var terceiraDezena = valorInteiro.ToString().Length > 2 ? valorInteiro.ToString()[2].ToString() : String.Empty;
    string valor = "";
    switch (centena)
    {
        case 0:
            break;

        case 1:
            valor = dicDecimais[Int32.TryParse(primeiraDezena, out int unitario) ? unitario : Int32.MinValue];
            break;

        case 2:
            var dezenaDois = dicDezenas.Where(x=> x.Key.ToString().StartsWith(primeiraDezena)).FirstOrDefault().Value;
            var decimalDois = dicDecimais.Where(x => x.Key.ToString().StartsWith(segundaDezena)).FirstOrDefault().Value;
           if(decimalDois.Contains("zero"))
                return dezenaDois.ToString();

            valor = dezenaDois?.ToString()
                + " e "
                + decimalDois;

            if(valorInteiro.ToString().Length == 2)
                valor = String.Empty;
            break;

        case 3:
            var centenaTres = dicCentenas.Where(x => x.Key.ToString().StartsWith(primeiraDezena)).FirstOrDefault().Value;
            var dezenaTres = dicDezenas.Where(x => x.Key.ToString().StartsWith(segundaDezena)).FirstOrDefault().Value;
            var decimalTres = dicDecimais.Where(x => x.Key.ToString().StartsWith(terceiraDezena)).FirstOrDefault().Value;

            valor = centenaTres?.ToString()
                + " e "
                + dezenaTres
                + " e "
                + decimalTres;

            if (valorInteiro.ToString().Length == 3)
                valor = String.Empty;
            break;

        default:
            break;
    }
    return valor;
}

string RetornaValorTipoDezena(int tamanho, string valor, Dictionary<int, string> dicDecimais, Dictionary<int, string> dicDezenas, Dictionary<int, string> dicCentenas, Dictionary<double, string> dicInfinitos)
{
    string tipoDezena = RetornaTipoDezena(tamanho);
    switch (tipoDezena)
    {
        case "isDecimal":
            //var valorDecimal = dicDecimais.Where(c => c.Key.ToString().Length <= tamanho).FirstOrDefault().Value;
            //valor += " " + valorDecimal;
            break;
        case "isDezena":
            //var valorDezena = dicDezenas.Where(c => c.Key.ToString().Length <= tamanho).FirstOrDefault().Value;
            //valor += " " + valorDezena;
            break;
        case "isCentena":
            var valorCentena = dicCentenas.Where(c => c.Key.ToString().Length >= tamanho).FirstOrDefault().Value;
            valor += " " + valorCentena + " ";
            break;
        case "isMilhar":
            var valorMilhar = dicInfinitos.Where(x => x.Key.ToString().Length <= tamanho).FirstOrDefault().Value;
            valor += " " + valorMilhar + " ";
            break;
        case "isMilhoes":
            var valorMilhoes = dicInfinitos.Where(x => x.Key.ToString().Length >= tamanho).FirstOrDefault().Value;
            var primeiroDigito = Int32.TryParse(valorInteiro.ToString()[0].ToString(), out int result) ? result :0 ;
            if (primeiroDigito > 1)
                valorMilhoes = "milhões ";
            valor += " " + valorMilhoes + " ";
            break;
        case "isBilhoes":
            var valorBilhoes = dicInfinitos.Where(x => x.Key.ToString().Length >= tamanho).FirstOrDefault().Value;
            if (valorInteiro.ToString()[0] > 1)
                valorBilhoes = "bilhões ";
            valor += " " + valorBilhoes + " ";
            break;
        case "isTrilhoes":
            var valorTrilhoes = dicInfinitos.Where(x => x.Key.ToString().Length >= tamanho).FirstOrDefault().Value;
            if (valorInteiro.ToString()[0] > 1)
                valorTrilhoes = "trilhões ";
            valor += " " + valorTrilhoes + " ";
            break;
        case "isQuadrilhoes":
            var valorQuadrilhoes = dicInfinitos.Where(x => x.Key.ToString().Length >= tamanho).FirstOrDefault().Value;
            if (valorInteiro.ToString()[0] > 1)
                valorQuadrilhoes = "quadrilhões ";
            valor += " " + valorQuadrilhoes + " ";
            break;
        case "isQuintilhoes":
            var valorQuintilhoes = dicInfinitos.Where(x => x.Key.ToString().Length >= tamanho && x.Key.ToString().Length < Double.MaxValue).FirstOrDefault().Value;
            if (valorInteiro.ToString()[0] > 1)
                valorQuintilhoes = "quintilhões ";
            valor += " " + valorQuintilhoes + " ";
            break;
        default:
            valor = "Número grande demais! Não foi possível verifica-lo.";
            break;
    }
    return valor; 
}
#endregion