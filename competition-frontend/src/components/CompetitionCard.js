import React from 'react';
import './CompetitionCard.css';

const CompetitionCard = ({ competition }) => {
  const formatDate = (dateString) => {
    if (!dateString) return 'N/A';
    const date = new Date(dateString);
    return date.toLocaleDateString('es-ES', {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
    });
  };

  return (
    <div className="competition-card">
      <div className="competition-header">
        <div className="competition-title">
     <h3>{competition.name}</h3>
          <span className={`competition-type ${competition.type?.toLowerCase()}`}>
   {competition.type}
    </span>
 </div>
        {competition.emblem && (
    <img 
   src={competition.emblem} 
    alt={`${competition.name} emblem`}
      className="competition-emblem"
      />
     )}
      </div>

      <div className="competition-details">
     <div className="detail-row">
          <span className="detail-label">Área:</span>
   <span className="detail-value">
   {competition.area?.flag && (
          <img 
     src={competition.area.flag} 
                alt={competition.area.name}
                className="area-flag"
              />
            )}
  {competition.area?.name} ({competition.area?.code})
          </span>
        </div>

        <div className="detail-row">
        <span className="detail-label">Código:</span>
        <span className="detail-value">{competition.code}</span>
     </div>

        <div className="detail-row">
 <span className="detail-label">Plan:</span>
        <span className="detail-value">{competition.plan}</span>
        </div>

        {competition.currentSeason && (
<div className="current-season">
            <h4>Temporada Actual</h4>
          <div className="season-info">
           <div className="season-dates">
   <span>?? Inicio: {formatDate(competition.currentSeason.startDate)}</span>
      <span>?? Fin: {formatDate(competition.currentSeason.endDate)}</span>
   </div>
           {competition.currentSeason.currentMatchday && (
          <div className="matchday">
                  Jornada: {competition.currentSeason.currentMatchday}
     </div>
              )}
  </div>
</div>
        )}

   <div className="competition-footer">
  <span className="seasons-count">
     ?? {competition.numberOfAvailableSeasons} temporada(s) disponible(s)
   </span>
          <span className="last-updated">
        Actualizado: {formatDate(competition.lastUpdated)}
          </span>
   </div>
   </div>
    </div>
  );
};

export default CompetitionCard;
