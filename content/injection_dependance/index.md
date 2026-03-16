---
title: "Injection de dépendance"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 19
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

Il existe **3 types d'injection** :

**Type 1 : Injection par constructeur (recommandée)**

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

**Type 2 : Injection par propriété**

```csharp
public class ServiceUtilisateur
{
    // Propriété publique
    public INotificationService NotificationService { get; set; }
    
    public void EnregistrerUtilisateur(string nom, string contact)
    {
        Console.WriteLine($"✓ Utilisateur {nom} enregistré");
        NotificationService.Envoyer(contact, "Bienvenue !");
    }
}
```

**Utilisation :**

```csharp
ServiceUtilisateur service = new ServiceUtilisateur();
service.NotificationService = new EmailService();  // Injection
service.EnregistrerUtilisateur("Alice", "alice@email.com");
```

**Type 3 : Injection par méthode**

```csharp
public class ServiceUtilisateur
{
    public void EnregistrerUtilisateur(string nom, string contact, 
                                      INotificationService notificationService)
    {
        Console.WriteLine($"✓ Utilisateur {nom} enregistré");
        notificationService.Envoyer(contact, "Bienvenue !");
    }
}
```

**Utilisation :**

```csharp
ServiceUtilisateur service = new ServiceUtilisateur();
service.EnregistrerUtilisateur("Alice", "alice@email.com", new EmailService());
```

---

### Comparaison des types d'injection

| Type | Avantages | Inconvénients | Quand utiliser |
|------|-----------|---------------|----------------|
| **Constructeur** | - Dépendances obligatoires<br>- Immutabilité (`readonly`)<br>- Dépendances claires | - Constructeur peut devenir long | **Par défaut** (99% des cas) |
| **Propriété** | - Dépendances optionnelles<br>- Flexible | - Risque d'oubli<br>- Mutable | Dépendances optionnelles |
| **Méthode** | - Dépendance ponctuelle | - Répétitif<br>- Pas de contrôle | Dépendance change souvent |

---

### Conteneur d'injection de dépendances (IoC Container)

**Problème :** Créer toutes les dépendances manuellement devient fastidieux.

**Solution :** Un conteneur IoC (Inversion of Control) gère la création des objets.

**Exemple avec Microsoft.Extensions.DependencyInjection :**

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

// Scoped : Une instance par "scope" (requête web par exemple)
services.AddScoped<INotificationService, EmailService>();

// Singleton : UNE SEULE instance pour toute l'application
services.AddSingleton<INotificationService, EmailService>();
```

### Comprendre les durées de vie

| Durée de vie | Création | Destruction | Cas d'usage |
|--------------|----------|-------------|-------------|
| **Transient** | À chaque injection | Après utilisation | Services légers sans état<br>ViewModels, Views |
| **Scoped** | Au début du scope | À la fin du scope | DbContext, Repositories<br>Services avec état temporaire |
| **Singleton** | Au démarrage | À la fermeture | Configuration, Logger<br>Cache, Services sans état |

**⚠️ Règle importante :** Un service Singleton **ne peut pas** dépendre d'un service Scoped.

```csharp
// ❌ ERREUR !
public class MonSingleton
{
    // DbContext est Scoped, impossible de l'injecter dans un Singleton
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
    // Utiliser un faux service
    var fakeService = new FakeNotificationService();
    var service = new ServiceUtilisateur(fakeService);
    
    service.EnregistrerUtilisateur("Bob", "bob@test.com");
    
    // Vérifier qu'un message a été "envoyé"
    Assert.AreEqual(1, fakeService.MessagesEnvoyes.Count);
    Assert.IsTrue(fakeService.MessagesEnvoyes[0].Contains("Bienvenue"));
}
```

**2. Flexibilité**

```csharp
// Basculer facilement entre implémentations
// En production
services.AddTransient<INotificationService, EmailService>();

// En développement
services.AddTransient<INotificationService, FakeNotificationService>();

// Pas besoin de changer ServiceUtilisateur !
```

**3. Découplage**

```csharp
// ServiceUtilisateur ne sait PAS quelle implémentation il utilise
// Il dépend seulement de l'interface INotificationService
```

**4. Maintenabilité**

```csharp
// Ajouter une nouvelle implémentation ne nécessite PAS de modifier les classes existantes
public class SlackService : INotificationService { ... }

// Juste changer l'enregistrement
services.AddTransient<INotificationService, SlackService>();
```

---

## Injection de dépendances avec WPF et MVVM

### Installation des packages NuGet

Avant de commencer, vous devez installer les packages suivants dans votre projet WPF.

**Dans Visual Studio :**
1. Clic droit sur le projet → **Manage NuGet Packages**
2. Onglet **Browse**
3. Installer ces packages :

**Packages obligatoires pour l'injection de dépendances :**

```powershell
# Conteneur d'injection de dépendances
Install-Package Microsoft.Extensions.DependencyInjection

