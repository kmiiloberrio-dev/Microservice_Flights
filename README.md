# Microservice_Flights
NewShore Prueba dev -  Proyecto de microservicios para consulta de vuelos  en Net Core 3.1

Se desarrollo el sistema en microservicios para el sistema de vuelos que permite la escalabilidad y modular los recursos sean necesarion según el microservicio.

El sistema cuenta con :
1. Un microservicio que se encarga de realizar la gestion de los vuelos.
2. Un gateway encargado de a canalización y balanceo de las peticiones realizdas al microservicio.
3. Una Página web para uso de los usuarios.

Iniciar el sistema

Para iniciar el sistema los pasos a seguir son :

1. Adjunto script de base de datos
2. Cambiar la cadena conexión en el microservicio
1. Iniciar el Microservicio, gateway y front-end los tres al mismo tiempo


Desarrollo del sistema:

1. Microservicio
    
   - Clean Arquitecture
   - CQRS se encarga de la segregación de comandos y querys
   - EntityFramework Core que se encarga toda la gestion a la base de datos
   - Inyección de dependencia propia de net core
   - Manejo de log propio de net core
   - Middleware de log que se encarga de capturar todo error automatizacon los try - catch
   - Code First
   - Cors
   - Swagguer para las pruebas a nivel de interfaz
   - Fluent Api para las validaciones de parametros a nivel de microservicio
   - Prueba unitaria xunit
  
2. Gateway

   - Inyección de dependencia propia de net core
   - Manejo de log propio de net core
   - Middleware de log que se encarga de capturar todo error automatizacon los try - catch
   - Cors
   - Swagguer para las pruebas a nivel de interfaz
   - Refactorización http client con Net core para consumo de apis
   
3. Common (implementa metodos que son reutilizados por los otros componentes)

   - Paginado
   - Manejador de excepciones (try - catch)

4. Front-End

   - Asp. net core 3.1
   - Tag helpers
   - Bootstrap
   - DataTable
   - Jquery
   - Ajax
   
5. Base de datos
  
   - SQLServer
   
