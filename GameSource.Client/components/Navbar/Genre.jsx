import React, { useState, useEffect } from "react";
import axios from "axios";

const Genre = (props) => {
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
    <>
      {genre.id}
      {genre.name}
    </>
  );
};

export default Genre;
