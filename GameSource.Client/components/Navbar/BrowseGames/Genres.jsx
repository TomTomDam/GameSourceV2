import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import axios from "axios";

const Genres = () => {
  const genreApi = "https://localhost:44336/api/genres/";
  const [genres, setGenres] = useState([]);
  const [isLoading, setLoading] = useState(true);

  useEffect(() => {
    axios
      .get(genreApi)
      .then((res) => {
        setGenres(res.data.data);
        setLoading(false);
      })
      .catch((err) => {
        console.log(err);
      });
  }, []);

  if (isLoading) {
    return <div>Loading...</div>;
  }

  return (
    <>
      {genres.map((genre) => {
        <li>
          <Link to={`/genres/${genre.id}`} className="nav-link dropdown-item">
            <span>{genre.name}</span>
          </Link>
        </li>;
      })}
    </>
  );
};

export default Genres;
