<template>
  <div class="container mt-5">
    <div class="row justify-content-center">
      <div class="col-md-8">
        <div class="card">
          <div class="card-header">Meu Perfil</div>
          <div class="card-body">
            <form @submit.prevent="handleUpdate">
              <div class="mb-3">
                <label for="name" class="form-label">Nome</label>
                <input type="text" class="form-control" id="name" v-model="user.nome" />
              </div>
              <div class="mb-3">
                <label for="email" class="form-label">Email</label>
                <input type="email" class="form-control" id="email" v-model="user.email" />
              </div>
              <div class="mb-3">
                <label for="telefone" class="form-label">Telefone</label>
                <input type="text" class="form-control" id="telefone" v-model="user.telefone" />
              </div>
              <button type="submit" class="btn btn-primary">Atualizar</button>
            </form>
          </div>
        </div>
        <div class="card mt-4">
          <div class="card-header">Excluir Conta</div>
          <div class="card-body">
            <p>A exclusão da sua conta é uma ação irreversível.</p>
            <button class="btn btn-danger" @click="handleDelete">Excluir Minha Conta</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue';
import { useAuthStore } from '@/stores/auth';
import { useRouter } from 'vue-router';
import type { User } from '@/types';

export default defineComponent({
  name: 'ProfileView',
  setup() {
    const authStore = useAuthStore();
    const router = useRouter();
    const user = ref<User>({} as User);

    onMounted(() => {
      if (authStore.user) {
        user.value = { ...authStore.user };
      }
    });

    const handleUpdate = async () => {
      try {
        await authStore.updateUser(user.value);
        alert('Perfil atualizado com sucesso!');
      } catch (error) {
        console.error(error);
        alert('Erro ao atualizar o perfil.');
      }
    };

    const handleDelete = async () => {
      if (confirm('Tem certeza que deseja excluir sua conta?')) {
        try {
          await authStore.deleteUser(user.value.id);
          authStore.logout();
          router.push('/login');
          alert('Conta excluída com sucesso!');
        } catch (error) {
          console.error(error);
          alert('Erro ao excluir a conta.');
        }
      }
    };

    return {
      user,
      handleUpdate,
      handleDelete,
    };
  },
});
</script>
