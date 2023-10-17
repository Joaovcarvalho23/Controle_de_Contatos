using Controle_de_Contatos.Data;
using Controle_de_Contatos.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace Controle_de_Contatos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<BancoDeDadosContext>(
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//quando chamarmos a interface IHttpContextAcessor, ele vai implementar a classe do HttpContextAcessor

            //Na linha 16, configuramos a inje��o de depend�ncia do contexto. Agora, precisamos configurar a inje��o de depend�ncia da Interface. Quando injetar a interface, quem deve resolver a classe de implementa��o? � feito abaixo:
            builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();//toda vez que a IContatoRepositorio for chamada, queremos que resolva chamar ContatoRepositorio
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();//registrando interface e reposit�rio do Usu�rio

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}