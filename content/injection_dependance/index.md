---
title: "Patron Singleton et injection de dépendance"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 20
---

## 1. Le patron Singleton
 
### Définition
 
Le **patron Singleton** garantit qu'une classe ne possède qu'**une seule instance** dans toute l'application, avec un point d'accès global à cette instance.
 
> Peu importe combien de fois vous demandez l'objet, vous obtenez toujours le même.
 
---
 
### Analogie : l'imprimante de la classe
 
Il y a **une seule imprimante** dans le local. Quand 30 étudiants veulent imprimer, ils envoient tous leurs travaux à **la même imprimante** — personne n'en crée une nouvelle pour lui tout seul. Si chacun instanciait sa propre imprimante, ce serait le chaos.
 
C'est exactement ça, le Singleton.
 
---
 
### Cas d'usage typiques
 
| Cas | Pourquoi un Singleton ? |
|---|---|
| Imprimante | Une seule file d'attente partagée entre tous |
| Logger | Tous les modules écrivent dans le même fichier |
| Configuration | Les paramètres sont lus une seule fois au démarrage |
| Cache | Un seul cache partagé dans toute l'application |
 
---
 
### Implémentation en C#
 
```csharp
public class Imprimante
{
    // 1. Champ statique privé — l'instance unique
    private static Imprimante _instance;
 
    // 2. Verrou thread-safe — évite les problèmes si deux threads demandent
    //    l'instance en même temps
    private static readonly object _lock = new object();
 
    private Queue<string> _fileAttente = new Queue<string>();
 
    // 3. Constructeur PRIVÉ — new Imprimante() est interdit de l'extérieur
    private Imprimante()
    {
        Console.WriteLine("🖨️ Imprimante initialisée (une seule fois !)");
    }
 
    // 4. Propriété statique publique — seul point d'accès
    public static Imprimante Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new Imprimante();
                }
            }
            return _instance;
        }
    }
 
    public void Imprimer(string document)
    {
        _fileAttente.Enqueue(document);
        Console.WriteLine($"📄 [{document}] ajouté à la file ({_fileAttente.Count} en attente)");
    }
}
```
 
**Utilisation — 3 étudiants, 1 seule imprimante :**
 
```csharp
Imprimante.Instance.Imprimer("Travail de Alexandre");
Imprimante.Instance.Imprimer("Travail de Hamza");
Imprimante.Instance.Imprimer("Travail de Kira");
Imprimante.Instance.Imprimer("Travail de Ellyn");

 
// Les 3 accèdent au MÊME objet Imprimante — même file d'attente
bool memeObjet = ReferenceEquals(Imprimante.Instance, Imprimante.Instance); // true
```
 
---
 
### Avantages et inconvénients
 
| ✅ Avantages | ⚠️ Inconvénients |
|---|---|
| Une seule instance garantie | Couplage fort (accès global statique) |
| Économie de ressources | Difficile à remplacer dans les tests |
| Initialisation paresseuse | Problèmes potentiels en multi-thread |
 
> **Note :** le Singleton classique est pratique, mais la solution moderne est de confier cette responsabilité au **conteneur IoC** (voir section 3), ce qui préserve la testabilité.
 
---

## Injection de dépendances (Contexte général)

### Le problème des dépendances

**Mise en situation : Un système de notification**

Vous développez une application qui doit envoyer des notifications :

* Par email
* Par SMS
* Par notification push

**❌ Approche naïve (dépendances câblées en dur) :**

```csharp
public class ServiceUtilisateur
{
    private EmailService emailService;
    private SMSService smsService;
    
    public ServiceUtilisateur()
    {
        // Créer les dépendances directement
        emailService = new EmailService();
        smsService = new SMSService();
    }
    
    public void EnregistrerUtilisateur(string nom, string email, string telephone)
    {
        // Logique d'enregistrement
        Console.WriteLine($"Utilisateur {nom} enregistré");
        
        // Envoyer notifications
        emailService.Envoyer(email, "Bienvenue !");
        smsService.Envoyer(telephone, "Compte créé");
    }
}
```

**Problèmes de cette approche :**

1. **Couplage fort**

```csharp
// ServiceUtilisateur DÉPEND DIRECTEMENT de EmailService et SMSService
// Si EmailService change, ServiceUtilisateur doit changer aussi
```

2. **Difficile à tester**

```csharp
// Pour tester ServiceUtilisateur, je DOIS avoir un serveur email fonctionnel
// Les emails seront vraiment envoyés pendant les tests !
```

3. **Pas flexible**

```csharp
// Et si je veux utiliser un autre service email ?
// Je dois modifier ServiceUtilisateur
```

