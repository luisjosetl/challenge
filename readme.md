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

### Casos de prueba
- **Ruta:** http://localhost:32774/api/authorization
    - **Método:** POST
    - **Descripcion:** Autorización aprobada
    - **Cuerpo de la solicitud (JSON):**
```json
{
    "authorizationRequest": {
        "monto": "500",
        "codigoOperacion": "CLIENTE_PRIMERO_APROBADO",
        "tipoAutorizacion": "COBRO",
        "clienteId": "094532e6-be77-4d5a-93c8-3d54131d4337"
    }
}
```
- **Ruta:** http://localhost:32774/api/authorization
    - **Método:** POST
    - **Descripcion:** Autorización denegada
    - **Cuerpo de la solicitud (JSON):**
```json
{
  "authorizationRequest": {
    "monto": "500.78",
    "codigoOperacion": "CLIENTE_PRIMERO_DENEGADO",
    "tipoAutorizacion": "COBRO",
    "clienteId": "094532e6-be77-4d5a-93c8-3d54131d4337"
  }
}
```
- **Ruta:** http://localhost:32774/api/authorization
    - **Método:** POST
    - **Descripcion:** Autorización segundo cliente. Caso reversa
    - **Cuerpo de la solicitud (JSON):**
```json
{
  "authorizationRequest": {
    "monto": "90000",
    "codigoOperacion": "CLIENTE_SEGUNDO_1",
    "tipoAutorizacion": "COBRO",
    "clienteId": "585ac58c-35fe-4f19-9b0d-99fad357940a"
  }
}
```
- **Ruta:** http://localhost:32774/api/authorization/approved-authorizations
    - **Método:** GET
    - **Descripcion:** Autorizaciones aprobadas

- **Ruta:** http://localhost:32774/api/authorization/reverse-authorizations
    - **Método:** GET
    - **Descripcion:** Autorizaciones en reversa

- **Ruta:** http://localhost:32774/api/authorization
    - **Método:** GET
    - **Descripcion:** Autorizaciones Lista

## Stack

- .NET 6
- Microsoft SQL Server
- RabbitMQ