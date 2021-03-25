import React from "react";
import { Link } from "react-router-dom";
import BrowseGames from "./BrowseGames/BrowseGames";

const Navbar = () => {
  const links = [
    {
      id: 1,
      title: "News",
      path: "news-articles"
    },
  ];

  const databaseLinks = [
    {
      id: 1,
      title: "View All Games",
      path: "/games",
    },
    {
      id: 2,
      title: "View All Genres",
      path: "/genres",
    },
    {
      id: 3,
      title: "View All Developers",
      path: "/developers",
    },
    {
      id: 4,
      title: "View All Publishers",
      path: "/publishers",
    },
    {
      id: 5,
      title: "View All Platforms",
      path: "/platforms",
    },
    {
      id: 6,
      title: "View All Platform Types",
      path: "/platform-types",
    },
  ];

  const linksMap = links.map((link) => {
    return (
      <li className="nav-item" key={link.id}>
        <Link to={link.path} className="nav-link">
          <span>{link.title}</span>
        </Link>
      </li>
    );
  });

  const databaseLinksMap = databaseLinks.map((link) => {
    return (
      <li className="nav-item" key={link.id}>
        <Link to={link.path} className="nav-link dropdown-item">
          <span>{link.title}</span>
        </Link>
      </li>
    );
  });

  return (
    <header>
      <nav className="navbar navbar-expand-sm navbar-toggleable-sm box-shadow mb-3">
        <div className="container">
          <Link to="/" className="navbar-brand">
            GameSource
          </Link>
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
                <a
                  className="nav-link dropdown-toggle"
                  id="navbarDropdown"
                  role="button"
                  data-bs-toggle="dropdown"
                  aria-haspopup="true"
                  aria-expanded="false"
                >
                  Browse Games
                </a>
                <ul
                  className="dropdown-menu dropdown-columns"
                  aria-labelledby="navbarDropdown"
                >
                  <BrowseGames />
                </ul>
              </li>
              <li className="nav-item dropdown">
                <a
                  className="nav-link dropdown-toggle"
                  id="navbarDropdown"
                  role="button"
                  data-bs-toggle="dropdown"
                  aria-haspopup="true"
                  aria-expanded="false"
                >
                  Database
                </a>
                <ul className="dropdown-menu" aria-labelledby="navbarDropdown">
                  {databaseLinksMap}
                </ul>
              </li>
              {linksMap}
            </ul>
          </div>
          <div className="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
            <ul className="navbar-nav ml-auto">
              {/* <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Administration</a>
                    <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                        <li><a class="nav-link dropdown-item">View All Users</a></li>
                        <li><a class="nav-link dropdown-item">View All User Roles</a></li>
                        <li><a class="nav-link dropdown-item">View All User Statuses</a></li>
                    </ul>
                </li> */}
              {/* <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">@User.Identity.Name</a>
                    <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                        <li><a class="nav-link dropdown-item">View my profile</a></li>
                        <li><a class="nav-link dropdown-item">Profile settings</a></li>
                        <li><a class="nav-link dropdown-item">Account settings</a></li>
                        <li><a class="nav-link dropdown-item">Logout</a></li>
                    </ul>
                </li> */}
              <li className="nav-item">
                <Link to="/register" className="nav-link">
                  <span>Register</span>
                </Link>
              </li>
              <li className="nav-item">
                <Link to="/login" className="nav-link">
                  <span>Login</span>
                </Link>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    </header>
  );
};

export default Navbar;
