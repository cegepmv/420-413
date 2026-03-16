---
title: "Patron commande"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 17
---

<!--

## Le patron Commande (Contexte général)

### Le problème qu'il résout

**Mise en situation : Un éditeur de texte**

Vous développez un éditeur de texte (comme Notepad). L'utilisateur peut :
- Couper du texte
- Copier du texte
- Coller du texte
- Annuler la dernière action
- Refaire une action annulée

**Question :** Comment organiser tout ça ?

**❌ Approche naïve (code spaghetti) :**

```csharp
private void btnCouper_Click(object sender, EventArgs e)
{
    // Code pour couper
    string texteSelectionne = txtEditeur.SelectedText;
    presse-papiers = texteSelectionne;
    txtEditeur.SelectedText = "";
    
    // Ajouter à l'historique pour annuler
    historique.Add(new ActionCouper { Texte = texteSelectionne, Position = ... });
}

private void btnCopier_Click(object sender, EventArgs e)
{
    // Code pour copier
    presse-papiers = txtEditeur.SelectedText;
    
    // Ajouter à l'historique
    historique.Add(new ActionCopier { Texte = ... });
}

private void btnColler_Click(object sender, EventArgs e)
{
    // Code pour coller
    txtEditeur.SelectedText = presse-papiers;
    
    // Ajouter à l'historique
    historique.Add(new ActionColler { Texte = presse-papiers, Position = ... });
}

private void btnAnnuler_Click(object sender, EventArgs e)
{
    var derniere = historique.Last();
    
    // 😱 Comment annuler selon le type d'action ?
    if (derniere is ActionCouper)
    {
        // Code pour défaire une coupe
    }
    else if (derniere is ActionCopier)
    {
        // Code pour défaire une copie
    }
    else if (derniere is ActionColler)
    {
        // Code pour défaire un collage
    }
    // ... et ainsi de suite pour chaque type d'action !
}
```

**Problèmes :**
1. **Code dupliqué** : La logique d'annulation est éparpillée partout
2. **Difficile à tester** : Impossible de tester une action sans l'interface
3. **Pas réutilisable** : Si on ajoute un raccourci clavier, il faut dupliquer le code
4. **Couplage fort** : Les boutons sont directement couplés à la logique métier
5. **Difficile à étendre** : Ajouter une nouvelle action nécessite de modifier plein d'endroits

### Le patron Commande — La solution

**Principe central :** Encapsuler une action dans un objet.

**Analogie :** Pensez à une télécommande TV :
- Chaque **bouton** est une commande (Volume+, Volume-, Chaine+, etc.)
- La télécommande ne sait pas comment augmenter le volume
- Elle envoie juste la commande "Volume+" à la TV
- La TV exécute la commande
- On peut **enregistrer** les commandes (macro)
- On peut **annuler** des commandes

**Diagramme conceptuel :**

```
┌─────────────────────────────┐
│     INTERFACE COMMANDE      │
│  + Executer()               │
│  + Annuler()                │
└─────────────────────────────┘
              △
              │ implémente
     ┌────────┴────────┬────────────┐
     │                 │            │
┌─────────┐      ┌──────────┐  ┌─────────┐
│Commande │      │Commande  │  │Commande │
│Couper   │      │Copier    │  │Coller   │
└─────────┘      └──────────┘  └─────────┘

┌─────────────────────────────┐
│        INVOCATEUR           │
│   (Bouton, raccourci)       │
│  - commande: ICommande      │
│  + AppuiSurBouton()         │
└─────────────────────────────┘
              │
              │ utilise
              ↓
┌─────────────────────────────┐
│        RÉCEPTEUR            │
│      (EditeurTexte)         │
│  + Couper()                 │
│  + Copier()                 │
│  + Coller()                 │
└─────────────────────────────┘
```

### Implémentation complète

**Étape 1 : Interface Commande**

```csharp
public interface ICommande
{
    void Executer();
    void Annuler();
}
```

**Étape 2 : Le Récepteur (celui qui fait le vrai travail)**

```csharp
public class EditeurTexte
{
    private StringBuilder contenu = new StringBuilder();
    private string presse_papiers = "";
    
    public string Contenu => contenu.ToString();
    
    public void Couper(int debut, int longueur)
    {
        presse_papiers = contenu.ToString(debut, longueur);
        contenu.Remove(debut, longueur);
        Console.WriteLine($"✂️  Texte coupé : '{presse_papiers}'");
    }
    
    public void Copier(int debut, int longueur)
    {
        presse_papiers = contenu.ToString(debut, longueur);
        Console.WriteLine($"📋 Texte copié : '{presse_papiers}'");
    }
    
    public void Coller(int position)
    {
        contenu.Insert(position, presse_papiers);
        Console.WriteLine($"📌 Texte collé : '{presse_papiers}' à la position {position}");
    }
    
    public void AjouterTexte(string texte)
    {
        contenu.Append(texte);
    }
    
    public void Afficher()
    {
        Console.WriteLine($"📄 Contenu : '{Contenu}'");
    }
}
```

**Étape 3 : Les Commandes concrètes**

```csharp
// Commande Couper
public class CommandeCouper : ICommande
{
    private EditeurTexte editeur;
    private int debut;
    private int longueur;
    private string texteCoupe;  // Pour pouvoir annuler
    
    public CommandeCouper(EditeurTexte editeur, int debut, int longueur)
    {
        this.editeur = editeur;
        this.debut = debut;
        this.longueur = longueur;
    }
    
    public void Executer()
    {
        texteCoupe = editeur.Contenu.Substring(debut, longueur);
        editeur.Couper(debut, longueur);
    }
    
    public void Annuler()
    {
        // Restaurer le texte coupé
        editeur.AjouterTexte(texteCoupe);
        Console.WriteLine($"↩️  Annulation : texte '{texteCoupe}' restauré");
    }
}

// Commande Copier
public class CommandeCopier : ICommande
{
    private EditeurTexte editeur;
    private int debut;
    private int longueur;
    
    public CommandeCopier(EditeurTexte editeur, int debut, int longueur)
    {
        this.editeur = editeur;
        this.debut = debut;
        this.longueur = longueur;
    }
    
    public void Executer()
    {
        editeur.Copier(debut, longueur);
    }
    
    public void Annuler()
    {
        // Copier ne modifie pas le texte, donc pas besoin d'annuler
        Console.WriteLine("↩️  Copier annulé (aucun changement au texte)");
    }
}

// Commande Coller
public class CommandeColler : ICommande
{
    private EditeurTexte editeur;
    private int position;
    private int longueurCollee;
    
    public CommandeColler(EditeurTexte editeur, int position)
    {
        this.editeur = editeur;
        this.position = position;
    }
    
    public void Executer()
    {
        // On suppose qu'on connaît la longueur du presse-papiers
        editeur.Coller(position);
    }
    
    public void Annuler()
    {
        // Retirer le texte collé
        Console.WriteLine($"↩️  Annulation du collage");
    }
}
```

