using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.IdentityModel.Claims;
using AuthorizationContext = System.Web.Mvc.AuthorizationContext;

namespace Ex2AppWebFbiMostWanted.Models
{
    // HACK: criacao de classe herdada (e override de metodo) para o novo atributo RequireClaims

    /// <summary>
    /// Atributo RequireClaims, que NAO e' multiplo, e cada especificacao tem 6 parametros string[]
    ///  opcionais: RolesAllowed , NamesAllowed , EmailAddressesAllowed , EmailDomainsAllowed ,
    ///  IssuersAllowed , NameIdentifiersAllowed .
    /// Cada parametro (array) pode ter varios valores, e assim, varias hipoteses (operacao booleana OR
    ///  em relacao aos valores EM cada parametro).
    /// Em caso de especificacao com varios parametros (arrays) dos 6, todos os parametros serao
    ///  obrigatorios (operacao booleana AND em relacao aos parametros).
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)] // true tem problemas nao resolvidos
    //[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class RequireClaimsAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Especificacao de roles necessarios para aceder ao recurso
        ///  (varios roles serao tidos em conta como hipoteses OR).
        /// </summary>
        public string[] RolesAllowed { get; set; }

        /// <summary>
        /// Especificacao de nomes de utilizadores necessarios para aceder ao recurso
        ///  (varios nomes de utilizadores serao tidos em conta como hipoteses OR).
        /// </summary>
        public string[] NamesAllowed { get; set; }

        /// <summary>
        /// Especificacao de e-mails necessarios para aceder ao recurso
        ///  (varios e-mails serao tidos em conta como hipoteses OR).
        /// </summary>
        public string[] EmailAddressesAllowed { get; set; }

        /// <summary>
        /// Especificacao de dominios de e-mail necessarios para aceder ao recurso
        ///  (varios dominios de e-mail serao tidos em conta como hipoteses OR).
        /// </summary>
        public string[] EmailDomainsAllowed { get; set; }

        /// <summary>
        /// Especificacao de issuers necessarios para aceder ao recurso
        ///  (varios issuers serao tidos em conta como hipoteses OR).
        /// </summary>
        public string[] IssuersAllowed { get; set; }

        /// <summary>
        /// Especificacao de name identifiers necessarios para aceder ao recurso
        ///  (varios name identifiers serao tidos em conta como hipoteses OR).
        /// </summary>
        public string[] NameIdentifiersAllowed { get; set; }

        /// <summary>
        /// Metodo auxiliar que inicializa um Enumerado (que depois sera' alvo de outras operacoes).
        /// </summary>
        /// <param name="enumerable">Enumerado a ser utilizado para a inicializacao.</param>
        /// <returns>Enumerado, que sera' o enumerable se estiver inicializado, ou novo objecto.</returns>
        private static ICollection<string> CreateNotNullAndToLower(IEnumerable<string> enumerable)
        {
            return (enumerable ?? new List<string>().ToArray()).Select(s => s.ToLower()).ToList();
        }

        /// <summary>
        /// Metodo auxiliar que verifica se enumContem e' vazia ou contem algum elemento de enumContido.
        /// </summary>
        /// <typeparam name="T">Tipo de cada elemento, em enumContem e em enumContido.</typeparam>
        /// <param name="enumContem">Coleccao de elementos (especificacao de regras).</param>
        /// <param name="enumContido">Enumerado de elementos (validacao de ocorrencias).</param>
        /// <returns>Booleano: enumContem e' vazia ou contem elemento(s) de enumContido.</returns>
        private static bool VazioOuContemAlgumDe<T>(ICollection<T> enumContem, IEnumerable<T> enumContido)
        {
            return (!enumContem.Any()) || ((enumContem.Any()) && (enumContido.Any(enumContem.Contains)));
        }

        /// <summary>
        /// Metodo auxiliar que filtra valores de claims de acordo com o tipo de claims indicado.
        /// </summary>
        /// <param name="enumClaims">Enumerado de claims a ser filtrado.</param>
        /// <param name="claimType">Tipo de claims que serve para filtrar os valores de claims.</param>
        /// <returns>Enumerado de valores de claims (strings) do tipo indicado.</returns>
        private static IEnumerable<string> SelectClaimsByType(IEnumerable<Claim> enumClaims,
                                                                                        string claimType)
        {
            return enumClaims.Where(c => c.ClaimType == claimType).Select(c => c.Value.ToLower());
        }

        /// <summary>
        /// Metodo a ser utilizado para validar o processo de autorizacao a recursos protegidos.
        /// </summary>
        /// <param name="filterContext">Contexto da operacao que necessita de autorizacao.</param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // HACK: adaptacao do que esta' em
