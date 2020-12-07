//Registered modules
import Vue from 'vue';
import axios from 'axios';
//import router from './router/router.js';
////Browse Games
//import BrowseGamesPage from './BrowseGamesPage.vue'
//import BrowseGamesByGenre from './components/BrowseGamesByGenre.vue'
//import BrowseGamesByPlatform from './components/BrowseGamesByPlatform.vue'
//Game Grid
//import GameCard from './components/GameCard.vue'
//import GamesCardGrid from './components/GamesCardGrid.vue'
//News Article
import NewsIndexPage from './NewsIndexPage.vue';
import NewsCategories from './components/NewsCategories.vue';
import NewsCalendar from './components/NewsCalendar.vue';

Vue.config.productionTip = false;

//Axios
const api = axios.create({
    baseURL: 'https://localhost:52817/api/'
});

const baseURL = axios.create({
    baseURL: 'https://localhost:44375/'
});

const axiosPlugin = {
    install(Vue) {
        Vue.prototype.$api = api;
        Vue.prototype.$baseURL = baseURL;
    }
}

Vue.use(axiosPlugin);

//Vue Instances
new Vue({
    el: '#news-index-menu',
    components: {
        NewsCategories,
        NewsCalendar
    },
    render: h => h(NewsIndexPage)
});

window.Vue = Vue;