**Étape 4 : L'Invocateur (gestionnaire de commandes)**

```csharp
public class GestionnaireCommandes
{
    private Stack<ICommande> historique = new Stack<ICommande>();
    private Stack<ICommande> historiqueAnnule = new Stack<ICommande>();
    
    public void ExecuterCommande(ICommande commande)
    {
        commande.Executer();
        historique.Push(commande);
        
        // Vider l'historique d'annulation (comme dans Word)
        historiqueAnnule.Clear();
    }
    
    public void Annuler()
    {
        if (historique.Count == 0)
        {
            Console.WriteLine("❌ Rien à annuler");
            return;
        }
        
        ICommande commande = historique.Pop();
        commande.Annuler();
        historiqueAnnule.Push(commande);
    }
    
    public void Refaire()
    {
        if (historiqueAnnule.Count == 0)
        {
            Console.WriteLine("❌ Rien à refaire");
            return;
        }
        
        ICommande commande = historiqueAnnule.Pop();
        commande.Executer();
        historique.Push(commande);
    }
}
```

**Étape 5 : Utilisation**

```csharp
class Program
{
    static void Main(string[] args)
    {
        // Créer l'éditeur et le gestionnaire
        EditeurTexte editeur = new EditeurTexte();
        GestionnaireCommandes gestionnaire = new GestionnaireCommandes();
        
        // Ajouter du texte initial
        Console.WriteLine("=== Initialisation ===");
        editeur.AjouterTexte("Bonjour le monde!");
        editeur.Afficher();
        
        // Créer et exécuter des commandes
        Console.WriteLine("\n=== Copier 'Bonjour' ===");
        ICommande copier = new CommandeCopier(editeur, 0, 7);
        gestionnaire.ExecuterCommande(copier);
        
        Console.WriteLine("\n=== Coller à la fin ===");
        ICommande coller = new CommandeColler(editeur, editeur.Contenu.Length);
        gestionnaire.ExecuterCommande(coller);
        editeur.Afficher();
        
        Console.WriteLine("\n=== Couper ' le monde' ===");
        ICommande couper = new CommandeCouper(editeur, 7, 9);
        gestionnaire.ExecuterCommande(couper);
        editeur.Afficher();
        
        Console.WriteLine("\n=== Annuler la dernière action ===");
        gestionnaire.Annuler();
        editeur.Afficher();
        
        Console.WriteLine("\n=== Refaire ===");
        gestionnaire.Refaire();
        editeur.Afficher();
    }
}
```

### Avantages du patron Commande

**1. Découplage**
```csharp
// Le bouton ne connaît PAS l'éditeur
// Il connaît seulement l'interface ICommande
Button btnCouper = new Button();
btnCouper.Command = new CommandeCouper(editeur, 0, 5);
```

**2. Historique automatique**
```csharp
// Toutes les commandes sont automatiquement historisées
// Annuler/Refaire gratuit !
```

**3. Testabilité**
```csharp
[TestMethod]
public void Test_CommandeCouper()
{
    EditeurTexte editeur = new EditeurTexte();
    editeur.AjouterTexte("Test");
    
    CommandeCouper cmd = new CommandeCouper(editeur, 0, 2);
    cmd.Executer();
    
    Assert.AreEqual("st", editeur.Contenu);
}
```

**4. Réutilisabilité**
```csharp
// Même commande utilisable de plusieurs façons
ICommande couper = new CommandeCouper(editeur, 0, 5);

// Via un bouton
button.Command = couper;

// Via un raccourci clavier
if (e.Key == Key.X && e.Control)
    gestionnaire.ExecuterCommande(couper);

// Via un menu
menuItem.Command = couper;
```

**5. Macros et scripts**
```csharp
// On peut créer des séquences de commandes
List<ICommande> macro = new List<ICommande>
{
    new CommandeCopier(editeur, 0, 5),
    new CommandeColler(editeur, 10),
    new CommandeCouper(editeur, 0, 5)
};

// Exécuter la macro
foreach (var cmd in macro)
    gestionnaire.ExecuterCommande(cmd);
```

### Variantes du patron Commande

**1. Commandes paramétrées**

```csharp
public interface ICommande<T>
{
    void Executer(T parametre);
    void Annuler();
}

public class CommandeRechercher : ICommande<string>
{
    public void Executer(string motCle)
    {
        // Rechercher le mot-clé
    }
    
    public void Annuler()
    {
        // Annuler la recherche
    }
}
```

**2. Commandes asynchrones**

```csharp
public interface ICommandeAsync
{
    Task ExecuterAsync();
    Task AnnulerAsync();
}

public class CommandeSauvegarder : ICommandeAsync
{
    public async Task ExecuterAsync()
    {
        await SauvegarderDansFichierAsync();
    }
    
    public async Task AnnulerAsync()
    {
        await RestaurerVersionPrecedenteAsync();
    }
}
```

### Le  (aperçu)

**WPF utilise le patron Commande !**

```csharp
// ICommand dans WPF
public interface ICommand
{
    bool CanExecute(object parameter);  // Peut-on exécuter ?
    void Execute(object parameter);      // Exécuter
    event EventHandler CanExecuteChanged; // Notifier changement d'état
}
```

**Différences avec notre version :**
- WPF n'a pas de méthode `Annuler()` (on peut l'ajouter)
- WPF a `CanExecute()` pour activer/désactiver les boutons
- WPF a un événement pour notifier les changements d'état

