# Requerimientos del Sistema de Gestión de Piscicultura

## Agrupación de Requerimientos Funcionales (Según Documentación Adicional)

Los requerimientos funcionales principales se agrupan en 5 categorías principales:

1. **RF1 - Gestión de Lotes y Especies**: Permite el registro y seguimiento de lotes de alevines, vinculándolos a proveedores certificados y especies específicas (Trucha, Carpa, Tilapia).

2. **RF2 - Gestión de Estanques e Inventarios**: Administra la disponibilidad física de los estanques y el stock de alimento concentrado, generando alertas automáticas de reabastecimiento.

3. **RF3 - Gestión de Monitoreo y Alimentación**: Registra diariamente los parámetros físico-químicos del agua y la cantidad de alimento suministrado para optimizar el crecimiento biológico.

4. **RF4 - Gestión de Biometrías y Cosecha**: Realiza el seguimiento de peso y talla para programar la cosecha final, clasificando el producto por categorías comerciales de tamaño y peso.

5. **RF5 - Gestión de Seguridad y Reportes**: Administra el acceso de usuarios por roles y genera informes de rendimiento que faciliten la toma de decisiones estratégicas.

---

## Requerimientos Funcionales Detallados

### RF-01: Gestión de Lotes de Producción

**Descripción**: El sistema debe permitir el registro y gestión completa de lotes de producción acuícola.

**Funcionalidades**:
- Crear, editar, consultar y eliminar lotes
- Asignar código único por estanque
- Registrar especie, fecha de siembra, cantidad de alevines
- Asociar proveedor y certificaciones sanitarias
- Control de procedencia de alevines
- Búsqueda y filtrado de lotes

**Prioridad**: Alta

---

### RF-02: Gestión del Proceso de Crianza (4 Fases)

**Descripción**: El sistema debe gestionar el proceso completo de crianza desde alevinaje hasta engorde final.

**Funcionalidades**:

#### Fase 1 - Alevinaje (0-3 meses)
- Registrar siembra en estanques pequeños (50m³)
- Control de capacidad: 5,000 alevines por estanque
- Registro de alimentación 6 veces al día
- Control de temperatura (18-22°C) y oxígeno disuelto

#### Fase 2 - Juveniles (3-8 meses)
- Registrar traslado a estanques medianos (200m³)
- Control de capacidad: 2,000 peces por estanque
- Registro de alimentación 4 veces al día
- Inicio de clasificación por tamaños

#### Fase 3 - Pre-engorde (8-14 meses)
- Registrar traslado a estanques grandes (500m³)
- Control de capacidad: 800 peces por estanque
- Registro de alimentación 3 veces al día
- Monitoreo semanal de peso promedio

#### Fase 4 - Engorde final (14-18 meses)
- Permanencia en estanques de engorde
- Alimentación controlada según curva de crecimiento
- Preparación para cosecha

**Registros al finalizar cada fase**:
- Cantidad de peces que avanzan
- Cantidad de peces perdidos por mortalidad (con causas)
- Peso promedio del lote y dispersión
- Condiciones del agua durante la fase

**Prioridad**: Alta

---

### RF-03: Control de Parámetros del Agua

**Descripción**: El sistema debe permitir el registro diario de parámetros físico-químicos del agua.

**Funcionalidades**:
- Registro diario de parámetros:
  - Temperatura
  - pH
  - Oxígeno disuelto
  - Amonio
  - Nitritos
  - Turbidez
- Registro de condiciones climáticas
- Historial de parámetros por estanque
- Alertas de parámetros fuera de rango óptimo
- Gráficos de tendencias temporales

**Prioridad**: Alta

---

### RF-04: Gestión de Tratamientos

**Descripción**: El sistema debe registrar la aplicación de tratamientos a los estanques y lotes.

**Funcionalidades**:
- Registro de tratamientos:
  - Antibióticos
  - Probióticos
  - Desinfectantes
- Asociación a estanque o lote específico
- Registro de dosis, fecha y personal responsable
- Historial de tratamientos
- Programación de tratamientos futuros

**Prioridad**: Media

---

### RF-05: Control de Alimentación Diaria

**Descripción**: El sistema debe registrar y controlar la alimentación diaria de los peces.

**Funcionalidades**:
- Registro de cantidad de alimento por estanque por día
- Selección de tipo de alimento según fase de crecimiento
- Registro de horarios de alimentación
- Asignación de personal responsable
- Observaciones del comportamiento alimenticio
- Control de stock de alimentos en bodega
- Alertas de stock bajo
- Cálculo de conversión alimenticia

