import { defineStore } from 'pinia'
import type { LoginDto, CreateUsuarioDto, UpdateUsuarioDto } from '@/types'
import { registerUser, updateUser, deleteUser } from '@/services/user';

const BASE_URL = '/api/auth';

const userStr = localStorage.getItem('user');
const user = userStr && userStr !== 'undefined' ? JSON.parse(userStr) : null;

export const useAuthStore = defineStore('auth', {
  state: () => ({
    token: localStorage.getItem('token') || null,
    user: user,
  }),
  getters: {
    isAuthenticated: (state) => !!state.token,
    isAdmin: (state) => state.user?.tipo === 'Administrador',
  },
  actions: {
    async login(loginDto: LoginDto) {
      const response = await fetch(`${BASE_URL}/login`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(loginDto),
      })

      if (response.ok) {
        const data = await response.json()
        this.token = data.token
        localStorage.setItem('token', data.token)

        if (data.user) {
          this.user = data.user
          localStorage.setItem('user', JSON.stringify(data.user))
        } else {
            this.user = null;
            localStorage.removeItem('user');
        }
        
      } else {
        throw new Error('Failed to login')
      }
    },
    async register(registerDto: CreateUsuarioDto) {
      try {
        await registerUser(registerDto);
      } catch (error) {
        throw error;
      }
    },
    logout() {
      this.token = null
      this.user = null
      localStorage.removeItem('token')
      localStorage.removeItem('user')
    },
    async updateUser(userData: UpdateUsuarioDto) {
        try {
            if (!this.user?.id) {
                throw new Error("User ID not found for update.");
            }
            const updatedUser = await updateUser(this.user.id, userData);
            this.user = updatedUser;
            localStorage.setItem('user', JSON.stringify(updatedUser));
        } catch (error) {
            throw error;
        }
    },
    async deleteUser(userId: number) {
        try {
            await deleteUser(userId);
            // Optionally, log out the user if their own account was deleted
            if (this.user?.id === userId) {
                this.logout();
            }
        } catch (error) {
            throw error;
        }
    },
  },
})
