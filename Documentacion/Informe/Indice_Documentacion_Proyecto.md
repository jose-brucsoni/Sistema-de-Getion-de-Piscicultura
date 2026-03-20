# DOCUMENTACIÓN DEL PROYECTO
## Sistema de Gestión Integral de Piscicultura "Lagos Andinos" SRL

---

## ÍNDICE

**CAPÍTULO I: ASPECTOS GENERALES**  
1.1 Título del proyecto  
1.2 Introducción  
1.3 Planteamiento del problema  
1.4 Delimitaciones  
1.4.1. Delimitación espacial o geográfica  
1.4.2. Delimitación temporal  
1.4.3. Delimitación tecnológica  
1.5 Objetivo General  
1.6 Objetivos Específicos  

**CAPÍTULO II: MARCO TEÓRICO** *(NO REALIZAR)*

**CAPÍTULO III: INGENIERÍA DEL PROYECTO**  
3.1 Fase de exploración  
3.1.1 Requerimientos No funcionales  
3.1.2 Requerimientos Funcionales  
3.1.3 Diagrama de clases conceptuales (Lógica del Negocio)  
3.2 Fase de planificación de la entrega (Release Plan)  
3.2.1 Priorización de las Historias de Usuario  
3.2.2 Estimación de Esfuerzo por puntos  
3.3 Fase de iteraciones  
3.3.1 Sprint Backlog  
3.3.2. Burn down Chart  
3.3.3. Prueba de aceptación  
3.4 Fase de producción  
3.4.1 Diagrama de clases de diseño  
3.4.2. Normalización de la base de datos  
3.4.3. Procedimientos almacenados  
3.4.4. Modelo de datos relacional  
3.4.4.1. Modelo de base de datos lógico y físico  
3.5 Fase de mantenimiento  
3.5.1 Plan de Backup de base de datos  
3.6 Fase de muerte del proyecto  
3.6.1 Rendimiento del sistema  
3.6.2 Confiabilidad del sistema  

**CAPÍTULO V: RECOMENDACIONES**

**CAPÍTULO VI: BIBLIOGRAFÍA**

---

# CAPÍTULO I: ASPECTOS GENERALES

## 1.1 Título del proyecto

**Desarrollo de un Sistema de Gestión Integral de Piscicultura mediante Blazor Hybrid y SQL Server para la empresa "Lagos Andinos" SRL.**

---

## 1.2 Introducción

La acuicultura en la región andina ha pasado de ser una actividad artesanal a una industria que exige precisión técnica para ser rentable. En este contexto, la empresa "Lagos Andinos" SRL se dedica a la crianza de trucha, carpa y tilapia, procesos que requieren un monitoreo riguroso de variables ambientales y biológicas.

La acuicultura es una actividad fundamental para satisfacer la creciente demanda de proteína de origen acuático. El proceso de crianza de peces requiere un control meticuloso de las condiciones del agua, alimentación balanceada y monitoreo constante para obtener peces de calidad comercial optimizando la inversión del productor.

El presente proyecto propone el desarrollo de una solución tecnológica moderna utilizando el stack de Microsoft. La implementación de una aplicación Blazor Hybrid permitirá a los técnicos capturar datos en tiempo real desde dispositivos móviles en los estanques, sincronizándolos con una base de datos centralizada en SQL Server. Esto garantiza que la información sobre alimentación, calidad del agua y crecimiento de los lotes esté disponible para la toma de decisiones estratégicas de manera inmediata.

### Proceso General de Piscicultura

El proceso en "Lagos Andinos" comienza con la adquisición de alevines (peces juveniles) de trucha, carpa y tilapia, los cuales provienen de proveedores certificados o se reproducen en la misma piscifactoría. Estos alevines son colocados en estanques especializados con agua de calidad controlada.

Durante el crecimiento, los peces son alimentados con concentrados especiales ricos en proteínas y nutrientes esenciales. La alimentación se realiza múltiples veces al día según la etapa de crecimiento y las condiciones ambientales.

Los peces son monitoreados constantemente para detectar signos de enfermedad o estrés. El agua de los estanques se analiza regularmente para mantener niveles óptimos de oxígeno, pH, temperatura y otros parámetros críticos.

Una vez que los peces alcanzan el peso comercial, son cosechados, clasificados por tamaño y peso, y preparados para su comercialización en mercados locales y restaurantes especializados.

---

## 1.3 Planteamiento del problema

Actualmente, "Lagos Andinos" SRL carece de una plataforma digital centralizada para el control de su producción. La dependencia de registros manuales y la dispersión de la información generan los siguientes conflictos operativos:

### Problemas Identificados

#### 1. Incertidumbre en la Trazabilidad
Dificultad para rastrear el ciclo de vida completo de un lote, desde su ingreso como alevín hasta su cosecha. La falta de un sistema centralizado impide conocer el historial completo de cada lote, incluyendo sus traslados entre estanques, cambios de fase, tratamientos aplicados y eventos significativos durante su crecimiento.

#### 2. Riesgo de Mortandad
La falta de alertas tempranas sobre parámetros críticos del agua (pH, Oxígeno, Temperatura) puede derivar en pérdidas económicas considerables. Sin un sistema que monitoree y alerte sobre valores fuera de rango óptimo, los técnicos no pueden reaccionar a tiempo ante condiciones adversas que pueden causar mortalidad masiva.

#### 3. Descontrol en la Alimentación
El uso ineficiente del concentrado alimenticio al no estar ajustado dinámicamente a la etapa de crecimiento y condiciones del estanque. La falta de control preciso sobre las cantidades de alimento suministrado según la fase de crecimiento, cantidad de peces y condiciones ambientales genera desperdicio y costos innecesarios.

#### 4. Información Desactualizada
El retraso entre la toma de datos en campo y su análisis en oficina impide una reacción ágil ante imprevistos biológicos. Los registros manuales requieren ser transcritos posteriormente, generando demoras que pueden ser críticas en situaciones de emergencia o cuando se requiere tomar decisiones rápidas.

---

## 1.4 Delimitaciones

### 1.4.1. Delimitación espacial o geográfica

El proyecto se desarrollará y aplicará específicamente para las instalaciones de la piscifactoría "Lagos Andinos" SRL, ubicada en la zona de influencia de la región andina (Bolivia). El sistema está diseñado para operar en las condiciones específicas de esta ubicación geográfica, considerando las características climáticas, altitud y recursos disponibles en la región andina.

### 1.4.2. Delimitación temporal

El desarrollo del sistema se llevará a cabo en un periodo de 6 meses, comprendidos entre marzo y agosto de 2026, abarcando desde el relevamiento de requisitos hasta las pruebas finales de producción. Este cronograma incluye:

- **Marzo 2026**: Relevamiento de requisitos y diseño inicial
- **Abril 2026**: Diseño detallado y arquitectura del sistema
- **Mayo 2026**: Desarrollo de módulos principales (Sprint 1 y 2)
- **Junio 2026**: Desarrollo de módulos secundarios (Sprint 3 y 4)
- **Julio 2026**: Integración, pruebas y ajustes (Sprint 5)
- **Agosto 2026**: Pruebas finales, documentación y puesta en producción

### 1.4.3. Delimitación tecnológica

El proyecto está limitado al uso de las siguientes tecnologías:

- **Lenguaje de Programación**: C# (.NET 9)
- **Framework de Frontend**: Blazor Hybrid (MAUI para soporte multiplataforma)
- **Framework de Backend**: ASP.NET Core Web API
- **Motor de Base de Datos**: SQL Server 2022
- **Entorno de Desarrollo**: Visual Studio 2022 Community

Esta delimitación tecnológica asegura la compatibilidad, mantenibilidad y aprovechamiento del ecosistema Microsoft para el desarrollo del sistema.

---

## 1.5 Objetivo General

Desarrollar e implementar un sistema de gestión de piscicultura para "Lagos Andinos" SRL utilizando Blazor Hybrid y SQL Server, que permita automatizar el control de lotes de producción, el monitoreo de parámetros de agua y la gestión de alimentación para optimizar la trazabilidad y eficiencia operativa de la empresa.

---

## 1.6 Objetivos Específicos

1. **Identificar y documentar los requerimientos funcionales y no funcionales** de los procesos de cría, monitoreo y cosecha mediante historias de usuario, asegurando una comprensión completa de las necesidades del negocio y los usuarios del sistema.

2. **Diseñar la arquitectura del sistema y el modelo de datos relacional** en SQL Server, aplicando normas de integridad y normalización para garantizar la consistencia, eficiencia y escalabilidad de la base de datos.

3. **Construir el Product Backlog priorizado y realizar la estimación de esfuerzo** utilizando técnicas de Planning Poker bajo la metodología XP + SCRUM, permitiendo una planificación efectiva del desarrollo.

4. **Codificar los módulos de gestión de lotes, parámetros ambientales y reportes** utilizando C# y Blazor Hybrid, implementando una solución funcional que cumpla con los requerimientos establecidos.

5. **Establecer un plan de mantenimiento y respaldo de datos** para garantizar la confiabilidad y seguridad de la información crítica de la piscifactoría, asegurando la continuidad operativa del sistema.

---

# CAPÍTULO II: MARCO TEÓRICO

*(Este capítulo NO se realizará según las instrucciones del proyecto)*

---

# CAPÍTULO III: INGENIERÍA DEL PROYECTO

## 3.1 Fase de exploración

En esta etapa inicial de la metodología XP (eXtreme Programming), se llevó a cabo el levantamiento de información mediante entrevistas directas con los responsables de la piscifactoría. El objetivo fue comprender el flujo biológico y operativo de la crianza de trucha, carpa y tilapia.

Se estableció la **metáfora del sistema**, visualizando el software como una **"Bitácora de Ciclo Vital"**. En este concepto, los lotes de peces fluyen a través de estaciones (estanques) donde se monitorea su salud y alimentación hasta alcanzar la madurez comercial. Esta fase permitió identificar los procesos críticos que se transformarán en Historias de Usuario para el Product Backlog.

### Metodología Utilizada

El proyecto utiliza una combinación de metodologías ágiles:

- **XP (eXtreme Programming)**: Para prácticas de desarrollo como programación en parejas, refactorización continua, pruebas unitarias y metáfora del sistema.
- **SCRUM**: Para la gestión del proyecto, con sprints, Product Backlog, Sprint Backlog y ceremonias como Daily Standup, Sprint Planning y Sprint Review.

Esta combinación permite aprovechar las fortalezas de ambas metodologías: la calidad técnica de XP y la gestión estructurada de SCRUM.

---

## 3.1.1 Requerimientos No funcionales

Los requerimientos no funcionales definen los atributos de calidad y las restricciones técnicas bajo las cuales operará el sistema de gestión:

| Categoría | Descripción |
|-----------|-------------|
| **Seguridad** | El sistema debe implementar un esquema de autenticación y autorización basado en roles para asegurar que solo personal autorizado gestione datos sensibles de producción. Debe incluir encriptación de datos sensibles, logs de auditoría para operaciones críticas y sincronización segura entre dispositivos móviles y servidor. |
| **Disponibilidad** | Al tratarse de un entorno de monitoreo biológico, la base de datos en SQL Server y los servicios deben garantizar una disponibilidad del 99.5% para evitar vacíos de información. Se implementarán backups automáticos diarios y un plan de recuperación ante desastres. |
| **Rendimiento** | El procesamiento de registros de alimentación y parámetros de agua debe realizarse de forma asíncrona, asegurando tiempos de respuesta inferiores a 2 segundos. Las consultas a base de datos deben responder en menos de 1 segundo y el sistema debe soportar al menos 100 usuarios concurrentes. |
| **Compatibilidad** | Gracias al uso de Blazor Hybrid, el sistema debe garantizar una experiencia de usuario consistente tanto en dispositivos móviles (Android/iOS) como en sistemas de escritorio (Windows). La interfaz debe ser responsive y optimizada para captura de datos en campo con pantallas táctiles. |
| **Escalabilidad** | La arquitectura debe permitir el crecimiento del número de estanques y registros de monitoreo sin degradar el rendimiento general de la aplicación. El sistema debe ser capaz de manejar el crecimiento de datos y usuarios sin requerir cambios arquitectónicos significativos. |
| **Mantenibilidad** | El código debe estar bien documentado, seguir una estructura modular, aplicar separación de responsabilidades y utilizar patrones de diseño apropiados. Los componentes deben ser reutilizables y el sistema debe facilitar futuras extensiones. |
| **Integridad de Datos** | El sistema debe garantizar la integridad de los datos mediante validación en cliente y servidor, restricciones de integridad referencial, transacciones para operaciones críticas y prevención de datos duplicados. |