**Utilisation en WPF :**
```xml
<Button Content="Sauvegarder" Command="{Binding SauvegarderCommand}" />
```

Quand le bouton est cliqué :
1. WPF vérifie `CanExecute()` → Active/désactive le bouton
2. WPF appelle `Execute()` → Exécute l'action

**C'est exactement le patron Commande appliqué à WPF !**

---
-->

---

## 🎯 Objectifs du cours

À la fin de ce cours, vous serez capable de :
- Comprendre le problème que résout le patron Commande
- Implémenter le patron Commande en C#
- Créer un système d'annulation/refaire (Undo/Redo)
- Appliquer ce patron dans vos projets WPF

---

## 📌 Partie 1 : Comprendre le problème

### L'analogie de la télécommande TV 🎮

Imaginez une télécommande de télévision :
- Elle a des boutons : Volume +, Volume -, Chaîne +, Chaîne -, etc.
- **Quand vous appuyez sur "Volume +"**, la télécommande ne fait PAS elle-même le travail
- Elle **envoie une commande** à la TV qui fait le travail

**Pourquoi c'est génial ?**
- ✅ La télécommande peut contrôler plusieurs appareils (TV, lecteur DVD, etc.)
- ✅ On peut programmer des macros (un bouton qui fait plusieurs actions)
- ✅ On peut changer ce que fait un bouton sans changer la télécommande

### Le problème concret : Un éditeur de texte

Vous créez un éditeur de texte simple. L'utilisateur peut :
- Écrire du texte
- **Annuler** sa dernière action (Ctrl+Z)
- **Refaire** une action annulée (Ctrl+Y)

**❌ Mauvaise approche :**

```csharp
private void btnEcrire_Click(object sender, EventArgs e)
{
    texte += "Bonjour";
    // Comment on annule ça maintenant ??? 😱
}

private void btnAnnuler_Click(object sender, EventArgs e)
{
    // Impossible de savoir quoi annuler !
    // On ne sait pas quelle action a été faite en dernier
}
```

**Problèmes :**
1. Impossible de savoir quelle action annuler
2. Chaque bouton est couplé directement à la logique
3. Pas de réutilisation possible
4. Difficile à tester

---

## 🔧 Partie 2 : La solution - Le patron Commande

### Principe de base

**Idée centrale :** Transformer chaque action en un OBJET.

Au lieu de faire l'action directement, on :
1. **Crée un objet** qui représente l'action
2. **Exécute** cet objet quand on veut
3. **Conserve** cet objet dans un historique
4. **Annule** en appelant la méthode Annuler() de l'objet

### Les 4 éléments du patron

```
┌─────────────────────┐
│   ICommande         │  ← Interface
│  + Executer()       │
│  + Annuler()        │
└─────────────────────┘
          △
          │ implémente
┌─────────┴──────────┐
│ CommandeEcrire     │  ← Commande concrète
│ - texte            │
│ + Executer()       │
│ + Annuler()        │
└────────────────────┘
          │ utilise
          ↓
┌────────────────────┐
│    Editeur         │  ← Récepteur (fait le vrai travail)
│  + AjouterTexte()  │
│  + RetirerTexte()  │
└────────────────────┘
```

1. **ICommande** : Interface avec Executer() et Annuler()
2. **Commande concrète** : Implémente l'interface (ex: CommandeEcrire)
3. **Récepteur** : L'objet qui fait le vrai travail (ex: Editeur)
4. **Gestionnaire** : Conserve l'historique et gère Undo/Redo

---

## 💻 Partie 3 : Exemple complet et simple

### Scénario : Un calculateur avec historique

On va créer une calculatrice qui peut **additionner** et **soustraire** des nombres, avec possibilité d'**annuler** les opérations.

### Étape 1 : L'interface

```csharp
public interface ICommande
{
    void Executer();
    void Annuler();
}
```

Simple ! Toute commande doit pouvoir s'exécuter et s'annuler.

### Étape 2 : Le récepteur (la calculatrice)

```csharp
public class Calculatrice
{
    private int resultat = 0;
    
    public int Resultat 
    { 
        get { return resultat; } 
    }
    
    public void Additionner(int valeur)
    {
        resultat += valeur;
        Console.WriteLine($"➕ Ajout de {valeur} → Résultat = {resultat}");
    }
    
    public void Soustraire(int valeur)
    {
        resultat -= valeur;
        Console.WriteLine($"➖ Retrait de {valeur} → Résultat = {resultat}");
    }
}
```

La calculatrice sait juste faire des opérations. Elle ne sait rien des commandes ou de l'historique.

### Étape 3 : Les commandes concrètes

```csharp
// Commande pour additionner
public class CommandeAddition : ICommande
{
    private Calculatrice calculatrice;
    private int valeur;
    
    public CommandeAddition(Calculatrice calc, int val)
    {
        calculatrice = calc;
        valeur = val;
    }
    
    public void Executer()
    {
        calculatrice.Additionner(valeur);
    }
    
    public void Annuler()
    {
        // Pour annuler une addition, on soustrait !
        calculatrice.Soustraire(valeur);
        Console.WriteLine($"↩️ Annulation de +{valeur}");
    }
}

// Commande pour soustraire
public class CommandeSoustraction : ICommande
{
    private Calculatrice calculatrice;
    private int valeur;
    
    public CommandeSoustraction(Calculatrice calc, int val)
    {
        calculatrice = calc;
        valeur = val;
    }
    
    public void Executer()
    {
        calculatrice.Soustraire(valeur);
    }
    
    public void Annuler()
    {
        // Pour annuler une soustraction, on additionne !
        calculatrice.Additionner(valeur);
        Console.WriteLine($"↩️ Annulation de -{valeur}");
    }
}
```

### Étape 4 : Le gestionnaire d'historique

