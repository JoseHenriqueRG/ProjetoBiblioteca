<template>
  <div class="container mt-5">
    <div class="row justify-content-center">
      <div class="col-md-6">
        <div class="card">
          <div class="card-header">Cadastro de Usuário</div>
          <div class="card-body">
            <form @submit.prevent="handleRegister" novalidate>
              <div class="mb-3">
                <label for="name" class="form-label">Nome</label>
                <input
                  type="text"
                  class="form-control"
                  :class="{ 'is-invalid': nameError }"
                  id="name"
                  v-model="name"
                  required
                  @input="nameError = ''"
                />
                <div class="invalid-feedback">{{ nameError }}</div>
              </div>
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
              <div class="mb-3">
                <label for="confirmPassword" class="form-label">Confirmar Senha</label>
                <input
                  type="password"
                  class="form-control"
                  :class="{ 'is-invalid': confirmPasswordError }"
                  id="confirmPassword"
                  v-model="confirmPassword"
                  required
                  @input="confirmPasswordError = ''"
                />
                <div class="invalid-feedback">{{ confirmPasswordError }}</div>
              </div>
              <div class="mb-3">
                <label for="telefone" class="form-label">Telefone</label>
                <input
                  type="text"
                  class="form-control"
                  id="telefone"
                  v-model="telefone"
                />
              </div>
              <div class="mb-3">
                <label for="tipo" class="form-label">Tipo</label>
                <select class="form-select" id="tipo" v-model="tipo">
                  <option value="UsuarioPadrao">Usuário Padrão</option>
                  <option value="Administrador">Administrador</option>
                </select>
              </div>
              <button type="submit" class="btn btn-success">Cadastrar</button>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import axios from 'axios';
import { useRouter } from 'vue-router';

export default defineComponent({
  name: 'RegisterView',
  setup() {
    const name = ref('');
    const email = ref('');
    const password = ref('');
    const confirmPassword = ref('');
    const telefone = ref('');
    const tipo = ref('UsuarioPadrao');
    const nameError = ref('');
    const emailError = ref('');
    const passwordError = ref('');
    const confirmPasswordError = ref('');
    const router = useRouter();

    const validateForm = () => {
      let isValid = true;
      nameError.value = '';
      emailError.value = '';
      passwordError.value = '';
      confirmPasswordError.value = '';

      if (!name.value) {
        nameError.value = 'O nome é obrigatório.';
        isValid = false;
      }

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
      } else if (password.value.length < 6) {
        passwordError.value = 'A senha deve ter pelo menos 6 caracteres.';
        isValid = false;
      }

      if (!confirmPassword.value) {
        confirmPasswordError.value = 'A confirmação de senha é obrigatória.';
        isValid = false;
      } else if (password.value !== confirmPassword.value) {
        confirmPasswordError.value = 'As senhas não coincidem.';
        isValid = false;
      }

      return isValid;
    };

    const handleRegister = async () => {
      if (!validateForm()) {
        return;
      }
      try {
        const token = localStorage.getItem('token');
        await axios.post(
          '/api/usuarios',
          {
            nome: name.value,
            email: email.value,
            senha: password.value,
            telefone: telefone.value,
            tipo: tipo.value,
          },
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
        router.push('/login');
      } catch (error) {
        alert('Erro ao cadastrar usuário. Verifique os dados e tente novamente.');
      }
    };

    return {
      name,
      email,
      password,
      confirmPassword,
      telefone,
      tipo,
      nameError,
      emailError,
      passwordError,
      confirmPasswordError,
      handleRegister,
    };
  },
});
</script>

<style scoped>
/* Estilos específicos para este componente, se necessário */
</style>
