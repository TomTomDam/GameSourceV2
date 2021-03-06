import React from "react";
import { Link } from "react-router-dom";

const Card = (props) => {
  const games = props.games;

  return games.map((game) => (
    <div className="row">
      <div key={game.id} className="col-3">
        <Link to={`/games/${game.id}`} className="nav-link dropdown-item">
          <span className="text-white">{game.name}</span>
        </Link>
      </div>
    </div>
  ));
};

export default Card;
