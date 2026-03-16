---
title: "Patron Repository et EF Core"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 20
---

## 🎯 Objectifs du cours

À la fin de ce cours, vous serez capable de :
- Comprendre le patron Repository et son utilité
- Utiliser Entity Framework Core pour accéder à une base de données
- Créer et appliquer des migrations
- Générer automatiquement le schéma de base de données
- Intégrer le tout dans une architecture MVVM

---

## 📌 Partie 1 : Le problème sans Repository

### Scénario : Une application de gestion de tâches

Vous développez une application WPF pour gérer des tâches. Sans le patron Repository, votre ViewModel pourrait ressembler à ceci :

```csharp
public class TachesViewModel : INotifyPropertyChanged
{
    private ObservableCollection<Tache> taches;
    
    public TachesViewModel()
    {
        // ❌ Le ViewModel parle directement à la base de données !
        using (var context = new ApplicationDbContext())
        {
            taches = new ObservableCollection<Tache>(context.Taches.ToList());
        }
    }
    
    public void AjouterTache(string titre)
    {
        var tache = new Tache { Titre = titre };
        
        // ❌ Logique de base de données dans le ViewModel
        using (var context = new ApplicationDbContext())
        {
            context.Taches.Add(tache);
            context.SaveChanges();
        }
        
        taches.Add(tache);
    }
    
    public void SupprimerTache(Tache tache)
    {
        // ❌ Encore du code de base de données !
        using (var context = new ApplicationDbContext())
        {
            context.Taches.Remove(tache);
            context.SaveChanges();
        }
        
        taches.Remove(tache);
    }
}
```

### ❌ Problèmes :

1. **Couplage fort** : Le ViewModel dépend directement d'Entity Framework
2. **Code dupliqué** : `using (var context = ...)` partout
3. **Difficile à tester** : Impossible de tester sans base de données réelle
4. **Violation de MVVM** : Le ViewModel ne devrait pas connaître la base de données
5. **Pas réutilisable** : Si on veut accéder aux tâches ailleurs, on duplique le code

---

## 🔧 Partie 2 : La solution - Le patron Repository

### Qu'est-ce que le patron Repository ?

**Définition :** Le Repository agit comme une **collection en mémoire** d'objets, mais qui sont en réalité stockés dans une base de données.

**Analogie :** C'est comme une bibliothèque :
- Vous demandez un livre au bibliothécaire (Repository)
- Vous ne savez pas où le livre est stocké (base de données, fichier, cloud...)
- Le bibliothécaire s'occupe de tout

### Architecture en couches

```
┌─────────────────────────────────────┐
│         Vue (XAML)                  │
│  - MainWindow.xaml                  │
└─────────────────┬───────────────────┘
                  │
┌─────────────────▼───────────────────┐
│         ViewModel                    │
│  - TachesViewModel                  │
│  - Commandes ICommand               │
└─────────────────┬───────────────────┘
                  │ utilise
┌─────────────────▼───────────────────┐
│      Repository (Interface)         │
│  - ITacheRepository                 │
│    + ObtenirTout()                  │
│    + Ajouter(tache)                 │
│    + Supprimer(id)                  │
└─────────────────┬───────────────────┘
                  │ implémente
┌─────────────────▼───────────────────┐
│  Repository Concret (EF Core)       │
│  - TacheRepository                  │
│  - Utilise DbContext                │
└─────────────────┬───────────────────┘
                  │
┌─────────────────▼───────────────────┐
│      Base de données                │
│  - SQL Server / SQLite / etc.       │
└─────────────────────────────────────┘
```

### Les composants

1. **Interface Repository** : Définit les opérations (contrat)
2. **Repository concret** : Implémente les opérations avec EF Core
3. **DbContext** : Point d'accès à la base de données (EF Core)
4. **Modèle** : Classes qui représentent les tables

---

## 💻 Partie 3 : Implémentation complète avec EF Core

