---
title: "Entity Framework Core"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 18
---


# Introduction à Entity Framework Core
**Cours pour débutants - Développement d'applications pour entreprises**

---

## 🎯 Objectifs du cours

À la fin de ce cours, vous serez capable de :
- Comprendre ce qu'est Entity Framework **Core** et pourquoi on l'utilise
- Créer une base de données à partir de classes C#
- Effectuer des opérations CRUD (Create, Read, Update, Delete)
- Utiliser les migrations pour gérer le schéma de base de données
- Comprendre la différence entre Code First et Database First
- Choisir entre SQLite et SQL Server selon vos besoins

---

## 📌 Partie 1 : C'est quoi Entity Framework Core ? (15 min)

### Le problème : Accéder à une base de données en C#

Imaginez que vous voulez créer une application pour gérer des étudiants. Vous avez besoin de :
- Sauvegarder les étudiants dans une base de données
- Récupérer la liste des étudiants
- Modifier les informations d'un étudiant
- Supprimer un étudiant

**Sans Entity Framework**, vous devriez écrire du SQL directement :

```csharp
// ❌ Code difficile et verbeux
using (var connection = new SqlConnection(connectionString))
{
    connection.Open();
    
    // Pour ajouter un étudiant
    string sql = "INSERT INTO Etudiants (Nom, Prenom, Age) VALUES (@Nom, @Prenom, @Age)";
    using (var command = new SqlCommand(sql, connection))
    {
        command.Parameters.AddWithValue("@Nom", "Tremblay");
        command.Parameters.AddWithValue("@Prenom", "Jean");
        command.Parameters.AddWithValue("@Age", 20);
        command.ExecuteNonQuery();
    }
    
    // Pour récupérer tous les étudiants
    sql = "SELECT * FROM Etudiants";
    using (var command = new SqlCommand(sql, connection))
    {
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var etudiant = new Etudiant
                {
                    Id = (int)reader["Id"],
                    Nom = (string)reader["Nom"],
                    Prenom = (string)reader["Prenom"],
                    Age = (int)reader["Age"]
                };
                // Faire quelque chose avec l'étudiant...
            }
        }
    }
}
```

**Problèmes :**
1. ❌ Beaucoup de code répétitif
2. ❌ SQL mélangé avec C# (difficile à maintenir)
3. ❌ Risque d'erreurs (typos dans les noms de colonnes)
4. ❌ Difficile à tester
5. ❌ Pas de vérification à la compilation

---

### La solution : Entity Framework Core

**Entity Framework Core (EF Core)** est un **ORM** (Object-Relational Mapper).

