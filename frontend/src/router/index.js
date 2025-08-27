import { createRouter, createMemoryHistory } from "vue-router";

// ниже объявляется массив путей для маршрутизатора vue-router
// поля name, path и component используем базово обязательно
// в будущем добавим еще поле beforeEnter, оно нужно для ограничения доступа к странице если человек не залогинился
const routes = [
    {
        name: 'home',
        path: '/',
        component: () => import('../views/MainView.vue')
    },
    {
        name: 'testBackEnd',
        path: '/test',
        component: () => import("../views/BackendTest.vue")
    }
]

export const router = new createRouter({
    routes,
    history: createMemoryHistory()
})