import Vue from '../../lib/vue.js'
import Router from '../../lib/vue-router.js'
import Account from '../components/Account'
import Email from '../components/Email'
import Privacy from '../components/Privacy'

Vue.use(Router);

export default new Router({
    routes: [
        {
            path: '/account-settings',
            name: 'Account',
            component: Account
        },
        {
            path: '/email-settings',
            name: 'Email',
            component: Email
        },
        {
            path: '/privacy-settings',
            name: 'Privacy',
            component: Privacy
        }
    ]
});