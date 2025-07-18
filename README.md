Projet CRMCustomerBehavior - Semaine 2 : Mon Travail
Ce document résume mon travail et mes réalisations durant la Semaine 2 de mon stage, axé sur la mise en place d'une API et l'intégration de l'observabilité.

🎯 Ce que j'ai fait cette semaine
Cette semaine, j'ai travaillé sur les points suivants :

Développement de l'API : J'ai créé une API ASP.NET Core simple avec un endpoint /CustomerEvents pour enregistrer les événements clients.

Intégration de la Journalisation (Serilog) : J'ai configuré Serilog pour que l'API puisse générer des logs structurés, ce qui est essentiel pour comprendre le comportement de l'application.

Mise en place du Traçage Distribué (OpenTelemetry) : J'ai intégré OpenTelemetry dans l'API pour capturer automatiquement les traces des requêtes HTTP. J'ai veillé à ce que mon service soit correctement nommé (CRMCustomerBehaviorService) pour être identifié dans les outils de traçage.

Déploiement de Jaeger avec Docker : J'ai utilisé Docker pour lancer Jaeger, le système de visualisation des traces. J'ai appris à gérer les conteneurs Docker (lancer, arrêter, supprimer, vérifier le statut).

Tests et Dépannage : J'ai utilisé Postman pour envoyer des requêtes à l'API et j'ai passé beaucoup de temps à diagnostiquer les problèmes de communication entre mon application et Jaeger. Cela a inclus la vérification des configurations réseau, des pare-feu, et l'ajustement de l'adresse de l'agent Jaeger (host.docker.internal puis l'IP de la passerelle Docker).

💡 Compétences acquises et Apprentissages clés
Grâce à ce travail, j'ai renforcé mes compétences en :

Développement d'API avec ASP.NET Core.

Intégration de systèmes de journalisation et de traçage.

Utilisation de Docker pour la conteneurisation d'applications.

Dépannage réseau et diagnostic de problèmes complexes dans un environnement Windows/WSL2/Docker.

⚠️ Point sur le Traçage Jaeger : Défi Rencontré
Malgré mes efforts et la validation de chaque composant (l'API fonctionne, Docker Desktop est "running", Jaeger est lancé et reçoit ses propres traces internes), les traces de mon service CRMCustomerBehaviorService n'apparaissent pas encore dans l'interface de Jaeger.

Cela indique un problème de communication très spécifique et de bas niveau entre mon application .NET et le conteneur Jaeger, qui a été difficile à résoudre à distance et pourrait nécessiter un diagnostic direct de la configuration réseau de mon système.

➡️ Prochaines Étapes
Résoudre le problème de communication réseau pour que les traces de CRMCustomerBehaviorService soient visibles dans Jaeger.

Ajouter des traces personnalisées dans le code de l'API.

Explorer d'autres aspects de l'observabilité.







