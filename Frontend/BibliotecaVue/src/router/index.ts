import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '../views/LoginView.vue'
import RegisterView from '../views/RegisterView.vue'
import BookFormView from '../views/BookFormView.vue'
import DashboardView from '../views/DashboardView.vue'
import BookListView from '../views/BookListView.vue' // Import the new component

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      redirect: '/login',
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView,
    },
    {
      path: '/register',
      name: 'register',
      component: RegisterView,
    },
    {
      path: '/books',
      name: 'book-list',
      component: BookListView,
    },
    {
      path: '/books/add',
      name: 'book-add',
      component: BookFormView,
    },
    {
      path: '/books/edit/:id',
      name: 'book-edit',
      component: BookFormView,
    },
    {
      path: '/dashboard',
      name: 'dashboard',
      component: DashboardView,
    },
  ],
})

export default router