import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import axios from "axios";

const Platforms = () => {
  const platformApi = "https://localhost:44336/api/platforms/";
  const [platforms, setPlatforms] = useState([]);

  useEffect(() => {
    axios
      .get(platformApi)
      .then((res) => {
        setPlatforms(res.data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  }, []);

  return (
    <>
      {platforms.map((platform) => {
        <>
          <div className="dropdown-header">By Platform</div>
          <span className="dropdown-divider"></span>
          <li>
            <div className="dropdown-header">By Platform</div>
            <span className="dropdown-divider"></span>
            <Link to={`/platform/${platform.id}`}>
              <span>{platform.name}</span>
            </Link>
          </li>
        </>;
      })}
    </>
  );
};

export default Platforms;
