# CASO DE ESTUDIO: SISTEMA DE GESTIÓN DE PISCICULTURA "LAGOS ANDINOS" SRL

## Contexto del Problema

La acuicultura en la región andina ha pasado de ser una actividad artesanal a una industria que exige precisión técnica para ser rentable. En este contexto, la empresa "Lagos Andinos" SRL se dedica a la crianza de trucha, carpa y tilapia, procesos que requieren un monitoreo riguroso de variables ambientales y biológicas.

La acuicultura es una actividad fundamental para satisfacer la creciente demanda de proteína de origen acuático. El proceso de crianza de peces requiere un control meticuloso de las condiciones del agua, alimentación balanceada y monitoreo constante para obtener peces de calidad comercial optimizando la inversión del productor.

El presente proyecto propone el desarrollo de una solución tecnológica moderna utilizando el stack de Microsoft. La implementación de una aplicación Blazor Hybrid permitirá a los técnicos capturar datos en tiempo real desde dispositivos móviles en los estanques, sincronizándolos con una base de datos centralizada en SQL Server. Esto garantiza que la información sobre alimentación, calidad del agua y crecimiento de los lotes esté disponible para la toma de decisiones estratégicas de manera inmediata.

### Proceso General

El proceso en "Lagos Andinos" comienza con la adquisición de alevines (peces juveniles) de trucha, carpa y tilapia, los cuales provienen de proveedores certificados o se reproducen en la misma piscifactoría. Estos alevines son colocados en estanques especializados con agua de calidad controlada.

Durante el crecimiento, los peces son alimentados con concentrados especiales ricos en proteínas y nutrientes esenciales. La alimentación se realiza múltiples veces al día según la etapa de crecimiento y las condiciones ambientales.

Los peces son monitoreados constantemente para detectar signos de enfermedad o estrés. El agua de los estanques se analiza regularmente para mantener niveles óptimos de oxígeno, pH, temperatura y otros parámetros críticos.

Una vez que los peces alcanzan el peso comercial, son cosechados, clasificados por tamaño y peso, y preparados para su comercialización en mercados locales y restaurantes especializados.

---

## Planteamiento del Problema

Actualmente, "Lagos Andinos" SRL carece de una plataforma digital centralizada para el control de su producción. La dependencia de registros manuales y la dispersión de la información generan los siguientes conflictos operativos:

### Problemas Identificados

1. **Incertidumbre en la Trazabilidad**: Dificultad para rastrear el ciclo de vida completo de un lote, desde su ingreso como alevín hasta su cosecha.

2. **Riesgo de Mortandad**: La falta de alertas tempranas sobre parámetros críticos del agua (pH, Oxígeno, Temperatura) puede derivar en pérdidas económicas considerables.

3. **Descontrol en la Alimentación**: El uso ineficiente del concentrado alimenticio al no estar ajustado dinámicamente a la etapa de crecimiento y condiciones del estanque.

4. **Información Desactualizada**: El retraso entre la toma de datos en campo y su análisis en oficina impide una reacción ágil ante imprevistos biológicos.

---

## Delimitaciones del Proyecto

### Delimitación Espacial o Geográfica

El proyecto se desarrollará y aplicará específicamente para las instalaciones de la piscifactoría "Lagos Andinos" SRL, ubicada en la zona de influencia de la región andina (Bolivia).

### Delimitación Temporal

El desarrollo del sistema se llevará a cabo en un periodo de 4 meses, comprendidos entre marzo y junio de 2026, abarcando desde el relevamiento de requisitos hasta las pruebas finales de producción.

### Delimitación Tecnológica

- **Lenguaje de Programación**: C# (.NET 9)
- **Framework de Frontend**: Blazor Hybrid (MAUI para soporte multiplataforma)
- **Framework de Backend**: ASP.NET Core Web API
- **Motor de Base de Datos**: SQL Server 2022
- **Entorno de Desarrollo**: Visual Studio 2022 Community

---

## Objetivo General