**ORM = Pont entre le monde des objets (C#) et le monde relationnel (Base de données)**

```
Classes C#  ←→  Entity Framework  ←→  Tables SQL
Propriétés  ←→                   ←→  Colonnes
Objets      ←→                   ←→  Lignes
```

**Avec Entity Framework**, le même code devient :

```csharp
// ✅ Code simple et lisible
using (var context = new EcoleDbContext())
{
    // Ajouter un étudiant
    var etudiant = new Etudiant 
    { 
        Nom = "Tremblay", 
        Prenom = "Jean", 
        Age = 20 
    };
    context.Etudiants.Add(etudiant);
    context.SaveChanges();
    
    // Récupérer tous les étudiants
    var etudiants = context.Etudiants.ToList();
}
```

**Avantages :**
1. ✅ Code plus court et plus clair
2. ✅ Pas besoin d'écrire du SQL
3. ✅ Vérification à la compilation (IntelliSense)
4. ✅ Moins d'erreurs
5. ✅ Facile à tester

---

### Vocabulaire de base

| Terme | Définition | Exemple |
|-------|------------|---------|
| **Entité** | Une classe C# qui représente une table | `class Etudiant` |
| **DbContext** | La classe qui gère la connexion à la base de données | `class EcoleDbContext : DbContext` |
| **DbSet** | Une collection qui représente une table | `DbSet<Etudiant> Etudiants` |
| **Migration** | Un fichier qui décrit les changements au schéma | `Add-Migration AjouterEtudiants` |
| **LINQ** | Langage de requêtes intégré à C# | `context.Etudiants.Where(e => e.Age > 18)` |

---

## 💻 Partie 2 : Première application avec EF Core (30 min)

### Étape 1 : Installer les packages NuGet

Dans Visual Studio :
1. Clic droit sur le projet → **Manage NuGet Packages**
2. Onglet **Browse**
3. Installer ces packages selon votre choix de base de données :

**Packages de base (obligatoires) :**

```powershell
# Entity Framework Core
Install-Package Microsoft.EntityFrameworkCore

# Outils pour les migrations
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.EntityFrameworkCore.Design
```

**Choisir votre provider de base de données :**

**Option 1 : SQLite (recommandé pour apprendre)**

```powershell
Install-Package Microsoft.EntityFrameworkCore.Sqlite
```

**Pourquoi SQLite ?**
- 💾 Base de données dans un fichier (pas de serveur à installer)
- 🚀 Parfait pour apprendre et développer
- 📦 Facile à partager
- ✅ Aucune configuration requise

**Option 2 : SQL Server (pour production)**

```powershell
Install-Package Microsoft.EntityFrameworkCore.SqlServer
```

**Pourquoi SQL Server ?**
- 🏢 Standard en entreprise
- 💪 Performance supérieure
- 🔒 Sécurité avancée
- 📊 Outils de gestion (SSMS)

**Résumé des packages :**

| Package | SQLite | SQL Server |
|---------|--------|------------|
| `Microsoft.EntityFrameworkCore` | ✅ Obligatoire | ✅ Obligatoire |
| `Microsoft.EntityFrameworkCore.Tools` | ✅ Obligatoire | ✅ Obligatoire |
| `Microsoft.EntityFrameworkCore.Design` | ✅ Obligatoire | ✅ Obligatoire |
| `Microsoft.EntityFrameworkCore.Sqlite` | ✅ | ❌ |
| `Microsoft.EntityFrameworkCore.SqlServer` | ❌ | ✅ |

**💡 Conseil :** Commencez avec SQLite pour apprendre, puis passez à SQL Server pour vos projets d'entreprise.

---

### Étape 2 : Créer votre première entité (classe modèle)

Une **entité** est simplement une classe C# qui représente une table.

```csharp
// Models/Etudiant.cs
namespace MonEcole.Models
{
    public class Etudiant
    {
        // ✅ EF détecte automatiquement "Id" comme clé primaire
        public int Id { get; set; }
        
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
}
```

**Règles importantes :**
- Une propriété nommée `Id` ou `NomClasseId` devient automatiquement la **clé primaire**
- EF détecte automatiquement le type de données SQL selon le type C#
- Les propriétés publiques deviennent des colonnes

**Correspondance des types :**

| Type C# | Type SQL |
|---------|----------|
| `int` | INTEGER |
| `string` | TEXT ou VARCHAR |
| `bool` | BIT |
| `DateTime` | DATETIME |
| `decimal` | DECIMAL |
| `double` | FLOAT |

---

### 💡 Qu'est-ce que SQL Server LocalDB ?

**SQL Server LocalDB** est une version **légère** de SQL Server spécialement conçue pour les développeurs.

**Caractéristiques :**
- 📦 Installé automatiquement avec Visual Studio
- 🚀 Démarre à la demande (pas de service Windows permanent)
- 💾 Stockage dans des fichiers MDF (comme SQLite utilise .db)
- 🔧 Idéal pour développement et apprentissage
- ⚡ Fonctionne comme SQL Server complet mais en plus léger

**Différences avec SQL Server complet :**

| Aspect | LocalDB | SQL Server Express/Full |
|--------|---------|------------------------|
| Installation | Avec Visual Studio | Installation séparée |
| Service Windows | Non (à la demande) | Oui (toujours actif) |
| Connexions réseau | Non (local uniquement) | Oui |
| Gestion | Minimal | SSMS complet |
| Taille | ~50 MB | ~500 MB+ |
| Production | ❌ Non | ✅ Oui |

**Vérifier si LocalDB est installé :**

Ouvrir **PowerShell** ou **CMD** :

```powershell
sqllocaldb info
```

**Résultat attendu :**
```
MSSQLLocalDB
```

**Si LocalDB n'est pas installé :**
1. Télécharger **SQL Server Express** : https://www.microsoft.com/sql-server/sql-server-downloads
2. Choisir **Download now** sous "Express"
3. Pendant l'installation, sélectionner **Download Media** → **LocalDB**

---

### Étape 3 : Créer le DbContext

Le **DbContext** est la classe la plus importante. C'est votre **porte d'entrée** vers la base de données.

**Version SQLite (fichier local) :**

```csharp
// Data/EcoleDbContext.cs
using Microsoft.EntityFrameworkCore;
using MonEcole.Models;

namespace MonEcole.Data
{
    public class EcoleDbContext : DbContext
    {
        // ✅ DbSet = Une table dans la base de données
        public DbSet<Etudiant> Etudiants { get; set; }
        
        // ✅ Configuration de la connexion SQLite
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Créer une base de données SQLite dans un fichier "ecole.db"
            options.UseSqlite("Data Source=ecole.db");
        }
    }
}
```

**Version SQL Server LocalDB :**

```csharp
// Data/EcoleDbContext.cs
using Microsoft.EntityFrameworkCore;
using MonEcole.Models;

namespace MonEcole.Data
{
    public class EcoleDbContext : DbContext
    {
        // ✅ DbSet = Une table dans la base de données
        public DbSet<Etudiant> Etudiants { get; set; }
        
        // ✅ Configuration de la connexion SQL Server LocalDB
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // SQL Server LocalDB (installé avec Visual Studio)
            options.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;
                  Database=EcoleDb;
                  Trusted_Connection=True;
                  MultipleActiveResultSets=true;"
            );
        }
    }
}
```

**Explications de la chaîne de connexion SQL Server LocalDB :**

| Élément | Valeur | Description |
|---------|--------|-------------|
| `Server=` | `(localdb)\mssqllocaldb` | Instance LocalDB par défaut |
| `Database=` | `EcoleDb` | Nom de la base de données à créer |
| `Trusted_Connection=` | `True` | Authentification Windows (pas de mot de passe) |
| `MultipleActiveResultSets=` | `true` | Permet plusieurs requêtes simultanées |

**💡 Variantes de chaînes de connexion SQL Server :**

```csharp
// LocalDB (recommandé pour apprendre)
"Server=(localdb)\\mssqllocaldb;Database=MaBase;Trusted_Connection=True;"

// SQL Express (instance nommée)
"Server=.\\SQLEXPRESS;Database=MaBase;Trusted_Connection=True;"

// SQL Server complet (serveur distant)
"Server=192.168.1.100;Database=MaBase;User Id=user;Password=pass;"

// Azure SQL Database
"Server=tcp:monserveur.database.windows.net,1433;Database=MaBase;User Id=user;Password=pass;"
```

**Explications :**

| Élément | SQLite | SQL Server LocalDB |
|---------|--------|-------------------|
| **Méthode** | `UseSqlite()` | `UseSqlServer()` |
| **Fichier** | `Data Source=ecole.db` | N/A (fichier MDF géré automatiquement) |
| **Serveur** | N/A | `Server=(localdb)\mssqllocaldb` |
| **Base de données** | Fichier `.db` | `Database=EcoleDb` |
| **Authentification** | N/A | `Trusted_Connection=True` |

**💡 Astuce :** Pour basculer entre SQLite et SQL Server LocalDB, il suffit de changer la méthode `OnConfiguring()` !

---

### ⚡ Meilleure approche : Configuration flexible (Injection de dépendances)

**Problème avec OnConfiguring() :**
- ❌ Chaîne de connexion hardcodée dans le code
- ❌ Nécessite de modifier le code pour changer de BD
- ❌ Pas professionnel

**Solution : Utiliser DbContextOptions (recommandé en entreprise)**

#### Étape 1 : Modifier le DbContext

```csharp
// Data/EcoleDbContext.cs
using Microsoft.EntityFrameworkCore;
using MonEcole.Models;

namespace MonEcole.Data
{
    public class EcoleDbContext : DbContext
    {
        // ✅ Constructeur qui accepte les options
        public EcoleDbContext(DbContextOptions<EcoleDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Etudiant> Etudiants { get; set; }
        
        // ❌ Plus besoin de OnConfiguring() !
    }
}
```

#### Étape 2 : Configuration dans Program.cs (Console) ou App.xaml.cs (WPF)

**Pour une application Console :**

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MonEcole.Data;
using MonEcole.Models;

class Program
{
    static void Main(string[] args)
    {
        // Créer le conteneur de services
        var services = new ServiceCollection();
        
        // ═══════════════════════════════════════════════════
        // OPTION 1 : SQLite (décommenter celle-ci)
        // ═══════════════════════════════════════════════════
        services.AddDbContext<EcoleDbContext>(options =>
            options.UseSqlite("Data Source=ecole.db"));
        
        // ═══════════════════════════════════════════════════
        // OPTION 2 : SQL Server LocalDB (ou celle-ci)
        // ═══════════════════════════════════════════════════
        // services.AddDbContext<EcoleDbContext>(options =>
        //     options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EcoleDb;Trusted_Connection=True;"));
        
        var serviceProvider = services.BuildServiceProvider();
        
        // Utiliser le DbContext
        using (var context = serviceProvider.GetRequiredService<EcoleDbContext>())
        {
            // CRUD ici
            context.Etudiants.Add(new Etudiant { Nom = "Test", Prenom = "Jean", Age = 20 });
            context.SaveChanges();
        }
    }
}
```

**Pour une application WPF :**

```csharp
// App.xaml.cs
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Windows;

public partial class App : Application
{
    private readonly IHost _host;
    
    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // ═══════════════════════════════════════════════════
                // Configuration de la base de données (choisir UNE option)
                // ═══════════════════════════════════════════════════
                
                // OPTION 1 : SQLite
                services.AddDbContext<EcoleDbContext>(options =>
                    options.UseSqlite("Data Source=ecole.db"));
                
                // OU OPTION 2 : SQL Server LocalDB
                // services.AddDbContext<EcoleDbContext>(options =>
                //     options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EcoleDb;Trusted_Connection=True;"));
                
                // Autres services...
                services.AddTransient<MainWindow>();
            })
            .Build();
    }
    
    // ... reste du code
}
```

**Packages requis pour cette approche :**

```powershell
Install-Package Microsoft.Extensions.DependencyInjection
Install-Package Microsoft.Extensions.Hosting  # Pour WPF
```

#### Avantages de cette approche ✅

| Aspect | OnConfiguring() | DbContextOptions |
|--------|----------------|------------------|
| **Configuration** | Hardcodée dans le code | Centralisée (App.xaml.cs ou Program.cs) |
| **Changement de BD** | Modifier le DbContext | Changer UNE ligne |
| **Testabilité** | Difficile | Facile (mock le DbContext) |
| **Production** | ❌ Non recommandé | ✅ Standard |
| **Configuration externe** | ❌ Impossible | ✅ Possible (appsettings.json) |

#### Encore mieux : Utiliser appsettings.json

**Créer appsettings.json :**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=ecole.db"
  },
  "DatabaseProvider": "SQLite"
}
```

