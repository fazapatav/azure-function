# Azure Function - Integración con Cosmos DB + Frontend React

## ?? Descripción
Solución completa con Azure Function (backend) conectada a Cosmos DB y una aplicación React (frontend) para gestionar competiciones deportivas.

## ?? Estructura del Proyecto

```
azure function/
??? competition/           # Backend - Azure Function (.NET 8)
?   ??? Function1.cs
?   ??? DiagnosticFunction.cs
?   ??? Models/
?   ?   ??? CompetitionData.cs
?   ??? Services/
?   ?   ??? CosmosDbService.cs
?   ??? local.settings.json
?   ??? host.json
?   ??? competition.csproj
?   ??? README.md
?   ??? start-frontend.ps1
?   ??? GUIA-INICIO.ps1
?   ??? COMO-USAR.md
?   ??? SOLUCION-ERROR-404.md
?
??? competition-frontend/  # Frontend - React App
    ??? src/
    ?   ??? components/
    ?   ?   ??? CompetitionCard.js
    ?   ?   ??? SearchBar.js
  ?   ??? services/
    ?   ?   ??? competitionService.js
  ?   ??? App.js
    ?   ??? index.js
  ??? public/
    ??? package.json
 ??? .env
    ??? README.md
```

## ?? Componentes

### Backend (competition/)
- **Azure Function** con .NET 8
- **Cosmos DB** para almacenamiento
- **API REST** con múltiples endpoints de consulta

### Frontend (competition-frontend/)
- **React 18** con hooks modernos
- **Axios** para consumir la API
- **Diseño responsivo** y moderno
- **Búsqueda y filtros** dinámicos

## ?? Requisitos
- .NET 8
- Azure Functions Runtime v4
- Azure Cosmos DB account
- Node.js 16+ y npm
- Azure Functions Core Tools (opcional)

## ?? Inicio Rápido

### Opción 1: Script Automático (Recomendado)

```powershell
# Desde la carpeta competition/
cd competition

# Ver guía completa
.\GUIA-INICIO.ps1

# Iniciar frontend automáticamente
.\start-frontend.ps1
```

### Opción 2: Manual

#### 1. Configurar Backend

```bash
# Desde la carpeta competition/
cd competition

# Configurar local.settings.json con tus credenciales de Cosmos DB

# Ejecutar Azure Function
func start
# O presionar F5 en Visual Studio
```

#### 2. Configurar Frontend

```bash
# Desde la raíz (azure function/)
cd competition-frontend

# Instalar dependencias
npm install

# Configurar .env con la URL de tu API

# Iniciar aplicación
npm start
```

## ?? Configuración

### 1. Variables de Entorno (Backend)

Actualiza `competition/local.settings.json`:

```json
{
  "Values": {
 "CosmosDbConnectionString": "AccountEndpoint=https://YOUR-ACCOUNT.documents.azure.com:443/;AccountKey=YOUR-KEY;",
    "CosmosDbDatabaseName": "competitions",
    "CosmosDbContainerName": "football"
  }
}
```

### 2. Variables de Entorno (Frontend)

Actualiza `competition-frontend/.env`:

```env
# Desarrollo local
REACT_APP_API_URL=http://localhost:7232/api
REACT_APP_FUNCTION_KEY=

# Producción (editar .env.production)
# REACT_APP_API_URL=https://tu-function-app.azurewebsites.net/api
# REACT_APP_FUNCTION_KEY=tu-function-key
```

### 3. CORS Configuration

El archivo `competition/host.json` ya incluye la configuración de CORS para permitir peticiones desde:
- `http://localhost:3000` (desarrollo)
- Tu dominio de producción

## ?? Desarrollo Local

### Terminal 1: Backend (Azure Function)
```bash
cd competition
func start
# O ejecutar desde Visual Studio (F5)
```
? Backend disponible en: `http://localhost:7232`

### Terminal 2: Frontend (React)
```bash
cd competition-frontend
npm start
```
? Frontend disponible en: `http://localhost:3000`

## ?? Endpoints

### Obtener todas las competiciones
```http
GET http://localhost:7232/api/GetCompetitions
```

### Filtrar por nombre
```http
GET http://localhost:7232/api/GetCompetitions?name=champions
```

### Filtrar por tipo (LEAGUE, CUP)
```http
GET http://localhost:7232/api/GetCompetitions?type=LEAGUE
```

### Filtrar por área geográfica
```http
GET http://localhost:7232/api/GetCompetitions?area=Africa
```

### Obtener un documento específico por ID
```http
GET http://localhost:7232/api/GetCompetitions?id=YOUR_DOCUMENT_ID
```

### Diagnóstico de Cosmos DB
```http
GET http://localhost:7232/api/DiagnosticCosmosDb
```

## ?? Características del Frontend

