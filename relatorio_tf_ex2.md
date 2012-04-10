## Changelog (por ordem cronologica normal/nao inversa):

Antes de se ler este ficheiro, convem ler as notas iniciais contidas no ficheiro [readme.md](https://github.com/cmfaustino/PROMPT11-08-Security.cmfaustino/blob/master/readme.md).

### 22-03-2012 (fora do prazo):

Para o **Exercicio (do Trabalho) Final 2 (aplicação Web baseada em ASP.NET MVC e em WIF)**:

* foi criada uma base de dados simples (com apenas uma tabela) que simula alguns dados sobre fugitivos do FBI (*Federated Bureau International*);

* varios ficheiros teem o comentario (C# ou XML ou HTML) "HACK:" (sem aspas), de modo a especificar o que foi feito, em relacao a alteracoes.

### 09-04-2012 (fora do prazo):

Para o **Exercicio (do Trabalho) Final 2 (aplicação Web baseada em ASP.NET MVC e em WIF)** (cont.):

* foram realizados, em primeiro lugar, os exercicios "Exercício 1 - Relying Party" (excepto os Passos 1, pois nao e' app MVC vazia, e 7, pois o codigo podera' ser util atraves de utilizacao diferente das classes nele referidas) e "Exercício 2 - Configurar ACS" (excepto o Passo 5, pois utilizou-se a opcao Add STS Reference) da "Sessão 2";

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

* e, assim, com a geracao de certificados para site com nome diferente, foi possibilitada a alteracao do URI `http://www.prompt11.local` (referido nos enunciados dos exercicios do modulo) que passou agora a ser **`http://www.prompt11tf2.local`** (utilizado na implementacao deste exercicio do trabalho final do modulo);

* sendo a localizacao do site na pasta/namespace/projecto [`Ex2AppWebFbiMostWanted`](https://github.com/cmfaustino/PROMPT11-08-Security.cmfaustino/tree/master/TrabalhoFinal/Ex2AppWebFbiMostWanted) (com permissoes de leitura, execucao, e listagem para a conta/utilizador Windows `IIS APPPOOL\www.prompt11tf2.local`;

* com o enlace de protocolo **HTTPS no porto 3443** com o certificado acima gerado `www.prompt11tf2.local`;

* metadata de STS (via opcao Add STS Reference) em <https://demos1-prompt11.accesscontrol.windows.net/FederationMetadata/2007-06/FederationMetadata.xml>, depois de erradamente se ter utilizado, com a mesma opcao, <https://prompt11.accesscontrol.windows.net/FederationMetadata/2007-06/FederationMetadata.xml>;

* criacao de login **IIS APPPOOL\www.prompt11tf2.local** em SQL Server Management Studio (SSMS) -> Security->Logins, e em Properties->User Mapping deste login, as permissoes: `db_owner` e `public` para a bd *Ex2AppWebFbiMostWanted.Models.FbiMostWantedContext*, e `public` para a bd *master*;

--> Este exercicio esta' incompleto, pois ainda nao se fez nada do que esta' referido _especificamente no enunciado do trabalho final_ (os "seguintes requisitos adicionais").

(Fim do ficheiro .md)