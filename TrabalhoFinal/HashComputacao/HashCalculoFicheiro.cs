using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HashComputacao
{
    /// <summary>
    /// Classe utilizada para proceder ao calculo de hash de um ou mais ficheiros,
    ///  utilizando um algoritmo/metodo de hash.
    /// </summary>
    public static class HashCalculoFicheiro
    {
        /// <summary>
        /// Converte <see cref="System.Byte"/>[] para <see cref="System.String"/>,
        ///  utilizando <see cref="System.Text.StringBuilder"/>.
        /// </summary>
        /// <param name="array">Valor de <see cref="System.Byte"/>[] a converter
        ///  em <see cref="System.String"/>.</param>
        /// <returns>Valor de <see cref="System.String"/> que
        ///  representa <see cref="System.Byte"/>[].</returns>
        public static string ConverterByteArrayParaStringComStringBuilder(byte[] array)
        {
            // http://social.msdn.microsoft.com/Forums/en-US/Vsexpressvcs/thread/b8f7837b-e396-494e-88e1-30547fcf385f/
            // http://msdn.microsoft.com/en-us/library/9b4d9sx5.aspx
            // http://msdn.microsoft.com/en-us/library/xd12z8ts.aspx
            
            var sb = new StringBuilder();
            array.ToList().ForEach(b => sb.Append(b)); // sb.Append(Convert.ToString(b))
            return sb.ToString();
        }

        /// <summary>
        /// Converte <see cref="System.Byte"/>[] para <see cref="System.String"/>, utilizando
        /// <see cref="System.String.Join{T}(System.String,System.Collections.Generic.IEnumerable{T})"/>.
        /// </summary>
        /// <param name="array">Valor de <see cref="System.Byte"/>[] a converter
        ///  em <see cref="System.String"/>.</param>
        /// <returns>Valor de <see cref="System.String"/> que
        ///  representa <see cref="System.Byte"/>[].</returns>
        public static string ConverterByteArrayParaStringComStringJoin(byte[] array)
        {
            // http://aspdotnetcodebook.blogspot.com/2011/07/how-to-compute-hash-of-file-in-c.html
            // http://msdn.microsoft.com/en-us/library/dd992421.aspx
            
            return String.Join("", array);
        }

        /// <summary>
        /// Converte <see cref="System.Byte"/>[] para <see cref="System.String"/>,
        ///  utilizando <see cref="System.String.Format(System.String,System.Object)"/>
        ///  e <see cref="System.Text.StringBuilder"/>.
        /// </summary>
        /// <param name="array">Valor de <see cref="System.Byte"/>[] a converter
        ///  em <see cref="System.String"/>.</param>
        /// <returns>Valor de <see cref="System.String"/> que
        ///  representa <see cref="System.Byte"/>[].</returns>
        public static string ConverterByteArrayParaStringComStringFormat(byte[] array)
        {
            // http://sharpertutorials.com/calculate-md5-checksum-file/
            // http://msdn.microsoft.com/en-us/library/xa627k19.aspx
            
            var sb = new StringBuilder();
            array.ToList().ConvertAll(b => string.Format("{0:X2}", b)).ForEach(b => sb.Append(b));
            return sb.ToString();

            //for (i = 0; i < array.Length; i++)
            //{
            //    Console.Write(String.Format("{0:X2}", array[i]));
            //    if ((i % 4) == 3) Console.Write(" ");
            //}
            //Console.WriteLine();
        }

        /// <summary>
        /// Converte <see cref="System.Byte"/>[] para <see cref="System.String"/>,
        ///  utilizando <see cref="System.BitConverter"/> e removendo os tracos entre cada hexadecimal.
        /// </summary>
        /// <param name="array">Valor de <see cref="System.Byte"/>[] a converter
        ///  em <see cref="System.String"/>.</param>
        /// <returns>Valor de <see cref="System.String"/> que
        ///  representa <see cref="System.Byte"/>[].</returns>
        public static string ConverterByteArrayParaStringComBitConverter(byte[] array)
        {
            // http://msdn.microsoft.com/en-us/library/3a733s97.aspx
            
            return BitConverter.ToString(array).Replace("-", "");
        }

        /// <summary>
        /// Converte <see cref="System.Byte"/>[] para <see cref="System.String"/>.
        ///  Este e' o metodo utilizado (por defeito), que e' igual a
        /// <see cref="HashComputacao.HashCalculoFicheiro.ConverterByteArrayParaStringComBitConverter"/>.
        /// </summary>
        /// <param name="array">Valor de <see cref="System.Byte"/>[] a converter
        ///  em <see cref="System.String"/>.</param>
        /// <returns>Valor de <see cref="System.String"/> que
        ///  representa <see cref="System.Byte"/>[].</returns>
        public static string ConverterByteArrayParaString(byte[] array)
        {
            // escolheu-se o metodo que parece ser o mais facil para se perceber o resultado
            return ConverterByteArrayParaStringComBitConverter(array);
        }

        /// <summary>
        /// Calcula hash de apenas um ficheiro, utilizando um determinado algoritmo.
        /// </summary>
        /// <param name="nomeFicheiro">Caminho e nome do ficheiro para o qual sera' calculado
        ///  o hash.</param>
        /// <param name="computacaoHash">Classe do algoritmo a ser utilizado para o calculo do hash.
        ///  Pode ser uma subclasse nao-abstracta
        ///  de <see cref="System.Security.Cryptography.HashAlgorithm"/>.</param>
        /// <returns>Hash do ficheiro, ou entao, <code>string.Empty</code>.</returns>
        private static string CalcularHashFicheiroUnico(string nomeFicheiro,
                                                        HashAlgorithm computacaoHash)
        {
            // 1 - tentar abrir stream de ficheiro
            FileStream fileStream;
            try
            {
                fileStream = File.OpenRead(nomeFicheiro);
            }
            catch (Exception)
            {
                return string.Empty;
            }

            /* 2 - calcular hash de forma simples:
             *  forma mais elaborada implicaria utilizar, por exemplo, CryptoStream, com algo do genero
             * var cryptoStream = new CryptoStream(fileStream, computacaoHash, CryptoStreamMode.Read); */
            byte[] hashval;
            try
            {
                hashval = computacaoHash.ComputeHash(fileStream);
            }
            catch (Exception)
            {
                hashval = new byte[0];
            }
            finally
            {
                fileStream.Close();
            }

            // 3 - transformar hash em string
            var str = ConverterByteArrayParaString(hashval);

            // 4 - retornar string de hash
            return str;
        }

        /// <summary>
        /// Calcula hash de um ou mais ficheiros, utilizando um determinado algoritmo,
        ///  e retornando os pares ficheiro-hash.
        /// </summary>
        /// <param name="caminhoPasta">Directoria de um ou mais ficheiros.</param>
        /// <param name="padraoNomesFicheiros">Nome(s) do(s) ficheiro(s). Pode conter wildcards.</param>
        /// <param name="computacaoHash">Classe do algoritmo a ser utilizado para o calculo do hash.
        ///  Pode ser uma subclasse nao-abstracta
        ///  de <see cref="System.Security.Cryptography.HashAlgorithm"/>.</param>
        /// <returns><see cref="System.Collections.Generic.Dictionary{TKey,TValue}"/> que contem
        ///  os pares (nome_ficheiro,hash_ficheiro), ou entao, <code>null</code>.</returns>
        public static Dictionary<string,string> CalcularHash(string caminhoPasta,
                                            string padraoNomesFicheiros, HashAlgorithm computacaoHash)
        {
            // 0 - trim
            var caminhoPastaTrimed = caminhoPasta.Trim();
            var padraoNomesFicheirosTrimed = padraoNomesFicheiros.Trim();

            /* 1 - tratar null e/ou empty: se for este o caso,
             *  a) assume-se a directoria corrente de trabalho ; b) assumem-se todos os ficheiros */
            var pathPasta = string.IsNullOrEmpty(caminhoPastaTrimed) ? Directory.GetCurrentDirectory()
                                                                    : caminhoPastaTrimed;
            var patternFilenames = string.IsNullOrEmpty(padraoNomesFicheirosTrimed) ? "*"
                                                                    : padraoNomesFicheirosTrimed;

            // 2 - verificar se existem ficheiros, testando o contrario, depois do teste de excepcoes
            IEnumerable<string> nomesFicheiros;
            try
            {
                // .ToList() e' para evitar "Possible multiple enumeration of IEnumerable" de ReSharper
                nomesFicheiros = Directory.EnumerateFiles(pathPasta, patternFilenames,
                                                            SearchOption.TopDirectoryOnly).ToList();
                if (!nomesFicheiros.Any())
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }

            /* 3 - calcular hash, para cada ficheiro existente na directoria, que corresponda ao padrao
             *  nomesFicheiros -> Dictionary: construcao com nomeFicheiro e CalcularHashFicheiroUnico */
            return nomesFicheiros.ToDictionary(nomeFicheiro => nomeFicheiro,
                nomeFicheiro => CalcularHashFicheiroUnico(nomeFicheiro, computacaoHash));
        }
    }
}
