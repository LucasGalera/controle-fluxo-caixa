# Configuração de Monitoramento

## Ferramentas Utilizadas
- **Prometheus**: Sistema de monitoramento e alerta.
- **Grafana**: Plataforma de análise e visualização de métricas.

## Configuração do Prometheus
1. **Instalação**:
    - Baixe e instale o Prometheus a partir do site oficial.

2. **Configuração**:
    - Crie um arquivo de configuração `prometheus.yml` com o seguinte conteúdo:
    ```yaml
    global:
      scrape_interval: 15s

    scrape_configs:
      - job_name: 'servicos'
        static_configs:
          - targets: ['localhost:9090', 'localhost:9091']
    ```

3. **Execução**:
    - Inicie o Prometheus com o comando:
    ```bash
    ./prometheus --config.file=prometheus.yml
    ```

## Configuração do Grafana
1. **Instalação**:
    - Baixe e instale o Grafana a partir do site oficial.

2. **Configuração**:
    - Inicie o Grafana com o comando:
    ```bash
    ./bin/grafana-server
    ```

3. **Adicionando o Prometheus como Fonte de Dados**:
    - Acesse o painel do Grafana em `http://localhost:3000`.
    - Vá para **Configuration** > **Data Sources** > **Add data source**.
    - Selecione **Prometheus** e configure a URL como `http://localhost:9090`.

4. **Criação de Dashboards**:
    - Crie dashboards personalizados para visualizar as métricas coletadas pelo Prometheus.

## Monitoramento de Serviços
- **Exportadores**: Utilize exportadores para coletar métricas específicas dos serviços. Por exemplo, o `node_exporter` para métricas de sistema e o `dotnet_exporter` para métricas de aplicações .NET.
- **Alertas**: Configure alertas no Prometheus para notificar sobre problemas críticos. Adicione regras de alerta no arquivo `prometheus.yml`:
    ```yaml
    rule_files:
      - "alert.rules"

    alerting:
      alertmanagers:
        - static_configs:
            - targets: ['localhost:9093']
    ```

## Referências
- Documentação do Prometheus
- Documentação do Grafana