# Arquitectura del Sistema - Estructura de Páginas y Componentes

## Tecnología Base

- **Lenguaje de Programación**: C# (.NET 9)
- **Framework de Frontend**: Blazor Hybrid (MAUI para soporte multiplataforma)
- **Framework de Backend**: ASP.NET Core Web API
- **Motor de Base de Datos**: SQL Server 2022
- **Entorno de Desarrollo**: Visual Studio 2022 Community

## Arquitectura del Sistema

El sistema utiliza una arquitectura híbrida que combina:

- **Aplicación Móvil/Desktop**: Blazor Hybrid con MAUI permite ejecutar la aplicación en dispositivos móviles (Android/iOS) y sistemas de escritorio (Windows), permitiendo a los técnicos capturar datos directamente en los estanques.

- **API Backend**: ASP.NET Core Web API maneja la lógica de negocio y proporciona endpoints RESTful para la sincronización de datos.

- **Base de Datos Centralizada**: SQL Server 2022 almacena toda la información de producción, permitiendo análisis y reportes en tiempo real.

- **Sincronización en Tiempo Real**: Los datos capturados en campo se sincronizan automáticamente con la base de datos centralizada, garantizando información actualizada para la toma de decisiones.

---

## Estructura de Páginas Principales

### Resumen de Páginas (15-18 páginas con @page)

| Módulo | Páginas | Descripción |
|--------|---------|-------------|
| Lotes | 4 | Index, Create, Edit, Details |
| Crianza | 5 | Index, FaseDetails, Traslado, Mortalidad, Muestreo |
| Parámetros Agua | 3 | Index, Registrar, Historial |
| Tratamientos | 3 | Index, Create, Historial |
| Alimentación | 3 | Index, Registrar, Inventario |
| Inventario | 2 | Index, Biomasa |
| Cosecha | 3 | Index, Registrar, Clasificacion |
| Reportes | 2 | Index, Productividad |
| Configuración | 3 | Estanques, Especies, Proveedores |
| General | 3 | Home/Index, Login, NotFound |

---

## Detalle de Páginas por Módulo

### 1. Módulo de Lotes de Producción

#### `Pages/Lotes/Index.razor`
- Listado de todos los lotes
- Búsqueda y filtros (por especie, estanque, fecha, proveedor)
- Vista de estado actual de cada lote
- Acciones: Ver detalles, Editar, Crear nuevo

#### `Pages/Lotes/Create.razor`
- Formulario para crear nuevo lote
- Campos: código, especie, fecha de siembra, cantidad de alevines, proveedor, certificaciones
- Validación de datos

#### `Pages/Lotes/Edit.razor`
- Formulario para editar lote existente
- Similar a Create pero con datos precargados
- Validación y confirmación de cambios

#### `Pages/Lotes/Details.razor`
- Vista detallada de un lote específico
- Información completa: historial de fases, parámetros, alimentación, cosechas
- Gráficos y estadísticas del lote

---

### 2. Módulo de Gestión de Crianza

#### `Pages/Crianza/Index.razor`
- Dashboard de todas las fases activas
- Vista por estanque y por fase
- Indicadores de estado (peces, peso promedio, días en fase)
- Alertas y notificaciones

#### `Pages/Crianza/FaseDetails.razor`
- Detalle completo de una fase específica
- Información del lote, estanque, parámetros, alimentación
- Historial de eventos en la fase
- Opción de registrar traslado o finalizar fase

#### `Pages/Crianza/Traslado.razor`
- Formulario para registrar traslado entre fases
- Selección de lote origen y destino
- Registro de cantidad de peces trasladados
- Validación de capacidad del estanque destino

#### `Pages/Crianza/Mortalidad.razor`
- Registro de mortalidad de peces
- Campos: cantidad, causa, fecha, observaciones
- Clasificación de causas (enfermedad, estrés, depredadores, etc.)
- Historial de mortalidad por lote

