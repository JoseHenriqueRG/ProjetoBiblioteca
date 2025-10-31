export interface LoginDto {
  email: string;
  senha?: string;
}

export interface CreateUsuarioDto {
  nome: string;
  email: string;
  telefone: string;
  senha?: string;
  tipo: 'administrador' | 'padrão';
}

export interface UpdateUsuarioDto {
  nome: string;
  email: string;
  telefone: string;
  tipo: 'administrador' | 'padrão';
}

export interface User {
    id: number;
    nome: string;
    email: string;
    telefone: string;
    tipo: 'administrador' | 'padrão';
}

export interface LocacaoDto {
    id: number;
    livroId: number;
    livroTitulo: string;
    usuarioId: number;
    usuarioNome: string;
    dataRetirada: string;
    dataDevolucaoPrevista: string;
    dataDevolucaoReal?: string;
    multa: number;
    status: 'Pendente' | 'Devolvido';
}

export interface CreateLocacaoDto {
    LivroId: number;
    UsuarioId: number;
    DataRetirada: string;
    DataDevolucaoPrevista: string;
}

export interface LivroDto {
    id: number;
    titulo: string;
    autor: string;
    isbn: string;
    editora: string;
    anoPublicacao: number | null;
    quantidade: number | null;
}

export interface CreateLivroDto {
    titulo: string;
    autor: string;
    editora: string;
    anoPublicacao: number | null;
    isbn: string;
    quantidade: number | null;
}

export interface UpdateLivroDto {
    titulo: string;
    autor: string;
    editora: string;
    anoPublicacao: number | null;
    isbn: string;
    quantidade: number | null;
}

export interface DevolucaoDto {
    multa: number;
}