**Lire la configuration :**

```csharp
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");
var provider = configuration["DatabaseProvider"];

// Configuration selon le provider
if (provider == "SQLite")
{
    services.AddDbContext<EcoleDbContext>(options =>
        options.UseSqlite(connectionString));
}
else if (provider == "SqlServer")
{
    services.AddDbContext<EcoleDbContext>(options =>
        options.UseSqlServer(connectionString));
}
```

**Avantage :** Pour changer de base de données, il suffit de modifier le fichier JSON !

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EcoleDb;Trusted_Connection=True;"
  },
  "DatabaseProvider": "SqlServer"
}
```

**💡 Résumé des approches :**

| Approche | Difficulté | Flexibilité | Utilisation |
|----------|-----------|-------------|-------------|
| `OnConfiguring()` | ⭐ Facile | ❌ Faible | Apprentissage rapide |
| `DbContextOptions` | ⭐⭐ Moyenne | ✅ Bonne | Projets réels |
| `appsettings.json` | ⭐⭐⭐ Avancée | ✅✅ Excellente | Production |

**Recommandation pour le cours :**
- **Commencer avec** `OnConfiguring()` (simple pour comprendre)
- **Passer à** `DbContextOptions` (approche professionnelle)
- **Bonus** `appsettings.json` (pour les étudiants avancés)

---

### Étape 4 : Créer la base de données avec les migrations

Les **migrations** permettent de créer et modifier le schéma de base de données.

**Dans la Package Manager Console** (Tools → NuGet Package Manager → Package Manager Console) :

```powershell
# 1. Créer la migration initiale
Add-Migration InitialCreate

