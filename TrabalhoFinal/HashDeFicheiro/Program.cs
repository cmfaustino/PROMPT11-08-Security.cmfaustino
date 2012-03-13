using System;
using System.Security.Cryptography;

namespace HashDeFicheiro
{
    class Program
    {
        /// <summary>
        /// Apresenta a sintaxe do programa, em estilo MS-DOS (linha de comandos do Windows).
        ///  Por exemplo, executar "help copy" na linha de comandos do Windows, para ver o "estilo".
        /// </summary>
        static void ApresentaSintaxe()
        {
        // http://stackoverflow.com/questions/616584/how-do-i-get-the-name-of-the-current-executable-in-c
            // http://msdn.microsoft.com/en-us/library/system.appdomain.friendlyname.aspx

            // 1 - obter nome deste programa executavel
            string nomeDoProgramaExecutavel;
            try
            {
                nomeDoProgramaExecutavel = AppDomain.CurrentDomain.FriendlyName.ToUpper();
            }
            catch (Exception)
            {
                nomeDoProgramaExecutavel = "HashDeFicheiro".ToUpper();
            }

            // 2 - apresentar sintaxe em estilo MS-DOS
            const string sintaxeNomeDeFicheiro = "nome_de_ficheiro";
            const string sintaxeMetodoAlgoritmoDeHashASerUtilizado =
                                                            "metodo_algoritmo_de_hash_a_ser_utilizado";
            Console.WriteLine("Calcula o hash de ficheiro(s), utilizando o algoritmo indicado.");
            Console.WriteLine();
            Console.WriteLine("=> Sintaxe:");
            Console.WriteLine();
            Console.WriteLine("{0} {1} {2}", nomeDoProgramaExecutavel,
                                            "[unidade:][caminho][" + sintaxeNomeDeFicheiro +"]",
                                            "[" + sintaxeMetodoAlgoritmoDeHashASerUtilizado + "]");
            Console.WriteLine();
            Console.WriteLine("===> {0} - Pode conter wildcards (* ?).", sintaxeNomeDeFicheiro);
            Console.WriteLine();
            Console.WriteLine("===> {0} - Pode ser qualquer um destes metodos/algoritmos:",
                                                            sintaxeMetodoAlgoritmoDeHashASerUtilizado);
            Console.WriteLine();
            HashComputacao.HashMetodoCalculo.Metodos.ForEach(m => Console.Write("  {0}  ;", m));
            Console.WriteLine(".");
            Console.WriteLine();
            Console.WriteLine("     Se nenhum for especificado, por defeito sera' {0} .",
                                                HashComputacao.HashMetodoCalculo.MetodoHashPreDefinido);
            Console.WriteLine();
        }

        /// <summary>
        /// Programa principal que calcula o hash de um ficheiro (ou ficheiros, se houver wildcards).
        /// </summary>
        /// <param name="args">Argumentos: PathNome de Ficheiro(s); Metodo de Computacao de Hash.</param>
        static void Main(string[] args)
        {
            string ficheiroCaminhoNome;
            string hashAlgoritmoNome;

            // 1 - se tem, no maximo, um argumento
            if (args.Length < 2) // <= 1
            {

                // 2 - se nao tem argumentos
                if (args.Length < 1) // <= 0
                {
                    // 3 - apresenta a sintaxe e pede o caminho_completo_nome do ficheiro
                    ApresentaSintaxe();
                    Console.WriteLine("Escreva CaminhoCompleto/NomeFicheiro (ficheiro pode ter * ?):");
                    Console.Write("    ");
                    ficheiroCaminhoNome = Console.ReadLine();
                }
                else
                {
                    // 4 - caso contrario, obtem-se o ficheiro pelo argumento
                    ficheiroCaminhoNome = args[0];
                }
                // 5 - obtem-se o algoritmo atraves da pre'-definicao
                hashAlgoritmoNome = HashComputacao.HashMetodoCalculo.MetodoHashPreDefinido;
            }
            else
            {
                // 6 - se tem mais de um argumento, obtem-se ficheiro e algoritmo atraves dos argumentos
                ficheiroCaminhoNome = args[0];
                hashAlgoritmoNome = args[1];
            }

            // 7 - validar se existe(m) ficheiro(s), se sim, obter caminho e padrao, senao, da' erro
            string ficheiroPastaNome;
            string ficheiroPadraoNome;
            if (!HashComputacao.GestaoSistemaFicheiros.ExistemFicheiros(ficheiroCaminhoNome,
                                                        out ficheiroPastaNome, out ficheiroPadraoNome))
            {
                Console.Error.WriteLine("ERRO: Localizacao de ficheiro(s) invalida!");
                Console.Error.WriteLine("Argumento: {0}", ficheiroCaminhoNome);
                Console.Error.WriteLine("Localizacao: {0}", ficheiroPastaNome);
                Console.Error.WriteLine("Padrao Nomes: {0}", ficheiroPadraoNome);
            }
            else
            {
                // 8 - validar se existe metodo/algoritmo de hash, se sim, obter instancia, senao, da' erro
                var hashInst = HashComputacao.HashMetodoCalculo.
                                                            ObterClasseMetodoValido(hashAlgoritmoNome);
                if (hashInst == null)
                {
                    Console.Error.WriteLine("ERRO: Metodo/algoritmo de hash invalido/nao encontrado!");
                    Console.Error.WriteLine("Algoritmo: {0}", hashAlgoritmoNome);
                }
                else
                {
                    // 9 - calcular hash de ficheiro(s), se sim, obter dicionario, senao, da' erro
                    var dictFicheirosHashs = HashComputacao.HashCalculoFicheiro.
                                        CalcularHash(ficheiroPastaNome, ficheiroPadraoNome, hashInst);
                    if ((dictFicheirosHashs == null) || (dictFicheirosHashs.Count < 1))
                    {
                        Console.Error.WriteLine(
                            "ERRO: Localizacao de ficheiro(s) invalida, ou ficheiros apagado(s)!");
                        Console.Error.WriteLine("Argumento: {0}", ficheiroCaminhoNome);
                        Console.Error.WriteLine("Localizacao: {0}", ficheiroPastaNome);
                        Console.Error.WriteLine("Padrao Nomes: {0}", ficheiroPadraoNome);
                    }
                    else
                    {
                        // 10 - mostrar hash no ecran, percorrendo o dicionario de pares (nome_fich,hash) *** *** ***
                        Console.WriteLine("RESULTADO DA EXECUCAO:");
                        Console.WriteLine();
                        Console.WriteLine("Argumento: {0}", ficheiroCaminhoNome);
                        Console.WriteLine("Localizacao: {0}", ficheiroPastaNome);
                        Console.WriteLine("Padrao Nomes: {0}", ficheiroPadraoNome);
                        Console.WriteLine("Algoritmo: {0}", hashAlgoritmoNome);
                        Console.WriteLine();

                        int indexFichsHashs = 0;
                        var contagemFichsHashs = dictFicheirosHashs.Count;
                        foreach (var fichHash in dictFicheirosHashs)
                        {
                            ++indexFichsHashs;
                            Console.WriteLine("{0} de {1}: Ficheiro = {2}", indexFichsHashs, contagemFichsHashs, fichHash.Key);
                            Console.WriteLine("{0} de {1}: Hash = {2}", indexFichsHashs, contagemFichsHashs, fichHash.Value);
                            Console.WriteLine();
                        }
                        
                        // *** FIM ***
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}
