using Biblioteca.ApplicationCore.Dtos;
using Biblioteca.ApplicationCore.Interfaces;
using Biblioteca.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;

namespace Biblioteca.ApplicationCore.Services
{
    /// <summary>
    /// A estrat�gia � usar as classes UserManager<Usuario> e RoleManager<IdentityRole<int>> injetadas diretamente
    /// no UsuarioService.O UserManager j� atua como uma camada de reposit�rio para o modelo Usuario, abstraindo 
    /// todo o acesso a dados (cria��o, busca, atualiza��o, exclus�o, gerenciamento de senhas, etc.). 
    /// 
    /// Criar um UsuarioRepository em cima do UserManager seria, em grande parte, apenas um inv�lucro redundante, 
    /// adicionando uma camada extra de c�digo sem um benef�cio claro, j� que o UserManager � a forma padr�o e 
    /// recomendada para gerenciar usu�rios com o Identity.
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly ITokenService _tokenService;

        public UsuarioService(UserManager<Usuario> userManager, RoleManager<IdentityRole<int>> roleManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        public async Task<UsuarioDto> CreateUserAsync(CreateUsuarioDto createUserDto)
        {
            var user = new Usuario
            {
                UserName = createUserDto.Email,
                Email = createUserDto.Email,
                Nome = createUserDto.Nome,
                PhoneNumber = createUserDto.Telefone,
                Tipo = createUserDto.Tipo
            };

            var result = await _userManager.CreateAsync(user, createUserDto.Senha);

            if (!result.Succeeded)
            {
                throw new System.Exception("Falha ao criar o usu�rio.");
            }

            await AddToRole(createUserDto.Tipo, user);

            return new UsuarioDto { Id = user.Id, Nome = user.Nome ?? "", Email = user.Email ?? "", Telefone = user.PhoneNumber ?? "", Tipo = user.Tipo ?? "" };
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public IEnumerable<UsuarioDto> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            return users.Select(user => new UsuarioDto { Id = user.Id, Nome = user.Nome ?? "", Email = user.Email ?? "", Telefone = user.PhoneNumber ?? "", Tipo = user.Tipo ?? "" });
        }

        public async Task<UsuarioDto?> GetUserByIdAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return null;
            return new UsuarioDto { Id = user.Id, Nome = user.Nome ?? "", Email = user.Email ?? "", Telefone = user.PhoneNumber ?? "", Tipo = user.Tipo ?? "" };
        }

        public async Task<(bool, TokenDto?)> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Senha))
            {
                return (false, null);
            }

            var token = await _tokenService.GenerateToken(user);
            return (true, new TokenDto { Token = token });
        }

        public async Task<UsuarioDto> UpdateUserAsync(int id, UpdateUsuarioDto updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new System.Exception("Usu�rio n�o encontrado.");
            }

            user.Nome = updateUserDto.Nome;
            user.Email = updateUserDto.Email;
            user.UserName = updateUserDto.Email;
            user.PhoneNumber = updateUserDto.Telefone;
            user.Tipo = updateUserDto.Tipo;

            var result = await _userManager.UpdateAsync(user);

            if (!string.IsNullOrEmpty(updateUserDto.Senha))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, updateUserDto.Senha);
            }
            
            await AddToRole(updateUserDto.Tipo, user);


            if (!result.Succeeded)
            { 
                throw new System.Exception("Falha ao atualizar o usu�rio.");
            }

            return new UsuarioDto { Id = user.Id, Nome = user.Nome ?? "", Email = user.Email ?? "", Telefone = user.PhoneNumber ?? "", Tipo = user.Tipo ?? "" };
        }

        private async Task AddToRole(string roleName, Usuario user)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole<int>(roleName));
            }
            
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
            await _userManager.AddToRoleAsync(user, roleName);
        }
    }
}