```csharp
public class GestionnaireCommandes
{
    private Stack<ICommande> historique = new Stack<ICommande>();
    private Stack<ICommande> historiqueAnnule = new Stack<ICommande>();
    
    // Exécuter une nouvelle commande
    public void ExecuterCommande(ICommande commande)
    {
        commande.Executer();
        historique.Push(commande);
        
        // Quand on fait une nouvelle action, on efface l'historique "Refaire"
        historiqueAnnule.Clear();
    }
    
    // Annuler la dernière commande (Undo)
    public void Annuler()
    {
        if (historique.Count == 0)
        {
            Console.WriteLine("❌ Rien à annuler");
            return;
        }
        
        ICommande commande = historique.Pop();
        commande.Annuler();
        historiqueAnnule.Push(commande);
    }
    
    // Refaire la dernière commande annulée (Redo)
    public void Refaire()
    {
        if (historiqueAnnule.Count == 0)
        {
            Console.WriteLine("❌ Rien à refaire");
            return;
        }
        
        ICommande commande = historiqueAnnule.Pop();
        commande.Executer();
        historique.Push(commande);
    }
}
```

### Étape 5 : Utilisation

```csharp
class Program
{
    static void Main(string[] args)
    {
        // Créer la calculatrice et le gestionnaire
        Calculatrice calc = new Calculatrice();
        GestionnaireCommandes gestionnaire = new GestionnaireCommandes();
        
        Console.WriteLine("=== Calculatrice avec historique ===\n");
        Console.WriteLine($"Résultat initial : {calc.Resultat}\n");
        
        // Créer et exécuter des commandes
        Console.WriteLine("--- Action 1 : Ajouter 10 ---");
        ICommande cmd1 = new CommandeAddition(calc, 10);
        gestionnaire.ExecuterCommande(cmd1);
        
        Console.WriteLine("\n--- Action 2 : Ajouter 5 ---");
        ICommande cmd2 = new CommandeAddition(calc, 5);
        gestionnaire.ExecuterCommande(cmd2);
        
        Console.WriteLine("\n--- Action 3 : Soustraire 3 ---");
        ICommande cmd3 = new CommandeSoustraction(calc, 3);
        gestionnaire.ExecuterCommande(cmd3);
        
        Console.WriteLine($"\n📊 Résultat actuel : {calc.Resultat}");
        
        // Annuler
        Console.WriteLine("\n--- Annuler la dernière action ---");
        gestionnaire.Annuler();
        Console.WriteLine($"📊 Résultat : {calc.Resultat}");
        
        Console.WriteLine("\n--- Annuler encore ---");
        gestionnaire.Annuler();
        Console.WriteLine($"📊 Résultat : {calc.Resultat}");
        
        // Refaire
        Console.WriteLine("\n--- Refaire ---");
        gestionnaire.Refaire();
        Console.WriteLine($"📊 Résultat : {calc.Resultat}");
    }
}
```

**Sortie du programme :**
```
=== Calculatrice avec historique ===

Résultat initial : 0

--- Action 1 : Ajouter 10 ---
➕ Ajout de 10 → Résultat = 10

--- Action 2 : Ajouter 5 ---
➕ Ajout de 5 → Résultat = 15

--- Action 3 : Soustraire 3 ---
➖ Retrait de 3 → Résultat = 12

📊 Résultat actuel : 12

--- Annuler la dernière action ---
↩️ Annulation de -3
➕ Ajout de 3 → Résultat = 15
📊 Résultat : 15

--- Annuler encore ---
↩️ Annulation de +5
➖ Retrait de 5 → Résultat = 10
📊 Résultat : 10

--- Refaire ---
➕ Ajout de 5 → Résultat = 15
📊 Résultat : 15
```

---

## ✅ Avantages du patron Commande

### 1. Séparation des responsabilités
- Le **bouton** ne sait pas comment faire l'action
- La **commande** encapsule l'action
- Le **récepteur** fait le vrai travail

### 2. Historique gratuit
- Chaque action est un objet → facile à conserver
- Undo/Redo automatique

### 3. Réutilisation
```csharp
ICommande cmd = new CommandeAddition(calc, 5);

// Utilisable partout :
bouton.Click += (s, e) => gestionnaire.ExecuterCommande(cmd);
raccourci.KeyDown += (s, e) => gestionnaire.ExecuterCommande(cmd);
menu.Click += (s, e) => gestionnaire.ExecuterCommande(cmd);
```

### 4. Facilité de test
```csharp
[TestMethod]
public void TestCommandeAddition()
{
    Calculatrice calc = new Calculatrice();
    CommandeAddition cmd = new CommandeAddition(calc, 10);
    
    cmd.Executer();
    Assert.AreEqual(10, calc.Resultat);
    
    cmd.Annuler();
    Assert.AreEqual(0, calc.Resultat);
}
```

### 5. Macros et scripts
```csharp
// Créer une séquence de commandes
List<ICommande> macro = new List<ICommande>
{
    new CommandeAddition(calc, 10),
    new CommandeAddition(calc, 5),
    new CommandeSoustraction(calc, 2)
};

// Exécuter la macro
foreach (var cmd in macro)
{
    gestionnaire.ExecuterCommande(cmd);
}
```

---

## 🔗 

**Bonne nouvelle !** WPF utilise déjà le patron Commande avec `ICommand`.

### L'interface ICommand de WPF

```csharp
public interface ICommand
{
    bool CanExecute(object parameter);  // Est-ce qu'on peut exécuter ?
    void Execute(object parameter);      // Exécuter l'action
    event EventHandler CanExecuteChanged; // Notifier les changements
}
```

### Différences avec notre version
 
