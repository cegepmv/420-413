---
title: "Patron commande"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 16
---

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

### Le lien avec WPF ICommand (aperçu)

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
