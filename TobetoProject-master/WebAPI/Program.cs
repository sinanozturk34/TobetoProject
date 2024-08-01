using Business;
using Core.CrossCutingConcerns.Exceptions.Extensions;
using DataAccess;
using WebAPI.Utilities;
using Microsoft.OpenApi.Models;
using Autofac.Core;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;

using Autofac.Extensions.DependencyInjection;
using Autofac;
using Core.Utilities.Security.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.Utilities.Security.Jwt;
using Core.DependencyResolvers;
using Core.Utilities.IoC;
using Core.Extensions;
using Core.Aspects;
using Business.Autofac;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));

// Add services to the container.
builder.Services.AddBusinessServices();
builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddValidationAspect();
builder.Services.AddControllers();


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
});

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });



builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",       
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "Please enter token"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
                { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            new string[] { }
        }
    });
});


builder.Services.AddDependencyResolvers(new ICoreModule[] { new CoreModule() });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]}");
    c.AddServer(new OpenApiServer()
    {
        Url= "http://localhost:5062",
        
    }); 
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();


app.UseAuthentication();

app.UseAuthorization();


app.UseCors((cors) => { cors.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });

app.MapControllers();

app.Run();