| Notre version | WPF ICommand |
|--------------|--------------|
| `Executer()` | `Execute(object parameter)` |
| `Annuler()` | ❌ Pas inclus (mais on peut l'ajouter!) |
| ❌ Pas de CanExecute | `CanExecute()` pour activer/désactiver boutons |
 
### 🔧 RelayCommand - C'est quoi ?
 
**Question importante :** WPF fournit l'interface `ICommand`, mais **AUCUNE implémentation de base** !
 
**Deux solutions :**
 
#### Option 1 : Utiliser CommunityToolkit.Mvvm (recommandé) ✅
 
Microsoft fournit maintenant `RelayCommand` via un package NuGet :
 
```bash
# Installer le package
Install-Package CommunityToolkit.Mvvm
```
 
```csharp
using CommunityToolkit.Mvvm.Input;
 
// Dans le ViewModel
public ICommand AjouterCommand { get; set; }
 
public MonViewModel()
{
    AjouterCommand = new RelayCommand(
        execute: () => calculatrice.Additionner(10),
        canExecute: () => calculatrice.Resultat < 100
    );
}
```
 
**Avantage :** Déjà testé et maintenu par Microsoft !
 
#### Option 2 : Créer votre propre RelayCommand (pour apprendre)
 
Si vous voulez comprendre comment ça fonctionne ou si vous ne pouvez pas installer de packages :
 
```csharp
using System;
using System.Windows.Input;
 
public class RelayCommand : ICommand
{
    private readonly Action execute;        // Quoi faire quand on clique
    private readonly Func<bool> canExecute; // Quand activer le bouton
    
    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        this.execute = execute;
        this.canExecute = canExecute;
    }
    
    // WPF utilise cet événement pour savoir quand réévaluer CanExecute
    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }
    
    // Le bouton est-il actif ?
    public bool CanExecute(object parameter)
    {
        return canExecute == null || canExecute();
    }
    
    // Exécuter l'action
    public void Execute(object parameter)
    {
        execute();
    }
}
```
 
**Les deux options fonctionnent exactement de la même façon !**
 
**Alternative :** Vous pouvez aussi utiliser des librairies comme **MVVM Light** ou **CommunityToolkit.Mvvm** qui fournissent déjà `RelayCommand`.
 
---
 
### Exemple complet en WPF
 
#### MainWindow.xaml (la vue)
 
```xml
<Window x:Class="MonApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:MonApp"
        Title="Calculatrice" Height="200" Width="300">
    
    <StackPanel Margin="20">
        <TextBlock Text="Résultat :" FontSize="16"/>
        <TextBox Text="{Binding Resultat, Mode=OneWay}" IsReadOnly="True" FontSize="24"/>
        
        <!-- Le bouton est lié à la commande du ViewModel -->
        <Button Content="Ajouter 10" Command="{Binding AjouterCommand}" Height="40"/>
    </StackPanel>
</Window>
```
 
#### MainWindow.xaml.cs (code-behind)
 
```csharp
using System.Windows;
 
namespace MonApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // ✅ Créer le ViewModel et le définir comme DataContext
            DataContext = new MonViewModel();
        }
    }
}
```
 
#### MonViewModel.cs (le ViewModel)
 
```csharp
using System.ComponentModel;
using System.Windows.Input;
 
public class MonViewModel : INotifyPropertyChanged
{
    private Calculatrice calculatrice;
    
    public event PropertyChangedEventHandler PropertyChanged;
    
    // Propriété liée au TextBox
    public int Resultat => calculatrice.Resultat;
    
    // Commande liée au Button
    public ICommand AjouterCommand { get; set; }
    
    public MonViewModel()
    {
        calculatrice = new Calculatrice();
        
        // Créer la commande
        AjouterCommand = new RelayCommand(
            execute: () => 
            {
                calculatrice.Additionner(10);
                // Notifier que Resultat a changé
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Resultat)));
            },
            canExecute: () => calculatrice.Resultat < 100
        );
    }
}
```
 
**Comment ça fonctionne :**
 
1. **Au démarrage** : `MainWindow.xaml.cs` crée le `MonViewModel` et le met dans `DataContext`
2. **Le XAML** se lie automatiquement au ViewModel grâce au `{Binding}`
3. **Quand on clique** : WPF appelle `CanExecute()` puis `Execute()`
4. **Le bouton se désactive automatiquement** quand `Resultat >= 100` ✨
---

## 📝 Exercices pratiques

### Exercice 1 : Éditeur de texte simple (15 min)

Créez un éditeur de texte qui peut :
- **Ajouter du texte** à la fin
- **Effacer le dernier mot**
- **Annuler** les actions
- **Refaire** les actions annulées

**Squelette de départ :**

```csharp
public class EditeurTexte
{
    private StringBuilder texte = new StringBuilder();
    
    public string Texte => texte.ToString();
    
    public void AjouterMot(string mot)
    {
        // TODO: Ajouter le mot + un espace
    }
    
    public void RetirerMot(string mot)
    {
        // TODO: Retirer le mot (avec son espace)
    }
}

// TODO: Créer CommandeAjouterMot
// TODO: Créer CommandeEffacerMot
```

**Test :**
```csharp
EditeurTexte editeur = new EditeurTexte();
GestionnaireCommandes gestionnaire = new GestionnaireCommandes();

gestionnaire.ExecuterCommande(new CommandeAjouterMot(editeur, "Bonjour"));
gestionnaire.ExecuterCommande(new CommandeAjouterMot(editeur, "le"));
gestionnaire.ExecuterCommande(new CommandeAjouterMot(editeur, "monde"));

Console.WriteLine(editeur.Texte); // Bonjour le monde 

gestionnaire.Annuler();
Console.WriteLine(editeur.Texte); // Bonjour le 

gestionnaire.Refaire();
Console.WriteLine(editeur.Texte); // Bonjour le monde 
```

<details>
<summary>📖 Voir la solution complète</summary>

```csharp
// Le récepteur
public class EditeurTexte
{
    private StringBuilder texte = new StringBuilder();
    
    public string Texte => texte.ToString();
    
    public void AjouterMot(string mot)
    {
        texte.Append(mot + " ");
        Console.WriteLine($"✍️ Ajout du mot '{mot}' → Texte: {Texte}");
    }
    
    public void RetirerMot(string mot)
    {
        // Retirer le mot + l'espace
        int position = texte.ToString().LastIndexOf(mot + " ");
        if (position >= 0)
        {
            texte.Remove(position, mot.Length + 1);
            Console.WriteLine($"🗑️ Retrait du mot '{mot}' → Texte: {Texte}");
        }
    }
}

// Commande pour ajouter un mot
public class CommandeAjouterMot : ICommande
{
    private EditeurTexte editeur;
    private string mot;
    
    public CommandeAjouterMot(EditeurTexte editeur, string mot)
    {
        this.editeur = editeur;
        this.mot = mot;
    }
    
    public void Executer()
    {
        editeur.AjouterMot(mot);
    }
    
    public void Annuler()
    {
        editeur.RetirerMot(mot);
        Console.WriteLine($"↩️ Annulation de l'ajout de '{mot}'");
    }
}

// Commande pour effacer un mot
public class CommandeEffacerMot : ICommande
{
    private EditeurTexte editeur;
    private string mot;
    
    public CommandeEffacerMot(EditeurTexte editeur, string mot)
    {
        this.editeur = editeur;
        this.mot = mot;
    }
    
    public void Executer()
    {
        editeur.RetirerMot(mot);
    }
    
    public void Annuler()
    {
        editeur.AjouterMot(mot);
        Console.WriteLine($"↩️ Annulation de l'effacement de '{mot}'");
    }
}

// Programme de test
class Program
{
    static void Main(string[] args)
    {
        EditeurTexte editeur = new EditeurTexte();
        GestionnaireCommandes gestionnaire = new GestionnaireCommandes();
        
        Console.WriteLine("=== Éditeur de texte ===\n");
        
        gestionnaire.ExecuterCommande(new CommandeAjouterMot(editeur, "Bonjour"));
        gestionnaire.ExecuterCommande(new CommandeAjouterMot(editeur, "le"));
        gestionnaire.ExecuterCommande(new CommandeAjouterMot(editeur, "monde"));
        
        Console.WriteLine($"\n📄 Texte final : '{editeur.Texte}'\n");
        
        Console.WriteLine("--- Annuler ---");
        gestionnaire.Annuler();
        Console.WriteLine($"📄 Texte : '{editeur.Texte}'\n");
        
        Console.WriteLine("--- Refaire ---");
        gestionnaire.Refaire();
        Console.WriteLine($"📄 Texte : '{editeur.Texte}'");
    }
}
```

**Sortie du programme :**
```
=== Éditeur de texte ===

✍️ Ajout du mot 'Bonjour' → Texte: Bonjour 
✍️ Ajout du mot 'le' → Texte: Bonjour le 
✍️ Ajout du mot 'monde' → Texte: Bonjour le monde 

📄 Texte final : 'Bonjour le monde '

--- Annuler ---
🗑️ Retrait du mot 'monde' → Texte: Bonjour le 
↩️ Annulation de l'ajout de 'monde'
📄 Texte : 'Bonjour le '

--- Refaire ---
✍️ Ajout du mot 'monde' → Texte: Bonjour le monde 
📄 Texte : 'Bonjour le monde '
```

</details>

---

### Exercice 2 : Contrôleur de lumière (15 min)

Créez un système qui contrôle une lumière :
- **Allumer** la lumière
- **Éteindre** la lumière
- **Changer l'intensité** (0-100)
- Avec **Undo/Redo**

**Indices :**
```csharp
public class Lumiere
{
    private bool estAllumee = false;
    private int intensite = 50; // 0-100
    
    public void Allumer() { /* TODO */ }
    public void Eteindre() { /* TODO */ }
    public void ReglerIntensite(int niveau) { /* TODO */ }
}

// TODO: CommandeAllumer (doit se souvenir de l'état précédent pour annuler)
// TODO: CommandeEteindre
// TODO: CommandeChangerIntensite (doit se souvenir de l'intensité précédente)
```

**Défi bonus :** Créer une macro qui :
1. Allume la lumière
2. Met l'intensité à 75
3. Attend 2 secondes
4. Éteint la lumière

<details>
<summary>📖 Voir la solution complète</summary>

```csharp
// Le récepteur
public class Lumiere
{
    private bool estAllumee = false;
    private int intensite = 50; // 0-100
    
    public bool EstAllumee => estAllumee;
    public int Intensite => intensite;
    
    public void Allumer()
    {
        estAllumee = true;
        Console.WriteLine($"💡 Lumière ALLUMÉE (intensité: {intensite}%)");
    }
    
    public void Eteindre()
    {
        estAllumee = false;
        Console.WriteLine($"🌑 Lumière ÉTEINTE");
    }
    
    public void ReglerIntensite(int niveau)
    {
        if (niveau < 0) niveau = 0;
        if (niveau > 100) niveau = 100;
        
        intensite = niveau;
        Console.WriteLine($"🔆 Intensité réglée à {intensite}%");
    }
    
    public void AfficherEtat()
    {
        string etat = estAllumee ? "ALLUMÉE" : "ÉTEINTE";
        Console.WriteLine($"📊 État: {etat}, Intensité: {intensite}%");
    }
}

// Commande pour allumer
public class CommandeAllumer : ICommande
{
    private Lumiere lumiere;
    private bool etatPrecedent;
    
    public CommandeAllumer(Lumiere lumiere)
    {
        this.lumiere = lumiere;
    }
    
    public void Executer()
    {
        etatPrecedent = lumiere.EstAllumee;
        lumiere.Allumer();
    }
    
    public void Annuler()
    {
        if (!etatPrecedent)
        {
            lumiere.Eteindre();
            Console.WriteLine("↩️ Annulation : lumière éteinte");
        }
    }
}

// Commande pour éteindre
public class CommandeEteindre : ICommande
{
    private Lumiere lumiere;
    private bool etatPrecedent;
    
    public CommandeEteindre(Lumiere lumiere)
    {
        this.lumiere = lumiere;
    }
    
    public void Executer()
    {
        etatPrecedent = lumiere.EstAllumee;
        lumiere.Eteindre();
    }
    
    public void Annuler()
    {
        if (etatPrecedent)
        {
            lumiere.Allumer();
            Console.WriteLine("↩️ Annulation : lumière rallumée");
        }
    }
}

// Commande pour changer l'intensité
public class CommandeChangerIntensite : ICommande
{
    private Lumiere lumiere;
    private int nouvelleIntensite;
    private int intensitePrecedente;
    
    public CommandeChangerIntensite(Lumiere lumiere, int intensite)
    {
        this.lumiere = lumiere;
        this.nouvelleIntensite = intensite;
    }
    
    public void Executer()
    {
        intensitePrecedente = lumiere.Intensite;
        lumiere.ReglerIntensite(nouvelleIntensite);
    }
    
    public void Annuler()
    {
        lumiere.ReglerIntensite(intensitePrecedente);
        Console.WriteLine($"↩️ Annulation : intensité restaurée à {intensitePrecedente}%");
    }
}

// Programme de test
class Program
{
    static void Main(string[] args)
    {
        Lumiere lumiere = new Lumiere();
        GestionnaireCommandes gestionnaire = new GestionnaireCommandes();
        
        Console.WriteLine("=== Contrôleur de lumière ===\n");
        lumiere.AfficherEtat();
        
        Console.WriteLine("\n--- Allumer la lumière ---");
        gestionnaire.ExecuterCommande(new CommandeAllumer(lumiere));
        
        Console.WriteLine("\n--- Changer l'intensité à 75% ---");
        gestionnaire.ExecuterCommande(new CommandeChangerIntensite(lumiere, 75));
        
        Console.WriteLine("\n--- Changer l'intensité à 100% ---");
        gestionnaire.ExecuterCommande(new CommandeChangerIntensite(lumiere, 100));
        
        Console.WriteLine("\n--- Éteindre ---");
        gestionnaire.ExecuterCommande(new CommandeEteindre(lumiere));
        
        Console.WriteLine("\n--- Annuler (rallumer) ---");
        gestionnaire.Annuler();
        
        Console.WriteLine("\n--- Annuler (intensité à 75%) ---");
        gestionnaire.Annuler();
        lumiere.AfficherEtat();
        
        Console.WriteLine("\n--- Refaire (intensité à 100%) ---");
        gestionnaire.Refaire();
        lumiere.AfficherEtat();
    }
}
```

**Sortie du programme :**
```
=== Contrôleur de lumière ===

📊 État: ÉTEINTE, Intensité: 50%

--- Allumer la lumière ---
💡 Lumière ALLUMÉE (intensité: 50%)

--- Changer l'intensité à 75% ---
🔆 Intensité réglée à 75%

--- Changer l'intensité à 100% ---
🔆 Intensité réglée à 100%

--- Éteindre ---
🌑 Lumière ÉTEINTE

--- Annuler (rallumer) ---
💡 Lumière ALLUMÉE (intensité: 100%)
↩️ Annulation : lumière rallumée

--- Annuler (intensité à 75%) ---
🔆 Intensité réglée à 75%
↩️ Annulation : intensité restaurée à 75%
📊 État: ALLUMÉE, Intensité: 75%

--- Refaire (intensité à 100%) ---
🔆 Intensité réglée à 100%
📊 État: ALLUMÉE, Intensité: 100%
```

### 🎁 Solution du défi bonus : Macro

```csharp
class Program
{
    static void Main(string[] args)
    {
        Lumiere lumiere = new Lumiere();
        GestionnaireCommandes gestionnaire = new GestionnaireCommandes();
        
        Console.WriteLine("=== Macro automatique ===\n");
        
        // Créer la séquence de commandes
        List<ICommande> macro = new List<ICommande>
        {
            new CommandeAllumer(lumiere),
            new CommandeChangerIntensite(lumiere, 75)
        };
        
        // Exécuter la macro
        Console.WriteLine("▶️ Exécution de la macro...\n");
        foreach (var cmd in macro)
        {
            gestionnaire.ExecuterCommande(cmd);
            System.Threading.Thread.Sleep(500); // Pause de 0.5 sec
        }
        
        // Attendre 2 secondes
        Console.WriteLine("\n⏳ Attente de 2 secondes...");
        System.Threading.Thread.Sleep(2000);
        
        // Éteindre
        Console.WriteLine("\n🔚 Fin de la macro");
        gestionnaire.ExecuterCommande(new CommandeEteindre(lumiere));
        
        lumiere.AfficherEtat();
    }
}
```

</details>

---

### Exercice 3 : Application WPF avec ICommand (bonus)

Créez une petite application WPF avec :
- Un `TextBox` pour afficher le résultat
- Des boutons `+1`, `-1`, `×2`, `÷2`
- Des boutons `Undo` et `Redo`

Utilisez le patron Commande pour tout gérer !

**Structure suggérée :**
```csharp
// ViewModel
public class CalculatriceViewModel : INotifyPropertyChanged
{
    private GestionnaireCommandes gestionnaire;
    
    public ICommand AdditionCommand { get; set; }
    public ICommand SoustractionCommand { get; set; }
    public ICommand UndoCommand { get; set; }
    public ICommand RedoCommand { get; set; }
    
    // TODO: Implémenter
}
```

<details>
<summary>📖 Voir la solution complète</summary>

### XAML (MainWindow.xaml)

```xml
<Window x:Class="CalculatriceApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Calculatrice avec Undo/Redo" Height="300" Width="400">
    <Grid Margin="20">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>
        
        <!-- Affichage du résultat -->
        <TextBlock Grid.Row="0" Text="Résultat :" FontSize="16" Margin="0,0,0,10"/>
        <TextBox Grid.Row="1" Text="{Binding Resultat, Mode=OneWay}" 
                 FontSize="32" FontWeight="Bold" TextAlignment="Center"
                 IsReadOnly="True" Margin="0,0,0,20"/>
        
        <!-- Boutons d'opérations -->
        <StackPanel Grid.Row="2" Orientation="Horizontal" HorizontalAlignment="Center" Margin="0,0,0,20">
            <Button Content="+1" Command="{Binding AdditionCommand}" Width="60" Height="40" Margin="5"/>
            <Button Content="-1" Command="{Binding SoustractionCommand}" Width="60" Height="40" Margin="5"/>
            <Button Content="×2" Command="{Binding MultiplicationCommand}" Width="60" Height="40" Margin="5"/>
            <Button Content="÷2" Command="{Binding DivisionCommand}" Width="60" Height="40" Margin="5"/>
        </StackPanel>
        
        <!-- Boutons Undo/Redo -->
        <StackPanel Grid.Row="3" Orientation="Horizontal" HorizontalAlignment="Center">
            <Button Content="↩️ Annuler" Command="{Binding UndoCommand}" Width="100" Height="35" Margin="5"/>
            <Button Content="↪️ Refaire" Command="{Binding RedoCommand}" Width="100" Height="35" Margin="5"/>
        </StackPanel>
    </Grid>
</Window>
```

### Code-behind (MainWindow.xaml.cs)

```csharp
using System.Windows;

namespace CalculatriceApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CalculatriceViewModel();
        }
    }
}
```

### ViewModel (CalculatriceViewModel.cs)

```csharp
using System.ComponentModel;
using System.Windows.Input;

namespace CalculatriceApp
{
    // RelayCommand : implémentation simple de ICommand
    public class RelayCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;
        
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute();
        }
        
        public void Execute(object parameter)
        {
            execute();
        }
    }
    
    // ViewModel
    public class CalculatriceViewModel : INotifyPropertyChanged
    {
        private Calculatrice calculatrice;
        private GestionnaireCommandes gestionnaire;
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        public int Resultat => calculatrice.Resultat;
        
        public ICommand AdditionCommand { get; set; }
        public ICommand SoustractionCommand { get; set; }
        public ICommand MultiplicationCommand { get; set; }
        public ICommand DivisionCommand { get; set; }
        public ICommand UndoCommand { get; set; }
        public ICommand RedoCommand { get; set; }
        
        public CalculatriceViewModel()
        {
            calculatrice = new Calculatrice();
            gestionnaire = new GestionnaireCommandes();
            
            // Créer les commandes
            AdditionCommand = new RelayCommand(() => ExecuterOperation(new CommandeAddition(calculatrice, 1)));
            SoustractionCommand = new RelayCommand(() => ExecuterOperation(new CommandeSoustraction(calculatrice, 1)));
            MultiplicationCommand = new RelayCommand(() => ExecuterOperation(new CommandeMultiplication(calculatrice, 2)));
            DivisionCommand = new RelayCommand(() => ExecuterOperation(new CommandeDivision(calculatrice, 2)));
            
            UndoCommand = new RelayCommand(
                () => { gestionnaire.Annuler(); NotifierChangement(); },
                () => gestionnaire.PeutAnnuler
            );
            
            RedoCommand = new RelayCommand(
                () => { gestionnaire.Refaire(); NotifierChangement(); },
                () => gestionnaire.PeutRefaire
            );
        }
        
        private void ExecuterOperation(ICommande commande)
        {
            gestionnaire.ExecuterCommande(commande);
            NotifierChangement();
        }
        
        private void NotifierChangement()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Resultat)));
            CommandManager.InvalidateRequerySuggested(); // Rafraîchir les boutons
        }
    }
}
```

### Modèle - Calculatrice améliorée

```csharp
public class Calculatrice
{
    private int resultat = 0;
    
    public int Resultat => resultat;
    
    public void Additionner(int valeur)
    {
        resultat += valeur;
    }
    
    public void Soustraire(int valeur)
    {
        resultat -= valeur;
    }
    
    public void Multiplier(int valeur)
    {
        resultat *= valeur;
    }
    
    public void Diviser(int valeur)
    {
        if (valeur != 0)
            resultat /= valeur;
    }
}
```

### Commandes supplémentaires

```csharp
// Commande Multiplication
public class CommandeMultiplication : ICommande
{
    private Calculatrice calculatrice;
    private int valeur;
    private int resultatAvant;
    
    public CommandeMultiplication(Calculatrice calc, int val)
    {
        calculatrice = calc;
        valeur = val;
    }
    
    public void Executer()
    {
        resultatAvant = calculatrice.Resultat;
        calculatrice.Multiplier(valeur);
    }
    
    public void Annuler()
    {
        // Pour annuler une multiplication, on divise
        calculatrice.Diviser(valeur);
    }
}

// Commande Division
public class CommandeDivision : ICommande
{
    private Calculatrice calculatrice;
    private int valeur;
    private int resultatAvant;
    
    public CommandeDivision(Calculatrice calc, int val)
    {
        calculatrice = calc;
        valeur = val;
    }
    
    public void Executer()
    {
        resultatAvant = calculatrice.Resultat;
        calculatrice.Diviser(valeur);
    }
    
    public void Annuler()
    {
        // Pour annuler une division, on multiplie
        calculatrice.Multiplier(valeur);
    }
}
```

### Gestionnaire amélioré avec propriétés

```csharp
public class GestionnaireCommandes
{
    private Stack<ICommande> historique = new Stack<ICommande>();
    private Stack<ICommande> historiqueAnnule = new Stack<ICommande>();
    
    public bool PeutAnnuler => historique.Count > 0;
    public bool PeutRefaire => historiqueAnnule.Count > 0;
    
    public void ExecuterCommande(ICommande commande)
    {
        commande.Executer();
        historique.Push(commande);
        historiqueAnnule.Clear();
    }
    
    public void Annuler()
    {
        if (historique.Count == 0) return;
        
        ICommande commande = historique.Pop();
        commande.Annuler();
        historiqueAnnule.Push(commande);
    }
    
    public void Refaire()
    {
        if (historiqueAnnule.Count == 0) return;
        
        ICommande commande = historiqueAnnule.Pop();
        commande.Executer();
        historique.Push(commande);
    }
}
```

### Résultat

L'application WPF affiche :
- Un grand champ de texte montrant le résultat
- 4 boutons d'opération (+1, -1, ×2, ÷2)
- 2 boutons pour Annuler et Refaire
- Les boutons Undo/Redo sont automatiquement désactivés quand il n'y a rien à annuler/refaire

**Fonctionnalités :**
- ✅ Toutes les opérations sont des commandes
- ✅ Historique complet avec Undo/Redo
- ✅ Les boutons se désactivent automatiquement grâce à `CanExecute`
- ✅ Architecture MVVM propre

</details>

---

## 🎓 Résumé (5 min)

### Ce qu'on a appris

✅ **Le problème** : Code difficile à maintenir, impossible d'annuler les actions

✅ **La solution** : Transformer chaque action en objet

✅ **Les éléments** :
- Interface `ICommande` avec `Executer()` et `Annuler()`
- Commandes concrètes qui implémentent l'interface
- Récepteur qui fait le vrai travail
- Gestionnaire qui gère l'historique

✅ **Les avantages** :
- Séparation des responsabilités
- Undo/Redo facile
- Réutilisabilité
- Testabilité
- Possibilité de macros

✅ **En WPF** : `ICommand` utilise ce patron !

### Quand l'utiliser ?

👍 **OUI, utilisez le patron Commande quand :**
- Vous avez besoin d'Undo/Redo
- Vous voulez logger toutes les actions
- Vous créez des macros/scripts
- Vous voulez découpler l'interface de la logique

👎 **NON, n'utilisez pas le patron Commande si :**
- L'action est très simple (un seul appel de méthode)
- Vous n'avez pas besoin d'historique
- La performance est critique (les objets ajoutent du overhead)

---