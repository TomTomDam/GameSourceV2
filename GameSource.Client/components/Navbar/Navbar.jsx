import React from "react";
import { Link } from "react-router-dom";
import BrowseGames from "./BrowseGames/BrowseGames";

const Navbar = () => {
  const links = [
    {
      id: 1,
      title: "About",
      path: "/about",
    },
    {
      id: 2,
      title: "Contact",
      path: "/contact",
    },
    {
      id: 3,
      title: "Login",
      path: "/login",
    },
  ];

  const browseGamesLinks = [
    {
      id: 1,
      title: "View All Games",
      path: "/games"
    },
    {
      id: 2,
      title: "View All Genres",
      path: "/genres"
    },
    {
      id: 3,
      title: "View All Developers",
      path: "/developers"
    },
    {
      id: 4,
      title: "View All Publishers",
      path: "/publishers"
    },
    {
      id: 5,
      title: "View All Platforms",
      path: "/platforms"
    },
    {
      id: 6,
      title: "View All Platform Types",
      path: "/platform-types"
    },
  ]

  const linksMap = links.map((link) => {
    return (
      <li className="nav-item" key={link.id}>
        <Link to={link.path} className="nav-link">
          <span>{link.title}</span>
        </Link>
      </li>
    );
  });

  const browseGamesLinksMap = browseGamesLinks.map((link) => {
    return (
      <li className="nav-link" key={link.id}>
        <Link to={link.path} className="nav-link dropdown-item">
        <span>{link.title}</span>
        </Link>
      </li>
    )
  })

  return (
    <header>
      <nav className="navbar navbar-expand-sm navbar-toggleable-sm box-shadow mb-3">
        <div className="container">
          <Link to="/" className="navbar-brand">GameSource</Link>
          <button
            className="navbar-toggler"
            type="button"
            data-toggle="collapse"
            data-target=".navbar-collapse"
            aria-controls="navbarSupportedContent"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
            <ul className="navbar-nav flex-grow-1">
                <li className="nav-item dropdown">
                    <a className="nav-link dropdown-toggle" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Browse Games</a>
                    <ul className="dropdown-menu dropdown-columns" aria-labelledby="navbarDropdown">
                        <BrowseGames />
                    </ul>
                </li>
                <li className="nav-item dropdown">
                    <a className="nav-link dropdown-toggle" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Database</a>
                    <ul className="dropdown-menu" aria-labelledby="navbarDropdown">
                      {browseGamesLinksMap}
                    </ul>
                </li>
                <li className="nav-item">
                    <a className="nav-link">News</a>
                </li>
                {linksMap}
            </ul>
          </div>
        </div>
      </nav>
    </header>
  );
};

export default Navbar;