**Prioridad**: Alta

---

### RF-06: Gestión de Inventario de Peces

**Descripción**: El sistema debe mantener un inventario actualizado de todos los peces en producción.

**Funcionalidades**:
- Control de cantidad de peces por lote y estanque
- Cálculo de biomasa total por estanque (kg de peces por m³)
- Registro de muestreos semanales de peso y talla
- Control de conversión alimenticia (kg alimento / kg pez producido)
- Alertas de sobrepoblación
- Vista consolidada por especie

**Prioridad**: Alta

---

### RF-07: Clasificación y Control de Calidad

**Descripción**: El sistema debe permitir la clasificación de peces según múltiples criterios.

**Funcionalidades**:

#### Por Calidad
- Clase A: Peces perfectos, sin deformidades, coloración óptima
- Clase B: Peces comerciales con características aceptables
- Clase C: Peces con defectos menores, precio reducido

#### Por Peso/Talla
- Pequeño (P): 150-250g
- Mediano (M): 250-350g
- Grande (G): 350-450g
- Extra Grande (XG): Más de 450g

#### Por Especie
- Trucha arcoíris: Mercado premium
- Carpa común: Mercado popular
- Tilapia: Mercado intermedio

**Prioridad**: Alta

---

### RF-08: Control de Cosecha y Distribución

**Descripción**: El sistema debe registrar y controlar el proceso de cosecha.

**Funcionalidades**:
- Registro de cosechas por lote (fecha, cantidad, peso total)
- Clasificación de peces cosechados según criterios de calidad
- Control de peces procesados vs. peces vendidos vivos
- Registro de personal responsable
- Asignación de destino de venta:
  - Mercado mayorista
  - Restaurantes
  - Supermercados
- Registro de temperaturas durante transporte
- Validación de totales (procesados + vendidos vivos = cosechados)

**Prioridad**: Alta

---

### RF-09: Registro de Mortalidad

**Descripción**: El sistema debe registrar todas las pérdidas de peces con sus causas.

**Funcionalidades**:
- Registro de cantidad de peces perdidos
- Especificación de causa de mortalidad:
  - Enfermedad
  - Estrés
  - Depredadores
  - Contaminación
  - Otras causas
- Asociación a lote y fase específica
- Historial de mortalidad
- Gráficos de mortalidad por causa
- Alertas de mortalidad masiva

**Prioridad**: Alta

---

### RF-10: Gestión de Estanques

**Descripción**: El sistema debe gestionar la información de todos los estanques.

**Funcionalidades**:
- CRUD completo de estanques
- Clasificación por tipo:
  - Pequeños (50m³)
  - Medianos (200m³)
  - Grandes (500m³)
- Control de capacidad máxima
- Estado del estanque (activo, inactivo, mantenimiento)
- Asignación de lotes a estanques
- Validación de capacidad disponible

**Prioridad**: Media

---

### RF-11: Gestión de Especies

**Descripción**: El sistema debe gestionar la información de las especies cultivadas.

**Funcionalidades**:
- CRUD completo de especies
- Configuración de parámetros óptimos por especie:
  - Temperatura
  - pH
  - Oxígeno
  - Otros parámetros
- Rangos de peso comercial
- Tiempos estimados por fase
- Alimentación recomendada por fase

**Prioridad**: Media

---

### RF-12: Gestión de Proveedores

**Descripción**: El sistema debe gestionar la información de proveedores de alevines.

**Funcionalidades**:
- CRUD completo de proveedores
- Registro de certificaciones sanitarias
- Historial de compras por proveedor
- Evaluación de calidad de proveedores
- Contactos y datos de proveedores

**Prioridad**: Media

---

### RF-13: Reportes y Análisis

**Descripción**: El sistema debe generar reportes de productividad y análisis.

**Funcionalidades**:
- Reportes de productividad estacional
- Reportes por especie
- Reportes por estanque
- Análisis de conversión alimenticia
- Análisis de mortalidad
- Tendencias de crecimiento
- Exportación de reportes (PDF, Excel)
- Gráficos y visualizaciones

**Prioridad**: Media

---

### RF-14: Control de Ciclos Reproductivos

**Descripción**: El sistema debe registrar ciclos reproductivos para especies que se reproducen en la piscifactoría.

**Funcionalidades**:
- Registro de eventos reproductivos
- Control de reproductores
- Seguimiento de desoves
- Registro de alevines producidos internamente