### Étape 1 : Installation des packages NuGet

```bash
# Entity Framework Core
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.SqlServer
# OU pour SQLite (plus simple pour débuter)
Install-Package Microsoft.EntityFrameworkCore.Sqlite

# Outils pour les migrations
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.EntityFrameworkCore.Design
```

### Étape 2 : Créer le modèle (entité)

```csharp
// Models/Tache.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace MonApp.Models
{
    public class Tache
    {
        // ✅ Clé primaire (EF détecte automatiquement "Id" ou "TacheId")
        public int Id { get; set; }
        
        // ✅ Champ obligatoire
        [Required]
        [MaxLength(200)]
        public string Titre { get; set; }
        
        public string Description { get; set; }
        
        public bool EstTerminee { get; set; }
        
        public DateTime DateCreation { get; set; }
        
        // ✅ Constructeur par défaut nécessaire pour EF
        public Tache()
        {
            DateCreation = DateTime.Now;
            EstTerminee = false;
        }
    }
}
```

### Étape 3 : Créer le DbContext

```csharp
// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using MonApp.Models;

namespace MonApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        // ✅ DbSet = Table dans la base de données
        public DbSet<Tache> Taches { get; set; }
        
        // ✅ Configuration de la connexion
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Option 1 : SQLite (fichier local - simple pour débuter)
            options.UseSqlite("Data Source=taches.db");
            
            // Option 2 : SQL Server
            // options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TachesDb;Trusted_Connection=True;");
        }
        
        // ✅ Configuration avancée (optionnel)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configuration de la table
            modelBuilder.Entity<Tache>(entity =>
            {
                entity.ToTable("Taches");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Titre).IsRequired().HasMaxLength(200);
                
                // Seed data (données initiales)
                entity.HasData(
                    new Tache { Id = 1, Titre = "Apprendre EF Core", EstTerminee = false },
                    new Tache { Id = 2, Titre = "Créer une migration", EstTerminee = false }
                );
            });
        }
    }
}
```

### Étape 4 : Créer l'interface Repository

```csharp
// Repositories/ITacheRepository.cs
using System.Collections.Generic;
using MonApp.Models;

namespace MonApp.Repositories
{
    public interface ITacheRepository
    {
        // Opérations CRUD (Create, Read, Update, Delete)
        List<Tache> ObtenirTout();
        Tache ObtenirParId(int id);
        void Ajouter(Tache tache);
        void Modifier(Tache tache);
        void Supprimer(int id);
        
        // Opérations spécifiques
        List<Tache> ObtenirTachesNonTerminees();
        int CompterTaches();
    }
}
```

### Étape 5 : Implémenter le Repository avec EF Core

```csharp
// Repositories/TacheRepository.cs
using System.Collections.Generic;
using System.Linq;
using MonApp.Data;
using MonApp.Models;

namespace MonApp.Repositories
{
    public class TacheRepository : ITacheRepository
    {
        private readonly ApplicationDbContext _context;
        
        public TacheRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public List<Tache> ObtenirTout()
        {
            return _context.Taches.ToList();
        }
        
        public Tache ObtenirParId(int id)
        {
            return _context.Taches.Find(id);
        }
        
        public void Ajouter(Tache tache)
        {
            _context.Taches.Add(tache);
            _context.SaveChanges(); // ✅ Persiste les changements
        }
        
        public void Modifier(Tache tache)
        {
            _context.Taches.Update(tache);
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
        
        public List<Tache> ObtenirTachesNonTerminees()
        {
            return _context.Taches
                .Where(t => !t.EstTerminee)
                .ToList();
        }
        
        public int CompterTaches()
        {
            return _context.Taches.Count();
        }
    }
}
```

### Étape 6 : Utiliser le Repository dans le ViewModel