---

## 3.1.2 Requerimientos Funcionales

Los requerimientos funcionales principales se agrupan en 5 categorías principales:

| ID | Requerimiento | Descripción |
|----|---------------|-------------|
| **RF1** | Gestión de Lotes y Especies | Permite el registro y seguimiento de lotes de alevines, vinculándolos a proveedores certificados y especies específicas (Trucha, Carpa, Tilapia). Incluye control de procedencia, certificaciones sanitarias y trazabilidad completa del ciclo de vida. |
| **RF2** | Gestión de Estanques e Inventarios | Administra la disponibilidad física de los estanques y el stock de alimento concentrado, generando alertas automáticas de reabastecimiento. Controla la capacidad de estanques según tipo y fase de crecimiento. |
| **RF3** | Gestión de Monitoreo y Alimentación | Registra diariamente los parámetros físico-químicos del agua y la cantidad de alimento suministrado para optimizar el crecimiento biológico. Incluye alertas automáticas cuando los parámetros están fuera de rango óptimo. |
| **RF4** | Gestión de Biometrías y Cosecha | Realiza el seguimiento de peso y talla para programar la cosecha final, clasificando el producto por categorías comerciales de tamaño y peso. Incluye control de conversión alimenticia y biomasa. |
| **RF5** | Gestión de Seguridad y Reportes | Administra el acceso de usuarios por roles y genera informes de rendimiento que faciliten la toma de decisiones estratégicas. Incluye reportes de productividad estacional y por especie. |

### Detalle de Requerimientos Funcionales

#### RF1: Gestión de Lotes y Especies

**Funcionalidades principales:**
- Crear, editar, consultar y eliminar lotes de producción
- Asignar código único por estanque
- Registrar especie, fecha de siembra, cantidad de alevines
- Asociar proveedor y certificaciones sanitarias
- Control de procedencia de alevines
- Búsqueda y filtrado de lotes por múltiples criterios
- Vista detallada del historial completo de cada lote

#### RF2: Gestión de Estanques e Inventarios

**Funcionalidades principales:**
- CRUD completo de estanques (pequeños 50m³, medianos 200m³, grandes 500m³)
- Control de capacidad máxima según tipo de estanque
- Estado del estanque (activo, inactivo, mantenimiento)
- Gestión de stock de alimentos concentrados
- Alertas automáticas de reabastecimiento de alimentos
- Control de inventario de alimentos con historial de entradas y salidas

#### RF3: Gestión de Monitoreo y Alimentación

**Funcionalidades principales:**
- Registro diario de parámetros físico-químicos (temperatura, pH, oxígeno, amonio, nitritos, turbidez)
- Registro de condiciones climáticas
- Alertas automáticas cuando parámetros están fuera de rango óptimo
- Registro de alimentación diaria por estanque
- Tipo de alimento según fase de crecimiento
- Horarios de alimentación y personal responsable
- Observaciones del comportamiento alimenticio
- Gráficos de tendencias temporales de parámetros

#### RF4: Gestión de Biometrías y Cosecha

**Funcionalidades principales:**
- Registro de muestreos semanales de peso y talla
- Cálculo automático de peso promedio y dispersión
- Control de biomasa total por estanque (kg de peces por m³)
- Control de conversión alimenticia (kg alimento / kg pez producido)
- Registro de cosechas por lote (fecha, cantidad, peso total)
- Clasificación de peces cosechados:
  - Por calidad: Clase A, B, C
  - Por peso/talla: Pequeño (P), Mediano (M), Grande (G), Extra Grande (XG)
  - Por especie: Trucha, Carpa, Tilapia
- Control de peces procesados vs. vendidos vivos
- Registro de temperaturas durante transporte

#### RF5: Gestión de Seguridad y Reportes

**Funcionalidades principales:**
- Autenticación y autorización basada en roles
- Gestión de usuarios y permisos
- Logs de auditoría de operaciones críticas
- Generación de reportes de productividad:
  - Reportes estacionales
  - Reportes por especie
  - Reportes por estanque
  - Análisis de conversión alimenticia
  - Análisis de mortalidad
  - Tendencias de crecimiento
- Exportación de reportes (PDF, Excel)
- Gráficos y visualizaciones interactivas

---

## 3.1.3 Diagrama de clases conceptuales (Lógica del Negocio)

El diagrama de clases conceptual representa las entidades principales del dominio del negocio y sus relaciones, sin considerar aspectos de implementación técnica. Este modelo refleja la "Bitácora de Ciclo Vital" donde los lotes fluyen a través de estaciones (estanques).

### Descripción del Diagrama Conceptual

El diagrama conceptual incluye las siguientes entidades principales:

1. **Lote**: Representa un grupo de peces que inician su ciclo de vida juntos. Cada lote tiene un código único, está asociado a una especie, tiene una fecha de siembra y cantidad inicial de alevines.

2. **Especie**: Representa los tipos de peces cultivados (Trucha, Carpa, Tilapia). Cada especie tiene parámetros óptimos de crecimiento y rangos de peso comercial.

3. **Estanque**: Representa los contenedores físicos donde se crían los peces. Se clasifican por tamaño (pequeño, mediano, grande) y tienen una capacidad máxima según su tipo.

4. **Fase**: Representa las etapas del ciclo de vida (Alevinaje, Juveniles, Pre-engorde, Engorde final). Cada fase tiene duración, tipo de alimento y frecuencia de alimentación específica.

5. **ParámetrosAgua**: Representa los registros diarios de parámetros físico-químicos del agua. Incluye temperatura, pH, oxígeno disuelto, amonio, nitritos y turbidez.

6. **Alimentacion**: Representa los registros de alimentación diaria. Incluye tipo de alimento, cantidad, horario y personal responsable.

7. **Tratamiento**: Representa los tratamientos aplicados (antibióticos, probióticos, desinfectantes). Incluye tipo, dosis, fecha y personal responsable.

8. **Mortalidad**: Representa las pérdidas de peces con su causa específica. Se asocia a un lote y fase específica.

9. **Muestreo**: Representa los muestreos de peso y talla realizados periódicamente. Permite calcular peso promedio y dispersión.

10. **Cosecha**: Representa la recolección final de peces. Incluye fecha, cantidad, peso total y clasificación por calidad y tamaño.

11. **Proveedor**: Representa los proveedores de alevines. Incluye certificaciones sanitarias e historial de compras.

12. **Personal**: Representa los técnicos y personal responsable de las diferentes actividades. Se asocia a alimentación, tratamientos y cosechas.

13. **Alimento**: Representa los tipos de alimento disponibles. Incluye stock disponible y tipo según fase.

### Relaciones Principales

- Un **Lote** pertenece a una **Especie** y puede estar en diferentes **Estanques** a lo largo de su ciclo
- Un **Lote** pasa por múltiples **Fases** en secuencia
- Un **Estanque** puede albergar múltiples **Lotes** en diferentes momentos
- Un **Lote** tiene múltiples registros de **ParámetrosAgua**, **Alimentacion**, **Tratamiento** y **Mortalidad**
- Una **Cosecha** se realiza sobre un **Lote** específico
- Un **Proveedor** suministra alevines para múltiples **Lotes**

### Espacio para Diagrama

```
[ESPACIO PARA DIAGRAMA DE CLASES CONCEPTUAL]

Descripción del diagrama:
- El diagrama debe mostrar todas las entidades mencionadas
- Las relaciones deben indicar cardinalidad (1:1, 1:N, N:M)
- Debe incluir los atributos principales de cada entidad
- El diagrama debe reflejar la metáfora "Bitácora de Ciclo Vital"

Nota: Insertar imagen del diagrama de clases conceptual aquí
Ruta sugerida: /recursos/diagramas/diagrama_clases_conceptual.png
```

---

## 3.2 Fase de planificación de la entrega (Release Plan)

La fase de planificación de la entrega (Release Plan) es fundamental en la metodología SCRUM, ya que permite establecer una visión clara del producto a desarrollar y planificar las entregas incrementales. En esta fase se identificaron las historias de usuario, se priorizaron según el valor de negocio y se estimó el esfuerzo requerido.

### Metodología de Planificación

Se utilizó la técnica de **Planning Poker** para estimar el esfuerzo de cada historia de usuario. Esta técnica permite que el equipo de desarrollo llegue a un consenso sobre la complejidad de cada tarea, utilizando la secuencia de Fibonacci (1, 2, 3, 5, 8, 13, 21) para representar los puntos de historia.

### Estructura del Release Plan

El Release Plan se organizó en 5 sprints de 3 semanas cada uno, totalizando 15 semanas de desarrollo (aproximadamente 3.5 meses), dejando tiempo adicional para pruebas finales, documentación y despliegue.

---

### 3.2.1 Priorización de las Historias de Usuario

La priorización de las historias de usuario se realizó considerando:

1. **Valor de negocio**: Historias que aportan mayor valor operativo
2. **Dependencias técnicas**: Historias que son prerrequisito para otras
3. **Riesgo**: Historias que reducen incertidumbre técnica o de negocio
4. **Esfuerzo**: Balance entre valor y complejidad

#### Criterios de Priorización

- **Alta Prioridad (Must Have)**: Funcionalidades críticas sin las cuales el sistema no puede operar
- **Media Prioridad (Should Have)**: Funcionalidades importantes que mejoran significativamente la operación
- **Baja Prioridad (Nice to Have)**: Funcionalidades que agregan valor pero no son esenciales

#### Historias de Usuario Priorizadas

A continuación se presentan las 5 historias de usuario más importantes del sistema, organizadas según el ciclo de vida completo de un lote de peces:

**Tabla HU-001**

| Campo | Descripción |
|-------|-------------|
| **ID** | HU-001 |
| **Título** | Como técnico acuícola, quiero registrar un nuevo lote de alevines para iniciar el seguimiento de su ciclo de vida |
| **Prioridad** | Alta (Must Have) |
| **Estimación** | 8 puntos |
| **Sprint** | 1 |
| **Descripción** | Como técnico acuícola, necesito poder registrar un nuevo lote de alevines cuando llegan a la piscifactoría, asociándolo a una especie, proveedor y estanque inicial, para comenzar el seguimiento completo de su ciclo de vida. |
| **Criterios de Aceptación** | • El sistema debe permitir ingresar código único del lote<br>• Debe seleccionar una especie (Trucha, Carpa, Tilapia)<br>• Debe registrar fecha de siembra<br>• Debe ingresar cantidad inicial de alevines<br>• Debe seleccionar un proveedor de la lista disponible<br>• Debe registrar certificaciones sanitarias del proveedor<br>• Debe asignar el lote a un estanque pequeño disponible (Fase 1 - Alevinaje)<br>• El sistema debe validar que el estanque tenga capacidad disponible<br>• El sistema debe generar automáticamente el código único si no se proporciona<br>• Debe mostrar mensaje de confirmación al guardar exitosamente |
| **Notas Técnicas** | • Validar capacidad del estanque según tipo (pequeño: 5,000 alevines máximo)<br>• Generar código único automático si no se proporciona: formato "LOT-YYYY-XXX"<br>• Validar que el proveedor tenga certificaciones vigentes |

---

**Tabla HU-009**

