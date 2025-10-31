import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '../views/LoginView.vue'
import RegisterView from '../views/RegisterView.vue'
import BookFormView from '../views/BookFormView.vue'
import BookListView from '../views/BookListView.vue'
import ProfileView from '../views/ProfileView.vue'
import RentalListView from '../views/RentalListView.vue'
import RentalFormView from '../views/RentalFormView.vue'
import MyRentalsView from '../views/MyRentalsView.vue'
import { useAuthStore } from '@/stores/auth'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      redirect: '/login'
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView
    },
    {
      path: '/register',
      name: 'register',
      component: RegisterView
    },
    {
      path: '/books',
      name: 'book-list',
      component: BookListView,
      meta: { requiresAuth: true }
    },
    {
      path: '/books/add',
      name: 'book-add',
      component: BookFormView,
      meta: { requiresAuth: true, requiresAdmin: true }
    },
    {
      path: '/books/edit/:id',
      name: 'book-edit',
      component: BookFormView,
      meta: { requiresAuth: true, requiresAdmin: true }
    },
    {
      path: '/profile',
      name: 'profile',
      component: ProfileView,
      meta: { requiresAuth: true }
    },
    {
      path: '/rentals',
      name: 'rental-list',
      component: RentalListView,
      meta: { requiresAuth: true, requiresAdmin: true }
    },
    {
      path: '/rentals/add',
      name: 'rental-add',
      component: RentalFormView,
      meta: { requiresAuth: true, requiresAdmin: true }
    },
    {
      path: '/my-rentals',
      name: 'my-rentals',
      component: MyRentalsView,
      meta: { requiresAuth: true }
    }
  ]
})

router.beforeEach((to, from, next) => {
  const authStore = useAuthStore()

  if (to.meta.requiresAuth && !authStore.isAuthenticated) {
    return next({ name: 'login' })
  }

  if (to.meta.requiresAdmin && !authStore.isAdmin) {
    return next({ name: 'book-list' }) 
  }

  next()
})

export default router