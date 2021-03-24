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
import GenreIndex from "./components/Genres/Index";
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
      <Route path="/genres" component={GenreIndex}/>
      <Route path="/developers" component={DeveloperIndex}/>
      <Route path="/publishers" component={PublisherIndex}/>
      <Route path="/platforms" component={PlatformIndex}/>
      <Route path="/platform-types" component={PlatformTypeIndex}/>
      <Route path="/reviews" component={ReviewIndex}/>
      <Route path="/review-comments" component={ReviewCommentIndex}/>
      <Route path="/news-articles" component={NewsArticleIndex}/>
      <Route path="/news-article-categories" component={NewsArticleCategoryIndex}/>
    </div>
  );
};

export default App;