# 2. Créer la base de données
Update-Database
```

**Ce qui se passe :**

1. `Add-Migration InitialCreate` :
   - EF analyse votre `DbContext` et vos entités
   - Génère un fichier dans `Migrations/` qui contient le code pour créer les tables
   - **Rien n'est encore créé dans la base de données !**

2. `Update-Database` :
   - Exécute le code de migration
   - **Crée réellement la base de données** et les tables

**Résultat :** Vous avez maintenant un fichier `ecole.db` (SQLite) ou une base de données `EcoleDb` (SQL Server) avec une table `Etudiants` !

---

### 💡 Comparaison SQLite vs SQL Server LocalDB

**Après avoir créé votre base de données, voici comment vérifier qu'elle existe :**

#### Avec SQLite :
- Un fichier `ecole.db` apparaît dans le dossier de votre projet (généralement `bin\Debug\net6.0`)
- Vous pouvez l'ouvrir avec **DB Browser for SQLite** (gratuit) : https://sqlitebrowser.org
- Le fichier peut être copié/partagé facilement

#### Avec SQL Server LocalDB :
- **Option 1 : Visual Studio**
  - Menu **View** → **SQL Server Object Explorer**
  - Développer **SQL Server** → **(localdb)\mssqllocaldb**
  - Développer **Databases** → **EcoleDb**
  - Clic droit sur une table → **View Data**

- **Option 2 : SQL Server Management Studio (SSMS)**
  - Télécharger SSMS : https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms
  - Se connecter à : `(localdb)\mssqllocaldb`
  - Développer **Databases** → **EcoleDb**

- **Option 3 : Ligne de commande**
  ```powershell
  # Se connecter à LocalDB
  sqlcmd -S "(localdb)\mssqllocaldb"
  
  # Lister les bases de données
  SELECT name FROM sys.databases;
  GO
  ```

**Tableau comparatif :**

| Aspect | SQLite | SQL Server LocalDB |
|--------|--------|-------------------|
| **Installation** | Aucune | Avec Visual Studio |
| **Fichier** | `ecole.db` (visible dans le projet) | Fichiers MDF/LDF (cachés) |
| **Localisation** | `bin\Debug\net6.0\ecole.db` | `C:\Users\[user]\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB` |
| **Performance** | Limitée (petits projets) | Excellente (comme SQL Server) |
| **Utilisateurs simultanés** | 1 lecteur à la fois | Plusieurs (comme SQL Server) |
| **Outils** | DB Browser for SQLite | Visual Studio, SSMS |
| **Partage** | Copier le fichier .db | Script SQL ou Backup |
| **Production** | ❌ Déconseillé | ⚠️ LocalDB non, SQL Server oui |
| **Apprentissage** | ✅ Très simple | ✅ Simple (si Visual Studio installé) |
| **Type de données** | Limité (5 types) | Complet (dizaines de types) |

**💡 Recommandation :**
- **Pour débuter rapidement** : SQLite (zéro configuration)
- **Pour apprendre SQL Server** : LocalDB (expérience réaliste)
- **Pour projets d'école** : SQLite ou LocalDB selon préférence
- **Pour entreprise** : SQL Server complet (ou PostgreSQL, MySQL)

---

### Étape 5 : Utiliser EF Core - Les opérations CRUD

Maintenant qu'on a notre base de données, voyons comment l'utiliser.

#### **C**reate - Ajouter des données

```csharp
using (var context = new EcoleDbContext())
{
    // Créer un nouvel étudiant
    var etudiant = new Etudiant
    {
        Nom = "Tremblay",
        Prenom = "Jean",
        Age = 20,
        Email = "jean.tremblay@email.com"
    };
    
    // Ajouter à la base de données
    context.Etudiants.Add(etudiant);
    
    // ✅ IMPORTANT : Sauvegarder les changements !
    context.SaveChanges();
    
    Console.WriteLine($"Étudiant ajouté avec l'ID : {etudiant.Id}");
}
```

**Important :** `SaveChanges()` **DOIT** être appelé pour enregistrer les modifications !

#### **R**ead - Lire des données

```csharp
using (var context = new EcoleDbContext())
{
    // 1. Obtenir TOUS les étudiants
    var tousLesEtudiants = context.Etudiants.ToList();
    
    foreach (var e in tousLesEtudiants)
    {
        Console.WriteLine($"{e.Prenom} {e.Nom} - {e.Age} ans");
    }
    
    // 2. Obtenir un étudiant par son ID
    var etudiant = context.Etudiants.Find(1);
    
    // 3. Filtrer avec LINQ
    var etudiantsMajeurs = context.Etudiants
        .Where(e => e.Age >= 18)
        .ToList();
    
    // 4. Chercher par nom
    var jeans = context.Etudiants
        .Where(e => e.Prenom == "Jean")
        .ToList();
    
    // 5. Premier étudiant qui correspond
    var premierJean = context.Etudiants
        .FirstOrDefault(e => e.Prenom == "Jean");
    
    // 6. Compter les étudiants
    int total = context.Etudiants.Count();
    
    // 7. Vérifier si un étudiant existe
    bool existe = context.Etudiants.Any(e => e.Email == "jean@email.com");
}
```

#### **U**pdate - Modifier des données

```csharp
using (var context = new EcoleDbContext())
{
    // 1. Trouver l'étudiant à modifier
    var etudiant = context.Etudiants.Find(1);
    
    if (etudiant != null)
    {
        // 2. Modifier les propriétés
        etudiant.Age = 21;
        etudiant.Email = "nouveau.email@email.com";
        
        // 3. Sauvegarder
        context.SaveChanges();
        
        Console.WriteLine("Étudiant modifié !");
    }
}
```

#### **D**elete - Supprimer des données

```csharp
using (var context = new EcoleDbContext())
{
    // 1. Trouver l'étudiant à supprimer
    var etudiant = context.Etudiants.Find(1);
    
    if (etudiant != null)
    {
        // 2. Supprimer
        context.Etudiants.Remove(etudiant);
        
        // 3. Sauvegarder
        context.SaveChanges();
        
        Console.WriteLine("Étudiant supprimé !");
    }
}
```

---

### Exemple complet dans une application Console

```csharp
// Program.cs
using System;
using System.Linq;
using MonEcole.Data;
using MonEcole.Models;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Gestion des étudiants ===\n");
        
        using (var context = new EcoleDbContext())
        {
            // Ajouter des étudiants
            Console.WriteLine("Ajout d'étudiants...");
            context.Etudiants.Add(new Etudiant 
            { 
                Nom = "Tremblay", 
                Prenom = "Jean", 
                Age = 20, 
                Email = "jean@email.com" 
            });
            context.Etudiants.Add(new Etudiant 
            { 
                Nom = "Gagnon", 
                Prenom = "Marie", 
                Age = 19, 
                Email = "marie@email.com" 
            });
            context.Etudiants.Add(new Etudiant 
            { 
                Nom = "Roy", 
                Prenom = "Pierre", 
                Age = 22, 
                Email = "pierre@email.com" 
            });
            context.SaveChanges();
            Console.WriteLine("✅ Étudiants ajoutés\n");
            
            // Afficher tous les étudiants
            Console.WriteLine("Liste des étudiants :");
            var etudiants = context.Etudiants.ToList();
            foreach (var e in etudiants)
            {
                Console.WriteLine($"  - {e.Prenom} {e.Nom}, {e.Age} ans ({e.Email})");
            }
            
            // Filtrer les étudiants majeurs
            Console.WriteLine("\nÉtudiants de 20 ans et plus :");
            var majeurs = context.Etudiants
                .Where(e => e.Age >= 20)
                .ToList();
            foreach (var e in majeurs)
            {
                Console.WriteLine($"  - {e.Prenom} {e.Nom}");
            }
            
            // Modifier un étudiant
            Console.WriteLine("\nModification de Jean...");
            var jean = context.Etudiants.FirstOrDefault(e => e.Prenom == "Jean");
            if (jean != null)
            {
                jean.Age = 21;
                context.SaveChanges();
                Console.WriteLine($"✅ Jean a maintenant {jean.Age} ans");
            }
            
            // Supprimer un étudiant
            Console.WriteLine("\nSuppression de Pierre...");
            var pierre = context.Etudiants.FirstOrDefault(e => e.Prenom == "Pierre");
            if (pierre != null)
            {
                context.Etudiants.Remove(pierre);
                context.SaveChanges();
                Console.WriteLine("✅ Pierre supprimé");
            }
            
            // Afficher le total
            int total = context.Etudiants.Count();
            Console.WriteLine($"\nNombre total d'étudiants : {total}");
        }
        
        Console.WriteLine("\nAppuyez sur une touche pour quitter...");
        Console.ReadKey();
    }
}
```

---

## 🔧 Partie 3 : Fonctionnalités avancées (20 min)

### Annotations de données (Data Annotations)

Vous pouvez ajouter des contraintes à vos entités avec des **attributs** :

```csharp
using System.ComponentModel.DataAnnotations;

