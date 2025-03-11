# Configuração de Logging

## Ferramentas Utilizadas
- **ELK Stack**: Conjunto de ferramentas para gerenciamento de logs, composto por Elasticsearch, Logstash e Kibana.

## Configuração do Elasticsearch
1. **Instalação**:
    - Baixe e instale o Elasticsearch a partir do site oficial.

2. **Configuração**:
    - Edite o arquivo de configuração `elasticsearch.yml` para definir as configurações básicas:
    ```yaml
    cluster.name: "fluxo-caixa-cluster"
    node.name: "node-1"
    network.host: "localhost"
    ```

3. **Execução**:
    - Inicie o Elasticsearch com o comando:
    ```bash
    ./bin/elasticsearch
    ```

## Configuração do Logstash
1. **Instalação**:
    - Baixe e instale o Logstash a partir do site oficial.

2. **Configuração**:
    - Crie um arquivo de configuração `logstash.conf` com o seguinte conteúdo:
    ```yaml
    input {
      file {
        path => "/var/log/servicos/*.log"
        start_position => "beginning"
      }
    }

    filter {
      grok {
        match => { "message" => "%{COMBINEDAPACHELOG}" }
      }
    }

    output {
      elasticsearch {
        hosts => ["localhost:9200"]
        index => "servicos-logs-%{+YYYY.MM.dd}"
      }
    }
    ```

3. **Execução**:
    - Inicie o Logstash com o comando:
    ```bash
    ./bin/logstash -f logstash.conf
    ```

## Configuração do Kibana
1. **Instalação**:
    - Baixe e instale o Kibana a partir do site oficial.

2. **Configuração**:
    - Edite o arquivo de configuração `kibana.yml` para definir as configurações básicas:
    ```yaml
    server.port: 5601
    server.host: "localhost"
    elasticsearch.hosts: ["http://localhost:9200"]
    ```

3. **Execução**:
    - Inicie o Kibana com o comando:
    ```bash
    ./bin/kibana
    ```

4. **Criação de Dashboards**:
    - Acesse o painel do Kibana em `http://localhost:5601`.
    - Vá para **Management** > **Index Patterns** > **Create index pattern**.
    - Crie um padrão de índice para visualizar os logs coletados pelo Logstash.

## Referências
- Documentação do Elasticsearch
- Documentação do Logstash
- Documentação do Kibana