#### `Pages/Crianza/Muestreo.razor`
- Registro de muestreos de peso y talla
- Entrada de datos de muestra representativa
- Cálculo automático de peso promedio y dispersión
- Gráficos de crecimiento

---

### 3. Módulo de Parámetros del Agua

#### `Pages/ParametrosAgua/Index.razor`
- Listado de registros de parámetros
- Filtros por estanque, fecha, rango de valores
- Alertas de parámetros fuera de rango
- Vista de tabla y gráficos

#### `Pages/ParametrosAgua/Registrar.razor`
- Formulario diario de registro de parámetros
- Campos: temperatura, pH, oxígeno, amonio, nitritos, turbidez
- Condiciones climáticas
- Validación de rangos óptimos

#### `Pages/ParametrosAgua/Historial.razor`
- Historial completo de parámetros por estanque
- Gráficos de tendencias temporales
- Comparación entre estanques
- Exportación de datos

---

### 4. Módulo de Tratamientos

#### `Pages/Tratamientos/Index.razor`
- Listado de tratamientos aplicados
- Filtros por tipo, estanque, fecha
- Estado de tratamientos activos
- Próximas aplicaciones programadas

#### `Pages/Tratamientos/Create.razor`
- Formulario para registrar tratamiento
- Campos: tipo (antibiótico, probiótico, desinfectante), dosis, fecha, estanque/lote
- Personal responsable
- Observaciones

#### `Pages/Tratamientos/Historial.razor`
- Historial completo de tratamientos
- Por lote o por estanque
- Efectividad de tratamientos
- Reportes de uso

---

### 5. Módulo de Alimentación

#### `Pages/Alimentacion/Index.razor`
- Vista diaria de alimentación
- Registros por estanque y horario
- Resumen de consumo diario
- Alertas de alimentación pendiente

#### `Pages/Alimentacion/Registrar.razor`
- Formulario de registro de alimentación
- Campos: estanque, tipo de alimento, cantidad, horario, personal
- Observaciones de comportamiento
- Validación según fase

#### `Pages/Alimentacion/Inventario.razor`
- Control de stock de alimentos
- Tipos de alimento disponibles
- Niveles de inventario
- Alertas de stock bajo
- Historial de entradas y salidas

---

### 6. Módulo de Inventario

#### `Pages/Inventario/Index.razor`
- Vista general de inventario por estanque
- Cantidad de peces, peso total, biomasa
- Estado de cada lote
- Resumen por especie

#### `Pages/Inventario/Biomasa.razor`
- Control detallado de biomasa
- Cálculo de kg de peces por m³ de agua
- Conversión alimenticia (kg alimento / kg pez)
- Gráficos de evolución
- Alertas de sobrepoblación

---

### 7. Módulo de Cosecha

#### `Pages/Cosecha/Index.razor`
- Listado de cosechas realizadas
- Filtros por fecha, especie, lote
- Resumen de producción
- Estado de clasificación

#### `Pages/Cosecha/Registrar.razor`
- Formulario de registro de cosecha
- Campos: lote, fecha, cantidad, peso total
- Personal responsable
- Destino de venta
- Temperatura de transporte

#### `Pages/Cosecha/Clasificacion.razor`
- Clasificación de peces cosechados
- Por calidad (A/B/C), peso/talla (P/M/G/XG)
- Cantidad por categoría
- Validación de totales

---

### 8. Módulo de Reportes

#### `Pages/Reportes/Index.razor`
- Dashboard de reportes disponibles
- Acceso a diferentes tipos de reportes
- Filtros de fecha y parámetros
- Exportación de reportes

#### `Pages/Reportes/Productividad.razor`
- Reportes de productividad estacional
- Por especie, por estanque, por período
- Gráficos comparativos
- Métricas de eficiencia

---

### 9. Módulo de Configuración

#### `Pages/Configuracion/Estanques.razor`
- Gestión de estanques
- CRUD completo de estanques
- Configuración de capacidad y tipo
- Estado de estanques (activo/inactivo/mantenimiento)

