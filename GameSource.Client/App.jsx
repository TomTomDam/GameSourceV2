import React, { useState } from "react";
import { Route } from "react-router-dom";

//Components
import Home from "./components/Home";
import Footer from "./components/Footer";
import Navbar from "./components/Navbar/Navbar";
import About from "./components/About";
import Contact from "./components/Contact";
import Login from "./components/Account/Login";
import Register from "./components/Account/Register";
import GameIndex from "./components/Game/Index";
import GenreIndex from "./components/Genre/Index";
import DeveloperIndex from "./components/Developer/Index";
import PublisherIndex from "./components/Publisher/Index";
import PlatformIndex from "./components/Platform/Index";
import PlatformTypeIndex from "./components/PlatformType/Index";
import ReviewIndex from "./components/Review/Index";
import ReviewCommentIndex from "./components/ReviewComment/Index";
import NewsArticleIndex from "./components/NewsArticle/Index";
import NewsArticleCategoryIndex from "./components/NewsArticleCategory/Index";

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
      <Footer />
    </div>
  );
};

export default App;