# Host et configuration (inclut DependencyInjection)
Install-Package Microsoft.Extensions.Hosting
```

**Packages pour Entity Framework Core (si vous utilisez une base de données) :**

```powershell
# Entity Framework Core
Install-Package Microsoft.EntityFrameworkCore

# Provider SQL Server
Install-Package Microsoft.EntityFrameworkCore.SqlServer
# OU pour SQLite
Install-Package Microsoft.EntityFrameworkCore.Sqlite

# Outils pour les migrations
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.EntityFrameworkCore.Design
```

**Packages optionnels mais recommandés :**

```powershell
# Pour les commandes MVVM (RelayCommand)
Install-Package CommunityToolkit.Mvvm

# Pour lire les fichiers de configuration JSON
Install-Package Microsoft.Extensions.Configuration.Json

# Pour les User Secrets (développement)
Install-Package Microsoft.Extensions.Configuration.UserSecrets
```

**⚠️ Note importante :** Le package `Microsoft.Extensions.Hosting` **inclut déjà** `Microsoft.Extensions.DependencyInjection`, donc techniquement vous n'avez besoin que de `Hosting`. Mais on peut installer les deux pour plus de clarté.

---

### Configuration complète d'une application WPF

Dans une application WPF professionnelle, on configure l'injection de dépendances dans `App.xaml.cs`.

---

### App.xaml - Retirer le StartupUri

```xml
<Application x:Class="MonApp.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <!-- ✅ Pas de StartupUri - on gère le démarrage manuellement -->
</Application>
```

---

### App.xaml.cs - Configuration du conteneur IoC

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        private readonly IHost _host;
        
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // ═══════════════════════════════════════════
                    // 1. BASE DE DONNÉES (Scoped)
                    // ═══════════════════════════════════════════
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseSqlServer(
                            @"Server=(localdb)\mssqllocaldb;
                              Database=MonAppDb;
                              Trusted_Connection=True;"
                        );
                    });
                    
                    // ═══════════════════════════════════════════
                    // 2. REPOSITORIES (Scoped)
                    // ═══════════════════════════════════════════
                    services.AddScoped<ITacheRepository, TacheRepository>();
                    services.AddScoped<ICategorieRepository, CategorieRepository>();
                    
                    // ═══════════════════════════════════════════
                    // 3. SERVICES MÉTIER (Scoped)
                    // ═══════════════════════════════════════════
                    services.AddScoped<ITacheService, TacheService>();
                    services.AddScoped<INotificationService, EmailService>();
                    
                    // ═══════════════════════════════════════════
                    // 4. VIEWMODELS (Transient)
                    // ═══════════════════════════════════════════
                    services.AddTransient<TachesViewModel>();
                    services.AddTransient<CategoriesViewModel>();
                    services.AddTransient<StatistiquesViewModel>();
                    
                    // ═══════════════════════════════════════════
                    // 5. VIEWS (Transient)
                    // ═══════════════════════════════════════════
                    services.AddTransient<MainWindow>();
                    
                    // ═══════════════════════════════════════════
                    // 6. SERVICES SINGLETON
                    // ═══════════════════════════════════════════
                    // services.AddSingleton<ILogger, FileLogger>();
                })
                .Build();
        }
        
        protected override async void OnStartup(StartupEventArgs e)
        {
            // Démarrer le host
            await _host.StartAsync();
            
            // Résoudre et afficher MainWindow
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
            
            base.OnStartup(e);
        }
        
        protected override async void OnExit(ExitEventArgs e)
        {
            // Nettoyer proprement
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
```

---

### Architecture complète avec DI