```csharp
// ViewModels/TachesViewModel.cs
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using MonApp.Models;
using MonApp.Repositories;
using CommunityToolkit.Mvvm.Input;

namespace MonApp.ViewModels
{
    public class TachesViewModel : INotifyPropertyChanged
    {
        private readonly ITacheRepository _repository;
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        public ObservableCollection<Tache> Taches { get; set; }
        
        public ICommand AjouterCommand { get; set; }
        public ICommand SupprimerCommand { get; set; }
        public ICommand ToggleTermineeCommand { get; set; }
        
        public TachesViewModel(ITacheRepository repository)
        {
            _repository = repository;
            
            // ✅ Charger les tâches depuis la base de données
            ChargerTaches();
            
            // ✅ Créer les commandes
            AjouterCommand = new RelayCommand<string>(AjouterTache);
            SupprimerCommand = new RelayCommand<Tache>(SupprimerTache);
            ToggleTermineeCommand = new RelayCommand<Tache>(ToggleTerminee);
        }
        
        private void ChargerTaches()
        {
            var taches = _repository.ObtenirTout();
            Taches = new ObservableCollection<Tache>(taches);
        }
        
        private void AjouterTache(string titre)
        {
            if (string.IsNullOrWhiteSpace(titre))
                return;
            
            var tache = new Tache { Titre = titre };
            
            // ✅ Le Repository s'occupe de la base de données
            _repository.Ajouter(tache);
            
            // Ajouter à l'UI
            Taches.Add(tache);
        }
        
        private void SupprimerTache(Tache tache)
        {
            if (tache == null)
                return;
            
            // ✅ Le Repository s'occupe de la base de données
            _repository.Supprimer(tache.Id);
            
            // Retirer de l'UI
            Taches.Remove(tache);
        }
        
        private void ToggleTerminee(Tache tache)
        {
            if (tache == null)
                return;
            
            tache.EstTerminee = !tache.EstTerminee;
            
            // ✅ Mettre à jour dans la base de données
            _repository.Modifier(tache);
            
            // Notifier l'UI
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Taches)));
        }
    }
}
```

---

## 🗄️ Partie 4 : Les Migrations

### Qu'est-ce qu'une migration ?

Une **migration** est un fichier qui contient les instructions pour :
- Créer/modifier le schéma de la base de données
- Ajouter/supprimer des tables ou colonnes
- Garder un historique des changements

**Avantage :** Vous pouvez versionner votre base de données comme votre code !

### Créer la première migration

Dans la **Console du Gestionnaire de package** (Tools > NuGet Package Manager > Package Manager Console) :

```powershell
# Créer la migration initiale
Add-Migration InitialCreate

# Appliquer la migration (créer la base de données)
Update-Database
```

**Ce qui se passe :**
1. EF Core analyse votre `DbContext` et vos modèles
2. Génère un fichier de migration dans `Migrations/`
3. `Update-Database` exécute le SQL pour créer les tables

### Structure d'une migration

```csharp
// Migrations/20240316120000_InitialCreate.cs
public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // ✅ Code pour appliquer la migration
        migrationBuilder.CreateTable(
            name: "Taches",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Titre = table.Column<string>(maxLength: 200, nullable: false),
                Description = table.Column<string>(nullable: true),
                EstTerminee = table.Column<bool>(nullable: false),
                DateCreation = table.Column<DateTime>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Taches", x => x.Id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // ✅ Code pour annuler la migration
        migrationBuilder.DropTable(name: "Taches");
    }
}
```

### Modifier le modèle et créer une nouvelle migration

Supposons qu'on veut ajouter une priorité aux tâches :

```csharp
// Models/Tache.cs
public class Tache
{
    public int Id { get; set; }
    public string Titre { get; set; }
    public string Description { get; set; }
    public bool EstTerminee { get; set; }
    public DateTime DateCreation { get; set; }
    
    // ✅ Nouveau champ !
    public int Priorite { get; set; } // 1 = Basse, 2 = Moyenne, 3 = Haute
}
```

Créer la migration :

