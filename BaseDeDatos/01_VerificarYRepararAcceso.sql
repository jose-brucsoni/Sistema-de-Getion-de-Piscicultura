/*
  VERIFICACIÓN (opción 2) + reparación del error 4060 / inicio de sesión en Piscicultura

  En SSMS conéctate a la MISMA instancia que en appsettings (Server=...).

  NOTA IMPORTANTE: Si tu login de Windows es el PROPIETARIO de la base, en SQL Server
  aparece como usuario "dbo", no con tu nombre. Eso es normal y da acceso total.
  En ese caso NO hace falta CREATE USER ni agregar a db_owner (y fallan con error 15063 / 15151).

  A) Ejecuta el bloque "1) DIAGNÓSTICO".
  B) Ejecuta el bloque "2) REPARACIÓN" solo si el diagnóstico lo indica.
*/

/* ========== 1) DIAGNÓSTICO ========== */
SET NOCOUNT ON;

DECLARE @LoginWindows SYSNAME = N'DESKTOP-9GET9J9\Joseca';

PRINT N'--- Instancia (debe coincidir con la de la aplicación) ---';
SELECT @@SERVERNAME AS Instancia, @@SERVICENAME AS Servicio;

PRINT N'--- Base Piscicultura ---';
SELECT name, state_desc, user_access_desc, SUSER_SNAME(owner_sid) AS PropietarioDeLaBase
FROM sys.databases
WHERE name = N'Piscicultura';

PRINT N'--- LOGIN en el servidor ---';
SELECT name, type_desc
FROM sys.server_principals
WHERE name = @LoginWindows;

IF DB_ID(N'Piscicultura') IS NOT NULL
BEGIN
    PRINT N'--- Huérfanos en Piscicultura ---';
    USE [Piscicultura];
    EXEC sp_change_users_login @Action = N'Report';

    PRINT N'--- Cómo está mapeado tu login DENTRO de la base (si ves dbo = eres dueño; es correcto) ---';
    SELECT sp.name AS LoginServidor,
           dp.name AS UsuarioEnEstaBase,
           dp.type_desc
    FROM sys.server_principals sp
    INNER JOIN sys.database_principals dp ON dp.sid = sp.sid
    WHERE sp.name = @LoginWindows;

    PRINT N'--- Búsqueda por nombre (solo si NO eres dbo aparecerá tu nombre) ---';
    SELECT dp.name AS UsuarioEnBase, dp.type_desc
    FROM sys.database_principals dp
    WHERE dp.name = @LoginWindows;

    PRINT N'--- Roles donde participa el usuario con tu nombre (vacío si eres solo dbo) ---';
    SELECT r.name AS Rol, m.name AS Miembro
    FROM sys.database_role_members drm
    INNER JOIN sys.database_principals r ON drm.role_principal_id = r.principal_id
    INNER JOIN sys.database_principals m ON drm.member_principal_id = m.principal_id
    WHERE m.name = @LoginWindows;
END
ELSE
BEGIN
    PRINT N'ERROR: No existe la base Piscicultura en esta instancia.';
END
GO

/* ========== 2) REPARACIÓN (sysadmin) ==========
   Si en el diagnóstico "UsuarioEnEstaBase" ya es dbo, NO ejecutes este bloque:
   los permisos ya son máximos. El error de la app será por otra causa (instancia distinta, etc.).
*/
USE master;
GO

DECLARE @LoginWindows SYSNAME = N'DESKTOP-9GET9J9\Joseca';

IF DB_ID(N'Piscicultura') IS NULL
BEGIN
    RAISERROR(N'No existe la base Piscicultura en esta instancia.', 16, 1);
    RETURN;
END

IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = @LoginWindows)
BEGIN
    DECLARE @sqlLogin NVARCHAR(600) = N'CREATE LOGIN ' + QUOTENAME(@LoginWindows) + N' FROM WINDOWS';
    EXEC sp_executesql @sqlLogin;
    PRINT N'LOGIN creado.';
END
GO

USE [Piscicultura];
GO

DECLARE @LoginWindows SYSNAME = N'DESKTOP-9GET9J9\Joseca';

DECLARE @sid VARBINARY(85) = (SELECT sid FROM sys.server_principals WHERE name = @LoginWindows);

IF @sid IS NULL
BEGIN
    RAISERROR(N'No existe el LOGIN en el servidor. Revisa @LoginWindows.', 16, 1);
    RETURN;
END

DECLARE @nombreUsuarioEnBase SYSNAME =
(
    SELECT dp.name
    FROM sys.database_principals dp
    WHERE dp.sid = @sid
);

IF @nombreUsuarioEnBase = N'dbo'
BEGIN
    PRINT N'OK: Tu login ya está mapeado como dbo (propietario). No se crea usuario duplicado ni se toca db_owner.';
    PRINT N'Si la aplicación sigue fallando, revisa que appsettings use el mismo Server= que esta instancia (@@SERVERNAME arriba).';
    RETURN;
END

IF @nombreUsuarioEnBase IS NULL
BEGIN
    DECLARE @sqlUser NVARCHAR(600) = N'CREATE USER ' + QUOTENAME(@LoginWindows) + N' FOR LOGIN ' + QUOTENAME(@LoginWindows);
    BEGIN TRY
        EXEC sp_executesql @sqlUser;
        PRINT N'USER creado en Piscicultura.';
    END TRY
    BEGIN CATCH
        PRINT N'Error en CREATE USER: ' + ERROR_MESSAGE();
        THROW;
    END CATCH
END
ELSE IF @nombreUsuarioEnBase <> N'dbo'
BEGIN
    DECLARE @sqlFix NVARCHAR(600) = N'ALTER USER ' + QUOTENAME(@nombreUsuarioEnBase) + N' WITH LOGIN = ' + QUOTENAME(@LoginWindows);
    BEGIN TRY
        EXEC sp_executesql @sqlFix;
        PRINT N'Usuario re-enlazado al login.';
    END TRY
    BEGIN CATCH
        PRINT N'ALTER USER: ' + ERROR_MESSAGE();
    END CATCH
END
GO

USE [Piscicultura];
GO

DECLARE @LoginWindows SYSNAME = N'DESKTOP-9GET9J9\Joseca';
DECLARE @sid2 VARBINARY(85) = (SELECT sid FROM sys.server_principals WHERE name = @LoginWindows);

IF (SELECT dp.name FROM sys.database_principals dp WHERE dp.sid = @sid2) = N'dbo'
BEGIN
    PRINT N'(Omitido ADD MEMBER: ya eres dbo.)';
END
ELSE IF NOT EXISTS (
    SELECT 1
    FROM sys.database_role_members drm
    INNER JOIN sys.database_principals r ON drm.role_principal_id = r.principal_id
    INNER JOIN sys.database_principals m ON drm.member_principal_id = m.principal_id
    WHERE r.name = N'db_owner' AND m.sid = @sid2)
BEGIN
    DECLARE @sqlRole NVARCHAR(600) =
        N'ALTER ROLE db_owner ADD MEMBER ' + QUOTENAME(
            (SELECT dp.name FROM sys.database_principals dp WHERE dp.sid = @sid2));
    BEGIN TRY
        EXEC sp_executesql @sqlRole;
        PRINT N'Usuario agregado a db_owner.';
    END TRY
    BEGIN CATCH
        PRINT N'Error en ALTER ROLE: ' + ERROR_MESSAGE();
    END CATCH
END
GO

PRINT N'Fin del bloque de reparación.';
GO
