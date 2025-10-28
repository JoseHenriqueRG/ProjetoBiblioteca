<template>
  <div class="container mt-5">
    <h1>Dashboard</h1>
    <p>Bem-vindo à sua biblioteca!</p>

    <div class="card mt-4">
      <div class="card-header">
        <h2>Livros Disponíveis</h2>
      </div>
      <div class="card-body">
        <table class="table">
          <thead>
            <tr>
              <th>Título</th>
              <th>Autor</th>
              <th>ISBN</th>
              <th>Ações</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="livro in livros" :key="livro.id">
              <td>{{ livro.titulo }}</td>
              <td>{{ livro.autor }}</td>
              <td>{{ livro.isbn }}</td>
              <td>
                <button class="btn btn-sm btn-primary">Emprestar</button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue';
import axios from 'axios';

interface Livro {
  id: number;
  titulo: string;
  autor: string;
  isbn: string;
}

export default defineComponent({
  name: 'DashboardView',
  setup() {
    const livros = ref<Livro[]>([]);

    const fetchLivros = async () => {
      try {
        const token = localStorage.getItem('token');
        const response = await axios.get('/api/livros', {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });
        livros.value = response.data;
      } catch (error) {
        console.error('Erro ao buscar livros:', error);
      }
    };

    onMounted(() => {
      fetchLivros();
    });

    return {
      livros,
    };
  },
});
</script>

<style scoped>
/* Estilos específicos para este componente, se necessário */
</style>