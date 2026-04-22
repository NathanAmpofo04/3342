var builder = WebApplication.CreateBuilder(args);

// 1. Tell the app to support Controllers (this is the "brain" for your ProfileController)
builder.Services.AddControllers();

// 2. Turn on the Swagger testing page tools
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 3. Enable the Swagger testing page (even in development)
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// 4. Tell the app to find and "map" your ProfileController
app.MapControllers();

app.Run();