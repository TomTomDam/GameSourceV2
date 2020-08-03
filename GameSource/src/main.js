import Vue from 'vue'

Vue.config.productionTip = false

Vue.component('game-card', require('./components/GameCard.vue').default);
Vue.component('games-card-grid', require('./components/GamesCardGrid.vue').default);
Vue.component('browse-games-by-genre', require('./components/BrowseGamesByGenre.vue').default);
Vue.component('browse-games-by-platform', require('./components/BrowseGamesByPlatform.vue').default);

window.Vue = Vue;