// http://stackoverflow.com/questions/5117782/how-to-extend-authorizeattribute-and-check-the-users-roles
// http://stackoverflow.com/questions/2140601/mvc-authorize-attribute-httpunauthorizedresult-formsauthentication
            // http://aspnet.codeplex.com/SourceControl/changeset/view/22929#266447
            // http://forums.asp.net/post/3279878.aspx

            // variavel para backup do resultado inicial
            var filterContextResultInicial = filterContext.Result;
            
            // invocacao do processo de autorizacao base
            base.OnAuthorization(filterContext);

            // de seguida, existe adaptacao do codigo do passo 7 do exercicio 1 da sessao 2 do modulo

            //var controller = filterContext.Controller as Controller;
            //if (controller != null)
            //{
            //}
            
            // 1 - obter user com claims
            var claimUser = filterContext.HttpContext.User as IClaimsPrincipal; // ident = this.User

            //var emailClaim = ident.Identities[0].Claims
            //                        .Where(c => c.ClaimType == ClaimTypes.Email).FirstOrDefault();
            //return "Hi there " + (emailClaim != null ? emailClaim.Value : "stranger");

            if (claimUser != null)
            {
                // 2 - inicializar regras especificadas no atributo
                var rolesAllowed = CreateNotNullAndToLower(RolesAllowed);
                var namesAllowed = CreateNotNullAndToLower(NamesAllowed);
                var emailAddressesAllowed = CreateNotNullAndToLower(EmailAddressesAllowed);
                var emailDomainsAllowed = CreateNotNullAndToLower(EmailDomainsAllowed);
                var issuersAllowed = CreateNotNullAndToLower(IssuersAllowed);
                var nameIdentifiersAllowed = CreateNotNullAndToLower(NameIdentifiersAllowed);

                // 3 - obter claims do user
                var claimUserClaims = claimUser.Identities.SelectMany(id => id.Claims).ToList();

                // 4 - obter valores de claims, com base no tipo dos mesmos
                var claimUserRoles = SelectClaimsByType(claimUserClaims, ClaimTypes.Role);
                var claimUserNames = SelectClaimsByType(claimUserClaims, ClaimTypes.Name);
                var claimUserEmailAddresses = SelectClaimsByType(claimUserClaims, ClaimTypes.Email)
                                                                                            .ToList();
                var claimUserEmailDomains = claimUserEmailAddresses.Select(cv => cv.Split('@')[1]);
                var claimUserIssuers = claimUserClaims.Select(c => c.Issuer.ToLower());
                var claimUserNameIdentifiers = SelectClaimsByType(claimUserClaims,
                                                                            ClaimTypes.NameIdentifier);

                // 5 - verificar se, para cada regra, a validacao de claims e' bem sucedida
                var isClaimUserRoleOk = VazioOuContemAlgumDe(rolesAllowed, claimUserRoles);
                var isClaimUserNameOk = VazioOuContemAlgumDe(namesAllowed, claimUserNames);
                var isClaimUserEmailAddressOk = VazioOuContemAlgumDe(
                                                        emailAddressesAllowed, claimUserEmailAddresses);
                var isClaimUserEmailDomainOk = VazioOuContemAlgumDe(
                                                            emailDomainsAllowed, claimUserEmailDomains);
                var isClaimUserIssuerOk = VazioOuContemAlgumDe(issuersAllowed, claimUserIssuers);
                var isClaimUserNameIdentifierOk = VazioOuContemAlgumDe(
                                                    nameIdentifiersAllowed, claimUserNameIdentifiers);

                // 6 - validacao final: SE todas as validacoes parciais foram bem sucedidas...
                if (isClaimUserRoleOk && isClaimUserNameOk && isClaimUserEmailAddressOk &&
                    isClaimUserEmailDomainOk && isClaimUserIssuerOk && isClaimUserNameIdentifierOk)
                {
                    // 7 - ...entao o resultado da autorizacao sera' positivo
                    if (filterContext.Result is HttpUnauthorizedResult)
                    {
                        //filterContext.Result = filterContextResultInicial;
                    }
                    return;
                }
            }

            // 8 - ...caso contrario, o resultado da autorizacao sera' negativo
            //if (!(filterContext.Result is HttpUnauthorizedResult))
            //{
                filterContext.Result = new HttpUnauthorizedResult();
            //}
        }

        // HACK: a redefinicao deste metodo e' desnecessaria
        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    ;
        //}
    }
}