```
App.xaml.cs (Configuration DI)
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

### Exemple complet : Application de gestion de tâches

#### 1. Modèle

```csharp
// Models/Tache.cs
public class Tache
{
    public int Id { get; set; }
    public string Titre { get; set; }
    public bool EstTerminee { get; set; }
    public DateTime DateCreation { get; set; }
}
```

#### 2. DbContext

```csharp
// Data/ApplicationDbContext.cs
public class ApplicationDbContext : DbContext
{
    public DbSet<Tache> Taches { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
```

#### 3. Repository

```csharp
// Repositories/ITacheRepository.cs
public interface ITacheRepository
{
    List<Tache> ObtenirTout();
    void Ajouter(Tache tache);
    void Supprimer(int id);
}

// Repositories/TacheRepository.cs
public class TacheRepository : ITacheRepository
{
    private readonly ApplicationDbContext _context;
    
    // ✅ DbContext injecté automatiquement
    public TacheRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public List<Tache> ObtenirTout()
    {
        return _context.Taches.ToList();
    }
    
    public void Ajouter(Tache tache)
    {
        _context.Taches.Add(tache);
        _context.SaveChanges();
    }
    
    public void Supprimer(int id)
    {
        var tache = _context.Taches.Find(id);
        if (tache != null)
        {
            _context.Taches.Remove(tache);
            _context.SaveChanges();
        }
    }
}
```

#### 4. Service métier

```csharp
// Services/ITacheService.cs
public interface ITacheService
{
    List<Tache> ObtenirTachesNonTerminees();
    void AjouterTache(string titre);
    void MarquerCommeTerminee(int id);
}

// Services/TacheService.cs
public class TacheService : ITacheService
{
    private readonly ITacheRepository _repository;
    private readonly INotificationService _notificationService;
    
    // ✅ Repository et NotificationService injectés
    public TacheService(
        ITacheRepository repository,
        INotificationService notificationService)
    {
        _repository = repository;
        _notificationService = notificationService;
    }
    
    public List<Tache> ObtenirTachesNonTerminees()
    {
        return _repository.ObtenirTout()
            .Where(t => !t.EstTerminee)
            .ToList();
    }
    
    public void AjouterTache(string titre)
    {
        var tache = new Tache 
        { 
            Titre = titre, 
            DateCreation = DateTime.Now 
        };
        _repository.Ajouter(tache);
        _notificationService.Envoyer("admin@app.com", "Nouvelle tâche créée");
    }
    
    public void MarquerCommeTerminee(int id)
    {
        var tache = _repository.ObtenirTout().FirstOrDefault(t => t.Id == id);
        if (tache != null)
        {
            tache.EstTerminee = true;
            _notificationService.Envoyer("admin@app.com", "Tâche terminée");
        }
    }
}
```

#### 5. ViewModel

```csharp
// ViewModels/TachesViewModel.cs
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

public class TachesViewModel : INotifyPropertyChanged
{
    private readonly ITacheService _tacheService;
    
    public event PropertyChangedEventHandler PropertyChanged;
    
    public ObservableCollection<Tache> Taches { get; set; }
    
    public ICommand AjouterCommand { get; set; }
    public ICommand MarquerTermineeCommand { get; set; }
    
    // ✅ Service injecté automatiquement
    public TachesViewModel(ITacheService tacheService)
    {
        _tacheService = tacheService;
        
        ChargerTaches();
        
        AjouterCommand = new RelayCommand<string>(AjouterTache);
        MarquerTermineeCommand = new RelayCommand<int>(MarquerTerminee);
    }
    
    private void ChargerTaches()
    {
        var taches = _tacheService.ObtenirTachesNonTerminees();
        Taches = new ObservableCollection<Tache>(taches);
    }
    
    private void AjouterTache(string titre)
    {
        if (string.IsNullOrWhiteSpace(titre)) return;
        
        _tacheService.AjouterTache(titre);
        ChargerTaches();
    }
    
    private void MarquerTerminee(int id)
    {
        _tacheService.MarquerCommeTerminee(id);
        ChargerTaches();
    }
}
```

#### 6. View

```csharp
// Views/MainWindow.xaml.cs
using System.Windows;

namespace MonApp.Views
{
    public partial class MainWindow : Window
    {
        // ✅ ViewModel injecté automatiquement
        public MainWindow(TachesViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
```

```xml
<!-- Views/MainWindow.xaml -->
<Window x:Class="MonApp.Views.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Gestion de tâches" Height="400" Width="600">
    
    <Grid Margin="10">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>
        
        <!-- Zone d'ajout -->
        <StackPanel Grid.Row="0" Orientation="Horizontal" Margin="0,0,0,10">
            <TextBox x:Name="txtTitre" Width="400" Height="30"/>
            <Button Content="Ajouter" Width="100" Margin="10,0,0,0"
                    Command="{Binding AjouterCommand}"
                    CommandParameter="{Binding Text, ElementName=txtTitre}"/>
        </StackPanel>
        
        <!-- Liste des tâches -->
        <ListBox Grid.Row="1" ItemsSource="{Binding Taches}">
            <ListBox.ItemTemplate>
                <DataTemplate>
                    <StackPanel Orientation="Horizontal">
                        <CheckBox IsChecked="{Binding EstTerminee}" 
                                 Command="{Binding DataContext.MarquerTermineeCommand, 
                                          RelativeSource={RelativeSource AncestorType=Window}}"
                                 CommandParameter="{Binding Id}"/>
                        <TextBlock Text="{Binding Titre}" Margin="10,0,0,0"/>
                    </StackPanel>
                </DataTemplate>
            </ListBox.ItemTemplate>
        </ListBox>
    </Grid>
</Window>
```

---

### Avantages de cette architecture

✅ **Séparation des responsabilités** : Chaque couche a un rôle précis

✅ **Testabilité** : Chaque couche peut être testée indépendamment

✅ **Maintenabilité** : Changements localisés dans une seule couche

✅ **Réutilisabilité** : Services et repositories réutilisables

✅ **Pas de création manuelle** : Le conteneur IoC gère tout

---

## Exercices pratiques

### Exercice 1 : Ajouter un service de logging

Créez un service de logging qui enregistre toutes les actions dans un fichier.

**Tâches :**

1. Créer l'interface `ILogger` avec une méthode `Log(string message)`
2. Créer `FileLogger` qui écrit dans un fichier texte
3. Créer `ConsoleLogger` qui écrit dans la console
4. Enregistrer `ILogger` comme Singleton dans `App.xaml.cs`
5. Injecter `ILogger` dans `TacheService` et logger chaque action

<details>
<summary>📖 Voir la solution</summary>

```csharp
// Services/ILogger.cs
public interface ILogger
{
    void Log(string message);
}

// Services/FileLogger.cs
public class FileLogger : ILogger
{
    private readonly string _filePath = "app.log";
    
    public void Log(string message)
    {
        var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
        File.AppendAllText(_filePath, logEntry + Environment.NewLine);
    }
}

// Services/ConsoleLogger.cs
public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
    }
}

// Dans App.xaml.cs
services.AddSingleton<ILogger, FileLogger>(); // ou ConsoleLogger

// Dans TacheService.cs
public class TacheService : ITacheService
{
    private readonly ITacheRepository _repository;
    private readonly INotificationService _notificationService;
    private readonly ILogger _logger;
    
    public TacheService(
        ITacheRepository repository,
        INotificationService notificationService,
        ILogger logger) // ✅ Logger injecté
    {
        _repository = repository;
        _notificationService = notificationService;
        _logger = logger;
    }
    
    public void AjouterTache(string titre)
    {
        _logger.Log($"Ajout de la tâche: {titre}");
        
        var tache = new Tache 
        { 
            Titre = titre, 
            DateCreation = DateTime.Now 
        };
        _repository.Ajouter(tache);
        _notificationService.Envoyer("admin@app.com", "Nouvelle tâche créée");
        
        _logger.Log($"Tâche '{titre}' ajoutée avec succès");
    }
}
```

</details>

---

### Exercice 2 : Créer un service de configuration

Créez un service de configuration qui lit les paramètres depuis `appsettings.json`.

**Tâches :**

1. Créer `appsettings.json` avec des paramètres (titre de l'app, email admin, etc.)
2. Créer l'interface `IConfiguration` avec des propriétés
3. Créer `AppConfiguration` qui lit le fichier JSON
4. Enregistrer comme Singleton
5. Utiliser dans les services

<details>
<summary>📖 Voir la solution</summary>

```json
// appsettings.json
{
  "AppTitle": "Gestionnaire de Tâches Pro",
  "AdminEmail": "admin@monapp.com",
  "NotificationsEnabled": true,
  "DatabasePath": "Data Source=taches.db"
}
```

```csharp
// Services/IAppConfiguration.cs
public interface IAppConfiguration
{
    string AppTitle { get; }
    string AdminEmail { get; }
    bool NotificationsEnabled { get; }
    string DatabasePath { get; }
}

// Services/AppConfiguration.cs
using System.IO;
using System.Text.Json;

public class AppConfiguration : IAppConfiguration
{
    public string AppTitle { get; private set; }
    public string AdminEmail { get; private set; }
    public bool NotificationsEnabled { get; private set; }
    public string DatabasePath { get; private set; }
    
    public AppConfiguration()
    {
        var json = File.ReadAllText("appsettings.json");
        var config = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        
        AppTitle = config["AppTitle"].ToString();
        AdminEmail = config["AdminEmail"].ToString();
        NotificationsEnabled = bool.Parse(config["NotificationsEnabled"].ToString());
        DatabasePath = config["DatabasePath"].ToString();
    }
}

// Dans App.xaml.cs
services.AddSingleton<IAppConfiguration, AppConfiguration>();

// Utilisation dans TacheService
public class TacheService : ITacheService
{
    private readonly IAppConfiguration _config;
    private readonly INotificationService _notificationService;
    
    public TacheService(
        ITacheRepository repository,
        INotificationService notificationService,
        IAppConfiguration config)
    {
        _repository = repository;
        _notificationService = notificationService;
        _config = config;
    }
    
    public void AjouterTache(string titre)
    {
        var tache = new Tache { Titre = titre, DateCreation = DateTime.Now };
        _repository.Ajouter(tache);
        
        if (_config.NotificationsEnabled)
        {
            _notificationService.Envoyer(
                _config.AdminEmail, 
                $"Nouvelle tâche: {titre}"
            );
        }
    }
}
```

</details>

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

### Configuration WPF

```csharp
// App.xaml.cs - Template de base
_host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
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
    })
    .Build();
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