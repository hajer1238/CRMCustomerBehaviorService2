# Projet CRM - Analyse du Comportement Client

Ce dépôt contient le service d'analyse du comportement client développé dans le cadre de mon stage.

---

## Objectif Général du Projet
L'objectif de ce service est de collecter et d'analyser les événements clés liés au comportement des clients (navigation, ajouts au panier, achats, etc.) pour mieux comprendre leurs besoins et anticiper leurs actions.

---

## Avancement du Projet (Plan Semaine par Semaine)

### **Semaine 1 : Mise en Place et Journalisation (Terminé ✅)**

**Objectifs de la Semaine 1 :**
* Comprendre le flux du CRM choisi (collecte des événements clients).
* Intégrer `ILogger` pour la journalisation.
* Identifier les points de log pertinents.

**Réalisations :**
1.  **Initialisation du Projet .NET 8 Web API :** Création de la structure de base du projet.
2.  **Modèle de Données `CustomerEvent` :** Définition de la structure des événements clients (ID, CustomerId, EventType, Timestamp).
3.  **Contrôleur `CustomerEventsController` :** Implémentation d'un endpoint `POST /CustomerEvents` pour recevoir et traiter les événements.
4.  **Intégration de Serilog :** Configuration d'une solution de journalisation avancée pour capturer les informations détaillées des événements reçus.
5.  **Test Fonctionnel réussi :** Validation de l'API via Postman. La réception des données et l'affichage des logs dans la console ont été confirmés.

