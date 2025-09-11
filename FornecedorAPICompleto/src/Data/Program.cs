using System.Text.Json.Serialization;
using Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// ---Configuração da API ---
var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();


// --- Configuração do Entity Framework e do Banco de Dados ---
builder.Services.AddDbContext<MeuDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- Configuração de Autenticação JWT (Placeholder) ---
// Em um sistema real, você configuraria a validação do token JWT aqui.
// Ex: builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)...

var app = builder.Build();
 
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Inicializa o banco de dados e aplica as migrações ao iniciar a aplicação.
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<EstoqueDbContext>();
//    context.Database.Migrate();
//}

app.Run();