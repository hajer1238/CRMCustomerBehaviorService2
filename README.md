Projet CRMCustomerBehavior - Semaine 2 : API de Gestion des Événements Clients avec Observabilité# CRMCustomerBehaviorService2
🎯 Objectifs de la Semaine 2
Développer une API ASP.NET Core pour enregistrer les événements liés au comportement des clients.

Intégrer Serilog pour une journalisation structurée des événements de l'application.

Mettre en place le traçage distribué avec OpenTelemetry.

Utiliser Jaeger comme backend pour la visualisation des traces.

Containeriser Jaeger avec Docker.

Tester l'API et observer les traces générées.

🚀 Fonctionnalités Implémentées
API ASP.NET Core : Un endpoint /CustomerEvents capable de recevoir des événements client via des requêtes HTTP POST.

Journalisation (Serilog) : Configuration de Serilog pour enregistrer les informations importantes de l'application dans la console.

Traçage Distribué (OpenTelemetry) :

Intégration du SDK OpenTelemetry pour ASP.NET Core.

Instrumentation automatique des requêtes HTTP entrantes.

Configuration d'un Service Name clair (CRMCustomerBehaviorService) pour l'identification dans Jaeger.

Utilisation de l'exporteur Jaeger pour envoyer les traces.

Containerisation (Docker) : Déploiement de Jaeger en tant que conteneur Docker jaegertracing/all-in-one.

Test d'API (Postman) : Utilisation de Postman pour envoyer des requêtes à l'API et déclencher la génération de traces.

📂 Structure du Projet
crm1/
├── CRMCustomerBehavior/
│   ├── Controllers/
│   │   └── CustomerEventsController.cs  # Gère le endpoint /CustomerEvents
│   ├── Models/
│   │   └── CustomerEvent.cs             # Modèle de données pour un événement client
│   ├── Program.cs                       # Configuration de l'application, Serilog, OpenTelemetry
│   ├── CRMCustomerBehavior.csproj       # Fichier projet .NET
│   └── appsettings.json                 # Fichier de configuration
└── README.md                            # Ce fichier

🛠️ Comment Lancer le Projet
Suivez ces étapes pour démarrer l'API et Jaeger sur votre machine.

Pré-requis
Docker Desktop (Assurez-vous qu'il est installé, configuré avec WSL2, et qu'il est "running").

.NET 8 SDK ou version ultérieure.

Postman (ou un outil similaire pour envoyer des requêtes HTTP).

Un terminal PowerShell (recommandé).

Instructions de Lancement
Cloner le Dépôt :
Ouvrez un terminal PowerShell et clonez ce dépôt :

git clone https://github.com/TonNomUtilisateur/TonNomDeDepot.git
cd TonNomDeDepot # Remplacez par le nom de votre dépôt cloné (ex: crm1)

Vérifier et Mettre à Jour Program.cs :
Ouvrez le fichier CRMCustomerBehavior/Program.cs. Assurez-vous que la configuration de l'exporteur Jaeger utilise l'adresse IP de la passerelle Docker.

Trouvez l'IP de la passerelle Docker :
Ouvrez un terminal PowerShell en tant qu'Administrateur et exécutez :

docker network inspect bridge --format "{{(index .IPAM.Config 0).Gateway}}"

Notez l'adresse IP affichée (ex: 172.17.0.1).

Mettez à jour Program.cs :
Modifiez la ligne o.AgentHost avec cette IP :

// ... dans la section AddJaegerExporter
tracerProviderBuilder.AddJaegerExporter(o =>
{
    o.AgentHost = "VOTRE_IP_DE_PASSERELLE"; // <-- REMPLACEZ PAR L'IP RÉELLE TROUVÉE
    o.AgentPort = 6831;
});

Sauvegardez le fichier Program.cs !

Désactiver Temporairement le Pare-feu et l'Antivirus (Recommandé pour le test) :
Pour éviter les blocages de communication, il est fortement recommandé de désactiver temporairement le Pare-feu Windows Defender (pour les profils privé et public) et tout antivirus tiers avant de lancer les services. N'oubliez pas de les réactiver après vos tests.