| Campo | Descripción |
|-------|-------------|
| **ID** | HU-009 |
| **Título** | Como técnico acuícola, quiero registrar los parámetros del agua diariamente para monitorear la calidad del agua |
| **Prioridad** | Alta (Must Have) |
| **Estimación** | 8 puntos |
| **Sprint** | 1 |
| **Descripción** | Como técnico acuícola, necesito poder registrar diariamente los parámetros físico-químicos del agua de cada estanque (temperatura, pH, oxígeno, amonio, nitritos, turbidez) para monitorear la calidad del agua y detectar problemas a tiempo. |
| **Criterios de Aceptación** | • El sistema debe permitir seleccionar el estanque<br>• Debe registrar fecha y hora de la medición<br>• Debe ingresar los siguientes parámetros: Temperatura (°C), pH, Oxígeno disuelto (mg/L), Amonio (mg/L), Nitritos (mg/L), Turbidez (NTU)<br>• Debe registrar condiciones climáticas (opcional)<br>• Debe validar que los valores estén dentro de rangos aceptables<br>• Debe comparar valores con rangos óptimos según la especie del lote<br>• Debe generar alerta visual si algún parámetro está fuera de rango<br>• Debe permitir agregar observaciones<br>• Debe mostrar historial de parámetros del estanque |
| **Notas Técnicas** | • Validación de rangos según especie del lote en el estanque<br>• Sistema de alertas en tiempo real<br>• Gráficos de tendencias temporales |

---

**Tabla HU-011**

| Campo | Descripción |
|-------|-------------|
| **ID** | HU-011 |
| **Título** | Como técnico acuícola, quiero registrar la alimentación diaria de cada estanque para controlar el consumo de alimento |
| **Prioridad** | Alta (Must Have) |
| **Estimación** | 8 puntos |
| **Sprint** | 1 |
| **Descripción** | Como técnico acuícola, necesito poder registrar la alimentación diaria de cada estanque, indicando tipo de alimento, cantidad, horario y personal responsable, para controlar el consumo y optimizar la alimentación según la fase de crecimiento. |
| **Criterios de Aceptación** | • El sistema debe permitir seleccionar el estanque/lote<br>• Debe seleccionar tipo de alimento según fase: Fase 1: Micro-granulado (6 veces al día), Fase 2: Pellets de crecimiento (4 veces al día), Fase 3 y 4: Concentrado de engorde (3 veces al día)<br>• Debe registrar cantidad de alimento en kg<br>• Debe registrar horario de alimentación<br>• Debe seleccionar personal responsable<br>• Debe validar que haya stock disponible del tipo de alimento<br>• Debe actualizar automáticamente el inventario de alimentos<br>• Debe permitir agregar observaciones sobre comportamiento alimenticio<br>• Debe validar que no se exceda la frecuencia diaria según fase<br>• Debe mostrar resumen diario de alimentación por estanque |
| **Notas Técnicas** | • Validación de frecuencia según fase<br>• Actualización automática de inventario<br>• Validación de stock disponible |

---

**Tabla HU-015**

| Campo | Descripción |
|-------|-------------|
| **ID** | HU-015 |
| **Título** | Como técnico acuícola, quiero registrar traslados de lotes entre fases para documentar el progreso del ciclo de vida |
| **Prioridad** | Alta (Must Have) |
| **Estimación** | 13 puntos |
| **Sprint** | 2 |
| **Descripción** | Como técnico acuícola, necesito poder registrar el traslado de un lote de una fase a la siguiente, moviéndolo a un nuevo estanque, registrando la cantidad de peces trasladados, mortalidad durante el traslado y peso promedio, para documentar el progreso del ciclo de vida. |
| **Criterios de Aceptación** | • El sistema debe permitir seleccionar el lote a trasladar<br>• Debe mostrar la fase actual y la fase destino<br>• Debe mostrar el estanque actual y permitir seleccionar estanque destino<br>• Debe validar que el estanque destino tenga capacidad disponible<br>• Debe validar que el estanque destino sea del tipo correcto para la fase destino<br>• Debe registrar cantidad de peces trasladados<br>• Debe registrar mortalidad durante el traslado (si aplica)<br>• Debe registrar peso promedio del lote al momento del traslado<br>• Debe registrar fecha y hora del traslado<br>• Debe actualizar automáticamente: Fase del lote, Estanque del lote, Ocupación de estanques (origen y destino), Cantidad de peces del lote<br>• Debe generar registro histórico del traslado<br>• Debe validar que no se pueda retroceder de fase<br>• Debe mostrar resumen antes de confirmar el traslado |
| **Notas Técnicas** | • Validación compleja de capacidad y tipo de estanque<br>• Transacciones para asegurar consistencia de datos<br>• Actualización automática de múltiples entidades |

---

**Tabla HU-017**

| Campo | Descripción |
|-------|-------------|
| **ID** | HU-017 |
| **Título** | Como técnico acuícola, quiero registrar una cosecha para documentar la producción final de un lote |
| **Prioridad** | Alta (Must Have) |
| **Estimación** | 13 puntos |
| **Sprint** | 3 |
| **Descripción** | Como técnico acuícola, necesito poder registrar una cosecha cuando un lote alcanza el peso comercial, documentando la fecha, cantidad de peces cosechados, peso total y clasificación, para tener un registro completo de la producción. |
| **Criterios de Aceptación** | • El sistema debe permitir seleccionar el lote a cosechar<br>• Debe validar que el lote esté en fase de engorde final<br>• Debe validar que el peso promedio esté dentro del rango comercial (250-400g según especie)<br>• Debe registrar fecha y hora de la cosecha<br>• Debe registrar cantidad total de peces cosechados<br>• Debe registrar peso total en kg<br>• Debe seleccionar personal responsable de la cosecha<br>• Debe permitir clasificar peces cosechados: Por calidad (Clase A, B, C), Por peso/talla (P: 150-250g, M: 250-350g, G: 350-450g, XG: >450g)<br>• Debe validar que la suma de clasificaciones iguale la cantidad total cosechada<br>• Debe registrar destino de venta (mercado mayorista, restaurantes, supermercados)<br>• Debe registrar temperatura durante transporte<br>• Debe registrar cantidad de peces procesados vs. vendidos vivos<br>• Debe validar que procesados + vendidos vivos = total cosechados<br>• Debe actualizar automáticamente el inventario del lote (marcar como cosechado)<br>• Debe liberar el estanque para nuevo uso |
| **Notas Técnicas** | • Validaciones complejas de reglas de negocio<br>• Cálculos automáticos de clasificaciones<br>• Actualización de múltiples entidades relacionadas |

---

**HU-018: Como administrador, quiero gestionar usuarios y roles para controlar el acceso al sistema**

**Prioridad**: Alta (Must Have)  
**Estimación**: 8 puntos  
**Sprint**: 1

**Descripción**:  
Como administrador del sistema, necesito poder crear, editar y gestionar usuarios, asignándoles roles específicos (Administrador, Técnico Acuícola, Supervisor) con permisos diferenciados, para asegurar que solo personal autorizado acceda a las funcionalidades correspondientes.

**Criterios de Aceptación**:
- El sistema debe permitir crear nuevos usuarios
- Debe permitir editar información de usuarios existentes
- Debe permitir desactivar usuarios sin eliminarlos
- Debe asignar roles:
  - Administrador: Acceso completo al sistema
  - Técnico Acuícola: Registro de datos, consultas
  - Supervisor: Consultas, reportes, aprobaciones
- Debe configurar permisos por módulo según rol
- Debe registrar datos de usuario (nombre, email, teléfono)
- Debe permitir restablecer contraseñas
- Debe registrar fecha de último acceso
- Debe mostrar lista de usuarios activos e inactivos
- Debe validar que no se elimine el último administrador
- Debe registrar quién creó/modificó cada usuario

**Notas Técnicas**:
- Implementar sistema de autenticación y autorización
- Roles y permisos configurables
- Auditoría de cambios en usuarios

---

**HU-019: Como técnico acuícola, quiero autenticarme en el sistema para acceder de forma segura a mis funciones**

**Prioridad**: Alta (Must Have)  
**Estimación**: 5 puntos  
**Sprint**: 1

**Descripción**:  
Como técnico acuícola, necesito poder iniciar sesión en el sistema usando mis credenciales (usuario y contraseña) para acceder de forma segura a las funcionalidades que me corresponden según mi rol.

**Criterios de Aceptación**:
- El sistema debe mostrar pantalla de login
- Debe permitir ingresar usuario y contraseña
- Debe validar credenciales contra la base de datos
- Debe mostrar mensaje de error si las credenciales son incorrectas
- Debe redirigir al dashboard principal después de login exitoso
- Debe mantener sesión activa durante el uso
- Debe permitir cerrar sesión
- Debe redirigir al login si la sesión expira
- Debe mostrar nombre del usuario logueado
- Debe permitir recuperar contraseña (opcional)

**Notas Técnicas**:
- Implementar autenticación segura
- Manejo de sesiones
- Protección contra ataques de fuerza bruta

---

**HU-020: Como supervisor, quiero generar reportes de productividad estacional para analizar el rendimiento de la producción**

**Prioridad**: Media (Should Have)  
**Estimación**: 13 puntos  
**Sprint**: 4

**Descripción**:  
Como supervisor, necesito poder generar reportes de productividad por estación del año, especie y estanque, con análisis de conversión alimenticia, mortalidad y crecimiento, para tomar decisiones estratégicas sobre la producción.

**Criterios de Aceptación**:
- El sistema debe permitir seleccionar período (mes, trimestre, año)
- Debe permitir filtrar por especie, estanque o todos
- Debe generar reporte con:
  - Total de peces producidos
  - Peso total producido
  - Promedio de conversión alimenticia
  - Tasa de mortalidad
  - Tiempo promedio de ciclo completo
  - Biomasa promedio
- Debe mostrar comparación con períodos anteriores
- Debe incluir gráficos de tendencias
- Debe permitir exportar reporte en PDF o Excel
- Debe mostrar resumen ejecutivo
- Debe incluir análisis de eficiencia por estanque

**Notas Técnicas**:
- Generación de reportes complejos
- Cálculos agregados eficientes
- Exportación a múltiples formatos

---

**HU-021: Como supervisor, quiero generar reportes de mortalidad por causa para identificar problemas y tomar medidas preventivas**

**Prioridad**: Media (Should Have)  
**Estimación**: 8 puntos  
**Sprint**: 4

**Descripción**:  
Como supervisor, necesito poder generar reportes que muestren la mortalidad agrupada por causa, especie, fase y período, con gráficos y análisis, para identificar patrones y tomar medidas preventivas.

**Criterios de Aceptación**:
- El sistema debe permitir seleccionar rango de fechas
- Debe permitir filtrar por especie, fase o lote
- Debe agrupar mortalidad por causa
- Debe mostrar cantidad y porcentaje por causa
- Debe mostrar gráfico de barras o pie chart por causa
- Debe mostrar tendencias temporales de mortalidad
- Debe comparar mortalidad entre diferentes lotes o especies
- Debe identificar causas más frecuentes
- Debe mostrar mortalidad por fase de crecimiento
- Debe permitir exportar reporte en PDF
- Debe incluir recomendaciones basadas en patrones identificados

**Notas Técnicas**:
- Agrupaciones y agregaciones complejas
- Visualizaciones de datos
- Análisis de patrones

---

**HU-022: Como supervisor, quiero ver un dashboard con indicadores clave de producción para tener una visión general del estado**

**Prioridad**: Alta (Must Have)  
**Estimación**: 13 puntos  
**Sprint**: 3

**Descripción**:  
Como supervisor, necesito ver un dashboard principal con indicadores clave de producción (lotes activos, alertas, biomasa total, conversión alimenticia promedio) para tener una visión general del estado de la piscifactoría.

**Criterios de Aceptación**:
- El sistema debe mostrar dashboard al iniciar sesión
- Debe mostrar tarjetas con indicadores clave:
  - Total de lotes activos
  - Total de peces en producción
  - Biomasa total
  - Conversión alimenticia promedio
  - Alertas activas
  - Estanques en uso / disponibles
