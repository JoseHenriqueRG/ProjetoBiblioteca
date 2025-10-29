<template>
  <div class="container mt-5">
    <div class="d-flex justify-content-between align-items-center mb-4">
      <h1>Gerenciamento de Livros</h1>
      <RouterLink to="/books/add" class="btn btn-primary">Cadastrar Livro</RouterLink>
    </div>

    <div class="mb-3">
      <input
        type="text"
        class="form-control"
        placeholder="Pesquisar por título, autor ou ISBN..."
        v-model="searchQuery"
        @input="handleSearch"
      />
    </div>

    <table class="table table-striped">
      <thead>
        <tr>
          <th>Título</th>
          <th>Autor</th>
          <th>Editora</th>
          <th>Ano</th>
          <th>ISBN</th>
          <th>Quantidade</th>
          <th>Ações</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="livro in livros" :key="livro.id">
          <td>{{ livro.titulo }}</td>
          <td>{{ livro.autor }}</td>
          <td>{{ livro.editora }}</td>
          <td>{{ livro.anoPublicacao }}</td>
          <td>{{ livro.isbn }}</td>
          <td>{{ livro.quantidade }}</td>
          <td>
            <RouterLink :to="`/books/edit/${livro.id}`" class="btn btn-sm btn-warning me-2">Editar</RouterLink>
            <button @click="deleteLivro(livro.id)" class="btn btn-sm btn-danger">Excluir</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue';
import axios from 'axios';
import { useRouter } from 'vue-router';

interface Livro {
  id: number;
  titulo: string;
  autor: string;
  editora: string;
  anoPublicacao: number;
  isbn: string;
  quantidade: number;
}

export default defineComponent({
  name: 'BookListView',
  setup() {
    const livros = ref<Livro[]>([]);
    const searchQuery = ref('');
    const router = useRouter();

    const fetchLivros = async () => {
      try {
        const token = localStorage.getItem('token');
        const response = await axios.get('/api/livros', {
          headers: { Authorization: `Bearer ${token}` },
        });
        livros.value = response.data;
      } catch (error) {
        console.error('Erro ao buscar livros:', error);
      }
    };

    const handleSearch = async () => {
      try {
        const token = localStorage.getItem('token');
        const response = await axios.get(`/api/livros/search?query=${searchQuery.value}`,
         {
          headers: { Authorization: `Bearer ${token}` },
        });
        livros.value = response.data;
      } catch (error) {
        console.error('Erro ao pesquisar livros:', error);
      }
    };

    const deleteLivro = async (id: number) => {
      if (confirm('Tem certeza que deseja excluir este livro?')) {
        try {
          const token = localStorage.getItem('token');
          await axios.delete(`/api/livros/${id}`, {
            headers: { Authorization: `Bearer ${token}` },
          });
          fetchLivros(); // Refresh the list
        } catch (error) {
          console.error('Erro ao excluir livro:', error);
          alert('Erro ao excluir livro. Verifique se você tem permissão.');
        }
      }
    };

    onMounted(fetchLivros);

    return {
      livros,
      searchQuery,
      handleSearch,
      deleteLivro,
    };
  },
});
</script>

<style scoped>
/* Estilos específicos para este componente, se necessário */
</style>
