---
title: "Patron observateur"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 15
---

## Partie 1 : Le patron Observateur (Contexte général)

### Qu'est-ce qu'un patron de conception ?

**Définition simple :** Un patron de conception est une **solution éprouvée** à un **problème récurrent** en programmation.

**Analogie :** C'est comme une recette de cuisine. Si vous voulez faire un gâteau, vous n'inventez pas la méthode à chaque fois — vous suivez une recette qui a fait ses preuves.

**Les patrons de conception ne sont pas :**
- ❌ Du code à copier-coller
- ❌ Une bibliothèque ou un framework
- ❌ Spécifiques à un langage

**Les patrons de conception sont :**
- ✅ Des principes d'organisation du code
- ✅ Des solutions conceptuelles
- ✅ Applicables dans n'importe quel langage orienté objet

### Le patron Observateur — Le problème qu'il résout

**Mise en situation réelle :**

Imaginez une station météo. Elle mesure :
- La température
- L'humidité
- La pression

Plusieurs appareils doivent afficher ces données :
- Un thermomètre digital
- Une application mobile
- Un site web
- Un système d'alerte

**Problème :** Comment faire en sorte que tous ces appareils soient notifiés automatiquement quand les données changent ?

**❌ Mauvaise solution (couplage fort) :**

```csharp
public class StationMeteo
{
    private double temperature;
    
    public void SetTemperature(double temp)
    {
        temperature = temp;
        
        // Mettre à jour chaque appareil manuellement
        thermometre.Afficher(temp);
        appMobile.MettreAJour(temp);
        siteWeb.Actualiser(temp);
        systemeAlerte.Verifier(temp);
        
        // Problème : Si on ajoute un nouvel appareil, 
        // il faut modifier cette méthode !
    }
}
```

**Problèmes de cette approche :**
1. La station météo **connaît** tous les appareils (couplage fort)
2. Pour ajouter un nouvel appareil, il faut **modifier** la station
3. Impossible de **désactiver** un appareil sans modifier le code
4. Code **rigide** et **difficile à maintenir**

### Le patron Observateur — La solution

**Principe fondamental :** Inverser la dépendance.

Au lieu que la station connaisse tous les appareils, **les appareils s'abonnent à la station**.

**Analogie :** C'est comme une newsletter par email :
- Le site web (sujet) publie du contenu
- Les lecteurs (observateurs) s'abonnent
- Quand un nouvel article sort, tous les abonnés reçoivent un email
- Les lecteurs peuvent se désabonner à tout moment
- Le site web ne connaît pas les lecteurs individuellement

**Diagramme conceptuel :**

```
┌─────────────────────────────────────┐
│       SUJET (Observable)            │
│  - Liste d'observateurs             │
│  + Attacher(observateur)            │
│  + Détacher(observateur)            │
│  + Notifier()                       │
└─────────────────────────────────────┘
                  │
                  │ notifie
                  ↓
        ┌─────────────────┐
        │   OBSERVATEUR    │
        │   (Interface)    │
        │  + Actualiser()  │
        └─────────────────┘
              △
              │ implémente
     ┌────────┴────────┬────────────┐
     │                 │            │
┌────────┐      ┌──────────┐  ┌─────────┐
│Thermo- │      │App       │  │Site     │
│mètre   │      │Mobile    │  │Web      │
└────────┘      └──────────┘  └─────────┘
```

### Implémentation complète

**Étape 1 : L'interface Observateur**

```csharp
// Ce que TOUS les observateurs doivent implémenter
public interface IObservateur
{
    void Actualiser(double temperature, double humidite, double pression);
}
```

**Étape 2 : Le Sujet (Observable)**

```csharp
public class StationMeteo
{
    // Liste des observateurs abonnés
    private List<IObservateur> observateurs = new List<IObservateur>();
    
    // Données
    private double temperature;
    private double humidite;
    private double pression;
    
    // Méthodes d'abonnement
    public void Abonner(IObservateur observateur)
    {
        observateurs.Add(observateur);
        Console.WriteLine($"{observateur.GetType().Name} s'est abonné");
    }
    
    public void Desabonner(IObservateur observateur)
    {
        observateurs.Remove(observateur);
        Console.WriteLine($"{observateur.GetType().Name} s'est désabonné");
    }
    
    // Notifier tous les observateurs
    private void Notifier()
    {
        Console.WriteLine("=== Notification des observateurs ===");
        foreach (var observateur in observateurs)
        {
            observateur.Actualiser(temperature, humidite, pression);
        }
    }
    
    // Quand les données changent
    public void SetMesures(double temp, double hum, double press)
    {
        Console.WriteLine($"\n📡 Nouvelles mesures reçues");
        temperature = temp;
        humidite = hum;
        pression = press;
        
        // Notifier automatiquement
        Notifier();
    }
}
```

**Étape 3 : Les observateurs concrets**