#### `Pages/Configuracion/Especies.razor`
- Gestión de especies
- CRUD completo de especies
- Configuración de parámetros óptimos por especie
- Rangos de peso comercial

#### `Pages/Configuracion/Proveedores.razor`
- Gestión de proveedores
- CRUD completo de proveedores
- Certificaciones sanitarias
- Historial de compras

---

### 10. Páginas Generales

#### `Pages/Home/Index.razor`
- Dashboard principal del sistema
- Resumen general: lotes activos, alertas, estadísticas
- Gráficos de producción
- Accesos rápidos a módulos principales

#### `Pages/Home/Login.razor`
- Página de autenticación (si aplica)
- Login de usuarios
- Recuperación de contraseña

#### `Pages/Shared/NotFound.razor`
- Página 404
- Manejo de rutas no encontradas

---

## Componentes Reutilizables (20-25 componentes)

### Componentes de Formularios

#### `Components/Forms/EstanqueSelector.razor`
- Selector dropdown de estanques
- Filtro por tipo de estanque
- Muestra capacidad disponible

#### `Components/Forms/EspecieSelector.razor`
- Selector dropdown de especies
- Muestra información de la especie seleccionada

#### `Components/Forms/LoteSelector.razor`
- Selector de lotes
- Filtro por especie, estanque, estado
- Búsqueda avanzada

#### `Components/Forms/ProveedorSelector.razor`
- Selector de proveedores
- Muestra certificaciones

#### `Components/Forms/FechaRangePicker.razor`
- Selector de rango de fechas
- Validación de fechas
- Formato consistente

#### `Components/Forms/ParametrosAguaForm.razor`
- Formulario completo de parámetros del agua
- Validación de rangos
- Indicadores visuales de valores óptimos

#### `Components/Forms/AlimentacionForm.razor`
- Formulario de registro de alimentación
- Cálculo automático según fase
- Validación de cantidades

#### `Components/Forms/ClasificacionForm.razor`
- Formulario de clasificación de peces
- Múltiples criterios (calidad, peso, especie)
- Validación de totales

---

### Componentes de Visualización

#### `Components/Display/LoteCard.razor`
- Tarjeta visual de un lote
- Información resumida: especie, fase, cantidad, estanque
- Estado visual (colores según fase)
- Acciones rápidas

#### `Components/Display/EstanqueInfo.razor`
- Panel de información de estanque
- Capacidad, ocupación, parámetros actuales
- Estado del estanque

#### `Components/Display/FaseBadge.razor`
- Badge visual de la fase actual
- Color según fase
- Días en fase

#### `Components/Display/ParametrosTable.razor`
- Tabla de parámetros del agua
- Indicadores de valores óptimos
- Formato condicional (verde/amarillo/rojo)

#### `Components/Display/MortalidadChart.razor`
- Gráfico de mortalidad
- Por causa, por fecha, por lote
- Tendencias temporales

#### `Components/Display/BiomasaChart.razor`
- Gráfico de biomasa
- Evolución temporal
- Comparación entre estanques

#### `Components/Display/CalidadBadge.razor`
- Badge de calidad (A/B/C)
- Color según clase
- Tooltip con descripción

---

### Componentes de Tablas/Listados

#### `Components/Tables/DataTable.razor`
- Tabla genérica reutilizable
- Paginación, ordenamiento, filtros
- Acciones personalizables
- Responsive

#### `Components/Tables/LotesTable.razor`
- Tabla específica de lotes
- Columnas: código, especie, fase, estanque, cantidad, fecha
- Acciones: ver, editar, detalles

#### `Components/Tables/CosechasTable.razor`
- Tabla de cosechas
- Columnas: fecha, lote, cantidad, peso, clasificación
- Filtros y exportación

#### `Components/Tables/TratamientosTable.razor`
- Tabla de tratamientos
- Columnas: fecha, tipo, estanque, personal, estado
- Filtros por tipo y fecha

