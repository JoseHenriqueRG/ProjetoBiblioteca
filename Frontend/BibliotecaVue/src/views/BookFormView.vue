<template>
  <div class="container mt-5">
    <div class="row justify-content-center">
      <div class="col-md-8">
        <div class="card">
          <div class="card-header">Cadastro de Livro</div>
          <div class="card-body">
            <form @submit.prevent="handleBookSubmit" novalidate>
              <div class="mb-3">
                <label for="title" class="form-label">Título</label>
                <input
                  type="text"
                  class="form-control"
                  :class="{ 'is-invalid': titleError }"
                  id="title"
                  v-model="title"
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
                  v-model="author"
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
                  v-model="isbn"
                  required
                  @input="isbnError = ''"
                />
                <div class="invalid-feedback">{{ isbnError }}</div>
              </div>
              <div class="mb-3">
                <label for="publicationYear" class="form-label">Ano de Publicação</label>
                <input
                  type="number"
                  class="form-control"
                  :class="{ 'is-invalid': publicationYearError }"
                  id="publicationYear"
                  v-model="publicationYear"
                  required
                  @input="publicationYearError = ''"
                />
                <div class="invalid-feedback">{{ publicationYearError }}</div>
              </div>
              <div class="mb-3">
                <label for="genre" class="form-label">Gênero</label>
                <input
                  type="text"
                  class="form-control"
                  id="genre"
                  v-model="genre"
                />
              </div>
              <div class="mb-3">
                <label for="editora" class="form-label">Editora</label>
                <input
                  type="text"
                  class="form-control"
                  id="editora"
                  v-model="editora"
                />
              </div>
              <div class="mb-3">
                <label for="quantidade" class="form-label">Quantidade</label>
                <input
                  type="number"
                  class="form-control"
                  id="quantidade"
                  v-model="quantidade"
                  required
                />
              </div>
              <button type="submit" class="btn btn-primary">Salvar Livro</button>
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
  name: 'BookFormView',
  setup() {
    const title = ref('');
    const author = ref('');
    const isbn = ref('');
    const publicationYear = ref<number | null>(null);
    const genre = ref('');
    const editora = ref('');
    const quantidade = ref<number | null>(null);

    const titleError = ref('');
    const authorError = ref('');
    const isbnError = ref('');
    const publicationYearError = ref('');
    const router = useRouter();

    const validateForm = () => {
      let isValid = true;
      titleError.value = '';
      authorError.value = '';
      isbnError.value = '';
      publicationYearError.value = '';

      if (!title.value) {
        titleError.value = 'O título é obrigatório.';
        isValid = false;
      }

      if (!author.value) {
        authorError.value = 'O autor é obrigatório.';
        isValid = false;
      }

      if (!isbn.value) {
        isbnError.value = 'O ISBN é obrigatório.';
        isValid = false;
      }

      if (!publicationYear.value) {
        publicationYearError.value = 'O ano de publicação é obrigatório.';
        isValid = false;
      } else if (isNaN(publicationYear.value) || publicationYear.value < 1000 || publicationYear.value > new Date().getFullYear()) {
        publicationYearError.value = 'Por favor, insira um ano de publicação válido.';
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
        await axios.post(
          '/api/livros',
          {
            titulo: title.value,
            autor: author.value,
            isbn: isbn.value,
            editora: editora.value,
            quantidade: quantidade.value,
          },
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
        router.push('/dashboard');
      } catch (error) {
        alert('Erro ao cadastrar livro. Verifique os dados e tente novamente.');
      }
    };

    return {
      title,
      author,
      isbn,
      publicationYear,
      genre,
      editora,
      quantidade,
      titleError,
      authorError,
      isbnError,
      publicationYearError,
      handleBookSubmit,
    };
  },
});
</script>

<style scoped>
/* Estilos específicos para este componente, se necessário */
</style>
