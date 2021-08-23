import Vue from 'vue'
import './plugins/axios'
import App from './App.vue'
import vuetify from './plugins/vuetify'
import axios from 'axios'

const base = axios.create({
  baseURL: 'https://localhost:5001/vendOrama/VendOrama'
})

Vue.prototype.$https = base;

Vue.config.productionTip = false

new Vue({
  vuetify,
  render: h => h(App)
}).$mount('#app')
