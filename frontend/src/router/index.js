import { createRouter, createWebHistory} from "vue-router";
import BeginView from "../views/BeginView.vue"
// ниже объявляется массив путей для маршрутизатора vue-router
// поля name, path и component используем базово обязательно
// в будущем добавим еще поле beforeEnter, оно нужно для ограничения доступа к странице если человек не залогинился
const routes = [
    {
        name: 'home',
        path: '/',
        component: () => import('../views/MainView.vue'),
        beforeEnter: (to, from, next) => {
            guard(to, from, next)
        }
    },
    // {
    //     name: 'testBackEnd',
    //     path: '/test',
    //     component: () => import("../views/BackendTest.vue")
    // },
    // {
    //     name:'BeginPage',
    //     path:'/begin',
    //     component: BeginView,
    // },
    {
        path: '/auth',
        name: 'Auth',
        component: () => import("../views/LoginView.vue"),
        // beforeEnter: (to, from, next) => {
        //     guard(to, from, next)
        // }
    },
    {
        path: '/profile',
        name: 'Profile',
        component: () => import("../views/Profile.vue"),
        beforeEnter: (to, from, next) => {
            guard(to, from, next)
        }
        // meta: { requiresAuth: true }
    },
    {
        path: '/add-site',
        name: 'Add-site',
        component: () => import('../views/AddSiteView.vue'),
        beforeEnter: (to, from, next) => {
            guard(to, from, next)
        }
    },
    {
        path: '/tests',
        name: 'Tests',
        component: () => import('../views/Tests.vue'),
        // beforeEnter: (to, from, next) => {
        //     guard(to, from, next)
        // }
    },
    {
        path: '/tests/plan-test',
        name: 'add-plan-test',
        component: () => import('../views/AddPlanTests.vue'),
        beforeEnter: (to, from, next) => {
            guard(to, from, next)
        }
    },


]

const guard = function (to, from, next) {
    const token = localStorage.getItem('token')

    if(token) {
        next();
    } else {
        router.push({ path: '/auth' })
    }
}

export const router = new createRouter({
    routes,
    history: createWebHistory()
})

// router.beforeEach((to, from, next) => {
//   const isAuthenticated = !!localStorage.getItem('authToken');
  
//   if (to.meta.requiresAuth && !isAuthenticated) {
//     next('/auth');
//   } else {
//     next();
//   }
// });