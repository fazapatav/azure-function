import React, { useState } from 'react';
import './SearchBar.css';

const SearchBar = ({ onSearch, loading }) => {
  const [searchType, setSearchType] = useState('all');
  const [searchValue, setSearchValue] = useState('');

  const handleSearch = (e) => {
    e.preventDefault();
    
    const filters = {};
    
    if (searchType !== 'all' && searchValue.trim()) {
      filters[searchType] = searchValue.trim();
    }
    
    onSearch(filters);
  };

  const handleReset = () => {
    setSearchType('all');
  setSearchValue('');
    onSearch({});
  };

  return (
    <div className="search-bar">
      <form onSubmit={handleSearch} className="search-form">
  <div className="search-inputs">
          <select
            value={searchType}
            onChange={(e) => setSearchType(e.target.value)}
     className="search-select"
    disabled={loading}
          >
            <option value="all">Todas las competiciones</option>
     <option value="name">Buscar por nombre</option>
    <option value="type">Filtrar por tipo</option>
            <option value="area">Filtrar por área</option>
   </select>

   {searchType !== 'all' && searchType !== 'type' && (
          <input
        type="text"
         value={searchValue}
          onChange={(e) => setSearchValue(e.target.value)}
  placeholder={
         searchType === 'name'
        ? 'Ej: Champions, Premier...'
       : 'Ej: Africa, Europe...'
        }
       className="search-input"
          disabled={loading}
/>
          )}

          {searchType === 'type' && (
            <select
    value={searchValue}
              onChange={(e) => setSearchValue(e.target.value)}
   className="search-select"
 disabled={loading}
            >
            <option value="">Seleccionar tipo</option>
      <option value="LEAGUE">Liga</option>
    <option value="CUP">Copa</option>
            </select>
      )}

          <div className="search-buttons">
      <button
        type="submit"
    className="btn btn-primary"
      disabled={loading || (searchType !== 'all' && !searchValue)}
            >
  {loading ? (
    <span className="spinner"></span>
     ) : (
           <>?? Buscar</>
     )}
            </button>

    {(searchType !== 'all' || searchValue) && (
  <button
                type="button"
     onClick={handleReset}
       className="btn btn-secondary"
                disabled={loading}
      >
              ?? Limpiar
     </button>
      )}
        </div>
        </div>
      </form>
    </div>
  );
};

export default SearchBar;
