// --- Toutes les directives 'using' DOIVENT être au début du fichier ---
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog; // Pour une meilleure journalisation
using OpenTelemetry.Trace; // Pour le traçage (suivi des requêtes)
using OpenTelemetry.Exporter.Jaeger; // Pour envoyer les traces à Jaeger
using OpenTelemetry.Resources; // Pour nommer ton service dans OpenTelemetry
using OpenTelemetry.Logs; // Pour l'intégration des logs avec OpenTelemetry (optionnel)

// --- Fin des directives 'using' ---

var builder = WebApplication.CreateBuilder(args);

// --- Configuration de Serilog (Journalisation) ---
// Configure Serilog pour écrire les logs dans la console.
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console() // Les logs apparaîtront dans ta console (terminal VS Code)
    .CreateLogger();

builder.Host.UseSerilog(); // Indique à l'application d'utiliser Serilog comme son système de journalisation par défaut

// --- Configuration des Services (Injection de Dépendances) ---
// Ajoute la prise en charge des contrôleurs MVC/API, essentiels pour ton CustomerEventsController.
builder.Services.AddControllers();

// --- Configuration d'OpenTelemetry (Traçage et Observabilité) ---
builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        // Ajoute un nom pour ta source d'activité si tu crées des spans manuelles plus tard.
        tracerProviderBuilder.AddSource("CRMCustomerBehavior.ActivitySource");

        // Donne un nom clair à ton service. Ce nom apparaîtra dans Jaeger/autres outils de traçage.
        tracerProviderBuilder.SetResourceBuilder(
            ResourceBuilder.CreateDefault().AddService("CRMCustomerBehaviorService"));

        // Instrumente ASP.NET Core : capture automatiquement les traces des requêtes HTTP entrantes vers ton API.
        tracerProviderBuilder.AddAspNetCoreInstrumentation();

        // L'instrumentation pour HttpClient a été retirée temporairement pour résoudre une erreur.
        // Si tu as besoin de tracer des requêtes HTTP sortantes de ton API, nous pourrons la réintégrer plus tard.
        // tracerProviderBuilder.AddHttpClientInstrumentation(); 

        // Décommente si tu utilises Entity Framework Core pour interagir avec une base de données.
        // tracerProviderBuilder.AddEntityFrameworkCoreInstrumentation();

        // Configure l'exportateur Jaeger pour envoyer tes traces.
        // L'agent Jaeger doit être en cours d'exécution (souvent via Docker) sur cette adresse/port.
        tracerProviderBuilder.AddJaegerExporter(o =>
        {
            o.AgentHost = "172.17.0.1"; // <-- MODIFICATION ICI : Utilise host.docker.internal
            o.AgentPort = 6831; // Port par défaut de l'agent Jaeger
        });
    });

// --- Configuration de Swagger/OpenAPI (pour tester facilement tes API dans le navigateur) ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- Construction de l'application ---
var app = builder.Build();

// --- Configuration du Pipeline de Requêtes HTTP ---
// Ces middlewares déterminent comment les requêtes HTTP sont traitées par ton application.

// En environnement de développement, utilise Swagger pour explorer et tester tes API.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Tu pourras accéder à Swagger UI via /swagger dans ton navigateur
}

// Redirige les requêtes HTTP vers HTTPS pour une meilleure sécurité.
app.UseHttpsRedirection();

// Active le middleware d'autorisation (si tu as une logique de sécurité).
app.UseAuthorization();

// Mappe les routes de tes contrôleurs API (comme /CustomerEvents) aux méthodes correspondantes.
// C'est essentiel pour que ton API réponde aux requêtes.
app.MapControllers();

// Lance l'application et la fait écouter les requêtes.
app.Run();