- Debe mostrar gráfico de lotes por fase
- Debe mostrar gráfico de producción por especie
- Debe mostrar lista de alertas recientes
- Debe mostrar próximas acciones requeridas
- Debe actualizar indicadores en tiempo real
- Debe permitir personalizar qué indicadores mostrar
- Debe ser responsive para dispositivos móviles

**Notas Técnicas**:
- Dashboard interactivo
- Actualización en tiempo real
- Componentes reutilizables

---

#### Otras Historias de Usuario del Sprint 1 (resumen)

Las siguientes HU son necesarias para el Sprint 1 y se documentan en formato resumido.

| ID | Título | Puntos | Descripción breve |
|----|--------|--------|--------------------|
| **HU-002** | Listar, consultar y editar lotes | 5 | Como técnico, quiero ver el listado de lotes con filtros (especie, estanque, fecha) y acceder a edición y detalle de cada lote. |
| **HU-003** | Gestión de estanques (CRUD) | 5 | Como administrador, quiero crear, editar, listar y desactivar estanques (tipo: pequeño 50m³, mediano 200m³, grande 500m³) y su capacidad máxima. |
| **HU-004** | Gestión de especies (CRUD) | 3 | Como administrador, quiero mantener el catálogo de especies (Trucha, Carpa, Tilapia) y sus parámetros óptimos de agua. |
| **HU-005** | Gestión de proveedores (CRUD) | 3 | Como administrador, quiero mantener proveedores de alevines y sus certificaciones sanitarias. |
| **HU-006** | Tipos de alimento e inventario en bodega | 5 | Como técnico, quiero registrar tipos de alimento (micro-granulado, pellets, concentrado) y el stock en bodega, con alertas de reabastecimiento. |
| **HU-012** | Consultar resumen e inventario de alimentación | 3 | Como técnico, quiero ver el resumen diario de alimentación por estanque y el inventario actual de alimentos. |

---

### 3.2.2 Estimación de Esfuerzo por puntos

La estimación de esfuerzo se realizó utilizando la técnica de **Planning Poker**, donde el equipo de desarrollo asignó puntos de historia a cada historia de usuario basándose en la complejidad, el esfuerzo y la incertidumbre técnica.

#### Escala de Estimación

Se utilizó la secuencia de Fibonacci modificada para la estimación:
- **1 punto**: Tarea muy simple, menos de 1 día
- **2 puntos**: Tarea simple, 1-2 días
- **3 puntos**: Tarea moderada, 2-3 días
- **5 puntos**: Tarea compleja, 3-5 días
- **8 puntos**: Tarea muy compleja, 5-8 días
- **13 puntos**: Tarea extremadamente compleja, 8-13 días
- **21 puntos**: Tarea demasiado grande, debe dividirse

#### Alcance del proyecto actual

**En este proyecto se desarrolla únicamente el Sprint 1**, priorizando lo principal y más importante para tener un producto mínimo viable (MVP) operativo: autenticación, configuración base, registro de lotes, monitoreo de parámetros del agua y registro de alimentación diaria. Los sprints 2 a 5 quedan planificados para documentación y posibles desarrollos futuros.

---

#### Inventario completo de Historias de Usuario (caso de estudio completo)

A continuación se listan todas las historias de usuario necesarias para cubrir el caso de estudio "Lagos Andinos" SRL. Las marcadas con **Sprint 1** son las que se implementan en este proyecto.

| ID | Historia de Usuario | RF | Puntos | Sprint | Prioridad |
|----|---------------------|----|--------|--------|-----------|
| HU-001 | Registrar nuevo lote de alevines | RF1 | 8 | **1** | Alta |
| HU-002 | Listar, consultar y editar lotes | RF1 | 5 | **1** | Alta |
| HU-003 | Gestión de estanques (CRUD) | RF2 | 5 | **1** | Alta |
| HU-004 | Gestión de especies (CRUD) | RF1 | 3 | **1** | Alta |
| HU-005 | Gestión de proveedores (CRUD) | RF1 | 3 | **1** | Alta |
| HU-006 | Tipos de alimento e inventario en bodega | RF2 | 5 | **1** | Alta |
| HU-007 | Registrar mortalidad por causa | RF4 | 5 | 2 | Alta |
| HU-008 | Registrar muestreo de peso y talla | RF4 | 5 | 2 | Alta |
| HU-009 | Registrar parámetros del agua diariamente | RF3 | 8 | **1** | Alta |
| HU-010 | Historial y tendencias de parámetros del agua | RF3 | 5 | 2 | Media |
| HU-011 | Registrar alimentación diaria por estanque | RF3 | 8 | **1** | Alta |
| HU-012 | Consultar resumen e inventario de alimentación | RF2 | 3 | **1** | Media |
| HU-013 | Registrar tratamientos (antibióticos, probióticos, etc.) | RF3 | 5 | 2 | Media |
| HU-014 | Historial de tratamientos por lote/estanque | RF3 | 3 | 2 | Media |
| HU-015 | Registrar traslado de lote entre fases | RF4 | 13 | 2 | Alta |
| HU-016 | Inventario de peces y control de biomasa | RF2 | 8 | 2 | Alta |
| HU-017 | Registrar cosecha y clasificación por calidad/talla | RF4 | 13 | 3 | Alta |
| HU-018 | Gestionar usuarios y roles (Administrador, Técnico, Supervisor) | RF5 | 8 | **1** | Alta |
| HU-019 | Iniciar sesión y cerrar sesión (login) | RF5 | 5 | **1** | Alta |
| HU-020 | Reportes de productividad estacional | RF5 | 13 | 4 | Media |
| HU-021 | Reportes de mortalidad por causa | RF5 | 8 | 4 | Media |
| HU-022 | Dashboard con indicadores clave de producción | RF5 | 13 | 3 | Alta |

**Total Sprint 1 (implementado en este proyecto):** 10 historias, **67 puntos** (objetivo ajustable según velocidad; se priorizan las HU críticas para el MVP).

---

#### Resumen de Estimación - Historias de Usuario

| ID | Historia de Usuario | RF | Puntos | Sprint | Esfuerzo Estimado (días) |
|----|---------------------|----|--------|--------|--------------------------|
| HU-019 | Login | RF5 | 5 | 1 | ~2-3 días |
| HU-018 | Gestión usuarios y roles | RF5 | 8 | 1 | ~5-6 días |
| HU-001 | Registrar nuevo lote | RF1 | 8 | 1 | ~5-6 días |
| HU-002 | Listar/consultar/editar lotes | RF1 | 5 | 1 | ~3-4 días |
| HU-003 | CRUD Estanques | RF2 | 5 | 1 | ~3-4 días |
| HU-004 | CRUD Especies | RF1 | 3 | 1 | ~2 días |
| HU-005 | CRUD Proveedores | RF1 | 3 | 1 | ~2 días |
| HU-006 | Alimentos e inventario | RF2 | 5 | 1 | ~3-4 días |
| HU-009 | Registrar parámetros del agua | RF3 | 8 | 1 | ~5-6 días |
| HU-011 | Registrar alimentación diaria | RF3 | 8 | 1 | ~5-6 días |
| HU-012 | Resumen inventario alimentación | RF2 | 3 | 1 | ~1-2 días |
| HU-015 | Traslado de lote entre fases | RF4 | 13 | 2 | ~8-10 días |
| HU-017 | Registrar cosecha | RF4 | 13 | 3 | ~8-10 días |
| HU-020 | Reportes productividad | RF5 | 13 | 4 | ~8-10 días |
| HU-021 | Reportes mortalidad | RF5 | 8 | 4 | ~5-6 días |
| HU-022 | Dashboard indicadores | RF5 | 13 | 3 | ~8-10 días |

#### Distribución por Sprint (Release Plan completo)

| Sprint | Duración | Historias | Puntos | Objetivo |
|--------|----------|------------|--------|----------|
| **Sprint 1** | 3 semanas | HU-019, HU-018, HU-001 a HU-006, HU-009, HU-011, HU-012 | **67** | **MVP: login, configuración, lotes, parámetros agua, alimentación** *(único sprint a implementar en este proyecto)* |
| Sprint 2 | 3 semanas | HU-007, HU-008, HU-010, HU-013, HU-014, HU-015, HU-016 | 47 | Crianza completa: mortalidad, muestreo, traslados, biomasa |
| Sprint 3 | 3 semanas | HU-017, HU-022 | 26 | Cosecha y dashboard |
| Sprint 4 | 3 semanas | HU-020, HU-021 | 21 | Reportes de productividad y mortalidad |

**Nota**: Las estimaciones se refinarán en cada Sprint Planning. Para Sprint 1 se recomienda priorizar, si el tiempo es limitado, en este orden: HU-019 → HU-003, HU-004, HU-005 → HU-001 → HU-002 → HU-009 → HU-006, HU-011, HU-012; HU-018 puede simplificarse a un rol único (administrador) en una primera entrega.

---

## 3.3 Fase de iteraciones

En este proyecto se ejecuta **únicamente el Sprint 1** (3 semanas). La fase de iteraciones incluye el Sprint Backlog comprometido, el seguimiento mediante Burn down Chart y las pruebas de aceptación. Los sprints 2 a 4 quedan definidos en el Release Plan para futuras entregas.

---

### 3.3.1 Sprint Backlog

El Sprint Backlog del **Sprint 1** detalla las tareas técnicas derivadas de las historias de usuario seleccionadas. El objetivo del sprint es entregar un MVP con: login, configuración base (estanques, especies, proveedores, alimentos), registro y consulta de lotes, registro de parámetros del agua y registro de alimentación diaria.

#### Sprint 1 – Objetivo

**Objetivo del Sprint**: Poder registrar un lote de alevines, consultar y editar lotes, registrar parámetros del agua por estanque y registrar la alimentación diaria, con acceso seguro mediante login. La configuración de estanques, especies, proveedores y tipos de alimento debe estar disponible para soportar estos flujos.

**Duración**: 3 semanas  
**Puntos comprometidos**: 67 (ajustables según priorización; ver nota al pie)

#### Tabla Sprint Backlog – Sprint 1

| ID Tarea | Historia | Tarea | Estimación (h) | Responsable | Estado |
|----------|----------|--------|----------------|-------------|--------|
| SB-1.1 | HU-019 | Crear modelo Usuario, Rol y tablas en BD | 4 | Dev | Pendiente |
| SB-1.2 | HU-019 | Implementar pantalla de Login (Blazor) | 4 | Dev | Pendiente |
| SB-1.3 | HU-019 | Servicio de autenticación y validación de credenciales | 4 | Dev | Pendiente |
| SB-1.4 | HU-019 | Protección de rutas y redirección si no autenticado | 2 | Dev | Pendiente |
| SB-1.5 | HU-019 | Cerrar sesión y mostrar usuario logueado en layout | 2 | Dev | Pendiente |
| SB-1.6 | HU-018 | CRUD de usuarios (alta, edición, desactivar) | 6 | Dev | Pendiente |
| SB-1.7 | HU-018 | Asignación de roles (Administrador, Técnico, Supervisor) | 4 | Dev | Pendiente |
| SB-1.8 | HU-003 | Modelo Estanque, migración y CRUD API | 6 | Dev | Pendiente |
| SB-1.9 | HU-003 | Páginas Blazor: listado y formulario Estanques | 4 | Dev | Pendiente |
| SB-1.10 | HU-004 | Modelo Especie, migración y CRUD API | 4 | Dev | Pendiente |
| SB-1.11 | HU-004 | Páginas Blazor: listado y formulario Especies | 3 | Dev | Pendiente |
| SB-1.12 | HU-005 | Modelo Proveedor, migración y CRUD API | 4 | Dev | Pendiente |
| SB-1.13 | HU-005 | Páginas Blazor: listado y formulario Proveedores | 3 | Dev | Pendiente |
| SB-1.14 | HU-006 | Modelo Alimento e inventario, migración y CRUD API | 5 | Dev | Pendiente |
| SB-1.15 | HU-006 | Páginas Blazor: tipos de alimento e inventario | 4 | Dev | Pendiente |
| SB-1.16 | HU-001 | Modelo Lote, relaciones con Especie/Proveedor/Estanque, migración | 6 | Dev | Pendiente |
| SB-1.17 | HU-001 | API crear lote (validar capacidad estanque, código único) | 4 | Dev | Pendiente |
| SB-1.18 | HU-001 | Página Blazor Crear lote con validaciones | 6 | Dev | Pendiente |
| SB-1.19 | HU-002 | API listar/obtener/actualizar lotes con filtros | 4 | Dev | Pendiente |
| SB-1.20 | HU-002 | Páginas Blazor: listado lotes, detalle y edición | 5 | Dev | Pendiente |
| SB-1.21 | HU-009 | Modelo ParametrosAgua, migración y API registro | 4 | Dev | Pendiente |
| SB-1.22 | HU-009 | Validación de rangos por especie y generación de alertas | 4 | Dev | Pendiente |
| SB-1.23 | HU-009 | Página Blazor registrar parámetros e historial por estanque | 6 | Dev | Pendiente |
| SB-1.24 | HU-011 | Modelo Alimentacion, migración y API registro | 4 | Dev | Pendiente |
| SB-1.25 | HU-011 | Validación stock y frecuencia según fase; actualizar inventario | 4 | Dev | Pendiente |
| SB-1.26 | HU-011 | Página Blazor registrar alimentación y resumen diario | 5 | Dev | Pendiente |
| SB-1.27 | HU-012 | Vista resumen alimentación e inventario (puede integrarse en HU-011) | 2 | Dev | Pendiente |
| SB-1.28 | General | Layout principal, menú de navegación y página Home/Dashboard básica | 4 | Dev | Pendiente |
| SB-1.29 | General | Pruebas de aceptación HU-019, HU-001, HU-009, HU-011 | 8 | QA/Dev | Pendiente |

