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

### Exemple complet MVVM : Gestion de produits

#### Étape 1 : Le Model

**Produit.cs**
```csharp
public class Produit
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public double Prix { get; set; }
    public int Stock { get; set; }
}
```

C'est juste une classe de données, rien de spécial.

#### Étape 2 : Le ViewModel

**ProduitViewModel.cs**
```csharp
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class ProduitViewModel : INotifyPropertyChanged
{
    // Collection observable pour la liste
    public ObservableCollection<Produit> Produits { get; set; }
    
    // Produit sélectionné dans la liste
    private Produit produitSelectionne;
    public Produit ProduitSelectionne
    {
        get { return produitSelectionne; }
        set 
        { 
            produitSelectionne = value;
            OnPropertyChanged();
        }
    }
    
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
    
    private int nouveauStock;
    public int NouveauStock
    {
        get { return nouveauStock; }
        set 
        { 
            nouveauStock = value;
            OnPropertyChanged();
        }
    }
    
    // Constructeur
    public ProduitViewModel()
    {
        Produits = new ObservableCollection<Produit>
        {
            new Produit { Id = 1, Nom = "Clavier", Prix = 49.99, Stock = 15 },
            new Produit { Id = 2, Nom = "Souris", Prix = 29.99, Stock = 25 },
            new Produit { Id = 3, Nom = "Écran", Prix = 299.99, Stock = 8 }
        };
    }
    
    // Méthodes publiques (appelées par les boutons via Commands)
    public void AjouterProduit()
    {
        var nouveau = new Produit
        {
            Id = Produits.Count + 1,
            Nom = NouveauNom,
            Prix = NouveauPrix,
            Stock = NouveauStock
        };
        
        Produits.Add(nouveau);
        
        // Réinitialiser les champs
        NouveauNom = "";
        NouveauPrix = 0;
        NouveauStock = 0;
    }
    
    public void SupprimerProduit()
    {
        if (ProduitSelectionne != null)
        {
            Produits.Remove(ProduitSelectionne);
        }
    }
    
    // INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

#### Étape 3 : La View

**MainWindow.xaml**
```xml
<Window x:Class="GestionProduits.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Gestion de Produits - MVVM" 
        Height="500" 
        Width="700">
    
    <Grid Margin="15">
        <Grid.RowDefinitions>
            <RowDefinition Height="50" />
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>
        
        <!-- Titre -->
        <TextBlock Grid.Row="0" 
                   Text="📦 Gestion de Produits" 
                   FontSize="24" 
                   FontWeight="Bold"
                   VerticalAlignment="Center" />
        
        <!-- Contenu -->
        <Grid Grid.Row="1" Margin="0,10,0,0">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="2*" />
                <ColumnDefinition Width="*" />
            </Grid.ColumnDefinitions>
            
            <!-- Liste des produits (gauche) -->
            <DataGrid Grid.Column="0" 
                      ItemsSource="{Binding Produits}"
                      SelectedItem="{Binding ProduitSelectionne}"
                      AutoGenerateColumns="False"
                      Margin="0,0,10,0">
                <DataGrid.Columns>
                    <DataGridTextColumn Header="ID" Binding="{Binding Id}" Width="50" />
                    <DataGridTextColumn Header="Nom" Binding="{Binding Nom}" Width="*" />
                    <DataGridTextColumn Header="Prix" Binding="{Binding Prix, StringFormat={}{0:C}}" Width="100" />
                    <DataGridTextColumn Header="Stock" Binding="{Binding Stock}" Width="80" />
                </DataGrid.Columns>
            </DataGrid>
            
            <!-- Panneau de contrôle (droite) -->
            <Border Grid.Column="1" 
                    BorderBrush="LightGray" 
                    BorderThickness="1" 
                    Padding="10">
                <StackPanel>
                    <TextBlock Text="Ajouter un produit" 
                               FontWeight="Bold" 
                               FontSize="16"
                               Margin="0,0,0,15" />
                    
                    <Label Content="Nom :" />
                    <TextBox Text="{Binding NouveauNom}" Height="30" Margin="0,0,0,10" />
                    
                    <Label Content="Prix :" />
                    <TextBox Text="{Binding NouveauPrix}" Height="30" Margin="0,0,0,10" />
                    
                    <Label Content="Stock :" />
                    <TextBox Text="{Binding NouveauStock}" Height="30" Margin="0,0,0,20" />
                    
                    <Button Content="Ajouter" 
                            Height="40" 
                            Background="Green"
                            Foreground="White"
                            Click="BtnAjouter_Click"
                            Margin="0,0,0,10" />
                    
                    <Button Content="Supprimer" 
                            Height="40" 
                            Background="Red"
                            Foreground="White"
                            Click="BtnSupprimer_Click" />
                </StackPanel>
            </Border>
        </Grid>
    </Grid>
    
</Window>
```

#### Étape 4 : Code-behind (minimal)

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
            
            // Créer et assigner le ViewModel
            viewModel = new ProduitViewModel();
            this.DataContext = viewModel;
        }
        
        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AjouterProduit();
        }
        
        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SupprimerProduit();
        }
    }
}
```

### ObservableCollection vs List

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

### Commands (amélioration avancée)

Dans un MVVM pur, on ne devrait pas avoir de `Click="..."` dans le XAML. À la place, on utilise des **Commands**.

**RelayCommand.cs** (classe utilitaire) :
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

**ProduitViewModel avec Commands :**
```csharp
public class ProduitViewModel : INotifyPropertyChanged
{
    // ... propriétés ...
    
    public ICommand AjouterCommand { get; }
    public ICommand SupprimerCommand { get; }
    
    public ProduitViewModel()
    {
        // ... initialisation ...
        
        AjouterCommand = new RelayCommand(AjouterProduit);
        SupprimerCommand = new RelayCommand(SupprimerProduit, () => ProduitSelectionne != null);
    }
    
    // ... méthodes ...
}
```

**XAML avec Commands :**
```xml
<Button Content="Ajouter" Command="{Binding AjouterCommand}" />
<Button Content="Supprimer" Command="{Binding SupprimerCommand}" />
```

**Avantage :** Aucun code-behind nécessaire !

---