```powershell
# Créer une nouvelle migration
Add-Migration AjouterPriorite

# Appliquer
Update-Database
```

### Commandes utiles des migrations

```powershell
# Créer une migration
Add-Migration NomDeLaMigration

# Appliquer toutes les migrations
Update-Database

# Annuler la dernière migration
Update-Database -Migration NomMigrationPrecedente

# Voir les migrations appliquées
Get-Migration

# Générer le script SQL (sans l'appliquer)
Script-Migration

# Supprimer la dernière migration (si pas encore appliquée)
Remove-Migration
```

---

## 🏗️ Partie 5 : Intégration complète avec MVVM

### Structure du projet

```
MonApp/
├── Models/
│   └── Tache.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Repositories/
│   ├── ITacheRepository.cs
│   └── TacheRepository.cs
├── ViewModels/
│   └── TachesViewModel.cs
├── Views/
│   ├── MainWindow.xaml
│   └── MainWindow.xaml.cs
└── Migrations/
    └── (fichiers générés automatiquement)
```

### MainWindow.xaml

```xml
<Window x:Class="MonApp.Views.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Gestionnaire de Tâches" Height="500" Width="600">
    
    <Grid Margin="10">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>
        
        <!-- Zone d'ajout -->
        <StackPanel Grid.Row="0" Orientation="Horizontal" Margin="0,0,0,10">
            <TextBox x:Name="txtNouvelleTache" Width="400" Height="30" 
                     VerticalContentAlignment="Center"/>
            <Button Content="Ajouter" Width="80" Height="30" Margin="10,0,0,0"
                    Command="{Binding AjouterCommand}"
                    CommandParameter="{Binding Text, ElementName=txtNouvelleTache}"/>
        </StackPanel>
        
        <!-- Liste des tâches -->
        <ListView Grid.Row="1" ItemsSource="{Binding Taches}">
            <ListView.View>
                <GridView>
                    <GridViewColumn Header="✓" Width="40">
                        <GridViewColumn.CellTemplate>
                            <DataTemplate>
                                <CheckBox IsChecked="{Binding EstTerminee}"
                                         Command="{Binding DataContext.ToggleTermineeCommand, 
                                                  RelativeSource={RelativeSource AncestorType=ListView}}"
                                         CommandParameter="{Binding}"/>
                            </DataTemplate>
                        </GridViewColumn.CellTemplate>
                    </GridViewColumn>
                    
                    <GridViewColumn Header="Tâche" Width="300" DisplayMemberBinding="{Binding Titre}"/>
                    
                    <GridViewColumn Header="Date" Width="120" DisplayMemberBinding="{Binding DateCreation, StringFormat=dd/MM/yyyy}"/>
                    
                    <GridViewColumn Header="Actions" Width="80">
                        <GridViewColumn.CellTemplate>
                            <DataTemplate>
                                <Button Content="🗑️" 
                                       Command="{Binding DataContext.SupprimerCommand,
                                                RelativeSource={RelativeSource AncestorType=ListView}}"
                                       CommandParameter="{Binding}"/>
                            </DataTemplate>
                        </GridViewColumn.CellTemplate>
                    </GridViewColumn>
                </GridView>
            </ListView.View>
        </ListView>
    </Grid>
</Window>
```

### MainWindow.xaml.cs

```csharp
using System.Windows;
using MonApp.Data;
using MonApp.Repositories;
using MonApp.ViewModels;

namespace MonApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // ✅ Créer le DbContext
            var context = new ApplicationDbContext();
            
            // ✅ Créer le Repository
            var repository = new TacheRepository(context);
            
            // ✅ Créer le ViewModel avec le Repository
            DataContext = new TachesViewModel(repository);
        }
    }
}
```

---

## ✅ Avantages du patron Repository

### 1. Séparation des responsabilités

```
ViewModel → Ne connaît PAS Entity Framework
Repository → S'occupe de TOUTE la logique de données
```

### 2. Testabilité