#### `Components/Tables/AlimentacionTable.razor`
- Tabla de alimentación
- Columnas: fecha, estanque, tipo, cantidad, horario
- Resumen diario

---

### Componentes de Navegación

#### `Components/Navigation/Sidebar.razor`
- Menú lateral de navegación
- Agrupación por módulos
- Indicadores de notificaciones
- Responsive (colapsable)

#### `Components/Navigation/Breadcrumb.razor`
- Migas de pan
- Navegación jerárquica
- Enlaces a páginas padre

---

### Componentes de Validación/Feedback

#### `Components/Validation/ValidationSummary.razor`
- Resumen de errores de validación
- Lista de mensajes
- Estilo consistente

#### `Components/Feedback/ToastNotification.razor`
- Notificaciones toast
- Tipos: éxito, error, advertencia, información
- Auto-dismiss configurable

#### `Components/Feedback/LoadingSpinner.razor`
- Indicador de carga
- Overlay o inline
- Mensaje personalizable

---

## Layouts y Estructuras (3-4 archivos)

### `Shared/MainLayout.razor`
- Layout principal de la aplicación
- Estructura: Header, Sidebar, Main Content, Footer
- Responsive design
- Manejo de autenticación

### `Shared/NavMenu.razor`
- Menú de navegación principal
- Enlaces a todas las secciones
- Agrupación lógica
- Estado activo

### `Shared/Footer.razor` (Opcional)
- Pie de página
- Información de la empresa
- Enlaces adicionales

### `App.razor`
- Componente raíz de la aplicación
- Configuración de routing
- Manejo de errores globales

---

## Estructura de Carpetas Completa

```
Proyecto/
├── Pages/
│   ├── Home/
│   │   └── Index.razor
│   ├── Lotes/
│   │   ├── Index.razor
│   │   ├── Create.razor
│   │   ├── Edit.razor
│   │   └── Details.razor
│   ├── Crianza/
│   │   ├── Index.razor
│   │   ├── FaseDetails.razor
│   │   ├── Traslado.razor
│   │   ├── Mortalidad.razor
│   │   └── Muestreo.razor
│   ├── ParametrosAgua/
│   │   ├── Index.razor
│   │   ├── Registrar.razor
│   │   └── Historial.razor
│   ├── Tratamientos/
│   │   ├── Index.razor
│   │   ├── Create.razor
│   │   └── Historial.razor
│   ├── Alimentacion/
│   │   ├── Index.razor
│   │   ├── Registrar.razor
│   │   └── Inventario.razor
│   ├── Inventario/
│   │   ├── Index.razor
│   │   └── Biomasa.razor
│   ├── Cosecha/
│   │   ├── Index.razor
│   │   ├── Registrar.razor
│   │   └── Clasificacion.razor
│   ├── Reportes/
│   │   ├── Index.razor
│   │   └── Productividad.razor
│   ├── Configuracion/
│   │   ├── Estanques.razor
│   │   ├── Especies.razor
│   │   └── Proveedores.razor
│   └── Shared/
│       └── NotFound.razor
│
├── Components/
│   ├── Forms/
│   │   ├── EstanqueSelector.razor
│   │   ├── EspecieSelector.razor
│   │   ├── LoteSelector.razor
│   │   ├── ProveedorSelector.razor
│   │   ├── FechaRangePicker.razor
│   │   ├── ParametrosAguaForm.razor
│   │   ├── AlimentacionForm.razor
│   │   └── ClasificacionForm.razor
│   ├── Display/
│   │   ├── LoteCard.razor
│   │   ├── EstanqueInfo.razor
│   │   ├── FaseBadge.razor
│   │   ├── ParametrosTable.razor
│   │   ├── MortalidadChart.razor
│   │   ├── BiomasaChart.razor
│   │   └── CalidadBadge.razor
│   ├── Tables/
│   │   ├── DataTable.razor
│   │   ├── LotesTable.razor
│   │   ├── CosechasTable.razor
│   │   ├── TratamientosTable.razor
│   │   └── AlimentacionTable.razor
│   ├── Navigation/
│   │   ├── Sidebar.razor
│   │   └── Breadcrumb.razor
│   └── Validation/
│       ├── ValidationSummary.razor
│       ├── ToastNotification.razor
│       └── LoadingSpinner.razor
│
├── Shared/
│   ├── MainLayout.razor
│   ├── NavMenu.razor
│   └── Footer.razor
│
├── Services/
│   ├── ILoteService.cs
│   ├── IParametrosAguaService.cs
│   ├── IAlimentacionService.cs
│   ├── ICrianzaService.cs
│   ├── ICosechaService.cs
│   ├── IReporteService.cs
│   └── IConfiguracionService.cs
│
├── Models/
│   ├── Lote.cs
│   ├── Estanque.cs
│   ├── Especie.cs
│   ├── ParametrosAgua.cs
│   ├── Tratamiento.cs
│   ├── Alimentacion.cs
│   ├── Cosecha.cs
│   └── ...
│
├── Data/
│   ├── ApplicationDbContext.cs
│   └── Migrations/
│
└── App.razor
```