### Búsqueda y Filtros
- ? Búsqueda por nombre de competición
- ? Filtro por tipo (Liga/Copa)
- ? Filtro por área geográfica
- ? Visualización de todas las competiciones

### Interfaz
- ?? Diseño 100% responsivo
- ?? UI moderna con gradientes
- ? Loading states y manejo de errores
- ?? Estadísticas en tiempo real
- ?? Tarjetas con información detallada

### Datos Mostrados
- Nombre y código de competición
- Tipo y área geográfica
- Bandera del país/región
- Temporada actual con fechas
- Jornada actual
- Número de temporadas disponibles
- Última actualización

## ??? Troubleshooting

### ? Error de CORS
```
Access to fetch has been blocked by CORS policy
```
**Solución:**
1. Verifica que `competition/host.json` incluya tu dominio en `allowedOrigins`
2. Reinicia la Azure Function

### ? Error 404 en Cosmos DB
```
NotFound (404); Substatus: 1003; "Owner resource does not exist"
```
**Solución:**
1. Ejecuta: `http://localhost:7232/api/DiagnosticCosmosDb`
2. Verifica los nombres de database y container
3. Consulta `competition/SOLUCION-ERROR-404.md`

### ? Frontend no se conecta al backend
```
Network Error / Cannot connect to API
```
**Solución:**
1. Verifica que la Azure Function esté ejecutándose
2. Revisa la URL en `competition-frontend/.env`
3. Abre la consola del navegador para más detalles

### ? "npm: command not found"
**Solución:**
1. Instala Node.js desde https://nodejs.org/
2. Reinicia la terminal
3. Verifica: `node --version` y `npm --version`

## ?? Estructura de Datos en Cosmos DB

El documento en Cosmos DB debe tener la siguiente estructura:

```json
{
  "id": "unique-document-id",
  "count": 173,
  "filters": {},
  "competitions": [
    {
      "id": 2006,
      "area": {
        "id": 2001,
        "name": "Africa",
"code": "AFR",
        "flag": null
      },
   "name": "WC Qualification CAF",
      "code": "QCAF",
      "type": "CUP",
      "emblem": null,
      "plan": "TIER_FOUR",
      "currentSeason": {
   "id": 555,
 "startDate": "2019-09-04",
        "endDate": "2021-11-16",
   "currentMatchday": 6,
     "winner": null
      },
      "numberOfAvailableSeasons": 3,
      "lastUpdated": "2022-03-13T18:51:44Z"
    }
  ]
}
```

## ?? Desplegar en Producción

### Backend (Azure Function)

```bash
# Desde Visual Studio: Clic derecho ? Publish
# O usando Azure CLI:
cd competition
func azure functionapp publish tu-function-app-name
```

### Frontend (React)

#### Opción 1: Azure Static Web Apps
```bash
cd competition-frontend
npm run build
swa deploy ./build
```

#### Opción 2: Azure App Service
```bash
cd competition-frontend
npm run build
az webapp up --name tu-webapp-name --resource-group tu-resource-group
```

### Configuración Post-Despliegue

1. Actualiza las variables de entorno en Azure Portal
2. Configura CORS en tu Function App
3. Actualiza `REACT_APP_API_URL` en el frontend

## ??? Consideraciones de Seguridad

- **NUNCA** subas `local.settings.json` al repositorio (ya está en `.gitignore`)
- **NUNCA** expongas tu `REACT_APP_FUNCTION_KEY` en repositorios públicos
- Usa **Azure Key Vault** para almacenar connection strings en producción
- Configura **CORS** apropiadamente (solo dominios necesarios)
- Considera usar **Managed Identity** en lugar de connection strings
- Implementa **Azure AD** para autenticación en producción

## ?? Mejoras Futuras

- [ ] Implementar caché con Azure Redis
- [ ] Agregar paginación para grandes conjuntos de datos
- [ ] Implementar autenticación con Azure AD
- [ ] Agregar Application Insights para monitoreo
- [ ] Crear tests unitarios e integración
- [ ] Agregar más filtros y búsquedas avanzadas
- [ ] Implementar modo oscuro en el frontend
- [ ] Agregar favoritos y personalización

## ?? Documentación Adicional

- [Backend README](competition/README.md)
- [Frontend README](../competition-frontend/README.md)
- [Solución Error 404](competition/SOLUCION-ERROR-404.md)
- [Guía de Inicio](competition/GUIA-INICIO.ps1)
- [Cómo Usar](competition/COMO-USAR.md)

## ?? Licencia
Este proyecto es de código abierto.

## ?? Contribuir

Las contribuciones son bienvenidas. Por favor:
1. Fork el proyecto
2. Crea una rama para tu feature
3. Commit tus cambios
4. Push a la rama
5. Abre un Pull Request

---

**Desarrollado con ?? usando .NET 8 + Azure Functions + React + Cosmos DB**
