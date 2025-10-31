<template>
  <div class="container mt-5">
    <h2>Nova Locação</h2>
    <form @submit.prevent="handleSubmit">
      <div class="mb-3">
        <label for="livro" class="form-label">Livro</label>
        <select class="form-select" id="livro" v-model="livroId">
          <option v-for="book in books" :key="book.id" :value="book.id">{{ book.titulo }}</option>
        </select>
      </div>
      <div class="mb-3">
        <label for="usuario" class="form-label">Usuário</label>
        <select class="form-select" id="usuario" v-model="usuarioId">
          <option v-for="user in users" :key="user.id" :value="user.id">{{ user.nome }}</option>
        </select>
      </div>
      <div class="mb-3">
        <label for="dataRetirada" class="form-label">Data da Retirada</label>
        <input type="date" class="form-control" id="dataRetirada" v-model="dataRetirada" />
      </div>
      <div class="mb-3">
        <label for="dataDevolucao" class="form-label">Data da Devolução</label>
        <input type="date" class="form-control" id="dataDevolucao" v-model="dataDevolucao" />
      </div>
      <button type="submit" class="btn btn-primary">Salvar</button>
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { createLocacao } from '@/services/rental';
import { getLivros } from '@/services/book';
import { getUsers } from '@/services/user';
import type { LivroDto, User } from '@/types';

export default defineComponent({
  name: 'RentalFormView',
  setup() {
    const router = useRouter();
    const books = ref<LivroDto[]>([]);
    const users = ref<User[]>([]);
    const livroId = ref<number | null>(null);
    const usuarioId = ref<number | null>(null);
    const dataRetirada = ref('');
    const dataDevolucao = ref('');

    const fetchBooksAndUsers = async () => {
      try {
        books.value = await getLivros();
        users.value = await getUsers();
      } catch (error) {
        console.error(error);
        alert('Erro ao buscar livros e usuários.');
      }
    };

    onMounted(fetchBooksAndUsers);

    const handleSubmit = async () => {
      if (!livroId.value || !usuarioId.value || !dataRetirada.value || !dataDevolucao.value) {
        alert('Todos os campos são obrigatórios.');
        return;
      }

      const rentalData = {
        LivroId: livroId.value,
        UsuarioId: usuarioId.value,
        DataRetirada: new Date(dataRetirada.value + 'T00:00:00Z').toISOString(),
        DataDevolucaoPrevista: new Date(dataDevolucao.value + 'T00:00:00Z').toISOString(),
      };

      console.log('Creating rental with data:', rentalData);

      try {
        await createLocacao(rentalData);
        router.push('/rentals');
      } catch (error: any) {
        console.error(error);
        alert(`Erro ao criar locação: ${error.message}`);
      }
    };

    return {
      books,
      users,
      livroId,
      usuarioId,
      dataRetirada,
      dataDevolucao,
      handleSubmit,
    };
  },
});
</script>
