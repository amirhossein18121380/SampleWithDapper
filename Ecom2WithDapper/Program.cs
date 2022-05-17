using DataAccess.Dal;
using DataAccess.Tools;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICategoryDal, CategoryDal>();
builder.Services.AddScoped<IProductCategoryDal, ProductCategoryDal>();
builder.Services.AddScoped<IProductDal, ProductDal>();
builder.Services.AddScoped<IProductReceiptDal, ProductReceiptDal>();
builder.Services.AddScoped<IProductSaleFactorDal, ProductSaleFactorDal>();
builder.Services.AddScoped<IReceiptDal, ReceiptDal>();
builder.Services.AddScoped<ISaleFactorDal, SaleFactorDal>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


ConfigurationHelper.Configure(app.Configuration);
app.Run();