---

## Resumen de Archivos .razor

| Tipo | Cantidad | Descripción |
|------|----------|-------------|
| **Páginas principales** | 15-18 | Páginas con @page para cada funcionalidad |
| **Componentes reutilizables** | 20-25 | Componentes compartidos sin @page |
| **Layouts** | 3-4 | Estructuras de página |
| **TOTAL** | **38-47 archivos .razor** | |

---

## Estrategia de Reutilización

### 1. Componentes Base Genéricos
- **DataTable.razor**: Tabla reutilizable con paginación, ordenamiento y filtros
- **FormModal.razor**: Modal genérico para formularios (si se implementa)
- **ConfirmDialog.razor**: Diálogo de confirmación reutilizable

### 2. Servicios Compartidos
- Separación de lógica de negocio en servicios
- Interfaces para facilitar testing y mantenimiento
- Inyección de dependencias

### 3. Modelos Compartidos
- Modelos de datos centralizados
- Validación de datos
- DTOs para transferencia de datos

### 4. Componentes Específicos por Módulo
- Agrupar componentes relacionados en carpetas
- Usar herencia de componentes cuando sea posible
- Implementar interfaces comunes para componentes similares

---

## Ventajas de esta Estructura

1. **Modularidad**: Cada módulo está en su propia carpeta, facilitando la navegación
2. **Reutilización**: Componentes compartidos en `Components/` evitan duplicación
3. **Mantenibilidad**: Fácil localizar y modificar funcionalidades específicas
4. **Escalabilidad**: Estructura permite agregar nuevas funcionalidades sin afectar existentes
5. **Separación de responsabilidades**: Páginas, componentes y servicios claramente separados
6. **Consistencia**: Componentes reutilizables aseguran UI/UX consistente

---

## Consideraciones de Implementación

### Performance
- Uso de componentes virtualizados para listas grandes
- Lazy loading de módulos pesados
- Caching de datos frecuentemente consultados

### Responsive Design y Multiplataforma
- Componentes adaptables a diferentes tamaños de pantalla
- Menú lateral colapsable en móviles
- Tablas con scroll horizontal en pantallas pequeñas
- Experiencia de usuario consistente en Android, iOS y Windows gracias a Blazor Hybrid
- Optimización de interfaz para captura de datos en campo (pantallas táctiles)
- Funcionalidad offline con sincronización posterior cuando haya conexión

### Accesibilidad
- Etiquetas ARIA apropiadas
- Navegación por teclado
- Contraste de colores adecuado

### Seguridad
- Validación en cliente y servidor
- Autenticación y autorización basada en roles
- Protección contra inyección SQL (Entity Framework)
- Encriptación de datos sensibles
- Logs de auditoría para operaciones críticas
- Sincronización segura entre dispositivos móviles y servidor