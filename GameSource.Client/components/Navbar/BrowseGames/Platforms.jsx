import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import axios from "axios";

const Platforms = () => {
  const platformApi = "https://localhost:44336/api/platforms/";
  const [platforms, setPlatforms] = useState([]);
  const [isLoading, setLoading] = useState(true);

  useEffect(() => {
    axios
      .get(platformApi)
      .then((res) => {
        setPlatforms(res.data.data);
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
      {platforms.map((platform) => {
        <li key={platform.id}>
          <Link to={`/platform/${platform.id}`}>
            <span>{platform.name}</span>
          </Link>
        </li>;
      })}
    </>
  );
};

export default Platforms;
