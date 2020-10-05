# Desafio DbServer
### Por William da Silva


Olá.

Este é o App Pokedex Desafio DbServer, desenvolvido para as plataformas  Android, iOS e UWP.
Neste App serão encontradas três telas:
	1. Tela de Entrada com Animação.
	2. Tela de Listagem de Pokemons com Filtro.
	3. Tela de Detalhes do Pokemon.

-----------------------------------

### TELAS
1. Tela de Entrada
	A tela de entrada contém apenas uma simples animação de entrada do aplicativo.
	Para as animações utilizei apenas as funcionalidades já existentes no Xamarin Forms (ex.: FadeTo, RotateTo, etc). 
	Após um tempo pré determinado a navegação é realizada para a tela seguinte.

2. Tela de Listagem
	O design da tela de listagem é composto por elementos do Xamarin Forms sem necessidade de utilização de componentes ou controles customizados.
	Para a paginação utilizei a biblioteca 'Xamarin.Forms.Extended.InfiniteScrolling' que trata de maneira automatizada a necessidade de mais registros enquanto o usuário faz a rolagem, bastando implementar o evento de carregamento.
	A lista de pokémons é sempre buscada na API, afim de exemplificar melhor a funcionalidade da paginação, enquanto que as imagens são armazenadas em um banco SQLite após o primeiro download.
<br>
### Services
Para o desenvolvimento dos serviços que o App faz uso, utilizei classes que costumo usar em alguns projetos
<br>
### Injeção de Dependência
Infelizmente não consegui deixar a injeção de dependências como eu queria a tempo. Por conta do prazo acabei fazendo uso de um 'ContainerHelper' que continha os serviços instanciados de forma pública e estática, permitindo seu uso de qualquer lugar do código.
