# Projeto Biblioteca

Este é um projeto de exemplo de uma aplicação de biblioteca, com backend em .NET Core e frontend em Vue.js.

## Tecnologias Utilizadas

### Backend
* **.NET Core:** Framework para desenvolvimento de aplicações web.
* **ASP.NET Core:** Para a construção da API REST.
* **Entity Framework Core:** ORM para interação com o banco de dados.
* **Postgresql:** Banco de dados relacional.

### Frontend
* **Vue.js:** Framework progressivo para a construção de interfaces de usuário.
* **TypeScript:** Superset de JavaScript que adiciona tipagem estática.
* **Vite:** Ferramenta de build para o frontend.

### Containerização
* **Docker:** Plataforma para desenvolvimento, deploy e execução de aplicações em containers.
* **Docker Compose:** Ferramenta para definir e rodar aplicações multi-container Docker.

## Padrões Utilizados

* **Repository Pattern:** Abstrai a camada de acesso a dados, facilitando a manutenção e a troca do banco de dados.
* **Service Layer:** Isola a lógica de negócio da aplicação, promovendo a reutilização de código e a separação de responsabilidades.
* **Dependency Injection:** Utilizado para gerenciar as dependências entre os componentes da aplicação.
* **DTOs (Data Transfer Objects):** Utilizados para transferir dados entre as camadas da aplicação, evitando a exposição de entidades do domínio.

## Como Executar o Projeto

### Pré-requisitos
* Docker
* Docker Compose

### Passos

1. Abra um terminal na pasta raiz do projeto.
2. Execute o seguinte comando para construir e iniciar os containers:

```bash
docker-compose up -d --build
```

3. A aplicação estará disponível nos seguintes endereços:
    * **Frontend:** [http://localhost:8080](http://localhost:8080)
    * **Backend API:** [http://localhost:5000](http://localhost:5000)
