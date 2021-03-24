import React, { useState } from "react";
import { Route } from "react-router-dom";

//Components
import Home from "./components/Home";
import Navbar from "./components/Navbar/Navbar";
import About from "./components/About";
import Contact from "./components/Contact";
import Login from "./components/Account/Login";
import Register from "./components/Account/Register";
import GameIndex from "./components/Games/Index";
import DeveloperIndex from "./components/Developers/Index";
import PublisherIndex from "./components/Publishers/Index";
import PlatformIndex from "./components/Platforms/Index";
import PlatformTypeIndex from "./components/PlatformTypes/Index";
import ReviewIndex from "./components/PlatformTypes/Index";
import ReviewCommentIndex from "./components/PlatformTypes/Index";
import NewsArticleIndex from "./components/PlatformTypes/Index";
import NewsArticleCategoryIndex from "./components/PlatformTypes/Index";

const App = () => {
  return (
    <div className="container">
      <Navbar />
      <Route exact path="/" component={Home}/>
      <Route path="/about" component={About}/>
      <Route path="/contact" component={Contact}/>
      <Route path="/login" component={Login}/>
      <Route path="/register" component={Register}/>
      <Route path="/games" component={GameIndex}/>
      <Route path="/developers" components={DeveloperIndex}/>
      <Route path="/publishers" components={PublisherIndex}/>
      <Route path="/platforms" components={PlatformIndex}/>
      <Route path="/platform-types" components={PlatformTypeIndex}/>
      <Route path="/reviews" components={ReviewIndex}/>
      <Route path="/review-comments" components={ReviewCommentIndex}/>
      <Route path="/news-articles" components={NewsArticleIndex}/>
      <Route path="/news-article-categories" components={NewsArticleCategoryIndex}/>
    </div>
  );
};

export default App;