public class Etudiant
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Le nom est obligatoire")]
    [MaxLength(50)]
    public string Nom { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Prenom { get; set; }
    
    [Range(16, 99, ErrorMessage = "L'âge doit être entre 16 et 99")]
    public int Age { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    [Phone]
    public string Telephone { get; set; }
}
```

**Annotations courantes :**

| Attribut | Description | Exemple |
|----------|-------------|---------|
| `[Required]` | Champ obligatoire (NOT NULL) | `[Required]` |
| `[MaxLength(n)]` | Longueur maximale | `[MaxLength(100)]` |
| `[MinLength(n)]` | Longueur minimale | `[MinLength(3)]` |
| `[Range(min, max)]` | Valeur entre min et max | `[Range(0, 100)]` |
| `[EmailAddress]` | Format email valide | `[EmailAddress]` |
| `[Phone]` | Format téléphone | `[Phone]` |
| `[Key]` | Clé primaire (si pas nommée "Id") | `[Key]` |

---

### Relations entre tables

#### Relation 1-à-plusieurs (One-to-Many)

**Exemple :** Un étudiant peut avoir plusieurs notes

```csharp
// Un étudiant
public class Etudiant
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    
    // ✅ Navigation property : Collection de notes
    public List<Note> Notes { get; set; }
}

// Plusieurs notes
public class Note
{
    public int Id { get; set; }
    public string Matiere { get; set; }
    public int Valeur { get; set; }
    
    // ✅ Clé étrangère
    public int EtudiantId { get; set; }
    
    // ✅ Navigation property : L'étudiant associé
    public Etudiant Etudiant { get; set; }
}
```

**Dans le DbContext :**

```csharp
public class EcoleDbContext : DbContext
{
    public DbSet<Etudiant> Etudiants { get; set; }
    public DbSet<Note> Notes { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=ecole.db");
    }
}
```

**Utilisation :**

```csharp
using (var context = new EcoleDbContext())
{
    // Créer un étudiant avec ses notes
    var etudiant = new Etudiant
    {
        Nom = "Tremblay",
        Prenom = "Jean",
        Notes = new List<Note>
        {
            new Note { Matiere = "Mathématiques", Valeur = 85 },
            new Note { Matiere = "Français", Valeur = 78 },
            new Note { Matiere = "Anglais", Valeur = 92 }
        }
    };
    
    context.Etudiants.Add(etudiant);
    context.SaveChanges();
    
    // Récupérer un étudiant avec ses notes
    var jean = context.Etudiants
        .Include(e => e.Notes)  // ✅ IMPORTANT : Include pour charger les notes
        .FirstOrDefault(e => e.Prenom == "Jean");
    
    Console.WriteLine($"{jean.Prenom} a {jean.Notes.Count} notes :");
    foreach (var note in jean.Notes)
    {
        Console.WriteLine($"  - {note.Matiere}: {note.Valeur}%");
    }
}
```

**⚠️ Important :** Sans `.Include()`, les notes ne seront PAS chargées !

---

### Données initiales (Seed Data)

Vous pouvez ajouter des données par défaut lors de la création de la base de données :

```csharp
public class EcoleDbContext : DbContext
{
    public DbSet<Etudiant> Etudiants { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=ecole.db");
    }
    
    // ✅ Ajouter des données initiales
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Etudiant>().HasData(
            new Etudiant 
            { 
                Id = 1, 
                Nom = "Tremblay", 
                Prenom = "Jean", 
                Age = 20, 
                Email = "jean@email.com" 
            },
            new Etudiant 
            { 
                Id = 2, 
                Nom = "Gagnon", 
                Prenom = "Marie", 
                Age = 19, 
                Email = "marie@email.com" 
            }
        );
    }
}
```

**Après avoir ajouté le seed data :**

```powershell
Add-Migration AjouterDonneesInitiales
Update-Database
```

Les données seront automatiquement insérées !

---

## 🗄️ Partie 4 : Les Migrations en détail (15 min)

### Qu'est-ce qu'une migration ?

Une **migration** est comme un **historique Git pour votre base de données**.

- Chaque migration décrit un **changement** au schéma
- On peut **avancer** (appliquer) ou **reculer** (annuler)
- On peut voir l'historique complet des changements

### Cycle de travail avec les migrations

```
1. Modifier le modèle C#
    ↓
2. Créer une migration (Add-Migration)
    ↓
3. Appliquer la migration (Update-Database)
    ↓
4. La base de données est à jour !
```

### Commandes essentielles

```powershell
# Créer une nouvelle migration
Add-Migration NomDeLaMigration

# Appliquer toutes les migrations en attente
Update-Database

# Voir la liste des migrations
Get-Migration

# Annuler la dernière migration (revenir en arrière)
Update-Database -Migration NomMigrationPrecedente

# Supprimer la dernière migration (si pas encore appliquée)
Remove-Migration

