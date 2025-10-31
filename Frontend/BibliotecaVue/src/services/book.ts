import type { LivroDto, CreateLivroDto, UpdateLivroDto } from '@/types';
import { useAuthStore } from '@/stores/auth';

const BASE_URL = 'https://localhost:44325/api/livros';

const getHeaders = () => {
    const authStore = useAuthStore();
    return {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${authStore.token}`,
    };
};

export const getLivros = async (): Promise<LivroDto[]> => {
    const response = await fetch(BASE_URL, { headers: getHeaders() });
    if (!response.ok) {
        throw new Error('Failed to fetch books');
    }
    return response.json();
};

export const getBookById = async (id: number): Promise<LivroDto> => {
    const response = await fetch(`${BASE_URL}/${id}`, { headers: getHeaders() });
    if (!response.ok) {
        throw new Error(`Failed to fetch book with id ${id}`);
    }
    return response.json();
};

export const createBook = async (bookData: CreateLivroDto): Promise<LivroDto> => {
    const response = await fetch(BASE_URL, {
        method: 'POST',
        headers: getHeaders(),
        body: JSON.stringify(bookData),
    });
    if (!response.ok) {
        throw new Error('Failed to create book');
    }
    return response.json();
};

export const updateBook = async (id: number, bookData: UpdateLivroDto): Promise<LivroDto> => {
    const response = await fetch(`${BASE_URL}/${id}`, {
        method: 'PUT',
        headers: getHeaders(),
        body: JSON.stringify(bookData),
    });
    if (!response.ok) {
        throw new Error(`Failed to update book with id ${id}`);
    }
    return response.json();
};

export const searchBooks = async (query: string): Promise<LivroDto[]> => {
    const response = await fetch(`${BASE_URL}/search?query=${query}`, { headers: getHeaders() });
    if (!response.ok) {
        throw new Error('Failed to search books');
    }
    return response.json();
};

export const deleteBook = async (id: number): Promise<void> => {
    const response = await fetch(`${BASE_URL}/${id}`, {
        method: 'DELETE',
        headers: getHeaders(),
    });
    if (!response.ok) {
        throw new Error(`Failed to delete book with id ${id}`);
    }
};
