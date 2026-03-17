CARATULA

INDICE
CAPÍTULO I: ASPECTOS GENERALES
1.1 Título del proyecto
Desarrollo de un Sistema de Gestión Integral de Piscicultura mediante Blazor Hybrid y SQL Server para la empresa "Lagos Andinos" SRL.
1.2 Introducción
La acuicultura en la región andina ha pasado de ser una actividad artesanal a una industria que exige precisión técnica para ser rentable. En este contexto, la empresa "Lagos Andinos" SRL se dedica a la crianza de trucha, carpa y tilapia, procesos que requieren un monitoreo riguroso de variables ambientales y biológicas.
El presente proyecto propone el desarrollo de una solución tecnológica moderna utilizando el stack de Microsoft. La implementación de una aplicación Blazor Hybrid permitirá a los técnicos capturar datos en tiempo real desde dispositivos móviles en los estanques, sincronizándolos con una base de datos centralizada en SQL Server. Esto garantiza que la información sobre alimentación, calidad del agua y crecimiento de los lotes esté disponible para la toma de decisiones estratégicas de manera inmediata.
1.3 Planteamiento del problema
Actualmente, "Lagos Andinos" SRL carece de una plataforma digital centralizada para el control de su producción. La dependencia de registros manuales y la dispersión de la información generan los siguientes conflictos operativos:
Incertidumbre en la Trazabilidad: Dificultad para rastrear el ciclo de vida completo de un lote, desde su ingreso como alevín hasta su cosecha.
Riesgo de Mortandad: La falta de alertas tempranas sobre parámetros críticos del agua (pH, Oxígeno, Temperatura) puede derivar en pérdidas económicas considerables.
Descontrol en la Alimentación: El uso ineficiente del concentrado alimenticio al no estar ajustado dinámicamente a la etapa de crecimiento y condiciones del estanque.
Información Desactualizada: El retraso entre la toma de datos en campo y su análisis en oficina impide una reacción ágil ante imprevistos biológicos.
1.4 Delimitaciones
1.4.1. Delimitación espacial o geográfica
El proyecto se desarrollará y aplicará específicamente para las instalaciones de la piscifactoría "Lagos Andinos" SRL, ubicada en la zona de influencia de la región andina (Bolivia).
1.4.2. Delimitación temporal
El desarrollo del sistema se llevará a cabo en un periodo de 4 meses, comprendidos entre marzo y junio de 2026, abarcando desde el relevamiento de requisitos hasta las pruebas finales de producción.
1.4.3. Delimitación tecnológica
Lenguaje de Programación: C# (.NET 9).
Framework de Frontend: Blazor Hybrid (MAUI para soporte multiplataforma).
Framework de Backend: ASP.NET Core Web API.
Motor de Base de Datos: SQL Server 2022.
Entorno de Desarrollo: Visual Studio 2022 Community.
1.5 Objetivo General
Desarrollar e implementar un sistema de gestión de piscicultura para "Lagos Andinos" SRL utilizando Blazor Hybrid y SQL Server, que permita automatizar el control de lotes de producción, el monitoreo de parámetros de agua y la gestión de alimentación para optimizar la trazabilidad y eficiencia operativa de la empresa.
1.6 Objetivos Específicos
Identificar y documentar los requerimientos funcionales y no funcionales de los procesos de cría, monitoreo y cosecha mediante historias de usuario.
Diseñar la arquitectura del sistema y el modelo de datos relacional en SQL Server, aplicando normas de integridad y normalización.
Construir el Product Backlog priorizado y realizar la estimación de esfuerzo utilizando técnicas de Planning Poker bajo la metodología XP + SCRUM.
Codificar los módulos de gestión de lotes, parámetros ambientales y reportes utilizando C# y Blazor Hybrid.
Establecer un plan de mantenimiento y respaldo de datos para garantizar la confiabilidad y seguridad de la información crítica de la piscifactoría.

3.1 Fase de exploración
En esta etapa inicial de la metodología XP, se llevó a cabo el levantamiento de información mediante entrevistas directas con los responsables de la piscifactoría. El objetivo fue comprender el flujo biológico y operativo de la crianza de trucha, carpa y tilapia.
Se estableció la metáfora del sistema, visualizando el software como una "Bitácora de Ciclo Vital". En este concepto, los lotes de peces fluyen a través de estaciones (estanques) donde se monitorea su salud y alimentación hasta alcanzar la madurez comercial. Esta fase permitió identificar los procesos críticos que se transformarán en Historias de Usuario para el Product Backlog.
3.1.1 Requerimientos No funcionales
Los requerimientos no funcionales definen los atributos de calidad y las restricciones técnicas bajo las cuales operará el sistema de gestión:
Categoría
Descripción
Seguridad
El sistema debe implementar un esquema de autenticación y autorización basado en roles para asegurar que solo personal autorizado gestione datos sensibles de producción.
Disponibilidad
Al tratarse de un entorno de monitoreo biológico, la base de datos en SQL Server y los servicios deben garantizar una disponibilidad del 99.5% para evitar vacíos de información.
Rendimiento
El procesamiento de registros de alimentación y parámetros de agua debe realizarse de forma asíncrona, asegurando tiempos de respuesta inferiores a 2 segundos.
Compatibilidad
Gracias al uso de Blazor Hybrid, el sistema debe garantizar una experiencia de usuario consistente tanto en dispositivos móviles (Android/iOS) como en sistemas de escritorio (Windows).
Escalabilidad
La arquitectura debe permitir el crecimiento del número de estanques y registros de monitoreo sin degradar el rendimiento general de la aplicación.


3.1.2 Requerimientos Funcionales

ID
Requerimiento
Descripción
RF1
Gestión de Lotes y Especies
Permite el registro y seguimiento de lotes de alevines, vinculándolos a proveedores certificados y especies específicas (Trucha, Carpa, Tilapia).
RF2
Gestión de Estanques e Inventarios
Administra la disponibilidad física de los estanques y el stock de alimento concentrado, generando alertas automáticas de reabastecimiento.
RF3
Gestión de Monitoreo y Alimentación
Registra diariamente los parámetros físico-químicos del agua y la cantidad de alimento suministrado para optimizar el crecimiento biológico.
RF4
Gestión de Biometrías y Cosecha
Realiza el seguimiento de peso y talla para programar la cosecha final, clasificando el producto por categorías comerciales de tamaño y peso.
RF5
Gestión de Seguridad y Reportes
Administra el acceso de usuarios por roles y genera informes de rendimiento que faciliten la toma de decisiones estratégicas.



3.1.3 Diagrama de clases conceptuales (Lógica del Negocio)

