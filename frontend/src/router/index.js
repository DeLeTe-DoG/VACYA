import { createRouter, createWebHashHistory} from "vue-router";
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
    {
        name: 'testBackEnd',
        path: '/test',
        component: () => import("../views/BackendTest.vue")
    },
    {
        name:'BeginPage',
        path:'/begin',
        component: BeginView,
    },
    {
        path: '/login',
        name: 'Login',
        component: () => import("../views/LoginView.vue")
    },
    {
        path: '/register',
        name: 'Register',
        component: () => import("../components/ApiTester/FormRegister.vue")
    },
    {
        path: '/profile',
        name: 'Profile',
        component: () => import("../components/ApiTester/Profile.vue"),
        meta: { requiresAuth: true }
    }
]

const guard = function (to, from, next) {
    const token = localStorage.getItem('token')

    if(token) {
        next();
    } else {
        router.replace({ path: '/login' })
    }
}

export const router = new createRouter({
    routes,
    history: createWebHashHistory()
})

router.beforeEach((to, from, next) => {
  const isAuthenticated = !!localStorage.getItem('authToken');
  
  if (to.meta.requiresAuth && !isAuthenticated) {
    next('/login');
  } else {
    next();
  }
});