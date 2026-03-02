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

### Exercice - 📈 Système d'Alerte Boursière — Patron de Conception Observateur

#### Contexte

Vous devez concevoir un **système de suivi d'actions boursières** basé sur le patron de conception **Observateur (Observer Pattern)**. Lorsqu'une action (ex. : `NVDA`, `AAPL`) enregistre une variation de prix significative, tous les investisseurs abonnés à cette action doivent en être **notifiés automatiquement et en temps réel**.

---

#### Travail demandé

#### Partie 1 — Diagramme de classes *(à réaliser avant tout développement)*

Concevez le **diagramme de classes UML** correspondant au système décrit ci-dessous.

Votre diagramme devra faire apparaître :
- Les classes et interfaces avec leurs attributs et méthodes
- Les relations entre les classes (association, implémentation, dépendance)
- Les multiplicités lorsqu'elles sont pertinentes

---

#### Partie 2 — Implémentation

#### 2.1 L'interface `Observateur`

Définissez une interface `Observateur` représentant toute entité souhaitant être notifiée d'un changement de prix.

**Méthode requise :**
```java
void mettreAJour(String codeAction, double prix);
```

---

#### 2.2 La classe `Action` *(Le Sujet / Observable)*

Cette classe représente une action cotée en bourse. Elle joue le rôle de **sujet observable**.

**Attributs :**
- `nom` : le code de l'action (ex. : `"NVDA"`)
- `prix` : le prix courant de l'action

**Responsabilités :**
- Maintenir une **liste d'abonnés** (observateurs)
- Permettre l'**ajout** et la **suppression** d'abonnés
- Notifier les abonnés lors d'un changement de prix

**⚠️ Condition Spéciale :**
> La notification ne doit se déclencher **que si la variation de prix est supérieure à 2%** par rapport au dernier prix enregistré.

**Formule de variation :**
```
variation (%) = |nouveau_prix - ancien_prix| / ancien_prix × 100
```

---

#### 2.3 Les observateurs concrets

##### 🧑‍💼 `AgentBoursier`
Simule un agent financier qui réagit aux alertes de prix.

**Comportement attendu à la réception d'une notification :**
```
Achat/Vente suggéré pour [codeAction] — Nouveau prix : [prix] €
```

---

##### 📱 `ApplicationMobile`
Simule une application mobile qui envoie des notifications push à l'utilisateur.

**Comportement attendu à la réception d'une notification :**
```
🔔 Alerte de prix ! [codeAction] est désormais à [prix] €
```

---

#### Exemple de scénario de test
```java
Action nvda = new Action("NVDA", 100.0);

AgentBoursier agent = new AgentBoursier("Alice");
ApplicationMobile app = new ApplicationMobile("Bob");

nvda.abonner(agent);
nvda.abonner(app);

nvda.setPrix(101.0); // variation : 1% → aucune notification
nvda.setPrix(104.0); // variation : ~3% → notification déclenchée ✅
nvda.setPrix(105.0); // variation : ~1% → aucune notification
```

---

## Délégués et Événements en C#

### 1. Les Délégués

Un **délégué** est un type qui représente une référence vers une méthode.
Il définit une **signature** (paramètres + type de retour) que toute méthode associée doit respecter.

> 💡 Un délégué est un **contrat** : toute méthode qui respecte ce contrat peut lui être assignée.

#### Déclaration
```csharp
delegate void MonDélégué(string message);
```

#### Utilisation
```csharp
delegate void MonDélégué(string message);

static void Afficher(string message)
{
    Console.WriteLine(message);
}

static void Main()
{
    MonDélégué d = Afficher;
    d("Bonjour !");  // Bonjour !
}
```

#### Multicast — Plusieurs méthodes

Un délégué peut pointer vers **plusieurs méthodes** à la fois.
```csharp
MonDélégué d = Afficher;
d += Enregistrer;  // Ajouter une méthode

d("Bonjour !");    // Les deux méthodes sont appelées

d -= Afficher;     // Retirer une méthode
```

---

### 2. Les Expressions Lambda

Une **lambda** est une méthode anonyme concise, souvent utilisée avec les délégués.
```csharp
// Syntaxe : (paramètres) => expression
delegate int Operation(int a, int b);

Operation additionner = (a, b) => a + b;
Console.WriteLine(additionner(3, 4));  // 7

// Avec un bloc d'instructions
delegate void Salutation(string nom);

Salutation saluer = nom =>
{
    string msg = $"Bonjour, {nom} !";
    Console.WriteLine(msg);
};
saluer("Alice");  // Bonjour, Alice !
```

---

### 3. Les Événements

Un **événement** permet à une classe de **notifier d'autres classes** qu'il s'est passé
quelque chose. Il est basé sur un délégué.

> 💡 Un événement est un **bouton d'alarme** : quand il est déclenché, tous les abonnés sont prévenus.

#### Déclaration
```csharp
public class Minuterie
{
    // 1. Déclarer le délégué
    public delegate void SonnerieDélégué(string message);

    // 2. Déclarer l'événement
    public event SonnerieDélégué Sonnerie;

    public void Démarrer()
    {
        // 3. Déclencher l'événement
        Sonnerie?.Invoke("⏰ Temps écoulé !");
    }
}
```

#### Abonnement
```csharp
Minuterie m = new Minuterie();

m.Sonnerie += msg => Console.WriteLine($"Reçu : {msg}");

m.Démarrer();
// Reçu : ⏰ Temps écoulé !
```

---

### 4. Convention `EventHandler<TEventArgs>`

