---
title: "Tests unitaires"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 23
---

# Introduction aux tests unitaires — xUnit & Moq

---

## Table des matières

1. [Pourquoi tester ?](#1-pourquoi-tester-)
2. [Mise en place du projet de tests](#2-mise-en-place-du-projet-de-tests)
3. [Anatomie d'un test — patron AAA](#3-anatomie-dun-test--patron-aaa)
4. [Les assertions xUnit](#4-les-assertions-xunit)
5. [Tester un modèle — `Utilisateur`](#5-tester-un-modèle--utilisateur)
6. [Tester une classe sans dépendance — `Calculatrice`](#6-tester-une-classe-sans-dépendance--calculatrice)
7. [Interfaces et testabilité — le problème du couplage](#7-interfaces-et-testabilité--le-problème-du-couplage)
8. [Fake Repository — tester sans base de données](#8-fake-repository--tester-sans-base-de-données)
9. [Tester le Repository réel avec une BD en mémoire](#9-tester-le-repository-réel-avec-une-bd-en-mémoire)
10. [Introduction à Moq](#10-introduction-à-moq)
11. [Tester le ViewModel — `UserInputViewModel`](#11-tester-le-viewmodel--userinputviewmodel)
12. [Rendre le ViewModel testable — refactoring](#12-rendre-le-viewmodel-testable--refactoring)
13. [Tester du code async](#13-tester-du-code-async)
14. [Fake vs Mock — quand utiliser quoi ?](#14-fake-vs-mock--quand-utiliser-quoi-)
15. [Bonnes pratiques](#15-bonnes-pratiques)
16. [Résumé](#16-résumé)

---

## 1. Pourquoi tester ?

### Le problème sans tests

Dans WpfApp2, pour vérifier que la `Calculatrice` fonctionne, on doit :

1. Lancer l'application WPF
2. Cliquer plusieurs fois sur le bouton
3. Regarder le résultat à l'écran

C'est lent, manuel, et on ne teste qu'un cas à la fois. Si on modifie `Additionner()` pour corriger un bug, rien ne nous garantit qu'on n'a pas cassé `Soustraire()`.

C'est ce qu'on appelle une **régression** : une modification qui casse quelque chose qui fonctionnait avant.

### Ce que les tests apportent

- **Détection des régressions** automatiquement à chaque modification
- **Documentation vivante** : un test bien nommé explique ce que le code fait
- **Conception meilleure** : du code difficile à tester est souvent du code mal structuré
- **Confiance** pour refactoriser

### Ce qu'un test unitaire ne doit jamais faire

- Écrire dans la vraie base de données SQLite (`utilisateurs.db`)
- Dépendre d'une connexion réseau
- Afficher une fenêtre WPF (`MessageBox.Show`)
- Dépendre du résultat d'un autre test

### Structure du projet WpfApp2

Voici les éléments qu'on va tester, du plus simple au plus complexe :

```
┌─────────────────────────────────────────────────────────────┐
│                         WpfApp2                             │
│  MainWindow.xaml    EntreeUtilisateur.xaml  (Vues)          │
│  MonViewModel       UserInputViewModel      (ViewModels)    │
│  Calculatrice                               (Logique)       │
├─────────────────────────────────────────────────────────────┤
│                         Domaine                             │
│  Utilisateur                                (Modèle)        │
│  IUtilisateurRepository                     (Interface)     │
│  UtilisateurRepository                      (Repository)    │
│  UtilisateurDbContext                       (BD)            │
└─────────────────────────────────────────────────────────────┘
```

On va tester dans cet ordre :
**Modèle → Calculatrice → Repository → ViewModel**

---

## 2. Mise en place du projet de tests

### Créer le projet xUnit

Dans Visual Studio :

1. Clic droit sur la **Solution** → **Add** → **New Project**
2. Choisir **xUnit Test Project (.NET)**
3. Nommer `WpfApp2.Tests`

### Ajouter les références de projet

Clic droit sur `WpfApp2.Tests` → **Add** → **Project Reference** → cocher `Domaine`.

> **Pourquoi `Domaine` et non `WpfApp2` ?**
> `Utilisateur`, `IUtilisateurRepository` et `UtilisateurRepository` vivent dans `Domaine`.
> `WpfApp2` contient du code WPF (`MessageBox`, `Window`) qui ne peut pas être chargé dans un projet de test standard. La `Calculatrice` devrait idéalement être déplacée dans `Domaine` — c'est la bonne pratique d'architecture.

### Packages NuGet à installer dans `WpfApp2.Tests`

```
xunit
xunit.runner.visualstudio
Moq
Microsoft.NET.Test.Sdk
Microsoft.EntityFrameworkCore.InMemory
```

### Structure du projet de tests

```
WpfApp2.Tests/
├── Modeles/
│   └── UtilisateurTests.cs
├── Calculatrice/
│   └── CalculatriceTests.cs
├── Repository/
│   └── UtilisateurRepositoryTests.cs
├── ViewModels/
│   └── UserInputViewModelTests.cs
└── Fakes/
    └── FakeUtilisateurRepository.cs
```

---

## 3. Anatomie d'un test — patron AAA

Tout test unitaire suit trois étapes obligatoires :

```
Arrange → Act → Assert
```

- **Arrange** : préparer les données, créer les objets
- **Act** : exécuter le code qu'on teste
- **Assert** : vérifier que le résultat est celui attendu

```csharp
[Fact]
public void Additionner_ValeurDe10_ResultatEgal10()
{
    // Arrange — préparer
    var calc = new Calculatrice();

    // Act — exécuter
    calc.Additionner(10);

    // Assert — vérifier
    Assert.Equal(10, calc.Resultat);
}
```

### `[Fact]` — test simple

`[Fact]` marque une méthode comme un test. xUnit l'exécute automatiquement.

### `[Theory]` + `[InlineData]` — test paramétré

Exécute le même test avec plusieurs jeux de données différents :

```csharp
[Theory]
[InlineData(5,   5)]
[InlineData(10, 10)]
[InlineData(50, 50)]
[InlineData(0,   0)]
public void Additionner_DiversesValeurs_ResultatCorrect(int valeur, int attendu)
{
    // Arrange
    var calc = new Calculatrice();

    // Act
    calc.Additionner(valeur);

    // Assert
    Assert.Equal(attendu, calc.Resultat);
}
```

Cela génère **4 tests distincts** à partir d'une seule méthode.

### Convention de nommage

```
NomMethode_Scénario_RésultatAttendu
```

| ✅ Bon nom | ❌ Mauvais nom |
|---|---|
| `Additionner_ValeurDe10_ResultatEgal10` | `Test1` |
| `AjouterUtilisateur_NomVide_NAppellePasLeRepo` | `TestAjout` |
| `UtilisateurExiste_DejaPresent_RetourneTrue` | `TestUtilisateur` |

Un bon nom de test se lit comme une phrase : *si j'appelle X dans le contexte Y, je m'attends à Z.*

---

## 4. Les assertions xUnit

Les assertions vérifient que le résultat correspond à ce qu'on attend. Si une assertion échoue, le test échoue.

### Égalité

```csharp
Assert.Equal(10, calc.Resultat);       // valeur attendue en premier, obtenue en second
Assert.NotEqual(0, calc.Resultat);
Assert.Equal("Tremblay", utilisateur.Nom);
```

### Booléens

```csharp
Assert.True(calc.Resultat < 100);
Assert.False(utilisateur.NomUtilisateur == null);
```

### Null

```csharp
Assert.Null(resultat);
Assert.NotNull(utilisateur);
```

### Collections

```csharp
Assert.Empty(liste);                   // liste vide
Assert.NotEmpty(liste);                // au moins 1 élément
Assert.Single(liste);                  // exactement 1 élément
Assert.Equal(3, liste.Count);
Assert.Contains(utilisateur, liste);
```

### Exceptions

```csharp
// Vérifier qu'une exception synchrone est levée
Assert.Throws<ArgumentException>(() => calc.Additionner(-999));

// Vérifier qu'une exception async est levée
await Assert.ThrowsAsync<InvalidOperationException>(
    () => service.AjouterAsync(null)
);
```

---

## 5. Tester un modèle — `Utilisateur`

Le modèle `Utilisateur` est la classe la plus simple à tester : pas de dépendances, pas de BD, juste des propriétés et un constructeur.

### La classe à tester

```csharp
// Domaine/Utilisateur.cs
public class Utilisateur
{
    [Key]
    public int Id { get; set; }
    public string Nom { get; set; }

    [MaxLength(50)]
    public string Prenom { get; set; }

    [Required]
    public string NomUtilisateur { get; set; }

    public int Age { get; set; }
    public DateTime DateCreation { get; set; }

    public Utilisateur()
    {
        DateCreation = DateTime.Now;
    }
}
```

### Les tests du modèle

```csharp
// WpfApp2.Tests/Modeles/UtilisateurTests.cs
using Domaine;

public class UtilisateurTests
{
    [Fact]
    public void Constructeur_SansParametres_InitialiseDateCreationAujourdhui()
    {
        // Arrange + Act
        var utilisateur = new Utilisateur();

        // Assert
        Assert.Equal(DateTime.Today, utilisateur.DateCreation.Date);
    }

    [Fact]
    public void Constructeur_SansParametres_DateCreationNonNull()
    {
        var utilisateur = new Utilisateur();

        // DateCreation ne doit pas être la valeur par défaut (01/01/0001)
        Assert.NotEqual(default(DateTime), utilisateur.DateCreation);
    }

    [Fact]
    public void Proprietes_AssignationDirecte_ValeursConservees()
    {
        // Arrange
        var utilisateur = new Utilisateur
        {
            Nom            = "Tremblay",
            Prenom         = "Alexia",
            NomUtilisateur = "alexia.tremblay",
            Age            = 20
        };

        // Assert
        Assert.Equal("Tremblay",       utilisateur.Nom);
        Assert.Equal("Alexia",         utilisateur.Prenom);
        Assert.Equal("alexia.tremblay",utilisateur.NomUtilisateur);
        Assert.Equal(20,               utilisateur.Age);
    }

    [Theory]
    [InlineData("Tremblay", "Alexia",  "alexia.tremblay")]
    [InlineData("Gagnon",   "Samuel",  "sam.gagnon")]
    [InlineData("Roy",      "Mathieu", "mat.roy")]
    public void Proprietes_DiversNoms_BienAssignes(string nom, string prenom, string nomUtil)
    {
        var utilisateur = new Utilisateur
        {
            Nom = nom, Prenom = prenom, NomUtilisateur = nomUtil
        };

        Assert.Equal(nom,    utilisateur.Nom);
        Assert.Equal(prenom, utilisateur.Prenom);
        Assert.Equal(nomUtil,utilisateur.NomUtilisateur);
    }
}
```

> **Pourquoi tester le modèle ?** Pour valider que les règles d'initialisation (comme `DateCreation = DateTime.Now`) fonctionnent correctement, et pour s'assurer que les refactorisations futures ne cassent pas le comportement attendu.

---

## 6. Tester une classe sans dépendance — `Calculatrice`

La `Calculatrice` n'a aucune dépendance externe — elle est parfaite pour débuter. On la crée, on appelle ses méthodes, on vérifie le résultat.

### La classe à tester

```csharp
// WpfApp2/Calculatrice.cs
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
    }

    public void Soustraire(int valeur)
    {
        resultat -= valeur;
    }
}
```

### Les tests

```csharp
// WpfApp2.Tests/Calculatrice/CalculatriceTests.cs
public class CalculatriceTests
{
    // ── État initial ───────────────────────────────────────────────────

    [Fact]
    public void Resultat_Initial_EstZero()
    {
        var calc = new Calculatrice();

        Assert.Equal(0, calc.Resultat);
    }

    // ── Additionner ────────────────────────────────────────────────────

    [Fact]
    public void Additionner_ValeurDe10_ResultatEgal10()
    {
        // Arrange
        var calc = new Calculatrice();

        // Act
        calc.Additionner(10);

        // Assert
        Assert.Equal(10, calc.Resultat);
    }

    [Fact]
    public void Additionner_AppeleDeuxFois_CumuleLesValeurs()
    {
        // Arrange
        var calc = new Calculatrice();

        // Act
        calc.Additionner(10);
        calc.Additionner(10);

        // Assert
        Assert.Equal(20, calc.Resultat);
    }

    [Theory]
    [InlineData(10,  10)]
    [InlineData(50,  50)]
    [InlineData(100, 100)]
    [InlineData(0,   0)]
    public void Additionner_DiversesValeurs_ResultatCorrect(int valeur, int attendu)
    {
        var calc = new Calculatrice();
        calc.Additionner(valeur);
        Assert.Equal(attendu, calc.Resultat);
    }

    // ── Soustraire ─────────────────────────────────────────────────────

    [Fact]
    public void Soustraire_De10_ResultatEgalMoins10()
    {
        var calc = new Calculatrice();
        calc.Soustraire(10);
        Assert.Equal(-10, calc.Resultat);
    }

    [Fact]
    public void Soustraire_ApresAddition_ResultatCorrect()
    {
        // Arrange
        var calc = new Calculatrice();
        calc.Additionner(30);

        // Act
        calc.Soustraire(10);

        // Assert
        Assert.Equal(20, calc.Resultat);
    }

    // ── Règle métier : PeutAjouter (tirée de MonViewModel) ─────────────
    // MonViewModel bloque l'ajout quand Resultat >= 100

    [Fact]
    public void Resultat_InferieurA100_PeutEncoreAjouter()
    {
        var calc = new Calculatrice();
        calc.Additionner(90);

        Assert.True(calc.Resultat < 100);
    }

    [Fact]
    public void Resultat_Egal100_NePeutPlusAjouter()
    {
        var calc = new Calculatrice();
        calc.Additionner(100);

        Assert.False(calc.Resultat < 100);
    }
}
```

> **Remarque sur l'isolation :** chaque test crée sa propre instance de `Calculatrice`. Le résultat d'un test n'affecte jamais les autres.

---

## 7. Interfaces et testabilité — le problème du couplage

Avant de tester le Repository et le ViewModel, il faut comprendre pourquoi les **interfaces** sont essentielles pour les tests.

### Sans interface — couplage fort

```csharp
// ❌ Le ViewModel crée lui-même son repository
public class UserInputViewModel
{
    private UtilisateurRepository _repo = new UtilisateurRepository();
    //                                     ↑ couplé à la vraie BD
}
```

Pour tester ce ViewModel, on est **obligé** d'avoir une vraie base de données SQLite. C'est lent, fragile, et le test n'est plus unitaire.

### Avec interface — couplage faible

```csharp
// ✅ Le ViewModel reçoit une interface
public partial class UserInputViewModel : ObservableObject
{
    private readonly IUtilisateurRepository _utilisateurRepository;

    public UserInputViewModel(IUtilisateurRepository utilisateurRepository)
    {
        _utilisateurRepository = utilisateurRepository;
        //                        ↑ peut être n'importe quelle implémentation
    }
}
```

Dans les tests, on passe un **faux repository** (Fake ou Mock) — pas de BD, pas de réseau, exécution instantanée.

```
Production :  UserInputViewModel ← UtilisateurRepository (vraie BD SQLite)
Tests     :   UserInputViewModel ← FakeUtilisateurRepository (List<> en mémoire)
```

La classe `UserInputViewModel` ne sait pas — et ne doit pas savoir — quelle implémentation elle reçoit.

---

## 8. Fake Repository — tester sans base de données

Un **Fake** est une implémentation réelle mais simplifiée d'une interface, qui stocke les données en mémoire plutôt qu'en base de données.

### L'interface à implémenter

```csharp
// Domaine/Repository/IUtilisateurRepository.cs
public interface IUtilisateurRepository
{
    Task AjouterUtilisateurAsync(Utilisateur utilisateur);
    bool UtilisateurExiste(string nom, string prenom, string nomUtilisateur);
}
```

### Le Fake Repository

```csharp
// WpfApp2.Tests/Fakes/FakeUtilisateurRepository.cs
using Domaine;
using Domaine.Repository;

public class FakeUtilisateurRepository : IUtilisateurRepository
{
    // Stockage en mémoire — visible depuis les tests pour vérification
    public List<Utilisateur> Utilisateurs { get; } = new List<Utilisateur>();

    public Task AjouterUtilisateurAsync(Utilisateur utilisateur)
    {
        utilisateur.Id = Utilisateurs.Count + 1; // Simuler l'auto-incrément
        Utilisateurs.Add(utilisateur);
        return Task.CompletedTask;
    }

    public bool UtilisateurExiste(string nom, string prenom, string nomUtilisateur)
    {
        return Utilisateurs.Any(u =>
            u.Nom            == nom &&
            u.Prenom         == prenom &&
            u.NomUtilisateur == nomUtilisateur);
    }
}
```

### Tester le Fake lui-même

Avant d'utiliser le Fake dans d'autres tests, on peut vérifier qu'il fonctionne correctement :

```csharp
// WpfApp2.Tests/Fakes/FakeUtilisateurRepositoryTests.cs
public class FakeUtilisateurRepositoryTests
{
    [Fact]
    public async Task AjouterUtilisateurAsync_NouvelUtilisateur_EstAjouteListe()
    {
        // Arrange
        var repo = new FakeUtilisateurRepository();
        var utilisateur = new Utilisateur { Nom = "Tremblay", Prenom = "Alexia", NomUtilisateur = "alexia.t" };

        // Act
        await repo.AjouterUtilisateurAsync(utilisateur);

        // Assert
        Assert.Single(repo.Utilisateurs);
        Assert.Equal("Tremblay", repo.Utilisateurs[0].Nom);
    }

    [Fact]
    public void UtilisateurExiste_UtilisateurPresent_RetourneTrue()
    {
        // Arrange
        var repo = new FakeUtilisateurRepository();
        repo.Utilisateurs.Add(new Utilisateur
        {
            Nom = "Gagnon", Prenom = "Samuel", NomUtilisateur = "sam.g"
        });

        // Act
        bool existe = repo.UtilisateurExiste("Gagnon", "Samuel", "sam.g");

        // Assert
        Assert.True(existe);
    }

    [Fact]
    public void UtilisateurExiste_UtilisateurAbsent_RetourneFalse()
    {
        var repo = new FakeUtilisateurRepository();

        bool existe = repo.UtilisateurExiste("Inconnu", "Personne", "zzz");

        Assert.False(existe);
    }
}
```

---

## 9. Tester le Repository réel avec une BD en mémoire

Pour tester `UtilisateurRepository` (la vraie implémentation EF Core) sans créer de fichier `.db`, on utilise le **provider `InMemory`** d'EF Core.

### Package requis

```
Microsoft.EntityFrameworkCore.InMemory
```

### La vraie implémentation à tester

```csharp
// Domaine/Repository/UtilisateurRepository.cs
public class UtilisateurRepository : IUtilisateurRepository
{
    private UtilisateurDbContext _db;

    public UtilisateurRepository(UtilisateurDbContext dbContext)
    {
        _db = dbContext;
    }

    public async Task AjouterUtilisateurAsync(Utilisateur utilisateur)
    {
        await _db.Utilisateurs.AddAsync(utilisateur);
        await _db.SaveChangesAsync();
    }

    public bool UtilisateurExiste(string nom, string prenom, string nomUtilisateur)
    {
        return _db.Utilisateurs.Any(ut =>
            ut.Nom            == nom &&
            ut.Prenom         == prenom &&
            ut.NomUtilisateur == nomUtilisateur);
    }
}
```

### Les tests avec BD InMemory

```csharp
// WpfApp2.Tests/Repository/UtilisateurRepositoryTests.cs
using Domaine;
using Domaine.Repository;
using Microsoft.EntityFrameworkCore;

public class UtilisateurRepositoryTests
{
    // Méthode utilitaire — crée un DbContext InMemory frais pour chaque test
    private UtilisateurDbContext CreerContextInMemory()
    {
        var options = new DbContextOptionsBuilder<UtilisateurDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            //                                  ↑ nom unique = BD isolée par test
            .Options;

        return new UtilisateurDbContext(options);
    }

    [Fact]
    public async Task AjouterUtilisateurAsync_NouvelUtilisateur_EstPersiste()
    {
        // Arrange
        using var context = CreerContextInMemory();
        var repo = new UtilisateurRepository(context);
        var utilisateur = new Utilisateur
        {
            Nom = "Bouchard", Prenom = "Émilie", NomUtilisateur = "emilie.b"
        };

        // Act
        await repo.AjouterUtilisateurAsync(utilisateur);

        // Assert
        Assert.Equal(1, await context.Utilisateurs.CountAsync());
    }

    [Fact]
    public async Task AjouterUtilisateurAsync_DeuxUtilisateurs_DeuxEnregistrements()
    {
        // Arrange
        using var context = CreerContextInMemory();
        var repo = new UtilisateurRepository(context);

        // Act
        await repo.AjouterUtilisateurAsync(
            new Utilisateur { Nom = "Roy", Prenom = "Mathieu", NomUtilisateur = "mat.roy" });
        await repo.AjouterUtilisateurAsync(
            new Utilisateur { Nom = "Côté", Prenom = "Sophie", NomUtilisateur = "sophie.c" });

        // Assert
        Assert.Equal(2, await context.Utilisateurs.CountAsync());
    }

    [Fact]
    public async Task UtilisateurExiste_UtilisateurAjoute_RetourneTrue()
    {
        // Arrange
        using var context = CreerContextInMemory();
        var repo = new UtilisateurRepository(context);
        await repo.AjouterUtilisateurAsync(
            new Utilisateur { Nom = "Lavoie", Prenom = "Antoine", NomUtilisateur = "ant.lavoie" });

        // Act
        bool existe = repo.UtilisateurExiste("Lavoie", "Antoine", "ant.lavoie");

        // Assert
        Assert.True(existe);
    }

    [Fact]
    public void UtilisateurExiste_BDVide_RetourneFalse()
    {
        // Arrange
        using var context = CreerContextInMemory();
        var repo = new UtilisateurRepository(context);

        // Act
        bool existe = repo.UtilisateurExiste("Quelconque", "Personne", "qqun");

        // Assert
        Assert.False(existe);
    }

    [Fact]
    public async Task UtilisateurExiste_NomDifferent_RetourneFalse()
    {
        // Arrange
        using var context = CreerContextInMemory();
        var repo = new UtilisateurRepository(context);
        await repo.AjouterUtilisateurAsync(
            new Utilisateur { Nom = "Fortin", Prenom = "Camille", NomUtilisateur = "cam.fortin" });

        // Act — même prénom et nomUtilisateur, mais nom différent
        bool existe = repo.UtilisateurExiste("FORTIN_DIFFERENT", "Camille", "cam.fortin");

        // Assert
        Assert.False(existe);
    }
}
```

> **Pourquoi `Guid.NewGuid().ToString()` comme nom de BD ?** Chaque test reçoit une base de données InMemory avec un nom unique. Sans ça, tous les tests partagent la même BD en mémoire — les données d'un test contaminent les autres.

---

## 10. Introduction à Moq

### Le problème du Fake manuel

Écrire un Fake à la main fonctionne, mais :
- Si l'interface a 10 méthodes, il faut toutes les implémenter
- On ne peut pas facilement vérifier **comment** une méthode a été appelée
- On ne peut pas facilement **simuler une erreur**

**Moq** génère automatiquement un faux objet à partir d'une interface. On configure seulement ce dont on a besoin.

### Créer un Mock

```csharp
using Moq;

// Créer un mock de l'interface
var mockRepo = new Mock<IUtilisateurRepository>();
```

### Configurer le comportement — `Setup`

```csharp
// "Quand UtilisateurExiste est appelé avec ces paramètres, retourner false"
mockRepo.Setup(r => r.UtilisateurExiste(
    It.IsAny<string>(),     // n'importe quelle valeur pour nom
    It.IsAny<string>(),     // n'importe quelle valeur pour prenom
    It.IsAny<string>()))    // n'importe quelle valeur pour nomUtilisateur
    .Returns(false);

// "Quand AjouterUtilisateurAsync est appelé, ne rien faire (Task vide)"
mockRepo.Setup(r => r.AjouterUtilisateurAsync(It.IsAny<Utilisateur>()))
        .Returns(Task.CompletedTask);
```

### Obtenir l'objet à injecter — `.Object`

```csharp
// mockRepo est le "contrôleur" — mockRepo.Object est le vrai objet
IUtilisateurRepository repo = mockRepo.Object;
```

### Vérifier qu'une méthode a été appelée — `Verify`

```csharp
// Vérifier qu'AjouterUtilisateurAsync a été appelé exactement une fois
mockRepo.Verify(
    r => r.AjouterUtilisateurAsync(It.IsAny<Utilisateur>()),
    Times.Once
);

// Vérifier qu'une méthode n'a JAMAIS été appelée
mockRepo.Verify(
    r => r.AjouterUtilisateurAsync(It.IsAny<Utilisateur>()),
    Times.Never
);
```

### Capturer le paramètre passé — `Callback`

```csharp
Utilisateur? utilisateurCapture = null;

mockRepo.Setup(r => r.AjouterUtilisateurAsync(It.IsAny<Utilisateur>()))
        .Callback<Utilisateur>(u => utilisateurCapture = u) // capture l'objet passé
        .Returns(Task.CompletedTask);

// Après l'appel, on peut inspecter utilisateurCapture
Assert.Equal("Tremblay", utilisateurCapture!.Nom);
```

### Simuler une exception

```csharp
// "Quand AjouterUtilisateurAsync est appelé, lancer une exception"
mockRepo.Setup(r => r.AjouterUtilisateurAsync(It.IsAny<Utilisateur>()))
        .ThrowsAsync(new Exception("Connexion BD perdue"));
```

### Récapitulatif des méthodes Moq

| Méthode | Rôle |
|---|---|
| `new Mock<T>()` | Crée le mock |
| `.Setup(...).Returns(x)` | Configure un retour synchrone |
| `.Setup(...).Returns(Task.CompletedTask)` | Configure un retour async vide |
| `.Setup(...).ReturnsAsync(x)` | Configure un retour async avec valeur |
| `.Setup(...).ThrowsAsync(ex)` | Simule une exception async |
| `.Setup(...).Callback<T>(x => ...)` | Capture le paramètre passé |
| `.Object` | Obtient l'objet à injecter |
| `.Verify(..., Times.Once)` | Vérifie appelé exactement 1 fois |
| `.Verify(..., Times.Never)` | Vérifie jamais appelé |
| `It.IsAny<T>()` | Accepte n'importe quelle valeur |

---

## 11. Tester le ViewModel — `UserInputViewModel`

### Le problème actuel

`UserInputViewModel` contient du code WPF difficile à tester :

```csharp
// ❌ Appels WPF dans le ViewModel — bloquants en test
MessageBox.Show("Tous les champs sont obligatoires.", ...);
MessageBox.Show("L'utilisateur existe déjà", ...);
```

Ces `MessageBox.Show()` tentent d'afficher une vraie fenêtre WPF pendant les tests, ce qui cause soit une exception, soit un blocage.

Nous verrons comment résoudre ce problème proprement à la section 12. Pour l'instant, testons ce qui est testable directement.

### Tests avec le Fake Repository

```csharp
// WpfApp2.Tests/ViewModels/UserInputViewModelTests.cs
using Domaine;
using WpfApp2.UserInputDialog;

public class UserInputViewModelTests
{
    // Setup commun — recréé avant chaque test
    private readonly FakeUtilisateurRepository _fakeRepo;
    private readonly UserInputViewModel        _vm;

    public UserInputViewModelTests()
    {
        _fakeRepo = new FakeUtilisateurRepository();
        _vm       = new UserInputViewModel(_fakeRepo);
    }

    // ── Tests de l'état initial ────────────────────────────────────────

    [Fact]
    public void Constructeur_EtatInitial_ChampsVides()
    {
        Assert.True(string.IsNullOrEmpty(_vm.Nom));
        Assert.True(string.IsNullOrEmpty(_vm.Prenom));
        Assert.True(string.IsNullOrEmpty(_vm.NomUtilisateur));
    }

    // ── Tests de l'ajout ───────────────────────────────────────────────

    [Fact]
    public async Task AjouterUtilisateurAsync_ChampsValides_AjouteAuRepository()
    {
        // Arrange
        _vm.Nom            = "Tremblay";
        _vm.Prenom         = "Alexia";
        _vm.NomUtilisateur = "alexia.t";

        // Act
        await _vm.AjouterUtilisateurAsync();

        // Assert
        Assert.Single(_fakeRepo.Utilisateurs);
    }

    [Fact]
    public async Task AjouterUtilisateurAsync_ChampsValides_NomCorrectementAssigne()
    {
        // Arrange
        _vm.Nom            = "Gagnon";
        _vm.Prenom         = "Samuel";
        _vm.NomUtilisateur = "sam.g";

        // Act
        await _vm.AjouterUtilisateurAsync();

        // Assert
        Assert.Equal("Gagnon", _fakeRepo.Utilisateurs[0].Nom);
        Assert.Equal("Samuel", _fakeRepo.Utilisateurs[0].Prenom);
    }

    [Fact]
    public async Task AjouterUtilisateurAsync_ChampsValides_VideoLesChampsApresAjout()
    {
        // Arrange
        _vm.Nom            = "Roy";
        _vm.Prenom         = "Mathieu";
        _vm.NomUtilisateur = "mat.roy";

        // Act
        await _vm.AjouterUtilisateurAsync();

        // Assert — les champs sont remis à vide
        Assert.Equal(string.Empty, _vm.Nom);
        Assert.Equal(string.Empty, _vm.Prenom);
        Assert.Equal(string.Empty, _vm.NomUtilisateur);
    }

    [Fact]
    public async Task AjouterUtilisateurAsync_AvecEspaces_TrimmeLesValeurs()
    {
        // Arrange
        _vm.Nom            = "  Côté  ";
        _vm.Prenom         = "  Sophie  ";
        _vm.NomUtilisateur = "  sophie.c  ";

        // Act
        await _vm.AjouterUtilisateurAsync();

        // Assert — Trim() doit avoir été appliqué
        Assert.Equal("Côté",     _fakeRepo.Utilisateurs[0].Nom);
        Assert.Equal("Sophie",   _fakeRepo.Utilisateurs[0].Prenom);
        Assert.Equal("sophie.c", _fakeRepo.Utilisateurs[0].NomUtilisateur);
    }
}
```

### Tests avec Moq

```csharp
public class UserInputViewModelMoqTests
{
    [Fact]
    public async Task AjouterUtilisateurAsync_ChampsValides_AppelleAjouterDuRepository()
    {
        // Arrange
        var mockRepo = new Mock<IUtilisateurRepository>();

        mockRepo.Setup(r => r.UtilisateurExiste(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(false);

        mockRepo.Setup(r => r.AjouterUtilisateurAsync(It.IsAny<Utilisateur>()))
                .Returns(Task.CompletedTask);

        var vm = new UserInputViewModel(mockRepo.Object);
        vm.Nom = "Fortin"; vm.Prenom = "Camille"; vm.NomUtilisateur = "cam.f";

        // Act
        await vm.AjouterUtilisateurAsync();

        // Assert — vérifier que la méthode du repository a bien été appelée
        mockRepo.Verify(
            r => r.AjouterUtilisateurAsync(It.IsAny<Utilisateur>()),
            Times.Once
        );
    }

    [Fact]
    public async Task AjouterUtilisateurAsync_NomVide_NAppelleJamaisLeRepository()
    {
        // Arrange
        var mockRepo = new Mock<IUtilisateurRepository>();
        mockRepo.Setup(r => r.UtilisateurExiste(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(false);

        var vm = new UserInputViewModel(mockRepo.Object);
        vm.Nom            = "";         // ← champ vide
        vm.Prenom         = "Camille";
        vm.NomUtilisateur = "cam.f";

        // Act
        await vm.AjouterUtilisateurAsync();

        // Assert — AjouterUtilisateurAsync ne doit jamais avoir été appelé
        mockRepo.Verify(
            r => r.AjouterUtilisateurAsync(It.IsAny<Utilisateur>()),
            Times.Never
        );
    }

    [Fact]
    public async Task AjouterUtilisateurAsync_CaptureLesParametres()
    {
        // Arrange
        Utilisateur? utilisateurCapture = null;

        var mockRepo = new Mock<IUtilisateurRepository>();
        mockRepo.Setup(r => r.UtilisateurExiste(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(false);
        mockRepo.Setup(r => r.AjouterUtilisateurAsync(It.IsAny<Utilisateur>()))
                .Callback<Utilisateur>(u => utilisateurCapture = u)
                .Returns(Task.CompletedTask);

        var vm = new UserInputViewModel(mockRepo.Object);
        vm.Nom = "Gauthier"; vm.Prenom = "Félix"; vm.NomUtilisateur = "felix.g";

        // Act
        await vm.AjouterUtilisateurAsync();

        // Assert — inspecter l'objet passé au repository
        Assert.NotNull(utilisateurCapture);
        Assert.Equal("Gauthier", utilisateurCapture!.Nom);
        Assert.Equal("Félix",    utilisateurCapture.Prenom);
    }
}
```

---

## 12. Rendre le ViewModel testable — refactoring

### Le problème

`UserInputViewModel` appelle `MessageBox.Show()` directement — ce qui bloque les tests.

```csharp
// ❌ Couplage fort avec WPF
MessageBox.Show("Tous les champs sont obligatoires.", "Erreur", ...);
```

### La solution — interface de dialogue

On crée une interface qui abstrait l'affichage des messages :

```csharp
// Domaine/Services/IDialogService.cs
public interface IDialogService
{
    void AfficherErreur(string message, string titre = "Erreur");
    void AfficherInfo(string message, string titre = "Information");
    bool Confirmer(string message, string titre = "Confirmation");
}
```

Deux implémentations :

```csharp
// WpfApp2/Services/WpfDialogService.cs — utilisé en production
public class WpfDialogService : IDialogService
{
    public void AfficherErreur(string message, string titre = "Erreur")
        => MessageBox.Show(message, titre, MessageBoxButton.OK, MessageBoxImage.Error);

    public void AfficherInfo(string message, string titre = "Information")
        => MessageBox.Show(message, titre, MessageBoxButton.OK, MessageBoxImage.Information);

    public bool Confirmer(string message, string titre = "Confirmation")
        => MessageBox.Show(message, titre, MessageBoxButton.YesNo) == MessageBoxResult.Yes;
}

// WpfApp2.Tests/Fakes/FakeDialogService.cs — utilisé dans les tests
public class FakeDialogService : IDialogService
{
    public List<string> MessagesErreur { get; } = new();
    public List<string> MessagesInfo   { get; } = new();
    public bool ReponseConfirmation    { get; set; } = true;

    public void AfficherErreur(string message, string titre = "Erreur")
        => MessagesErreur.Add(message);

    public void AfficherInfo(string message, string titre = "Information")
        => MessagesInfo.Add(message);

    public bool Confirmer(string message, string titre = "Confirmation")
        => ReponseConfirmation;
}
```

### ViewModel refactorisé

```csharp
// UserInputViewModel.cs — version testable
public partial class UserInputViewModel : ObservableObject
{
    private readonly IUtilisateurRepository _utilisateurRepository;
    private readonly IDialogService         _dialogService;

    [ObservableProperty] private string nom;
    [ObservableProperty] private string prenom;
    [ObservableProperty] private string nomUtilisateur;

    public UserInputViewModel(
        IUtilisateurRepository utilisateurRepository,
        IDialogService dialogService)
    {
        _utilisateurRepository = utilisateurRepository;
        _dialogService         = dialogService;
    }

    [RelayCommand(CanExecute = nameof(PeutCreerUtilisateur))]
    public async Task AjouterUtilisateurAsync()
    {
        if (string.IsNullOrWhiteSpace(Nom) ||
            string.IsNullOrWhiteSpace(Prenom) ||
            string.IsNullOrWhiteSpace(NomUtilisateur))
        {
            // ✅ Plus de MessageBox.Show direct
            _dialogService.AfficherErreur("Tous les champs sont obligatoires.");
            return;
        }

        await _utilisateurRepository.AjouterUtilisateurAsync(new Utilisateur
        {
            Nom            = Nom.Trim(),
            Prenom         = Prenom.Trim(),
            NomUtilisateur = NomUtilisateur.Trim()
        });

        Nom = string.Empty;
        Prenom = string.Empty;
        NomUtilisateur = string.Empty;
    }

    private bool PeutCreerUtilisateur()
    {
        bool existe = _utilisateurRepository.UtilisateurExiste(Nom, Prenom, NomUtilisateur);
        if (existe) _dialogService.AfficherErreur("L'utilisateur existe déjà.");
        return !existe;
    }
}
```

### Tests du ViewModel refactorisé

```csharp
public class UserInputViewModelRefactorisesTests
{
    private readonly FakeUtilisateurRepository _fakeRepo;
    private readonly FakeDialogService         _fakeDialog;
    private readonly UserInputViewModel        _vm;

    public UserInputViewModelRefactorisesTests()
    {
        _fakeRepo   = new FakeUtilisateurRepository();
        _fakeDialog = new FakeDialogService();
        _vm         = new UserInputViewModel(_fakeRepo, _fakeDialog);
    }

    [Fact]
    public async Task AjouterUtilisateurAsync_NomVide_AfficheMessageErreur()
    {
        // Arrange
        _vm.Nom            = "";
        _vm.Prenom         = "Alexia";
        _vm.NomUtilisateur = "alexia.t";

        // Act
        await _vm.AjouterUtilisateurAsync();

        // Assert — un message d'erreur a été affiché
        Assert.Single(_fakeDialog.MessagesErreur);
        Assert.Empty(_fakeRepo.Utilisateurs);
    }

    [Fact]
    public async Task AjouterUtilisateurAsync_NomVide_NAjoutePasAuRepository()
    {
        // Arrange
        _vm.Nom = ""; _vm.Prenom = "Alexia"; _vm.NomUtilisateur = "alexia.t";

        // Act
        await _vm.AjouterUtilisateurAsync();

        // Assert
        Assert.Empty(_fakeRepo.Utilisateurs);
    }

    [Fact]
    public async Task AjouterUtilisateurAsync_ChampsValides_AucunMessageErreur()
    {
        // Arrange
        _vm.Nom = "Roy"; _vm.Prenom = "Mathieu"; _vm.NomUtilisateur = "mat.roy";

        // Act
        await _vm.AjouterUtilisateurAsync();

        // Assert
        Assert.Empty(_fakeDialog.MessagesErreur);
        Assert.Single(_fakeRepo.Utilisateurs);
    }
}
```

### Enregistrement dans IoC (App.xaml.cs)

```csharp
// En production — WpfDialogService
services.AddTransient<IDialogService, WpfDialogService>();
services.AddTransient<UserInputViewModel>();

// En test — FakeDialogService injecté manuellement
var vm = new UserInputViewModel(fakeRepo, fakeDialog);
```

---

## 13. Tester du code async

`AjouterUtilisateurAsync` est `async Task` — les tests qui l'appellent doivent aussi être `async Task`.

### Règle absolue

```csharp
// ✅ Toujours async Task
[Fact]
public async Task AjouterUtilisateurAsync_Exemple_Fonctionne()
{
    await _vm.AjouterUtilisateurAsync();
    Assert.Single(_fakeRepo.Utilisateurs);
}

// ❌ async void — xUnit ne peut pas capturer les exceptions !
[Fact]
public async void MauvaisTest()  // NE JAMAIS FAIRE
{
    await _vm.AjouterUtilisateurAsync();
}
```

### Configurer Moq pour les méthodes async

```csharp
// Méthode qui retourne Task (équivalent void async)
mockRepo.Setup(r => r.AjouterUtilisateurAsync(It.IsAny<Utilisateur>()))
        .Returns(Task.CompletedTask);    // ← pas ReturnsAsync ici

// Méthode qui retourne Task<T>
mockRepo.Setup(r => r.ObtenirParIdAsync(It.IsAny<int>()))
        .ReturnsAsync(new Utilisateur()); // ← ReturnsAsync pour Task<T>

// Simuler une exception async
mockRepo.Setup(r => r.AjouterUtilisateurAsync(It.IsAny<Utilisateur>()))
        .ThrowsAsync(new Exception("BD inaccessible"));
```

### Tester une exception async

```csharp
[Fact]
public async Task AjouterUtilisateurAsync_BDEchoue_PropageLException()
{
    // Arrange
    var mockRepo = new Mock<IUtilisateurRepository>();
    mockRepo.Setup(r => r.UtilisateurExiste(
        It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
        .Returns(false);
    mockRepo.Setup(r => r.AjouterUtilisateurAsync(It.IsAny<Utilisateur>()))
            .ThrowsAsync(new Exception("Connexion perdue"));

    var vm = new UserInputViewModel(mockRepo.Object, new FakeDialogService());
    vm.Nom = "Test"; vm.Prenom = "Test"; vm.NomUtilisateur = "test";

    // Assert — vérifier que l'exception est bien propagée
    await Assert.ThrowsAsync<Exception>(
        () => vm.AjouterUtilisateurAsync()
    );
}
```

---

## 14. Fake vs Mock — quand utiliser quoi ?

| Situation | Fake manuel | Moq |
|---|---|---|
| Vérifier l'**état** (`fakeRepo.Utilisateurs.Count == 1`) | ✅ Simple et lisible | Possible mais verbeux |
| Vérifier qu'une **méthode a été appelée** (`Verify`) | Nécessite du code custom | ✅ Fait pour ça |
| Interface avec **peu de méthodes** (IUtilisateurRepository en a 2) | ✅ Rapide à écrire | ✅ |
| Interface avec **beaucoup de méthodes** | Fastidieux | ✅ Configure seulement ce qu'on utilise |
| **Capturer les paramètres** passés | Nécessite du code custom | ✅ `.Callback<T>` |
| **Simuler une exception** | Nécessite du code custom | ✅ `.ThrowsAsync()` |
| Réutilisation dans plusieurs fichiers de tests | ✅ Classe réutilisable | À reconfigurer dans chaque test |

### Règle pratique

> **Utilisez un Fake** quand vous voulez vérifier ce qui a été sauvegardé ou l'état final.
> **Utilisez Moq** quand vous voulez vérifier comment ou combien de fois une méthode a été appelée.

---

## 15. Bonnes pratiques

### Un test = un seul comportement

```csharp
// ❌ Trop de vérifications — si ça échoue, on ne sait pas pourquoi
[Fact]
public async Task AjouterUtilisateur_TestGeneral()
{
    await _vm.AjouterUtilisateurAsync();
    Assert.Single(_fakeRepo.Utilisateurs);       // vérif 1
    Assert.Equal(string.Empty, _vm.Nom);         // vérif 2
    Assert.Equal("Tremblay", _fakeRepo.Utilisateurs[0].Nom); // vérif 3
}

// ✅ Un test, un comportement — l'échec est immédiatement localisé
[Fact] public async Task AjouterUtilisateur_AjouteAuRepository()  { ... }
[Fact] public async Task AjouterUtilisateur_VideLesChamps()        { ... }
[Fact] public async Task AjouterUtilisateur_NomCorrectAssigne()    { ... }
```

### Tests indépendants — constructeur pour le setup commun

```csharp
public class UserInputViewModelTests
{
    private readonly FakeUtilisateurRepository _fakeRepo;
    private readonly UserInputViewModel        _vm;

    // Exécuté avant CHAQUE test automatiquement par xUnit
    public UserInputViewModelTests()
    {
        _fakeRepo = new FakeUtilisateurRepository(); // ← nouveau à chaque test
        _vm       = new UserInputViewModel(_fakeRepo, new FakeDialogService());
    }
}
```

### Nommer clairement

```csharp
// ✅ Le nom dit tout — on n'a même pas besoin de lire le corps
public async Task AjouterUtilisateurAsync_NomVide_NAjoutePasAuRepository()
public async Task AjouterUtilisateurAsync_ChampsValides_VideLesChampsApresAjout()
public void UtilisateurExiste_UtilisateurPresent_RetourneTrue()
```

### Ne jamais utiliser `async void` dans les tests

```csharp
[Fact] public async Task TestCorrect() { ... }   // ✅
[Fact] public async void TestIncorrect() { ... }  // ❌ exceptions non captées
```

---

## 16. Résumé

```
PROGRESSION DES TESTS

  1. Modèle (Utilisateur)
       → Pas de dépendances, tests les plus simples
       → Valider constructeur, propriétés, initialisations

  2. Classe autonome (Calculatrice)
       → Pas de dépendances, logique pure
       → [Fact] et [Theory] avec [InlineData]

  3. Repository (UtilisateurRepository)
       → Dépend de DbContext → BD InMemory
       → UseInMemoryDatabase + Guid unique par test

  4. ViewModel (UserInputViewModel)
       → Dépend de IUtilisateurRepository → Fake ou Mock
       → Si MessageBox : extraire IDialogService

  5. Tests avec Moq
       → Vérifier comment les méthodes sont appelées (Verify)
       → Capturer les paramètres (.Callback)
       → Simuler des erreurs (.ThrowsAsync)

STRUCTURE DU PROJET

  WpfApp2.Tests/
  ├── Modeles/             ← UtilisateurTests.cs
  ├── Calculatrice/        ← CalculatriceTests.cs
  ├── Repository/          ← UtilisateurRepositoryTests.cs (InMemory)
  ├── ViewModels/          ← UserInputViewModelTests.cs
  └── Fakes/
      ├── FakeUtilisateurRepository.cs
      └── FakeDialogService.cs

XUNIT
  [Fact]                         → test simple
  [Theory] + [InlineData(...)]   → test paramétré
  Assert.Equal(attendu, obtenu)  → vérifier égalité
  Assert.True / False            → vérifier booléen
  Assert.Single / Empty          → vérifier collection
  Assert.ThrowsAsync<T>()        → vérifier exception async

MOQ
  new Mock<IInterface>()                      → créer le mock
  .Setup(...).Returns(x)                      → retour synchrone
  .Setup(...).Returns(Task.CompletedTask)     → retour async void
  .Setup(...).ReturnsAsync(x)                 → retour async avec valeur
  .Setup(...).ThrowsAsync(ex)                 → simuler exception
  .Setup(...).Callback<T>(x => capture = x)  → capturer paramètre
  .Object                                     → obtenir l'objet à injecter
  .Verify(..., Times.Once)                    → appelé exactement 1 fois
  .Verify(..., Times.Never)                   → jamais appelé
  It.IsAny<T>()                               → n'importe quelle valeur

RÈGLES D'OR
  1. Un test = un seul comportement vérifié
  2. async Task, jamais async void
  3. Chaque test est indépendant — nouveau Fake/Mock dans le constructeur
  4. Nommer : Méthode_Scénario_RésultatAttendu
  5. Jamais écrire dans la vraie BD, jamais ouvrir de fenêtre WPF
  6. Si MessageBox bloque les tests → extraire IDialogService
```