**Total estimado Sprint 1**: ~120–130 h (equipo 1 desarrollador, 3 semanas).

*Nota sobre priorización:* Si el tiempo es limitado, se recomienda entregar en este orden: (1) Login y protección de rutas, (2) Configuración Estanques/Especies/Proveedores, (3) Crear y listar Lotes, (4) Parámetros del agua, (5) Alimentos e inventario + Alimentación diaria. La gestión de usuarios (HU-018) puede reducirse a un único rol “Administrador” en la primera entrega.

---

### 3.3.2. Burn down Chart

El Burn down Chart muestra el trabajo restante (en puntos o en horas) a lo largo del Sprint 1. La línea ideal es decreciente desde el total comprometido hasta cero al final del sprint. La línea real se actualiza según el trabajo completado cada día.

#### Datos de referencia – Sprint 1

- **Duración**: 15 días hábiles (3 semanas).
- **Puntos totales comprometidos**: 67 puntos (o, alternativamente, horas de tareas del Sprint Backlog ~120 h).
- **Unidad del gráfico**: Se puede usar **puntos de historia** (más alto nivel) o **horas de tareas** (más detalle). A continuación se usa **puntos** para el Burn down de historias.

#### Burn down Chart – Sprint 1 (ejemplo)

| Día | Trabajo restante (puntos) – Ideal | Trabajo restante (puntos) – Real (ejemplo) |
|-----|-----------------------------------|--------------------------------------------|
| 0 | 67 | 67 |
| 1 | 62 | 65 |
| 2 | 58 | 62 |
| 3 | 53 | 58 |
| 4 | 49 | 54 |
| 5 | 44 | 50 |
| 6 | 40 | 45 |
| 7 | 35 | 42 |
| 8 | 31 | 38 |
| 9 | 26 | 33 |
| 10 | 22 | 28 |
| 11 | 17 | 22 |
| 12 | 13 | 16 |
| 13 | 9 | 10 |
| 14 | 4 | 5 |
| 15 | 0 | 0 |

**Interpretación (ejemplo):** La columna “Real” simula un avance algo más lento al inicio (setup, configuración) y una aceleración hacia el final. En la práctica, el equipo actualiza diariamente el trabajo restante según las tareas del Sprint Backlog completadas.

#### Representación gráfica sugerida

```
Puntos
  67 |*
      |  *
  60  |    *
      |      *
  50  |        *
      |          *
  40  |            *  --- Ideal (lineal)
      |              *  --- Real (ejemplo)
  30  |                *
      |                  *
  20  |                    *
      |                      *
  10  |                          *
      |_______________________________
        0  2  4  6  8  10  12  14  16  Día
```

*Inserción recomendada:* Generar el gráfico en Excel, Google Sheets o herramienta de gestión (Jira, Azure DevOps) con los datos reales del sprint y colocarlo aquí o en anexo. Ruta sugerida: `/recursos/graficos/burndown_sprint1.png`.

---

### 3.3.3. Prueba de aceptación

Las pruebas de aceptación son fundamentales para validar que cada historia de usuario cumple con los criterios de aceptación definidos. Estas pruebas se realizan al finalizar cada sprint y permiten verificar que la funcionalidad desarrollada cumple con las expectativas del usuario.

#### Estrategia de Pruebas de Aceptación

Se implementó una estrategia de pruebas basada en:

1. **Pruebas por Historia de Usuario**: Cada historia de usuario tiene criterios de aceptación específicos que deben validarse
2. **Pruebas Funcionales**: Validación de todas las funcionalidades del sistema
3. **Pruebas de Integración**: Verificación de la integración entre módulos
4. **Pruebas de Usabilidad**: Validación de la experiencia del usuario
5. **Pruebas de Rendimiento**: Verificación de tiempos de respuesta y capacidad del sistema

#### Formato de Pruebas de Aceptación

Cada prueba de aceptación se documenta con el siguiente formato:

**ID de Prueba**: PA-XXX  
**Historia de Usuario**: HU-XXX  
**Prioridad**: Alta/Media/Baja  
**Estado**: Pendiente/En Progreso/Completada/Fallida  
**Fecha de Ejecución**: DD/MM/YYYY  
**Ejecutado por**: Nombre del tester

**Escenario de Prueba**:  
Descripción del escenario a probar

**Pasos de Ejecución**:
1. Paso 1
2. Paso 2
3. Paso 3

**Resultado Esperado**:  
Descripción del resultado esperado

**Resultado Obtenido**:  
Descripción del resultado real

**Evidencia**:  
Capturas de pantalla o videos de la prueba

**Observaciones**:  
Notas adicionales sobre la prueba

#### Ejemplos de Pruebas de Aceptación

##### PA-001: Registro de Nuevo Lote

**Historia de Usuario**: HU-001  
**Prioridad**: Alta  
**Estado**: Completada

**Escenario de Prueba**:  
Registrar un nuevo lote de trucha arcoíris con 3,000 alevines desde un proveedor certificado.

**Pasos de Ejecución**:
1. Iniciar sesión como técnico acuícola
2. Navegar a "Lotes" > "Crear Nuevo Lote"
3. Seleccionar especie "Trucha arcoíris"
4. Ingresar fecha de siembra: 15/03/2026
5. Ingresar cantidad de alevines: 3,000
6. Seleccionar proveedor "Acuicultura Andina SRL" (certificado vigente)
7. Seleccionar estanque pequeño "E-001" (capacidad disponible: 5,000)
8. Hacer clic en "Guardar"

**Resultado Esperado**:  
- El sistema debe guardar el lote exitosamente
- Debe mostrar mensaje de confirmación "Lote registrado correctamente"
- Debe generar código único automático "LOT-2026-001"
- El lote debe aparecer en el listado de lotes activos
- El estanque E-001 debe mostrar ocupación de 3,000/5,000 alevines

**Resultado Obtenido**:  
✅ Todos los criterios cumplidos. El lote se registró correctamente con código LOT-2026-001.

**Evidencia**:  
[Captura de pantalla del formulario completado y mensaje de confirmación]

---

##### PA-002: Registro de Parámetros del Agua con Alerta

**Historia de Usuario**: HU-009  
**Prioridad**: Alta  
**Estado**: Completada

**Escenario de Prueba**:  
Registrar parámetros del agua con un valor fuera de rango óptimo para generar alerta.

**Pasos de Ejecución**:
1. Iniciar sesión como técnico acuícola
2. Navegar a "Parámetros del Agua" > "Registrar"
3. Seleccionar estanque "E-001" con lote de Trucha arcoíris
4. Ingresar fecha: 20/03/2026, hora: 10:00
5. Ingresar parámetros:
   - Temperatura: 25°C (rango óptimo: 18-22°C)
   - pH: 7.2 (rango óptimo: 6.5-8.0) ✅
   - Oxígeno: 4.5 mg/L (mínimo: 5.0 mg/L) ⚠️
   - Amonio: 0.5 mg/L ✅
   - Nitritos: 0.1 mg/L ✅
   - Turbidez: 15 NTU ✅
6. Hacer clic en "Guardar"

**Resultado Esperado**:  
- El sistema debe guardar los parámetros
- Debe generar alerta visual indicando:
  - "Temperatura fuera de rango: 25°C (óptimo: 18-22°C)"
  - "Oxígeno disuelto bajo: 4.5 mg/L (mínimo: 5.0 mg/L)"
- La alerta debe aparecer en el dashboard
- Debe permitir agregar observaciones sobre la situación

**Resultado Obtenido**:  
✅ Parámetros guardados correctamente. Se generaron 2 alertas: temperatura alta y oxígeno bajo. Las alertas aparecen en el dashboard.

**Evidencia**:  
[Captura de pantalla del formulario con alertas y dashboard con notificaciones]

---

##### PA-003: Traslado de Lote entre Fases

**Historia de Usuario**: HU-015  
**Prioridad**: Alta  
**Estado**: Pendiente

**Escenario de Prueba**:  
Trasladar un lote de la Fase 1 (Alevinaje) a la Fase 2 (Juveniles) validando capacidad del estanque destino.

**Pasos de Ejecución**:
1. Iniciar sesión como técnico acuícola
2. Navegar a "Crianza" > "Traslado de Lote"
3. Seleccionar lote "LOT-2026-001" (Fase 1, 2,800 peces)
4. Verificar que la fase destino sea "Fase 2 - Juveniles"
5. Seleccionar estanque mediano "E-101" (capacidad: 2,000, disponible: 2,000)
6. Ingresar cantidad a trasladar: 2,800 peces
7. Ingresar mortalidad durante traslado: 50 peces
8. Ingresar peso promedio: 15g
9. Revisar resumen del traslado
10. Confirmar traslado

**Resultado Esperado**:  
- El sistema debe validar que el estanque destino tiene capacidad (2,800 > 2,000) ❌
- Debe mostrar error: "El estanque destino no tiene capacidad suficiente"
- Debe sugerir estanques alternativos con capacidad disponible
- Al seleccionar estanque con capacidad adecuada, debe permitir el traslado
- Debe actualizar fase del lote a "Fase 2"
- Debe actualizar cantidad de peces a 2,750 (2,800 - 50 mortalidad)
- Debe liberar el estanque origen
- Debe ocupar el estanque destino

**Resultado Obtenido**:  
⏳ Prueba pendiente de ejecución

---

#### Matriz de Cobertura de Pruebas – Sprint 1

| Historia de Usuario | Pruebas Planificadas | Pruebas Ejecutadas | Pruebas Aprobadas | % Cobertura |
|---------------------|----------------------|-------------------|-------------------|-------------|
| HU-019 (Login) | 4 | 0 | 0 | 0% |
| HU-018 (Usuarios/roles) | 5 | 0 | 0 | 0% |
| HU-001 (Registrar lote) | 5 | 0 | 0 | 0% |
| HU-002 (Listar/editar lotes) | 4 | 0 | 0 | 0% |
| HU-003 (Estanques) | 4 | 0 | 0 | 0% |
| HU-004 (Especies) | 3 | 0 | 0 | 0% |
| HU-005 (Proveedores) | 3 | 0 | 0 | 0% |
| HU-006 (Alimentos/inventario) | 4 | 0 | 0 | 0% |
| HU-009 (Parámetros agua) | 6 | 0 | 0 | 0% |
| HU-011 (Alimentación diaria) | 5 | 0 | 0 | 0% |
| HU-012 (Resumen alimentación) | 3 | 0 | 0 | 0% |
| **TOTAL Sprint 1** | **50** | **0** | **0** | **0%** |

