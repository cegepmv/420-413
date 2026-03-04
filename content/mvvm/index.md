---
title: "MVVM"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 16
---

## Le patron MVVM (Model-View-ViewModel)

### Qu'est-ce que MVVM ?

**MVVM** = **Model-View-ViewModel**

C'est un **patron de conception** (design pattern) qui organise le code d'une application WPF en **3 couches** :

| Couche | Responsabilité | Fichier |
|--------|----------------|---------|
| **Model** | Les données brutes (classes métier) | `Personne.cs`, `Produit.cs` |
| **View** | L'interface graphique (XAML) | `MainWindow.xaml` |
| **ViewModel** | Logique de présentation, data binding | `MainViewModel.cs` |

### Pourquoi MVVM ?

**Problème avec le code-behind (approche traditionnelle) :**

```csharp
// MainWindow.xaml.cs
public partial class MainWindow : Window
{
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        // Logique métier mélangée avec l'UI
        string nom = txtNom.Text;
        double prix = double.Parse(txtPrix.Text);
        
        // Calculs...
        // Accès base de données...
        // Mise à jour de l'interface...
    }
}
```

**Problèmes :**
- Logique mélangée avec l'interface
- Difficile à tester
- Difficile à réutiliser
- Code-behind devient énorme

**Avec MVVM :**
- ✅ Séparation claire des responsabilités
- ✅ Code testable (ViewModel peut être testé sans UI)
- ✅ Réutilisable
- ✅ Code-behind quasi vide

### Architecture MVVM

```
View (XAML)
    ↕ Data Binding
ViewModel (Logique de présentation)
    ↕ 
Model (Données)
```

**Règles :**
1. La **View** ne contient QUE du XAML (et un code-behind minimal)
2. Le **ViewModel** ne connaît PAS la View (pas de `MessageBox`, pas de `txtNom.Text`)
3. Le **Model** ne connaît ni View ni ViewModel

---

## Progression pédagogique : du simple au complexe

