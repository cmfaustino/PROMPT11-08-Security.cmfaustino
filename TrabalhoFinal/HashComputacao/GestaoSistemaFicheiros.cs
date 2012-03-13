using System;
using System.IO;
using System.Linq;

namespace HashComputacao
{
    /// <summary>
    /// Classe cujo objectivo e' gerir o sistema de ficheiros,
    ///  por meio de operacoes relativas a ficheiros e directorias.
    ///  Neste momento, so' existe um metodo.
    /// </summary>
    public static class GestaoSistemaFicheiros
    {
        /// <summary>
        /// Verifica se existe(m) ficheiro(s) especificado(s) por
        ///  <paramref name="caminhoPastaComFicheiros"/>,
        ///  e se sim, retorna directoria e padrao do nome desse(s) ficheiro(s),
        ///  em <paramref name="caminhoPasta"/> e <paramref name="nomesFicheiros"/>,
        ///  e ainda um <code>bool</code> indicando o resultado.
        /// </summary>
        /// <param name="caminhoPastaComFicheiros">Especificacao (path e nome) de ficheiro(s).
        ///  Wildcards sao permitidos.</param>
        /// <param name="caminhoPasta">Directoria do(s) ficheiro(s)
        ///  (que pode ser a actual, a directoria corrente de trabalho),
        ///  ou entao, <code>string.Empty</code>.</param>
        /// <param name="nomesFicheiros">Padrao do nome do(s) ficheiro(s),
        ///  ou entao, <code>string.Empty</code>.</param>
        /// <returns>Indica se existem ficheiros especificado(s) por
        ///  <paramref name="caminhoPastaComFicheiros"/>.</returns>
        public static bool ExistemFicheiros(string caminhoPastaComFicheiros,
                                            out string caminhoPasta, out string nomesFicheiros)
        {
            // 0 - trim
            var caminhoPastaComFicheirosTrimed = caminhoPastaComFicheiros.Trim();
            
            // 1 - verificar caso de apenas whitespaces
            if (string.IsNullOrEmpty(caminhoPastaComFicheirosTrimed))
            {
                caminhoPasta = string.Empty; // ""
                nomesFicheiros = string.Empty; // ""
                return false;
            }
            
            /* 2 - verificar caso de excepcao na separacao entre directoria e ficheiro(s):
             *  caracteres invalidos ou grande demais */
            try
            {
                caminhoPasta = Path.GetDirectoryName(caminhoPastaComFicheirosTrimed);
                nomesFicheiros = Path.GetFileName(caminhoPastaComFicheirosTrimed);
            }
            catch (Exception) // ArgumentException ou PathTooLongException ; ArgumentException
            {
                caminhoPasta = string.Empty; // ""
                nomesFicheiros = string.Empty; // ""
                return false;
            }
            
            /* 3 - verificar caso de informacao de directoria vazia,
             *  se for esse o caso, entao, assume-se a directoria corrente de trabalho:
             *  "Directory information for path, or null if path denotes a root directory or is null.
             *  Returns String.Empty if path does not contain directory information." */
            if (string.IsNullOrEmpty(caminhoPasta))
            {
                caminhoPasta = Directory.GetCurrentDirectory();
            }
            
            /* 4 - verificar caso de informacao de ficheiro vazia:
             *  "The characters after the last directory character in path. If the last character of path
             *  is a directory or volume separator character, this method returns String.Empty .
             *  If path is null, this method returns null." */
            if (string.IsNullOrEmpty(nomesFicheiros))
            {
                nomesFicheiros = string.Empty; // ""
                return false;
            }
            
            // 5 - verificar se existem mesmo ficheiros, testando o contrario
            try
            {
                if (!Directory.EnumerateFiles(caminhoPasta, nomesFicheiros,
                                                SearchOption.TopDirectoryOnly).Any())
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            // 6 - caso contrario, significa que esta' tudo bem, e que existem ficheiros
            return true;
        }
    }
}
