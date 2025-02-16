# README - Site de Vente de Vin

## Description
Ce projet est un site web de vente de vin développé en C# avec Razor. Il permet aux utilisateurs de parcourir un catalogue de vins, d'ajouter des produits à leur panier, de passer des commandes et de gérer leur compte utilisateur.

## Fonctionnalités
- **Gestion des utilisateurs** :
  - Création de compte
  - Connexion / Déconnexion
  - Modification du profil
  - Suppression de compte
- **Gestion des produits** :
  - Consultation du catalogue de vins
  - Ajout de produits au panier
  - Suppression de produits du panier
  - Validation de la commande
- **Historique des commandes** :
  - Consultation des commandes passées
  - Détails des commandes

## Technologies utilisées
- **Langage** : C#
- **Framework** : ASP.NET Core avec Razor Pages
- **Base de données** : SQL Server
- **Authentification** : Identity ASP.NET
- **Gestion des sessions** : Cookie-based authentication

## Installation et Exécution
### Prérequis
- .NET 7.0 ou version ultérieure
- SQL Server

### Étapes d'installation
1. **Cloner le dépôt**
   ```sh
   git clone https://github.com/Les-vignes-nobles/Vignoble-WEB.git
   cd Vignoble-WEB
   ```
2. **Configurer la base de données**
   - Modifier le fichier `appsettings.json` avec vos informations de connexion SQL Server
   ```json
   "ConnectionStrings": {
    "UrlAPIConnection": "http://URLAPI/api",
    "APIUsername" : "User1",
      "APIPassword" : "Password1"
  },
   ```
   - Appliquer les migrations :
   ```sh
   dotnet ef database update
   ```
3. **Lancer l'application**
   ```sh
   dotnet run
   ```
4. Accéder à l'application via [http://localhost:5000](http://localhost:5000)
