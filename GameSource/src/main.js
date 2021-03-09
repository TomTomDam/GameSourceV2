//Registered modules
import Vue from 'vue';
import axios from 'axios';
//import router from './router/router.js';
//Browse Games
import BrowseGamesPage from './views/Navbar/BrowseGamesPage.vue'
import BrowseGamesByGenre from './components/Navbar/BrowseGamesByGenre.vue'
import BrowseGamesByPlatform from './components/Navbar/BrowseGamesByPlatform.vue'
////Game Grid
//import GameCard from './components/Games/GameCard.vue'
//import GamesCardGrid from './components/Games/GamesCardGrid.vue'
//News Article
//import NewsIndexPage from './views/News/NewsIndexPage.vue';
//import NewsCategories from './components/News/NewsCategories.vue';
//import NewsCalendar from './components/News/NewsCalendar.vue';

Vue.config.productionTip = false;

//Axios
const api = axios.create({
    baseURL: 'https://localhost:44386/api/'
});

const axiosPlugin = {
    install(Vue) {
        Vue.prototype.$api = api;
    }
}

const baseURL = 'https://localhost:44375/';
Vue.prototype.$baseURL = baseURL;

Vue.use(axiosPlugin);

//Vue Instances
new Vue({
    el: '#browse-games-page',
    components: {
        BrowseGamesByGenre,
        BrowseGamesByPlatform
    },
    render: h => h(BrowseGamesPage)
});

//new Vue({
//    el: '#news-index-menu',
//    components: {
//        NewsCategories,
//        NewsCalendar
//    },
//    render: h => h(NewsIndexPage)
//});

window.Vue = Vue;