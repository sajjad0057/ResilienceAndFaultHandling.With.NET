using Polly;
using PracticePolly.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add HttpClient with Polly policies
builder.Services.AddHttpClient<IPostService, PostService>(client =>
 {
    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
})
.AddTransientHttpErrorPolicy(policy =>
    policy.WaitAndRetryAsync(3, retry => TimeSpan.FromSeconds(Math.Pow(2, retry))))
.AddTransientHttpErrorPolicy(policy =>
    policy.CircuitBreakerAsync(2, TimeSpan.FromSeconds(10)))
.AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(5));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
