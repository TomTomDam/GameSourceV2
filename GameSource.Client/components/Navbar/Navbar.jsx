import React from "react";
import { Link } from "react-router-dom";

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
  ];

  const linksMap = links.map((link) => {
    return (
      <li key={link.id}>
        <Link to={link.path}>
          <span>{link.title}</span>
        </Link>
      </li>
    );
  });

  return (
    <header>
      <Link to="/">GameSource</Link>
      <nav>{linksMap}</nav>
    </header>
  );
};

export default Navbar;
