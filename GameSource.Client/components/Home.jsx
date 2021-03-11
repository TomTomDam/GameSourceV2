import React, { useState, useEffect } from "react";
import axios from "axios";

const Home = () => {
  const genreApi = "https://localhost:44336/api/genres/";
  const [genres, setGenres] = useState([]);

  useEffect(() => {
    axios
      .get(genreApi)
      .then((res) => {
        setGenres(res.data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  }, []);

  return (
    <div>
      <h1>Welcome to GameSource</h1>
      {genres.map((genre) => (
        <Genre key={genre.id} genre={genre}/>
      ))}
    </div>
  );
};

const Genre = (props) => {
  const genre = props.genre;

  return (
    <div>
      {genre.id}
      {genre.name}
    </div>
  );
};

export default Home;
