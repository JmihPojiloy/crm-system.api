using api.Data;
using api.Data.SiteContentData;
using api.Mapper;
using api.Entities;
using api.Dto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ILeadsRepository, LeadsRepository>();
builder.Services.AddScoped<IRepository<BlogPostDto>, BlogRepository>();
builder.Services.AddScoped<IRepository<ContactInfoDto>, ContactRepository>();
builder.Services.AddScoped<IRepository<MainContentDto>, MainContentRepository>();
builder.Services.AddScoped<IRepository<ProjectDto>, ProjectRepository>();
builder.Services.AddScoped<IRepository<ServiceDto>, ServiceRepository>();

builder.Services.AddScoped<LeadMapper>();
builder.Services.AddScoped<IMapper<BlogPostDto, BlogPost>, BlogMapper>();
builder.Services.AddScoped<IMapper<ContactInfoDto, ContactInfo>, ContactInfoMapper>();
builder.Services.AddScoped<IMapper<MainContentDto, MainContent>, MainContentMapper>();
builder.Services.AddScoped<IMapper<ProjectDto, Project>, ProjectMapper>();
builder.Services.AddScoped<IMapper<ServiceDto, Service>, ServiceMapper>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Leads API",
        Description = "Web API for final project",
        Version = "v1"
    });
});

builder.Services.AddDbContext<LeadDb>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Lead"));
});

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LeadDb>();
    context.Database.Migrate();
    context.InitializeDatabase();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lead collection service");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
