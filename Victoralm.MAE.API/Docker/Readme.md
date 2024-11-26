# Docker

## IPCAs

-   Lembrar que o Dockerfile só enxerga arquivos e pastas da raiz onde está contido em diante. Arquivos e pastas acima da raiz não poderão ser acessados. Esse é o principal motivo para utilizar-se um DockerCompose.
-   Build do container baseado no Dockerfile: No terminal, já na pasta onde o Dockerfile está contido (raiz):
    ```bash
    docker build .
    ```

## DockerCompose

-   Executar o arquivo dockercompose - notar o `-f` que permite especificar o nome do arquivo:

    ```bash
    docker-compose -f <nome_do_arquivo> up
    # Como em:
    docker-compose -f mae_development.yml up
    # Para fazer rebuild a cada alteração no código (Reverificar)
    docker-compose -f mae_development.yml up -w
    # Para forçar um novo build
    docker-compose -f mae_development.yml up --build
    ```

-   Swagger urls:
    -   Para o exemplo em questão: http://localhost:32790/swagger/index.html, https://localhost:32791/swagger/index.html
