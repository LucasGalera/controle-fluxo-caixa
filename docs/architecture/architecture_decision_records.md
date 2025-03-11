# Decisões Arquiteturais (ADR - Architectural Decision Records)

## 1. Padrão Arquitetural

### Decisão
Optamos por utilizar uma arquitetura de microsserviços.

### Motivos
- **Escalabilidade**: Microsserviços permitem escalar individualmente cada serviço conforme a demanda, garantindo melhor utilização de recursos.
- **Resiliência**: A falha de um serviço não afeta os demais, aumentando a resiliência do sistema.
- **Desenvolvimento Independente**: Equipes podem trabalhar em diferentes serviços de forma independente, acelerando o desenvolvimento.
- **Flexibilidade Tecnológica**: Cada serviço pode ser desenvolvido com a tecnologia mais adequada para sua função específica.

### Trade-offs Considerados
- **Complexidade Operacional**: Microsserviços aumentam a complexidade de implantação e monitoramento, exigindo ferramentas robustas de orquestração e monitoramento.
- **Comunicação entre Serviços**: A comunicação entre microsserviços pode introduzir latência e complexidade adicional, necessitando de protocolos eficientes e mecanismos de fallback.

## 2. Tecnologias Utilizadas

### .NET Core 9.0
- **Motivos**: 
  - Alta performance e suporte a aplicações escaláveis.
  - Suporte a desenvolvimento multiplataforma.
  - Comunidade ativa e suporte a longo prazo.

### Docker
- **Motivos**: 
  - Facilita a containerização dos serviços, garantindo portabilidade e consistência entre ambientes.
  - Amplamente adotado e suportado por diversas plataformas de orquestração.
  - **Versão Atual**: 20.10.8

### Kubernetes
- **Motivos**: 
  - Orquestração de containers, facilitando o gerenciamento de escalabilidade e resiliência.
  - Suporte a deploys automatizados, escalabilidade automática e recuperação de falhas.
  - **Versão Atual**: 1.22.0

### Nginx
- **Motivos**: 
  - Balanceamento de carga eficiente e flexível.
  - Suporte a proxy reverso e caching.
  - **Versão Atual**: 1.21.3

### Redis
- **Motivos**: 
  - Sistema de cache distribuído de alta performance.
  - Suporte a persistência de dados e operações atômicas.
  - **Versão Atual**: 6.2.5

### Polly
- **Motivos**: 
  - Implementação de padrões de resiliência como retries e circuit breakers.
  - Fácil integração com .NET Core.
  - **Versão Atual**: 7.2.3

### Prometheus e Grafana
- **Motivos**: 
  - Monitoramento e visualização de métricas em tempo real.
  - Suporte a alertas e dashboards customizáveis.
  - **Versão Atual Prometheus**: 2.29.1
  - **Versão Atual Grafana**: 8.1.2

### OAuth2 e JWT
- **Motivos**: 
  - Autenticação e autorização seguras.
  - Amplamente adotados e suportados por diversas bibliotecas e frameworks.
  - **Versão Atual OAuth2**: 2.0
  - **Versão Atual JWT**: 8.0.0

### Azure API Management
- **Motivos**: 
  - Gerenciamento e proteção de APIs.
  - Suporte a políticas de segurança, rate limiting e análise de tráfego.
  - **Versão Atual**: 2021-08-01

### RabbitMQ
- **Motivos**: 
  - Mensageria assíncrona eficiente e confiável.
  - Suporte a diversos padrões de troca de mensagens e integração com múltiplas linguagens.
  - **Versão Atual**: 3.8.19

## 3. Registro de Decisões

### Decisão 1: Utilização de Microsserviços
- **Data**: 06/03/2025
- **Motivo**: Garantir escalabilidade, resiliência e flexibilidade no desenvolvimento.
- **Trade-offs**: Aumento da complexidade operacional e comunicação entre serviços.

### Decisão 2: Adoção de .NET Core 9.0
- **Data**: 06/03/2025
- **Motivo**: Alta performance, suporte multiplataforma e comunidade ativa.
- **Trade-offs**: Necessidade de familiarização com a nova versão para a equipe.

### Decisão 3: Uso de Docker e Kubernetes
- **Data**: 06/03/2025
- **Motivo**: Facilitar a containerização e orquestração dos serviços.
- **Trade-offs**: Curva de aprendizado para configuração e gerenciamento.

### Decisão 4: Implementação de Redis para Cache
- **Data**: 06/03/2025
- **Motivo**: Melhorar a performance e reduzir a carga nos serviços.
- **Trade-offs**: Necessidade de gerenciamento adicional do sistema de cache.

### Decisão 5: Utilização de Polly para Resiliência
- **Data**: 06/03/2025
- **Motivo**: Implementar padrões de resiliência como retries e circuit breakers.
- **Trade-offs**: Complexidade adicional na configuração e monitoramento.

### Decisão 6: Adoção de Prometheus e Grafana para Monitoramento
- **Data**: 06/03/2025
- **Motivo**: Monitoramento em tempo real e visualização de métricas.
- **Trade-offs**: Necessidade de configuração e manutenção dos dashboards.

### Decisão 7: Uso de OAuth2 e JWT para Segurança
- **Data**: 06/03/2025
- **Motivo**: Garantir autenticação e autorização seguras.
- **Trade-offs**: Complexidade na implementação e gerenciamento de tokens.

### Decisão 8: Implementação de Azure API Management
- **Data**: 06/03/2025
- **Motivo**: Gerenciamento e proteção de APIs.
- **Trade-offs**: Custo adicional do serviço gerenciado.

### Decisão 9: Utilização de RabbitMQ para Mensageria
- **Data**: 06/03/2025
- **Motivo**: Mensageria assíncrona eficiente e confiável.
- **Trade-offs**: Necessidade de gerenciamento adicional do sistema de mensageria.