import React from "react";
import Genres from "./Genres";
import Platforms from "./Platforms";

const BrowseGames = () => {
  return (
    <>
      <span className="dropdown-header fw-bold">By Genre</span>
      <span className="divider"></span>
      <Genres />
      <span className="dropdown-header fw-bold">By Platform</span>
      <span className="divider"></span>
      <Platforms />
    </>
  );
};

export default BrowseGames;
