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

---

## ⚠️ Requisitos previos obligatorios

Antes de ejecutar el proyecto debes tener instalado **todo lo siguiente** en orden:

### 1. .NET 8 SDK
Descarga e instala el SDK de .NET 8 desde:
> https://dotnet.microsoft.com/en-us/download/dotnet/8.0

Verifica la instalación con:
```bash
dotnet --version
```
Debe mostrar una versión `8.x.x`.

### 2. Carga de trabajo de .NET MAUI para Windows
Ejecuta este comando en la terminal **como administrador**:
```bash
dotnet workload install maui-windows
```

Verifica que esté instalado con:
```bash
dotnet workload list
```
Debe aparecer `maui-windows` en la lista.

### 3. Windows App Runtime 1.4 (obligatorio para ejecutar la app)
La aplicación requiere el **Windows App Runtime versión 1.4** para poder ejecutarse en Windows.

Descarga e instala el runtime (ejecutar **como administrador**):
> https://aka.ms/windowsappsdk/1.4/latest/windowsappruntimeinstall-x64.exe

⚠️ **Importante:** Si el instalador se ejecuta sin permisos de administrador, salteará la instalación del paquete principal y la app no podrá abrirse. Siempre ejecutar con **clic derecho → Ejecutar como administrador**.

### 4. Git
Descarga e instala Git desde:
> https://git-scm.com/downloads

### 5. Editor (elige uno)

#### Opción A — Visual Studio Code (recomendado para este proyecto)
Instala VS Code desde:
> https://code.visualstudio.com/

Luego instala estas extensiones dentro de VS Code:
- **C# Dev Kit** (Microsoft)
- **.NET MAUI** (Microsoft)

#### Opción B — Visual Studio 2022
Descarga Visual Studio 2022 desde:
> https://visualstudio.microsoft.com/

Durante la instalación marca la carga de trabajo: **.NET Multi-platform App UI development**.

---

## Instalación y ejecución

### 1. Clonar el repositorio

```bash
git clone https://github.com/Mxtsi7/Gestion-de-inventario.git
cd Gestion-de-inventario/InventoryControl
```

### 2. Restaurar dependencias

```bash
dotnet restore
```

### 3. Compilar la solución

```bash
dotnet build
```

Debe mostrar: `Compilación correcta` sin errores.

### 4. Ejecutar la aplicación

#### Desde Visual Studio Code
1. Abre la carpeta `Gestion-de-inventario` en VS Code.
2. En el panel lateral busca **Solution Explorer**.
3. Expande la solución y haz clic derecho en `InventoryControl.UI`.
4. Selecciona **Set as Startup Project**.
5. Clic derecho nuevamente → **Start Without Debugging**.

#### Desde la terminal
```bash
dotnet run --project InventoryControl.UI --framework net8.0-windows10.0.19041.0
```

#### Desde Visual Studio 2022
1. Abre `InventoryControl/InventoryControl.sln`.
2. Selecciona `InventoryControl.UI` como proyecto de inicio.
3. Elige **Windows Machine** como destino.
4. Presiona **F5**.

---

## Base de datos

La aplicación usa **SQLite** y crea el archivo de base de datos automáticamente al iniciar por primera vez. No se requiere ninguna configuración adicional.

---

## Paquetes principales

| Paquete | Versión |
|---|---|
| Microsoft.Maui.Controls | 8.0.14 |
| MediatR | 12.2.0 |
| FluentValidation.DependencyInjectionExtensions | útima |
| Microsoft.EntityFrameworkCore.Sqlite | útima |
| CommunityToolkit.Mvvm | 8.2.2 |
| Microsoft.Extensions.DependencyInjection | 8.0.1 |

---

## Problemas comunes

### Error: `This application requires the Windows App Runtime Version 1.4`
Falta instalar el runtime de Windows App SDK.

**Solución:** Descarga y ejecuta **como administrador**:
> https://aka.ms/windowsappsdk/1.4/latest/windowsappruntimeinstall-x64.exe

### Error: `Clase no registrada (0x80040154 REGDB_E_CLASSNOTREG)`
El runtime de Windows App SDK no está instalado o fue instalado sin permisos de administrador.

**Solución:** Reinstala el Windows App Runtime ejecutando el instalador como administrador (ver paso 3 de Requisitos).

### Error: `XA5300 No se encontró el directorio Android SDK`
El proyecto está configurado solo para Windows. Este error no aplica.

### Error: `MSBUILD : error MSB1003`
Estás ejecutando `dotnet build` fuera de la carpeta correcta.

**Solución:**
```bash
cd InventoryControl
dotnet build
```

### Workload de MAUI no encontrado
Ejecuta en la terminal como administrador:
```bash
dotnet workload install maui-windows
```

---

## Flujo general de la arquitectura

1. La UI envía comandos o consultas vía MediatR.
2. MediatR redirige al handler correspondiente en Application.
3. Application ejecuta el caso de uso y valida con FluentValidation.
4. Infrastructure persiste datos con EF Core y SQLite.
5. Domain mantiene las reglas del negocio puras, sin dependencias externas.

---

## Autores

Proyecto académico / empresarial para gestión de inventario.
