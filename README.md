# Gestion-de-inventario
# Gestion-de-inventario

Aplicación de control de inventario construida con **.NET MAUI** y **C#**, siguiendo una arquitectura por capas con enfoque **Clean Architecture + CQRS**.

## Tecnologías

- .NET 8
- .NET MAUI
- C#
- MediatR
- Entity Framework Core
- SQLite
- FluentValidation

## Estructura del proyecto

```text
InventoryControl/
├── InventoryControl.sln
├── InventoryControl.Domain/
├── InventoryControl.Application/
├── InventoryControl.Infrastructure/
└── InventoryControl.UI/
```

### Descripción de capas

- **Domain**: entidades, reglas de negocio, interfaces y objetos de valor.
- **Application**: comandos, consultas, DTOs y validaciones.
- **Infrastructure**: persistencia con EF Core y configuración de SQLite.
- **UI**: interfaz .NET MAUI, páginas XAML y viewmodels.

## Requisitos previos

Antes de ejecutar el proyecto, asegúrate de tener instalado:

- Visual Studio 2022.
- .NET 8 SDK.
- Carga de trabajo de **.NET MAUI** en Visual Studio.
- Git.

## Instalación en Visual Studio 2022

1. Abre **Visual Studio Installer**.
2. Elige **Modificar** sobre tu instalación de Visual Studio 2022.
3. Marca la carga de trabajo **.NET Multi-platform App UI development**.
4. Instala los componentes opcionales sugeridos por Visual Studio.
5. Espera a que finalice la instalación.

## Clonar el repositorio

```bash
git clone https://github.com/Mxtsi7/Gestion-de-inventario.git
cd Gestion-de-inventario
```

## Restaurar dependencias

Ve a la carpeta de la solución:

```bash
cd InventoryControl
```

Restaura los paquetes NuGet:

```bash
dotnet restore
```

## Compilar la solución

```bash
dotnet build
```

## Abrir en Visual Studio

1. Entra a la carpeta `InventoryControl`.
2. Abre el archivo `InventoryControl.sln`.
3. Espera a que Visual Studio restaure los paquetes automáticamente.
4. Selecciona el proyecto de inicio: `InventoryControl.UI`.
5. Ejecuta la aplicación.

## Ejecución desde línea de comandos

Si ya tienes MAUI correctamente instalado:

```bash
cd InventoryControl
dotnet build
dotnet run --project InventoryControl.UI
```

## Base de datos

La aplicación utiliza **SQLite** y crea el archivo de base de datos localmente al iniciar por primera vez.

## Paquetes principales

- MediatR
- FluentValidation.DependencyInjectionExtensions
- Microsoft.EntityFrameworkCore.Sqlite
- CommunityToolkit.Mvvm
- Microsoft.Extensions.DependencyInjection

## Problemas comunes

### 1. `MSBUILD : error MSB1003`
Significa que estás ejecutando `dotnet build` fuera de la carpeta que contiene el archivo `.sln` o `.csproj`.

Solución:

```bash
cd InventoryControl
dotnet build
```

### 2. `No se encuentra ningún proyecto`
Eso ocurre cuando estás parado en una carpeta que no contiene un archivo `.csproj`.

Verifica que existan estos archivos:

- `InventoryControl/InventoryControl.sln`
- `InventoryControl/InventoryControl.Domain/InventoryControl.Domain.csproj`
- `InventoryControl/InventoryControl.Application/InventoryControl.Application.csproj`
- `InventoryControl/InventoryControl.Infrastructure/InventoryControl.Infrastructure.csproj`
- `InventoryControl/InventoryControl.UI/InventoryControl.UI.csproj`

### 3. MAUI no compila o faltan workloads
Abre Visual Studio Installer y confirma que la carga de trabajo de .NET MAUI esté instalada.

## Flujo general

1. La UI envía comandos o consultas.
2. MediatR redirige al handler correspondiente.
3. Application ejecuta el caso de uso.
4. Infrastructure persiste datos con EF Core y SQLite.
5. Domain mantiene las reglas del negocio.

## Estado actual

El repositorio ya incluye la base de la solución, los proyectos por capa y los archivos principales necesarios para continuar con el desarrollo.

## Autores

Proyecto académico / empresarial para gestión de inventario.