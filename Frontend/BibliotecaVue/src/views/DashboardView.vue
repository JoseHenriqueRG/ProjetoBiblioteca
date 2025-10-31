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
            </tr>
          </thead>
          <tbody>
            <tr v-for="livro in livros" :key="livro.id">
              <td>{{ livro.titulo }}</td>
              <td>{{ livro.autor }}</td>
              <td>{{ livro.isbn }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue';
import { getLivros } from '@/services/book';
import type { LivroDto } from '@/types';

export default defineComponent({
  name: 'DashboardView',
  setup() {
    const livros = ref<LivroDto[]>([]);

    const fetchLivros = async () => {
      try {
        livros.value = await getLivros();
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