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
                throw new System.Exception("Falha ao criar o usuário.");
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

        public async Task<(bool, LoginResponseDto?)> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Senha))
            {
                return (false, null);
            }

            var token = await _tokenService.GenerateToken(user);
            var userDto = new UsuarioDto { Id = user.Id, Nome = user.Nome ?? "", Email = user.Email ?? "", Telefone = user.PhoneNumber ?? "", Tipo = user.Tipo ?? "" };
            var loginResponse = new LoginResponseDto { Token = token, User = userDto };

            return (true, loginResponse);
        }

        public async Task<UsuarioDto> UpdateUserAsync(int id, UpdateUsuarioDto updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new System.Exception("Usuário não encontrado.");
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
                throw new System.Exception("Falha ao atualizar o usuário.");
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
