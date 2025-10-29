<template>
  <div class="container mt-5">
    <div class="row justify-content-center">
      <div class="col-md-8">
        <div class="card">
          <div class="card-header">{{ isEditMode ? 'Editar Livro' : 'Cadastro de Livro' }}</div>
          <div class="card-body">
            <form @submit.prevent="handleBookSubmit" novalidate>
              <div class="mb-3">
                <label for="title" class="form-label">Título</label>
                <input
                  type="text"
                  class="form-control"
                  :class="{ 'is-invalid': titleError }"
                  id="title"
                  v-model="livro.titulo"
                  required
                  @input="titleError = ''"
                />
                <div class="invalid-feedback">{{ titleError }}</div>
              </div>
              <div class="mb-3">
                <label for="author" class="form-label">Autor</label>
                <input
                  type="text"
                  class="form-control"
                  :class="{ 'is-invalid': authorError }"
                  id="author"
                  v-model="livro.autor"
                  required
                  @input="authorError = ''"
                />
                <div class="invalid-feedback">{{ authorError }}</div>
              </div>
              <div class="mb-3">
                <label for="isbn" class="form-label">ISBN</label>
                <input
                  type="text"
                  class="form-control"
                  :class="{ 'is-invalid': isbnError }"
                  id="isbn"
                  v-model="livro.isbn"
                  required
                  @input="isbnError = ''"
                />
                <div class="invalid-feedback">{{ isbnError }}</div>
              </div>
              <div class="mb-3">
                <label for="editora" class="form-label">Editora</label>
                <input
                  type="text"
                  class="form-control"
                  id="editora"
                  v-model="livro.editora"
                />
              </div>
              <div class="mb-3">
                <label for="anoPublicacao" class="form-label">Ano de Publicação</label>
                <input
                  type="number"
                  class="form-control"
                  id="anoPublicacao"
                  v-model.number="livro.anoPublicacao"
                />
              </div>
              <div class="mb-3">
                <label for="quantidade" class="form-label">Quantidade</label>
                <input
                  type="number"
                  class="form-control"
                  id="quantidade"
                  v-model.number="livro.quantidade"
                  required
                />
              </div>
              <button type="submit" class="btn btn-primary">{{ isEditMode ? 'Salvar Alterações' : 'Cadastrar Livro' }}</button>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted, computed } from 'vue';
import axios from 'axios';
import { useRouter, useRoute } from 'vue-router';

interface Livro {
  id: number | null;
  titulo: string;
  autor: string;
  editora: string;
  anoPublicacao: number | null;
  isbn: string;
  quantidade: number | null;
}

export default defineComponent({
  name: 'BookFormView',
  setup() {
    const livro = ref<Livro>({ 
        id: null,
        titulo: '', 
        autor: '', 
        editora: '', 
        anoPublicacao: null, 
        isbn: '', 
        quantidade: null 
    });

    const titleError = ref('');
    const authorError = ref('');
    const isbnError = ref('');
    const router = useRouter();
    const route = useRoute();

    const isEditMode = computed(() => !!route.params.id);

    onMounted(async () => {
      if (isEditMode.value) {
        try {
          const token = localStorage.getItem('token');
          const response = await axios.get(`/api/livros/${route.params.id}`,
          {
            headers: { Authorization: `Bearer ${token}` },
          });
          livro.value = response.data;
        } catch (error) {
          console.error('Erro ao buscar dados do livro:', error);
          alert('Não foi possível carregar os dados do livro para edição.');
        }
      }
    });

    const validateForm = () => {
      let isValid = true;
      titleError.value = '';
      authorError.value = '';
      isbnError.value = '';

      if (!livro.value.titulo) {
        titleError.value = 'O título é obrigatório.';
        isValid = false;
      }

      if (!livro.value.autor) {
        authorError.value = 'O autor é obrigatório.';
        isValid = false;
      }

      if (!livro.value.isbn) {
        isbnError.value = 'O ISBN é obrigatório.';
        isValid = false;
      }

      return isValid;
    };

    const handleBookSubmit = async () => {
      if (!validateForm()) {
        return;
      }
      try {
        const token = localStorage.getItem('token');
        const headers = { Authorization: `Bearer ${token}` };
        const bookData = {
            titulo: livro.value.titulo,
            autor: livro.value.autor,
            editora: livro.value.editora,
            isbn: livro.value.isbn,
            anoPublicacao: livro.value.anoPublicacao,
            quantidade: livro.value.quantidade,
        };

        if (isEditMode.value) {
          await axios.put(`/api/livros/${route.params.id}`, bookData, { headers });
        } else {
          await axios.post('/api/livros', bookData, { headers });
        }
        router.push('/books');
      } catch (error) {
        alert(`Erro ao ${isEditMode.value ? 'atualizar' : 'cadastrar'} livro. Verifique os dados e tente novamente.`);
      }
    };

    return {
      livro,
      isEditMode,
      titleError,
      authorError,
      isbnError,
      handleBookSubmit,
    };
  },
});
</script>

<style scoped>
/* Estilos específicos para este componente, se necessário */
</style>
