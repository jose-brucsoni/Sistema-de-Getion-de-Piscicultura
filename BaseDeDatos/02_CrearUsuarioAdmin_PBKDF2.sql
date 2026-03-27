/*
  Crear/actualizar usuario administrador con hash PBKDF2 compatible con la app.

  Compatibilidad:
  - Tabla: dbo.Usuario
  - Tabla: dbo.Rol
  - Formato hash esperado por la app:
      PBKDF2$<iteraciones>$<salt_base64>$<key_base64>

  Importante:
  - Este script NO calcula PBKDF2 en SQL Server.
  - Debes pegar un hash ya generado (PowerShell/C#) en @PasswordHash.
*/

USE [Piscicultura];
GO

DECLARE @Username       NVARCHAR(50)  = N'admin';
DECLARE @Correo         NVARCHAR(120) = N'admin@piscicultura.local';
DECLARE @PasswordHash   NVARCHAR(255) = N'PBKDF2$100000$que8FVXvP37DWsDFRllCJg==$wKSZ239EZFq5sfkJdhQSuQypAwP2kexZOh4o7eol0J0='; -- contraseña temporal: Admin@2026!
DECLARE @RolAdminNombre NVARCHAR(50)  = N'Administrador';
DECLARE @IdRolAdmin     INT;

IF @PasswordHash NOT LIKE N'PBKDF2$%$%$%'
BEGIN
    RAISERROR(N'@PasswordHash no tiene el formato PBKDF2 esperado.', 16, 1);
    RETURN;
END

SELECT TOP (1) @IdRolAdmin = r.IdRol
FROM dbo.Rol r
WHERE r.Nombre = @RolAdminNombre;

IF @IdRolAdmin IS NULL
BEGIN
    INSERT INTO dbo.Rol (Nombre, Descripcion, Activo)
    VALUES (N'Administrador', N'Acceso completo al sistema', 1);

    SET @IdRolAdmin = SCOPE_IDENTITY();
END

IF EXISTS (SELECT 1 FROM dbo.Usuario WHERE Username = @Username)
BEGIN
    UPDATE dbo.Usuario
    SET IdRol = @IdRolAdmin,
        Correo = @Correo,
        PasswordHash = @PasswordHash,
        Activo = 1
    WHERE Username = @Username;
END
ELSE
BEGIN
    INSERT INTO dbo.Usuario (IdRol, Username, Correo, PasswordHash, Activo, CreatedAt)
    VALUES (@IdRolAdmin, @Username, @Correo, @PasswordHash, 1, SYSUTCDATETIME());
END

SELECT
    u.IdUsuario,
    u.Username,
    u.Correo,
    r.Nombre AS Rol,
    u.Activo,
    u.CreatedAt
FROM dbo.Usuario u
INNER JOIN dbo.Rol r ON r.IdRol = u.IdRol
WHERE u.Username = @Username;

PRINT N'Usuario administrador listo. Inicia sesión y cambia la contraseña temporal.';
GO

/*
  Generar nuevo hash PBKDF2 en PowerShell (mismo formato de la app):

  $password = 'TuPasswordFuerteAqui!';
  $iterations = 100000;
  $salt = New-Object byte[] 16;
  $rng = [System.Security.Cryptography.RandomNumberGenerator]::Create();
  $rng.GetBytes($salt);
  $pbkdf2 = New-Object System.Security.Cryptography.Rfc2898DeriveBytes(
      $password, $salt, $iterations, [System.Security.Cryptography.HashAlgorithmName]::SHA256);
  $key = $pbkdf2.GetBytes(32);
  "PBKDF2$" + $iterations + "$" + [Convert]::ToBase64String($salt) + "$" + [Convert]::ToBase64String($key)
*/
