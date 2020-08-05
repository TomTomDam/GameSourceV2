import Vue from 'vue';
import axios from 'axios';
import VueRouter from 'vue-router';
import AccountSettingsPage from './AccountSettingsPage.vue'

Vue.config.productionTip = false;

//Registered components
Vue.component('game-card', require('./components/GameCard.vue').default);
Vue.component('games-card-grid', require('./components/GamesCardGrid.vue').default);
Vue.component('browse-games-by-genre', require('./components/BrowseGamesByGenre.vue').default);
Vue.component('browse-games-by-platform', require('./components/BrowseGamesByPlatform.vue').default);
Vue.component('account-settings', require('./components/AccountSettings.vue').default);
Vue.component('email-settings', require('./components/EmailSettings.vue').default);
Vue.component('privacy-settings', require('./components/PrivacySettings.vue').default);

//Axios
const api = axios.create({
    baseURL: 'http://localhost:52817/api/'
});

const axiosPlugin = {
    install(Vue) {
        Vue.prototype.$api = api;
    }
}

Vue.use(axiosPlugin);

//VueRouter
Vue.use(VueRouter);

const routes = [
    { path: '/account', component: 'account-settings' },
    { path: '/email', component: 'email-settings' },
    { path: '/privacy', component: 'privacy-settings' },
];

const router = new VueRouter({
    routes,
    mode: 'history'
});

//Vue Instances
new Vue({
    el: '#account-settings-page',
    router,
    render: h => h(AccountSettingsPage)
});

window.Vue = Vue;