Lancer Jaeger (Conteneur Docker) :

Assurez-vous que Docker Desktop est bien lancé et indique "Engine running".

Ouvrez un terminal PowerShell en tant qu'Administrateur.

Arrêtez et supprimez tout conteneur Jaeger existant pour une nouvelle session propre :

docker stop jaeger
docker rm jaeger

Lancez le conteneur Jaeger :

docker run -d --name jaeger -e COLLECTOR_ZIPKIN_HOST_PORT=:9411 -e COLLECTOR_OTLP_ENABLED=true -p 6831:6831/udp -p 16686:16686 jaegertracing/all-in-one:latest

Vérifiez que Jaeger tourne : docker ps (vous devriez voir jaeger avec le statut Up).

Accédez à l'interface Jaeger dans votre navigateur : http://localhost:16686. (Elle devrait s'ouvrir).

Lancer l'API ASP.NET Core :

Ouvrez un NOUVEAU terminal PowerShell (pas celui de Jaeger).

Naviguez vers le dossier de l'API :

cd CRMCustomerBehavior

Lancez l'application :

dotnet run

Attendez que l'application démarre et affiche Now listening on: http://localhost:5130.

Envoyer des Requêtes depuis Postman :

Ouvrez Postman.

Créez une requête POST vers http://localhost:5130/CustomerEvents.

Définissez le Content-Type à application/json dans les headers.

Utilisez un corps JSON comme suit (vous pouvez envoyer plusieurs requêtes en variant les valeurs) :

{
    "customerId": "client_001",
    "eventType": "LoggedIn",
    "timestamp": "2025-07-18T10:00:00Z"
}

Envoyez plusieurs requêtes. Vérifiez les logs dans le terminal de votre API pour confirmer qu'elles sont bien reçues.

Vérifier les Traces dans Jaeger :

Retournez à l'interface Jaeger dans votre navigateur (http://localhost:16686).

Dans le panneau de gauche, sous "Search", cliquez sur le menu déroulant "Service".

Sélectionnez CRMCustomerBehaviorService (si visible).

Cliquez sur le bouton "Find Traces".

⚠️ Statut Actuel du Traçage Jaeger
Malgré une configuration correcte et de nombreuses tentatives de dépannage, les traces générées par le service CRMCustomerBehaviorService n'apparaissent pas encore dans l'interface de Jaeger sous leur propre nom. Jaeger affiche uniquement ses propres traces internes (jaeger-all-in-one).

Ce qui a été confirmé et fonctionne :

L'API ASP.NET Core démarre correctement et traite les requêtes (visible dans les logs du terminal de l'API).

Docker Desktop est pleinement opérationnel ("Engine running").

Le conteneur Jaeger est lancé, accessible via http://localhost:16686, et reçoit même ses propres traces internes.

La configuration d'OpenTelemetry dans Program.cs est correcte, utilisant l'adresse IP de la passerelle Docker pour la communication.

Des étapes de dépannage approfondies ont été menées, incluant la désactivation temporaire du pare-feu Windows et de l'antivirus, ainsi que des tests de connectivité réseau.

Problème persistant :

Il semble y avoir un blocage de communication très spécifique et persistant (probablement lié au trafic UDP) entre l'application .NET (sur Windows) et le conteneur Jaeger (dans l'environnement WSL2/Docker), qui n'a pas pu être résolu à distance. Cela pourrait nécessiter un diagnostic direct de la configuration réseau du système.

➡️ Prochaines Étapes / Travail Futur
Diagnostiquer et résoudre le problème de communication réseau pour que les traces de CRMCustomerBehaviorService apparaissent dans Jaeger.

Ajouter des instrumentations personnalisées (custom spans) dans le code de l'API pour tracer des opérations spécifiques.

Explorer d'autres fonctionnalités d'OpenTelemetry (métriques, logs).

Déployer l'API et Jaeger dans un environnement de test ou de production.
