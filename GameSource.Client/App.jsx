import React, { useState } from "react";
import { Route } from "react-router-dom";

//Components
import Home from "./components/Home";
import Navbar from "./components/Navbar/Navbar";
import About from "./components/About";
import Contact from "./components/Contact";

const App = () => {
  return (
    <div>
      <Navbar />
      <Route exact path="/" component={Home}/>
      <Route path="/about" component={About}/>
      <Route path="/contact" component={Contact}/>
    </div>
  );
};

export default App;
