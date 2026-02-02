---
title: "Programmation orientée objet - partie 2"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 10
---

## Table des matières
10. [Le mot-clé Virtual](#10-le-mot-clé-virtual)
11. [Le mot-clé Abstract](#11-le-mot-clé-abstract)
12. [Le mot-clé Sealed](#12-le-mot-clé-sealed)
13. [Les Classes Abstraites](#13-les-classes-abstraites)
14. [Les Interfaces](#14-les-interfaces)
15. [Membres Statiques](#15-membres-statiques)

---

# Cours POO C# - Partie 2 : Concepts Avancés

## 10. Le mot-clé Virtual

### 10.1 Définition

Le mot-clé `virtual` permet de déclarer une méthode qui **peut être redéfinie** (override) dans les classes dérivées. C'est la base du polymorphisme d'exécution en C#.

**Concepts clés :**
- Une méthode `virtual` **a une implémentation** dans la classe de base
- Les classes dérivées **peuvent** (mais ne sont pas obligées) la redéfinir
- Si elles ne la redéfinissent pas, elles utilisent l'implémentation de base

**Analogie :** 
C'est comme une recette de base que vous pouvez personnaliser. La recette originale existe et fonctionne, mais vous pouvez l'adapter à votre goût.

### 10.2 Syntaxe et Utilisation

```csharp
// ==========================================
// CLASSE DE BASE avec méthode virtual
// ==========================================
public class Animal
{
    protected string _nom;
    
    public Animal(string nom)
    {
        _nom = nom;
    }
    
    // ==========================================
    // MÉTHODE VIRTUAL - Peut être redéfinie
    // ==========================================
    public virtual void EmettreS on()
    {
        Console.WriteLine($"{_nom} émet un son générique.");
    }
    
    // ==========================================
    // MÉTHODE NORMALE (non-virtual) - Ne peut PAS être redéfinie polymorphiquement
    // ==========================================
    public void Dormir()
    {
        Console.WriteLine($"{_nom} dort.");
    }
}

// ==========================================
// CLASSE DÉRIVÉE - Redéfinit la méthode virtual
// ==========================================
public class Chien : Animal
{
    public Chien(string nom) : base(nom) { }
    
    // ==========================================
    // OVERRIDE - Redéfinition de la méthode virtual
    // ==========================================
    public override void EmettreSound()
    {
        Console.WriteLine($"{_nom} aboie: Wouf wouf!");
    }
}

public class Chat : Animal
{
    public Chat(string nom) : base(nom) { }
    
    public override void EmettreSound()
    {
        Console.WriteLine($"{_nom} miaule: Miaou!");
    }
}

public class Vache : Animal
{
    public Vache(string nom) : base(nom) { }
    
    public override void EmettreSound()
    {
        Console.WriteLine($"{_nom} meugle: Meuh!");
    }
}

// ==========================================
// DÉMONSTRATION DU POLYMORPHISME
// ==========================================
class Program
{
    static void Main()
    {
        // Création d'objets de types différents
        Animal animal1 = new Chien("Rex");
        Animal animal2 = new Chat("Félix");
        Animal animal3 = new Vache("Marguerite");
        Animal animal4 = new Animal("Créature");
        
        // ==========================================
        // POLYMORPHISME EN ACTION
        // Même appel de méthode, comportements différents
        // ==========================================
        animal1.EmettreSound(); // Rex aboie: Wouf wouf!
        animal2.EmettreSound(); // Félix miaule: Miaou!
        animal3.EmettreSound(); // Marguerite meugle: Meuh!
        animal4.EmettreSound(); // Créature émet un son générique.
        
        // ==========================================
        // UTILISATION PRATIQUE : Tableau polymorphe
        // ==========================================
        Animal[] animaux = new Animal[]
        {
            new Chien("Max"),
            new Chat("Minou"),
            new Vache("Bella"),
            new Chien("Rocky")
        };
        
        Console.WriteLine("\n=== Concert animalier ===");
        foreach (Animal animal in animaux)
        {
            animal.EmettreSound(); // Appelle la bonne version automatiquement
        }
    }
}
```

**Affichage :**
```
Rex aboie: Wouf wouf!
Félix miaule: Miaou!
Marguerite meugle: Meuh!
Créature émet un son générique.

=== Concert animalier ===
Max aboie: Wouf wouf!
Minou miaule: Miaou!
Bella meugle: Meuh!
Rocky aboie: Wouf wouf!
```

### 10.3 Exemple Complet : Système de Calcul de Salaire

```csharp
public class Employe
{
    public string Nom { get; set; }
    public decimal SalaireBase { get; set; }
    
    public Employe(string nom, decimal salaireBase)
    {
        Nom = nom;
        SalaireBase = salaireBase;
    }
    
    // ==========================================
    // MÉTHODE VIRTUAL - Calcul de base
    // ==========================================
    public virtual decimal CalculerSalaire()
    {
        Console.WriteLine($"[Employe] Calcul standard pour {Nom}");
        return SalaireBase;
    }
    
    public virtual void AfficherDetails()
    {
        Console.WriteLine($"\n=== {Nom} ===");
        Console.WriteLine($"Type: Employé");
        Console.WriteLine($"Salaire de base: {SalaireBase:C}");
        Console.WriteLine($"Salaire total: {CalculerSalaire():C}");
    }
}

public class Manager : Employe
{
    public decimal Prime { get; set; }
    
    public Manager(string nom, decimal salaireBase, decimal prime)
        : base(nom, salaireBase)
    {
        Prime = prime;
    }
    
    // ==========================================
    // OVERRIDE - Redéfinition pour Manager
    // ==========================================
    public override decimal CalculerSalaire()
    {
        Console.WriteLine($"[Manager] Calcul avec prime pour {Nom}");
        return SalaireBase + Prime;
    }
    
    public override void AfficherDetails()
    {
        base.AfficherDetails(); // Appelle la version de base
        Console.WriteLine($"Prime: {Prime:C}");
    }
}

public class Vendeur : Employe
{
    public decimal CommissionPourcentage { get; set; }
    public decimal VentesTotales { get; set; }
    
    public Vendeur(string nom, decimal salaireBase, decimal commission)
        : base(nom, salaireBase)
    {
        CommissionPourcentage = commission;
    }
    
    public override decimal CalculerSalaire()
    {
        Console.WriteLine($"[Vendeur] Calcul avec commission pour {Nom}");
        decimal commission = VentesTotales * (CommissionPourcentage / 100);
        return SalaireBase + commission;
    }
    
    public override void AfficherDetails()
    {
        base.AfficherDetails();
        Console.WriteLine($"Commission: {CommissionPourcentage}%");
        Console.WriteLine($"Ventes: {VentesTotales:C}");
    }
}

public class Stagiaire : Employe
{
    public int HeuresTravaillees { get; set; }
    public decimal TauxHoraire { get; set; }
    
    public Stagiaire(string nom, decimal tauxHoraire)
        : base(nom, 0) // Pas de salaire de base
    {
        TauxHoraire = tauxHoraire;
    }
    
    public override decimal CalculerSalaire()
    {
        Console.WriteLine($"[Stagiaire] Calcul horaire pour {Nom}");
        return HeuresTravaillees * TauxHoraire;
    }
    
    public override void AfficherDetails()
    {
        Console.WriteLine($"\n=== {Nom} ===");
        Console.WriteLine($"Type: Stagiaire");
        Console.WriteLine($"Taux horaire: {TauxHoraire:C}");
        Console.WriteLine($"Heures travaillées: {HeuresTravaillees}h");
        Console.WriteLine($"Salaire total: {CalculerSalaire():C}");
    }
}

// ==========================================
// UTILISATION
// ==========================================
class Program
{
    static void Main()
    {
        // ==========================================
        // Création d'employés de différents types
        // ==========================================
        Employe e1 = new Employe("Alice", 3000);
        Manager m1 = new Manager("Bob", 4000, 1000);
        Vendeur v1 = new Vendeur("Charlie", 2500, 5) { VentesTotales = 50000 };
        Stagiaire s1 = new Stagiaire("David", 15) { HeuresTravaillees = 120 };
        
        // ==========================================
        // POLYMORPHISME : Liste hétérogène
        // ==========================================
        List<Employe> employes = new List<Employe> { e1, m1, v1, s1 };
        
        Console.WriteLine("=== CALCUL DES SALAIRES ===\n");
        
        decimal masseSalariale = 0;
        foreach (Employe employe in employes)
        {
            // Appelle la bonne version de CalculerSalaire()
            // automatiquement selon le type réel
            decimal salaire = employe.CalculerSalaire();
            masseSalariale += salaire;
            
            employe.AfficherDetails();
        }
        
        Console.WriteLine($"\n=== TOTAL ===");
        Console.WriteLine($"Masse salariale totale: {masseSalariale:C}");
    }
}
```

### 10.4 Règles Importantes de `virtual` et `override`

```csharp
public class ClasseBase
{
    // ✅ Méthode virtual - Peut être redéfinie
    public virtual void Methode1() { }
    
    // ✅ Méthode normale - Ne peut pas être redéfinie polymorphiquement
    public void Methode2() { }
    
    // ✅ Propriété virtual
    public virtual string Propriete { get; set; }
}

public class ClasseDerivee : ClasseBase
{
    // ✅ OK - Override d'une méthode virtual
    public override void Methode1() { }
    
    // ❌ ERREUR - Ne peut pas override une méthode non-virtual
    // public override void Methode2() { }
    
    // ✅ OK - Override d'une propriété virtual
    public override string Propriete { get; set; }
    
    // ⚠️ ATTENTION - Masquage avec 'new' (pas polymorphique)
    public new void Methode2() 
    { 
        // Ceci n'est PAS du polymorphisme
        // C'est du "masquage" (hiding)
    }
}
```

**Différence entre `override` et `new` :**

```csharp
public class Base
{
    public virtual void Afficher()
    {
        Console.WriteLine("Base.Afficher()");
    }
}

public class Derivee1 : Base
{
    public override void Afficher() // OVERRIDE
    {
        Console.WriteLine("Derivee1.Afficher()");
    }
}

public class Derivee2 : Base
{
    public new void Afficher() // NEW (masquage)
    {
        Console.WriteLine("Derivee2.Afficher()");
    }
}

// Test
Base b1 = new Derivee1();
b1.Afficher(); // "Derivee1.Afficher()" - POLYMORPHISME ✅

Base b2 = new Derivee2();
b2.Afficher(); // "Base.Afficher()" - PAS de polymorphisme ⚠️

Derivee2 d2 = new Derivee2();
d2.Afficher(); // "Derivee2.Afficher()" - Appelle la version masquée
```

---

## 11. Le mot-clé Abstract

### 11.1 Définition

Le mot-clé `abstract` permet de déclarer :
1. **Des classes abstraites** : Classes incomplètes qui ne peuvent pas être instanciées
2. **Des méthodes abstraites** : Méthodes sans implémentation qui DOIVENT être redéfinies dans les classes dérivées

**Différence avec `virtual` :**
- `virtual` : Méthode **avec** implémentation, redéfinition **optionnelle**
- `abstract` : Méthode **sans** implémentation, redéfinition **obligatoire**

**Analogie :**
- Une classe abstraite est comme un **plan architectural incomplet**
- Elle définit la structure mais certaines parties doivent être complétées
- Vous ne pouvez pas habiter dans un plan, vous devez construire la maison complète

### 11.2 Méthodes Abstraites

```csharp
public abstract class Forme
{
    protected string _nom;
    protected string _couleur;
    
    public Forme(string nom, string couleur)
    {
        _nom = nom;
        _couleur = couleur;
    }
    
    // ==========================================
    // MÉTHODE ABSTRAITE - Pas d'implémentation
    // DOIT être redéfinie dans les classes dérivées
    // ==========================================
    public abstract double CalculerAire();
    
    public abstract double CalculerPerimetre();
    
    // ==========================================
    // MÉTHODE CONCRÈTE - A une implémentation
    // ==========================================
    public void AfficherInfos()
    {
        Console.WriteLine($"\n{_nom} ({_couleur})");
        Console.WriteLine($"Aire: {CalculerAire():F2}");
        Console.WriteLine($"Périmètre: {CalculerPerimetre():F2}");
    }
}

// ==========================================
// CLASSE DÉRIVÉE - DOIT implémenter les méthodes abstraites
// ==========================================
public class Cercle : Forme
{
    private double _rayon;
    
    public Cercle(double rayon, string couleur)
        : base("Cercle", couleur)
    {
        _rayon = rayon;
    }
    
    // ==========================================
    // IMPLÉMENTATION OBLIGATOIRE
    // ==========================================
    public override double CalculerAire()
    {
        return Math.PI * _rayon * _rayon;
    }
    
    public override double CalculerPerimetre()
    {
        return 2 * Math.PI * _rayon;
    }
}

public class Rectangle : Forme
{
    private double _longueur;
    private double _largeur;
    
    public Rectangle(double longueur, double largeur, string couleur)
        : base("Rectangle", couleur)
    {
        _longueur = longueur;
        _largeur = largeur;
    }
    
    public override double CalculerAire()
    {
        return _longueur * _largeur;
    }
    
    public override double CalculerPerimetre()
    {
        return 2 * (_longueur + _largeur);
    }
}

// ==========================================
// SI on oublie d'implémenter une méthode abstraite → ERREUR
// ==========================================
/*
public class Triangle : Forme // ❌ ERREUR DE COMPILATION
{
    // Erreur: Triangle ne redéfinit pas les méthodes abstraites
}
*/

// ==========================================
// UTILISATION
// ==========================================
class Program
{
    static void Main()
    {
        // ❌ ERREUR - Impossible d'instancier une classe abstraite
        // Forme f = new Forme("Test", "Rouge");
        
        // ✅ OK - Instanciation des classes concrètes
        Forme cercle = new Cercle(5, "Rouge");
        Forme rectangle = new Rectangle(4, 6, "Bleu");
        
        // Polymorphisme
        cercle.AfficherInfos();
        rectangle.AfficherInfos();
        
        // Liste polymorphe
        List<Forme> formes = new List<Forme>
        {
            new Cercle(3, "Vert"),
            new Rectangle(5, 2, "Jaune"),
            new Cercle(7, "Orange")
        };
        
        double aireTotal e = 0;
        foreach (Forme forme in formes)
        {
            aireTotal += forme.CalculerAire();
        }
        Console.WriteLine($"\nAire totale: {aireTotal:F2}");
    }
}
```

### 11.3 Propriétés Abstraites

Les propriétés peuvent aussi être abstraites.

```csharp
public abstract class Vehicule
{
    // ==========================================
    // PROPRIÉTÉ ABSTRAITE
    // ==========================================
    public abstract int NombreRoues { get; }
    
    public abstract string TypeCarburant { get; set; }
    
    public void AfficherInfos()
    {
        Console.WriteLine($"Véhicule à {NombreRoues} roues");
        Console.WriteLine($"Carburant: {TypeCarburant}");
    }
}

public class Voiture : Vehicule
{
    public override int NombreRoues 
    { 
        get { return 4; } 
    }
    
    private string _typeCarburant;
    public override string TypeCarburant 
    { 
        get { return _typeCarburant; }
        set { _typeCarburant = value; }
    }
    
    public Voiture()
    {
        _typeCarburant = "Essence";
    }
}

public class Moto : Vehicule
{
    public override int NombreRoues { get { return 2; } }
    
    public override string TypeCarburant { get; set; } = "Essence";
}
```

### 11.4 Combinaison de Virtual et Abstract

Une classe abstraite peut contenir un mélange de méthodes abstraites, virtuelles et concrètes.

```csharp
public abstract class Animal
{
    protected string _nom;
    
    public Animal(string nom)
    {
        _nom = nom;
    }
    
    // ==========================================
    // MÉTHODE ABSTRAITE - Implémentation obligatoire
    // ==========================================
    public abstract void EmettreSound();
    
    // ==========================================
    // MÉTHODE VIRTUELLE - Redéfinition optionnelle
    // ==========================================
    public virtual void Manger()
    {
        Console.WriteLine($"{_nom} mange.");
    }
    
    // ==========================================
    // MÉTHODE CONCRÈTE - Implémentation finale
    // ==========================================
    public void Dormir()
    {
        Console.WriteLine($"{_nom} dort.");
    }
}

public class Chien : Animal
{
    public Chien(string nom) : base(nom) { }
    
    // OBLIGATOIRE - Méthode abstraite
    public override void EmettreSound()
    {
        Console.WriteLine($"{_nom} aboie!");
    }
    
    // OPTIONNEL - Méthode virtuelle
    public override void Manger()
    {
        Console.WriteLine($"{_nom} dévore ses croquettes!");
    }
    
    // Dormir() est hérité tel quel
}
```

---

## 12. Le mot-clé Sealed

### 12.1 Définition

Le mot-clé `sealed` **empêche l'héritage** ou la **redéfinition** :
1. **Classe sealed** : Aucune classe ne peut en hériter
2. **Méthode sealed** : Aucune classe dérivée ne peut la redéfinir

**Pourquoi utiliser sealed ?**
- **Sécurité** : Empêcher les modifications non désirées
- **Performance** : Optimisations possibles par le compilateur
- **Design** : Indiquer qu'une classe est "complète" et finale

**Analogie :**
C'est comme **sceller un document** avec de la cire. Une fois scellé, il ne peut plus être modifié.

### 12.2 Classe Sealed

```csharp
// ==========================================
// CLASSE SEALED - Ne peut pas être héritée
// ==========================================
public sealed class MathUtils
{
    public static double CalculerMoyenne(params double[] nombres)
    {
        return nombres.Average();
    }
    
    public static int Max(int a, int b)
    {
        return a > b ? a : b;
    }
}

// ❌ ERREUR DE COMPILATION - Impossible d'hériter d'une classe sealed
/*
public class MesUtils : MathUtils
{
    // Erreur: cannot derive from sealed type 'MathUtils'
}
*/
```

**Exemples de classes sealed dans .NET :**
- `String` : sealed (impossible d'hériter de string)
- `Int32`, `Double`, etc. : sealed
- `DateTime` : sealed

```csharp
// ❌ Impossible
// public class MaChaine : String { }

// ✅ Utilisation normale
string texte = "Bonjour";
```

### 12.3 Méthode Sealed

Une méthode `sealed` empêche sa redéfinition dans les classes dérivées ultérieures.

**Important :** Une méthode ne peut être `sealed` que si elle `override` déjà une méthode.

```csharp
public class Animal
{
    public virtual void EmettreSound()
    {
        Console.WriteLine("Son animal");
    }
}

public class Mammifere : Animal
{
    // ==========================================
    // OVERRIDE + SEALED
    // Cette version est finale, ne peut plus être redéfinie
    // ==========================================
    public sealed override void EmettreSound()
    {
        Console.WriteLine("Son de mammifère");
    }
}

public class Chien : Mammifere
{
    // ❌ ERREUR - Ne peut pas override une méthode sealed
    /*
    public override void EmettreSound()
    {
        Console.WriteLine("Wouf!");
    }
    */
    
    // ✅ OK - Méthode différente (pas un override)
    public void Aboyer()
    {
        Console.WriteLine("Wouf!");
    }
}
```

### 12.4 Cas d'Usage de Sealed

#### Exemple 1 : Classe Utilitaire Complète

```csharp
// Classe utilitaire qui ne devrait jamais être modifiée
public sealed class ConfigurationManager
{
    private static ConfigurationManager _instance;
    private Dictionary<string, string> _settings;
    
    private ConfigurationManager()
    {
        _settings = new Dictionary<string, string>();
        ChargerConfiguration();
    }
    
    public static ConfigurationManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ConfigurationManager();
            return _instance;
        }
    }
    
    private void ChargerConfiguration()
    {
        // Chargement de la configuration
    }
    
    public string ObtenirValeur(string cle)
    {
        return _settings.ContainsKey(cle) ? _settings[cle] : null;
    }
}

// Impossible d'hériter pour modifier le comportement
```

#### Exemple 2 : Méthode Sealed pour la Sécurité

```csharp
public class SystemeSécurité
{
    public virtual bool VerifierAcces(string utilisateur)
    {
        Console.WriteLine("Vérification de base");
        return true;
    }
}

public class SystemeAvance : SystemeSécurité
{
    // Version finale de la vérification - ne doit plus être modifiée
    public sealed override bool VerifierAcces(string utilisateur)
    {
        Console.WriteLine("Vérification avancée (FINALE)");
        // Logique critique de sécurité
        bool accesBase = base.VerifierAcces(utilisateur);
        bool verification2FA = Verifier2FA(utilisateur);
        bool verificationBiometrique = VerifierBiometrie(utilisateur);
        
        return accesBase && verification2FA && verificationBiometrique;
    }
    
    private bool Verifier2FA(string utilisateur) { return true; }
    private bool VerifierBiometrie(string utilisateur) { return true; }
}

// Aucune classe dérivée ne peut affaiblir la sécurité
public class SystemeTresAvance : SystemeAvance
{
    // ❌ Ne peut pas override VerifierAcces
    // La sécurité est garantie
}
```

---

## 13. Les Classes Abstraites

### 13.1 Définition Complète

Une **classe abstraite** est une classe déclarée avec le mot-clé `abstract` qui :
- **Ne peut pas être instanciée** directement
- Peut contenir des **méthodes abstraites** (sans implémentation) ET des **méthodes concrètes** (avec implémentation)
- Peut avoir des **constructeurs** (appelés par les classes dérivées)
- Peut avoir des **attributs**, **propriétés**, etc.
- Sert de **modèle** ou de **base** pour d'autres classes

**Quand utiliser une classe abstraite ?**
- Quand vous voulez définir un **comportement commun** pour un groupe de classes
- Quand certaines méthodes **doivent** être implémentées différemment par chaque classe dérivée
- Quand vous voulez **partager du code** entre classes similaires
- Quand la classe représente un **concept abstrait** qui ne devrait pas exister seul

### 13.2 Anatomie Complète d'une Classe Abstraite

```csharp
public abstract class Vehicule
{
    // ==========================================
    // 1. ATTRIBUTS (comme une classe normale)
    // ==========================================
    protected string _marque;
    protected string _modele;
    private int _annee;
    protected double _kilometrage;
    
    // ==========================================
    // 2. PROPRIÉTÉS
    // ==========================================
    public string Marque { get { return _marque; } }
    public string Modele { get { return _modele; } }
    public int Annee { get { return _annee; } }
    public double Kilometrage { get { return _kilometrage; } }
    
    // ==========================================
    // 3. CONSTRUCTEUR (Oui, les classes abstraites peuvent en avoir!)
    // ==========================================
    protected Vehicule(string marque, string modele, int annee)
    {
        _marque = marque;
        _modele = modele;
        _annee = annee;
        _kilometrage = 0;
        Console.WriteLine($"Constructeur de Vehicule appelé pour {marque} {modele}");
    }
    
    // ==========================================
    // 4. MÉTHODES ABSTRAITES - Implémentation obligatoire
    // ==========================================
    public abstract void Demarrer();
    
    public abstract double CalculerConsommation(double distance);
    
    public abstract string ObtenirTypeCarburant();
    
    // ==========================================
    // 5. MÉTHODES VIRTUELLES - Redéfinition optionnelle
    // ==========================================
    public virtual void Accelerer(int vitesse)
    {
        Console.WriteLine($"Le véhicule accélère à {vitesse} km/h");
    }
    
    public virtual void Klaxonner()
    {
        Console.WriteLine("Beep beep!");
    }
    
    // ==========================================
    // 6. MÉTHODES CONCRÈTES - Héritées telles quelles
    // ==========================================
    public void Rouler(double distance)
    {
        _kilometrage += distance;
        double consommation = CalculerConsommation(distance); // Appelle la méthode abstraite
        Console.WriteLine($"Parcouru {distance} km. Consommation: {consommation:F2}L");
        Console.WriteLine($"Kilométrage total: {_kilometrage} km");
    }
    
    public void AfficherInfos()
    {
        Console.WriteLine($"\n=== {_marque} {_modele} ({_annee}) ===");
        Console.WriteLine($"Type de carburant: {ObtenirTypeCarburant()}");
        Console.WriteLine($"Kilométrage: {_kilometrage} km");
    }
    
    // ==========================================
    // 7. PROPRIÉTÉS ABSTRAITES
    // ==========================================
    public abstract int NombreRoues { get; }
}
```

### 13.3 Implémentation de la Classe Abstraite

```csharp
// ==========================================
// CLASSE CONCRÈTE 1 : Voiture
// ==========================================
public class Voiture : Vehicule
{
    private string _typeCarburant;
    private double _consommationAu100;
    private int _nombrePortes;
    
    public Voiture(string marque, string modele, int annee, int portes, string carburant, double consommation)
        : base(marque, modele, annee) // Appelle le constructeur abstrait
    {
        _nombrePortes = portes;
        _typeCarburant = carburant;
        _consommationAu100 = consommation;
    }
    
    // Implémentation obligatoire des méthodes abstraites
    public override void Demarrer()
    {
        Console.WriteLine($"La voiture {_marque} {_modele} démarre avec la clé.");
    }
    
    public override double CalculerConsommation(double distance)
    {
        return (distance / 100) * _consommationAu100;
    }
    
    public override string ObtenirTypeCarburant()
    {
        return _typeCarburant;
    }
    
    // Implémentation de la propriété abstraite
    public override int NombreRoues 
    { 
        get { return 4; } 
    }
    
    // Redéfinition optionnelle d'une méthode virtuelle
    public override void Klaxonner()
    {
        Console.WriteLine("La voiture klaxonne: BEEP BEEP!");
    }
    
    // Nouvelle méthode spécifique
    public void OuvrirCoffre()
    {
        Console.WriteLine("Coffre ouvert");
    }
}

// ==========================================
// CLASSE CONCRÈTE 2 : Moto
// ==========================================
public class Moto : Vehicule
{
    private bool _aSidecar;
    private double _consommationAu100;
    
    public Moto(string marque, string modele, int annee, bool sidecar, double consommation)
        : base(marque, modele, annee)
    {
        _aSidecar = sidecar;
        _consommationAu100 = consommation;
    }
    
    public override void Demarrer()
    {
        Console.WriteLine($"La moto {_marque} {_modele} démarre avec le bouton start.");
    }
    
    public override double CalculerConsommation(double distance)
    {
        double consommation = (distance / 100) * _consommationAu100;
        if (_aSidecar)
            consommation *= 1.2; // 20% de plus avec sidecar
        return consommation;
    }
    
    public override string ObtenirTypeCarburant()
    {
        return "Essence";
    }
    
    public override int NombreRoues 
    { 
        get { return _aSidecar ? 3 : 2; } 
    }
    
    // Utilise la méthode virtuelle héritée (pas de redéfinition)
    // Donc Klaxonner() sera "Beep beep!" de la classe de base
}

// ==========================================
// CLASSE CONCRÈTE 3 : Camion
// ==========================================
public class Camion : Vehicule
{
    private double _capaciteChargeTonnes;
    private double _consommationAu100;
    private double _chargeActuelle;
    
    public Camion(string marque, string modele, int annee, double capacite, double consommation)
        : base(marque, modele, annee)
    {
        _capaciteChargeTonnes = capacite;
        _consommationAu100 = consommation;
        _chargeActuelle = 0;
    }
    
    public override void Demarrer()
    {
        Console.WriteLine($"Le camion {_marque} {_modele} démarre avec un vrombissement.");
    }
    
    public override double CalculerConsommation(double distance)
    {
        double baseConsommation = (distance / 100) * _consommationAu100;
        // Consommation augmente avec la charge
        double facteurCharge = 1 + (_chargeActuelle / _capaciteChargeTonnes) * 0.5;
        return baseConsommation * facteurCharge;
    }
    
    public override string ObtenirTypeCarburant()
    {
        return "Diesel";
    }
    
    public override int NombreRoues { get { return 18; } }
    
    public void Charger(double poids)
    {
        if (_chargeActuelle + poids <= _capaciteChargeTonnes)
        {
            _chargeActuelle += poids;
            Console.WriteLine($"Chargement de {poids}t. Charge totale: {_chargeActuelle}t");
        }
        else
        {
            Console.WriteLine($"Impossible! Capacité dépassée.");
        }
    }
}
```

### 13.4 Utilisation Polymorphe

```csharp
class Program
{
    static void Main()
    {
        // ❌ IMPOSSIBLE - Classe abstraite
        // Vehicule v = new Vehicule("Test", "Test", 2020);
        
        // ✅ OK - Classes concrètes
        Vehicule voiture = new Voiture("Toyota", "Camry", 2023, 4, "Essence", 7.5);
        Vehicule moto = new Moto("Harley", "Davidson", 2022, false, 4.5);
        Vehicule camion = new Camion("Volvo", "FH16", 2021, 25, 30);
        
        // ==========================================
        // POLYMORPHISME EN ACTION
        // ==========================================
        List<Vehicule> parc = new List<Vehicule> { voiture, moto, camion };
        
        Console.WriteLine("=== DÉMARRAGE DE TOUS LES VÉHICULES ===\n");
        foreach (Vehicule vehicule in parc)
        {
            vehicule.Demarrer(); // Appelle la bonne version
        }
        
        Console.WriteLine("\n=== TRAJET DE 100 KM ===\n");
        foreach (Vehicule vehicule in parc)
        {
            vehicule.Rouler(100);
            vehicule.AfficherInfos();
        }
        
        // ==========================================
        // UTILISATION SPÉCIFIQUE
        // ==========================================
        if (camion is Camion c)
        {
            c.Charger(10);
            c.Rouler(50); // Consommation affectée par la charge
        }
    }
}
```

### 13.5 Exemple Complet : Système de Paiement

```csharp
// ==========================================
// CLASSE ABSTRAITE : Moyen de Paiement
// ==========================================
public abstract class MoyenPaiement
{
    protected string _titulaire;
    protected DateTime _dateTransaction;
    
    public string Titulaire { get { return _titulaire; } }
    public DateTime DateTransaction { get { return _dateTransaction; } }
    
    protected MoyenPaiement(string titulaire)
    {
        _titulaire = titulaire;
    }
    
    // Méthodes abstraites
    public abstract bool Payer(decimal montant);
    public abstract bool Verifier();
    public abstract string ObtenirType();
    
    // Méthode virtuelle
    public virtual void AfficherRecu(decimal montant)
    {
        Console.WriteLine($"\n====== REÇU DE PAIEMENT ======");
        Console.WriteLine($"Titulaire: {_titulaire}");
        Console.WriteLine($"Type: {ObtenirType()}");
        Console.WriteLine($"Montant: {montant:C}");
        Console.WriteLine($"Date: {_dateTransaction:F}");
        Console.WriteLine($"==============================\n");
    }
    
    // Méthode concrète
    protected void EnregistrerTransaction()
    {
        _dateTransaction = DateTime.Now;
        Console.WriteLine($"[LOG] Transaction enregistrée pour {_titulaire}");
    }
}

// Classes concrètes...
// (voir continuation dans le message suivant)
```

```csharp
public class CarteCredit : MoyenPaiement
{
    private string _numero;
    private DateTime _dateExpiration;
    private decimal _limiteCredit;
    private decimal _soldeUtilise;
    
    public CarteCredit(string titulaire, string numero, DateTime expiration, decimal limite)
        : base(titulaire)
    {
        _numero = numero;
        _dateExpiration = expiration;
        _limiteCredit = limite;
        _soldeUtilise = 0;
    }
    
    public override bool Verifier()
    {
        if (_dateExpiration < DateTime.Now)
        {
            Console.WriteLine("❌ Carte expirée");
            return false;
        }
        return true;
    }
    
    public override bool Payer(decimal montant)
    {
        if (!Verifier())
            return false;
        
        if (_soldeUtilise + montant > _limiteCredit)
        {
            Console.WriteLine($"❌ Limite de crédit dépassée ({_limiteCredit:C})");
            return false;
        }
        
        _soldeUtilise += montant;
        EnregistrerTransaction();
        Console.WriteLine($"✅ Paiement de {montant:C} effectué par carte de crédit");
        Console.WriteLine($"   Solde disponible: {_limiteCredit - _soldeUtilise:C}");
        return true;
    }
    
    public override string ObtenirType()
    {
        return "Carte de Crédit";
    }
}

public class CompteBancaire : MoyenPaiement
{
    private string _numeroCompte;
    private decimal _solde;
    
    public CompteBancaire(string titulaire, string numero, decimal soldeInitial)
        : base(titulaire)
    {
        _numeroCompte = numero;
        _solde = soldeInitial;
    }
    
    public override bool Verifier()
    {
        return true; // Toujours valide
    }
    
    public override bool Payer(decimal montant)
    {
        if (_solde < montant)
        {
            Console.WriteLine($"❌ Solde insuffisant (Disponible: {_solde:C})");
            return false;
        }
        
        _solde -= montant;
        EnregistrerTransaction();
        Console.WriteLine($"✅ Paiement de {montant:C} effectué par compte bancaire");
        Console.WriteLine($"   Nouveau solde: {_solde:C}");
        return true;
    }
    
    public override string ObtenirType()
    {
        return "Compte Bancaire";
    }
}

// Utilisation
List<MoyenPaiement> moyensPaiement = new List<MoyenPaiement>
{
    new CarteCredit("Alice Martin", "1234-5678-9012-3456", DateTime.Now.AddYears(2), 5000),
    new CompteBancaire("Bob Gagnon", "CA123456", 2000)
};

foreach (var moyen in moyensPaiement)
{
    if (moyen.Payer(150))
    {
        moyen.AfficherRecu(150);
    }
}
```

---

## 14. Les Interfaces

### 14.1 Définition Approfondie

Une **interface** est un **contrat** qui définit un ensemble de membres (méthodes, propriétés, événements) que les classes doivent implémenter, **sans fournir d'implémentation**.

**Différences clés : Interface vs Classe Abstraite**

| Aspect | Interface | Classe Abstraite |
|--------|-----------|------------------|
| Implémentation | Aucune (contrat pur) | Peut contenir du code |
| Héritage multiple | ✅ Oui | ❌ Non |
| Constructeurs | ❌ Non | ✅ Oui |
| Champs | ❌ Non | ✅ Oui |
| Modificateurs d'accès | Tous public | Variés (private, protected, etc.) |
| But | Définir un comportement | Partager du code commun |

**Quand utiliser une interface ?**
- Définir un **comportement** commun à des classes sans relation hiérarchique
- Permettre l'**héritage multiple de comportements**
- Créer des **contrats** que différentes classes doivent respecter
- Favoriser le **couplage faible** dans votre architecture

**Analogie :**
Une interface est comme un **certificat de compétence** :
- Un pilote peut avoir : certificat voiture, certificat moto, certificat avion
- Chaque certificat garantit certaines compétences
- Différentes personnes peuvent avoir différentes combinaisons de certificats

### 14.2 Déclaration d'une Interface

```csharp
// ==========================================
// CONVENTION : Préfixe 'I' pour les interfaces
// ==========================================
public interface IVolant
{
    // ==========================================
    // MÉTHODES (pas d'implémentation)
    // ==========================================
    void Voler();
    void Atterrir();
    
    // ==========================================
    // PROPRIÉTÉS (seulement les signatures)
    // ==========================================
    double AltitudeMaximale { get; }
    double Vitesse { get; set; }
    
    // ==========================================
    // TOUS les membres sont PUBLIC par défaut
    // Pas besoin de spécifier 'public'
    // ==========================================
}

public interface INageant
{
    void Nager();
    void Plonger(double profondeur);
    double ProfondeurMaximale { get; }
}

public interface IMarchant
{
    void Marcher();
    void Courir();
    int VitesseMarche { get; }
}
```

### 14.3 Implémentation d'Interfaces

```csharp
// ==========================================
// Une classe peut implémenter PLUSIEURS interfaces
// ==========================================
public class Canard : IVolant, INageant, IMarchant
{
    private double _altitudeActuelle;
    private double _profondeurActuelle;
    
    // ==========================================
    // Implémentation de IVolant
    // ==========================================
    public double AltitudeMaximale { get { return 1000; } }
    public double Vitesse { get; set; }
    
    public void Voler()
    {
        _altitudeActuelle = 100;
        Console.WriteLine($"Le canard vole à {_altitudeActuelle}m");
    }
    
    public void Atterrir()
    {
        _altitudeActuelle = 0;
        Console.WriteLine("Le canard atterrit");
    }
    
    // ==========================================
    // Implémentation de INageant
    // ==========================================
    public double ProfondeurMaximale { get { return 5; } }
    
    public void Nager()
    {
        Console.WriteLine("Le canard nage à la surface");
    }
    
    public void Plonger(double profondeur)
    {
        if (profondeur <= ProfondeurMaximale)
        {
            _profondeurActuelle = profondeur;
            Console.WriteLine($"Le canard plonge à {profondeur}m");
        }
        else
        {
            Console.WriteLine("Trop profond!");
        }
    }
    
    // ==========================================
    // Implémentation de IMarchant
    // ==========================================
    public int VitesseMarche { get { return 5; } }
    
    public void Marcher()
    {
        Console.WriteLine("Le canard se dandine");
    }
    
    public void Courir()
    {
        Console.WriteLine("Le canard court maladroitement");
    }
}

// ==========================================
// Autre classe avec un sous-ensemble d'interfaces
// ==========================================
public class Avion : IVolant
{
    public double AltitudeMaximale { get { return 12000; } }
    public double Vitesse { get; set; }
    
    public void Voler()
    {
        Console.WriteLine($"L'avion vole à {Vitesse} km/h");
    }
    
    public void Atterrir()
    {
        Console.WriteLine("L'avion atterrit sur la piste");
    }
}

public class Poisson : INageant
{
    public double ProfondeurMaximale { get { return 500; } }
    
    public void Nager()
    {
        Console.WriteLine("Le poisson nage gracieusement");
    }
    
    public void Plonger(double profondeur)
    {
        Console.WriteLine($"Le poisson plonge à {profondeur}m");
    }
}
```

### 14.4 Polymorphisme avec Interfaces

```csharp
class Program
{
    static void Main()
    {
        // ==========================================
        // Collections polymorphes basées sur les interfaces
        // ==========================================
        
        // Tous les êtres volants
        List<IVolant> volants = new List<IVolant>
        {
            new Canard(),
            new Avion { Vitesse = 800 },
            new Canard()
        };
        
        Console.WriteLine("=== DÉCOLLAGE ===");
        foreach (IVolant volant in volants)
        {
            volant.Voler(); // Polymorphisme!
        }
        
        // Tous les êtres nageants
        List<INageant> nageants = new List<INageant>
        {
            new Canard(),
            new Poisson()
        };
        
        Console.WriteLine("\n=== PLONGÉE ===");
        foreach (INageant nageant in nageants)
        {
            nageant.Nager();
            nageant.Plonger(3);
        }
        
        // ==========================================
        // Le canard peut être utilisé comme 3 types différents
        // ==========================================
        Canard donald = new Canard();
        
        IVolant v = donald;      // Référence comme IVolant
        INageant n = donald;     // Référence comme INageant
        IMarchant m = donald;    // Référence comme IMarchant
        
        v.Voler();
        n.Nager();
        m.Marcher();
    }
}
```

### 14.5 Vérification de Type avec Interfaces

```csharp
public void TraiterAnimal(object animal)
{
    // ==========================================
    // Test avec 'is'
    // ==========================================
    if (animal is IVolant)
    {
        Console.WriteLine("Cet animal peut voler!");
    }
    
    if (animal is INageant)
    {
        Console.WriteLine("Cet animal peut nager!");
    }
    
    // ==========================================
    // Cast avec 'as'
    // ==========================================
    IVolant volant = animal as IVolant;
    if (volant != null)
    {
        volant.Voler();
    }
    
    // ==========================================
    // Pattern matching (C# 7+)
    // ==========================================
    if (animal is IVolant v)
    {
        Console.WriteLine($"Altitude max: {v.AltitudeMaximale}m");
        v.Voler();
    }
}
```

### 14.6 Implémentation Explicite d'Interface

Utilisée pour **résoudre les conflits** quand deux interfaces ont des membres avec le même nom.

```csharp
public interface IAnimal
{
    void Manger();
    string Nom { get; }
}

public interface IRobot
{
    void Manger(); // Même nom!
    string Nom { get; }
}

public class CyberChien : IAnimal, IRobot
{
    private string _nom;
    
    public CyberChien(string nom)
    {
        _nom = nom;
    }
    
    // ==========================================
    // IMPLÉMENTATION EXPLICITE pour IAnimal
    // ==========================================
    void IAnimal.Manger()
    {
        Console.WriteLine($"{_nom} mange de la nourriture organique");
    }
    
    string IAnimal.Nom
    {
        get { return $"{_nom} (animal)"; }
    }
    
    // ==========================================
    // IMPLÉMENTATION EXPLICITE pour IRobot
    // ==========================================
    void IRobot.Manger()
    {
        Console.WriteLine($"{_nom} recharge ses batteries");
    }
    
    string IRobot.Nom
    {
        get { return $"{_nom} (robot)"; }
    }
    
    // ==========================================
    // Méthode publique normale
    // ==========================================
    public void SeReposer()
    {
        Console.WriteLine($"{_nom} se met en veille");
    }
}

// Utilisation
CyberChien cyber = new CyberChien("RoboDog");

// ❌ ERREUR - Ambiguïté
// cyber.Manger();

// ✅ OK - Cast explicite
IAnimal animal = cyber;
animal.Manger();           // Nourriture organique
Console.WriteLine(animal.Nom);

IRobot robot = cyber;
robot.Manger();            // Recharge batteries
Console.WriteLine(robot.Nom);

cyber.SeReposer();         // ✅ OK - Méthode publique
```

### 14.7 Exemple Complet : Système de Notifications

```csharp
// ==========================================
// INTERFACES
// ==========================================
public interface INotifiable
{
    void EnvoyerNotification(string message);
    bool EstActif { get; set; }
}

public interface IConfigurable
{
    void Configurer(Dictionary<string, string> parametres);
    Dictionary<string, string> ObtenirConfiguration();
}

public interface IPrioritaire
{
    int Priorite { get; set; }
}

// ==========================================
// CLASSES D'IMPLÉMENTATION
// ==========================================
public class NotificationEmail : INotifiable, IConfigurable
{
    public bool EstActif { get; set; } = true;
    public string AdresseEmail { get; private set; }
    public string ServeurSMTP { get; private set; }
    
    public void EnvoyerNotification(string message)
    {
        if (EstActif)
        {
            Console.WriteLine($"📧 [EMAIL] À: {AdresseEmail}");
            Console.WriteLine($"   Message: {message}");
            Console.WriteLine($"   Via: {ServeurSMTP}\n");
        }
    }
    
    public void Configurer(Dictionary<string, string> parametres)
    {
        if (parametres.ContainsKey("email"))
            AdresseEmail = parametres["email"];
        if (parametres.ContainsKey("smtp"))
            ServeurSMTP = parametres["smtp"];
    }
    
    public Dictionary<string, string> ObtenirConfiguration()
    {
        return new Dictionary<string, string>
        {
            { "email", AdresseEmail },
            { "smtp", ServeurSMTP }
        };
    }
}

public class NotificationSMS : INotifiable, IConfigurable, IPrioritaire
{
    public bool EstActif { get; set; } = true;
    public int Priorite { get; set; } = 1;
    public string NumeroTelephone { get; private set; }
    
    public void EnvoyerNotification(string message)
    {
        if (EstActif)
        {
            Console.WriteLine($"📱 [SMS] Au: {NumeroTelephone}");
            Console.WriteLine($"   Message: {message}");
            Console.WriteLine($"   Priorité: {Priorite}\n");
        }
    }
    
    public void Configurer(Dictionary<string, string> parametres)
    {
        if (parametres.ContainsKey("telephone"))
            NumeroTelephone = parametres["telephone"];
    }
    
    public Dictionary<string, string> ObtenirConfiguration()
    {
        return new Dictionary<string, string>
        {
            { "telephone", NumeroTelephone }
        };
    }
}

public class NotificationPush : INotifiable, IPrioritaire
{
    public bool EstActif { get; set; } = true;
    public int Priorite { get; set; } = 2;
    public string DeviceId { get; set; }
    
    public void EnvoyerNotification(string message)
    {
        if (EstActif)
        {
            Console.WriteLine($"🔔 [PUSH] Device: {DeviceId}");
            Console.WriteLine($"   Message: {message}");
            Console.WriteLine($"   Priorité: {Priorite}\n");
        }
    }
}

// ==========================================
// GESTIONNAIRE
// ==========================================
public class GestionnaireNotifications
{
    private List<INotifiable> _canaux = new List<INotifiable>();
    
    public void AjouterCanal(INotifiable canal)
    {
        _canaux.Add(canal);
        Console.WriteLine($"✅ Canal ajouté: {canal.GetType().Name}");
    }
    
    public void EnvoyerATous(string message)
    {
        Console.WriteLine($"\n{'='.ToString().PadLeft(50, '=')}");
        Console.WriteLine($"ENVOI À TOUS LES CANAUX");
        Console.WriteLine($"{'='.ToString().PadLeft(50, '=')}\n");
        
        foreach (INotifiable canal in _canaux)
        {
            canal.EnvoyerNotification(message);
        }
    }
    
    public void EnvoyerParPriorite(string message, int prioriteMin)
    {
        Console.WriteLine($"\n{'='.ToString().PadLeft(50, '=')}");
        Console.WriteLine($"ENVOI PRIORITAIRE (>= {prioriteMin})");
        Console.WriteLine($"{'='.ToString().PadLeft(50, '=')}\n");
        
        foreach (INotifiable canal in _canaux)
        {
            // Vérifie si le canal supporte IPrioritaire
            if (canal is IPrioritaire prioritaire)
            {
                if (prioritaire.Priorite >= prioriteMin)
                {
                    canal.EnvoyerNotification(message);
                }
            }
        }
    }
    
    public void ConfigurerCanaux()
    {
        foreach (INotifiable canal in _canaux)
        {
            if (canal is IConfigurable configurable)
            {
                Console.WriteLine($"\nConfiguration de {canal.GetType().Name}:");
                var config = configurable.ObtenirConfiguration();
                foreach (var param in config)
                {
                    Console.WriteLine($"  {param.Key}: {param.Value}");
                }
            }
        }
    }
}

// ==========================================
// UTILISATION
// ==========================================
class Program
{
    static void Main()
    {
        GestionnaireNotifications gestionnaire = new GestionnaireNotifications();
        
        // Création et configuration des canaux
        var email = new NotificationEmail();
        email.Configurer(new Dictionary<string, string>
        {
            { "email", "user@example.com" },
            { "smtp", "smtp.example.com" }
        });
        
        var sms = new NotificationSMS { Priorite = 3 };
        sms.Configurer(new Dictionary<string, string>
        {
            { "telephone", "+1-514-555-0123" }
        });
        
        var push = new NotificationPush 
        { 
            DeviceId = "ABC123",
            Priorite = 2
        };
        
        // Ajout des canaux
        gestionnaire.AjouterCanal(email);
        gestionnaire.AjouterCanal(sms);
        gestionnaire.AjouterCanal(push);
        
        // Envoi de notifications
        gestionnaire.EnvoyerATous("Bienvenue dans le système!");
        gestionnaire.EnvoyerParPriorite("ALERTE: Activité suspecte détectée!", 2);
        
        // Affichage de la configuration
        gestionnaire.ConfigurerCanaux();
    }
}
```

### 14.8 Interfaces vs Classes Abstraites : Quand Utiliser Quoi ?

**Utilisez une INTERFACE quand :**
- Vous définissez un **comportement** que des classes sans relation peuvent partager
- Vous voulez permettre l'**héritage multiple** de comportements
- Vous créez un **plugin system** ou une architecture découplée
- Les implémentations seront très **différentes**

**Exemples d'interfaces :** `IComparable`, `IDisposable`, `IEnumerable`

**Utilisez une CLASSE ABSTRAITE quand :**
- Vous voulez **partager du code** entre classes liées
- Vous avez une hiérarchie **"est-un"** claire
- Vous voulez fournir une **implémentation par défaut**
- Les classes dérivées ont beaucoup en commun

**Exemples :** `Stream`, `DbConnection`, `Control` (UI)

**Exemple combiné :**
```csharp
// Interface pour le comportement
public interface IPayable
{
    bool EffectuerPaiement(decimal montant);
}

// Classe abstraite pour le code commun
public abstract class MoyenPaiement : IPayable
{
    protected string _titulaire;
    
    protected MoyenPaiement(string titulaire)
    {
        _titulaire = titulaire;
    }
    
    // Implémentation commune
    public void AfficherTitulaire()
    {
        Console.WriteLine($"Titulaire: {_titulaire}");
    }
    
    // Implémentation de l'interface (peut être virtual ou abstract)
    public abstract bool EffectuerPaiement(decimal montant);
}

public class CarteCredit : MoyenPaiement
{
    public CarteCredit(string titulaire) : base(titulaire) { }
    
    public override bool EffectuerPaiement(decimal montant)
    {
        Console.WriteLine($"Paiement de {montant:C} par carte");
        return true;
    }
}
```

---

## 15. Membres Statiques

### 15.1 Définition

Les membres **statiques** (attributs, méthodes, propriétés) appartiennent à la **classe elle-même** plutôt qu'aux instances individuelles de la classe.

**Analogie :**
- Membres d'instance = Caractéristiques **personnelles** (votre âge, votre nom)
- Membres statiques = Caractéristiques **partagées** (le nombre total d'êtres humains sur Terre)

**Caractéristiques :**
- Un seul exemplaire existe pour toute la classe
- Partagé entre toutes les instances
- Accessible via le nom de la classe (pas via une instance)
- Existe même sans aucune instance créée

### 15.2 Attributs Statiques

```csharp
public class Compteur
{
    // ==========================================
    // ATTRIBUT STATIQUE - Partagé par toutes les instances
    // ==========================================
    private static int _nombreInstances = 0;
    
    // ==========================================
    // ATTRIBUT D'INSTANCE - Unique pour chaque objet
    // ==========================================
    private int _id;
    
    public int Id { get { return _id; } }
    
    // ==========================================
    // PROPRIÉTÉ STATIQUE
    // ==========================================
    public static int NombreInstances
    {
        get { return _nombreInstances; }
    }
    
    // ==========================================
    // CONSTRUCTEUR
    // ==========================================
    public Compteur()
    {
        _nombreInstances++;      // Modifie la variable STATIQUE
        _id = _nombreInstances;  // Assigne un ID unique basé sur le compteur
        Console.WriteLine($"Instance #{_id} créée. Total: {_nombreInstances}");
    }
}

// ==========================================
// UTILISATION
// ==========================================
class Program
{
    static void Main()
    {
        Console.WriteLine($"Instances au départ: {Compteur.NombreInstances}"); // 0
        
        Compteur c1 = new Compteur(); // Instance #1 créée. Total: 1
        Compteur c2 = new Compteur(); // Instance #2 créée. Total: 2
        Compteur c3 = new Compteur(); // Instance #3 créée. Total: 3
        
        Console.WriteLine($"\nTotal d'instances: {Compteur.NombreInstances}"); // 3
        Console.WriteLine($"ID de c1: {c1.Id}"); // 1
        Console.WriteLine($"ID de c2: {c2.Id}"); // 2
        Console.WriteLine($"ID de c3: {c3.Id}"); // 3
        
        // ==========================================
        // Le compteur est PARTAGÉ
        // ==========================================
        Compteur c4 = new Compteur(); // Instance #4 créée. Total: 4
        Console.WriteLine($"\nTotal après c4: {Compteur.NombreInstances}"); // 4
    }
}
```

### 15.3 Méthodes Statiques

```csharp
public class CalculatriceMath
{
    // ==========================================
    // MÉTHODES STATIQUES - Pas besoin d'instance
    // ==========================================
    public static double CalculerAireRectangle(double longueur, double largeur)
    {
        return longueur * largeur;
    }
    
    public static double CalculerAireCercle(double rayon)
    {
        return Math.PI * rayon * rayon;
    }
    
    public static int CalculerFactorielle(int n)
    {
        if (n <= 1) return 1;
        return n * CalculerFactorielle(n - 1);
    }
    
    public static bool EstPremier(int nombre)
    {
        if (nombre < 2) return false;
        for (int i = 2; i <= Math.Sqrt(nombre); i++)
        {
            if (nombre % i == 0) return false;
        }
        return true;
    }
}

// Utilisation - SANS créer d'objet
double aire = CalculatriceMath.CalculerAireRectangle(5, 3); // 15
double cercle = CalculatriceMath.CalculerAireCercle(4);     // ~50.27
int fact = CalculatriceMath.CalculerFactorielle(5);         // 120
bool premier = CalculatriceMath.EstPremier(17);             // true
```

### 15.4 Classes Statiques

Une classe **entièrement statique** ne peut contenir que des membres statiques et ne peut pas être instanciée.

```csharp
// ==========================================
// CLASSE STATIQUE - Ne peut pas être instanciée
// ==========================================
public static class Convertisseur
{
    // Toutes les méthodes doivent être statiques
    
    public static double CelsiusVersFahrenheit(double celsius)
    {
        return (celsius * 9 / 5) + 32;
    }
    
    public static double FahrenheitVersCelsius(double fahrenheit)
    {
        return (fahrenheit - 32) * 5 / 9;
    }
    
    public static double KilometresVersMiles(double km)
    {
        return km * 0.621371;
    }
    
    public static double MilesVersKilometres(double miles)
    {
        return miles / 0.621371;
    }
}

// Utilisation
double fahrenheit = Convertisseur.CelsiusVersFahrenheit(25);    // 77
double miles = Convertisseur.KilometresVersMiles(100);          // 62.14

// ❌ ERREUR - Impossible d'instancier
// Convertisseur conv = new Convertisseur();
```

**Exemples de classes statiques dans .NET :**
- `Console`
- `Math`
- `File`
- `Directory`
- `Environment`

### 15.5 Constructeur Statique

Un **constructeur statique** est exécuté une seule fois, avant la première utilisation de la classe.

```csharp
public class Configuration
{
    public static string CheminFichier { get; private set; }
    public static DateTime DateInitialisation { get; private set; }
    public static Dictionary<string, string> Parametres { get; private set; }
    
    // ==========================================
    // CONSTRUCTEUR STATIQUE
    // Appelé automatiquement avant la première utilisation
    // ==========================================
    static Configuration()
    {
        Console.WriteLine("Initialisation de la configuration...");
        
        CheminFichier = "config.json";
        DateInitialisation = DateTime.Now;
        Parametres = new Dictionary<string, string>
        {
            { "version", "1.0" },
            { "langue", "fr" }
        };
        
        Console.WriteLine($"Configuration initialisée à {DateInitialisation}");
    }
    
    public static void AfficherConfiguration()
    {
        Console.WriteLine($"\nConfiguration:");
        Console.WriteLine($"  Fichier: {CheminFichier}");
        Console.WriteLine($"  Initialisée: {DateInitialisation}");
        foreach (var param in Parametres)
        {
            Console.WriteLine($"  {param.Key}: {param.Value}");
        }
    }
}

// Utilisation
class Program
{
    static void Main()
    {
        Console.WriteLine("Début du programme\n");
        
        // Le constructeur statique est appelé ici (première utilisation)
        Configuration.AfficherConfiguration();
        
        // Deuxième utilisation - constructeur statique PAS rappelé
        Configuration.AfficherConfiguration();
    }
}
```

**Affichage :**
```
Début du programme

Initialisation de la configuration...
Configuration initialisée à 01/02/2026 10:30:00

Configuration:
  Fichier: config.json
  Initialisée: 01/02/2026 10:30:00
  version: 1.0
  langue: fr

Configuration:
  Fichier: config.json
  Initialisée: 01/02/2026 10:30:00
  version: 1.0
  langue: fr
```

### 15.6 Exemple Complet : Gestionnaire de Base de Données

```csharp
public class GestionnaireDB
{
    // ==========================================
    // MEMBRES STATIQUES - Partagés
    // ==========================================
    private static string _chaine Connexion;
    private static int _nombreConnexionsActives = 0;
    private static int _nombreConnexionsTotales = 0;
    
    public static int NombreConnexionsActives 
    { 
        get { return _nombreConnexionsActives; } 
    }
    
    public static int NombreConnexionsTotales 
    { 
        get { return _nombreConnexionsTotales; } 
    }
    
    // ==========================================
    // MEMBRES D'INSTANCE - Uniques à chaque connexion
    // ==========================================
    private int _id;
    private bool _estConnecte;
    private DateTime _dateConnexion;
    
    public int Id { get { return _id; } }
    public bool EstConnecte { get { return _estConnecte; } }
    
    // ==========================================
    // CONSTRUCTEUR STATIQUE
    // ==========================================
    static GestionnaireDB()
    {
        Console.WriteLine("[STATIC] Initialisation du gestionnaire DB");
        _chaineConnexion = "Server=localhost;Database=test;";
    }
    
    // ==========================================
    // CONSTRUCTEUR D'INSTANCE
    // ==========================================
    public GestionnaireDB()
    {
        _nombreConnexionsTotales++;
        _id = _nombreConnexionsTotales;
        Console.WriteLine($"[INSTANCE #{_id}] Créée");
    }
    
    // ==========================================
    // MÉTHODE D'INSTANCE
    // ==========================================
    public void Connecter()
    {
        if (!_estConnecte)
        {
            _estConnecte = true;
            _dateConnexion = DateTime.Now;
            _nombreConnexionsActives++;
            Console.WriteLine($"[INSTANCE #{_id}] Connectée. Actives: {_nombreConnexionsActives}");
        }
    }
    
    public void Deconnecter()
    {
        if (_estConnecte)
        {
            _estConnecte = false;
            _nombreConnexionsActives--;
            Console.WriteLine($"[INSTANCE #{_id}] Déconnectée. Actives: {_nombreConnexionsActives}");
        }
    }
    
    // ==========================================
    // MÉTHODE STATIQUE
    // ==========================================
    public static void AfficherStatistiques()
    {
        Console.WriteLine($"\n=== STATISTIQUES DB ===");
        Console.WriteLine($"Chaîne de connexion: {_chaineConnexion}");
        Console.WriteLine($"Connexions actives: {_nombreConnexionsActives}");
        Console.WriteLine($"Total créées: {_nombreConnexionsTotales}");
        Console.WriteLine($"=======================\n");
    }
}

// Utilisation
class Program
{
    static void Main()
    {
        GestionnaireDB.AfficherStatistiques(); // Déclenche le constructeur statique
        
        GestionnaireDB db1 = new GestionnaireDB();
        GestionnaireDB db2 = new GestionnaireDB();
        GestionnaireDB db3 = new GestionnaireDB();
        
        db1.Connecter();
        db2.Connecter();
        db3.Connecter();
        
        GestionnaireDB.AfficherStatistiques();
        
        db1.Deconnecter();
        db2.Deconnecter();
        
        GestionnaireDB.AfficherStatistiques();
    }
}
```

### 15.7 Règles Importantes

```csharp
public class Exemple
{
    private static int _compteurStatique = 0;
    private int _compteurInstance = 0;
    
    // ==========================================
    // MÉTHODE STATIQUE
    // ==========================================
    public static void MethodeStatique()
    {
        // ✅ OK - Accès à membre statique
        _compteurStatique++;
        
        // ❌ ERREUR - Pas d'accès aux membres d'instance
        // _compteurInstance++; // ERREUR!
        // this._compteurInstance++; // ERREUR!
        
        // ✅ OK - Appel d'autre méthode statique
        AutreMethodeStatique();
        
        // ❌ ERREUR - Pas d'appel de méthode d'instance
        // MethodeInstance(); // ERREUR!
    }
    
    // ==========================================
    // MÉTHODE D'INSTANCE
    // ==========================================
    public void MethodeInstance()
    {
        // ✅ OK - Accès aux membres d'instance
        _compteurInstance++;
        
        // ✅ OK - Accès aux membres statiques aussi
        _compteurStatique++;
        
        // ✅ OK - Appel de méthodes statiques
        MethodeStatique();
        AutreMethodeStatique();
    }
    
    private static void AutreMethodeStatique() { }
}
```

---

## Résumé Final

### Tableau Récapitulatif des Concepts

| Concept | Définition | Exemple d'Usage |
|---------|------------|----------------|
| **virtual** | Méthode avec implémentation, redéfinition optionnelle | Comportement par défaut modifiable |
| **abstract** | Méthode sans implémentation, redéfinition obligatoire | Forcer les dérivées à implémenter |
| **sealed** | Empêche l'héritage ou la redéfinition | Classe/méthode finale |
| **override** | Redéfinit une méthode virtual ou abstract | Polymorphisme |
| **new** | Masque un membre de la classe de base | Éviter, préférer override |
| **base** | Accède aux membres de la classe parent | Appeler la version parente |
| **this** | Référence à l'instance actuelle | Distinguer attributs/paramètres |
| **static** | Membre appartenant à la classe | Utilitaires, compteurs partagés |
| **interface** | Contrat sans implémentation | Définir comportements multiples |
| **abstract class** | Classe incomplète avec code partagé | Base commune avec implémentation |

### Hiérarchie Complète d'Exemple

```csharp
// Interface
public interface IVolant
{
    void Voler();
}

// Classe abstraite
public abstract class Animal
{
    protected string _nom;
    
    public Animal(string nom) { _nom = nom; }
    
    // Méthode abstraite
    public abstract void EmettreSound();
    
    // Méthode virtuelle
    public virtual void Dormir()
    {
        Console.WriteLine($"{_nom} dort");
    }
    
    // Méthode concrète
    public void Respirer()
    {
        Console.WriteLine($"{_nom} respire");
    }
}

// Classe concrète
public class Oiseau : Animal, IVolant
{
    public Oiseau(string nom) : base(nom) { }
    
    // Implémentation obligatoire (abstract)
    public override void EmettreSound()
    {
        Console.WriteLine($"{_nom} chante");
    }
    
    // Redéfinition optionnelle (virtual)
    public override void Dormir()
    {
        Console.WriteLine($"{_nom} dort dans un nid");
    }
    
    // Implémentation d'interface
    public void Voler()
    {
        Console.WriteLine($"{_nom} vole");
    }
}

// Classe sealed
public sealed class Pingouin : Oiseau
{
    public Pingouin(string nom) : base(nom) { }
    
    // Sealed override
    public sealed override void Dormir()
    {
        Console.WriteLine($"{_nom} dort en groupe");
    }
    
    // Les pingouins ne volent pas, mais implémentent quand même IVolant
    public new void Voler()
    {
        Console.WriteLine($"{_nom} ne peut pas voler!");
    }
}

// ❌ Impossible d'hériter de Pingouin (sealed)
// public class SuperPingouin : Pingouin { }
```