Desarrollar e implementar un sistema de gestión de piscicultura para "Lagos Andinos" SRL utilizando Blazor Hybrid y SQL Server, que permita automatizar el control de lotes de producción, el monitoreo de parámetros de agua y la gestión de alimentación para optimizar la trazabilidad y eficiencia operativa de la empresa.

## Objetivos Específicos

1. Identificar y documentar los requerimientos funcionales y no funcionales de los procesos de cría, monitoreo y cosecha mediante historias de usuario.

2. Diseñar la arquitectura del sistema y el modelo de datos relacional en SQL Server, aplicando normas de integridad y normalización.

3. Construir el Product Backlog priorizado y realizar la estimación de esfuerzo utilizando técnicas de Planning Poker bajo la metodología XP + SCRUM.

4. Codificar los módulos de gestión de lotes, parámetros ambientales y reportes utilizando C# y Blazor Hybrid.

5. Establecer un plan de mantenimiento y respaldo de datos para garantizar la confiabilidad y seguridad de la información crítica de la piscifactoría.

---

## Metáfora del Sistema

Se estableció la metáfora del sistema, visualizando el software como una **"Bitácora de Ciclo Vital"**. En este concepto, los lotes de peces fluyen a través de estaciones (estanques) donde se monitorea su salud y alimentación hasta alcanzar la madurez comercial. Esta metáfora permite identificar los procesos críticos que se transformarán en Historias de Usuario para el Product Backlog.

---


## Requerimientos del Sistema

### 1. Registro de Lotes de Producción Acuícola

- Cada lote tiene un código único de identificación por estanque
- Registro de la especie, fecha de siembra, cantidad de alevines y proveedor
- Control de procedencia y certificaciones sanitarias de los alevines

### 2. Gestión del Proceso de Crianza Acuícola

El proceso inicia con la siembra de alevines en estanques especializados:

#### Fase 1 - Alevinaje (0-3 meses)
- Capacidad actual: 5,000 alevines por estanque pequeño (50m³)
- Alimentación 6 veces al día con alimento micro-granulado
- Control estricto de temperatura (18-22°C) y oxígeno disuelto

#### Fase 2 - Juveniles (3-8 meses)
- Traslado a estanques medianos (200m³) con capacidad para 2,000 peces
- Alimentación 4 veces al día con pellets de crecimiento
- Inicio de clasificación por tamaños para evitar canibalismo

#### Fase 3 - Pre-engorde (8-14 meses)
- Traslado a estanques grandes (500m³) con capacidad para 800 peces
- Alimentación 3 veces al día con concentrado de engorde
- Monitoreo semanal de peso promedio del lote

#### Fase 4 - Engorde final (14-18 meses)
- Permanencia en estanques de engorde hasta peso comercial
- Alimentación controlada según curva de crecimiento
- Preparación para cosecha cuando alcanzan peso óptimo

#### Registros al Finalizar Cada Fase
- Cantidad de peces que avanzan a la siguiente fase
- Cantidad de peces perdidos por mortalidad (especificando causas)
- Peso promedio del lote y dispersión de pesos
- Condiciones del agua durante la fase

### 3. Control de Parámetros del Agua y Tratamientos

Los técnicos acuícolas deben registrar diariamente:

- **Parámetros físico-químicos**: temperatura, pH, oxígeno, amonio, nitritos, turbidez
- **Aplicación de tratamientos**: antibióticos, probióticos, desinfectantes
- **Mantenimiento**: cambios de agua y mantenimiento de filtros
- **Fertilización**: aplicación de fertilizantes para fitoplancton
- **Condiciones climáticas**: que afectan los estanques

### 4. Gestión de Inventario de Peces

- Cada lote se identifica con un código único por estanque
- Los peces alcanzan peso comercial entre 250-400g según la especie
- Control de biomasa total por estanque (kg de peces por m³ de agua)
- Registro de muestreos de peso y talla semanales
- Control de conversión alimenticia (kg alimento / kg pez producido)

### 5. Clasificación y Control de Calidad

