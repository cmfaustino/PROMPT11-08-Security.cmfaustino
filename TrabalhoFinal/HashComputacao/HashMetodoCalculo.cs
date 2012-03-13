using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace HashComputacao
{
    /// <summary>
    /// Classe utilizada para validar e obter metodo (classe nao-abstracta) existente no namespace
    ///  definido em <see cref="Namespace"/>.
    ///  Neste momento, essa classe nao-abstracta sera' subclasse
    ///  de <see cref="System.Security.Cryptography.HashAlgorithm"/>.
    ///  Assim, deste modo, podem ser utilizados mais modos/classes de calculo de hash do que aqueles que
    ///  estao referidos em <see cref="HashAlgorithm.Create(String)"/>,
    ///  inclusive' aqueles que geram chaves aleatorias internas ("randomly generated key") para o hash.
    /// </summary>
    public static class HashMetodoCalculo
    {
        /// <summary>
        /// Namespace que serve de base aos metodos (classes) utilizados para calculo de hash.
        /// </summary>
        public static string Namespace = "System.Security.Cryptography";

        /// <summary>
        /// Metodo para calculo de hash que e' pre'-definido.
        /// </summary>
        public static string MetodoHashPreDefinido = "SHA1CryptoServiceProvider";

        /// <summary>
        /// Lista de metodos possiveis de serem utilizados para calculo de hash.
        /// Correspondem a nomes de classes nao-abstractas do namespace definido
        ///  em <see cref="Namespace"/>.
        /// </summary>
        public static List<string> Metodos =        // LEGENDA 1: _HA = HashAlgorithm.Create(String)
            new List<string>                        // LEGENDA 2: _SSCrypt = System.Security.Cryptography
                {                                   // LEGENDA 3: () = comentario no construtor sem param
                    "HMACMD5",                      // () "class with a randomly generated key."
                    "HMACRIPEMD160",                // () "class with a randomly generated 64-byte key."
                    "HMACSHA1",                     // () "class with a randomly generated key."
                    "HMACSHA256",                   // () "class with a randomly generated key."
                    "HMACSHA384",                   // () "class by using a randomly generated key."
                    "HMACSHA512",                   // () "class with a randomly generated key."
                    "MACTripleDES",                 // "uses  default implementation  TripleDES." (CSP?)
                    "MD5Cng",                       // 
                    "MD5CryptoServiceProvider",     // _HA: MD5 _SSCrypt.MD5
                    "RIPEMD160Managed",             // ==> MODO HASH *EXTRA*, COM ESTA IMPLEMENTACAO <==
                    "SHA1Cng",                      // 
                    "SHA1CryptoServiceProvider",    // _HA: SHA SHA1 _SSCrypt.SHA1 _SSCrypt.HashAlgorithm
                    "SHA1Managed",                  // 
                    "SHA256Cng",                    // 
                    "SHA256CryptoServiceProvider",  // 
                    "SHA256Managed",                // _HA: SHA256 SHA-256 _SSCrypt.SHA256 ; () "managed"
                    "SHA384Cng",                    // 
                    "SHA384CryptoServiceProvider",  // 
                    "SHA384Managed",                // _HA: SHA384 SHA-384 _SSCrypt.SHA384
                    "SHA512Cng",                    // 
                    "SHA512CryptoServiceProvider",  // 
                    "SHA512Managed"                 // _HA: SHA512 SHA-512 _SSCrypt.SHA512
                };

        /// <summary>
        /// Verifica se metodoAValidar e' valido, e se sim,
        ///  retorna valor correcto desse metodo que foi encontrado em <see cref="Metodos"/>.
        /// </summary>
        /// <param name="metodoAValidar">Metodo (nome de classe) a validar.</param>
        /// <param name="metodoValido">Metodo (nome de classe) encontrado,
        ///  ou entao, <code>default(string)</code>.</param>
        /// <returns>Indica se metodoAValidar e' valido (se foi encontrado na lista especificada
        ///  por <see cref="Metodos"/>).</returns>
        public static bool TryIsMetodoValido(string metodoAValidar, out string metodoValido)
        {
            // 1 - tentar encontrar metodo
            metodoValido = Metodos.Find(s => (s.ToUpper().Equals(metodoAValidar.ToUpper())));

            // 2 - retornar resultado
            return (!(metodoValido.Equals(default(string))));
        }

        /// <summary>
        /// Obtem instancia de classe (nao-abstracta) existente no namespace definido
        ///  em <see cref="Namespace"/>
        ///  correspondente 'a especificada pelo metodo metodoAValidar,
        ///  se este for valido (encontrado na lista especificada por <see cref="Metodos"/>).
        /// </summary>
        /// <param name="metodoAValidar">Metodo (nome de classe) a validar.</param>
        /// <returns>Instancia de classe (nao-abstracta) existente no namespace definido
        ///  em <see cref="Namespace"/>, ou entao, <code>null</code>.</returns>
        public static HashAlgorithm ObterClasseMetodoValido(string metodoAValidar)
        {
            string metodo;

            // 1 - tentar encontrar metodo
            if (TryIsMetodoValido(metodoAValidar, out metodo))
            {
                // 2 - tentar criar instancia de classe correspondente, ou retorna null
                var tipo = Type.GetType(Namespace + "." + metodo);
                return (tipo != null) ? (Activator.CreateInstance(tipo) as HashAlgorithm) : null;
            }

            // 3 - se nao se encontrou metodo, retorna null
            return null;
        }
    }
}
