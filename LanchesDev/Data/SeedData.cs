using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace LanchesDev.Data
{
    public static class SeedData
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            //Perfis customizados
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            //Perfis em um array de string
            string[] roleNames = { "Admin", "Member" };
            IdentityResult roleResult;

            //Percorrer o array e verifica se já existe
            foreach (var roleName in roleNames)
            {
                //cria perfil e os inclui no banco
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Criação do super úsuario
            var poweruser = new IdentityUser
            {
                //Pegar dados da configuração
                UserName = Configuration.GetSection("UserSettings")["UserName"],
                Email = Configuration.GetSection("UserSettings")["UserMail"]
            };

            string userPassword = Configuration.GetSection("UserSettings")["UserPassword"];

            //verifica se existe um usuário com o email informado
            var user = await UserManager.FindByEmailAsync(Configuration.GetSection("UserSettings")["UserEmail"]);

            if (user == null)
            {
                //cria o super usuário com os dados informados
                var createPowerUser = await UserManager.CreateAsync(poweruser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    // atribui o usuário ao perfil Admin
                    await UserManager.AddToRoleAsync(poweruser, "Admin");
                }
            }
        }
    }
}
