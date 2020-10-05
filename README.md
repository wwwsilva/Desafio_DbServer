# Desafio DbServer
### Por William da Silva


Olá.

Este é o App Pokedex Desafio DbServer, desenvolvido para as plataformas  Android, iOS e UWP.
Utilizando a arquitetura MVVM, neste aplicativo serão encontradas três telas:
	1. Tela de Entrada com Animação.
	2. Tela de Listagem de Pokemons com Filtro.
	3. Tela de Detalhes do Pokemon.

Espero que gostem e que olhem para o resultado e para o código com esmero, assim como foi o desenvolvimento nesses dois dias.

-----------------------------------

### TELAS
1. **Tela de Entrada**
	A tela de entrada contém apenas uma simples animação de entrada do aplicativo.
	Para as animações utilizei apenas as funcionalidades já existentes no Xamarin Forms (ex.: FadeTo, RotateTo, etc). 
	Após um tempo pré-determinado a navegação é realizada para a tela seguinte.

2. **Tela de Listagem**
	O design da tela de listagem é composto por elementos do Xamarin Forms sem necessidade de utilização de componentes ou controles customizados.
	Para a paginação utilizei a biblioteca 'Xamarin.Forms.Extended.InfiniteScrolling' que trata de maneira automatizada a necessidade de mais registros enquanto o usuário faz a rolagem, bastando implementar o evento de carregamento.
	A lista de pokémons é sempre buscada na API, afim de exemplificar melhor a funcionalidade da paginação, enquanto que as imagens são armazenadas em um banco SQLite após o primeiro download.

3. **Tela de Detalhes do Pokemon**
	A tela de detalhes consiste em uma PopupPage fornecida pela biblioteca 'Rg.Plugins.Popup', que já muito conhecida. Assim como a tela de listagem, também não foi necessário desenvolvimento de nenhum componente ou controle customizado. 
	Os dados do pokémon são buscados na API na primeira vez que ele é acessado e após isso ele é armazenado no bando SQLite.
	

### Services
Para o desenvolvimento dos serviços que o App faz uso, utilizei classes que costumo usar em alguns projetos. 
	* ApiService;
	* DataBaseService;
	* MessageService;
	* NavigationService;

### Ícones
Os ícones utilizados na aplicação estão adicionados apenas no projeto **portable**, na pasta Resources. Para o gerenciamento desses recursos, há uma classe chamada 'ImageHelper' que, em conjunto com o 'ImageConverter', gerencia a utilização dos arquivos e cria um cache em memória do que está sendo utilizado. Esta mesma classe poderia ter uma ligação com o banco de dados e gerenciar as imagens dos pokémons, porém não pude implementar a tempo.

### Acesso a API
Para acessar a API foi criada a classe ApiService que contém métodos Get genéricos, tanto para objetos quanto para arquivos.

### Injeção de Dependência
Infelizmente não consegui deixar a injeção de dependências como eu queria a tempo. Por conta do prazo acabei fazendo uso de um 'ContainerHelper' que continha os serviços instanciados de forma pública e estática, permitindo seu uso de qualquer lugar do código.

### Pacotes Utilizados
* Xamarin.Forms.Extended.InfiniteScrolling;
* Rg.Plugins.Popup;
* sqlite-net-pcl;
* SQLiteNetExtensions;
* Newtonsoft.Json;

### Observações
1. Como não tenho MAC disponível, não pude testar a compilação para IOS. Os testes foram realizados em Android e UWP.
2. Gosto de utilizar o mínimo de pacotes de terceiros. Por experiência, atualizações de versão costumam ser penosas quando há um uso excessivo de dependências de terceiros (mas nem por isso precisamos reinventar a roda). 
3. Utilização de CustomRenders no código não é um problema para mim, mas por uma escolha pessoal, prefiro evitar seu uso afim de objetivar ao máximo o desenvolvimento no projeto portable.

### IMPORTANTE
**Se for executar em UWP, a versão mínima é a 1809 (build 17763).**


---------

Agradeço a oportunidade de mostrar meu trabalho e espero que em um futuro próximo estejamos trabalhando juntos em grandes projetos.
 ; D