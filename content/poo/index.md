---
title: "Programmation orientée objet - partie 1"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 9
---

## Table des matières
1. [Introduction à la POO](#1-introduction-à-la-poo)
2. [Les Classes et les Objets](#2-les-classes-et-les-objets)
3. [Les Attributs (Champs)](#3-les-attributs-champs)
4. [Les Propriétés (Properties)](#4-les-propriétés-properties)
5. [L'Encapsulation](#5-lencapsulation)
6. [Les Méthodes](#6-les-méthodes)
7. [Les Constructeurs](#7-les-constructeurs)
8. [L'Héritage](#8-lhéritage)
9. [Le Polymorphisme](#9-le-polymorphisme)
---

## 1. Introduction à la POO

### 1.1 Qu'est-ce que la Programmation Orientée Objet ?

La **Programmation Orientée Objet (POO)** est un paradigme de programmation qui organise le code autour du concept d'**objets** plutôt que de fonctions et de logique. Un objet combine des **données** (ce qu'il possède) et des **comportements** (ce qu'il peut faire).

#### 🌍 Analogie du monde réel

Pensez à une voiture dans le monde réel :
- **Données** : couleur, marque, modèle, vitesse actuelle, niveau d'essence
- **Comportements** : démarrer, accélérer, freiner, tourner, klaxonner

En POO, nous modélisons ces concepts du monde réel dans notre code.

### 1.2 Pourquoi utiliser la POO ?

**Avant la POO (Programmation procédurale) :**
```csharp
// Variables dispersées
string voitureMarque = "Toyota";
string voitureModele = "Camry";
int voitureVitesse = 0;

// Fonctions séparées
void DemarrerVoiture() { /* ... */ }
void AccelererVoiture(int vitesse) { /* ... */ }
```

**Avec la POO :**
```csharp
// Tout est regroupé logiquement
class Voiture
{
    string marque;
    string modele;
    int vitesse;
    
    void Demarrer() { /* ... */ }
    void Accelerer(int increment) { /* ... */ }
}
```

**Avantages de la POO :**
1. **Organisation** : Le code est structuré et logique
2. **Réutilisabilité** : Les classes peuvent être réutilisées
3. **Maintenabilité** : Plus facile à maintenir et modifier
4. **Modularité** : Chaque classe a une responsabilité claire
5. **Abstraction** : Cache la complexité interne

### 1.3 Les 4 Piliers Fondamentaux de la POO

#### 1️⃣ **Encapsulation**
Regrouper les données et les méthodes ensemble, et contrôler l'accès aux données.

**Exemple concret :** Un compte bancaire
- Vous ne pouvez pas modifier directement le solde
- Vous devez passer par des méthodes (déposer, retirer)
- Cela protège l'intégrité des données

#### 2️⃣ **Héritage**
Créer de nouvelles classes basées sur des classes existantes.

**Exemple concret :** Véhicules
- Classe de base : `Vehicule` (propriétés communes : marque, modèle)
- Classes dérivées : `Voiture`, `Moto`, `Camion` (ajoutent leurs spécificités)

#### 3️⃣ **Polymorphisme**
Utiliser une même interface pour des types différents.

**Exemple concret :** Animaux
- Tous peuvent "émettre un son"
- Le chien aboie, le chat miaule, l'oiseau chante
- Même méthode, comportements différents

#### 4️⃣ **Abstraction**
Montrer seulement l'essentiel, cacher les détails complexes.

**Exemple concret :** Conduire une voiture
- Vous utilisez le volant, les pédales
- Vous n'avez pas besoin de comprendre le moteur interne

---

## 2. Les Classes et les Objets

### 2.1 Qu'est-ce qu'une Classe ?

Une **classe** est un **plan** ou un **modèle** qui définit la structure et le comportement d'objets. C'est comme un plan d'architecte pour construire une maison.

**Métaphore :** 
- Une classe est comme un **moule à biscuits** 🍪
- Les objets sont les **biscuits** créés avec ce moule
- Tous les biscuits ont la même forme (structure), mais peuvent avoir des différences (valeurs)

### 2.2 Qu'est-ce qu'un Objet ?

Un **objet** est une **instance** d'une classe. C'est une entité concrète créée à partir du modèle défini par la classe.

### 2.3 Anatomie d'une Classe

```csharp
// Déclaration d'une classe
public class Personne
{
    // ==========================================
    // ATTRIBUTS (CHAMPS) - Ce que l'objet possède
    // ==========================================
    private string nom;
    private string prenom;
    private int age;
    
    // ==========================================
    // PROPRIÉTÉS - Interface contrôlée pour les attributs
    // ==========================================
    public string Nom 
    { 
        get { return nom; }
        set { nom = value; }
    }
    
    // ==========================================
    // CONSTRUCTEUR - Comment créer l'objet
    // ==========================================
    public Personne(string nom, string prenom, int age)
    {
        this.nom = nom;
        this.prenom = prenom;
        this.age = age;
    }
    
    // ==========================================
    // MÉTHODES - Ce que l'objet peut faire
    // ==========================================
    public void SePresenter()
    {
        Console.WriteLine($"Bonjour, je suis {prenom} {nom}, {age} ans.");
    }
}
```

### 2.4 Création et Utilisation d'Objets

```csharp
class Program
{
    static void Main()
    {
        // ==========================================
        // CRÉATION D'OBJETS (INSTANCIATION)
        // ==========================================
        
        // Syntaxe : Type nomVariable = new Constructeur();
        Personne personne1 = new Personne("Tremblay", "Marie", 25);
        Personne personne2 = new Personne("Gagnon", "Jean", 30);
        Personne personne3 = new Personne("Roy", "Sophie", 28);
        
        // Chaque objet est UNIQUE et INDÉPENDANT
        // personne1, personne2, personne3 sont 3 objets différents
        
        // ==========================================
        // UTILISATION DES OBJETS
        // ==========================================
        
        personne1.SePresenter(); 
        // Affiche: Bonjour, je suis Marie Tremblay, 25 ans.
        
        personne2.SePresenter(); 
        // Affiche: Bonjour, je suis Jean Gagnon, 30 ans.
        
        // Accès aux propriétés
        Console.WriteLine(personne1.Nom); // Affiche: Tremblay
    }
}
```

### 2.5 Comprendre la Mémoire : Référence vs Valeur

```csharp
// Les CLASSES sont des types RÉFÉRENCE
Personne p1 = new Personne("Dubois", "Luc", 35);
Personne p2 = p1; // p2 pointe vers le MÊME objet que p1

p2.Nom = "Martin"; // Modifie l'objet

Console.WriteLine(p1.Nom); // Affiche: Martin
Console.WriteLine(p2.Nom); // Affiche: Martin
// p1 et p2 pointent vers le même objet en mémoire !
```

**Illustration :**
```
Mémoire:
┌─────────────────────────────┐
│  Objet Personne             │
│  Nom: "Martin"              │ ←── p1 pointe ici
│  Prenom: "Luc"              │ ←── p2 pointe ici aussi
│  Age: 35                    │
└─────────────────────────────┘
```

---

## 3. Les Attributs (Champs)

### 3.1 Définition

Les **attributs** (ou **champs** ou **fields**) sont des **variables** déclarées directement dans une classe. Ils représentent l'**état** ou les **données** de l'objet.

### 3.2 Déclaration des Attributs

```csharp
public class CompteBancaire
{
    // ==========================================
    // ATTRIBUTS PRIVÉS (convention : préfixe _)
    // ==========================================
    private string _numeroCompte;
    private string _titulaire;
    private decimal _solde;
    private DateTime _dateOuverture;
    private bool _estActif;
    
    // ==========================================
    // ATTRIBUTS PUBLICS (à éviter généralement)
    // ==========================================
    public int nombreTransactions; // Moins sécurisé
}
```

### 3.3 Pourquoi des Attributs Privés ?

**❌ Problème avec des attributs publics :**
```csharp
public class CompteBancaire
{
    public decimal solde; // PUBLIC = DANGER !
}

// Dans le code utilisateur
CompteBancaire compte = new CompteBancaire();
compte.solde = -5000; // ❌ On peut mettre n'importe quoi !
compte.solde = 999999999; // ❌ Aucune validation !
```

**✅ Solution avec des attributs privés :**
```csharp
public class CompteBancaire
{
    private decimal _solde; // PRIVÉ = PROTÉGÉ
    
    public void Deposer(decimal montant)
    {
        if (montant > 0) // ✅ Validation
        {
            _solde += montant;
        }
    }
}
```

### 3.4 Initialisation des Attributs

```csharp
public class Voiture
{
    // Initialisation lors de la déclaration
    private string _marque = "Inconnue";
    private int _annee = 2020;
    private double _kilometrage = 0.0;
    private bool _estDemarree = false;
    
    // Sans initialisation (valeurs par défaut)
    private int _nombrePortes; // 0 par défaut
    private string _couleur;   // null par défaut
}
```

**Valeurs par défaut en C# :**
- `int`, `double`, `float` : `0`
- `bool` : `false`
- `string`, objets : `null`
- `DateTime` : `01/01/0001 00:00:00`

---

## 4. Les Propriétés (Properties)

### 4.1 Qu'est-ce qu'une Propriété ?

Une **propriété** est un **membre** de classe qui fournit un mécanisme flexible pour **lire** et **écrire** les valeurs des attributs privés. C'est l'interface publique pour accéder aux données privées.

**Analogie :** 
- L'attribut privé = Le coffre-fort 🔒
- La propriété = La porte avec code d'accès 🚪
- Le getter = Ouvrir pour voir le contenu 👀
- Le setter = Ouvrir pour modifier le contenu ✍️

### 4.2 Syntaxe Complète d'une Propriété

```csharp
public class Personne
{
    // ==========================================
    // ATTRIBUT PRIVÉ
    // ==========================================
    private int _age;
    
    // ==========================================
    // PROPRIÉTÉ AVEC GET ET SET COMPLETS
    // ==========================================
    public int Age
    {
        // GETTER - Lit la valeur
        get 
        { 
            Console.WriteLine("Lecture de l'âge");
            return _age; 
        }
        
        // SETTER - Modifie la valeur
        set 
        { 
            Console.WriteLine($"Modification de l'âge: {value}");
            
            // VALIDATION avant d'assigner
            if (value >= 0 && value <= 150)
            {
                _age = value;
            }
            else
            {
                Console.WriteLine("Âge invalide!");
            }
        }
    }
}

// Utilisation
Personne p = new Personne();
p.Age = 25;        // Appelle le SETTER avec value = 25
int monAge = p.Age; // Appelle le GETTER
```

### 4.3 Types de Propriétés

#### A) Propriété Lecture/Écriture (Get/Set)

```csharp
public class Produit
{
    private string _nom;
    
    public string Nom
    {
        get { return _nom; }
        set { _nom = value; }
    }
}
```

#### B) Propriété en Lecture Seule (Get seulement)

```csharp
public class Personne
{
    private DateTime _dateNaissance;
    
    // On peut LIRE mais pas MODIFIER de l'extérieur
    public DateTime DateNaissance
    {
        get { return _dateNaissance; }
        // Pas de SET = lecture seule
    }
    
    public Personne(DateTime dateNaissance)
    {
        _dateNaissance = dateNaissance; // Défini dans le constructeur
    }
}

// Utilisation
Personne p = new Personne(new DateTime(1990, 5, 15));
Console.WriteLine(p.DateNaissance); // ✅ OK - Lecture
// p.DateNaissance = DateTime.Now;  // ❌ ERREUR - Pas de setter!
```

#### C) Propriété en Écriture Seule (Set seulement) - RARE

```csharp
public class CompteSecurise
{
    private string _motDePasse;
    
    // On peut ÉCRIRE mais pas LIRE (pour la sécurité)
    public string MotDePasse
    {
        set { _motDePasse = HashPassword(value); }
        // Pas de GET pour des raisons de sécurité
    }
    
    private string HashPassword(string password)
    {
        // Logique de hachage
        return password; // Simplifié
    }
}
```

#### D) Propriété Auto-Implémentée (Raccourci)

```csharp
public class Livre
{
    // ==========================================
    // PROPRIÉTÉ AUTO-IMPLÉMENTÉE
    // Le compilateur crée automatiquement un attribut privé caché
    // ==========================================
    public string Titre { get; set; }
    public string Auteur { get; set; }
    public int NombrePages { get; set; }
    
    // Équivalent à :
    // private string _titre;
    // public string Titre { get { return _titre; } set { _titre = value; } }
}

// Utilisation
Livre livre = new Livre();
livre.Titre = "Le Petit Prince";
livre.Auteur = "Antoine de Saint-Exupéry";
livre.NombrePages = 96;
```

#### E) Propriété avec Logique Métier

```csharp
public class Rectangle
{
    private double _longueur;
    private double _largeur;
    
    public double Longueur
    {
        get { return _longueur; }
        set 
        { 
            if (value > 0)
                _longueur = value;
            else
                throw new ArgumentException("La longueur doit être positive");
        }
    }
    
    public double Largeur
    {
        get { return _largeur; }
        set 
        { 
            if (value > 0)
                _largeur = value;
            else
                throw new ArgumentException("La largeur doit être positive");
        }
    }
    
    // ==========================================
    // PROPRIÉTÉ CALCULÉE (pas d'attribut associé)
    // ==========================================
    public double Aire
    {
        get { return _longueur * _largeur; }
        // Pas de SET - calculée à chaque fois
    }
    
    public double Perimetre
    {
        get { return 2 * (_longueur + _largeur); }
    }
}

// Utilisation
Rectangle rect = new Rectangle();
rect.Longueur = 5;
rect.Largeur = 3;
Console.WriteLine($"Aire: {rect.Aire}");           // 15
Console.WriteLine($"Périmètre: {rect.Perimetre}"); // 16
```

#### F) Propriété avec Accessibilité Différente

```csharp
public class CompteBancaire
{
    private decimal _solde;
    
    // ==========================================
    // GET public, SET privé
    // Tout le monde peut LIRE, seule la classe peut MODIFIER
    // ==========================================
    public decimal Solde
    {
        get { return _solde; }
        private set { _solde = value; } // SET PRIVÉ
    }
    
    // Méthodes publiques pour modifier le solde de manière contrôlée
    public void Deposer(decimal montant)
    {
        if (montant > 0)
            Solde += montant; // Utilise le setter privé
    }
    
    public bool Retirer(decimal montant)
    {
        if (montant > 0 && montant <= Solde)
        {
            Solde -= montant; // Utilise le setter privé
            return true;
        }
        return false;
    }
}

// Utilisation
CompteBancaire compte = new CompteBancaire();
Console.WriteLine(compte.Solde);  // ✅ OK - Lecture publique
// compte.Solde = 1000;           // ❌ ERREUR - Set est privé
compte.Deposer(1000);             // ✅ OK - Méthode publique
```

### 4.4 Le Mot-Clé `value`

Dans un **setter**, le mot-clé `value` représente la valeur qu'on essaie d'assigner.

```csharp
public class Exemple
{
    private int _nombre;
    
    public int Nombre
    {
        get { return _nombre; }
        set 
        {
            // 'value' contient la valeur assignée
            Console.WriteLine($"Valeur reçue: {value}");
            _nombre = value;
        }
    }
}

// Utilisation
Exemple ex = new Exemple();
ex.Nombre = 42; // 'value' vaut 42 dans le setter
```

### 4.5 Propriétés: Quand Utiliser Quoi ?

| Situation | Type de Propriété |
|-----------|-------------------|
| Données simples sans validation | Auto-implémentée `{ get; set; }` |
| Données nécessitant validation | Propriété complète avec logique |
| Données calculées | Get seulement (calculée) |
| Données définies à la création | Get seulement + constructeur |
| Données sensibles | Get public, Set privé |
| Mot de passe | Set seulement (écriture seule) |

---

## 5. L'Encapsulation

### 5.1 Définition Approfondie

L'**encapsulation** est le principe qui consiste à :
1. **Regrouper** les données (attributs) et les comportements (méthodes) dans une classe
2. **Cacher** les détails d'implémentation internes
3. **Contrôler** l'accès aux données via une interface publique

**Métaphore de la voiture :**
- Vous n'avez pas besoin de comprendre comment fonctionne le moteur (détails cachés)
- Vous utilisez le volant, les pédales, le levier de vitesse (interface publique)
- Le moteur est protégé sous le capot (encapsulation)

### 5.2 Modificateurs d'Accès

Les modificateurs d'accès contrôlent QUI peut accéder à quoi.

#### Tableau Complet des Modificateurs

| Modificateur | Accès | Usage Typique |
|--------------|-------|---------------|
| `private` | Classe uniquement | Attributs, méthodes internes |
| `protected` | Classe + classes dérivées | Méthodes partagées avec enfants |
| `internal` | Même assembly (projet) | Classes utilitaires internes |
| `protected internal` | Assembly OU dérivées | Cas spécifiques |
| `public` | Partout | Interface publique, API |

#### Exemples Détaillés

```csharp
public class Voiture
{
    // ==========================================
    // PRIVATE - Accessible UNIQUEMENT dans cette classe
    // ==========================================
    private string _numeroSerie;     // Donnée sensible
    private int _temperatureMoteur;  // Détail interne
    
    private void DemarrerMoteur()    // Méthode interne
    {
        Console.WriteLine("Moteur démarré");
    }
    
    // ==========================================
    // PROTECTED - Accessible dans cette classe ET ses dérivées
    // ==========================================
    protected double _consommationBase; // Les classes enfants peuvent y accéder
    
    protected void CalculerConsommation() // Méthode pour les dérivées
    {
        Console.WriteLine("Calcul de la consommation");
    }
    
    // ==========================================
    // PUBLIC - Accessible PARTOUT
    // ==========================================
    public string Marque { get; set; }   // Propriété publique
    public string Modele { get; set; }
    
    public void Demarrer()               // Méthode publique (interface)
    {
        DemarrerMoteur(); // Appelle la méthode privée
        Console.WriteLine("Voiture démarrée");
    }
    
    public void Accelerer(int vitesse)
    {
        // Interface publique qui utilise des détails privés
        _temperatureMoteur += 10;
        Console.WriteLine($"Accélération à {vitesse} km/h");
    }
}

// Classe dérivée
public class VoitureElectrique : Voiture
{
    public void ChargerBatterie()
    {
        // ✅ OK - Accès à protected
        _consommationBase = 0.15;
        CalculerConsommation();
        
        // ❌ ERREUR - Pas d'accès à private
        // _numeroSerie = "123"; // ERREUR DE COMPILATION
        // DemarrerMoteur();     // ERREUR DE COMPILATION
        
        // ✅ OK - Accès à public
        Marque = "Tesla";
        Demarrer();
    }
}

// Utilisation externe
class Program
{
    static void Main()
    {
        Voiture maVoiture = new Voiture();
        
        // ✅ OK - Accès aux membres publics
        maVoiture.Marque = "Toyota";
        maVoiture.Demarrer();
        maVoiture.Accelerer(50);
        
        // ❌ ERREUR - Pas d'accès aux membres privés
        // maVoiture._numeroSerie = "ABC123";      // ERREUR
        // maVoiture.DemarrerMoteur();             // ERREUR
        
        // ❌ ERREUR - Pas d'accès aux membres protected
        // maVoiture._consommationBase = 0.10;     // ERREUR
        // maVoiture.CalculerConsommation();       // ERREUR
    }
}
```

### 5.3 Exemple Complet : Compte Bancaire Bien Encapsulé

```csharp
public class CompteBancaire
{
    // ==========================================
    // ATTRIBUTS PRIVÉS - État interne protégé
    // ==========================================
    private string _numeroCompte;
    private string _titulaire;
    private decimal _solde;
    private List<string> _historiqueTransactions;
    private DateTime _dateOuverture;
    private const decimal FRAIS_RETRAIT = 1.50m; // Constante privée
    
    // ==========================================
    // PROPRIÉTÉS PUBLIQUES - Interface contrôlée
    // ==========================================
    
    // Lecture seule de l'extérieur
    public string NumeroCompte 
    { 
        get { return _numeroCompte; }
        private set { _numeroCompte = value; }
    }
    
    public string Titulaire
    {
        get { return _titulaire; }
        set 
        {
            if (!string.IsNullOrWhiteSpace(value))
                _titulaire = value;
        }
    }
    
    // Lecture seule - impossible de modifier directement
    public decimal Solde 
    { 
        get { return _solde; }
        private set { _solde = value; }
    }
    
    public DateTime DateOuverture 
    { 
        get { return _dateOuverture; }
    }
    
    // ==========================================
    // CONSTRUCTEUR
    // ==========================================
    public CompteBancaire(string titulaire, string numeroCompte)
    {
        _titulaire = titulaire;
        _numeroCompte = numeroCompte;
        _solde = 0;
        _dateOuverture = DateTime.Now;
        _historiqueTransactions = new List<string>();
        
        AjouterTransaction("Ouverture du compte");
    }
    
    // ==========================================
    // MÉTHODES PUBLIQUES - Actions autorisées
    // ==========================================
    
    public bool Deposer(decimal montant)
    {
        if (montant <= 0)
        {
            Console.WriteLine("Le montant doit être positif");
            return false;
        }
        
        _solde += montant;
        AjouterTransaction($"Dépôt de {montant:C}");
        Console.WriteLine($"Dépôt réussi. Nouveau solde: {_solde:C}");
        return true;
    }
    
    public bool Retirer(decimal montant)
    {
        if (montant <= 0)
        {
            Console.WriteLine("Le montant doit être positif");
            return false;
        }
        
        decimal montantTotal = montant + FRAIS_RETRAIT;
        
        if (montantTotal > _solde)
        {
            Console.WriteLine("Solde insuffisant");
            return false;
        }
        
        _solde -= montantTotal;
        AjouterTransaction($"Retrait de {montant:C} (frais: {FRAIS_RETRAIT:C})");
        Console.WriteLine($"Retrait réussi. Nouveau solde: {_solde:C}");
        return true;
    }
    
    public void AfficherHistorique()
    {
        Console.WriteLine($"\n=== Historique du compte {_numeroCompte} ===");
        foreach (string transaction in _historiqueTransactions)
        {
            Console.WriteLine(transaction);
        }
    }
    
    // ==========================================
    // MÉTHODES PRIVÉES - Détails d'implémentation
    // ==========================================
    
    private void AjouterTransaction(string description)
    {
        string transaction = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - {description}";
        _historiqueTransactions.Add(transaction);
    }
    
    private bool VerifierFrauude()
    {
        // Logique complexe de vérification
        return true;
    }
}

// ==========================================
// UTILISATION
// ==========================================
class Program
{
    static void Main()
    {
        CompteBancaire compte = new CompteBancaire("Jean Dupont", "12345");
        
        // ✅ Interface publique propre et sécurisée
        compte.Deposer(1000);
        compte.Retirer(50);
        compte.AfficherHistorique();
        
        // ✅ Lecture sécurisée
        Console.WriteLine($"Solde actuel: {compte.Solde:C}");
        
        // ❌ Impossible de tricher!
        // compte.Solde = 9999999; // ERREUR - Setter privé
        // compte._solde += 1000;  // ERREUR - Attribut privé
    }
}
```

### 5.4 Avantages de l'Encapsulation

1. **Sécurité** : Protection des données contre les modifications non autorisées
2. **Validation** : Contrôle des valeurs assignées
3. **Flexibilité** : Modification de l'implémentation interne sans casser le code externe
4. **Maintenance** : Code plus facile à comprendre et modifier
5. **Débogage** : Points de contrôle clairs pour tracer les problèmes

---

## 6. Les Méthodes

### 6.1 Définition

Les **méthodes** sont des **fonctions** définies à l'intérieur d'une classe. Elles représentent les **comportements** ou **actions** que les objets peuvent effectuer.

### 6.2 Syntaxe Complète

```csharp
[modificateur d'accès] [modificateurs] [type de retour] NomMethode([paramètres])
{
    // Corps de la méthode
    return valeur; // Si type de retour n'est pas void
}
```

### 6.3 Types de Méthodes

#### A) Méthode Sans Retour (void)

```csharp
public class Robot
{
    public void Avancer()
    {
        Console.WriteLine("Le robot avance");
        // Pas de return - void signifie "ne retourne rien"
    }
    
    public void Saluer(string nom)
    {
        Console.WriteLine($"Bonjour {nom}!");
    }
}
```

#### B) Méthode Avec Retour

```csharp
public class Calculatrice
{
    public int Additionner(int a, int b)
    {
        int resultat = a + b;
        return resultat; // DOIT retourner un int
    }
    
    public double CalculerMoyenne(double[] nombres)
    {
        double somme = 0;
        foreach (double nombre in nombres)
        {
            somme += nombre;
        }
        return somme / nombres.Length;
    }
    
    public bool EstPair(int nombre)
    {
        return nombre % 2 == 0; // Retourne true ou false
    }
}
```

#### C) Méthode Avec Paramètres

```csharp
public class GestionnaireEmail
{
    // Paramètres obligatoires
    public void EnvoyerEmail(string destinataire, string sujet, string message)
    {
        Console.WriteLine($"À: {destinataire}");
        Console.WriteLine($"Sujet: {sujet}");
        Console.WriteLine($"Message: {message}");
    }
    
    // Paramètres avec valeurs par défaut
    public void EnvoyerNotification(string message, string niveau = "INFO")
    {
        Console.WriteLine($"[{niveau}] {message}");
    }
}

// Utilisation
GestionnaireEmail gestionnaire = new GestionnaireEmail();
gestionnaire.EnvoyerEmail("jean@example.com", "Bienvenue", "Bonjour!");
gestionnaire.EnvoyerNotification("Système démarré"); // Utilise niveau par défaut
gestionnaire.EnvoyerNotification("Erreur détectée", "ERREUR"); // Spécifie le niveau
```

#### D) Surcharge de Méthodes (Overloading)

Plusieurs méthodes avec le **même nom** mais des **paramètres différents**.

```csharp
public class Calculatrice
{
    // Même nom, paramètres différents
    public int Additionner(int a, int b)
    {
        return a + b;
    }
    
    public int Additionner(int a, int b, int c)
    {
        return a + b + c;
    }
    
    public double Additionner(double a, double b)
    {
        return a + b;
    }
    
    public int Additionner(params int[] nombres) // Nombre variable de paramètres
    {
        int somme = 0;
        foreach (int nombre in nombres)
        {
            somme += nombre;
        }
        return somme;
    }
}

// Utilisation
Calculatrice calc = new Calculatrice();
calc.Additionner(5, 3);              // Appelle la version à 2 int
calc.Additionner(5, 3, 2);           // Appelle la version à 3 int
calc.Additionner(5.5, 3.2);          // Appelle la version à 2 double
calc.Additionner(1, 2, 3, 4, 5, 6); // Appelle la version avec params
```

### 6.4 Le Mot-Clé `this`

`this` fait référence à l'**instance actuelle** de la classe.

```csharp
public class Personne
{
    private string nom;
    private int age;
    
    public Personne(string nom, int age)
    {
        // 'this.nom' = attribut de la classe
        // 'nom' = paramètre du constructeur
        this.nom = nom; // Distingue l'attribut du paramètre
        this.age = age;
    }
    
    public void Comparer(Personne autre)
    {
        if (this.age > autre.age)
        {
            Console.WriteLine($"{this.nom} est plus âgé que {autre.nom}");
        }
    }
    
    public Personne ObtenirRéférence()
    {
        return this; // Retourne l'objet lui-même
    }
}
```

### 6.5 Méthodes d'Instance vs Méthodes Statiques

```csharp
public class Utilitaires
{
    // Attribut d'instance
    private int compteur = 0;
    
    // ==========================================
    // MÉTHODE D'INSTANCE - Nécessite un objet
    // ==========================================
    public void Incrementer()
    {
        compteur++; // Accède à l'attribut d'instance
        Console.WriteLine($"Compteur: {compteur}");
    }
    
    // ==========================================
    // MÉTHODE STATIQUE - Pas besoin d'objet
    // ==========================================
    public static int Additionner(int a, int b)
    {
        // Ne peut PAS accéder aux membres d'instance
        // compteur++; // ❌ ERREUR!
        return a + b;
    }
    
    public static double CalculerAire(double rayon)
    {
        return Math.PI * rayon * rayon;
    }
}

// Utilisation
// Méthode d'instance
Utilitaires util = new Utilitaires();
util.Incrementer(); // Besoin d'un objet

// Méthode statique
int somme = Utilitaires.Additionner(5, 3); // Pas besoin d'objet
double aire = Utilitaires.CalculerAire(5);
```

---

## 7. Les Constructeurs

### 7.1 Définition Approfondie

Un **constructeur** est une méthode spéciale qui est automatiquement appelée lors de la **création** d'un objet. Son rôle principal est d'**initialiser** l'objet dans un état valide.

**Caractéristiques d'un constructeur :**
- Même nom que la classe
- Pas de type de retour (même pas `void`)
- Peut avoir des paramètres
- Peut être surchargé (plusieurs constructeurs différents)
- Appelé automatiquement avec `new`

**Métaphore :** Le constructeur est comme le **mode d'emploi d'assemblage** d'un meuble IKEA. Il définit comment créer et préparer l'objet pour qu'il soit utilisable.

### 7.2 Constructeur Par Défaut

```csharp
public class Voiture
{
    public string Marque;
    public string Modele;
    public int Annee;
    
    // ==========================================
    // CONSTRUCTEUR PAR DÉFAUT (sans paramètres)
    // ==========================================
    public Voiture()
    {
        Console.WriteLine("Construction d'une voiture...");
        Marque = "Inconnue";
        Modele = "Standard";
        Annee = 2020;
    }
}

// Utilisation
Voiture v = new Voiture(); // Appelle le constructeur par défaut
Console.WriteLine($"{v.Marque} {v.Modele}"); // Inconnue Standard
```

**Important :** Si vous ne définissez AUCUN constructeur, C# crée automatiquement un constructeur par défaut vide. Mais si vous définissez au moins un constructeur, le constructeur par défaut automatique disparaît.

```csharp
public class Exemple1
{
    // Pas de constructeur défini
    // C# crée automatiquement : public Exemple1() { }
}

public class Exemple2
{
    public Exemple2(int valeur) { }
    // Le constructeur par défaut automatique N'EXISTE PLUS
}

// Utilisation
Exemple1 e1 = new Exemple1(); // ✅ OK
Exemple2 e2 = new Exemple2(); // ❌ ERREUR - Pas de constructeur sans paramètre
Exemple2 e3 = new Exemple2(5); // ✅ OK
```

### 7.3 Constructeur Avec Paramètres

```csharp
public class Personne
{
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public int Age { get; set; }
    
    // ==========================================
    // CONSTRUCTEUR AVEC PARAMÈTRES
    // ==========================================
    public Personne(string nom, string prenom, int age)
    {
        Console.WriteLine("Création d'une personne...");
        
        // Validation avant assignation
        if (string.IsNullOrWhiteSpace(nom))
            throw new ArgumentException("Le nom ne peut pas être vide");
        
        if (age < 0 || age > 150)
            throw new ArgumentException("Âge invalide");
        
        Nom = nom;
        Prenom = prenom;
        Age = age;
    }
}

// Utilisation
Personne p1 = new Personne("Tremblay", "Marie", 25);
// Personne p2 = new Personne("", "Jean", 30); // ❌ Exception levée
```

### 7.4 Surcharge de Constructeurs

Une classe peut avoir **plusieurs constructeurs** avec différents paramètres.

```csharp
public class Livre
{
    public string Titre { get; set; }
    public string Auteur { get; set; }
    public int Annee { get; set; }
    public int Pages { get; set; }
    
    // ==========================================
    // CONSTRUCTEUR 1 : Sans paramètres
    // ==========================================
    public Livre()
    {
        Titre = "Sans titre";
        Auteur = "Anonyme";
        Annee = DateTime.Now.Year;
        Pages = 0;
        Console.WriteLine("Livre créé avec valeurs par défaut");
    }
    
    // ==========================================
    // CONSTRUCTEUR 2 : Titre seulement
    // ==========================================
    public Livre(string titre)
    {
        Titre = titre;
        Auteur = "Anonyme";
        Annee = DateTime.Now.Year;
        Pages = 0;
        Console.WriteLine($"Livre '{titre}' créé");
    }
    
    // ==========================================
    // CONSTRUCTEUR 3 : Titre et Auteur
    // ==========================================
    public Livre(string titre, string auteur)
    {
        Titre = titre;
        Auteur = auteur;
        Annee = DateTime.Now.Year;
        Pages = 0;
        Console.WriteLine($"Livre '{titre}' de {auteur} créé");
    }
    
    // ==========================================
    // CONSTRUCTEUR 4 : Tous les paramètres
    // ==========================================
    public Livre(string titre, string auteur, int annee, int pages)
    {
        Titre = titre;
        Auteur = auteur;
        Annee = annee;
        Pages = pages;
        Console.WriteLine($"Livre complet créé");
    }
}

// Utilisation - Le compilateur choisit le bon constructeur
Livre l1 = new Livre();                                    // Constructeur 1
Livre l2 = new Livre("1984");                              // Constructeur 2
Livre l3 = new Livre("Le Petit Prince", "Saint-Exupéry"); // Constructeur 3
Livre l4 = new Livre("Dune", "Frank Herbert", 1965, 412); // Constructeur 4
```

### 7.5 Chaînage de Constructeurs avec `this`

Pour éviter la duplication de code, un constructeur peut appeler un autre constructeur de la même classe avec `: this()`.

```csharp
public class Rectangle
{
    public double Longueur { get; set; }
    public double Largeur { get; set; }
    public string Couleur { get; set; }
    
    // ==========================================
    // CONSTRUCTEUR PRINCIPAL (le plus complet)
    // ==========================================
    public Rectangle(double longueur, double largeur, string couleur)
    {
        Console.WriteLine("Constructeur principal appelé");
        Longueur = longueur;
        Largeur = largeur;
        Couleur = couleur;
    }
    
    // ==========================================
    // CONSTRUCTEUR qui appelle le principal
    // ==========================================
    public Rectangle(double longueur, double largeur) 
        : this(longueur, largeur, "Blanc") // Appelle le constructeur principal
    {
        Console.WriteLine("Constructeur sans couleur");
        // Pas besoin de répéter l'assignation de longueur et largeur
    }
    
    // ==========================================
    // CONSTRUCTEUR pour un carré
    // ==========================================
    public Rectangle(double cote) 
        : this(cote, cote, "Blanc") // Appelle le constructeur principal
    {
        Console.WriteLine("Constructeur pour carré");
    }
    
    // ==========================================
    // CONSTRUCTEUR par défaut
    // ==========================================
    public Rectangle() 
        : this(1, 1, "Blanc") // Appelle le constructeur principal
    {
        Console.WriteLine("Constructeur par défaut");
    }
}

// Utilisation
Rectangle r1 = new Rectangle(5, 3, "Rouge");
// Affiche: "Constructeur principal appelé"

Rectangle r2 = new Rectangle(5, 3);
// Affiche: "Constructeur principal appelé"
//          "Constructeur sans couleur"

Rectangle r3 = new Rectangle(4);
// Affiche: "Constructeur principal appelé"
//          "Constructeur pour carré"
```

**Ordre d'exécution :**
1. Le constructeur appelé avec `: this()` s'exécute en PREMIER
2. Puis le constructeur actuel s'exécute

### 7.6 Constructeurs Privés

Un constructeur peut être **privé** pour contrôler la création d'instances.

```csharp
// ==========================================
// PATTERN SINGLETON - Une seule instance possible
// ==========================================
public class Configuration
{
    private static Configuration _instance = null;
    
    public string CheminFichier { get; set; }
    public string Langue { get; set; }
    
    // ==========================================
    // CONSTRUCTEUR PRIVÉ - Impossible de faire 'new Configuration()'
    // ==========================================
    private Configuration()
    {
        CheminFichier = "config.json";
        Langue = "fr";
        Console.WriteLine("Configuration créée");
    }
    
    // ==========================================
    // MÉTHODE PUBLIQUE pour obtenir l'instance unique
    // ==========================================
    public static Configuration ObtenirInstance()
    {
        if (_instance == null)
        {
            _instance = new Configuration();
        }
        return _instance;
    }
}

// Utilisation
// Configuration c1 = new Configuration(); // ❌ ERREUR - Constructeur privé

Configuration c1 = Configuration.ObtenirInstance(); // ✅ OK
Configuration c2 = Configuration.ObtenirInstance(); // Retourne la même instance
Console.WriteLine(c1 == c2); // True - Même objet
```

### 7.7 Initialisation d'Objets

En plus des constructeurs, C# offre des syntaxes modernes pour initialiser les objets.

```csharp
public class Produit
{
    public string Nom { get; set; }
    public decimal Prix { get; set; }
    public string Categorie { get; set; }
    
    public Produit() { }
    
    public Produit(string nom, decimal prix)
    {
        Nom = nom;
        Prix = prix;
    }
}

// ==========================================
// SYNTAXE 1 : Constructeur traditionnel
// ==========================================
Produit p1 = new Produit("Laptop", 999.99m);
p1.Categorie = "Électronique";

// ==========================================
// SYNTAXE 2 : Initialiseur d'objet (Object Initializer)
// ==========================================
Produit p2 = new Produit 
{
    Nom = "Souris",
    Prix = 29.99m,
    Categorie = "Accessoires"
};

// ==========================================
// SYNTAXE 3 : Combinaison constructeur + initialiseur
// ==========================================
Produit p3 = new Produit("Clavier", 79.99m)
{
    Categorie = "Accessoires" // Ajoute la catégorie après la construction
};

// ==========================================
// SYNTAXE 4 : C# 9+ (sans répéter le type)
// ==========================================
Produit p4 = new("Écran", 299.99m) { Categorie = "Périphériques" };
```

### 7.8 Exemple Complet : Classe CompteBancaire

```csharp
public class CompteBancaire
{
    // ==========================================
    // ATTRIBUTS PRIVÉS
    // ==========================================
    private string _numeroCompte;
    private string _titulaire;
    private decimal _solde;
    private DateTime _dateOuverture;
    private static int _compteurComptes = 0; // Pour générer des numéros uniques
    
    // ==========================================
    // PROPRIÉTÉS
    // ==========================================
    public string NumeroCompte { get { return _numeroCompte; } }
    public string Titulaire { get { return _titulaire; } }
    public decimal Solde { get { return _solde; } }
    public DateTime DateOuverture { get { return _dateOuverture; } }
    
    // ==========================================
    // CONSTRUCTEUR 1 : Complet
    // ==========================================
    public CompteBancaire(string titulaire, decimal soldeInitial, string numeroCompte)
    {
        if (string.IsNullOrWhiteSpace(titulaire))
            throw new ArgumentException("Le titulaire ne peut pas être vide");
        
        if (soldeInitial < 0)
            throw new ArgumentException("Le solde initial ne peut pas être négatif");
        
        _titulaire = titulaire;
        _solde = soldeInitial;
        _numeroCompte = numeroCompte;
        _dateOuverture = DateTime.Now;
        _compteurComptes++;
        
        Console.WriteLine($"Compte {_numeroCompte} créé pour {_titulaire}");
    }
    
    // ==========================================
    // CONSTRUCTEUR 2 : Génère automatiquement le numéro
    // ==========================================
    public CompteBancaire(string titulaire, decimal soldeInitial)
        : this(titulaire, soldeInitial, GenererNumeroCompte())
    {
        Console.WriteLine("Numéro de compte généré automatiquement");
    }
    
    // ==========================================
    // CONSTRUCTEUR 3 : Solde initial de 0$
    // ==========================================
    public CompteBancaire(string titulaire)
        : this(titulaire, 0)
    {
        Console.WriteLine("Compte créé avec solde initial de 0$");
    }
    
    // ==========================================
    // MÉTHODE PRIVÉE pour générer un numéro
    // ==========================================
    private static string GenererNumeroCompte()
    {
        return $"CA{DateTime.Now.Year}{_compteurComptes + 1:D6}";
    }
    
    // ==========================================
    // MÉTHODES
    // ==========================================
    public void Deposer(decimal montant)
    {
        if (montant > 0)
        {
            _solde += montant;
            Console.WriteLine($"Dépôt de {montant:C}. Nouveau solde: {_solde:C}");
        }
    }
    
    public void AfficherInfos()
    {
        Console.WriteLine($"\n=== Compte {_numeroCompte} ===");
        Console.WriteLine($"Titulaire: {_titulaire}");
        Console.WriteLine($"Solde: {_solde:C}");
        Console.WriteLine($"Date d'ouverture: {_dateOuverture:d}");
    }
}

// ==========================================
// UTILISATION
// ==========================================
class Program
{
    static void Main()
    {
        // 3 façons de créer un compte
        CompteBancaire c1 = new CompteBancaire("Marie Tremblay", 1000, "CA2024001");
        CompteBancaire c2 = new CompteBancaire("Jean Gagnon", 500);
        CompteBancaire c3 = new CompteBancaire("Sophie Roy");
        
        c1.AfficherInfos();
        c2.AfficherInfos();
        c3.AfficherInfos();
        
        c3.Deposer(250);
        c3.AfficherInfos();
    }
}
```

---

## 8. L'Héritage

### 8.1 Concept Fondamental

L'**héritage** est un mécanisme qui permet à une classe (appelée **classe dérivée**, **classe enfant** ou **sous-classe**) d'hériter des membres (attributs et méthodes) d'une autre classe (appelée **classe de base**, **classe parent** ou **super-classe**).

**Métaphore biologique :**
- Vous héritez des caractéristiques de vos parents (couleur des yeux, groupe sanguin)
- Mais vous avez aussi vos propres caractéristiques uniques
- C'est la même chose en programmation

**Pourquoi l'héritage ?**
1. **Réutilisation du code** : Ne pas réécrire ce qui existe déjà
2. **Organisation hiérarchique** : Modéliser des relations "est un"
3. **Extensibilité** : Ajouter des fonctionnalités sans modifier l'existant
4. **Polymorphisme** : Traiter différents objets de manière uniforme

### 8.2 Syntaxe de l'Héritage

```csharp
// Syntaxe : class ClasseDerivee : ClasseDeBase
public class Animal
{
    // Classe de base
}

public class Chien : Animal
{
    // Chien hérite de Animal
}
```

### 8.3 Exemple Détaillé : Hiérarchie de Véhicules

```csharp
// ==========================================
// CLASSE DE BASE (PARENT)
// ==========================================
public class Vehicule
{
    // ==========================================
    // MEMBRES PROTÉGÉS - Accessibles aux dérivées
    // ==========================================
    protected string _marque;
    protected string _modele;
    protected int _annee;
    protected double _kilometrage;
    
    // ==========================================
    // PROPRIÉTÉS PUBLIQUES
    // ==========================================
    public string Marque 
    { 
        get { return _marque; } 
        set { _marque = value; }
    }
    
    public string Modele 
    { 
        get { return _modele; } 
        set { _modele = value; }
    }
    
    public int Annee { get; set; }
    
    public double Kilometrage 
    { 
        get { return _kilometrage; }
        protected set // Set protégé
        {
            if (value >= _kilometrage) // Ne peut qu'augmenter
                _kilometrage = value;
        }
    }
    
    // ==========================================
    // CONSTRUCTEUR
    // ==========================================
    public Vehicule(string marque, string modele, int annee)
    {
        Console.WriteLine("Constructeur de Vehicule appelé");
        _marque = marque;
        _modele = modele;
        _annee = annee;
        _kilometrage = 0;
    }
    
    // ==========================================
    // MÉTHODES
    // ==========================================
    public void Demarrer()
    {
        Console.WriteLine($"Le véhicule {_marque} {_modele} démarre.");
    }
    
    public void Rouler(double km)
    {
        _kilometrage += km;
        Console.WriteLine($"Parcouru {km} km. Total: {_kilometrage} km");
    }
    
    public void AfficherInfos()
    {
        Console.WriteLine($"\n=== {_marque} {_modele} ===");
        Console.WriteLine($"Année: {_annee}");
        Console.WriteLine($"Kilométrage: {_kilometrage} km");
    }
}

// ==========================================
// CLASSE DÉRIVÉE 1 (ENFANT)
// ==========================================
public class Voiture : Vehicule // Hérite de Vehicule
{
    // ==========================================
    // MEMBRES SPÉCIFIQUES à Voiture
    // ==========================================
    private int _nombrePortes;
    private string _typeCar burant;
    
    public int NombrePortes 
    { 
        get { return _nombrePortes; } 
        set { _nombrePortes = value; }
    }
    
    // ==========================================
    // CONSTRUCTEUR - Doit appeler le constructeur du parent
    // ==========================================
    public Voiture(string marque, string modele, int annee, int portes, string carburant)
        : base(marque, modele, annee) // Appelle le constructeur de Vehicule
    {
        Console.WriteLine("Constructeur de Voiture appelé");
        _nombrePortes = portes;
        _typeCarburant = carburant;
    }
    
    // ==========================================
    // NOUVELLE MÉTHODE spécifique à Voiture
    // ==========================================
    public void OuvrirCoffre()
    {
        Console.WriteLine("Coffre ouvert");
    }
    
    // ==========================================
    // ACCÈS AUX MEMBRES HÉRITÉS
    // ==========================================
    public void AfficherToutesInfos()
    {
        // Peut accéder aux membres protected et public du parent
        AfficherInfos(); // Méthode héritée
        Console.WriteLine($"Nombre de portes: {_nombrePortes}");
        Console.WriteLine($"Carburant: {_typeCarburant}");
        
        // Peut accéder aux attributs protected
        Console.WriteLine($"Marque (attribut protected): {_marque}");
    }
}

// ==========================================
// CLASSE DÉRIVÉE 2
// ==========================================
public class Moto : Vehicule
{
    private bool _aCompartiment;
    
    public bool ACompartiment { get; set; }
    
    public Moto(string marque, string modele, int annee, bool compartiment)
        : base(marque, modele, annee)
    {
        Console.WriteLine("Constructeur de Moto appelé");
        _aCompartiment = compartiment;
    }
    
    public void FaireWheeling()
    {
        Console.WriteLine($"La moto {Marque} fait un wheeling!");
    }
}

// ==========================================
// CLASSE DÉRIVÉE 3
// ==========================================
public class Camion : Vehicule
{
    private double _capaciteCharge; // En tonnes
    
    public double CapaciteCharge { get; set; }
    
    public Camion(string marque, string modele, int annee, double capacite)
        : base(marque, modele, annee)
    {
        _capaciteCharge = capacite;
    }
    
    public void Charger(double poids)
    {
        if (poids <= _capaciteCharge)
            Console.WriteLine($"Chargement de {poids} tonnes");
        else
            Console.WriteLine($"Dépassement de capacité!");
    }
}

// ==========================================
// UTILISATION
// ==========================================
class Program
{
    static void Main()
    {
        // Création d'objets dérivés
        Voiture voiture = new Voiture("Toyota", "Camry", 2023, 4, "Essence");
        Moto moto = new Moto("Harley-Davidson", "Street 750", 2022, true);
        Camion camion = new Camion("Volvo", "FH16", 2021, 20);
        
        // ==========================================
        // La voiture hérite de TOUT ce que Vehicule possède
        // ==========================================
        voiture.Demarrer();      // ✅ Méthode héritée
        voiture.Rouler(150);     // ✅ Méthode héritée
        voiture.AfficherInfos(); // ✅ Méthode héritée
        voiture.OuvrirCoffre();  // ✅ Méthode spécifique à Voiture
        
        // ==========================================
        // Même chose pour la moto
        // ==========================================
        moto.Demarrer();         // ✅ Méthode héritée
        moto.FaireWheeling();    // ✅ Méthode spécifique à Moto
        
        // ==========================================
        // Et le camion
        // ==========================================
        camion.Demarrer();       // ✅ Méthode héritée
        camion.Charger(15);      // ✅ Méthode spécifique à Camion
    }
}
```

**Affichage :**
```
Constructeur de Vehicule appelé
Constructeur de Voiture appelé
Constructeur de Vehicule appelé
Constructeur de Moto appelé
Constructeur de Vehicule appelé
Le véhicule Toyota Camry démarre.
Parcouru 150 km. Total: 150 km
=== Toyota Camry ===
Année: 2023
Kilométrage: 150 km
Coffre ouvert
...
```

### 8.4 Le Mot-Clé `base`

Le mot-clé `base` permet d'accéder aux membres de la classe parent.

```csharp
public class Animal
{
    protected string _nom;
    protected int _age;
    
    public Animal(string nom, int age)
    {
        _nom = nom;
        _age = age;
        Console.WriteLine($"Animal {nom} créé");
    }
    
    public void Manger()
    {
        Console.WriteLine($"{_nom} mange.");
    }
    
    public virtual void Dormir()
    {
        Console.WriteLine($"{_nom} dort paisiblement.");
    }
}

public class Chien : Animal
{
    private string _race;
    
    // ==========================================
    // UTILISATION 1 de 'base' : Appeler le constructeur parent
    // ==========================================
    public Chien(string nom, int age, string race)
        : base(nom, age) // Appelle Animal(nom, age)
    {
        _race = race;
        Console.WriteLine($"Chien de race {race} créé");
    }
    
    // ==========================================
    // UTILISATION 2 de 'base' : Appeler une méthode du parent
    // ==========================================
    public override void Dormir()
    {
        Console.WriteLine($"{_nom} le chien cherche un endroit confortable");
        base.Dormir(); // Appelle la méthode Dormir() de Animal
        Console.WriteLine("...et ronfle un peu");
    }
    
    public void AfficherTout()
    {
        // ==========================================
        // UTILISATION 3 de 'base' : Accéder à un membre parent
        // ==========================================
        Console.WriteLine($"Nom: {_nom}");        // Attribut hérité
        Console.WriteLine($"Âge: {_age}");        // Attribut hérité
        Console.WriteLine($"Race: {_race}");      // Attribut propre
        
        base.Manger(); // Appelle explicitement la méthode du parent
    }
}

// Utilisation
Chien chien = new Chien("Rex", 5, "Labrador");
chien.Dormir();
```

**Affichage :**
```
Animal Rex créé
Chien de race Labrador créé
Rex le chien cherche un endroit confortable
Rex dort paisiblement.
...et ronfle un peu
```

### 8.5 Hiérarchie Multi-Niveaux

L'héritage peut avoir plusieurs niveaux.

```csharp
// ==========================================
// NIVEAU 1 : Classe de base
// ==========================================
public class EtreVivant
{
    public bool EstVivant { get; set; } = true;
    
    public void Respirer()
    {
        Console.WriteLine("Respire...");
    }
}

// ==========================================
// NIVEAU 2 : Dérive de EtreVivant
// ==========================================
public class Animal : EtreVivant
{
    public void SeDeplacer()
    {
        Console.WriteLine("Se déplace");
    }
}

// ==========================================
// NIVEAU 3 : Dérive de Animal
// ==========================================
public class Mammifere : Animal
{
    public void Allaiter()
    {
        Console.WriteLine("Allaite ses petits");
    }
}

// ==========================================
// NIVEAU 4 : Dérive de Mammifere
// ==========================================
public class Chien : Mammifere
{
    public void Aboyer()
    {
        Console.WriteLine("Wouf wouf!");
    }
}

// Utilisation
Chien chien = new Chien();

// Le chien hérite de TOUS ses ancêtres
chien.Respirer();    // ✅ De EtreVivant (arrière-arrière-grand-parent)
chien.SeDeplacer();  // ✅ De Animal (arrière-grand-parent)
chien.Allaiter();    // ✅ De Mammifere (grand-parent)
chien.Aboyer();      // ✅ De Chien (lui-même)
```

**Hiérarchie :**
```
EtreVivant
    ↓ hérite
Animal
    ↓ hérite
Mammifere
    ↓ hérite
Chien
```

### 8.6 Relations "Est-Un" (Is-A)

L'héritage modélise une relation **"est un"**.

```csharp
public class Vehicule { }
public class Voiture : Vehicule { }

// Une Voiture "est un" Vehicule ✅
// Un Vehicule "est une" Voiture ❌ (faux)

Voiture v = new Voiture();
// v est une Voiture ✅
// v est aussi un Vehicule ✅ (par héritage)
```

**Test mental :** Si vous pouvez dire "X est un Y", alors X peut hériter de Y.
- Un chien **est un** animal ✅
- Une voiture **est un** véhicule ✅
- Un étudiant **est une** personne ✅
- Une maison **est un** bâtiment ✅

**Contre-exemples (mauvais héritage) :**
- Une maison **est un** toit ❌ (une maison **a un** toit → composition, pas héritage)
- Un étudiant **est un** cours ❌
- Une voiture **est un** moteur ❌ (une voiture **a un** moteur)

### 8.7 Restrictions de l'Héritage en C#

**Important :** En C#, une classe ne peut hériter que d'**UNE SEULE** classe (pas d'héritage multiple de classes).

```csharp
public class A { }
public class B { }

// ❌ ERREUR - Pas d'héritage multiple en C#
public class C : A, B { }

// ✅ OK - Héritage simple
public class C : A { }
```

**Cependant :** Une classe peut implémenter **plusieurs interfaces** (voir section sur les interfaces).

---

## 9. Le Polymorphisme

### 9.1 Définition Approfondie

Le **polymorphisme** (du grec "poly" = plusieurs, "morphe" = forme) est la capacité pour des objets de **types différents** de répondre à la **même interface** ou au **même appel de méthode**, mais avec des **comportements différents**.

**Analogie du monde réel :**
Imaginez un bouton "Démarrer" :
- Sur une voiture : Démarre le moteur
- Sur un ordinateur : Lance le système d'exploitation
- Sur une cafetière : Commence à infuser le café
- **Même action ("Démarrer"), comportements différents**

**Pourquoi le polymorphisme ?**
1. **Flexibilité** : Écrire du code qui fonctionne avec différents types
2. **Extensibilité** : Ajouter de nouveaux types sans modifier le code existant
3. **Abstraction** : Manipuler des objets sans connaître leur type exact
4. **Code réutilisable** : Une même fonction pour plusieurs types

### 9.2 Types de Polymorphisme

Il existe deux types principaux :
1. **Polymorphisme de compilation** (surcharge de méthodes)
2. **Polymorphisme d'exécution** (redéfinition de méthodes)

La **Programmation Orientée Objet (POO)** est un paradigme de programmation basé sur le concept d'objets qui contiennent des données (attributs) et du code (méthodes).

### Les 4 piliers de la POO :
- **Encapsulation** : Regrouper les données et méthodes, cacher les détails internes
- **Héritage** : Créer de nouvelles classes à partir de classes existantes
- **Polymorphisme** : Utiliser une interface commune pour des types différents
- **Abstraction** : Simplifier la complexité en cachant les détails d'implémentation

---

## 2. Les Classes et les Objets

### Qu'est-ce qu'une classe ?
Une classe est un **modèle** ou un **plan** qui définit la structure et le comportement d'objets.

### Qu'est-ce qu'un objet ?
Un objet est une **instance** d'une classe, c'est une entité concrète créée à partir du modèle.

### Syntaxe de base

```csharp
// Définition d'une classe
public class Personne
{
    // Attributs (champs)
    public string Nom;
    public string Prenom;
    public int Age;
    
    // Méthode
    public void SePresenter()
    {
        Console.WriteLine($"Bonjour, je m'appelle {Prenom} {Nom} et j'ai {Age} ans.");
    }
}

// Utilisation
class Program
{
    static void Main()
    {
        // Création d'un objet (instance)
        Personne personne1 = new Personne();
        personne1.Nom = "Tremblay";
        personne1.Prenom = "Marie";
        personne1.Age = 25;
        
        personne1.SePresenter(); // Affiche: Bonjour, je m'appelle Marie Tremblay et j'ai 25 ans.
    }
}
```

---

## 3. L'Encapsulation

L'encapsulation consiste à **protéger** les données d'une classe et à contrôler l'accès via des propriétés.

### Modificateurs d'accès

| Modificateur | Description |
|--------------|-------------|
| `public` | Accessible partout |
| `private` | Accessible uniquement dans la classe |
| `protected` | Accessible dans la classe et ses dérivées |
| `internal` | Accessible dans le même assembly |
| `protected internal` | Combinaison de protected et internal |

### Propriétés (Properties)

```csharp
public class Compte
{
    // Champs privés
    private string _numeroCompte;
    private decimal _solde;
    
    // Propriété avec get et set
    public string NumeroCompte
    {
        get { return _numeroCompte; }
        set { _numeroCompte = value; }
    }
    
    // Propriété avec logique de validation
    public decimal Solde
    {
        get { return _solde; }
        private set // set privé : lecture publique, écriture privée
        {
            if (value >= 0)
                _solde = value;
        }
    }
    
    // Propriété auto-implémentée (C# 3.0+)
    public string Titulaire { get; set; }
    
    // Propriété en lecture seule (C# 6.0+)
    public DateTime DateCreation { get; } = DateTime.Now;
    
    public void Deposer(decimal montant)
    {
        if (montant > 0)
            Solde += montant;
    }
    
    public bool Retirer(decimal montant)
    {
        if (montant > 0 && montant <= Solde)
        {
            Solde -= montant;
            return true;
        }
        return false;
    }
}
```

---

## 4. Les Constructeurs

Un constructeur est une méthode spéciale appelée lors de la création d'un objet.

### Types de constructeurs

```csharp
public class Voiture
{
    public string Marque { get; set; }
    public string Modele { get; set; }
    public int Annee { get; set; }
    
    // Constructeur par défaut (sans paramètres)
    public Voiture()
    {
        Marque = "Inconnue";
        Modele = "Inconnu";
        Annee = 2020;
    }
    
    // Constructeur avec paramètres
    public Voiture(string marque, string modele)
    {
        Marque = marque;
        Modele = modele;
        Annee = DateTime.Now.Year;
    }
    
    // Constructeur complet
    public Voiture(string marque, string modele, int annee)
    {
        Marque = marque;
        Modele = modele;
        Annee = annee;
    }
    
    // Chaînage de constructeurs avec 'this'
    public Voiture(string marque) : this(marque, "Standard", DateTime.Now.Year)
    {
    }
}

// Utilisation
var voiture1 = new Voiture();
var voiture2 = new Voiture("Toyota", "Camry");
var voiture3 = new Voiture("Honda", "Civic", 2023);
var voiture4 = new Voiture("Ford");
```

---

## 5. L'Héritage

L'héritage permet à une classe (classe dérivée) d'hériter des membres d'une autre classe (classe de base).

### Syntaxe de base

```csharp
// Classe de base (parent)
public class Animal
{
    public string Nom { get; set; }
    public int Age { get; set; }
    
    public virtual void Manger()
    {
        Console.WriteLine($"{Nom} est en train de manger.");
    }
    
    public virtual void Dormir()
    {
        Console.WriteLine($"{Nom} dort.");
    }
}

// Classe dérivée (enfant)
public class Chien : Animal
{
    public string Race { get; set; }
    
    // Nouvelle méthode spécifique au chien
    public void Aboyer()
    {
        Console.WriteLine($"{Nom} aboie: Wouf wouf!");
    }
    
    // Redéfinition (override) d'une méthode
    public override void Manger()
    {
        Console.WriteLine($"{Nom} le chien mange des croquettes.");
    }
}

// Autre classe dérivée
public class Chat : Animal
{
    public bool EstDomestique { get; set; }
    
    public void Miauler()
    {
        Console.WriteLine($"{Nom} miaule: Miaou!");
    }
    
    public override void Manger()
    {
        Console.WriteLine($"{Nom} le chat mange du poisson.");
    }
}

// Utilisation
var chien = new Chien 
{ 
    Nom = "Rex", 
    Age = 5, 
    Race = "Labrador" 
};
chien.Manger();  // Affiche: Rex le chien mange des croquettes.
chien.Aboyer();  // Affiche: Rex aboie: Wouf wouf!
chien.Dormir();  // Affiche: Rex dort.
```

### Le mot-clé `base`

```csharp
public class Employe
{
    public string Nom { get; set; }
    public decimal SalaireBase { get; set; }
    
    public Employe(string nom, decimal salaire)
    {
        Nom = nom;
        SalaireBase = salaire;
    }
    
    public virtual decimal CalculerSalaire()
    {
        return SalaireBase;
    }
}

public class Manager : Employe
{
    public decimal Prime { get; set; }
    
    // Appel du constructeur de base avec 'base'
    public Manager(string nom, decimal salaire, decimal prime) 
        : base(nom, salaire)
    {
        Prime = prime;
    }
    
    // Utilisation de la méthode de base avec 'base'
    public override decimal CalculerSalaire()
    {
        return base.CalculerSalaire() + Prime;
    }
}
```

### Modificateur `sealed`

Le mot-clé `sealed` empêche l'héritage d'une classe ou la redéfinition d'une méthode.

```csharp
// Classe scellée - ne peut pas être héritée
public sealed class ClasseFinale
{
    public void Methode() { }
}

// ERREUR: Impossible d'hériter d'une classe sealed
// public class Derivee : ClasseFinale { }

public class ClasseAvecMethodeScelle : Animal
{
    // Méthode scellée - ne peut plus être redéfinie dans les classes dérivées
    public sealed override void Manger()
    {
        Console.WriteLine("Implémentation finale");
    }
}
```

---

## 6. Le Polymorphisme

Le polymorphisme permet à des objets de différentes classes d'être traités de manière uniforme via une interface commune.

### Polymorphisme par héritage

```csharp
public class Forme
{
    public virtual double CalculerAire()
    {
        return 0;
    }
    
    public virtual void Dessiner()
    {
        Console.WriteLine("Dessiner une forme générique");
    }
}

public class Cercle : Forme
{
    public double Rayon { get; set; }
    
    public Cercle(double rayon)
    {
        Rayon = rayon;
    }
    
    public override double CalculerAire()
    {
        return Math.PI * Rayon * Rayon;
    }
    
    public override void Dessiner()
    {
        Console.WriteLine($"Dessiner un cercle de rayon {Rayon}");
    }
}

public class Rectangle : Forme
{
    public double Longueur { get; set; }
    public double Largeur { get; set; }
    
    public Rectangle(double longueur, double largeur)
    {
        Longueur = longueur;
        Largeur = largeur;
    }
    
    public override double CalculerAire()
    {
        return Longueur * Largeur;
    }
    
    public override void Dessiner()
    {
        Console.WriteLine($"Dessiner un rectangle {Longueur}x{Largeur}");
    }
}

// Utilisation du polymorphisme
class Program
{
    static void Main()
    {
        // Tableau polymorphe
        Forme[] formes = new Forme[]
        {
            new Cercle(5),
            new Rectangle(4, 6),
            new Cercle(3)
        };
        
        // Même code, comportements différents
        foreach (Forme forme in formes)
        {
            forme.Dessiner();
            Console.WriteLine($"Aire: {forme.CalculerAire():F2}");
            Console.WriteLine();
        }
    }
}
```

### Mots-clés `virtual`, `override`, `new`

```csharp
public class ClasseBase
{
    public virtual void MethodeVirtuelle()
    {
        Console.WriteLine("Méthode virtuelle de base");
    }
    
    public void MethodeNormale()
    {
        Console.WriteLine("Méthode normale de base");
    }
}

public class ClasseDerivee : ClasseBase
{
    // Override: redéfinition polymorphique
    public override void MethodeVirtuelle()
    {
        Console.WriteLine("Méthode redéfinie");
    }
    
    // New: masquage (non polymorphique)
    public new void MethodeNormale()
    {
        Console.WriteLine("Méthode masquée");
    }
}

// Test
ClasseBase obj1 = new ClasseDerivee();
obj1.MethodeVirtuelle(); // Affiche: Méthode redéfinie (polymorphisme)
obj1.MethodeNormale();   // Affiche: Méthode normale de base (pas de polymorphisme)

ClasseDerivee obj2 = new ClasseDerivee();
obj2.MethodeVirtuelle(); // Affiche: Méthode redéfinie
obj2.MethodeNormale();   // Affiche: Méthode masquée
```

---

## 7. Les Classes Abstraites

Une classe abstraite est une classe **incomplète** qui ne peut pas être instanciée directement. Elle sert de modèle pour d'autres classes.

### Caractéristiques
- Déclarée avec le mot-clé `abstract`
- Peut contenir des méthodes abstraites (sans implémentation) et concrètes (avec implémentation)
- Ne peut pas être instanciée
- Les classes dérivées **doivent** implémenter toutes les méthodes abstraites

```csharp
// Classe abstraite
public abstract class Vehicule
{
    public string Marque { get; set; }
    public string Modele { get; set; }
    
    // Constructeur (oui, les classes abstraites peuvent avoir des constructeurs)
    public Vehicule(string marque, string modele)
    {
        Marque = marque;
        Modele = modele;
    }
    
    // Méthode abstraite (sans implémentation)
    public abstract void Demarrer();
    
    // Méthode abstraite
    public abstract double CalculerConsommation(double distance);
    
    // Méthode concrète (avec implémentation)
    public void AfficherInfo()
    {
        Console.WriteLine($"Véhicule: {Marque} {Modele}");
    }
    
    // Méthode virtuelle (peut être redéfinie)
    public virtual void Klaxonner()
    {
        Console.WriteLine("Beep beep!");
    }
}

// Classe dérivée - doit implémenter toutes les méthodes abstraites
public class Voiture : Vehicule
{
    public int NombrePortes { get; set; }
    
    public Voiture(string marque, string modele, int portes) 
        : base(marque, modele)
    {
        NombrePortes = portes;
    }
    
    // Implémentation obligatoire
    public override void Demarrer()
    {
        Console.WriteLine($"La voiture {Marque} {Modele} démarre avec la clé.");
    }
    
    // Implémentation obligatoire
    public override double CalculerConsommation(double distance)
    {
        return distance * 0.07; // 7L/100km
    }
}

public class Moto : Vehicule
{
    public bool ASidecar { get; set; }
    
    public Moto(string marque, string modele) 
        : base(marque, modele)
    {
    }
    
    public override void Demarrer()
    {
        Console.WriteLine($"La moto {Marque} {Modele} démarre avec le kick.");
    }
    
    public override double CalculerConsommation(double distance)
    {
        return distance * 0.04; // 4L/100km
    }
    
    public override void Klaxonner()
    {
        Console.WriteLine("Beep beep! (son de moto)");
    }
}

// Utilisation
// var v = new Vehicule("Test", "Test"); // ERREUR: impossible d'instancier une classe abstraite

var voiture = new Voiture("Toyota", "Corolla", 4);
voiture.AfficherInfo();
voiture.Demarrer();
Console.WriteLine($"Consommation sur 100km: {voiture.CalculerConsommation(100)}L");

var moto = new Moto("Harley", "Davidson");
moto.Demarrer();
```

### Exemple avancé : Système de paiement

```csharp
public abstract class MoyenPaiement
{
    public string Titulaire { get; set; }
    public DateTime DateTransaction { get; protected set; }
    
    public abstract bool Payer(decimal montant);
    public abstract bool Verifier();
    
    public virtual void AfficherRecu(decimal montant)
    {
        Console.WriteLine($"Reçu - {Titulaire}");
        Console.WriteLine($"Montant: {montant:C}");
        Console.WriteLine($"Date: {DateTransaction}");
    }
}

public class CarteCredit : MoyenPaiement
{
    public string Numero { get; set; }
    public DateTime DateExpiration { get; set; }
    public decimal LimiteCredit { get; set; }
    private decimal _soldeUtilise;
    
    public override bool Verifier()
    {
        return DateExpiration > DateTime.Now;
    }
    
    public override bool Payer(decimal montant)
    {
        if (!Verifier())
        {
            Console.WriteLine("Carte expirée");
            return false;
        }
        
        if (_soldeUtilise + montant > LimiteCredit)
        {
            Console.WriteLine("Limite de crédit dépassée");
            return false;
        }
        
        _soldeUtilise += montant;
        DateTransaction = DateTime.Now;
        Console.WriteLine($"Paiement de {montant:C} effectué par carte de crédit");
        return true;
    }
}

public class Paypal : MoyenPaiement
{
    public string Email { get; set; }
    public decimal Solde { get; private set; }
    
    public void Recharger(decimal montant)
    {
        Solde += montant;
    }
    
    public override bool Verifier()
    {
        return !string.IsNullOrEmpty(Email) && Email.Contains("@");
    }
    
    public override bool Payer(decimal montant)
    {
        if (!Verifier())
        {
            Console.WriteLine("Email invalide");
            return false;
        }
        
        if (Solde < montant)
        {
            Console.WriteLine("Solde insuffisant");
            return false;
        }
        
        Solde -= montant;
        DateTransaction = DateTime.Now;
        Console.WriteLine($"Paiement de {montant:C} effectué via PayPal");
        return true;
    }
}
```

---

## 8. Les Interfaces

Une interface définit un **contrat** que les classes doivent respecter. Elle ne contient que des signatures de méthodes, propriétés, événements ou indexeurs (pas d'implémentation).

### Caractéristiques
- Déclarée avec le mot-clé `interface`
- Tous les membres sont publics par défaut
- Une classe peut implémenter plusieurs interfaces (contrairement à l'héritage)
- Pas de champs, pas de constructeurs
- Convention de nommage : préfixe `I` (ex: `IComparable`)

```csharp
// Définition d'interfaces
public interface IVolant
{
    void Voler();
    double AltitudeMax { get; }
}

public interface INageant
{
    void Nager();
    double ProfondeurMax { get; }
}

public interface IMarchant
{
    void Marcher();
}

// Classe implémentant une interface
public class Oiseau : IVolant, IMarchant
{
    public string Nom { get; set; }
    
    // Implémentation de IVolant
    public double AltitudeMax { get; set; } = 1000;
    
    public void Voler()
    {
        Console.WriteLine($"{Nom} vole dans le ciel.");
    }
    
    // Implémentation de IMarchant
    public void Marcher()
    {
        Console.WriteLine($"{Nom} marche sur le sol.");
    }
}

public class Canard : IVolant, INageant, IMarchant
{
    public string Nom { get; set; }
    
    public double AltitudeMax { get; set; } = 500;
    public double ProfondeurMax { get; set; } = 10;
    
    public void Voler()
    {
        Console.WriteLine($"{Nom} le canard vole.");
    }
    
    public void Nager()
    {
        Console.WriteLine($"{Nom} le canard nage.");
    }
    
    public void Marcher()
    {
        Console.WriteLine($"{Nom} le canard marche.");
    }
}

public class Poisson : INageant
{
    public string Nom { get; set; }
    public double ProfondeurMax { get; set; } = 100;
    
    public void Nager()
    {
        Console.WriteLine($"{Nom} le poisson nage sous l'eau.");
    }
}

// Utilisation polymorphe des interfaces
class Program
{
    static void FaireVoler(IVolant volant)
    {
        volant.Voler();
        Console.WriteLine($"Altitude maximale: {volant.AltitudeMax}m");
    }
    
    static void FaireNager(INageant nageant)
    {
        nageant.Nager();
        Console.WriteLine($"Profondeur maximale: {nageant.ProfondeurMax}m");
    }
    
    static void Main()
    {
        var oiseau = new Oiseau { Nom = "Aigle" };
        var canard = new Canard { Nom = "Donald" };
        var poisson = new Poisson { Nom = "Nemo" };
        
        FaireVoler(oiseau);
        FaireVoler(canard);
        
        FaireNager(canard);
        FaireNager(poisson);
        
        // Collection polymorphe
        List<INageant> animauxAquatiques = new List<INageant>
        {
            canard,
            poisson
        };
        
        foreach (var animal in animauxAquatiques)
        {
            animal.Nager();
        }
    }
}
```

### Interfaces vs Classes Abstraites

| Aspect | Interface | Classe Abstraite |
|--------|-----------|------------------|
| Héritage multiple | ✅ Oui (une classe peut implémenter plusieurs interfaces) | ❌ Non (une classe ne peut hériter que d'une seule classe) |
| Implémentation | ❌ Aucune (sauf depuis C# 8.0 avec implémentation par défaut) | ✅ Peut contenir des méthodes implémentées |
| Champs | ❌ Non | ✅ Oui |
| Constructeurs | ❌ Non | ✅ Oui |
| Modificateurs d'accès | Tous publics | Peut varier |
| Utilisation | Définir un contrat/comportement | Définir une base commune avec du code partagé |

### Exemple pratique : Système de notification

```csharp
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

public class NotificationEmail : INotifiable, IConfigurable
{
    public bool EstActif { get; set; } = true;
    public string AdresseEmail { get; set; }
    public string ServeurSMTP { get; set; }
    
    public void EnvoyerNotification(string message)
    {
        if (EstActif)
        {
            Console.WriteLine($"📧 Email envoyé à {AdresseEmail}: {message}");
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

public class NotificationSMS : INotifiable, IConfigurable
{
    public bool EstActif { get; set; } = true;
    public string NumeroTelephone { get; set; }
    public string FournisseurSMS { get; set; }
    
    public void EnvoyerNotification(string message)
    {
        if (EstActif)
        {
            Console.WriteLine($"📱 SMS envoyé au {NumeroTelephone}: {message}");
        }
    }
    
    public void Configurer(Dictionary<string, string> parametres)
    {
        if (parametres.ContainsKey("telephone"))
            NumeroTelephone = parametres["telephone"];
        if (parametres.ContainsKey("fournisseur"))
            FournisseurSMS = parametres["fournisseur"];
    }
    
    public Dictionary<string, string> ObtenirConfiguration()
    {
        return new Dictionary<string, string>
        {
            { "telephone", NumeroTelephone },
            { "fournisseur", FournisseurSMS }
        };
    }
}

public class GestionnaireNotifications
{
    private List<INotifiable> _canaux = new List<INotifiable>();
    
    public void AjouterCanal(INotifiable canal)
    {
        _canaux.Add(canal);
    }
    
    public void EnvoyerATous(string message)
    {
        foreach (var canal in _canaux)
        {
            canal.EnvoyerNotification(message);
        }
    }
}
```

### Implémentation explicite d'interface

Utilisée pour éviter les conflits de noms entre plusieurs interfaces.

```csharp
public interface IAnimal
{
    void Manger();
}

public interface IRobot
{
    void Manger(); // Même nom de méthode
}

public class CyberChien : IAnimal, IRobot
{
    // Implémentation explicite de IAnimal.Manger
    void IAnimal.Manger()
    {
        Console.WriteLine("Le cyber-chien mange de la vraie nourriture");
    }
    
    // Implémentation explicite de IRobot.Manger
    void IRobot.Manger()
    {
        Console.WriteLine("Le cyber-chien recharge ses batteries");
    }
    
    // Méthode publique normale
    public void SeReposer()
    {
        Console.WriteLine("Le cyber-chien se met en veille");
    }
}

// Utilisation
var cyberChien = new CyberChien();
// cyberChien.Manger(); // ERREUR: ambiguïté

IAnimal animal = cyberChien;
animal.Manger(); // Appelle IAnimal.Manger

IRobot robot = cyberChien;
robot.Manger(); // Appelle IRobot.Manger

cyberChien.SeReposer(); // OK
```

---

## 9. Concepts Avancés

### 9.1 Membres statiques

Les membres statiques appartiennent à la **classe** plutôt qu'à une instance.

```csharp
public class Compteur
{
    // Champ statique (partagé par toutes les instances)
    private static int _nombreInstances = 0;
    
    // Propriété statique
    public static int NombreInstances 
    { 
        get { return _nombreInstances; } 
    }
    
    // Champ d'instance
    public int Id { get; private set; }
    
    // Constructeur
    public Compteur()
    {
        _nombreInstances++;
        Id = _nombreInstances;
    }
    
    // Méthode statique
    public static void Reinitialiser()
    {
        _nombreInstances = 0;
    }
    
    // Méthode d'instance
    public void AfficherInfo()
    {
        Console.WriteLine($"Instance #{Id} - Total: {NombreInstances}");
    }
}

// Constructeur statique (appelé une seule fois avant la première utilisation)
public class Configuration
{
    public static string CheminFichier { get; private set; }
    
    static Configuration()
    {
        Console.WriteLine("Initialisation de la configuration...");
        CheminFichier = "config.json";
    }
}

// Utilisation
var c1 = new Compteur(); // NombreInstances = 1
var c2 = new Compteur(); // NombreInstances = 2
var c3 = new Compteur(); // NombreInstances = 3

Console.WriteLine(Compteur.NombreInstances); // 3
c2.AfficherInfo(); // Instance #2 - Total: 3
```

### 9.2 Classes statiques

Une classe statique ne peut contenir que des membres statiques et ne peut pas être instanciée.

```csharp
public static class Utilitaires
{
    public static double CalculerMoyenne(params double[] nombres)
    {
        if (nombres.Length == 0) return 0;
        return nombres.Average();
    }
    
    public static string FormaterMonnaie(decimal montant)
    {
        return $"{montant:C}";
    }
    
    public static T Max<T>(T a, T b) where T : IComparable<T>
    {
        return a.CompareTo(b) > 0 ? a : b;
    }
}

// Utilisation
double moyenne = Utilitaires.CalculerMoyenne(10, 20, 30, 40);
string prix = Utilitaires.FormaterMonnaie(99.99m);
int maximum = Utilitaires.Max(5, 10);
```

### 9.3 Classes partielles (Partial Classes)

Permettent de diviser la définition d'une classe en plusieurs fichiers.

```csharp
// Fichier: Personne.cs
public partial class Personne
{
    public string Nom { get; set; }
    public string Prenom { get; set; }
    
    partial void OnNomChanged();
}

// Fichier: Personne.Methodes.cs
public partial class Personne
{
    public void SePresenter()
    {
        Console.WriteLine($"Je suis {Prenom} {Nom}");
    }
    
    partial void OnNomChanged()
    {
        Console.WriteLine("Le nom a été modifié");
    }
}
```

### 9.4 Délégués et Événements (aperçu)

```csharp
// Délégué (type pointeur de fonction)
public delegate void NotificationHandler(string message);

public class Compte
{
    // Événement
    public event NotificationHandler SoldeModifie;
    
    private decimal _solde;
    
    public decimal Solde
    {
        get { return _solde; }
        set
        {
            _solde = value;
            // Déclencher l'événement
            SoldeModifie?.Invoke($"Nouveau solde: {_solde:C}");
        }
    }
}

// Utilisation
var compte = new Compte();
compte.SoldeModifie += (msg) => Console.WriteLine($"Notification: {msg}");
compte.Solde = 100; // Déclenche l'événement
```

### 9.5 Génériques (Generics)

```csharp
// Classe générique
public class Boite<T>
{
    private T _contenu;
    
    public void Ranger(T item)
    {
        _contenu = item;
    }
    
    public T Recuperer()
    {
        return _contenu;
    }
}

// Utilisation
var boiteEntiers = new Boite<int>();
boiteEntiers.Ranger(42);
int nombre = boiteEntiers.Recuperer();

var boiteTexte = new Boite<string>();
boiteTexte.Ranger("Bonjour");
string texte = boiteTexte.Recuperer();

// Classe générique avec contraintes
public class Repository<T> where T : class, new()
{
    private List<T> _items = new List<T>();
    
    public void Ajouter(T item)
    {
        _items.Add(item);
    }
    
    public T Creer()
    {
        return new T(); // Possible grâce à la contrainte 'new()'
    }
}
```

### 9.6 Extension Methods

```csharp
// Classe statique pour les méthodes d'extension
public static class StringExtensions
{
    // Méthode d'extension (noter le 'this' devant le premier paramètre)
    public static bool EstEmail(this string texte)
    {
        return texte.Contains("@") && texte.Contains(".");
    }
    
    public static string Inverser(this string texte)
    {
        char[] chars = texte.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }
    
    public static int CompterMots(this string texte)
    {
        return texte.Split(new[] { ' ', '\t', '\n' }, 
            StringSplitOptions.RemoveEmptyEntries).Length;
    }
}

// Utilisation
string email = "test@example.com";
bool valide = email.EstEmail(); // true

string mot = "Bonjour";
string inverse = mot.Inverser(); // "ruojnoB"

string phrase = "Ceci est une phrase";
int mots = phrase.CompterMots(); // 4
```

### 9.7 Records (C# 9.0+)

Les records sont des types de référence immuables optimisés pour stocker des données.

```csharp
// Record simple
public record Personne(string Nom, string Prenom, int Age);

// Utilisation
var p1 = new Personne("Tremblay", "Marie", 25);
var p2 = new Personne("Tremblay", "Marie", 25);

Console.WriteLine(p1 == p2); // true (égalité par valeur)

// Expression 'with' pour créer une copie modifiée
var p3 = p1 with { Age = 26 };

// Record avec propriétés additionnelles
public record Employe(string Nom, string Prenom, decimal Salaire)
{
    public string Departement { get; init; } = "Non assigné";
    
    public decimal CalculerSalaireAnnuel() => Salaire * 12;
}
```

---

## Résumé des concepts clés

### Quand utiliser quoi ?

| Concept | Utilisation |
|---------|-------------|
| **Classe normale** | Objets avec état et comportement |
| **Classe abstraite** | Base commune avec implémentation partielle |
| **Interface** | Contrat sans implémentation, héritage multiple |
| **Classe statique** | Méthodes utilitaires sans état |
| **Record** | Données immuables |
| **Sealed class** | Empêcher l'héritage |
| **Partial class** | Diviser une classe en plusieurs fichiers |

### Modificateurs d'accès (du plus au moins restrictif)

1. `private` - Classe uniquement
2. `protected` - Classe et dérivées
3. `internal` - Assembly actuel
4. `protected internal` - Assembly ou dérivées
5. `public` - Partout

### Principes SOLID

1. **S**ingle Responsibility: Une classe = une responsabilité
2. **O**pen/Closed: Ouvert à l'extension, fermé à la modification
3. **L**iskov Substitution: Les sous-classes doivent pouvoir remplacer leurs classes de base
4. **I**nterface Segregation: Interfaces petites et spécifiques
5. **D**ependency Inversion: Dépendre des abstractions, pas des implémentations concrètes

---

## Exercices pratiques

### Exercice 1 : Créer une hiérarchie de comptes bancaires
Créez une classe de base `CompteBancaire` et des classes dérivées `CompteEpargne` et `CompteCourant` avec des comportements différents.

### Exercice 2 : Système de formes géométriques
Créez une classe abstraite `Forme` avec des méthodes pour calculer l'aire et le périmètre. Implémentez des classes concrètes comme `Cercle`, `Carre`, `Triangle`.

### Exercice 3 : Gestion d'une bibliothèque
Utilisez des interfaces `IEmpruntable`, `IReservable` pour créer un système de gestion de livres, DVD, magazines.

### Exercice 4 : Simulateur de zoo
Créez une hiérarchie d'animaux avec des interfaces pour différents comportements (voler, nager, grimper).