# Générer le script SQL sans l'exécuter
Script-Migration

# Revenir à la base de données vide
Update-Database -Migration 0
```

**⚠️ Note importante :**

`Update-Database` ne prend **PAS** le nom du DbContext en paramètre. EF Core détecte automatiquement votre DbContext.

```powershell
# ✅ CORRECT
Update-Database

# ❌ INCORRECT
Update-Database EcoleDbContext
```

**Cas particulier : Plusieurs DbContext dans le projet**

Si vous avez plusieurs DbContext, utilisez le paramètre `-Context` :

```powershell
# Spécifier quel DbContext utiliser
Update-Database -Context EcoleDbContext

# Créer une migration pour un DbContext spécifique
Add-Migration InitialCreate -Context EcoleDbContext
```

**Cas particulier : Architecture multi-projets**

Si votre DbContext est dans un projet séparé (ex: MonApp.Data) :

```powershell
# Spécifier les projets
Update-Database -Context EcoleDbContext -Project MonApp.Data -StartupProject MonApp.Web

# OU dans Package Manager Console, sélectionner "Default project" en haut
```

### Exemple pratique de modification

**Scenario :** On veut ajouter un champ `DateNaissance` à la place de `Age`

```csharp
// AVANT
public class Etudiant
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public int Age { get; set; }  // On veut remplacer ça
}

// APRÈS
public class Etudiant
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public DateTime DateNaissance { get; set; }  // ✅ Nouveau champ
}
```

**Créer la migration :**

```powershell
Add-Migration RemplacerAgeParDateNaissance
Update-Database
```

**EF génère automatiquement le code pour :**
- Supprimer la colonne `Age`
- Ajouter la colonne `DateNaissance`

---

### Structure d'une migration

```csharp
public partial class RemplacerAgeParDateNaissance : Migration
{
    // ✅ Code pour APPLIQUER la migration
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Age",
            table: "Etudiants");

        migrationBuilder.AddColumn<DateTime>(
            name: "DateNaissance",
            table: "Etudiants",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }

    // ✅ Code pour ANNULER la migration
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "DateNaissance",
            table: "Etudiants");

        migrationBuilder.AddColumn<int>(
            name: "Age",
            table: "Etudiants",
            nullable: false,
            defaultValue: 0);
    }
}
```

---

## 🆚 Partie 5 : Code First vs Database First (10 min)

Il existe **deux approches** pour travailler avec EF Core :

### Code First (ce qu'on a fait jusqu'ici)

**Principe :** On écrit les classes C# **PUIS** EF crée la base de données.

```
Classes C#  →  Migrations  →  Base de données
```

**Avantages :**
- ✅ Contrôle total sur le code
- ✅ Facile à versionner (Git)
- ✅ Les migrations gardent l'historique
- ✅ Pratique pour les nouveaux projets

**Quand l'utiliser :**
- Nouveau projet
- Vous êtes responsable du modèle de données
- Travail en équipe avec Git

---

### Database First (l'inverse)

**Principe :** La base de données existe **DÉJÀ**, et EF génère les classes C#.

```
Base de données existante  →  Scaffold  →  Classes C# générées
```

**Commande :**

```powershell
Scaffold-DbContext "Data Source=ecole.db" Microsoft.EntityFrameworkCore.Sqlite -OutputDir Models
```

**Avantages :**
- ✅ Utiliser une base de données existante
- ✅ Génération automatique des classes
- ✅ Pratique si le DBA gère la base

**Quand l'utiliser :**
- Base de données déjà existante
- Projet legacy
- DBA gère le schéma de base de données

---

### Tableau comparatif

| Aspect | Code First | Database First |
|--------|------------|----------------|
| Point de départ | Classes C# | Base de données |
| Contrôle | Développeur | DBA ou existant |
| Modifications | Migrations | Scaffold à nouveau |
| Versionning | Facile (Git) | Plus complexe |
| Nouveau projet | ✅ Recommandé | ❌ |
| Projet existant | ❌ | ✅ Recommandé |

**Recommandation :** Pour vos projets de cégep, utilisez **Code First** !

---

## 📝 Exercices pratiques (20 min)

### Exercice 1 : Bibliothèque (Débutant)

Créez une application pour gérer une bibliothèque.

**Modèle :**

```csharp
public class Livre
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Titre { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Auteur { get; set; }
    
    [Range(1000, 2100)]
    public int AnneePublication { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string ISBN { get; set; }
    
    public bool EstDisponible { get; set; }
}
```

**Tâches :**

1. Créer le `DbContext`
2. Créer la migration et la base de données
3. Ajouter 5 livres
4. Afficher tous les livres disponibles
5. Marquer un livre comme emprunté (EstDisponible = false)
6. Supprimer un livre
7. Compter le nombre de livres par auteur

<details>
<summary>📖 Voir la solution</summary>

### BibliothequeDbContext.cs

**Version SQLite :**

```csharp
using Microsoft.EntityFrameworkCore;

namespace Bibliotheque.Data
{
    public class BibliothequeDbContext : DbContext
    {
        public DbSet<Livre> Livres { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=bibliotheque.db");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Données initiales
            modelBuilder.Entity<Livre>().HasData(
                new Livre 
                { 
                    Id = 1, 
                    Titre = "1984", 
                    Auteur = "George Orwell", 
                    AnneePublication = 1949,
                    ISBN = "978-0451524935",
                    EstDisponible = true
                },
                new Livre 
                { 
                    Id = 2, 
                    Titre = "Le Petit Prince", 
                    Auteur = "Antoine de Saint-Exupéry", 
                    AnneePublication = 1943,
                    ISBN = "978-0156012195",
                    EstDisponible = true
                },
                new Livre 
                { 
                    Id = 3, 
                    Titre = "Harry Potter à l'école des sorciers", 
                    Auteur = "J.K. Rowling", 
                    AnneePublication = 1997,
                    ISBN = "978-0439708180",
                    EstDisponible = true
                },
                new Livre 
                { 
                    Id = 4, 
                    Titre = "Le Seigneur des Anneaux", 
                    Auteur = "J.R.R. Tolkien", 
                    AnneePublication = 1954,
                    ISBN = "978-0618640157",
                    EstDisponible = true
                },
                new Livre 
                { 
                    Id = 5, 
                    Titre = "L'Étranger", 
                    Auteur = "Albert Camus", 
                    AnneePublication = 1942,
                    ISBN = "978-0679720201",
                    EstDisponible = false
                }
            );
        }
    }
}
```

**Version SQL Server :**

```csharp
using Microsoft.EntityFrameworkCore;

namespace Bibliotheque.Data
{
    public class BibliothequeDbContext : DbContext
    {
        public DbSet<Livre> Livres { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // ✅ SQL Server
            options.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;
                  Database=BibliothequeDb;
                  Trusted_Connection=True;"
            );
        }
        
        // ... reste du code identique
    }
}
```

**💡 Note :** Le reste du code (OnModelCreating, données initiales) est **identique** pour SQLite et SQL Server !

### Program.cs

```csharp
using System;
using System.Linq;
using Bibliotheque.Data;
using Bibliotheque.Models;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Gestion de bibliothèque ===\n");
        
        using (var context = new BibliothequeDbContext())
        {
            // 1. Afficher tous les livres disponibles
            Console.WriteLine("📚 Livres disponibles :");
            var livresDisponibles = context.Livres
                .Where(l => l.EstDisponible)
                .ToList();
            
            foreach (var livre in livresDisponibles)
            {
                Console.WriteLine($"  - {livre.Titre} par {livre.Auteur} ({livre.AnneePublication})");
            }
            
            // 2. Emprunter un livre
            Console.WriteLine("\n📖 Emprunt du livre '1984'...");
            var livre1984 = context.Livres
                .FirstOrDefault(l => l.Titre == "1984");
            
            if (livre1984 != null)
            {
                livre1984.EstDisponible = false;
                context.SaveChanges();
                Console.WriteLine("✅ Livre emprunté !");
            }
            
            // 3. Supprimer un livre
            Console.WriteLine("\n🗑️ Suppression de 'Le Petit Prince'...");
            var petitPrince = context.Livres
                .FirstOrDefault(l => l.Titre == "Le Petit Prince");
            
            if (petitPrince != null)
            {
                context.Livres.Remove(petitPrince);
                context.SaveChanges();
                Console.WriteLine("✅ Livre supprimé !");
            }
            
            // 4. Statistiques
            Console.WriteLine("\n📊 Statistiques :");
            int total = context.Livres.Count();
            int disponibles = context.Livres.Count(l => l.EstDisponible);
            int empruntes = total - disponibles;
            
            Console.WriteLine($"  Total de livres : {total}");
            Console.WriteLine($"  Disponibles : {disponibles}");
            Console.WriteLine($"  Empruntés : {empruntes}");
            
            // 5. Livres par auteur
            Console.WriteLine("\n👤 Livres par auteur :");
            var parAuteur = context.Livres
                .GroupBy(l => l.Auteur)
                .Select(g => new { Auteur = g.Key, Nombre = g.Count() })
                .ToList();
            
            foreach (var group in parAuteur)
            {
                Console.WriteLine($"  - {group.Auteur} : {group.Nombre} livre(s)");
            }
        }
        
        Console.WriteLine("\nAppuyez sur une touche pour quitter...");
        Console.ReadKey();
    }
}
```

### Commandes

```powershell
Add-Migration InitialCreate
Update-Database
```

</details>

---

### Exercice 2 : Blog avec articles et commentaires (Intermédiaire)

Créez un système de blog avec des articles et des commentaires.

**Modèles :**

```csharp
public class Article
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Titre { get; set; }
    
    [Required]
    public string Contenu { get; set; }
    
    public DateTime DatePublication { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Auteur { get; set; }
    
    // Relation 1-à-plusieurs
    public List<Commentaire> Commentaires { get; set; }
}

