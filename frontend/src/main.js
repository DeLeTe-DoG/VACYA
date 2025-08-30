import { createApp } from 'vue'
// import './style.css'
import App from './App.vue'
import { router } from './router'
import components from './components/UI'
import PageHeader from './components/layouts/PageHeader.vue'
import axios from 'axios'

const app = createApp(App)
// ниже регестрируются компоненты из массива UI
components.forEach(component => {
    app.component(component.name, component) //первое поле определяет имя компонента, второе - сам компонент
})
app.component("PageHeader", PageHeader)


axios.defaults.baseURL = 'https://vacya.onrender.com';

axios.interceptors.request.use((config) => {
  const token = localStorage.getItem('authToken');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});


app.use(router)

app.mount('#app')
