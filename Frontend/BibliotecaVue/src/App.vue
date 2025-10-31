<script setup lang="ts">
import { RouterLink, RouterView, useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth'

const router = useRouter()
const authStore = useAuthStore()

const logout = () => {
  authStore.logout()
  router.push('/login')
}
</script>

<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light">
    <div class="container-fluid">
      <RouterLink class="navbar-brand" to="/dashboard">Biblioteca Vue</RouterLink>
      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarNav"
        aria-controls="navbarNav"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarNav">
        <ul class="navbar-nav">
          <li class="nav-item" v-if="authStore.isAuthenticated">
            <RouterLink class="nav-link" to="/dashboard">Dashboard</RouterLink>
          </li>
          <li class="nav-item" v-if="authStore.isAuthenticated">
            <RouterLink class="nav-link" to="/books">Gerenciar Livros</RouterLink>
          </li>
          <li class="nav-item" v-if="authStore.isAuthenticated && authStore.isAdmin">
            <RouterLink class="nav-link" to="/register">Cadastrar Usuário</RouterLink>
          </li>
          <li class="nav-item" v-if="authStore.isAuthenticated && authStore.isAdmin">
            <RouterLink class="nav-link" to="/rentals">Gerenciar Locações</RouterLink>
          </li>
          <li class="nav-item" v-if="!authStore.isAuthenticated">
            <RouterLink class="nav-link" to="/login">Login</RouterLink>
          </li>
          <li class="nav-item" v-if="authStore.isAuthenticated">
            <RouterLink class="nav-link" to="/my-rentals">Minhas Locações</RouterLink>
          </li>
          <li class="nav-item" v-if="authStore.isAuthenticated">
            <a class="nav-link" href="#" @click.prevent="logout">Logout</a>
          </li>
        </ul>
      </div>
    </div>
  </nav>

  <RouterView />
</template>

<style scoped>
/* Estilos globais ou específicos do layout podem ser adicionados aqui */
</style>