4. **Pas réutilisable**

```csharp
// ServiceUtilisateur est lié à des implémentations spécifiques
// Impossible de le réutiliser avec d'autres services
```

---

### Le principe d'inversion de dépendance (DIP)

**Principe fondamental :** Dépendre d'abstractions, pas de concrétions.

**Analogie :** Les prises électriques

* Tous vos appareils utilisent une **prise standard** (abstraction)
* Vous ne câblez pas directement les appareils à l'alimentation électrique
* Vous pouvez brancher n'importe quel appareil (machine à café, ordinateur, lampe)
* L'appareil ne sait pas **d'où vient** l'électricité (centrale nucléaire, solaire, etc.)

**Diagramme :**

```
❌ AVANT (dépendance directe)
┌──────────────────┐
│ ServiceUtilisateur│
└──────────────────┘
        │
        │ dépend de
        ↓
┌──────────────────┐
│  EmailService    │  ← Classe concrète
└──────────────────┘

✅ APRÈS (inversion de dépendance)
┌──────────────────┐
│ ServiceUtilisateur│
└──────────────────┘
        │
        │ dépend de
        ↓
┌──────────────────┐
│ INotification    │  ← Interface (abstraction)
└──────────────────┘
        △
        │ implémente
    ┌───┴───┬────────────┐
┌────────┐ │         ┌────────┐
│Email   │ │         │SMS     │
│Service │ │         │Service │
└────────┘ │         └────────┘
        ┌────────┐
        │Push    │
        │Service │
        └────────┘
```

---

### Implémentation complète

**Étape 1 : Définir l'abstraction (interface)**

```csharp
public interface INotificationService
{
    void Envoyer(string destinataire, string message);
}
```

**Étape 2 : Créer les implémentations concrètes**

```csharp
// Service Email
public class EmailService : INotificationService
{
    public void Envoyer(string destinataire, string message)
    {
        Console.WriteLine($"📧 Email envoyé à {destinataire}: {message}");
        // Code réel pour envoyer un email
    }
}

// Service SMS
public class SMSService : INotificationService
{
    public void Envoyer(string destinataire, string message)
    {
        Console.WriteLine($"📱 SMS envoyé à {destinataire}: {message}");
        // Code réel pour envoyer un SMS
    }
}

// Service Push
public class PushNotificationService : INotificationService
{
    public void Envoyer(string destinataire, string message)
    {
        Console.WriteLine($"🔔 Notification push à {destinataire}: {message}");
        // Code réel pour envoyer une notification push
    }
}

// Faux service pour les tests
public class FakeNotificationService : INotificationService
{
    public List<string> MessagesEnvoyes = new List<string>();
    
    public void Envoyer(string destinataire, string message)
    {
        MessagesEnvoyes.Add($"{destinataire}: {message}");
        Console.WriteLine($"🧪 [TEST] Message enregistré (non envoyé)");
    }
}
```

**Étape 3 : Injection de dépendances**

Il existe **3 types d'injection**, mais on va couvrir seulement le premier:

**Injection par constructeur**

```csharp
public class ServiceUtilisateur
{
    private readonly INotificationService notificationService;
    
    // La dépendance est INJECTÉE via le constructeur
    public ServiceUtilisateur(INotificationService notificationService)
    {
        this.notificationService = notificationService;
    }
    
    public void EnregistrerUtilisateur(string nom, string contact)
    {
        Console.WriteLine($"✓ Utilisateur {nom} enregistré");
        notificationService.Envoyer(contact, "Bienvenue !");
    }
}
```

**Utilisation :**

```csharp
// En production : Email
INotificationService emailService = new EmailService();
ServiceUtilisateur service = new ServiceUtilisateur(emailService);
service.EnregistrerUtilisateur("Alice", "alice@email.com");

// En test : Fake
INotificationService fakeService = new FakeNotificationService();
ServiceUtilisateur serviceTest = new ServiceUtilisateur(fakeService);
serviceTest.EnregistrerUtilisateur("Bob", "bob@test.com");
```
---

### Conteneur d'injection de dépendances (IoC Container)
 
**Problème :** Créer toutes les dépendances manuellement devient fastidieux.
 
**Solution :** Un conteneur IoC (Inversion of Control) gère la création des objets automatiquement.
 
**Exemple avec `Microsoft.Extensions.DependencyInjection` :**
 