```csharp
// Observateur 1 : Thermomètre digital
public class ThermometreDigital : IObservateur
{
    public void Actualiser(double temperature, double humidite, double pression)
    {
        Console.WriteLine($"🌡️  Thermomètre : {temperature}°C");
    }
}

// Observateur 2 : Application mobile
public class ApplicationMobile : IObservateur
{
    public void Actualiser(double temperature, double humidite, double pression)
    {
        Console.WriteLine($"📱 App Mobile : Temp={temperature}°C, " +
                         $"Humidité={humidite}%, Pression={pression}hPa");
    }
}

// Observateur 3 : Système d'alerte
public class SystemeAlerte : IObservateur
{
    private const double SEUIL_CANICULE = 35;
    private const double SEUIL_GEL = 0;
    
    public void Actualiser(double temperature, double humidite, double pression)
    {
        if (temperature >= SEUIL_CANICULE)
            Console.WriteLine("⚠️  ALERTE : Canicule détectée !");
        else if (temperature <= SEUIL_GEL)
            Console.WriteLine("⚠️  ALERTE : Risque de gel !");
        else
            Console.WriteLine("✅ Système d'alerte : Conditions normales");
    }
}

// Observateur 4 : Site web
public class SiteWeb : IObservateur
{
    public void Actualiser(double temperature, double humidite, double pression)
    {
        Console.WriteLine($"🌐 Site Web actualisé : {temperature}°C");
    }
}
```

**Étape 4 : Utilisation**

```csharp
class Program
{
    static void Main(string[] args)
    {
        // Créer la station météo
        StationMeteo station = new StationMeteo();
        
        // Créer les observateurs
        ThermometreDigital thermo = new ThermometreDigital();
        ApplicationMobile appMobile = new ApplicationMobile();
        SystemeAlerte alerte = new SystemeAlerte();
        SiteWeb siteWeb = new SiteWeb();
        
        // Abonnement
        Console.WriteLine("=== Phase d'abonnement ===");
        station.Abonner(thermo);
        station.Abonner(appMobile);
        station.Abonner(alerte);
        station.Abonner(siteWeb);
        
        // Première mise à jour
        station.SetMesures(22.5, 65, 1013);
        
        // Deuxième mise à jour
        station.SetMesures(38, 45, 1010);
        
        // Le site web se désabonne
        Console.WriteLine("\n=== Site web se désabonne ===");
        station.Desabonner(siteWeb);
        
        // Troisième mise à jour (le site web ne reçoit plus de notification)
        station.SetMesures(-2, 80, 1020);
    }
}
```

**Sortie :**
```
=== Phase d'abonnement ===
ThermometreDigital s'est abonné
ApplicationMobile s'est abonné
SystemeAlerte s'est abonné
SiteWeb s'est abonné

📡 Nouvelles mesures reçues
=== Notification des observateurs ===
🌡️  Thermomètre : 22.5°C
📱 App Mobile : Temp=22.5°C, Humidité=65%, Pression=1013hPa
✅ Système d'alerte : Conditions normales
🌐 Site Web actualisé : 22.5°C

📡 Nouvelles mesures reçues
=== Notification des observateurs ===
🌡️  Thermomètre : 38°C
📱 App Mobile : Temp=38°C, Humidité=45%, Pression=1010hPa
⚠️  ALERTE : Canicule détectée !
🌐 Site Web actualisé : 38°C

=== Site web se désabonne ===
SiteWeb s'est désabonné

📡 Nouvelles mesures reçues
=== Notification des observateurs ===
🌡️  Thermomètre : -2°C
📱 App Mobile : Temp=-2°C, Humidité=80%, Pression=1020hPa
⚠️  ALERTE : Risque de gel !
```

### Avantages du patron Observateur

**1. Découplage (Low Coupling)**
```csharp
// La station ne connaît PAS les observateurs concrets
// Elle connaît seulement l'interface IObservateur
// On peut ajouter 100 nouveaux observateurs sans modifier la station
```

**2. Ouvert/Fermé (Open/Closed Principle)**
- **Ouvert** à l'extension : On peut ajouter de nouveaux observateurs
- **Fermé** à la modification : Pas besoin de modifier le sujet

**3. Flexibilité**
```csharp
// Abonnement/désabonnement dynamique à l'exécution
station.Abonner(nouvelObservateur);
station.Desabonner(ancienObservateur);
```

**4. Diffusion un-à-plusieurs**
```csharp
// Une seule mise à jour → Tous les observateurs notifiés
station.SetMesures(25, 60, 1015);  // Un appel
// → Notifie automatiquement tous les abonnés
```

### Quand utiliser le patron Observateur ?

**✅ Utilisez-le quand :**
- Un changement d'état dans un objet nécessite de mettre à jour d'autres objets
- Vous ne savez pas à l'avance combien d'objets doivent être notifiés
- Vous voulez découpler le sujet des observateurs
- Vous voulez permettre l'ajout/suppression d'observateurs dynamiquement

