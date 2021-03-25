import React from "react";
import Genres from "./Genres";
import Platforms from "./Platforms";

const BrowseGames = () => {
  return (
    <>
      <div className="dropdown-header fw-bold">By Genre</div>
      <span className="divider"></span>
      <Genres />
      <div className="dropdown-header fw-bold">By Platform</div>
      <span className="divider"></span>
      <Platforms />
    </>
  );
};

export default BrowseGames;
