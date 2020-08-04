import Vue from 'vue'
import axios from 'axios'

Vue.config.productionTip = false;

const api = axios.create({
    baseURL: 'http://localhost:52817/api/'
});

const axiosPlugin = {
    install(Vue) {
        Vue.prototype.$api = api;
    }
}

Vue.use(axiosPlugin);

Vue.component('game-card', require('./components/GameCard.vue').default);
Vue.component('games-card-grid', require('./components/GamesCardGrid.vue').default);
Vue.component('browse-games-by-genre', require('./components/BrowseGamesByGenre.vue').default);
Vue.component('browse-games-by-platform', require('./components/BrowseGamesByPlatform.vue').default);

window.Vue = Vue;