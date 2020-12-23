# Validação do Design de uma API Rest

O objetivo deste projeto é validar o Design de uma API Rest

##  Funcionalidades
- Verificar se todos os paths estão no idioma definido
- Verificar se todos os paths estão respeitando os padrões de rotas definido
- Verificar se todos os paths estão respeitando os verbos http
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
- Utilizar o Maturity Model do Martin Fowler para avaliar o nível da API
- Utilizar um container docker para executar a aplicação
- Utilizar o output para jogar a avaliação final
- Utilizar algum board para quebrar todas as atividades

## Material de Apoio
- https://medium.com/@wssilva.willian/design-de-api-rest-9807a5b16c9f
— https://blog.octo.com/pt-br/projetando-uma-api-rest/
— https://github.com/Gutem/http-api-design/
— https://www.thoughtworks.com/pt/insights/blog/rest-api-design-resource-modeling
— https://martinfowler.com/articles/richardsonMaturityModel.html
— https://tools.ietf.org/html/rfc2616
— https://developers.google.com/web/fundamentals/performance/http2/
— https://developer.mozilla.org/pt-BR/docs/Web/HTTP