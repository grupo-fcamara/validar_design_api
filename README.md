# Desafio validar desing de API

Esse é um desafio para validarmos o desing de uma API Rest!

##  Funcionalidades
- Verificar se todos os paths estão no idioma definido
- Verificar se todos os paths estão respeitando o padrão de rotas definido
- Verificar se todos os paths estão respeitando o verbo http
- Verificar se todos os paths estão respeitando os códigos http esperados para cada verbo http
- Verificar se todos os paths não possuem operações (cadastrar / consultar / getByID)
- Verificar se os níveis de path estão dentro do limite permitido
- Verificar se todos os paths possuem no maximo 1 identificador (Por exemplo get -> /veiculo/{codigo} e /veiculo/{id} não são permitidos)
- Verificar se todos os paths de consultas gerais possuem paginação (Por exemplo get -> /veiculos)
- Verificar se todos os paths possuem no maximo 2 rotas get (Permitido a consulta paginada /veiculos e a consulta pelo identificador /veiculo/{codigo})
- Verificar se todos os paths de consultas gerais possuem ordenação (Por exemplo get -> /veiculos)
- Verificar se todos os paths implementam HATEOAS
- Definir um nível para a API (De 0 a 5)
- Gerar um output com os problemas

## Pré-requisos
Antes de iniciar a validação da API, devemos passar como parametro as seguintes opções:

- Idioma: Devemos informar se o path esta em portugues ou inglês (Opcional / Padrão ingles)
- Padrão de Rotas: Devemos infomar se o padrão utilizado pela empresa é o Plural / Singular / Camel Case / Snak Case / Spinal Case (Opcional / padrão plural)
- Base URL: Deve ser informado o base url do serviço (Obrigatório)
- Path Swagger: Deve ser informado o path do swagger (Obrigatório)
- Path Versionado: Deve ser informado se a versão da api esta no path (Opcional / Padrão false)
- Verbos: Deve ser informado quais os verbos http permitidos (Opcional / Sugerir padrão)
- StatusCode x Verbos: Deve ser informado quais os verbos http permitidos por verbo (Opcional / Sugerir padrão)
- Níveis de Path: Deve ser informado quantos níveis são permitidos por path (Opcional / Padrão 2)

## Requisitos
- Utilizar Java 11 ou .net core 3
- Definir uma regra de avaliação da qualidade de API
- Definir como será o output dessa avaliação

## Material de Apoio
- https://medium.com/@wssilva.willian/design-de-api-rest-9807a5b16c9f