La convention standard en C# utilise `EventHandler<TEventArgs>` avec une classe
d'arguments qui hérite de `EventArgs`.
```csharp
// 1. Créer la classe d'arguments
public class TemperatureEventArgs : EventArgs
{
    public double Temperature { get; set; }
}

// 2. Déclarer l'événement
public class Capteur
{
    public event EventHandler<TemperatureEventArgs> TemperatureChangée;

    public void SetTemperature(double temp)
    {
        TemperatureChangée?.Invoke(this, new TemperatureEventArgs
        {
            Temperature = temp
        });
    }
}

// 3. S'abonner
Capteur capteur = new Capteur();

capteur.TemperatureChangée += (sender, e) =>
    Console.WriteLine($"Température : {e.Temperature}°C");

capteur.SetTemperature(25);
// Température : 25°C
```

---

### 5. Délégué vs Événement

| Caractéristique | Délégué | Événement |
|---|---|---|
| Assignation `=` depuis l'extérieur | ✅ | ❌ |
| Invocation depuis l'extérieur | ✅ | ❌ |
| `+=` / `-=` depuis l'extérieur | ✅ | ✅ |
| Usage typique | Callback | Notification |

> ⚠️ Un événement **encapsule** le délégué : seule la classe qui le déclare peut le déclencher.

---

### 6. Résumé
```
DÉLÉGUÉ
  └─ Type pointant vers une ou plusieurs méthodes
  └─ Définit une signature (paramètres + retour)
  └─ Supporte le multicast (+=  /  -=)
       │
       │ est la base de
       ▼
ÉVÉNEMENT
  └─ Délégué encapsulé dans une classe
  └─ Seule la classe propriétaire peut le déclencher
  └─ Les abonnés utilisent += et -=
```


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

## Une version améliorée pour encapsuler les détails d'implémentation

### L'interface `IObservateur`
```csharp
// Le contrat que tout observateur doit respecter
public interface IObservateur
{
    void Réagir(object sender, MesuresEventArgs e);
}
```

### `MesuresEventArgs`
```csharp
public class MesuresEventArgs : EventArgs
{
    public double Temperature { get; set; }
    public double Humidite    { get; set; }
    public double Pression    { get; set; }
}
```

### `StationMeteo` *(Le Sujet)*
```csharp
public class StationMeteo
{
    private event EventHandler<MesuresEventArgs> MesuresChangees;

    private double _temperature;
    private double _humidite;
    private double _pression;

    public void Abonner(IObservateur observateur)    => MesuresChangees += observateur.Réagir;
    public void Désabonner(IObservateur observateur) => MesuresChangees -= observateur.Réagir;

    // ✅ Un seul appel pour mettre à jour les 3 valeurs et notifier
    public void SetMesures(double temperature, double humidite, double pression)
    {
        _temperature = temperature;
        _humidite    = humidite;
        _pression    = pression;

        MesuresChangees?.Invoke(this, new MesuresEventArgs
        {
            Temperature = _temperature,
            Humidite    = _humidite,
            Pression    = _pression
        });
    }
}
```

### Les observateurs concrets
```csharp
public class AfficheurTemperature : IObservateur
{
    private string _nom;
    public AfficheurTemperature(string nom) => _nom = nom;

    public void Réagir(object sender, MesuresEventArgs e)
        => Console.WriteLine($"[{_nom}] Température : {e.Temperature}°C | Humidité : {e.Humidite}% | Pression : {e.Pression}hPa");
}

public class EnregistreurDonnées : IObservateur
{
    public void Réagir(object sender, MesuresEventArgs e)
        => Console.WriteLine($"📁 Sauvegardé — T:{e.Temperature}°C | H:{e.Humidite}% | P:{e.Pression}hPa");
}


// ✅ Déclenche une alerte si une valeur dépasse un seuil
public class AlerteClimatique : IObservateur
{
    public void Réagir(object sender, MesuresEventArgs e)
    {
        if (e.Temperature > 40)
            Console.WriteLine($"🌡 ALERTE — Température critique : {e.Temperature}°C !");
        if (e.Humidite > 90)
            Console.WriteLine($"💧 ALERTE — Humidité critique : {e.Humidite}% !");
        if (e.Pression < 950)
            Console.WriteLine($"🌪 ALERTE — Pression critique : {e.Pression}hPa !");
    }
}

```

### `Main`
```csharp
StationMeteo station = new StationMeteo();

station.Abonner(new AfficheurMesures("Écran principal"));
station.Abonner(new EnregistreurDonnées());
station.Abonner(new AlerteClimatique());

station.SetMesures(temperature: 42, humidite: 95, pression: 945);
// [Écran principal] Température : 42°C | Humidité : 95% | Pression : 945hPa
// 📁 Sauvegardé — T:42°C | H:95% | P:945hPa
// 🌡 ALERTE — Température critique : 42°C !
// 💧 ALERTE — Humidité critique : 95% !
// 🌪 ALERTE — Pression critique : 945hPa !
```

---

### Résumé des responsabilités

| Classe | Responsabilité |
|---|---|
| `IObservateur` | Définit le contrat de réaction |
| `MesuresEventArgs` | Transporte les données de l'événement |
| `StationMeteo` | Gère les abonnés et déclenche les notifications |
| `AfficheurTemperature` | Réagit en affichant la température |
| `EnregistreurDonnées` | Réagit en sauvegardant les données |
| `Main` | Crée les objets, les relie, simule les actions |

---

### Règles d'encapsulation respectées

- L'événement est `private` — on ne peut pas s'abonner directement depuis l'extérieur
- `Abonner()` prend une `IObservateur` — aucun couplage avec les classes concrètes
- `Main` ne connaît aucune méthode interne — il change juste l'état
- Ajouter un nouvel observateur ne nécessite **aucune modification** de `StationMeteo`
---

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

