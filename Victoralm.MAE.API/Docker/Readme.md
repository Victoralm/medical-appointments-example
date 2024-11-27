# Docker

## Notes

-   Remember that the Dockerfile only has access to files and folders within its root directory and below. Files and folders above the root cannot be accessed. This is the main reason for using Docker Compose.
-   Building the container based on the Dockerfile: In the terminal, within the folder where the Dockerfile is located (root):
    ```bash
    docker build .
    ```

## DockerCompose

-   Run the Docker Compose file - note the `-f` flag, which allows you to specify the file name:

    ```bash
    docker-compose -f <nome_do_arquivo> up
    # Like in:
    docker-compose -f mae_development.yml up
    # To rebuild at code change (Recheck)
    docker-compose -f mae_development.yml up -w
    # Forcing a new build
    docker-compose -f mae_development.yml up --build
    ```

-   Swagger urls:
    -   For the current example: http://localhost:32790/swagger/index.html, https://localhost:32791/swagger/index.html