Nous allons apprendre MVVM en 3 étapes :
1. **Exemple 1** : 2 champs texte simples (carte de visite)
2. **Exemple 2** : Ajout d'un bouton et d'une action (calculatrice d'âge)
3. **Exemple 3** : Ajout d'une liste (gestion de produits)

---

## Exemple 1 - MVVM simple : Carte de visite

**Objectif :** Créer une interface pour saisir un prénom et un nom, et afficher automatiquement le nom complet.

### Étape 1 : Le Model

**Personne.cs**
```csharp
public class Personne
{
    public string Prenom { get; set; }
    public string Nom { get; set; }
}
```

C'est juste une classe de données, rien de spécial. Pas de `INotifyPropertyChanged` ici car le Model représente juste les données brutes.

### Étape 2 : Le ViewModel

**PersonneViewModel.cs**
```csharp
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class PersonneViewModel : INotifyPropertyChanged
{
    // On contient l'objet métier ici !
    private Personne _personneMetier;

    public PersonneViewModel()
    {
        _personneMetier = new Personne { Prenom = "Jean", Nom = "Tremblay" };
    }

    public string Prenom
    {
        get => _personneMetier.Prenom;
        set 
        {
            _personneMetier.Prenom = value; // On écrit directement dans le Model
            OnPropertyChanged();
            OnPropertyChanged(nameof(NomComplet));
        }
    }

    public string Nom
    {
        get => _personneMetier.Nom;
        set 
        {
            _personneMetier.Nom = value; // On écrit directement dans le Model
            OnPropertyChanged();
            OnPropertyChanged(nameof(NomComplet));
        }
    }

    public string NomComplet => $"{Prenom} {Nom}";

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null) 
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
```

**Explications importantes :**

1. **Champs privés** (`prenom`, `nom`) : Stockent les valeurs
2. **Propriétés publiques** (`Prenom`, `Nom`) : Accessibles depuis le XAML
3. **`OnPropertyChanged()`** : Notifie WPF que la propriété a changé
4. **`nameof(NomComplet)`** : Quand prénom ou nom change, le nom complet doit se mettre à jour aussi
5. **`NomComplet`** : Propriété calculée, pas besoin de setter

### Étape 3 : La View

**MainWindow.xaml**
```xml
<Window x:Class="CarteVisite.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Carte de visite - MVVM" 
        Height="300" 
        Width="400">
    
    <StackPanel Margin="20">
        <!-- Titre -->
        <TextBlock Text="👤 Carte de visite" 
                   FontSize="24" 
                   FontWeight="Bold"
                   Margin="0,0,0,30" />
        
        <!-- Prénom -->
        <Label Content="Prénom :" FontWeight="Bold" />
        <TextBox Text="{Binding Prenom, UpdateSourceTrigger=PropertyChanged}" 
                 Height="30" 
                 FontSize="14"
                 Margin="0,0,0,15" />
        
        <!-- Nom -->
        <Label Content="Nom :" FontWeight="Bold" />
        <TextBox Text="{Binding Nom, UpdateSourceTrigger=PropertyChanged}" 
                 Height="30" 
                 FontSize="14"
                 Margin="0,0,0,30" />
        
        <!-- Affichage du nom complet (lecture seule) -->
        <Border BorderBrush="LightGray" 
                BorderThickness="2" 
                Padding="15"
                Background="#F0F0F0">
            <StackPanel>
                <TextBlock Text="Nom complet :" 
                           FontSize="12" 
                           Foreground="Gray" />
                <TextBlock Text="{Binding NomComplet}" 
                           FontSize="20" 
                           FontWeight="Bold"
                           Foreground="DarkBlue" />
            </StackPanel>
        </Border>
    </StackPanel>
    
</Window>
```

**Points importants dans le XAML :**

1. **`{Binding Prenom}`** : Lie le TextBox à la propriété `Prenom` du ViewModel
2. **`UpdateSourceTrigger=PropertyChanged`** : Met à jour immédiatement à chaque frappe (sinon, seulement quand on quitte le champ)
3. **`{Binding NomComplet}`** : Affiche automatiquement le nom complet
4. **Aucun `x:Name`** : Pas besoin, on utilise le binding !

### Étape 4 : Code-behind (quasi vide !)

**MainWindow.xaml.cs**
```csharp
using System.Windows;

namespace CarteVisite
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // C'est TOUT le code nécessaire !
            this.DataContext = new PersonneViewModel();
        }
    }
}
```

**C'est tout !** Aucune méthode `Button_Click`, aucun `txtNom.Text` !

### Comment ça fonctionne ?

**Quand vous tapez dans le TextBox "Prénom" :**
1. WPF détecte que le texte a changé
2. WPF appelle automatiquement `Prenom = "Alice"` dans le ViewModel
3. Le setter de `Prenom` déclenche `OnPropertyChanged()`
4. WPF reçoit la notification et met à jour tous les contrôles liés
5. Le TextBlock `NomComplet` se met à jour automatiquement !

**Résultat :** Vous tapez "Alice" puis "Tremblay" et vous voyez immédiatement "Alice Tremblay" s'afficher en bas !

---

## Exemple 2 - MVVM avec bouton : Calculatrice d'âge

**Objectif :** Entrer une année de naissance et calculer l'âge quand on clique sur un bouton.

### Le ViewModel avec une action

**AgeViewModel.cs**
```csharp
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class AgeViewModel : INotifyPropertyChanged
{
    // Année de naissance
    private int anneeNaissance;
    public int AnneeNaissance
    {
        get { return anneeNaissance; }
        set 
        { 
            anneeNaissance = value;
            OnPropertyChanged();
        }
    }
    
    // Âge calculé (résultat)
    private int age;
    public int Age
    {
        get { return age; }
        set 
        { 
            age = value;
            OnPropertyChanged();
        }
    }
    
    // Message à afficher
    private string message;
    public string Message
    {
        get { return message; }
        set 
        { 
            message = value;
            OnPropertyChanged();
        }
    }
    
    // Constructeur
    public AgeViewModel()
    {
        AnneeNaissance = 2000;
        Age = 0;
        Message = "Entrez votre année de naissance et cliquez sur Calculer";
    }
    
    // Méthode appelée par le bouton
    public void CalculerAge()
    {
        int anneeActuelle = DateTime.Now.Year;
        Age = anneeActuelle - AnneeNaissance;
        Message = $"Vous avez {Age} ans !";
    }
    
    // INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

### La View

**MainWindow.xaml**
```xml
<Window x:Class="CalculatriceAge.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Calculatrice d'âge - MVVM" 
        Height="300" 
        Width="400">
    
    <StackPanel Margin="20">
        <TextBlock Text="🎂 Calculatrice d'âge" 
                   FontSize="24" 
                   FontWeight="Bold"
                   Margin="0,0,0,30" />
        
        <Label Content="Année de naissance :" FontWeight="Bold" />
        <TextBox Text="{Binding AnneeNaissance}" 
                 Height="30" 
                 FontSize="14"
                 Margin="0,0,0,20" />
        
        <Button Content="Calculer mon âge" 
                Height="40" 
                FontSize="14"
                Background="DodgerBlue"
                Foreground="White"
                Click="BtnCalculer_Click"
                Margin="0,0,0,20" />
        
        <TextBlock Text="{Binding Message}" 
                   FontSize="16" 
                   TextAlignment="Center"
                   Foreground="Green"
                   FontWeight="Bold" />
    </StackPanel>
    
</Window>
```

### Code-behind (minimal)

**MainWindow.xaml.cs**
```csharp
using System.Windows;

namespace CalculatriceAge
{
    public partial class MainWindow : Window
    {
        private AgeViewModel viewModel;
        
        public MainWindow()
        {
            InitializeComponent();
            
            viewModel = new AgeViewModel();
            this.DataContext = viewModel;
        }
        
        // Seule méthode nécessaire : appeler le ViewModel
        private void BtnCalculer_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CalculerAge();
        }
    }
}
```

**Notez :** On a encore `Click="..."` ici, mais tout ce qu'on fait c'est appeler une méthode du ViewModel. La logique est dans le ViewModel, pas dans le code-behind !

---

## Exemple 3 - MVVM avec liste : Gestion de produits

Maintenant qu'on comprend les bases, passons à un exemple avec une liste.

### Le Model

**Produit.cs**
```csharp
public class Produit
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public double Prix { get; set; }
}
```

### Le ViewModel

**ProduitViewModel.cs**
```csharp
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class ProduitViewModel : INotifyPropertyChanged
{
    // Collection observable pour la liste
    public ObservableCollection<Produit> Produits { get; set; }
    
    // Champs pour le nouveau produit
    private string nouveauNom;
    public string NouveauNom
    {
        get { return nouveauNom; }
        set 
        { 
            nouveauNom = value;
            OnPropertyChanged();
        }
    }
    
    private double nouveauPrix;
    public double NouveauPrix
    {
        get { return nouveauPrix; }
        set 
        { 
            nouveauPrix = value;
            OnPropertyChanged();
        }
    }
    
    // Constructeur
    public ProduitViewModel()
    {
        // Initialiser la collection avec des données de test
        Produits = new ObservableCollection<Produit>
        {
            new Produit { Id = 1, Nom = "Clavier", Prix = 49.99 },
            new Produit { Id = 2, Nom = "Souris", Prix = 29.99 },
            new Produit { Id = 3, Nom = "Écran", Prix = 299.99 }
        };
        
        NouveauNom = "";
        NouveauPrix = 0;
    }
    
    // Méthode pour ajouter un produit
    public void AjouterProduit()
    {
        if (string.IsNullOrWhiteSpace(NouveauNom))
            return;
        
        var nouveau = new Produit
        {
            Id = Produits.Count + 1,
            Nom = NouveauNom,
            Prix = NouveauPrix
        };
        
        Produits.Add(nouveau);
        
        // Réinitialiser les champs
        NouveauNom = "";
        NouveauPrix = 0;
    }
    
    // INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

### La View

**MainWindow.xaml**
```xml
<Window x:Class="GestionProduits.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Gestion de Produits - MVVM" 
        Height="400" 
        Width="500">
    
    <Grid Margin="15">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto" />
            <RowDefinition Height="*" />
            <RowDefinition Height="Auto" />
        </Grid.RowDefinitions>
        
        <!-- Titre -->
        <TextBlock Grid.Row="0" 
                   Text="📦 Gestion de Produits" 
                   FontSize="24" 
                   FontWeight="Bold"
                   Margin="0,0,0,15" />
        
        <!-- Liste des produits -->
        <Border Grid.Row="1" 
                BorderBrush="LightGray" 
                BorderThickness="1"
                Margin="0,0,0,15">
            <ListBox ItemsSource="{Binding Produits}">
                <ListBox.ItemTemplate>
                    <DataTemplate>
                        <StackPanel Orientation="Horizontal" Margin="5">
                            <TextBlock Text="{Binding Nom}" 
                                       Width="150" 
                                       FontWeight="Bold" />
                            <TextBlock Text="{Binding Prix, StringFormat={}{0:C}}" 
                                       Foreground="Green" />
                        </StackPanel>
                    </DataTemplate>
                </ListBox.ItemTemplate>
            </ListBox>
        </Border>
        
        <!-- Formulaire d'ajout -->
        <StackPanel Grid.Row="2">
            <TextBlock Text="Ajouter un produit" 
                       FontWeight="Bold" 
                       Margin="0,0,0,10" />
            
            <Grid>
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="*" />
                    <ColumnDefinition Width="100" />
                    <ColumnDefinition Width="Auto" />
                </Grid.ColumnDefinitions>
                
                <TextBox Grid.Column="0"
                         Text="{Binding NouveauNom, UpdateSourceTrigger=PropertyChanged}" 
                         Height="30"
                         Margin="0,0,5,0" />
                
                <TextBox Grid.Column="1"
                         Text="{Binding NouveauPrix}" 
                         Height="30"
                         Margin="0,0,5,0" />
                
                <Button Grid.Column="2"
                        Content="Ajouter" 
                        Width="80"
                        Height="30"
                        Background="Green"
                        Foreground="White"
                        Click="BtnAjouter_Click" />
            </Grid>
        </StackPanel>
    </Grid>
    
</Window>
```

### Code-behind

**MainWindow.xaml.cs**
```csharp
using System.Windows;

namespace GestionProduits
{
    public partial class MainWindow : Window
    {
        private ProduitViewModel viewModel;
        
        public MainWindow()
        {
            InitializeComponent();
            
            viewModel = new ProduitViewModel();
            this.DataContext = viewModel;
        }
        
        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AjouterProduit();
        }
    }
}
```

**Point clé :** Remarquez comment la ListBox se met à jour automatiquement quand on ajoute un produit grâce à `ObservableCollection` !

---

## ObservableCollection vs List

**Pourquoi `ObservableCollection` et pas `List` ?**

```csharp
// ❌ Avec List - l'interface ne se met pas à jour
public List<Produit> Produits { get; set; }

// ✅ Avec ObservableCollection - l'interface se met à jour automatiquement
public ObservableCollection<Produit> Produits { get; set; }
```

`ObservableCollection<T>` implémente automatiquement `INotifyCollectionChanged` :
- Quand on ajoute un élément : l'interface se met à jour
- Quand on supprime un élément : l'interface se met à jour

---

## Commands (amélioration avancée - optionnel)

Dans un MVVM pur, on ne devrait pas avoir de `Click="..."` dans le XAML. À la place, on utilise des **Commands**.

### RelayCommand.cs (classe utilitaire)

```csharp
using System;
using System.Windows.Input;

public class RelayCommand : ICommand
{
    private Action execute;
    private Func<bool> canExecute;

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
```

### ProduitViewModel avec Commands

```csharp
public class ProduitViewModel : INotifyPropertyChanged
{
    // ... propriétés ...
    
    public ICommand AjouterCommand { get; }
    
    public ProduitViewModel()
    {
        // ... initialisation ...
        
        // Créer la command
        AjouterCommand = new RelayCommand(AjouterProduit, () => !string.IsNullOrWhiteSpace(NouveauNom));
    }
    
    // ... méthodes ...
}
```

### XAML avec Commands

```xml
<Button Content="Ajouter" Command="{Binding AjouterCommand}" />
```

**Avantage :** 
- Aucun code-behind nécessaire !
- Le bouton se désactive automatiquement si la condition n'est pas remplie

---

## Résumé MVVM

### Checklist de vérification

✅ **Votre application suit MVVM si :**
- [ ] Le code-behind est (quasi) vide
- [ ] Tous les contrôles utilisent `{Binding}`
- [ ] Les ViewModels implémentent `INotifyPropertyChanged`
- [ ] Les ViewModels utilisent `ObservableCollection` pour les listes
- [ ] Aucun `MessageBox` ou contrôle UI dans le ViewModel

❌ **Vous n'êtes PAS en MVVM si :**
- [ ] Vous avez de la logique dans les méthodes `Button_Click`
- [ ] Vous accédez à `txtNom.Text` directement
- [ ] Vous avez `MessageBox.Show()` dans le ViewModel
- [ ] Le code-behind contient de la logique métier

### Les 3 couches en résumé

| Couche | Contient | Ne contient PAS |
|--------|----------|-----------------|
| **Model** | Données brutes, propriétés simples | Logique UI, INotifyPropertyChanged |
| **ViewModel** | Logique métier, INotifyPropertyChanged, Collections observables | Références à la View, MessageBox |
| **View** | XAML, bindings | Logique métier, calculs |

---