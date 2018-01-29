# DesafioImusica

1- Arquivos para criação do banco de dados estão na pasta Database.
 
2 - Para rodar o projeto será necessário, gerar um repositório com as configurações de banco locais. Deverá ser excluído o arquivo "Repository" no caminho DAL/Entity/, após esse passo
adicionaremos na respectiva pasta um novo item, esse item deverá ser ADO.NET Entity Data Model com o nome padrão "Repository", na próxima tela escolheremos o modelo EF Designer from
database. O nome da string de conexão deverá ser "Connection". Isso gerará uma "connectionStrings" no arquivo App.Config, a mesma deverá ser copia da e colada no arquivo "Web.config"
no projeto "WebPage".


3 - Pontos importantes.
	
	3.1 - Arquivo com os scripts esta em WebPage/Scripts/Site/Site.js
	
	3.2 - Para adicionar um dependente devesse clicar no botão "+" no formulário de cadastro do funcionário
	
	3.3 - Para a excluir um dependente devesse selecionar a opção "Sim" no campo "Excluir Dependente" no formulário de cadastro do funcionário
	
	3.4 - A pesquisa por nome funcionário utiliza um "like/contains"
	
	3.5 - Existe um botão "Gerar Relatório" na página de consulta dos funcionários, o mesmo gera um arquivo excel utilizando o EPPlus
	
	3.6 - Foi criada uma otimização da query que faz a consulta dos funcionários, a mesma faz a busca sempre de 10 em 10 tuplas sempre se baseando na paginação
