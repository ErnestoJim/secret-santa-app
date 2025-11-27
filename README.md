````markdown
# Secret Santa App (.NET MAUI)

Aplicaci�n multi-plataforma (.NET MAUI) para gestionar un sorteo de Amigo Secreto familiar.

## Caracter�sticas
- Registro de participantes (Nombre completo + Email).
- Definici�n de restricciones (A no puede regalar a B).
- Generaci�n de asignaciones aleatorias respetando restricciones (no se muestran en pantalla para mantener el secreto).
- Env�o de correos con el resultado (l�mite de 40� + fecha de entrega + mensaje navide�o).
- Gesti�n de m�ltiples sorteos: guardar, cargar y eliminar (participantes + restricciones) en almacenamiento local (JSON).
- Solicitud segura de contrase�a SMTP mediante modal + SecureStorage.

## Requisitos
- .NET 9 SDK
- Workload .NET MAUI instalado
- Cuenta SMTP (Gmail con App Password, Outlook, SendGrid, etc.)

## Configuraci�n SMTP
1. Gmail: activa 2FA y crea un App Password para "Mail".
2. Edita `MainPage.xaml.cs` si cambias Host/Port/Username/FromEmail.
3. Contrase�a se pide en runtime y se guarda cifrada (SecureStorage).
4. Bot�n "Reset Password" elimina contrase�a almacenada.

## Uso
1. Introduce nombre del sorteo y opcionalmente guarda.
2. Agrega participantes y restricciones.
3. Pulsa "Generar Asignaciones".
4. Pulsa "Enviar Correos" y escribe la App Password SMTP.
5. Cada participante recibe su asignaci�n sin mostrarse en la app.

## Persistencia
- Sorteos guardados en `draws.json` (excluido del repo) en AppDataDirectory.
- Incluye participantes y restricciones; no incluye asignaciones.

## Seguridad
- No hay credenciales en c�digo fuente.
- `.gitignore` excluye bin/ obj/ .vs/ draws.json y trazas de herramientas.
- Contrase�a SMTP cifrada con SecureStorage.

## Estructura
```
Models/Participant.cs
Models/SecretSantaDraw.cs
Services/SecretSantaService.cs
Services/DrawStorageService.cs
Services/SmtpEmailSender.cs
MainPage.xaml / MainPage.xaml.cs
PasswordPromptPage.xaml.cs
```

## Ejecutar
```bash
dotnet build
dotnet run -f net9.0-windows10.0.19041.0
```

## Personalizaci�n
- Cambia el cuerpo y asunto del correo en `OnSendEmailsClicked`.
- Ajusta l�mite o fecha de entrega.
- Mostrar asignaciones (solo desarrollo): poner `IsVisible="True"` a `AssignmentsCollection`.

## Futuras mejoras
- Exportar a CSV.
- Validaci�n avanzada de email.
- Integraci�n con API de env�o (SendGrid).
- Modo prueba sin env�o real.

## Licencia
Agregar archivo `LICENSE` (recomendado MIT) antes de publicar.

Felices fiestas ??

````