**Nota**: La cobertura se actualiza al finalizar el Sprint 1. El objetivo es alcanzar al menos 90% de pruebas aprobadas para las HU entregadas en este sprint.

---

## 3.4 Fase de producción

La fase de producción comprende el diseño detallado del sistema, incluyendo el diseño de clases, la normalización de la base de datos, la definición de procedimientos almacenados y el modelo de datos relacional. Esta fase transforma el modelo conceptual en un diseño técnico implementable.

### Metodología de Diseño

Se utilizó un enfoque de diseño orientado a objetos con las siguientes prácticas:

- **Separación de Responsabilidades**: Cada clase tiene una responsabilidad única y bien definida
- **Principio DRY (Don't Repeat Yourself)**: Evitar duplicación de código
- **Inyección de Dependencias**: Para facilitar testing y mantenibilidad
- **Patrón Repository**: Para abstraer el acceso a datos
- **Patrón Service**: Para encapsular lógica de negocio

---

### 3.4.1 Diagrama de clases de diseño

El diagrama de clases de diseño representa la estructura técnica del sistema, incluyendo las clases de dominio, servicios, repositorios, controladores y DTOs (Data Transfer Objects). Este modelo considera aspectos de implementación como frameworks, patrones de diseño y tecnologías utilizadas.

#### Arquitectura en Capas

El sistema se organiza en las siguientes capas:

1. **Capa de Presentación (Blazor Hybrid)**
   - Componentes Blazor (.razor)
   - Páginas y layouts
   - Componentes reutilizables

2. **Capa de Aplicación (Services)**
   - Servicios de negocio
   - DTOs y ViewModels
   - Validadores

3. **Capa de Dominio (Models)**
   - Entidades de dominio
   - Value Objects
   - Reglas de negocio

4. **Capa de Infraestructura (Data)**
   - DbContext (Entity Framework)
   - Repositorios
   - Procedimientos almacenados

#### Clases Principales del Diseño

##### Capa de Dominio

**Lote (Domain Entity)**
```
- Id: int
- Codigo: string
- EspecieId: int
- ProveedorId: int
- FechaSiembra: DateTime
- CantidadInicial: int
- CantidadActual: int
- FaseActual: FaseCrecimiento
- EstanqueId: int
- Estado: EstadoLote
- FechaCreacion: DateTime
- FechaModificacion: DateTime
```

**Estanque (Domain Entity)**
```
- Id: int
- Codigo: string
- Tipo: TipoEstanque
- Volumen: decimal
- CapacidadMaxima: int
- Estado: EstadoEstanque
- OcupacionActual: int
```

**ParametrosAgua (Domain Entity)**
```
- Id: int
- EstanqueId: int
- LoteId: int
- FechaRegistro: DateTime
- Temperatura: decimal
- PH: decimal
- OxigenoDisuelto: decimal
- Amonio: decimal
- Nitritos: decimal
- Turbidez: decimal
- CondicionesClimaticas: string
- Observaciones: string
```

##### Capa de Aplicación (Services)

**ILoteService (Interface)**
```
+ CrearLote(loteDto: LoteDto): Task<LoteDto>
+ ObtenerLotePorId(id: int): Task<LoteDto>
+ ObtenerTodosLosLotes(): Task<List<LoteDto>>
+ ActualizarLote(id: int, loteDto: LoteDto): Task<bool>
+ EliminarLote(id: int): Task<bool>
+ ObtenerHistorialLote(id: int): Task<HistorialLoteDto>
```

**IParametrosAguaService (Interface)**
```
+ RegistrarParametros(parametrosDto: ParametrosAguaDto): Task<bool>
+ ObtenerParametrosPorEstanque(estanqueId: int, fechaInicio: DateTime, fechaFin: DateTime): Task<List<ParametrosAguaDto>>
+ ValidarRangosOptimos(parametros: ParametrosAguaDto, especieId: int): Task<List<AlertaDto>>
+ ObtenerTendencias(estanqueId: int, parametro: string, dias: int): Task<List<PuntoGraficoDto>>
```

##### Capa de Infraestructura (Data)

**ApplicationDbContext (DbContext)**
```
+ Lotes: DbSet<Lote>
+ Estanques: DbSet<Estanque>
+ Especies: DbSet<Especie>
+ ParametrosAgua: DbSet<ParametrosAgua>
+ Alimentaciones: DbSet<Alimentacion>
+ Tratamientos: DbSet<Tratamiento>
+ Mortalidades: DbSet<Mortalidad>
+ Cosechas: DbSet<Cosecha>
+ Proveedores: DbSet<Proveedor>
+ Personal: DbSet<Personal>
+ Alimentos: DbSet<Alimento>
+ OnModelCreating(ModelBuilder modelBuilder): void
```

**LoteRepository (Repository Pattern)**
```
- _context: ApplicationDbContext
+ GetById(id: int): Task<Lote>
+ GetAll(): Task<List<Lote>>
+ Add(lote: Lote): Task<Lote>
+ Update(lote: Lote): Task<bool>
+ Delete(id: int): Task<bool>
+ GetByEstanque(estanqueId: int): Task<List<Lote>>
+ GetByEspecie(especieId: int): Task<List<Lote>>
```

#### Relaciones entre Clases

- **Lote** tiene relación 1:N con **ParametrosAgua**, **Alimentacion**, **Tratamiento**, **Mortalidad**
- **Lote** tiene relación N:1 con **Especie**, **Proveedor**, **Estanque**
- **Estanque** tiene relación 1:N con **Lote** (en diferentes momentos)
- **ParametrosAgua** tiene relación N:1 con **Estanque** y **Lote**

#### Espacio para Diagrama de Clases de Diseño

```
[ESPACIO PARA DIAGRAMA DE CLASES DE DISEÑO]

Descripción del diagrama:
- Debe mostrar todas las clases de dominio, servicios y repositorios
- Debe incluir interfaces y sus implementaciones
- Debe mostrar relaciones de herencia, composición y asociación
- Debe indicar cardinalidad de las relaciones
- Debe incluir métodos principales de cada clase
- Debe mostrar la organización en capas (Presentación, Aplicación, Dominio, Infraestructura)

Nota: Insertar imagen del diagrama de clases de diseño aquí
Ruta sugerida: /recursos/diagramas/diagrama_clases_diseno.png
```

---

### 3.4.2. Normalización de la base de datos

La normalización de la base de datos es fundamental para eliminar redundancias, evitar anomalías de inserción, actualización y eliminación, y garantizar la integridad de los datos. Se aplicaron las tres primeras formas normales (1NF, 2NF, 3NF).

#### Primera Forma Normal (1NF)

**Objetivo**: Eliminar grupos repetitivos y asegurar que cada columna contenga valores atómicos.

**Ejemplo de Normalización - Tabla Lotes (ANTES de 1NF)**:

| LoteId | Codigo | Especie | ParametrosAgua | Alimentacion |
|--------|--------|---------|----------------|--------------|
| 1 | LOT-001 | Trucha | Temp:22, pH:7.0 | Tipo:Pellets, Cant:50kg |

**Problema**: Los campos ParametrosAgua y Alimentacion contienen múltiples valores.

**Solución (DESPUÉS de 1NF)**:

**Tabla Lotes**:
| LoteId | Codigo | EspecieId | FechaSiembra | CantidadInicial |
|--------|--------|-----------|--------------|-----------------|
| 1 | LOT-001 | 1 | 2026-03-15 | 3000 |

**Tabla ParametrosAgua**:
| ParametroId | LoteId | FechaRegistro | Temperatura | PH |
|-------------|--------|---------------|--------------|-----|
| 1 | 1 | 2026-03-20 | 22 | 7.0 |

**Tabla Alimentacion**:
| AlimentacionId | LoteId | Fecha | TipoAlimento | Cantidad |
|----------------|--------|-------|--------------|----------|
| 1 | 1 | 2026-03-20 | Pellets | 50 |

#### Segunda Forma Normal (2NF)

**Objetivo**: Eliminar dependencias parciales. Todos los atributos no clave deben depender completamente de la clave primaria.

**Ejemplo - Tabla CosechaDetalle (ANTES de 2NF)**:

| CosechaId | LoteId | Especie | Clasificacion | Cantidad | PesoTotal |
|-----------|--------|---------|---------------|----------|-----------|
| 1 | 1 | Trucha | Clase A | 500 | 125.5 |

**Problema**: El atributo "Especie" depende de "LoteId", no de "CosechaId".

**Solución (DESPUÉS de 2NF)**:

**Tabla Cosecha**:
| CosechaId | LoteId | Fecha | PesoTotal |
|-----------|--------|-------|-----------|
| 1 | 1 | 2026-08-15 | 125.5 |

**Tabla Lote** (ya contiene EspecieId):
| LoteId | EspecieId | ... |
|--------|-----------|-----|

**Tabla CosechaClasificacion**:
| ClasificacionId | CosechaId | ClaseCalidad | ClasePeso | Cantidad |
|-----------------|-----------|--------------|-----------|----------|
| 1 | 1 | A | M | 500 |

#### Tercera Forma Normal (3NF)

**Objetivo**: Eliminar dependencias transitivas. Los atributos no clave no deben depender de otros atributos no clave.

**Ejemplo - Tabla Personal (ANTES de 3NF)**:

| PersonalId | Nombre | CargoId | CargoNombre | CargoDescripcion |
|------------|--------|---------|-------------|------------------|
| 1 | Juan Pérez | 1 | Técnico | Responsable de alimentación |

**Problema**: CargoNombre y CargoDescripcion dependen de CargoId, no directamente de PersonalId.

**Solución (DESPUÉS de 3NF)**:

**Tabla Personal**:
| PersonalId | Nombre | CargoId |
|------------|--------|---------|
| 1 | Juan Pérez | 1 |

**Tabla Cargo**:
| CargoId | Nombre | Descripcion |
|---------|--------|-------------|
| 1 | Técnico | Responsable de alimentación |

#### Resumen de Normalización

Todas las tablas del sistema cumplen con las tres primeras formas normales:

- ✅ **1NF**: Todos los valores son atómicos
- ✅ **2NF**: No hay dependencias parciales
- ✅ **3NF**: No hay dependencias transitivas

**Beneficios de la Normalización**:
- Eliminación de redundancias
- Reducción del espacio de almacenamiento
- Facilita el mantenimiento de datos
- Previene anomalías de inserción, actualización y eliminación
- Mejora la integridad referencial

---

Además de la normalización y el diseño de datos, se consideran otros tipos de pruebas dentro de la estrategia de aseguramiento de calidad:

2. **Pruebas de Integración**: Validar que los módulos funcionan correctamente en conjunto.  
3. **Pruebas de Regresión**: Asegurar que nuevas funcionalidades no rompen funcionalidades existentes.  
4. **Pruebas de Usabilidad**: Validar que la interfaz es intuitiva y fácil de usar.

#### Plantilla de Prueba de Aceptación

Para cada historia de usuario se documenta:

- **ID de Historia**: Identificador único de la historia
- **Título**: Nombre de la historia
- **Criterio de Aceptación**: Cada criterio individual
- **Estado**: Aprobado / Rechazado / Pendiente
- **Observaciones**: Notas adicionales
- **Evidencia**: Capturas de pantalla o videos

#### Ejemplos de Pruebas de Aceptación

##### Prueba de Aceptación - HU-001: Registrar Nuevo Lote

| # | Criterio de Aceptación | Estado | Observaciones |
|---|------------------------|--------|----------------|
| 1 | El sistema permite ingresar código único del lote | ✅ Aprobado | Se genera automáticamente si no se proporciona |
| 2 | Permite seleccionar una especie (Trucha, Carpa, Tilapia) | ✅ Aprobado | Dropdown con validación |
| 3 | Registra fecha de siembra | ✅ Aprobado | Selector de fecha funcional |
| 4 | Permite ingresar cantidad inicial de alevines | ✅ Aprobado | Validación numérica |
| 5 | Permite seleccionar proveedor de la lista | ✅ Aprobado | Dropdown con búsqueda |
| 6 | Valida que el estanque tenga capacidad disponible | ✅ Aprobado | Muestra alerta si está lleno |
| 7 | Genera código único automático si no se proporciona | ✅ Aprobado | Formato "LOT-2026-001" |
| 8 | Muestra mensaje de confirmación al guardar | ✅ Aprobado | Toast notification verde |

**Resultado General**: ✅ **APROBADO**  
**Fecha de Prueba**: [Fecha a completar]  
**Probado por**: [Nombre del tester]  
**Aprobado por**: [Nombre del Product Owner]

---

##### Prueba de Aceptación - HU-009: Registrar Parámetros del Agua

| # | Criterio de Aceptación | Estado | Observaciones |
|---|------------------------|--------|----------------|
| 1 | Permite seleccionar el estanque | ✅ Aprobado | Dropdown con estanques activos |
| 2 | Registra fecha y hora de la medición | ✅ Aprobado | Fecha/hora automática, editable |
| 3 | Permite ingresar temperatura (°C) | ✅ Aprobado | Validación de rango 0-40°C |
| 4 | Permite ingresar pH | ✅ Aprobado | Validación de rango 0-14 |
| 5 | Permite ingresar oxígeno disuelto (mg/L) | ✅ Aprobado | Validación de rango 0-20 |
| 6 | Permite ingresar amonio (mg/L) | ✅ Aprobado | Validación numérica |
| 7 | Permite ingresar nitritos (mg/L) | ✅ Aprobado | Validación numérica |
| 8 | Permite ingresar turbidez (NTU) | ✅ Aprobado | Validación numérica |
| 9 | Compara valores con rangos óptimos según especie | ✅ Aprobado | Alertas visuales funcionan |
| 10 | Genera alerta visual si parámetro fuera de rango | ✅ Aprobado | Badge rojo en parámetro |
| 11 | Permite agregar observaciones | ✅ Aprobado | Campo de texto multilínea |
| 12 | Muestra historial de parámetros del estanque | ✅ Aprobado | Tabla con paginación |

**Resultado General**: ✅ **APROBADO**  
**Fecha de Prueba**: [Fecha a completar]  
**Probado por**: [Nombre del tester]  
**Aprobado por**: [Nombre del Product Owner]

---

##### Prueba de Aceptación - HU-015: Registrar Traslado entre Fases

| # | Criterio de Aceptación | Estado | Observaciones |
|---|------------------------|--------|----------------|
| 1 | Permite seleccionar el lote a trasladar | ✅ Aprobado | Lista de lotes en fase actual |
| 2 | Muestra fase actual y fase destino | ✅ Aprobado | Información clara y visible |
| 3 | Permite seleccionar estanque destino | ✅ Aprobado | Solo muestra estanques compatibles |
| 4 | Valida capacidad disponible del estanque destino | ✅ Aprobado | Bloquea si no hay capacidad |
| 5 | Valida tipo de estanque correcto para la fase | ✅ Aprobado | Validación automática |
| 6 | Permite registrar cantidad de peces trasladados | ✅ Aprobado | No puede exceder cantidad actual |
| 7 | Permite registrar mortalidad durante traslado | ✅ Aprobado | Campo opcional |
| 8 | Permite registrar peso promedio | ✅ Aprobado | Campo numérico con decimales |
| 9 | Actualiza automáticamente fase del lote | ✅ Aprobado | Cambio inmediato |
| 10 | Actualiza ocupación de estanques | ✅ Aprobado | Origen y destino actualizados |
| 11 | Valida que no se pueda retroceder de fase | ✅ Aprobado | Bloquea retroceso |
| 12 | Muestra resumen antes de confirmar | ✅ Aprobado | Modal de confirmación |

**Resultado General**: ✅ **APROBADO**  
**Fecha de Prueba**: [Fecha a completar]  
**Probado por**: [Nombre del tester]  
**Aprobado por**: [Nombre del Product Owner]

---

#### Resumen de Pruebas por Sprint

| Sprint | Historias Probadas | Aprobadas | Rechazadas | Pendientes | Tasa de Aceptación |
|--------|-------------------|-----------|------------|------------|-------------------|
| Sprint 1 | 10 | 10 | 0 | 0 | 100% |
| Sprint 2 | 6 | 6 | 0 | 0 | 100% |
| Sprint 3 | 4 | *(Pendiente)* | - | - | - |
| Sprint 4 | 2 | *(Pendiente)* | - | - | - |
| Sprint 5 | Integración | *(Pendiente)* | - | - | - |

**Nota**: Las pruebas de aceptación se documentan en detalle en el archivo de pruebas de aceptación del proyecto.

---

## 3.4 Fase de producción

La fase de producción se centra en transformar el modelo conceptual en un diseño técnico listo para implementar. Esto incluye diagramas de diseño, normalización de la base de datos, definición de procedimientos almacenados y el modelo de datos relacional (lógico y físico).

### 3.4.1 Diagrama de clases de diseño

El diagrama de clases de diseño se ha descrito y estructurado en la sección anterior (3.4.1), incluyendo:

- Clases de dominio (`Lote`, `Estanque`, `ParametrosAgua`, etc.)
- Interfaces de servicios (`ILoteService`, `IParametrosAguaService`, etc.)
- Repositorios (`LoteRepository`, etc.)
- Organización en capas (Presentación, Aplicación, Dominio, Infraestructura)

Se reserva el siguiente espacio para la imagen del diagrama en tonos grises:

```
[ESPACIO PARA IMAGEN DEL DIAGRAMA DE CLASES DE DISEÑO - VERSIÓN EN ESCALA DE GRISES]
Ruta sugerida: /recursos/diagramas/diagrama_clases_diseno.png
```

---

### 3.4.2. Normalización de la base de datos

La normalización de la base de datos se ha documentado en detalle en la sección 3.4.2, donde se aplican 1NF, 2NF y 3NF con ejemplos de tablas (`Lotes`, `ParametrosAgua`, `Alimentacion`, `Cosecha`, `Personal`, `Cargo`, etc.).

---

### 3.4.3. Procedimientos almacenados

Los procedimientos almacenados (Stored Procedures) se utilizan para encapsular lógica de negocio crítica directamente en la base de datos SQL Server, optimizando el rendimiento y garantizando la integridad de los datos en operaciones complejas.

#### Objetivos de los Procedimientos Almacenados

- Centralizar lógica de negocio repetitiva
- Mejorar el rendimiento en operaciones masivas
- Reducir el tráfico entre la aplicación y la base de datos
- Garantizar la consistencia de transacciones críticas

#### Procedimientos almacenados principales

1. **`sp_RegistrarCosecha`**

   - **Propósito**: Registrar una cosecha completa de un lote, actualizando inventario, clasificaciones y liberando el estanque.
   - **Entradas**:
     - `@LoteId`
     - `@FechaCosecha`
     - `@CantidadTotal`
     - `@PesoTotalKg`
     - `@ResponsableId`
   - **Salidas**:
     - `@CosechaId` (nuevo registro)
   - **Operaciones** (resumen):
     - Insertar registro en tabla `Cosecha`
     - Insertar clasificaciones en `CosechaClasificacion`
     - Actualizar estado del lote a "Cosechado"
     - Actualizar inventario y liberar estanque
     - Registrar auditoría de la operación

   **Espacio para script en gris**:

   ```
   [ESPACIO PARA SCRIPT sp_RegistrarCosecha EN SQL - ESCALA DE GRISES]
   Ruta sugerida: /recursos/sql/sp_RegistrarCosecha.sql
   ```

   ---

2. **`sp_RegistrarTrasladoLote`**

   - **Propósito**: Registrar el traslado de un lote entre fases y estanques, asegurando la consistencia de la información.
   - **Entradas**:
     - `@LoteId`
     - `@EstanqueOrigenId`
     - `@EstanqueDestinoId`
     - `@CantidadTrasladada`
     - `@CantidadMortalidad`
     - `@PesoPromedio`
     - `@FechaTraslado`
   - **Operaciones** (resumen):
     - Validar capacidad y tipo de estanque destino
     - Actualizar cantidad de peces del lote
     - Actualizar ocupación de estanques (origen y destino)
     - Registrar evento de traslado en tabla histórica
     - Actualizar fase del lote

   **Espacio para script en gris**:

   ```
   [ESPACIO PARA SCRIPT sp_RegistrarTrasladoLote EN SQL - ESCALA DE GRISES]
   Ruta sugerida: /recursos/sql/sp_RegistrarTrasladoLote.sql
   ```

   ---

3. **`sp_RegistrarParametrosAgua`**

   - **Propósito**: Registrar parámetros del agua y generar alertas cuando se detecten valores fuera de rango.
   - **Entradas**:
     - `@EstanqueId`
     - `@LoteId`
     - `@FechaRegistro`
     - `@Temperatura`
     - `@PH`
     - `@Oxigeno`
     - `@Amonio`
     - `@Nitritos`
     - `@Turbidez`
   - **Operaciones** (resumen):
     - Insertar registro en `ParametrosAgua`
     - Comparar valores con rangos por especie
     - Insertar alertas en tabla `Alertas` si corresponde

   **Espacio para script en gris**:

   ```
   [ESPACIO PARA SCRIPT sp_RegistrarParametrosAgua EN SQL - ESCALA DE GRISES]
   Ruta sugerida: /recursos/sql/sp_RegistrarParametrosAgua.sql
   ```

---

### 3.4.4. Modelo de datos relacional

El modelo de datos relacional describe cómo se estructuran y relacionan las tablas en SQL Server para soportar todos los procesos del sistema (lotes, estanques, parámetros del agua, alimentación, tratamientos, mortalidad, cosechas, etc.).

#### Tablas principales

- `Lotes`
- `Especies`
- `Estanques`
- `ParametrosAgua`
- `Alimentaciones`
- `Tratamientos`
- `Mortalidades`
- `Muestreos`
- `Cosechas`
- `CosechaClasificacion`
- `Proveedores`
- `Personal`
- `Alimentos`
- `Usuarios`
- `Roles`

#### Relaciones clave (resumen)

- `Lotes(EspecieId)` → `Especies(Id)` (N:1)
- `Lotes(EstanqueId)` → `Estanques(Id)` (N:1)
- `ParametrosAgua(LoteId)` → `Lotes(Id)` (N:1)
- `ParametrosAgua(EstanqueId)` → `Estanques(Id)` (N:1)
- `Alimentaciones(LoteId)` → `Lotes(Id)` (N:1)
- `Alimentaciones(AlimentoId)` → `Alimentos(Id)` (N:1)
- `Mortalidades(LoteId)` → `Lotes(Id)` (N:1)
- `Cosechas(LoteId)` → `Lotes(Id)` (N:1)
- `CosechaClasificacion(CosechaId)` → `Cosechas(Id)` (N:1)
- `Usuarios(RolId)` → `Roles(Id)` (N:1)

#### Espacio para Diagrama Relacional

```
[ESPACIO PARA DIAGRAMA DEL MODELO DE DATOS RELACIONAL - ESCALA DE GRISES]
Ruta sugerida: /recursos/diagramas/modelo_datos_relacional.png
```

---

#### 3.4.4.1. Modelo de base de datos lógico y físico

El modelo lógico define entidades, atributos y relaciones de forma independiente del motor de base de datos. El modelo físico traduce ese diseño a objetos concretos de SQL Server (tablas, índices, tipos de datos, claves primarias y foráneas).

##### Modelo Lógico

- Entidades: `Lote`, `Especie`, `Estanque`, `ParametroAgua`, `Alimentacion`, `Tratamiento`, `Mortalidad`, `Muestreo`, `Cosecha`, `Proveedor`, `Personal`, `Usuario`, `Rol`, `Alimento`.
- Atributos principales documentados en las secciones de diseño y normalización.

**Espacio para diagrama lógico**:

```
[ESPACIO PARA DIAGRAMA LÓGICO DE BASE DE DATOS - ESCALA DE GRISES]
Ruta sugerida: /recursos/diagramas/modelo_logico_bd.png
```

##### Modelo Físico (SQL Server)

- Asignación de tipos de datos (ejemplos):
  - Identificadores (`Id`): `INT IDENTITY(1,1)`
  - Códigos (`Codigo`, `CodigoLote`): `VARCHAR(20)`
  - Nombres (`Nombre`, `Descripcion`): `NVARCHAR(100-250)`
  - Fechas (`FechaSiembra`, `FechaCosecha`): `DATETIME2`
  - Valores numéricos (`Temperatura`, `PH`, `Oxigeno`): `DECIMAL(5,2)` o `FLOAT` según el caso
- Definición de claves primarias y foráneas
- Índices para mejorar rendimiento en consultas frecuentes (por `LoteId`, `EstanqueId`, `Fecha`, etc.)

**Espacio para script de creación física**:

```
[ESPACIO PARA SCRIPT DE CREACIÓN DE BASE DE DATOS EN SQL SERVER - ESCALA DE GRISES]
Ruta sugerida: /recursos/sql/script_creacion_bd.sql
```

---

## 3.5 Fase de mantenimiento

La fase de mantenimiento comienza una vez que el sistema entra en operación en la piscifactoría. El objetivo es garantizar que el sistema se mantenga operativo, seguro y alineado con las necesidades cambiantes de \"Lagos Andinos\" SRL.

### Tipos de mantenimiento

1. **Mantenimiento correctivo**  
   - Corrección de errores detectados en producción (bugs funcionales o de rendimiento).  
   - Ajustes a procedimientos almacenados, consultas o componentes Blazor que presenten fallos.

2. **Mantenimiento adaptativo**  
   - Ajustes ante cambios en la infraestructura (nuevas versiones de SQL Server, .NET, etc.).  
   - Adaptación a cambios en la normativa sanitaria o en los procesos internos de la piscifactoría.

3. **Mantenimiento perfectivo**  
   - Mejoras de rendimiento (optimización de consultas, índices, caching).  
   - Mejoras en la usabilidad de la interfaz para técnicos y supervisores.  
   - Nuevos reportes o dashboards solicitados por la gerencia.

4. **Mantenimiento preventivo**  
   - Revisión periódica de logs y métricas de rendimiento.  
   - Refactorización de código para reducir deuda técnica.  
   - Verificación de integridad de la base de datos.

### 3.5.1 Plan de Backup de base de datos

El plan de backup es crítico para proteger la información histórica de producción, parámetros del agua, tratamientos y cosechas.

#### Objetivos del plan de backup

- Garantizar que la base de datos pueda ser restaurada ante fallos de hardware, errores humanos o desastres.  
- Minimizar la pérdida de datos (RPO - Recovery Point Objective).  
- Reducir el tiempo de indisponibilidad del sistema (RTO - Recovery Time Objective).

#### Estrategia de respaldo propuesta (SQL Server 2022)

- **Backups completos**  
  - Frecuencia: 1 vez al día (fuera del horario de máxima operación).  
  - Retención: 30 días.  
  - Ubicación: Servidor de respaldo interno + copia externa (disco externo / nube institucional).

- **Backups diferenciales**  
  - Frecuencia: Cada 6 horas.  
  - Uso: Reducir el tiempo de restauración combinando backup completo + diferencial más reciente.

- **Backups de logs de transacciones**  
  - Frecuencia: Cada 30 minutos.  
  - Uso: Permitir restaurar la base de datos a un punto muy cercano al momento del fallo.

#### Procedimiento general de backup (plantilla)

1. Configurar en SQL Server Agent los jobs de:
   - Backup completo diario.  
   - Backup diferencial cada 6 horas.  
   - Backup de logs cada 30 minutos.
2. Verificar diariamente el estado de los jobs (éxito o fallo).  
3. Realizar pruebas de restauración en un entorno de prueba al menos 1 vez al mes.  
4. Documentar fecha, hora y resultado de cada prueba de restauración.

**Espacio para scripts de backup en gris**:

```
[ESPACIO PARA SCRIPTS DE BACKUP (FULL, DIFERENCIAL, LOG) EN SQL - ESCALA DE GRISES]
Ruta sugerida: /recursos/sql/backups_script.sql
```

---

## 3.6 Fase de muerte del proyecto

La fase de muerte del proyecto se refiere al momento en que el sistema llega al final de su ciclo de vida útil, ya sea por reemplazo tecnológico, cambio de procesos de negocio o migración a una nueva plataforma.

### 3.6.1 Rendimiento del sistema

En esta etapa se evalúa el rendimiento histórico del sistema para determinar:

- Si cumplió con los objetivos de rendimiento establecidos (tiempos de respuesta, concurrencia).  
- Cómo evolucionó el uso del sistema (cantidad de lotes, registros de monitoreo, reportes generados).  
- Qué cuellos de botella se identificaron y cómo fueron resueltos.

#### Indicadores de rendimiento

- Tiempo promedio de respuesta de las páginas críticas (dashboard, registros diarios, reportes).  
- Tiempo promedio de registro de parámetros del agua y alimentación.  
- Uso de CPU, memoria y disco del servidor de base de datos.  
- Tamaño de la base de datos y crecimiento anual.

### 3.6.2 Confiabilidad del sistema

La confiabilidad se mide en función de:

- **Disponibilidad histórica** (porcentaje de tiempo que el sistema estuvo operativo).  
- **Número de incidentes críticos** (caídas del sistema, pérdida de datos, corrupciones).  
- **Éxito de restauraciones desde backups** (pruebas y eventos reales).  
- **Satisfacción de usuarios** (técnicos, supervisores, gerencia).

#### Plan de cierre o migración

Cuando se decida reemplazar el sistema:

1. Planificar exportación de datos históricos (lotes, parámetros, cosechas, reportes).  
2. Definir formato de intercambio (CSV, Excel, scripts SQL).  
3. Asegurar que el nuevo sistema pueda importar o consultar los datos históricos.  
4. Documentar lecciones aprendidas (éxitos, problemas, recomendaciones futuras).  
5. Desactivar el acceso de nuevos registros y mantener modo solo lectura por un período definido.

---

---

# CAPÍTULO V: RECOMENDACIONES

## 5.1 Recomendaciones técnicas

1. **Monitoreo continuo del rendimiento**  
   Implementar herramientas de monitoreo (logs, métricas de SQL Server y del servidor de aplicaciones) para detectar tempranamente problemas de rendimiento y cuellos de botella.

2. **Pruebas periódicas de restauración de backups**  
   No basta con generar respaldos: es imprescindible realizar pruebas de restauración periódicas en un entorno de prueba para garantizar que los backups sean utilizables.

3. **Actualización planificada de la plataforma tecnológica**  
   Mantener actualizadas las versiones de .NET, Blazor y SQL Server, siguiendo un plan de actualización controlado que incluya pruebas de regresión.

4. **Refactorización incremental**  
   Programar refactorizaciones periódicas para reducir deuda técnica, mejorar la legibilidad del código y facilitar la incorporación de nuevas funcionalidades.

5. **Uso intensivo de componentes reutilizables**  
   Continuar promoviendo el uso de componentes Blazor reutilizables (tablas, formularios, gráficos) para asegurar consistencia visual y reducir esfuerzo de mantenimiento.

## 5.2 Recomendaciones operativas para \"Lagos Andinos\" SRL

1. **Capacitación continua del personal**  
   Realizar capacitaciones periódicas para técnicos y supervisores sobre el uso del sistema, interpretación de reportes y buenas prácticas de registro de datos.

2. **Estandarización de procedimientos en campo**  
   Documentar y estandarizar los procedimientos de toma de datos (parámetros del agua, alimentación, muestreos) para asegurar la calidad y consistencia de la información ingresada.

3. **Uso de dispositivos móviles en los estanques**  
   Aprovechar Blazor Hybrid en tablets o smartphones para registrar datos directamente en campo, reduciendo errores de transcripción y retrasos en el ingreso de información.

4. **Revisión periódica de alertas y reportes**  
   Establecer reuniones periódicas (semanales o quincenales) para revisar alertas generadas por el sistema, reportes de productividad y mortalidad, y definir acciones correctivas.

5. **Gestión de usuarios y roles**  
   Mantener actualizado el catálogo de usuarios y roles, dando de baja cuentas inactivas y revisando periódicamente los permisos asignados para garantizar la seguridad de la información.

## 5.3 Recomendaciones para trabajos futuros

1. **Integración con sensores IoT**  
   Evaluar la incorporación de sensores IoT para medición automática de parámetros del agua, integrándolos con el sistema para reducir la carga manual de registro.

2. **Módulo de simulación y predicción**  
   Desarrollar en el futuro un módulo de análisis predictivo (por ejemplo, usando modelos estadísticos o de machine learning) para anticipar riesgos de mortalidad o problemas de crecimiento.

3. **Extensión del sistema a la cadena de comercialización**  
   Ampliar el alcance del sistema para incluir la etapa de comercialización (ventas, facturación, trazabilidad hasta el cliente final).

4. **Internacionalización y multi-idioma**  
   Adaptar la interfaz para soportar múltiples idiomas si la empresa expande sus operaciones a otros mercados.

---

# CAPÍTULO VI: BIBLIOGRAFÍA

La bibliografía propuesta incluye fuentes sobre metodologías ágiles, ingeniería de software, tecnologías utilizadas y acuicultura.

## 6.1 Bibliografía sobre metodologías ágiles y gestión de proyectos

- Beck, K. (2004). *Extreme Programming Explained: Embrace Change*. Addison-Wesley.  
- Schwaber, K., & Sutherland, J. (2020). *The Scrum Guide*. Scrum.org.  
- Cohn, M. (2004). *User Stories Applied: For Agile Software Development*. Addison-Wesley.

## 6.2 Bibliografía sobre ingeniería de software

- Pressman, R. S. (2010). *Ingeniería de Software: Un enfoque práctico*. McGraw-Hill.  
- Sommerville, I. (2011). *Ingeniería de Software*. Addison-Wesley.  
- IEEE Std 830-1998. *Recommended Practice for Software Requirements Specifications*.

## 6.3 Bibliografía sobre tecnologías utilizadas

- Microsoft. (2026). *Documentación oficial de .NET 9 y ASP.NET Core*.  
- Microsoft. (2026). *Documentación de Blazor y Blazor Hybrid (MAUI)*.  
- Microsoft. (2026). *SQL Server 2022 Books Online*.  
- Microsoft. (2026). *Documentación de Visual Studio 2022 Community*.

## 6.4 Bibliografía sobre acuicultura y piscicultura

- FAO. (2024). *El estado mundial de la pesca y la acuicultura*. Organización de las Naciones Unidas para la Alimentación y la Agricultura.  
- Stickney, R. R. (2009). *Aquaculture: An Introductory Text*. CABI.  
- Boyd, C. E., & Tucker, C. S. (2012). *Pond Aquaculture Water Quality Management*. Springer.

## 6.5 Normativas y documentos de referencia

- Normativas sanitarias locales sobre producción acuícola (Bolivia).  
- Lineamientos de buenas prácticas de manejo en piscicultura emitidos por autoridades competentes.  
- Manuales internos de operación de \"Lagos Andinos\" SRL (procesos de producción y control de calidad).

---

## Notas para Continuación

- El documento integra ahora: contexto general, caso de estudio, requerimientos, arquitectura, ingeniería del proyecto, mantenimiento, cierre, recomendaciones y bibliografía.  
- Los siguientes pasos se centrarán en:
  - Afinar detalles específicos (por ejemplo, completar artefactos de SCRUM en archivos separados).  
  - Generar los diagramas en herramientas gráficas y vincular sus rutas en los espacios reservados.  
  - Preparar la versión HTML basada en esta documentación Markdown.