**❌ N'utilisez pas si :**
- Il n'y a qu'un seul observateur (pas besoin de ce patron)
- Les observateurs sont très peu nombreux et fixes (une simple méthode suffit)
- Performance critique (chaque notification a un coût)

### Variantes du patron Observateur

**1. Push vs Pull**

**Push (ce qu'on a fait) :** Le sujet envoie les données
```csharp
void Actualiser(double temp, double hum, double press)
{
    // Toutes les données sont poussées
}
```

**Pull :** L'observateur récupère ce dont il a besoin
```csharp
public interface IObservateur
{
    void Actualiser(StationMeteo station);
}

public class ThermometreDigital : IObservateur
{
    public void Actualiser(StationMeteo station)
    {
        // Je récupère seulement ce qui m'intéresse
        double temp = station.GetTemperature();
        Console.WriteLine($"Temp: {temp}°C");
    }
}
```

**2. Événements C# (version .NET du patron)**

C# implémente le patron Observateur avec les **événements** :

```csharp
public class StationMeteo
{
    // Définir un événement
    public event EventHandler<MesuresEventArgs> MesuresChangees;
    
    private double temperature;
    
    public void SetTemperature(double temp)
    {
        temperature = temp;
        
        // Déclencher l'événement
        MesuresChangees?.Invoke(this, new MesuresEventArgs 
        { 
            Temperature = temp 
        });
    }
}

// Classe pour passer les données
public class MesuresEventArgs : EventArgs
{
    public double Temperature { get; set; }
    public double Humidite { get; set; }
    public double Pression { get; set; }
}

// Utilisation
StationMeteo station = new StationMeteo();

// S'abonner avec +=
station.MesuresChangees += (sender, e) => 
{
    Console.WriteLine($"Température : {e.Temperature}°C");
};

// Déclencher
station.SetTemperature(25);
```

## Partie 2 : Le patron Observateur (INotifyPropertyChanged)

### Le problème

Avec le code précédent, si on modifie une propriété en C#, **l'interface ne se met PAS à jour** :

```csharp
Personne p = new Personne { Nom = "Alice" };
this.DataContext = p;

// Plus tard...
p.Nom = "Bob";  // L'interface ne change pas ! ❌
```

**Pourquoi ?** WPF ne sait pas que `Nom` a changé.

### La solution : INotifyPropertyChanged

Pour que WPF détecte les changements, la classe doit implémenter l'interface **INotifyPropertyChanged**.

**Classe Personne améliorée :**

```csharp
using System.ComponentModel;

public class Personne : INotifyPropertyChanged
{
    private string nom;
    private int age;
    
    public string Nom
    {
        get { return nom; }
        set 
        { 
            nom = value;
            OnPropertyChanged("Nom");  // Notifier le changement
        }
    }
    
    public int Age
    {
        get { return age; }
        set 
        { 
            age = value;
            OnPropertyChanged("Age");
        }
    }
    
    // Événement requis par l'interface
    public event PropertyChangedEventHandler PropertyChanged;
    
    // Méthode pour déclencher l'événement
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

**Maintenant, ça fonctionne :**

```csharp
Personne p = new Personne { Nom = "Alice" };
this.DataContext = p;

// Plus tard...
p.Nom = "Bob";  // L'interface se met à jour automatiquement ! ✅
```

### Explication détaillée

**Le patron Observateur** (Observer Pattern) :
1. Un objet (la **source**) notifie ses observateurs quand son état change
2. Les observateurs (ici, les **contrôles WPF**) écoutent ces notifications
3. Quand notifiés, ils se mettent à jour

**Dans notre exemple :**
- **Source** : La classe `Personne`
- **Observateurs** : Les TextBox et TextBlock
- **Notification** : `PropertyChanged?.Invoke(...)`

**Le `?` (opérateur null-conditional) :**
- `PropertyChanged?.Invoke(...)` = Si `PropertyChanged` n'est pas null, alors invoke
- Évite une exception si personne n'écoute

### Version moderne avec CallerMemberName

Pour éviter d'écrire le nom de la propriété en string, on peut utiliser `CallerMemberName` :

```csharp
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class Personne : INotifyPropertyChanged
{
    private string nom;
    private int age;
    
    public string Nom
    {
        get { return nom; }
        set 
        { 
            nom = value;
            OnPropertyChanged();  // Pas besoin de spécifier "Nom"
        }
    }
    
    public int Age
    {
        get { return age; }
        set 
        { 
            age = value;
            OnPropertyChanged();
        }
    }
    
    public event PropertyChangedEventHandler PropertyChanged;
    
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

**Avantage :** Plus sûr (pas de risque de typo dans le nom de propriété).

### Classe de base réutilisable

Pour éviter de réécrire le code dans chaque classe, créez une classe de base :

**BaseViewModel.cs :**
```csharp
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

**Utilisation :**
```csharp
public class Personne : BaseViewModel
{
    private string nom;
    
    public string Nom
    {
        get { return nom; }
        set 
        { 
            nom = value;
            OnPropertyChanged();
        }
    }
}
```

---
