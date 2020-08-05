//Registered modules
import Vue from 'vue';
import axios from 'axios';
import VueRouter from 'vue-router';
//Account Settings
import AccountSettingsPage from './AccountSettingsPage.vue'
import AccountSettings from './components/AccountSettings.vue'
import EmailSettings from './components/EmailSettings.vue'
import PrivacySettings from './components/PrivacySettings.vue'
//Browse Games
import BrowseGames from './BrowseGames.vue'
import BrowseGamesByGenre from './components/BrowseGamesByGenre.vue'
import BrowseGamesByPlatform from './components/BrowseGamesByPlatform.vue'
//Game Grid
//import GameCard from './components/GameCard.vue'
//import GamesCardGrid from './components/GamesCardGrid.vue'

Vue.config.productionTip = false;

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
    { path: '/account', component: AccountSettings },
    { path: '/email', component: EmailSettings },
    { path: '/privacy', component: PrivacySettings },
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

new Vue({
    el: '#browse-games',
    components: {
        BrowseGamesByGenre,
        BrowseGamesByPlatform
    },
    render: h => h(BrowseGames)
});

window.Vue = Vue;