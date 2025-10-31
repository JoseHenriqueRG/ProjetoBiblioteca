<template>
  <div class="container mt-5">
    <h2>Minhas Locações</h2>
    <table class="table">
      <thead>
        <tr>
          <th>Livro</th>
          <th>Data da Retirada</th>
          <th>Data de Devolução</th>
          <th>Status</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="rental in rentals" :key="rental.id">
          <td>{{ rental.livroTitulo }}</td>
          <td>{{ new Date(rental.dataRetirada).toLocaleDateString() }}</td>
          <td>{{ new Date(rental.dataDevolucaoPrevista).toLocaleDateString() }}</td>
          <td>{{ rental.status }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue';
import { getLocacoesByUserId } from '@/services/rental';
import { useAuthStore } from '@/stores/auth';
import type { LocacaoDto } from '@/types';

export default defineComponent({
  name: 'MyRentalsView',
  setup() {
    const rentals = ref<LocacaoDto[]>([]);
    const authStore = useAuthStore();

    const fetchRentals = async () => {
      if (authStore.user) {
        try {
          rentals.value = await getLocacoesByUserId(authStore.user.id);
        } catch (error) {
          console.error(error);
          alert('Erro ao buscar minhas locações.');
        }
      }
    };

    onMounted(fetchRentals);

    return {
      rentals,
    };
  },
});
</script>
