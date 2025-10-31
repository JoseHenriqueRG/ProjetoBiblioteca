<template>
  <div class="container mt-5">
    <h2>Gestão de Locações</h2>
    <router-link to="/rentals/add" class="btn btn-success mb-3">Nova Locação</router-link>
    <table class="table">
      <thead>
        <tr>
          <th>ID</th>
          <th>Livro</th>
          <th>Usuário</th>
          <th>Data da Retirada</th>
          <th>Data de Devolução</th>
          <th>Status</th>
          <th>Ações</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="rental in rentals" :key="rental.id">
          <td>{{ rental.id }}</td>
          <td>{{ rental.livroTitulo }}</td>
          <td>{{ rental.usuarioNome }}</td>
          <td>{{ new Date(rental.dataRetirada).toLocaleDateString() }}</td>
          <td>{{ new Date(rental.dataDevolucaoPrevista).toLocaleDateString() }}</td>
          <td>{{ rental.status }}</td>
          <td>
            <button v-if="rental.status === 'Pendente'" @click="devolver(rental.id)" class="btn btn-primary btn-sm">Devolver</button>
            <button v-if="rental.status === 'Pendente'" @click="renovar(rental.id)" class="btn btn-info btn-sm ms-2">Renovar</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue';
import { getLocacoes, devolverLocacao, renovarLocacao } from '@/services/rental';
import type { LocacaoDto } from '@/types';

export default defineComponent({
  name: 'RentalListView',
  setup() {
    const rentals = ref<LocacaoDto[]>([]);

    const fetchRentals = async () => {
      try {
        rentals.value = await getLocacoes();
      } catch (error) {
        console.error(error);
        alert('Erro ao buscar locações.');
      }
    };

    onMounted(fetchRentals);

    const devolver = async (id: number) => {
      if (confirm('Confirmar devolução?')) {
        try {
          await devolverLocacao(id);
          fetchRentals();
        } catch (error) {
          console.error(error);
          alert('Erro ao devolver livro.');
        }
      }
    };

    const renovar = async (id: number) => {
      const dias = prompt('Por quantos dias deseja renovar?', '7');
      if (dias) {
        try {
          await renovarLocacao(id, parseInt(dias, 10));
          fetchRentals();
        } catch (error) {
          console.error(error);
          alert('Erro ao renovar empréstimo.');
        }
      }
    };

    return {
      rentals,
      devolver,
      renovar,
    };
  },
});
</script>
