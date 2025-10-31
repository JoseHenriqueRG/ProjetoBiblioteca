<template>
  <div class="container mt-5">
    <div class="row justify-content-center">
      <div class="col-md-6">
        <div class="card">
          <div class="card-header">Login</div>
          <div class="card-body">
            <form @submit.prevent="handleLogin" novalidate>
              <div class="mb-3">
                <label for="email" class="form-label">Email</label>
                <input
                  type="email"
                  class="form-control"
                  :class="{ 'is-invalid': emailError }"
                  id="email"
                  v-model="email"
                  required
                  @input="emailError = ''"
                />
                <div class="invalid-feedback">{{ emailError }}</div>
              </div>
              <div class="mb-3">
                <label for="password" class="form-label">Senha</label>
                <input
                  type="password"
                  class="form-control"
                  :class="{ 'is-invalid': passwordError }"
                  id="password"
                  v-model="password"
                  required
                  @input="passwordError = ''"
                />
                <div class="invalid-feedback">{{ passwordError }}</div>
              </div>
              <button type="submit" class="btn btn-primary">Entrar</button>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { useRouter } from 'vue-router';
import { useAuthStore } from '@/stores/auth';

export default defineComponent({
  name: 'LoginView',
  setup() {
    const email = ref('');
    const password = ref('');
    const emailError = ref('');
    const passwordError = ref('');
    const router = useRouter();
    const authStore = useAuthStore();

    const validateForm = () => {
      let isValid = true;
      emailError.value = '';
      passwordError.value = '';

      if (!email.value) {
        emailError.value = 'O email é obrigatório.';
        isValid = false;
      } else if (!/\S+@\S+\.\S+/.test(email.value)) {
        emailError.value = 'Por favor, insira um email válido.';
        isValid = false;
      }

      if (!password.value) {
        passwordError.value = 'A senha é obrigatória.';
        isValid = false;
      }

      return isValid;
    };

    const handleLogin = async () => {
      if (!validateForm()) {
        return;
      }
      try {
        await authStore.login({ email: email.value, senha: password.value });
        router.push('/dashboard');
      } catch (error) {
        console.log(error);
        alert('Email ou senha inválidos.');
      }
    };

    return {
      email,
      password,
      emailError,
      passwordError,
      handleLogin,
    };
  },
});
</script>

<style scoped>
/* Estilos específicos para este componente, se necessário */
</style>