**Prioridad**: Baja

---

### RF-15: Registro de Incidentes Ambientales

**Descripción**: El sistema debe registrar incidentes que afecten la producción.

**Funcionalidades**:
- Registro de mortalidad masiva
- Registro de contaminación
- Registro de depredadores
- Registro de otros incidentes
- Asociación a lotes afectados
- Medidas correctivas aplicadas

**Prioridad**: Media

---

## Requerimientos No Funcionales

### RNF-01: Rendimiento

**Descripción**: El sistema debe responder en tiempos aceptables.

**Especificaciones**:
- Tiempo de carga de páginas: < 2 segundos
- Consultas a base de datos: < 1 segundo
- El procesamiento de registros de alimentación y parámetros de agua debe realizarse de forma **asíncrona**, asegurando tiempos de respuesta inferiores a 2 segundos
- Soporte para al menos 100 usuarios concurrentes
- Paginación en listados grandes (> 100 registros)

**Prioridad**: Alta

---

### RNF-02: Usabilidad

**Descripción**: El sistema debe ser intuitivo y fácil de usar.

**Especificaciones**:
- Interfaz consistente en todas las páginas
- Navegación clara y lógica
- Mensajes de error claros y útiles
- Validación en tiempo real
- Ayuda contextual disponible

**Prioridad**: Alta

---

### RNF-03: Seguridad

**Descripción**: El sistema debe proteger la información y el acceso.

**Especificaciones**:
- El sistema debe implementar un esquema de **autenticación y autorización basado en roles** para asegurar que solo personal autorizado gestione datos sensibles de producción
- Validación de datos en cliente y servidor
- Protección contra inyección SQL
- Encriptación de datos sensibles
- Logs de auditoría

**Prioridad**: Alta

---

### RNF-04: Disponibilidad

**Descripción**: El sistema debe estar disponible cuando se necesite.

**Especificaciones**:
- Al tratarse de un entorno de monitoreo biológico, la base de datos en SQL Server y los servicios deben garantizar una **disponibilidad del 99.5%** para evitar vacíos de información
- Tiempo de recuperación ante fallos: < 4 horas
- Backups automáticos diarios
- Recuperación ante desastres

**Prioridad**: Alta

---

### RNF-05: Escalabilidad

**Descripción**: El sistema debe poder crecer según las necesidades.

**Especificaciones**:
- La arquitectura debe permitir el crecimiento del número de estanques y registros de monitoreo sin degradar el rendimiento general de la aplicación
- Soporte para crecimiento de datos
- Arquitectura modular
- Posibilidad de agregar nuevos módulos
- Optimización de consultas

**Prioridad**: Media

---

### RNF-06: Mantenibilidad

**Descripción**: El sistema debe ser fácil de mantener y actualizar.

**Especificaciones**:
- Código bien documentado
- Estructura modular
- Separación de responsabilidades
- Uso de patrones de diseño
- Componentes reutilizables

**Prioridad**: Alta

---

### RNF-07: Compatibilidad

**Descripción**: El sistema debe funcionar en diferentes entornos.

**Especificaciones**:
- Gracias al uso de **Blazor Hybrid**, el sistema debe garantizar una experiencia de usuario consistente tanto en dispositivos móviles (Android/iOS) como en sistemas de escritorio (Windows)
- Responsive design (móvil, tablet, desktop)
- Compatible con SQL Server 2022
- .NET 9

**Prioridad**: Alta

---

### RNF-08: Integridad de Datos

**Descripción**: El sistema debe garantizar la integridad de los datos.

**Especificaciones**:
- Validación de datos obligatorios
- Restricciones de integridad referencial
- Transacciones para operaciones críticas
- Validación de reglas de negocio
- Prevención de datos duplicados

**Prioridad**: Alta

---

## Reglas de Negocio

### RN-01: Capacidad de Estanques
- Estanques pequeños: máximo 5,000 alevines
- Estanques medianos: máximo 2,000 peces
- Estanques grandes: máximo 800 peces
- No se puede exceder la capacidad del estanque

### RN-02: Fases de Crecimiento
- Un lote solo puede estar en una fase a la vez
- El traslado entre fases debe registrar la cantidad exacta de peces
- No se puede retroceder de fase

### RN-03: Parámetros del Agua
- Los parámetros deben estar dentro de rangos óptimos según la especie
- Alertas automáticas cuando los parámetros están fuera de rango
- Registro diario obligatorio de parámetros críticos

