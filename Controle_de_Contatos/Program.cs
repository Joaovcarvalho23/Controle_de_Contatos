using Controle_de_Contatos.Data;
using Controle_de_Contatos.Helper;
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
            builder.Services.AddScoped<ISessao, Sessao>();//registrando interface e classe da sess�o do Usu�rio

            builder.Services.AddSession(o =>
            {
                o.Cookie.HttpOnly = true; //o cookie de sess�o s� pode ser acessado por meio de solicita��es HTTP e n�o pode ser acessado por scripts no lado do cliente. Isso ajuda a proteger a sess�o contra ataques XSS (Cross-Site Scripting).
                o.Cookie.IsEssential = true; //usado para garantir que o cookie de sess�o seja sempre enviado, mesmo se o usu�rio optar por n�o aceitar cookies.
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}