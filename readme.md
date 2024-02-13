# Desafio Autorización de Pagos

Este repositorio contiene una aplicación que consta de dos APIs en .NET 6: ProcesadorDePagos y AutorizacionDePagos.

## Instrucciones de Ejecución

1. Tener Docker instalado.
2. Desde la carpeta raiz del proyecto donde se encuentra el docker-compose.yml. Ejecuta el siguiente comando

    ```bash
    docker-compose build
    ```

5. Inicia los contenedores Docker con el siguiente comando:

    ```bash
    docker-compose up
    ```
    o
    ```bash
    docker-compose up -d
    ```

## Pruebas de la API 

Está disponible en el archivo challenge.collection.json en este repositorio. Puedes importar este archivo en Postman para probar las diferentes rutas y funcionalidades de las APIs.

## Stack

- .NET 6
- Microsoft SQL Server
- RabbitMQ