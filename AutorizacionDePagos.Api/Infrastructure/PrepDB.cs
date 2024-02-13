using Microsoft.EntityFrameworkCore;

namespace AutorizacionDePagos.Api.Infrastructure
{
    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            Console.WriteLine("Appling migrations...");
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }
        }
    }
}
