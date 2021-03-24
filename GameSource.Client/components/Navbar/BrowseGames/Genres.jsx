import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import axios from "axios";

const Genres = () => {
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
      {genres.map((genre) => {
        <>
          <div className="dropdown-header">By Platform</div>
          <span className="dropdown-divider"></span>
          <li>
            <Link to={`/genres/${genre.id}`}>
              <span>{genre.name}</span>
            </Link>
          </li>
        </>;
      })}
    </>
  );
};

export default Genres;
