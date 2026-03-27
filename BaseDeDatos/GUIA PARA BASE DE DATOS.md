# Guia de ejecucion SQL en nueva PC

Esta guia define el orden recomendado para levantar la base de datos del proyecto en otra computadora.

## Prerrequisitos

- Tener SQL Server instalado (instancia local, por ejemplo `localhost`, `localhost\SQLEXPRESS` o LocalDB).
- Ejecutar en SSMS con un usuario con permisos para crear base/login/usuario.
- Verificar en la aplicacion que `ConnectionStrings:DefaultConnection` apunte a la misma instancia.

## Orden de ejecucion recomendado

### 1) Crear base y permisos iniciales

Ejecutar:

- `BaseDeDatos/00_CrearBaseYPermisos.sql`

Este script crea la base `Piscicultura` (si no existe) y configura acceso para el usuario Windows indicado en el script.

> Importante: cambia el valor de `@LoginWindows` dentro del script para que coincida con el usuario de la nueva PC.

---

### 2) Crear tablas principales (Sprint 1 y Sprint 2, esquema de catalogos y crianza)

Ejecutar:

- `BaseDeDatos/Tablas/Consulta.sql`

Este script crea tablas como `Rol`, `Usuario`, `Especie`, `Estanque`, `Proveedor`, `Lote`, `ParametroAgua`, `Mortalidad`, `Muestreo`, `Tratamiento`, `TrasladoLote`, `RegistroBiomasa`, etc., e inserta datos base de `Rol` y `FaseCrianza`.

---

### 3) Crear tablas/modelos en plural + procedimientos CRUD usados por la app

Ejecutar:

- `BaseDeDatos/Procedimientos/Procedimientos_CRUD_Modelos.sql`

Este script crea (si no existen) tablas alineadas a modelos C# (`Lotes`, `ParametrosAgua`, `InventarioItems`, `RegistrosAlimentacion`) y los stored procedures (`sp_*`) consumidos por servicios del sistema.

---

### 4) Ajustar tabla de alertas para el esquema actual de la app

El modulo actual usa `dbo.Alerta` con llaves foraneas a tablas en plural (`Lotes`, `ParametrosAgua`, `InventarioItems`).

Si ya ejecutaste `Consulta.sql`, es posible que `dbo.Alerta` se haya creado con llaves al esquema singular. Para evitar conflicto, ejecuta en este orden:

1. `USE [Piscicultura];`
2. `DROP TABLE IF EXISTS dbo.Alerta;`
3. Ejecutar `BaseDeDatos/03_CrearTablaAlerta_Modelos.sql`

---

### 5) Crear usuario administrador inicial (login en la app)

Ejecutar:

- `BaseDeDatos/02_CrearUsuarioAdmin_PBKDF2.sql`

Este script crea/actualiza el usuario `admin` con password hasheado (PBKDF2), rol administrador y cuenta activa.

---

### 6) Cargar datos de ejemplo para usar el sistema de inmediato (opcional pero recomendado)

Ejecutar:

- `BaseDeDatos/04_DatosEjemplo_Iniciales.sql`

Este script carga datos semilla (idempotentes) para no empezar desde cero:

- especies, estanques y proveedores de ejemplo
- lotes de ejemplo
- inventario/alimentacion de ejemplo
- parametros de agua y eventos de crianza de ejemplo
- alertas de ejemplo
- usuarios demo (`tecnico`, `supervisor`) para pruebas

## Script de diagnostico (solo si falla conexion/login)

Si aparece error de inicio de sesion o acceso a BD, ejecutar:

- `BaseDeDatos/01_VerificarYRepararAcceso.sql`

Sirve para diagnosticar instancia, login, usuario en BD, mapeo a `dbo` y reparar permisos cuando aplique.

## Checklist final rapido

1. La base `Piscicultura` existe.
2. Existen tablas: `dbo.Usuario`, `dbo.Rol`, `dbo.Lotes`, `dbo.ParametrosAgua`, `dbo.InventarioItems`, `dbo.RegistrosAlimentacion`, `dbo.Alerta`.
3. Existen procedimientos `sp_Lote_*`, `sp_ParametroAgua_*`, `sp_InventarioItem_*`, `sp_RegistroAlimentacion_*`.
4. Usuario `admin` existe y puede iniciar sesion.
5. (Si ejecutaste el paso 6) existen datos de ejemplo en lotes, inventario, parametros y crianza.
6. La cadena de conexion en `appsettings.json` coincide con la instancia SQL de la nueva PC.
