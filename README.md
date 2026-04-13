# Gestión de Inventario

Aplicación de control de inventario desarrollada con **.NET MAUI** y **C#**, siguiendo los principios de **Clean Architecture + CQRS**.

---

## Tecnologías Utilizadas

- **.NET 8**
- **.NET MAUI**
- **C#**
- **MediatR** (para CQRS)
- **Entity Framework Core**
- **SQLite**
- **FluentValidation**
- **CommunityToolkit.Mvvm**

---

## Estructura del Proyecto


### Descripción de las capas

| Capa                | Responsabilidad |
|---------------------|-----------------|
| **Domain**          | Entidades, objetos de valor, reglas de negocio e interfaces |
| **Application**     | Casos de uso (comandos y consultas), DTOs y validaciones |
| **Infrastructure**  | Acceso a datos, repositorios y configuración de base de datos |
| **UI**              | Interfaz gráfica con .NET MAUI, páginas y ViewModels |

---

## ⚠️ Requisitos Previos (Obligatorios)

Debes instalar todo lo siguiente **en orden** antes de ejecutar el proyecto:

### 1. .NET 8 SDK
Descarga e instala desde: ser una version 8.x.x para que funcione
[https://dotnet.microsoft.com/en-us/download/dotnet/8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) 


"debes instalar las extensiones de c# dev kit y .net maui para poder correr la aplicacion"

Verifica la instalación:
```bash
dotnet --version

dotnet workload install maui-windows

dotnet workload list

git clone https://github.com/Mxtsi7/Gestion-de-inventario.git
cd Gestion-de-inventario/InventoryControl

#paso 4
dotnet restore

#paso 3

dotnet build

#paso 4

"Desde Visual Studio Code:

Abre la carpeta Gestion-de-inventario en VS Code
En el Solution Explorer, haz clic derecho en InventoryControl.UI
Selecciona Set as Startup Project
Clic derecho → Start Without Debugging"