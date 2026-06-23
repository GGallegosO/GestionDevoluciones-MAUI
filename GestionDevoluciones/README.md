# 📦 Sistema de Gestión de Devoluciones (App Móvil)

Aplicación móvil multiplataforma desarrollada en **.NET MAUI** utilizando el patrón de arquitectura **MVVM** (Model-View-ViewModel). Diseñada para optimizar el registro de devoluciones de clientes, asegurar la trazabilidad del proceso y mantener actualizado el inventario en tiempo real.

## 🚀 Características Principales

* **Arquitectura Limpia (MVVM):** Separación estricta entre la lógica de negocio y la interfaz de usuario.
* **Gestión de Inventario Dinámico:** Ajuste automático de stock (SKU) tras la confirmación de una devolución procesada.
* **Catálogo Inteligente:** Detección automática de productos existentes y creación dinámica de nuevos registros (Marca y Modelo) al ingresar devoluciones de artículos desconocidos.
* **Búsqueda en Tiempo Real:** Filtros integrados (`SearchBar`) en todas las vistas (Pendientes, Concluidas e Inventario) sin mutar la data original de respaldo.
* **Prevención de Errores (UX):** Uso de ventanas modales y alertas nativas (`PushModalAsync` y `DisplayPromptAsync`) para confirmar acciones críticas.
* **Trazabilidad Completa:** Historial detallado de productos devueltos, motivos, fechas de ingreso y estatus de reembolso.

## 🛠️ Tecnologías Utilizadas

* **Framework:** .NET MAUI (.NET 10.0)
* **Lenguaje:** C#
* **Interfaz Gráfica:** XAML
* **Patrón de Diseño:** MVVM
* **Entorno de Desarrollo:** JetBrains Rider / Visual Studio

## 🧑‍💻 Autor

* **Gustavo Gallegos** - *Estudiante de Analista Programador, Instituto Profesional Santo Tomás.*

---
*Proyecto académico desarrollado para la asignatura de Programación .NET.*