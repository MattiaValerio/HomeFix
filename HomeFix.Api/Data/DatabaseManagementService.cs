using Microsoft.EntityFrameworkCore;

namespace HomeFix.Api.Data;

public class DatabaseManagementService
{
    public static void MigrateDatabase(IApplicationBuilder app)
    {
        int t = 0;
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using DataContext context = scope.ServiceProvider.GetRequiredService<DataContext>();


        while (!context.Database.CanConnect() && t <= 5)
        {
            Console.WriteLine("DB not ready...");
            t += 1;
            Thread.Sleep(5000);
        }

        if (context.Database.CanConnect())
        {
            context.Database.Migrate();
        }

        t = 0;
    }
}