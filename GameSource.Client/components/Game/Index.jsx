import React, { useState, useEffect } from "react";
import axios from "axios";
import Card from "./Card";

const Index = () => {
  const gameApi = "https://localhost:44336/api/games/";
  const [games, setGames] = useState([]);
  const [isLoading, setLoading] = useState(true);

  useEffect(() => {
    axios
      .get(gameApi)
      .then((res) => {
        setGames(res.data.data);
        setLoading(false);
      })
      .catch((err) => {
        console.log(err);
      });
  }, []);

  if (isLoading) {
    return <li>Loading...</li>;
  }

  return (
    <>
      <h1 className="text-center">All Games</h1>
      <Card games={games} />
    </>
  );
};

export default Index;
