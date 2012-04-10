## Changelog (por ordem cronologica normal/nao inversa):

Antes de se ler este ficheiro, convem ler as notas iniciais contidas no ficheiro [readme.md](https://github.com/cmfaustino/PROMPT11-08-Security.cmfaustino/blob/master/readme.md).

### 13-03-2012 (dentro do prazo):

Em relacao ao **Exercicio (do Trabalho) Final 1 (calculo de hash para um ficheiro(s))**:

* Esta implementacao permite utilizar qualquer uma das classes de `System.Security.Cryptography` indicadas em *`HashComputacao.HashMetodoCalculo.Metodos`* , inclusive `System.Security.Cryptography.RIPEMD160Managed`;

* Todos os ficheiros relativos ao codigo que implementa este Exercicio Final teem comentarios, mais ou menos legiveis;

* A implementacao do mecanismo de calculo de hash divide-se em 2 partes:

 A pasta/namespace/projecto [`HashComputacao`](https://github.com/cmfaustino/PROMPT11-08-Security.cmfaustino/tree/master/TrabalhoFinal/HashComputacao) contem classes que sao componentes isolados, contendo logica independente;

 A pasta/namespace/projecto [`HashDeFicheiro`](https://github.com/cmfaustino/PROMPT11-08-Security.cmfaustino/tree/master/TrabalhoFinal/HashDeFicheiro) contem a logica do programa requerido pelo enunciado, que, com excepcao de algumas validacoes, limita-se, depois, a invocar o que esta' na logica independente referida anteriormente.

* Consideracoes:

 [`System.Security.Cryptography.RIPEMD160Managed`](http://msdn.microsoft.com/en-us/library/system.security.cryptography.ripemd160managed.aspx) parece ser a unica classe (ou uma das classes) cuja implementacao de algoritmo de hash nao esta' contemplada em [`HashAlgorithm.Create`](http://msdn.microsoft.com/en-us/library/wet69s13.aspx), e por isso, pensa-se que o uso de [`HashAlgorithm.ComputeHash`](http://msdn.microsoft.com/en-us/library/system.security.cryptography.hashalgorithm.computehash.aspx) nao contempla todos os casos disponiveis na plataforma .NET (sera' gaffe da Microsoft? ou mau entendimento da documentacao MSDN por parte do aluno do PROMPT?);

 A sintaxe do programa realizado e' `HashDeFicheiro.exe <ficheiro_com_ou_sem_wildcards> (metodo_de_algoritmo_opcional)`. Teria sido melhor trocar a ordem dos parametros, e colocar primeiro o algoritmo/classe de hash, e so' depois o ficheiro, pois, desta forma, poder-se-ia enunciar varios ficheiros sem ser apenas por meio de wildcards * e ?, e percorrer-se o array `args[]` que existe como parametro do metodo `main` do programa, como forma de se ir obtendo os varios ficheiros, 'a semelhanca do que acontece com varios comandos MS-DOS ou UNIX.<br />
 Tal nao foi considerado na implementacao por esquecimento, mas, o modo actualmente implementado permite a possibilidade de nao haver metodo de hash especificado, e neste caso, utiliza-se um metodo de hash pre-definido, e tal e' possivel pelo facto do numero de parametros ser fixo (1 ou 2 parametros, de acordo com esta sintaxe), e do primeiro parametro ser o ficheiro, a nao ser que, no caso de indeterminado numero de parametros, se considerasse sempre o ultimo argumento como sendo o metodo de hash, o que tambem poderia ser confundido com alguma especificacao de ficheiro.

(Fim do ficheiro .md)