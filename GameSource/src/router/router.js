import Vue from 'vue';
import VueRouter from 'vue-router';
import AccountSettings from '../components/AccountSettings.vue';
import EmailSettings from '../components/EmailSettings.vue';
import PrivacySettings from '../components/PrivacySettings.vue';

Vue.use(VueRouter);

export default new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes: [
        {
            path: '/account/account',
            name: 'account',
            component: AccountSettings
        },
        {
            path: '/email/account',
            name: 'email',
            component: EmailSettings
        },
        {
            path: '/privacy/account',
            name: 'privacy',
            component: PrivacySettings
        }
    ]
});