### RN-04: Alimentación
- La cantidad de alimento debe ser proporcional a la fase y cantidad de peces
- No se puede registrar alimentación sin stock disponible
- La alimentación debe registrarse en los horarios establecidos

### RN-05: Cosecha
- Solo se puede cosechar cuando los peces alcanzan peso comercial (250-400g según especie)
- La suma de peces procesados y vendidos vivos debe igualar la cantidad cosechada
- La clasificación debe sumar el total de peces cosechados

### RN-06: Mortalidad
- La mortalidad debe registrarse con causa específica
- La mortalidad masiva (> 10% del lote) genera alerta automática
- La mortalidad reduce la cantidad de peces del lote

### RN-07: Conversión Alimenticia
- Se calcula como: kg de alimento / kg de pez producido
- Debe estar dentro de rangos aceptables según especie
- Valores anormales generan alertas

### RN-08: Biomasa
- No debe exceder 30 kg de peces por m³ de agua
- Alertas cuando se aproxima al límite
- Cálculo automático basado en cantidad y peso promedio

---

## Casos de Uso Principales

### CU-01: Registrar Nuevo Lote
**Actor**: Técnico Acuícola  
**Precondiciones**: Estanque disponible, proveedor registrado  
**Flujo**:
1. Acceder a módulo de Lotes
2. Crear nuevo lote
3. Ingresar datos: código, especie, fecha, cantidad, proveedor
4. Validar datos
5. Guardar lote
6. Asignar a estanque pequeño (Fase 1)

---

### CU-02: Registrar Parámetros del Agua
**Actor**: Técnico Acuícola  
**Precondiciones**: Estanque activo con lote  
**Flujo**:
1. Acceder a módulo de Parámetros del Agua
2. Seleccionar estanque
3. Ingresar valores de parámetros
4. Sistema valida rangos óptimos
5. Si hay valores fuera de rango, mostrar alerta
6. Guardar registro

---

### CU-03: Registrar Alimentación Diaria
**Actor**: Técnico Acuícola  
**Precondiciones**: Lote activo, stock de alimento disponible  
**Flujo**:
1. Acceder a módulo de Alimentación
2. Seleccionar estanque/lote
3. Ingresar tipo y cantidad de alimento
4. Seleccionar horario
5. Asignar personal responsable
6. Validar stock disponible
7. Guardar registro
8. Actualizar inventario de alimentos

---

### CU-04: Registrar Traslado entre Fases
**Actor**: Técnico Acuícola  
**Precondiciones**: Lote en fase anterior, estanque destino disponible  
**Flujo**:
1. Acceder a módulo de Crianza
2. Seleccionar lote a trasladar
3. Registrar cantidad de peces a trasladar
4. Seleccionar estanque destino
5. Validar capacidad del estanque destino
6. Registrar mortalidad si aplica
7. Registrar peso promedio
8. Confirmar traslado
9. Actualizar fase del lote

---

### CU-05: Registrar Cosecha
**Actor**: Técnico Acuícola / Supervisor  
**Precondiciones**: Lote en fase de engorde, peces con peso comercial  
**Flujo**:
1. Acceder a módulo de Cosecha
2. Seleccionar lote a cosechar
3. Registrar fecha, cantidad y peso total
4. Clasificar peces por calidad y peso
5. Asignar destino de venta
6. Registrar temperatura de transporte
7. Validar totales
8. Guardar cosecha
9. Actualizar inventario

---

## Priorización de Requerimientos

### Fase 1 (MVP - Mínimo Producto Viable)
- RF-01: Gestión de Lotes
- RF-02: Gestión del Proceso de Crianza (básico)
- RF-03: Control de Parámetros del Agua
- RF-05: Control de Alimentación Diaria
- RF-06: Gestión de Inventario (básico)
- RF-10: Gestión de Estanques
- RF-11: Gestión de Especies

### Fase 2 (Funcionalidades Esenciales)
- RF-02: Gestión del Proceso de Crianza (completo)
- RF-04: Gestión de Tratamientos
- RF-07: Clasificación y Control de Calidad
- RF-08: Control de Cosecha
- RF-09: Registro de Mortalidad
- RF-12: Gestión de Proveedores

### Fase 3 (Funcionalidades Avanzadas)
- RF-13: Reportes y Análisis
- RF-14: Control de Ciclos Reproductivos
- RF-15: Registro de Incidentes Ambientales
