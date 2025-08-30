import { createApp } from 'vue'
// import './style.css'
import App from './App.vue'
import { router } from './router'
import components from './components/UI'
import PageHeader from './components/layouts/PageHeader.vue'

const app = createApp(App)
// ниже регестрируются компоненты из массива UI
components.forEach(component => {
    app.component(component.name, component) //первое поле определяет имя компонента, второе - сам компонент
})
app.component("PageHeader", PageHeader)

app.use(router)

app.mount('#app')
