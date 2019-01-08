# CRUD utilizando .Net Core + WebApi + MariaDb + Dapper
Teste para a ingresso.com

#### Para execução do projeto a seguir é necessário:    

**1 -** Ter o MariaDB/MySQL instalado localmente  
**2 -** Configurar o acesso ao banco para que tenha o usuário/senha = root/admin(a conexão da api é feita com essas credenciais!)  
**3 -** Executar o script SQL "create_database.sql" que está no diretório "database" na raiz do repositório para a criação do banco de dados, como preferir(executando o script manualmente, importando pela IDE, etc)    

A api possui 5 endpoints para as 5 entidades pedidas no teste, cada qual com seus respectivos métodos CRUD(cada um utilizando seu respectivo HTTP Verb: GET,POST, PUT E DELETE).

# Entidade Filme
**GET**  
https://localhost:[porta]/api/Filme: Retorna JSON com todos os filmes cadastrados  
https://localhost:[porta]/api/Filme/1: Retorna JSON com o filme que contenha o id passado na url    

**POST**  
https://localhost:[porta]/api/Filme: Insere filme no banco de dados, de acordo com JSON passado.  
Ex de JSON esperado:    

```json
{  
  "nomeFilme": "Aquaman",  
  "genero": "Ação, Aventura",  
  "classificacaoIdade": 12,  
  "sinopse": "O filme revela a história de origem do meio-humano, meio-Atlante Arthur Curry, levando-o na jornada de sua vida — uma jornada que vai forçá-lo a não só encarar sua verdadeira identidade, mas também a descobrir se ele tem o que é necessário para ser… um rei." 
}
```  

**PUT**  
https://localhost:[porta]/api/Filme/1: Atualiza filme de acordo com JSON e id na url passados.  
Ex de JSON esperado: o mesmo do método POST.  

**DELETE:** 
https://localhost:[porta]/api/Filme/1: Exclui filme que contenha o id passado na url.  

# Entidade Cidade
**GET**  
https://localhost:[porta]/api/Cidade: Retorna JSON com todas as cidades cadastradas 
https://localhost:[porta]/api/Cidade/1: Retorna JSON com a cidade que contenha o id passado na url    

**POST**  
https://localhost:[porta]/api/Cidade: Insere cidade no banco de dados, de acordo com JSON passado.  
Ex de JSON esperado:    

```json
{  
  "nomeCidade": "RJ"  
}
```

**PUT**  
https://localhost:[porta]/api/Cidade/1: Atualiza cidade de acordo com JSON e id na url passados.  
Ex de JSON esperado: o mesmo do método POST.  

**DELETE**  
https://localhost:[porta]/api/Cidade/1: Exclui cidade que contenha o id passado na url.  

# Entidade Cinema
**GET**  
https://localhost:[porta]/api/Cinema: Retorna JSON com todos os cinemas cadastrados  
https://localhost:[porta]/api/Cinema/1: Retorna JSON com o cinema que contenha o id passado na url    

**POST**  
https://localhost:[porta]/api/Cinema: Insere cinema no banco de dados, de acordo com JSON passado.  
Ex de JSON esperado:    

```json
{  
  "nomeCinema": "Cinemark Barra",  
  "endereco": "Teste de endereço",  
  "idCidade": 1,  
  "salasArray": [1,2]  
}
```

**idCidade**: Cidade a qual o cinema pertence
**salasArray:** Array de int que informa quais salas serão cadastradas para o cinema.
**Obs:** Necessita ter as salas inseridas no array cadastradas, o mesmo para a cidade.

**PUT**  
https://localhost:[porta]/api/Cinema/1: Atualiza cinema de acordo com JSON e id na url passados.  
Ex de JSON esperado: o mesmo do método POST.  

**DELETE**  
https://localhost:[porta]/api/Cinema/1: Exclui cinema que contenha o id passado na url.  

# Entidade Sala
**GET**  
https://localhost:[porta]/api/Sala: Retorna JSON com todas as salas cadastradas   
https://localhost:[porta]/api/Sala/1: Retorna JSON com a sala que contenha o id passado na url    

**POST**  
https://localhost:[porta]/api/Sala: Insere sala no banco de dados, de acordo com JSON passado.  
Ex de JSON esperado:    

```json
{  
  "nomeSala": "Sala 1"
}
```

**PUT**  
https://localhost:[porta]/api/Sala/1: Atualiza sala de acordo com JSON e id na url passados.  
Ex de JSON esperado: o mesmo do método POST.  

**DELETE**  
https://localhost:[porta]/api/Filme/1: Exclui sala que contenha o id passado na url.  

# Entidade Sessao
**GET**  
https://localhost:[porta]/api/Sessao: Retorna JSON com todas as sessões cadastradas  
https://localhost:[porta]/api/Sessao/1: Retorna JSON com a sessão que contenha o id passado na url    

**POST:**  
https://localhost:[porta]/api/Sessao: Insere sessão no banco de dados, de acordo com JSON passado.  
Ex de JSON esperado:    

```json
{  
  "preco": 17.5,  
  "dataSessao": "2019-01-10",  
  "hora": "2019-01-10 15:00:00",  
  "tipoIdioma": "Dublado",
  "idCinema": 1,
  "idSala": 1,
  "idFilme": 1
}
```    
**Obs:** Necessita das entidades Filme, Sala e Cinema previamente cadastradas.

**PUT:**  
https://localhost:[porta]/api/Sessao/1: Atualiza sessão de acordo com JSON e id na url passados.  
Ex de JSON esperado: o mesmo do método POST.  

**DELETE:**  
https://localhost:[porta]/api/Sessao/1: Exclui sessão que contenha o id passado na url.  