```csharp
// ✅ Facile à tester avec un mock
public class MockTacheRepository : ITacheRepository
{
    private List<Tache> taches = new List<Tache>();
    
    public List<Tache> ObtenirTout() => taches;
    public void Ajouter(Tache tache) => taches.Add(tache);
    // ... etc
}

// Dans les tests
var mockRepo = new MockTacheRepository();
var viewModel = new TachesViewModel(mockRepo);
// Tester sans base de données réelle !
```

### 3. Réutilisabilité

```csharp
// Utiliser le même Repository partout
public class TachesViewModel
{
    public TachesViewModel(ITacheRepository repo) { ... }
}

public class StatistiquesViewModel
{
    public StatistiquesViewModel(ITacheRepository repo) { ... }
}
```

### 4. Changement de technologie facile

```csharp
// Passer de Entity Framework à Dapper ? Facile !
public class TacheRepositoryDapper : ITacheRepository
{
    // Nouvelle implémentation avec Dapper
    // Le ViewModel ne change PAS !
}
```

### 5. Code plus propre

```csharp
// ❌ Avant (sans Repository)
using (var context = new ApplicationDbContext())
{
    var taches = context.Taches.Where(t => !t.EstTerminee).ToList();
}

// ✅ Après (avec Repository)
var taches = _repository.ObtenirTachesNonTerminees();
```

---

## 📝 Exercice pratique

### Exercice : Ajouter une gestion de catégories

Créez une fonctionnalité de catégories pour les tâches :

**1. Créer le modèle Categorie**

```csharp
public class Categorie
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Couleur { get; set; }
}
```

**2. Modifier le modèle Tache**

```csharp
public class Tache
{
    // ... propriétés existantes
    
    public int? CategorieId { get; set; } // Clé étrangère
    public Categorie Categorie { get; set; } // Navigation property
}
```

**3. Mettre à jour le DbContext**

```csharp
public class ApplicationDbContext : DbContext
{
    public DbSet<Tache> Taches { get; set; }
    public DbSet<Categorie> Categories { get; set; } // ✅ Ajouter
    
    // ... reste du code
}
```

**4. Créer une migration**

```powershell
Add-Migration AjouterCategories
Update-Database
```

**5. Créer ICategorieRepository et l'implémentation**

```csharp
public interface ICategorieRepository
{
    List<Categorie> ObtenirTout();
    void Ajouter(Categorie categorie);
}
```

**6. Modifier le ViewModel pour gérer les catégories**

<details>
<summary>📖 Voir la solution complète</summary>

### Categorie.cs

```csharp
using System.ComponentModel.DataAnnotations;

namespace MonApp.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Nom { get; set; }
        
        [MaxLength(7)] // Format: #RRGGBB
        public string Couleur { get; set; }
        
        public Categorie()
        {
            Couleur = "#808080"; // Gris par défaut
        }
    }
}
```

### Tache.cs (modifiée)

```csharp
public class Tache
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Titre { get; set; }
    
    public string Description { get; set; }
    public bool EstTerminee { get; set; }
    public DateTime DateCreation { get; set; }
    
    // ✅ Relation avec Categorie
    public int? CategorieId { get; set; }
    public Categorie Categorie { get; set; }
    
    public Tache()
    {
        DateCreation = DateTime.Now;
        EstTerminee = false;
    }
}
```

### ApplicationDbContext.cs (modifié)