Los peces se clasifican según múltiples criterios:

#### Por Calidad
- **Clase A**: Peces perfectos, sin deformidades, coloración óptima
- **Clase B**: Peces comerciales con características aceptables
- **Clase C**: Peces con defectos menores, precio reducido

#### Por Peso/Talla
- **Pequeño (P)**: 150-250g
- **Mediano (M)**: 250-350g
- **Grande (G)**: 350-450g
- **Extra Grande (XG)**: Más de 450g

#### Por Especie
- **Trucha arcoíris**: Mercado premium, restaurantes
- **Carpa común**: Mercado popular, consumo masivo
- **Tilapia**: Mercado intermedio, supermercados

### 6. Control de Alimentación Diaria

El sistema debe registrar:

- Cantidad de alimento suministrado por estanque por día
- Tipo de alimento según la fase de crecimiento
- Horarios de alimentación y personal responsable
- Observación del comportamiento alimenticio de los peces
- Control de stock de alimentos en bodega

### 7. Control de Cosecha y Distribución

El sistema debe contemplar:

- Registro de cosechas por lote (fecha, cantidad, peso total)
- Clasificación de peces cosechados según criterios de calidad
- Control de peces procesados vs. peces vendidos vivos
- Personal responsable de la cosecha y procesamiento
- Destino de venta (mercado mayorista, restaurantes, supermercados)
- Registro de temperaturas durante transporte

> **Nota**: El sistema NO contempla el proceso de comercialización final, solo el control de producción y entrega al departamento de ventas ubicado en la planta de procesamiento en la ciudad.

---

## Consideraciones Adicionales Específicas

- Control de ciclos reproductivos para especies que se reproducen en la piscifactoría
- Registro de incidentes ambientales (mortalidad masiva, contaminación, depredadores)
- Reportes de productividad estacional y por especie

---

## Análisis de Entidades Principales

### Entidades Identificadas

1. **Lotes de Producción**
   - Código único, especie, fecha de siembra, cantidad de alevines, proveedor

2. **Estanques**
   - Tipos: pequeños (50m³), medianos (200m³), grandes (500m³)
   - Capacidad según fase

3. **Especies**
   - Trucha arcoíris, Carpa común, Tilapia

4. **Fases de Crecimiento**
   - Alevinaje, Juveniles, Pre-engorde, Engorde final

5. **Parámetros del Agua**
   - Temperatura, pH, oxígeno, amonio, nitritos, turbidez

6. **Tratamientos**
   - Antibióticos, probióticos, desinfectantes

7. **Alimentación**
   - Tipo, cantidad, horarios, personal responsable

8. **Cosechas**
   - Fecha, cantidad, peso total, clasificación

9. **Personal/Técnicos**
   - Responsables de diferentes actividades

10. **Proveedores**
    - Certificaciones sanitarias

11. **Clasificaciones de Calidad**
    - Por calidad (A/B/C), peso/talla (P/M/G/XG), especie

---

## Flujo de Proceso

```
1. Adquisición de alevines → Registro de lote
2. Siembra en estanque pequeño → Fase 1 (Alevinaje)
3. Traslado a estanque mediano → Fase 2 (Juveniles)
4. Traslado a estanque grande → Fase 3 (Pre-engorde)
5. Permanencia en engorde → Fase 4 (Engorde final)
6. Cosecha → Clasificación → Entrega a ventas
```

---

## Alcance del Sistema

### ✅ Incluye
- Control de producción desde siembra hasta entrega al departamento de ventas
- Registro de todas las fases de crecimiento
- Control de parámetros ambientales
- Gestión de alimentación e inventario
- Clasificación y control de calidad
- Reportes de productividad

### ❌ No Incluye
- Proceso de comercialización final
- Gestión de ventas al cliente final
- Facturación y cobranza

---

## Instrucciones para el Estudiante

Analizar el enunciado y completar el desarrollo del sistema de información. Puede aumentar atributos y realizar consideraciones necesarias siempre y cuando no contradiga el enunciado explícito del problema.
