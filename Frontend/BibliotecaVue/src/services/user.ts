import type { User, CreateUsuarioDto, UpdateUsuarioDto } from '@/types';
import { useAuthStore } from '@/stores/auth';

const BASE_URL = '/api/usuarios';

const getHeaders = () => {
    const authStore = useAuthStore();
    return {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${authStore.token}`,
    };
};

export const getUsers = async (): Promise<User[]> => {
    const response = await fetch(BASE_URL, { headers: getHeaders() });
    if (!response.ok) {
        throw new Error('Failed to fetch users');
    }
    return response.json();
};

export const registerUser = async (userData: CreateUsuarioDto): Promise<User> => {
    const response = await fetch(BASE_URL, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData),
    });
    if (!response.ok) {
        throw new Error('Failed to register user');
    }
    return response.json();
};

export const updateUser = async (id: number, userData: UpdateUsuarioDto): Promise<User> => {
    const response = await fetch(`${BASE_URL}/${id}`, {
        method: 'PUT',
        headers: getHeaders(),
        body: JSON.stringify(userData),
    });
    if (!response.ok) {
        throw new Error(`Failed to update user with id ${id}`);
    }
    return response.json();
};

export const deleteUser = async (id: number): Promise<void> => {
    const response = await fetch(`${BASE_URL}/${id}`, {
        method: 'DELETE',
        headers: getHeaders(),
    });
    if (!response.ok) {
        throw new Error(`Failed to delete user with id ${id}`);
    }
};
