---
title: "Injection de dépendance"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 17
---

## : Injection de dépendances (Contexte général)

### Le problème des dépendances

**Mise en situation : Un système de notification**

Vous développez une application qui doit envoyer des notifications :
- Par email
- Par SMS
- Par notification push

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

### Le principe d'inversion de dépendance (DIP)

**Principe fondamental :** Dépendre d'abstractions, pas de concrétions.

**Analogie :** Les prises électriques
- Tous vos appareils utilisent une **prise standard** (abstraction)
- Vous ne câblez pas directement les appareils à l'alimentation électrique
- Vous pouvez brancher n'importe quel appareil (machine à café, ordinateur, lampe)
- L'appareil ne sait pas **d'où vient** l'électricité (centrale nucléaire, solaire, etc.)

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

### Comparaison des types d'injection

| Type | Avantages | Inconvénients | Quand utiliser |
|------|-----------|---------------|----------------|
| **Constructeur** | - Dépendances obligatoires<br>- Immutabilité (`readonly`)<br>- Dépendances claires | - Constructeur peut devenir long | **Par défaut** (99% des cas) |
| **Propriété** | - Dépendances optionnelles<br>- Flexible | - Risque d'oubli<br>- Mutable | Dépendances optionnelles |
| **Méthode** | - Dépendance ponctuelle | - Répétitif<br>- Pas de contrôle | Dépendance change souvent |

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

### Le lien avec MVVM (aperçu)

**En MVVM, on injecte souvent des services dans les ViewModels :**

```csharp
public class ProduitViewModel
{
    private readonly IProduitRepository repository;
    private readonly INotificationService notificationService;
    
    // Injection par constructeur
    public ProduitViewModel(IProduitRepository repository, 
                           INotificationService notificationService)
    {
        this.repository = repository;
        this.notificationService = notificationService;
    }
    
    public void AjouterProduit(Produit produit)
    {
        repository.Ajouter(produit);
        notificationService.Envoyer("admin@app.com", "Nouveau produit ajouté");
    }
}
```

**Avantages en MVVM :**
- ✅ ViewModel testable sans interface graphique
- ✅ Réutilisable avec différents services
- ✅ Découplé de l'implémentation des services

---