```csharp
public class ApplicationDbContext : DbContext
{
    public DbSet<Tache> Taches { get; set; }
    public DbSet<Categorie> Categories { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=taches.db");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configuration Categorie
        modelBuilder.Entity<Categorie>(entity =>
        {
            entity.ToTable("Categories");
            entity.HasKey(c => c.Id);
            
            // Données initiales
            entity.HasData(
                new Categorie { Id = 1, Nom = "Travail", Couleur = "#2196F3" },
                new Categorie { Id = 2, Nom = "Personnel", Couleur = "#4CAF50" },
                new Categorie { Id = 3, Nom = "Urgent", Couleur = "#F44336" }
            );
        });
        
        // Configuration Tache avec relation
        modelBuilder.Entity<Tache>(entity =>
        {
            entity.ToTable("Taches");
            entity.HasKey(t => t.Id);
            
            // Relation avec Categorie
            entity.HasOne(t => t.Categorie)
                  .WithMany()
                  .HasForeignKey(t => t.CategorieId)
                  .OnDelete(DeleteBehavior.SetNull);
        });
    }
}
```

### ICategorieRepository.cs

```csharp
using System.Collections.Generic;
using MonApp.Models;

namespace MonApp.Repositories
{
    public interface ICategorieRepository
    {
        List<Categorie> ObtenirTout();
        Categorie ObtenirParId(int id);
        void Ajouter(Categorie categorie);
        void Supprimer(int id);
    }
}
```

### CategorieRepository.cs

```csharp
using System.Collections.Generic;
using System.Linq;
using MonApp.Data;
using MonApp.Models;

namespace MonApp.Repositories
{
    public class CategorieRepository : ICategorieRepository
    {
        private readonly ApplicationDbContext _context;
        
        public CategorieRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public List<Categorie> ObtenirTout()
        {
            return _context.Categories.ToList();
        }
        
        public Categorie ObtenirParId(int id)
        {
            return _context.Categories.Find(id);
        }
        
        public void Ajouter(Categorie categorie)
        {
            _context.Categories.Add(categorie);
            _context.SaveChanges();
        }
        
        public void Supprimer(int id)
        {
            var categorie = _context.Categories.Find(id);
            if (categorie != null)
            {
                _context.Categories.Remove(categorie);
                _context.SaveChanges();
            }
        }
    }
}
```

### TacheRepository.cs (modifié pour inclure les catégories)

```csharp
using Microsoft.EntityFrameworkCore;

public class TacheRepository : ITacheRepository
{
    private readonly ApplicationDbContext _context;
    
    public TacheRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public List<Tache> ObtenirTout()
    {
        // ✅ Inclure la catégorie dans la requête
        return _context.Taches
            .Include(t => t.Categorie)
            .ToList();
    }
    
    // ... autres méthodes
}
```

### Migration

```powershell
Add-Migration AjouterCategories
Update-Database
```

</details>

---

## 🎓 Résumé

### Ce qu'on a appris

✅ **Le patron Repository** : Sépare la logique de données du ViewModel

✅ **Entity Framework Core** : ORM pour accéder à la base de données

✅ **DbContext** : Point d'entrée vers la base de données

✅ **Migrations** : Versionner le schéma de base de données
- `Add-Migration` : Créer une migration
- `Update-Database` : Appliquer les migrations

✅ **Intégration MVVM** : Repository → ViewModel → Vue

### Architecture finale

```
Vue (XAML)
    ↕ Binding
ViewModel (Commandes, Propriétés)
    ↕ Utilise
Repository (Interface)
    ↕ Implémente
Repository Concret (EF Core)
    ↕ Utilise
DbContext (EF Core)
    ↕ Accède
Base de données
```

### Commandes essentielles

```powershell
# Créer une migration
Add-Migration NomMigration

# Appliquer les migrations
Update-Database

# Annuler une migration
Update-Database -Migration NomMigrationPrecedente

# Supprimer la dernière migration (non appliquée)
Remove-Migration

# Voir les migrations
Get-Migration
```

---

## 📚 Pour aller plus loin

- **Unit of Work** : Gérer plusieurs repositories ensemble
- **LINQ avancé** : Requêtes complexes avec Entity Framework
- **Async/Await** : Rendre les opérations de base de données asynchrones
- **AutoMapper** : Mapper entre entités et ViewModels
- **Validation** : Valider les données avant de sauvegarder

---

**Fin du cours ! 🎉**