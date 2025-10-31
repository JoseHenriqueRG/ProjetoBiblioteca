import type { LocacaoDto, CreateLocacaoDto } from '@/types';
import { useAuthStore } from '@/stores/auth';

const BASE_URL = 'https://localhost:44325/api/locacoes';

const getHeaders = () => {
    const authStore = useAuthStore();
    return {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${authStore.token}`,
    };
};

export const getLocacoes = async (): Promise<LocacaoDto[]> => {
    const response = await fetch(BASE_URL, { headers: getHeaders() });
    if (!response.ok) {
        throw new Error('Failed to fetch rentals');
    }
    return response.json();
};

export const createLocacao = async (locacao: CreateLocacaoDto): Promise<LocacaoDto> => {
    const response = await fetch(BASE_URL, {
        method: 'POST',
        headers: getHeaders(),
        body: JSON.stringify(locacao),
    });
    if (!response.ok) {
        throw new Error('Failed to create rental');
    }
    return response.json();
};

export const devolverLocacao = async (id: number): Promise<void> => {
    const response = await fetch(`${BASE_URL}/${id}/devolver`, {
        method: 'PUT',
        headers: getHeaders(),
    });
    if (!response.ok) {
        throw new Error('Failed to return rental');
    }
};

export const renovarLocacao = async (id: number, diasParaRenovar: number): Promise<void> => {
    const response = await fetch(`${BASE_URL}/${id}/renovar?diasParaRenovar=${diasParaRenovar}`, {
        method: 'PUT',
        headers: getHeaders(),
    });
    if (!response.ok) {
        throw new Error('Failed to renew rental');
    }
};

export const getLocacoesByUserId = async (userId: number): Promise<LocacaoDto[]> => {
    const response = await fetch(`${BASE_URL}/user/${userId}`, { headers: getHeaders() });
    if (!response.ok) {
        throw new Error('Failed to fetch rentals for user');
    }
    return response.json();
};
