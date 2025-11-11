import React, { useState, useEffect } from 'react';
import competitionService from './services/competitionService';
import CompetitionCard from './components/CompetitionCard';
import SearchBar from './components/SearchBar';
import './App.css';

function App() {
const [competitions, setCompetitions] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [stats, setStats] = useState({ count: 0 });

  // Cargar todas las competiciones al inicio
  useEffect(() => {
    loadAllCompetitions();
  }, []);

  const loadAllCompetitions = async () => {
    setLoading(true);
    setError(null);
    try {
   const data = await competitionService.getAllCompetitions();
      if (data.competitions) {
      setCompetitions(data.competitions);
        setStats({ count: data.count || data.competitions.length });
      } else {
        setCompetitions([]);
        setStats({ count: 0 });
      }
    } catch (err) {
      setError('Error al cargar las competiciones. Verifica que la API esté funcionando.');
   console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const handleSearch = async (filters) => {
  setLoading(true);
    setError(null);

    try {
      let data;

      // Si no hay filtros, cargar todas
      if (Object.keys(filters).length === 0) {
        data = await competitionService.getAllCompetitions();
    } else if (filters.name) {
  data = await competitionService.getCompetitionsByName(filters.name);
      } else if (filters.type) {
        data = await competitionService.getCompetitionsByType(filters.type);
      } else if (filters.area) {
    data = await competitionService.getCompetitionsByArea(filters.area);
      } else {
    data = await competitionService.searchCompetitions(filters);
 }

      if (data.competitions) {
        setCompetitions(data.competitions);
        setStats({ count: data.count || data.competitions.length });
      } else {
        setCompetitions([]);
        setStats({ count: 0 });
  }
    } catch (err) {
      setError('Error al buscar competiciones. Intenta nuevamente.');
   console.error(err);
setCompetitions([]);
      setStats({ count: 0 });
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="app">
      <header className="app-header">
  <div className="header-content">
          <h1>? Competition Manager</h1>
          <p>Explora competiciones deportivas de todo el mundo</p>
      </div>
      </header>

      <main className="app-main">
        <div className="container">
  <SearchBar onSearch={handleSearch} loading={loading} />

     {error && (
    <div className="error-message">
         <span className="error-icon">??</span>
          <p>{error}</p>
      <button onClick={loadAllCompetitions} className="retry-button">
   Reintentar
          </button>
      </div>
          )}

      {loading && !error && (
            <div className="loading-container">
    <div className="loading-spinner"></div>
              <p>Cargando competiciones...</p>
         </div>
  )}

          {!loading && !error && (
     <>
    <div className="stats-bar">
            <span className="stats-count">
     ?? {stats.count} competición(es) encontrada(s)
        </span>
            </div>

   {competitions.length === 0 ? (
     <div className="empty-state">
<div className="empty-icon">??</div>
                  <h3>No se encontraron competiciones</h3>
  <p>Intenta con otros criterios de búsqueda</p>
       </div>
   ) : (
         <div className="competitions-grid">
        {competitions.map((competition) => (
     <CompetitionCard
               key={`${competition.id || competition.CompetitionId}-${competition.code}`}
         competition={competition}
      />
    ))}
     </div>
   )}
    </>
          )}
        </div>
      </main>

      <footer className="app-footer">
 <p>
          Desarrollado con ? Azure Functions + React | 
          Datos en tiempo real desde Cosmos DB
 </p>
      </footer>
    </div>
  );
}

export default App;
