# Juego del Impostor (Real-time Web Game)

Un juego multijugador en tiempo real inspirado en "Among Us" o "Spyfall", construido con **ASP.NET Core 10**, **SignalR** y **Tailwind CSS**.

## 🎮 Cómo Jugar

1.  **Unirse**: Ingresa tu nombre. El primer jugador en entrar se convierte automáticamente en **ADMIN**.
2.  **Sala de Espera (Lobby)**:
    *   Todos los jugadores ven la lista de conectados en tiempo real.
    *   **Admin**: Configura el "Tema" (ej. Hospital, Banco) o déjalo vacío para un **Tema Aleatorio**.
    *   **Admin**: Define la cantidad de Impostores.
3.  **Iniciar**: El Admin inicia la partida.
4.  **El Juego**:
    *   **Civiles**: Ven el TEMA secreto (Todos ven el mismo lugar).
    *   **Impostor**: Ve una tarjeta ROJA que dice "Eres el IMPOSTOR" y NO ve el tema.
    *   *Objetivo*: Los civiles deben descubrir quién no sabe el tema. El impostor debe fingir que sabe dónde están.
5.  **Reiniciar**: El Admin puede presionar "Salir / Nueva Partida" para reiniciar el juego en **todos los dispositivos conectados**.

## 🚀 Despliegue Gratuito (Render.com)

Este proyecto está configurado con `Dockerfile` para ser desplegado fácilmente en **Render**:

1.  Sube este código a tu repositorio de **GitHub**.
2.  Crea una cuenta en [Render.com](https://render.com).
3.  Crea un nuevo **Web Service**.
4.  Conecta tu repositorio de GitHub.
5.  Selecciona **Docker** como Runtime.
6.  Elige el plan **Free**.

> **Nota**: En el plan gratuito, el servidor se "duerme" tras 15 minutos de inactividad. La primera vez que entres tardará unos 50 segundos en despertar.

## 🛠️ Tecnologías

*   **Backend**: .NET 10 (Minimal APIs)
*   **Real-time**: SignalR
*   **Frontend**: HTML5 + JavaScript (Vanilla)
*   **Estilos**: Tailwind CSS (CDN)
*   **Infraestructura**: Docker

## 💻 Ejecutar Localmente

Requisitos: tener instalado el [.NET SDK](https://dotnet.microsoft.com/download).

```bash
# Clonar o descargar el código
git clone https://github.com/TU_USUARIO/juego-impostor.git
cd juego-impostor/Antigravity

# Ejecutar el servidor
dotnet run --urls=http://localhost:5050
```

¡Abre tu navegador en `http://localhost:5050` y juega!
