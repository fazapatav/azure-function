import axios from 'axios';

// URL base de tu Azure Function
const API_BASE_URL = process.env.REACT_APP_API_URL || 'http://localhost:7232/api';
const FUNCTION_KEY = process.env.REACT_APP_FUNCTION_KEY || '';

// Crear instancia de axios con configuración base
const apiClient = axios.create({
  baseURL: API_BASE_URL,
  timeout: 10000,
headers: {
    'Content-Type': 'application/json',
  },
});

// Interceptor para agregar el function key si existe
apiClient.interceptors.request.use((config) => {
  if (FUNCTION_KEY) {
    config.params = {
      ...config.params,
      code: FUNCTION_KEY,
    };
  }
  return config;
});

/**
 * Servicio para interactuar con la API de competiciones
 */
const competitionService = {
  /**
   * Obtiene todas las competiciones
   */
  getAllCompetitions: async () => {
    try {
      const response = await apiClient.get('/GetCompetitions');
      return response.data;
    } catch (error) {
      console.error('Error fetching all competitions:', error);
throw error;
    }
  },

  /**
   * Filtra competiciones por nombre
   * @param {string} name - Nombre o parte del nombre a buscar
   */
  getCompetitionsByName: async (name) => {
    try {
      const response = await apiClient.get('/GetCompetitions', {
        params: { name },
    });
      return response.data;
    } catch (error) {
      console.error('Error fetching competitions by name:', error);
throw error;
    }
  },

  /**
   * Filtra competiciones por tipo
   * @param {string} type - Tipo de competición (LEAGUE, CUP)
   */
  getCompetitionsByType: async (type) => {
    try {
      const response = await apiClient.get('/GetCompetitions', {
        params: { type },
 });
      return response.data;
    } catch (error) {
      console.error('Error fetching competitions by type:', error);
      throw error;
    }
  },

  /**
   * Filtra competiciones por área geográfica
   * @param {string} area - Nombre del área o continente
   */
  getCompetitionsByArea: async (area) => {
    try {
      const response = await apiClient.get('/GetCompetitions', {
        params: { area },
      });
 return response.data;
    } catch (error) {
      console.error('Error fetching competitions by area:', error);
      throw error;
    }
  },

  /**
   * Obtiene una competición específica por ID
   * @param {string} id - ID del documento
   */
  getCompetitionById: async (id) => {
    try {
      const response = await apiClient.get('/GetCompetitions', {
        params: { id },
      });
      return response.data;
    } catch (error) {
   console.error('Error fetching competition by id:', error);
      throw error;
    }
  },

  /**
   * Busca competiciones con múltiples filtros
   * @param {Object} filters - Objeto con los filtros a aplicar
   */
  searchCompetitions: async (filters) => {
    try {
      const response = await apiClient.get('/GetCompetitions', {
      params: filters,
      });
      return response.data;
    } catch (error) {
      console.error('Error searching competitions:', error);
    throw error;
    }
  },
};

export default competitionService;
