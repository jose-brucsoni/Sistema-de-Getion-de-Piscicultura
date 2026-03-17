1. Autenticación y seguridad básica
HU-019 – Login

Pantalla de inicio de sesión funcional (usuario/contraseña).
Validación de credenciales contra BD.
Redirección al dashboard / página principal tras login.
Manejo de sesión (mantener sesión, logout, redirección al login si expira).
HU-018 – Gestión de usuarios y roles (puede ser simplificada a 1 rol en el MVP, pero está incluida en Sprint 1):

Tablas Usuarios y Roles en BD.
CRUD básico de usuarios (crear, editar, desactivar).
Asignación de rol al usuario (Admin/Técnico/Supervisor o, en simple, solo Admin).
Aplicación de autorización por rol a los módulos sensibles (al menos a nivel de menús / acceso).
2. Configuración base del sistema
HU-003 – Estanques (CRUD)

Modelo/entidad Estanque.
Tablas y migraciones correspondientes.
API/servicio para crear, listar, editar, desactivar estanques (tipos: pequeño, mediano, grande, con capacidad).
Páginas Blazor para listado y formulario de estanques.
HU-004 – Especies (CRUD)

Modelo Especie con parámetros óptimos de agua y rangos de peso comercial.
API/servicio + páginas para crear/editar/listar especies.
HU-005 – Proveedores (CRUD)

Modelo Proveedor con certificaciones sanitarias.
API/servicio + páginas para crear/editar/listar proveedores.
HU-006 – Tipos de alimento e inventario en bodega

Modelo Alimento + inventario (stock).
API/servicio para CRUD de alimentos y actualización de stock.
Página para ver tipos de alimento disponibles y niveles de inventario (con alerta de stock bajo).
3. Gestión de lotes (núcleo del negocio)
HU-001 – Registrar nuevo lote de alevines

Modelo Lote relacionado con Especie, Proveedor y Estanque.
Lógica para generar código único (LOT-YYYY-XXX) si no se proporciona.
Validación de capacidad del estanque según tipo (5.000/2.000/800).
Formulario Blazor para registrar lote con todos los campos y validaciones.
HU-002 – Listar, consultar y editar lotes

API para listar, obtener y actualizar lotes.
Página de listado con filtros (especie, estanque, fecha, estado).
Página de detalle/edición de lote.
4. Parámetros del agua (monitoreo ambiental)
HU-009 – Registrar parámetros del agua diariamente
Modelo ParametrosAgua vinculado a Estanque y Lote.
Formulario para registrar temperatura, pH, oxígeno, amonio, nitritos, turbidez + condiciones climáticas.
Validación de rangos según especie del lote.
Generación de alertas visuales cuando un valor esté fuera de rango.
Vista de historial por estanque (tabla básica con paginación).
5. Alimentación diaria e inventario asociado
HU-011 – Registrar alimentación diaria por estanque

Modelo Alimentacion vinculado a Lote/Estanque y Alimento.
Lógica para validar frecuencia según fase (6/4/3 veces al día).
Validación de stock disponible y actualización automática del inventario.
Formulario para registrar tipo de alimento, cantidad, horario y responsable.
HU-012 – Resumen de alimentación e inventario

Vista/resumen diario de alimentación por estanque (cantidad total suministrada).
Vista/resumen de inventario actual de alimentos (lo puedes integrar en la misma pantalla de HU-011/HU-006).
6. Estructura técnica y calidad
Arquitectura en capas montada (según ARQUITECTURA.md y diseño):

Capas: Presentación (Blazor), Aplicación (services/DTOs), Dominio (entidades), Infraestructura (ApplicationDbContext, repositorios).
ApplicationDbContext con DbSets para Lote, Estanque, Especie, Proveedor, Alimento, ParametrosAgua, Alimentacion, Usuario, Rol.
Migraciones aplicadas y BD SQL Server lista para trabajar.
Mínimo dashboard / página principal tras login

Puede ser una versión básica del futuro dashboard, pero al menos una página de bienvenida autenticada con enlaces a: Lotes, Estanques, Especies, Proveedores, Alimentos, Parámetros del agua, Alimentación.
Documentación y pruebas de aceptación

Tablas de HU-001, HU-009, HU-011 (y otras de Sprint 1) con criterios de aceptación validados en la práctica.
Evidencias básicas (capturas) para las pruebas de aceptación clave.