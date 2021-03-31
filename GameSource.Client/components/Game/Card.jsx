import React from "react";
import { Link } from "react-router-dom";

const Card = (props) => {
  const games = props.games;

  return games.map((game) => (
    <div className="row">
      <div key={game.id} className="col-sm">
        <Link to={`/games/${game.id}`} className="nav-link dropdown-item">
          <span>{game.name}</span>
        </Link>
      </div>
    </div>
  ));
};

export default Card;
