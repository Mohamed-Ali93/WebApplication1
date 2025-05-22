var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.WebApplication1>("webapplication1");
builder.AddProject<Projects.BookShop_Web>("bookshop");

builder.Build().Run();