```csharp
using Microsoft.Extensions.DependencyInjection;
 
class Program
{
    static void Main(string[] args)
    {
        // 1. Créer le conteneur
        var services = new ServiceCollection();
        
        // 2. Enregistrer les dépendances
        services.AddTransient<INotificationService, EmailService>();
        services.AddTransient<ServiceUtilisateur>();
        
        // 3. Construire le provider
        var serviceProvider = services.BuildServiceProvider();
        
        // 4. Résoudre automatiquement les dépendances
        var service = serviceProvider.GetService<ServiceUtilisateur>();
        // ↑ Le conteneur crée automatiquement EmailService et l'injecte
        
        // 5. Utiliser
        service.EnregistrerUtilisateur("Alice", "alice@email.com");
    }
}
```
 
**Durées de vie :**
 
```csharp
// Transient : Nouvelle instance à chaque demande
services.AddTransient<INotificationService, EmailService>();
 
// Scoped : Une instance par "scope" (requête web, par exemple)
services.AddScoped<INotificationService, EmailService>();
 
// Singleton : UNE SEULE instance pour toute l'application
services.AddSingleton<INotificationService, EmailService>();
```
 
---
 
### Comprendre les durées de vie
 
| Durée de vie | Création | Destruction | Cas d'usage |
|-------------|----------|-------------|-------------|
| **Transient** | À chaque injection | Après utilisation | Services légers sans état, ViewModels, Views |
| **Scoped** | Au début du scope | À la fin du scope | DbContext, Repositories, Services avec état temporaire |
| **Singleton** | Au démarrage | À la fermeture | Configuration, Logger, Cache, Services sans état |
 
**⚠️ Règle importante :** Un service Singleton **ne peut pas** dépendre d'un service Scoped.
 
```csharp
// ❌ ERREUR !
public class MonSingleton
{
    // DbContext est Scoped — impossible de l'injecter dans un Singleton
    public MonSingleton(ApplicationDbContext context) { }
}
```
 
---
 
### Avantages de l'injection de dépendances
 
**1. Testabilité**
 
```csharp
[TestMethod]
public void Test_EnregistrerUtilisateur()
{
    var fakeService = new FakeNotificationService();
    var service = new ServiceUtilisateur(fakeService);
    
    service.EnregistrerUtilisateur("Bob", "bob@test.com");
    
    Assert.AreEqual(1, fakeService.MessagesEnvoyes.Count);
    Assert.IsTrue(fakeService.MessagesEnvoyes[0].Contains("Bienvenue"));
}
```
 
**2. Flexibilité**
 
```csharp
// En production
services.AddTransient<INotificationService, EmailService>();
 
// En développement / tests
services.AddTransient<INotificationService, FakeNotificationService>();
 
// Pas besoin de changer ServiceUtilisateur !
```
 
**3. Découplage** — `ServiceUtilisateur` ne sait pas quelle implémentation il utilise.
 
**4. Maintenabilité** — Ajouter une implémentation ne nécessite pas de modifier les classes existantes.
 
```csharp
public class SlackService : INotificationService { ... }
 
// Juste changer l'enregistrement
services.AddTransient<INotificationService, SlackService>();
```
 
---
 
## Injection de dépendances avec WPF et MVVM
 
### Installation des packages NuGet
 
Dans Visual Studio : clic droit sur le projet → **Manage NuGet Packages** → onglet **Browse**.
 
**Package obligatoire :**
 
```
Install-Package Microsoft.Extensions.DependencyInjection
```
 
**Packages pour Entity Framework Core (si base de données) :**
 
```
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.Sqlite   # ou SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.EntityFrameworkCore.Design
```
 
**Packages optionnels recommandés :**
 
```
Install-Package CommunityToolkit.Mvvm
Install-Package Microsoft.Extensions.Configuration.Json
```
 
---
 
### Configuration complète d'une application WPF
 
On configure l'injection de dépendances dans `App.xaml.cs` en utilisant directement **`ServiceCollection`**.
 
---
 
### App.xaml — Retirer le StartupUri
 
```xml
<Application x:Class="MonApp.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <!-- ✅ Pas de StartupUri — on gère le démarrage manuellement -->
</Application>
```
 
---
### `App.xaml.cs` avec `appsettings.json`
 
**`appsettings.json`** — propriété *Copy to Output Directory* = **Copy always** :
 
```json
{
  "AppTitle": "Gestionnaire de tâches",
  "AdminEmail": "admin@monapp.com",
  "NotificationsEnabled": true,
  "ConnectionStrings": {
    "Default": "Data Source=app.db"
  }
}

```
 
### App.xaml.cs — Configuration du conteneur IoC avec ServiceCollection
 
