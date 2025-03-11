# Controle de Fluxo de Caixa

## Descrição do Projeto
Este projeto tem como objetivo desenvolver uma arquitetura de software escalável e resiliente para controlar o fluxo de caixa diário de um comerciante. O sistema inclui funcionalidades para lançamentos de débitos e créditos e a geração de relatórios diários consolidados.

## Tecnologias Utilizadas
- **.NET Core 9.0**: Framework para desenvolvimento dos serviços, escolhido por sua robustez e alta performance.
- **Docker**: Para containerização dos serviços, garantindo portabilidade e consistência entre ambientes.
- **Kubernetes**: Para orquestração de containers, facilitando o gerenciamento de escalabilidade e resiliência.
- **Nginx**: Utilizado como balanceador de carga para distribuir o tráfego entre as instâncias dos serviços.
- **Redis**: Sistema de cache distribuído para melhorar a performance e reduzir a carga nos serviços.
- **MongoDB**: Banco de dados NoSQL para armazenamento de dados.
- **Polly**: Biblioteca para implementação de padrões de resiliência como retries e circuit breakers.
- **Prometheus e Grafana**: Ferramentas para monitoramento e visualização de métricas.
- **OAuth2 e JWT**: Para autenticação e autorização seguras.
- **Azure API Management**: Para gerenciamento e proteção das APIs.
- **RabbitMQ**: Para mensageria assíncrona entre serviços.

## Configuração do Ambiente
### Pré-requisitos
- Docker instalado
- Kubernetes configurado
- .NET Core SDK 9.0 instalado

### Passos para Configuração
1. Clone o repositório:
    ```bash
    git clone https://github.com/seu-usuario/controle-fluxo-caixa.git
    cd controle-fluxo-caixa
    ```

2. Configure as variáveis de ambiente:
    ```bash
    cp .env.example .env
    # Edite o arquivo .env com suas configurações
    ```

3. Construa e inicie os containers Docker:
    ```bash
    docker-compose up --build
    ```

## Execução da Aplicação
### Localmente
1. Navegue até o diretório do serviço desejado:
    ```bash
    cd src/ServicoControleLancamentos
    ```

2. Execute o serviço:
    ```bash
    dotnet run
    ```

### Kubernetes
1. Aplique os manifests do Kubernetes:
    ```bash
    kubectl apply -f k8s/
    ```

## Testes
### Executando Testes Unitários e de Integração
1. Navegue até o diretório do serviço desejado:
    ```bash
    cd src/ServicoControleLancamentos
    ```

2. Execute os testes:
    ```bash
    dotnet test
    ```

## Deploy
### CI/CD
O pipeline de CI/CD está configurado no Azure DevOps/GitLab. Para realizar o deploy, siga os passos abaixo:
1. Faça commit das suas alterações:
    ```bash
    git add .
    git commit -m "Descrição das alterações"
    git push origin main
    ```

2. O pipeline será executado automaticamente e realizará o deploy para o ambiente configurado.

## Documentação
### Decisões Arquiteturais
- **Padrão Arquitetural**: Optamos por uma arquitetura de microsserviços para garantir escalabilidade e resiliência. Cada serviço pode ser desenvolvido e implantado independentemente.
- **Tecnologias**: Escolhemos .NET Core 9.0, Docker, Kubernetes, Nginx, Redis, Polly, Prometheus, Grafana, OAuth2, JWT, Azure API Management e RabbitMQ por suas capacidades de alta performance, segurança e facilidade de integração.

### Diagramas
- **Diagrama de Arquitetura**: Link para o diagrama de arquitetura
- **Diagrama de Fluxo de Dados**: Link para o diagrama de fluxo de dados
- **Diagrama de Implantação**: Link para o diagrama de implantação

### Documentação de APIs
- **Swagger/OpenAPI**: A documentação das APIs está disponível em swagger.yaml. Inclui todos os endpoints, métodos HTTP, parâmetros e exemplos de requisições e respostas.

### Políticas de Segurança
- **Autenticação e Autorização**: Implementamos OAuth2 e JWT para garantir que apenas usuários autenticados e autorizados possam acessar os serviços.
- **Criptografia**: Todos os dados sensíveis são criptografados em trânsito (HTTPS) e em repouso.
- **Proteção contra Ataques**: Implementamos medidas de segurança como rate limiting e proteção contra ataques DDoS.

### Monitoramento e Logging
- **Configuração de Monitoramento**: Utilizamos Prometheus para monitoramento e Grafana para visualização de métricas. A configuração detalhada está disponível em monitoring_setup.md.
- **Configuração de Logging**: Utilizamos o ELK Stack (Elasticsearch, Logstash, Kibana) para logging centralizado. A configuração detalhada está disponível em logging_setup.md.

### Testes
- **Estratégia de Testes**: Implementamos testes unitários, de integração e de carga para garantir a qualidade do código. A estratégia de testes está documentada em testing_strategy.md.
- **Cobertura de Testes**: Relatórios de cobertura de testes estão disponíveis em coverage_report.md.

### Futuras Evoluções
- **Roadmap**: Incluímos um roadmap com possíveis evoluções e melhorias para o sistema em roadmap.md.
- **Ideias de Implementação**: Descrevemos ideias de funcionalidades ou melhorias que poderiam ser implementadas no futuro em future_ideas.md.

## Contribuição
Contribuições são bem-vindas! Por favor, abra uma issue ou envie um pull request.

## Licença
Este projeto está licenciado sob a Licença MIT. Veja o arquivo LICENSE para mais detalhes.