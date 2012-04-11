## Changelog (por ordem cronologica normal/nao inversa):

Antes de se ler este ficheiro, convem ler as notas iniciais contidas no ficheiro [readme.md](https://github.com/cmfaustino/PROMPT11-08-Security.cmfaustino/blob/master/readme.md).

### 22-03-2012 (fora do prazo):

Para o **Exercicio (do Trabalho) Final 2 (aplicação Web baseada em ASP.NET MVC e em WIF)**:

* foi criada uma base de dados simples (com apenas uma tabela) que simula alguns dados sobre fugitivos do FBI (*Federated Bureau International*);

* varios ficheiros teem o comentario (C# ou XML ou HTML) "HACK:" (sem aspas), de modo a especificar o que foi feito, em relacao a alteracoes.

### 09-04-2012 (fora do prazo):

Para o **Exercicio (do Trabalho) Final 2 (aplicação Web baseada em ASP.NET MVC e em WIF)** (cont.):

* foram realizados, em primeiro lugar, 2 exercicios da "Sessão 2":

 * "Exercício 1 - Relying Party", excepto os Passos:
 - * 1, pois nao e' aplicacao MVC vazia, ja' que tem AccountController;
 - * e 7, pois o codigo podera' ser util atraves de utilizacao diferente das classes nele referidas, noutros sitios da aplicacao MVC - ver mais abaixo, aquando do `RequireClaimsAttribute`;

 * e "Exercício 2 - Configurar ACS" (excepto o Passo 5, pois utilizou-se a opcao Add STS Reference - ver mais abaixo, aquando da metadata de STS);

* utilizou-se <http://www.we-coffee.com/x509builder.aspx> para gerar 2 certificados que possam ser importados atraves do *snap-in de Certificados do MMC* (ambos estao na pasta "TrabalhoFinal_Certificados\repositorio_modulo_aluno"):
 * Address:	www.prompt11tf2.local
 * TLS/SSL certificate friendly name:	www.prompt11tf2.local
 * TLS/SSL certificate key length (bits):	1024
 * Contact e-mail:	carlos\_bat\_faustino@hotmail.com
 * Organization Name:	CCISEL
 * State/Province:	Lisboa
 * Country:	Portugal
 * ICA common name:	PROMPT11UC08TF
 * ICA certificate friendly name:	PROMPT11UC08TF
 * ICA certificate key length (bits):	1024

 * certificado de raiz (**A8D3D.p12**) na pasta **Autoridades de certificacao de raiz fidedigna** da store *computador local (localmachine)* e da store *Utilizador actual (currentuser)*;
 * certificado dependente (**A6AD8\_WOd139.pfx**) para store `computador local (localmachine)` na pasta **Pessoal** (chave privada/password = **`WOd139`** );

* e, assim, com a geracao de certificados para site com nome diferente, foi possibilitada a alteracao do URI `http://www.prompt11.local:8443/` (referido nos enunciados dos exercicios do modulo) que passou agora a ser **`http://www.prompt11tf2.local:3443/`** (utilizado na implementacao deste exercicio do trabalho final do modulo);

* sendo a localizacao do site na pasta/namespace/projecto [`Ex2AppWebFbiMostWanted`](https://github.com/cmfaustino/PROMPT11-08-Security.cmfaustino/tree/master/TrabalhoFinal/Ex2AppWebFbiMostWanted) (com permissoes de leitura, execucao, e listagem para a conta/utilizador Windows `IIS APPPOOL\www.prompt11tf2.local`;

* com o enlace de protocolo **HTTPS no porto 3443** (e nao 8443, para ser possivel existirem as 2 versoes - de sessoes PROMPT, e de trabalho final PROMPT - na mesma maquina) com o certificado acima gerado `www.prompt11tf2.local`;

* metadata de STS (via opcao Add STS Reference) em <https://demos1-prompt11.accesscontrol.windows.net/FederationMetadata/2007-06/FederationMetadata.xml>, depois de erradamente se ter utilizado, com a mesma opcao, <https://prompt11.accesscontrol.windows.net/FederationMetadata/2007-06/FederationMetadata.xml>;

* Na criacao de Relying Party no ACS, atraves da opcao "Enter settings manually", foram utilizados os seguintes dados, em <https://demos1-prompt11.accesscontrol.windows.net/v2/mgmt/web>:
 * Name: applocal2
 * Realm: https://www.prompt11tf2.local:3443/
 * Return URL: https://www.prompt11tf2.local:3443/
 * Error URL (optional): https://www.prompt11tf2.local:3443/
 * Token format: SAML 2.0
 * Token encryption policy: None
 * Token lifetime (secs): 600
 * Identity providers: (todos, ou seja, Google, Windows Live ID, Yahoo!)
 * Rules/Regras: Google - _Pass through_ de todos ; Windows Live ID - _carlos\_bat\_faustino@hotmail.com_ fica com _role_ `admin` ; Yahoo! - _Pass through_ de _any name_

* criacao de login **IIS APPPOOL\www.prompt11tf2.local** em SQL Server Management Studio (SSMS) -> Security->Logins, e em Properties->User Mapping deste login, as permissoes: `db_owner` e `public` para a bd *Ex2AppWebFbiMostWanted.Models.FbiMostWantedContext*, e `public` para a bd *master*;

### 10-04-2012 e 11-04-2012 (fora do prazo):

Para o **Exercicio (do Trabalho) Final 2 (aplicação Web baseada em ASP.NET MVC e em WIF)** (cont.):

Colocaram-se comentarios no codigo, 'a semelhanca do que esta' no exercicio final 1 (com HACK, e nao so').

Relativamente ao que esta' referido _especificamente no enunciado do trabalho final_ (os "seguintes requisitos adicionais"):

* Comentou-se, no ficheiro _Web.config_, dentro de `<system.web>`, o codigo:

    `<authorization>
      <deny users="?" />
    </authorization>`

 e assim **e' permitido o acesso anonimo**, segundo <http://msdn.microsoft.com/en-us/library/8aeskccd.aspx>;

* <strike>Seguiu-se as instrucoes referidas em <http://support.microsoft.com/?kbid=2002980> para Windows 7, no Conjunto Aplicacional (AppPool) `www.prompt11tf2.local` do IIS (Express 7.5);</strike>

* Alteraram-se os metodos `LogOn()` e `LogOff()` de GET em _AccountController.cs_, para contemplarem o desencadeamento manual de operacoes de log-in e log-off relativas 'a autenticacao federada, ou caso esta nao exista no _Web.config_, manter as operacoes pre'-definidas em ASP.NET MVC. O ACS nao suporta log-off federado: <http://blogs.msdn.com/b/avkashchauhan/archive/2011/11/18/how-to-perform-clean-logout-from-your-application-which-use-azure-acs-amp-identity-providers.aspx>;

* O **atributo `Authorize`** no metodo `ChangePassword()` de GET em _AccountController.cs_ denota um **recurso protegido, e a autenticacao federada ocorre 'a mesma quando se tenta aceder** a <https://www.prompt11tf2.local:3443/Account/ChangePassword>, se bem que a existencia deste metodo e da respectiva View nao faz muito sentido neste cenario de federacao, e por isso, nesta aplicacao Web MVC nao existem ecrans com links para este URI;

* Atraves do [ACS em demos1-prompt11...](https://demos1-prompt11.accesscontrol.windows.net/v2/mgmt/web) -> Developer (Desenvolvimento) Application Integration (Integracao do aplicativo) -> Login Pages (Paginas de Logon) -> applocal2 -> Download Example Login Page (Exemplo de Pagina de Logon para Baixar), obteve-se a pagina _applocal2LoginPageCode.html_ que foi colocada em Views\Account da aplicacao Web MVC deste exercicio, e novamente, o metodo `LogOn()` de GET em _AccountController.cs_ foi alterado para retornar uma view _LogOnFederado.aspx_ que serve de intermediaria (pois a view renderiza a pagina html obtida do ACS) no caso de autenticacao federada, e assim, **a seleccao do _home realm_ e' feita na propria aplicacao Web e nao no ACS**;

* Customizou-se a apresentacao dos erros HTTP <strike>404 e 500</strike>, atraves de:
 * Controller _ErrorController.cs_;
 * <strike>Views _Http404.cshtml_ e _Http500.cshtml_ em Views\Error;</strike>
 * View _Error.cshtml_ em Views\Shared;
 * Alteracao em _Web.config_, com acrescento de `<httpErrors`... dentro de `<system.webServer>`;

* Criou-se o atributo `RequireClaimsAttribute` em Models, <strike>que pode ser especificado multiplas vezes,</strike> e, em cada especificacao, pode-se indicar, opcionalmente:
 * array de zero ou mais roles (`RolesAllowed`);
 * array de zero ou mais nomes completos (`NamesAllowed`);
 * array de zero ou mais enderecos de e-mail (`EmailAddressesAllowed`);
 * array de zero ou mais dominios de e-mail (`EmailDomainsAllowed`), sendo que cada dominio **nao contemplara'** sub-dominios (exemplo: autorizar "google.com" nao ira' autorizar "mail.google.com");
 * array de zero ou mais nomes de issuers (`IssuersAllowed`);
 * array de zero ou mais identificadores de nomes (`NameIdentifiersAllowed`).

* Se, em cada especificacao, forem indicadas mais do que uma lista, entao significa que se consideram como AND (exemplo: se, na especificacao do atributo, for indicada uma lista de e-mails e uma lista de roles, isso significa que a autenticacao verifica, nas claims do user, se existe algum dos e-mails E algum dos roles). Deste modo, existe **processo de autorizacao baseado em claims com o atributo `RequireClaimsAttribute`**. Configuracoes:

 * GET: /Home/About/ <strike>(**multiplas/3 especificacoes**)</strike> :
 * - <strike>EmailDomainsAllowed { "gmail.com", "yahoo.com" } (QUALQUER UM de 2 dominios);</strike>
 * - <strike>RolesAllowed { "admin" } (OBTIDO COM carlos\_bat\_faustino@hotmail.com);</strike>
 * - EmailDomainsAllowed { "hotmail.com" } , NamesAllowed { "Pedro Félix", "Carlos Faustino" } (2 condicoes AO MESMO TEMPO, QUALQUER UM de 2 nomes);

 * GET: /FbiMostWanted/Edit/ :
 * - EmailAddressesAllowed {"pmhsfelix@gmail.com" , "carlos\_bat\_faustino@hotmail.com"} (QUALQUER UM de 2 e-mails);

 * POST: /FbiMostWanted/Delete/ :
 * - IssuersAllowed { "Google", "Yahoo!" } (QUALQUER UM de 2 issuers);

* Para a implementacao deste atributo, acabou por se criar codigo que foi inspirado no codigo fornecido no Passo 7 do Exercicio 1 ("Relying Party") da Sessao 2 deste modulo PROMPT.

Consideracoes:

* So' mais tarde e' que se viu, em <http://msdn.microsoft.com/en-us/library/gg185923.aspx> -> Configuring with the ACS Management Portal -> Claim Rules, que _name identifiers_ sao compostos por algarismos (pelo menos, nos exemplos), e assim, em principio sera' desnecessaria a utilizacao de `.toLower()` em `RequireClaimsAttribute` em relacao aos mesmos.

* A view _LogOnFederado.aspx_, intermediaria entre a aplicacao web e a pagina obtida do ACS para seleccao do _home realm_ na propria aplicacao, pelo facto de renderizar essa pagina, faz com que, no final, o resultado html tenha a inconveniencia de ter 2 ocorrencias da tag **&lt;HTML**, mas tal justifica-se para que nao seja necessario fazer qualquer tratamento da pagina obtida do ACS (basta copiar qualquer versao da pagina obtida do ACS para dentro da directoria).

* Tentou fazer-se com que o atributo RequireClaimsAttribute fosse de especificacao multipla, mas nao ocorreram problemas para os quais nao se encontrou solucao.

* Durante o desenvolvimento, ocorreu o erro (26) explicado em <http://www.microsoft.com/products/ee/transform.aspx?ProdName=Microsoft+SQL+Server&EvtSrc=MSSQLServer&EvtID=-1>, e nao se conseguia ver as bases de dados atraves do SQL Server Management Studio. A resolucao para estes 2 problemas foi iniciar manualmente os 2 servicos Windows:
 * SQL Server (SQLEXPRESS)
 * SQL Server Browser

* No processo de autorizacao, o ecra fica branco quando o resultado e' negativo/sem sucesso, mas mostra conteudo quando e' positivo/com sucesso.

(Fim do ficheiro .md)