```csharp
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using MonApp.Data;
using MonApp.Repositories;
using MonApp.Services;
using MonApp.ViewModels;
using MonApp.Views;
 
namespace MonApp
{
    public partial class App : Application
    {
        // On expose le fournisseur de services de manière statique pour y accéder au besoin
        public static IServiceProvider ServiceProvider { get; private set; }
 
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Lire appsettings.json
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
 
 
            // 1. Créer la collection de services
            var services = new ServiceCollection();
 
            // ═══════════════════════════════════════════
            // 2. BASE DE DONNÉES (Scoped)
            // ═══════════════════════════════════════════
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite("Data Source=monapp.db");
            });
 
            // ═══════════════════════════════════════════
            // 3. REPOSITORIES (Scoped)
            // ═══════════════════════════════════════════
            services.AddScoped<ITacheRepository, TacheRepository>();
            services.AddScoped<ICategorieRepository, CategorieRepository>();
 
            // ═══════════════════════════════════════════
            // 4. SERVICES MÉTIER (Scoped)
            // ═══════════════════════════════════════════
            services.AddScoped<ITacheService, TacheService>();
            services.AddScoped<INotificationService, EmailService>();
 
            // ═══════════════════════════════════════════
            // 5. VIEWMODELS (Transient)
            // ═══════════════════════════════════════════
            services.AddTransient<TachesViewModel>();
            services.AddTransient<CategoriesViewModel>();
            services.AddTransient<StatistiquesViewModel>();
 
            // ═══════════════════════════════════════════
            // 6. VIEWS (Transient)
            // ═══════════════════════════════════════════
            services.AddTransient<MainWindow>();
 
            // ═══════════════════════════════════════════
            // 7. SINGLETONS (ex. : Logger)
            // ═══════════════════════════════════════════
            // services.AddSingleton<ILogger, FileLogger>();
 
            // 8. Construire le provider et l'exposer
            // On construit le "conteneur" (celui qui fabrique les objets)
            ServiceProvider = services.BuildServiceProvider();
 
            // 9. Résoudre et afficher MainWindow
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
 
        protected override void OnExit(ExitEventArgs e)
        {
            // Libérer les ressources si nécessaire
            if (ServiceProvider is IDisposable disposable)
                disposable.Dispose();
 
            base.OnExit(e);
        }
    }
}
```
 
---
 
### Architecture complète avec DI
 
```
App.xaml.cs (ServiceCollection — configuration DI)
    ↓ résout
MainWindow (reçoit ViewModel injecté)
    ↓ utilise
ViewModel (reçoit Service injecté)
    ↓ utilise
Service métier (reçoit Repository injecté)
    ↓ utilise
Repository (reçoit DbContext injecté)
    ↓ utilise
DbContext
```
 
---

### Avantages de cette architecture
 
✅ **Séparation des responsabilités** : Chaque couche a un rôle précis
 
✅ **Testabilité** : Chaque couche peut être testée indépendamment
 
✅ **Maintenabilité** : Changements localisés dans une seule couche
 
✅ **Réutilisabilité** : Services et repositories réutilisables
 
✅ **Pas de création manuelle** : Le conteneur IoC gère tout
 
---
 ## Résumé
 
### Principes clés
 
✅ **Dépendre d'abstractions** (interfaces), pas de concrétions (classes)
 
✅ **Injection par constructeur** : Méthode recommandée (99% des cas)
 
✅ **Conteneur IoC** : Gère automatiquement la création et l'injection
 
✅ **3 durées de vie** :
- `Singleton` : Une instance globale
- `Scoped` : Une instance par scope (DbContext, Repository)
- `Transient` : Nouvelle instance à chaque fois (ViewModel, View)
 
### Configuration WPF avec ServiceCollection
 
```csharp
// App.xaml.cs — Template de base
protected override void OnStartup(StartupEventArgs e)
{
    base.OnStartup(e);
 
    var services = new ServiceCollection();
 
    // DbContext (Scoped)
    services.AddDbContext<MonDbContext>();
 
    // Repositories (Scoped)
    services.AddScoped<IMonRepository, MonRepository>();
 
    // Services (Scoped)
    services.AddScoped<IMonService, MonService>();
 
    // ViewModels (Transient)
    services.AddTransient<MonViewModel>();
 
    // Views (Transient)
    services.AddTransient<MainWindow>();
 
    // Singletons
    services.AddSingleton<ILogger, FileLogger>();
 
    ServiceProvider = services.BuildServiceProvider();
 
    var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
    mainWindow.Show();
}
```
 
### Architecture complète
 
```
App.xaml.cs
    → MainWindow (+ ViewModel)
        → ViewModel (+ Service)
            → Service (+ Repository + autres services)
                → Repository (+ DbContext)
                    → DbContext
```
 
---
 
**L'injection de dépendances est une pratique essentielle du développement professionnel !**