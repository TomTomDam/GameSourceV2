import React, { useState } from "react";
import { Route } from "react-router-dom";

//Components
import Home from "./components/Home";

const App = () => {
  return (
    <div>
      <Route component={Home}/>
    </div>
  );
};

export default App;
