# Competition Manager - Frontend React

Aplicación React para gestionar y visualizar competiciones deportivas conectada a Azure Functions.

## ?? Características

- ? Visualización de competiciones deportivas
- ?? Búsqueda por nombre, tipo y área geográfica
- ?? Estadísticas en tiempo real
- ?? Interfaz moderna y responsiva
- ? Conexión directa con Azure Functions
- ?? Soporte para múltiples filtros

## ?? Requisitos Previos

- Node.js 16+ y npm
- Azure Function ejecutándose (local o en Azure)

## ?? Instalación

### 1. Instalar dependencias

```bash
cd competition-frontend
npm install
```

### 2. Configurar variables de entorno

Edita el archivo `.env`:

```env
# Para desarrollo local
REACT_APP_API_URL=http://localhost:7232/api
REACT_APP_FUNCTION_KEY=

# Para producción (edita .env.production)
# REACT_APP_API_URL=https://tu-function-app.azurewebsites.net/api
# REACT_APP_FUNCTION_KEY=tu-function-key
```

## ????? Ejecutar en Desarrollo

```bash
npm start
```

La aplicación se abrirá automáticamente en [http://localhost:3000](http://localhost:3000)

## ?? Build para Producción

```bash
npm run build
```

Los archivos optimizados estarán en la carpeta `/build`.

## ?? Estructura del Proyecto

```
competition-frontend/
??? public/
?   ??? index.html
?   ??? manifest.json
??? src/
?   ??? components/
?   ?   ??? CompetitionCard.js  # Tarjeta de competición
?   ?   ??? CompetitionCard.css
?   ?   ??? SearchBar.js             # Barra de búsqueda
?   ?   ??? SearchBar.css
?   ??? services/
?   ?   ??? competitionService.js    # Cliente API
?   ??? App.js        # Componente principal
?   ??? App.css
?   ??? index.js
?   ??? index.css
??? .env      # Variables de entorno (desarrollo)
??? .env.production  # Variables de entorno (producción)
??? package.json
??? README.md
```

## ?? Configuración de Azure Function

### Desarrollo Local

1. Asegúrate de que tu Azure Function esté ejecutándose:
   ```bash
   cd ../
   func start
   ```

2. La URL por defecto será: `http://localhost:7232/api`

### Producción

1. Actualiza `.env.production` con tu URL de Azure:
   ```env
   REACT_APP_API_URL=https://tu-function-app.azurewebsites.net/api
 REACT_APP_FUNCTION_KEY=tu-function-key-aqui
   ```

2. Si tu Azure Function usa `AuthorizationLevel.Anonymous`, deja `REACT_APP_FUNCTION_KEY` vacío

## ?? Desplegar en Azure

### Opción 1: Azure Static Web Apps (Recomendado)

```bash
# Instalar Azure Static Web Apps CLI
npm install -g @azure/static-web-apps-cli

# Build de la aplicación
npm run build

# Desplegar
swa deploy ./build
```

### Opción 2: Azure App Service

```bash
# Build
npm run build

# Desplegar usando Azure CLI
az webapp up --name tu-webapp-name --resource-group tu-resource-group --html
```

### Opción 3: Azure Storage (Static Website)

```bash
# Build
npm run build

# Habilitar static website en tu storage account
az storage blob service-properties update --account-name tuaccount --static-website --index-document index.html

# Subir archivos
az storage blob upload-batch --account-name tuaccount --source ./build --destination '$web'
```

## ?? Funcionalidades

### Búsqueda y Filtros

- **Todas las competiciones**: Muestra todas las competiciones disponibles
- **Por nombre**: Busca competiciones por nombre (ej: "Champions", "Premier")
- **Por tipo**: Filtra por tipo de competición (LEAGUE o CUP)
- **Por área**: Filtra por región geográfica (ej: "Africa", "Europe")

### Información Mostrada

- Nombre y código de la competición
- Tipo (Liga o Copa)
- Área geográfica con bandera
- Temporada actual con fechas
- Jornada actual
- Número de temporadas disponibles
- Última actualización

## ??? Solución de Problemas

### Error: "Cannot connect to API"

1. Verifica que la Azure Function esté ejecutándose
2. Revisa la URL en `.env`
3. Comprueba que el CORS esté configurado en la Azure Function

### Error: "Unauthorized"

1. Verifica que `REACT_APP_FUNCTION_KEY` sea correcto
2. O cambia `AuthorizationLevel` a `Anonymous` en la Azure Function

### La aplicación no actualiza cambios

1. Detén el servidor (`Ctrl+C`)
2. Elimina `.env.local` si existe
3. Ejecuta `npm start` nuevamente

## ?? Responsive Design

La aplicación está optimizada para:
- ?? Móviles (< 768px)
- ?? Tablets (768px - 1024px)
- ??? Desktop (> 1024px)

## ?? Personalización

### Cambiar Colores

Edita `src/App.css`:

```css
/* Gradiente del header */
.app-header {
  background: linear-gradient(135deg, #TU_COLOR1 0%, #TU_COLOR2 100%);
}

/* Color primario de botones */
.btn-primary {
  background-color: #TU_COLOR;
}
```

### Agregar Nuevos Filtros

1. Actualiza `SearchBar.js` para agregar la opción
2. Modifica `App.js` para manejar el nuevo filtro
3. Asegúrate de que el backend soporte el filtro

## ?? Performance

- ? Code splitting automático
- ? Lazy loading de imágenes
- ? Optimización de bundle con Create React App
- ? Caché de peticiones HTTP

## ?? Seguridad

- No expongas tu `REACT_APP_FUNCTION_KEY` en repositorios públicos
- Usa variables de entorno para información sensible
- Considera usar Azure AD para autenticación en producción

## ?? Licencia

Este proyecto es de código abierto.

## ?? Contribuir

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## ?? Soporte

Si tienes problemas:
1. Revisa la consola del navegador para errores
2. Verifica que la Azure Function responda correctamente
3. Comprueba la configuración de CORS

---

Desarrollado con ?? usando React + Azure Functions