public class Commentaire
{
    public int Id { get; set; }
    
    [Required]
    public string Texte { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Auteur { get; set; }
    
    public DateTime DateCreation { get; set; }
    
    // Clé étrangère
    public int ArticleId { get; set; }
    public Article Article { get; set; }
}
```

**Tâches :**

1. Créer le `DbContext` avec les deux `DbSet`
2. Créer les migrations
3. Ajouter 3 articles avec 2-3 commentaires chacun
4. Afficher tous les articles avec le nombre de commentaires
5. Afficher les commentaires d'un article spécifique
6. Supprimer tous les commentaires d'un article
7. Trouver les 3 articles les plus récents

<details>
<summary>📖 Voir la solution</summary>

### BlogDbContext.cs

**Choisir votre provider :**

```csharp
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public class BlogDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Commentaire> Commentaires { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // OPTION 1 : SQLite
            options.UseSqlite("Data Source=blog.db");
            
            // OU OPTION 2 : SQL Server (décommenter cette ligne et commenter celle du dessus)
            // options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=BlogDb;Trusted_Connection=True;");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration de la relation
            modelBuilder.Entity<Commentaire>()
                .HasOne(c => c.Article)
                .WithMany(a => a.Commentaires)
                .HasForeignKey(c => c.ArticleId)
                .OnDelete(DeleteBehavior.Cascade); // Supprimer les commentaires si l'article est supprimé
            
            // Données initiales
            modelBuilder.Entity<Article>().HasData(
                new Article 
                { 
                    Id = 1, 
                    Titre = "Introduction à C#", 
                    Contenu = "C# est un langage de programmation...",
                    DatePublication = DateTime.Now.AddDays(-10),
                    Auteur = "Marie Tremblay"
                },
                new Article 
                { 
                    Id = 2, 
                    Titre = "Entity Framework Core pour débutants", 
                    Contenu = "EF Core est un ORM qui facilite...",
                    DatePublication = DateTime.Now.AddDays(-5),
                    Auteur = "Jean Gagnon"
                },
                new Article 
                { 
                    Id = 3, 
                    Titre = "MVVM et WPF", 
                    Contenu = "Le pattern MVVM permet de...",
                    DatePublication = DateTime.Now.AddDays(-1),
                    Auteur = "Pierre Roy"
                }
            );
            
            modelBuilder.Entity<Commentaire>().HasData(
                // Commentaires pour article 1
                new Commentaire 
                { 
                    Id = 1, 
                    ArticleId = 1, 
                    Texte = "Excellent article !",
                    Auteur = "Alice",
                    DateCreation = DateTime.Now.AddDays(-9)
                },
                new Commentaire 
                { 
                    Id = 2, 
                    ArticleId = 1, 
                    Texte = "Très clair, merci !",
                    Auteur = "Bob",
                    DateCreation = DateTime.Now.AddDays(-8)
                },
                // Commentaires pour article 2
                new Commentaire 
                { 
                    Id = 3, 
                    ArticleId = 2, 
                    Texte = "J'ai enfin compris EF Core !",
                    Auteur = "Charlie",
                    DateCreation = DateTime.Now.AddDays(-4)
                },
                new Commentaire 
                { 
                    Id = 4, 
                    ArticleId = 2, 
                    Texte = "Bon tutoriel",
                    Auteur = "David",
                    DateCreation = DateTime.Now.AddDays(-3)
                },
                new Commentaire 
                { 
                    Id = 5, 
                    ArticleId = 2, 
                    Texte = "Super utile",
                    Auteur = "Eve",
                    DateCreation = DateTime.Now.AddDays(-2)
                }
            );
        }
    }
}
```

**💡 Astuce :** Pour changer de base de données, il suffit de changer/décommenter une ligne dans `OnConfiguring()` et de recréer les migrations !

### Program.cs

```csharp
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Models;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Blog ===\n");
        
        using (var context = new BlogDbContext())
        {
            // 1. Afficher tous les articles avec le nombre de commentaires
            Console.WriteLine("📝 Articles :");
            var articles = context.Articles
                .Include(a => a.Commentaires)
                .ToList();
            
            foreach (var article in articles)
            {
                Console.WriteLine($"\n  {article.Titre}");
                Console.WriteLine($"  Par {article.Auteur} le {article.DatePublication:dd/MM/yyyy}");
                Console.WriteLine($"  💬 {article.Commentaires.Count} commentaire(s)");
            }
            
            // 2. Afficher les commentaires d'un article
            Console.WriteLine("\n\n💬 Commentaires de 'Entity Framework pour débutants' :");
            var articleEF = context.Articles
                .Include(a => a.Commentaires)
                .FirstOrDefault(a => a.Titre.Contains("Entity Framework"));
            
            if (articleEF != null)
            {
                foreach (var comm in articleEF.Commentaires)
                {
                    Console.WriteLine($"  - {comm.Auteur} : {comm.Texte}");
                }
            }
            
            // 3. Les 3 articles les plus récents
            Console.WriteLine("\n\n🆕 Les 3 articles les plus récents :");
            var recents = context.Articles
                .OrderByDescending(a => a.DatePublication)
                .Take(3)
                .ToList();
            
            foreach (var article in recents)
            {
                Console.WriteLine($"  - {article.Titre} ({article.DatePublication:dd/MM/yyyy})");
            }
            
            // 4. Statistiques
            Console.WriteLine("\n\n📊 Statistiques :");
            int totalArticles = context.Articles.Count();
            int totalCommentaires = context.Commentaires.Count();
            double moyenneCommentaires = context.Articles
                .Include(a => a.Commentaires)
                .Average(a => a.Commentaires.Count);
            
            Console.WriteLine($"  Total d'articles : {totalArticles}");
            Console.WriteLine($"  Total de commentaires : {totalCommentaires}");
            Console.WriteLine($"  Moyenne de commentaires par article : {moyenneCommentaires:F1}");
        }
        
        Console.WriteLine("\n\nAppuyez sur une touche pour quitter...");
        Console.ReadKey();
    }
}
```

</details>

---

## 🎓 Résumé

### Ce qu'on a appris

✅ **Entity Framework Core** : Un ORM qui fait le pont entre C# et les bases de données

✅ **DbContext** : La classe qui gère la connexion et les opérations

✅ **DbSet** : Représente une table dans la base de données

✅ **Entités** : Classes C# qui deviennent des tables

✅ **Migrations** : Système de versioning pour le schéma de base de données

✅ **CRUD** : Create, Read, Update, Delete avec EF Core

✅ **Relations** : Lier des tables entre elles (1-à-plusieurs, etc.)

✅ **LINQ** : Requêtes en C# au lieu de SQL

---

### Commandes à retenir

```powershell
# Créer une migration
Add-Migration NomDeLaMigration

