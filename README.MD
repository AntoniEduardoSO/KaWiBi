# KaWiBi - Back-end do Sistema de Tickets
Este repositório contém o back-end de um sistema de tickets focado em atender tanto o cliente (que pode abrir seus chamados) quanto a equipe de suporte (responsável por gerenciá-los). O projeto foi idealizado e desenvolvido pela dupla Katochi (Back-end) e Wizarion (Front-end), visando oferecer uma solução completa, eficiente e escalável para a gestão de chamados.

## Sobre o Projeto
O KaWiBi é um sistema de gestão de tickets (chamados) que permite a interação entre clientes e equipe de suporte de forma simples e organizada. O objetivo principal é tornar o processo de abertura, acompanhamento e resolução de chamados mais ágil e transparente.

## Principais características:

* Criação, edição e acompanhamento de tickets.
* Filtros avançados para encontrar tickets por status, título e outros critérios.
* Integração com um front-end React (desenvolvido por Wizarion).
* Arquitetura escalável e manutenível.

## Pré-Requisitos
* .NET 7.0 SDK
* Banco de Dados (ex.: SQL Server ou PostgreSQL, conforme sua configuração)
* Ferramentas de controle de versão (Git)

## Como Executar o Projeto
Siga as instruções abaixo dentro da pasta raiz do projeto (KaWiBi.Api):

1. Aplicar as Migrations ao Banco de Dados:

```bash
dotnet ef database update
```
2. Restaurar Todas as Dependências:

```bash
dotnet restore
```
3. Limpar Caches e Arquivos Temporários:

```bash
dotnet clean
```
4. Compilar o Projeto:

```bash
dotnet build
```
5. Executar a Aplicação:

```bash
dotnet run
```
Com a aplicação em execução, o back-end estará disponível, pronto para se comunicar com o front-end na porta 3000 e responder às requisições de criação e gerenciamento de tickets.

## Contribuição
Feedback, sugestões e contribuições são bem-vindas! Sinta-se à vontade para abrir issues, enviar pull requests ou entrar em contato diretamente com os desenvolvedores.

Este README fornece uma visão geral do projeto, explica seu propósito, descreve como executar e orienta o desenvolvedor ou usuário a entender o funcionamento do sistema.

## Sistema atualmente em Desenvolvimento

Caso deseje utilizar o programa, pode acessar na url swagger/index.html, logo mais sera upado esse projeto!

[MIT](https://choosealicense.com/licenses/mit/)