import React, { useState } from "react";
import { Route } from "react-router-dom";

//Components
import Home from "./components/Home";
import Navbar from "./components/Navbar/Navbar";
import About from "./components/About";
import Contact from "./components/Contact";
import Login from "./components/Navbar/Login";

const App = () => {
  return (
    <div className="container">
      <Navbar />
      <Route exact path="/" component={Home}/>
      <Route path="/about" component={About}/>
      <Route path="/contact" component={Contact}/>
      <Route path="/login" component={Login}/>
    </div>
  );
};

export default App;
