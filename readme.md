# PROMPT 2011 - Mo'dulo 8 - Mo'dulo "Seguranca e Gestao de Identidade na Web"

## Repositorio de cmfaustino

...Texto...

## Changelog:

### 09-04-2012 (fora do prazo):

Para o Exercicio 2 (aplicação Web baseada em ASP.NET MVC e em WIF) do Trabalho Final (cont.):

* foram realizados, em primeiro lugar, os exercicios "Exercício 1 - Relying Party" (excepto os Passos 1 e 7) e "Exercício 2 - Configurar ACS" (excepto o Passo 5) da "Sessão 2";

* utilizando <http://www.we-coffee.com/x509builder.aspx> para gerar 2 certificados que possam ser importados atraves do *snap-in de Certificados do MMC* (ambos estao na pasta "TrabalhoFinal_Certificados\repositorio_modulo_aluno"):
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

* e, assim, com a geracao de certificados para site com nome diferente, foi possibilitada a alteracao de `http://www.prompt11.local` (nos enunciados dos exercicios do modulo) que passou a ser `http://www.prompt11tf2.local` (no enunciado do trabalho final do modulo);

* sendo a localizacao do site na pasta "TrabalhoFinal\Ex2AppWebFbiMostWanted" (com permissoes de leitura, execucao, e listagem para a conta/utilizador Windows `IIS APPPOOL\www.prompt11tf2.local`;

* com o enlace de protocolo **HTTPS no porto 3443** com o certificado acima gerado `www.prompt11tf2.local`;

* metadata de STS em <https://demos1-prompt11.accesscontrol.windows.net/FederationMetadata/2007-06/FederationMetadata.xml>, depois de erradamente se ter utilizado <https://prompt11.accesscontrol.windows.net/FederationMetadata/2007-06/FederationMetadata.xml>;

* criacao de login **IIS APPPOOL\www.prompt11tf2.local** em SQL Server Management Studio (SSMS) -> Security->Logins, e em Properties->User Mapping deste login, as permissoes: `db_owner` e `public` para a bd *Ex2AppWebFbiMostWanted.Models.FbiMostWantedContext*, e `public` para a bd *master*;

### 22-03-2012 (fora do prazo):

Para o Exercicio 2 (aplicação Web baseada em ASP.NET MVC e em WIF) do Trabalho Final:

* foi criada uma base de dados simples (com apenas uma tabela) que simula alguns dados sobre fugitivos do FBI (*Federated Bureau International*);

* varios ficheiros teem o comentario (C# ou XML ou HTML) "HACK:" (sem aspas), de modo a especificar o que foi feito, em relacao a alteracoes.

### 13-03-2012 (dentro do prazo):

Exercicio 1 (calculo de hash para ficheiro(s)) do Trabalho Final esta' em Trabalho Final.

Esta implementacao permite utilizar qualquer uma das classes de System.Security.Cryptography indicadas em HashComputacao.HashMetodoCalculo.Metodos , inclusive System.Security.Cryptography.RIPEMD160Managed .

Por agora, nao ha relatorio, pois todos os ficheiros de codigo que implementa o Exercicio 1 teem comentarios, mais ou menos legiveis.

Os Exercicios 2, 3 e 4 do Trabalho Final ainda nao estao feitos. Preve-se haver mais alguma coisa feita/implementada, ate' Sabado dia 17 de Marco de 2012.