# Appliquer les migrations (EF détecte automatiquement le DbContext)
Update-Database

# Si vous avez plusieurs DbContext, spécifier lequel
Update-Database -Context EcoleDbContext

# Voir les migrations
Get-Migration

# Annuler une migration
Remove-Migration

# Revenir en arrière
Update-Database -Migration NomMigrationPrecedente
```

**💡 Astuce :** `Update-Database` ne prend **PAS** le nom du DbContext directement. EF Core le détecte automatiquement !

---

### Code minimal pour démarrer

```csharp
// 1. Modèle
public class MaClasse
{
    public int Id { get; set; }
    public string Nom { get; set; }
}

// 2. DbContext
public class MonDbContext : DbContext
{
    public DbSet<MaClasse> MesObjets { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // SQLite
        options.UseSqlite("Data Source=mabase.db");
        
        // OU SQL Server
        // options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MaBase;Trusted_Connection=True;");
    }
}

// 3. Utilisation
using (var context = new MonDbContext())
{
    // CRUD ici
    context.MesObjets.Add(new MaClasse { Nom = "Test" });
    context.SaveChanges();
}
```

**⚠️ Important :** Ce cours utilise **Entity Framework Core** (EF Core), la version moderne et cross-platform. Ne confondez pas avec Entity Framework 6 (ancien, Windows uniquement).

---

## 📚 Pour aller plus loin

- **Async/Await** : Rendre les opérations asynchrones (`ToListAsync()`, `SaveChangesAsync()`)
- **Requêtes complexes** : Jointures, groupements avancés
- **Lazy Loading** : Chargement automatique des relations
- **Transactions** : Gérer plusieurs opérations en une seule transaction
- **Performance** : Optimiser les requêtes EF Core

---

**Fin du cours ! 🎉**

Vous savez maintenant utiliser Entity Framework Core pour créer et gérer vos bases de données !