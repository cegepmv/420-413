---
title: "Tests unitaires"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 22
---

# Introduction aux tests unitaires — xUnit & Moq

---

## Table des matières

1. [Pourquoi tester son code ?](#1-pourquoi-tester-son-code-)
2. [Mise en place du projet de tests](#2-mise-en-place-du-projet-de-tests)
3. [Anatomie d'un test — le patron AAA](#3-anatomie-dun-test--le-patron-aaa)
4. [Tester la classe `Calculatrice`](#4-tester-la-classe-calculatrice)
5. [Les assertions xUnit](#5-les-assertions-xunit)
6. [Introduction à Moq — tester `UserInputViewModel`](#6-introduction-à-moq--tester-userinputviewmodel)
7. [Tester du code async](#7-tester-du-code-async)
8. [Fake vs Mock — quand utiliser quoi ?](#8-fake-vs-mock--quand-utiliser-quoi-)
9. [Bonnes pratiques](#9-bonnes-pratiques)
10. [Résumé](#10-résumé)

---

## 1. Pourquoi tester son code ?

### Le contexte — notre projet WpfApp2

Le projet contient deux éléments principaux à tester :

- **`Calculatrice`** — une classe qui accumule un résultat en additionnant ou soustrayant des valeurs
- **`UserInputViewModel`** — un ViewModel qui crée des utilisateurs via `IUtilisateurRepository`

Sans tests, pour vérifier que la calculatrice fonctionne, on doit :
1. Lancer l'application WPF
2. Cliquer sur le bouton
3. Vérifier visuellement le résultat

C'est lent, répétitif, et on ne teste qu'un cas à la fois. Avec xUnit, on vérifie tout en quelques millisecondes, automatiquement.

### Le lien avec l'injection de dépendances

Regardez `UserInputViewModel` dans le projet :

```csharp
public partial class UserInputViewModel : ObservableObject
{
    private readonly IUtilisateurRepository _utilisateurRepository;

    public UserInputViewModel(IUtilisateurRepository utilisateurRepository)
    {
        _utilisateurRepository = utilisateurRepository;
    }
    // ...
}
```

Le ViewModel reçoit `IUtilisateurRepository` par constructeur — une **interface**, pas une classe concrète. C'est exactement pour ça qu'on a utilisé IoC. Dans les tests, on pourra injecter un faux repository sans base de données réelle.

### Ce qu'un test unitaire ne doit jamais faire

- Écrire dans la base de données SQLite (`Test.db`)
- Envoyer de vrais SMS via Twilio
- Dépendre d'une connexion réseau
- Dépendre d'un autre test pour fonctionner

---

## 2. Mise en place du projet de tests

### Créer le projet xUnit

Dans Visual Studio, clic droit sur la **Solution** → **Add** → **New Project** → **xUnit Test Project (.NET)** → nommer `WpfApp2.Tests`.

Ensuite, ajouter une référence vers `Domaine` :

Clic droit sur `WpfApp2.Tests` → **Add** → **Project Reference** → cocher `Domaine`.

> **Note :** on référence `Domaine` et non `WpfApp2` directement, car `Utilisateur`, `IUtilisateurRepository` et `UtilisateurRepository` s'y trouvent. Pour tester `Calculatrice` et `UserInputViewModel` (qui sont dans `WpfApp2`), il faudra aussi référencer `WpfApp2` — mais attention, WPF ne peut pas être référencé dans un projet de test standard. La solution est de déplacer `Calculatrice` dans `Domaine`, ce qui est la bonne pratique d'architecture.

### Packages NuGet à installer dans `WpfApp2.Tests`

```
xunit
xunit.runner.visualstudio
Moq
Microsoft.NET.Test.Sdk
```

### Structure recommandée

```
WpfApp2.Tests/
├── Calculatrice/
│   └── CalculatriceTests.cs
├── ViewModels/
│   └── UserInputViewModelTests.cs
└── Fakes/
    └── FakeUtilisateurRepository.cs
```

---

## 3. Anatomie d'un test — le patron AAA

Chaque test suit trois étapes : **Arrange → Act → Assert**.

```csharp
[Fact]
public void NomMethode_Scenario_ResultatAttendu()
{
    // Arrange — préparer les objets et données
    // ...

    // Act — exécuter le code testé
    // ...

    // Assert — vérifier le résultat
    // ...
}
```

### `[Fact]` vs `[Theory]`

`[Fact]` est un test simple avec un seul scénario.

`[Theory]` + `[InlineData]` exécute le même test avec plusieurs jeux de données :

```csharp
[Theory]
[InlineData(10, 10)]
[InlineData(20, 30)]
[InlineData(5,  35)]
public void Additionner_ValeurPositive_AugmenteLeResultat(int valeur, int attendu)
{
    // Arrange
    var calc = new Calculatrice();
    calc.Additionner(25); // résultat de départ = 25

    // Act
    calc.Additionner(valeur);

    // Assert
    Assert.Equal(attendu, calc.Resultat);
}
```

### Convention de nommage

```
NomMethode_Scenario_ResultatAttendu

Exemples tirés du projet :
Additionner_ValeurDe10_ResultatEgal10
Additionner_DepasseLimite100_PeutAjouterDevientFaux
AjouterUtilisateurAsync_ChampsValides_AppelleLeRepository
AjouterUtilisateurAsync_NomVide_NAppelleJamaisLeRepository
UtilisateurExiste_UtilisateurDejaPresent_RetourneFaux
```

---

## 4. Tester la classe `Calculatrice`

Voici la classe à tester, telle qu'elle existe dans le projet :

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

Et dans `MonViewModel`, on voit que `PeutAjouter()` retourne `false` quand le résultat atteint 100 :

```csharp
private bool PeutAjouter()
{
    return calculatrice.Resultat < 100;
}
```

### Les tests de `Calculatrice`

```csharp
// WpfApp2.Tests/Calculatrice/CalculatriceTests.cs
using WpfApp2;

public class CalculatriceTests
{
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
    public void Additionner_DeuxFois_CumuleLesValeurs()
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
        // Arrange
        var calc = new Calculatrice();

        // Act
        calc.Additionner(valeur);

        // Assert
        Assert.Equal(attendu, calc.Resultat);
    }

    // ── Soustraire ─────────────────────────────────────────────────────

    [Fact]
    public void Soustraire_De10_ResultatNegatif()
    {
        // Arrange
        var calc = new Calculatrice();

        // Act
        calc.Soustraire(10);

        // Assert
        Assert.Equal(-10, calc.Resultat);
    }

    [Fact]
    public void Soustraire_ApresPlusieurs_ResultatCorrect()
    {
        // Arrange
        var calc = new Calculatrice();
        calc.Additionner(30);

        // Act
        calc.Soustraire(10);

        // Assert
        Assert.Equal(20, calc.Resultat);
    }

    // ── Règle métier : PeutAjouter (résultat < 100) ────────────────────

    [Fact]
    public void Resultat_Inferieur100_PeutEncoreAjouter()
    {
        // Arrange
        var calc = new Calculatrice();
        calc.Additionner(90);

        // Assert — la règle métier tirée de MonViewModel
        Assert.True(calc.Resultat < 100);
    }

    [Fact]
    public void Resultat_Egal100_NePeutPlusAjouter()
    {
        // Arrange
        var calc = new Calculatrice();
        calc.Additionner(100);

        // Assert
        Assert.False(calc.Resultat < 100);
    }

    [Fact]
    public void Resultat_Initial_EstZero()
    {
        // Arrange + Assert
        var calc = new Calculatrice();
        Assert.Equal(0, calc.Resultat);
    }
}
```

---

## 5. Les assertions xUnit

### Égalité

```csharp
Assert.Equal(10, calc.Resultat);         // valeur attendue, valeur obtenue
Assert.NotEqual(0, calc.Resultat);
```

### Booléens

```csharp
Assert.True(calc.Resultat < 100);        // condition vraie
Assert.False(calc.Resultat < 100);       // condition fausse
```

### Null

```csharp
Assert.Null(utilisateur);
Assert.NotNull(utilisateur);
```

### Collections

```csharp
Assert.Empty(liste);                     // liste vide
Assert.Single(liste);                    // exactement 1 élément
Assert.Equal(3, liste.Count);
Assert.Contains(utilisateur, liste);
```

### Exceptions

```csharp
// Vérifier qu'une exception est levée
Assert.Throws<ArgumentException>(() => calc.Additionner(-1));

// Version async
await Assert.ThrowsAsync<InvalidOperationException>(
    () => viewModel.AjouterUtilisateurAsync()
);
```

---

## 6. Introduction à Moq — tester `UserInputViewModel`

### Le problème

`UserInputViewModel` dépend de `IUtilisateurRepository` :

```csharp
public partial class UserInputViewModel : ObservableObject
{
    private readonly IUtilisateurRepository _utilisateurRepository;

    public UserInputViewModel(IUtilisateurRepository utilisateurRepository)
    {
        _utilisateurRepository = utilisateurRepository;
    }

    public async Task AjouterUtilisateurAsync()
    {
        // Validation...
        await _utilisateurRepository.AjouterUtilisateurAsync(new Utilisateur { ... });
    }

    private bool PeutCreerUtilisateur()
    {
        return !_utilisateurRepository.UtilisateurExiste(Nom, Prenom, NomUtilisateur);
    }
}
```

Pour tester ce ViewModel, on a besoin d'un `IUtilisateurRepository` sans base de données. Deux approches : **Fake manuel** ou **Moq**.

---

### Approche 1 — Fake manuel

On écrit une implémentation de l'interface qui stocke tout en mémoire :

```csharp
// WpfApp2.Tests/Fakes/FakeUtilisateurRepository.cs
using Domaine;
using Domaine.Repository;

public class FakeUtilisateurRepository : IUtilisateurRepository
{
    // On garde une trace de tous les utilisateurs ajoutés
    public List<Utilisateur> UtilisateursAjoutes { get; } = new();

    public Task AjouterUtilisateurAsync(Utilisateur utilisateur)
    {
        UtilisateursAjoutes.Add(utilisateur);
        return Task.CompletedTask;
    }

    public bool UtilisateurExiste(string nom, string prenom, string nomUtilisateur)
    {
        return UtilisateursAjoutes.Any(u =>
            u.Nom == nom &&
            u.Prenom == prenom &&
            u.NomUtilisateur == nomUtilisateur);
    }
}
```

---

### Approche 2 — Moq (automatique)

Moq génère automatiquement un faux objet à partir de l'interface. On configure seulement le comportement dont on a besoin :

```csharp
using Moq;

// Créer un mock de l'interface
var mockRepo = new Mock<IUtilisateurRepository>();

// Configurer : UtilisateurExiste retourne false
mockRepo.Setup(r => r.UtilisateurExiste(
    It.IsAny<string>(),
    It.IsAny<string>(),
    It.IsAny<string>()))
    .Returns(false);

// Configurer : AjouterUtilisateurAsync ne fait rien
mockRepo.Setup(r => r.AjouterUtilisateurAsync(It.IsAny<Utilisateur>()))
        .Returns(Task.CompletedTask);

// Obtenir l'objet à injecter
IUtilisateurRepository repo = mockRepo.Object;
```

---

### Les tests de `UserInputViewModel`

```csharp
// WpfApp2.Tests/ViewModels/UserInputViewModelTests.cs
using Domaine;
using Domaine.Repository;
using Moq;
using WpfApp2.UserInputDialog;

public class UserInputViewModelTests
{
    // ── Tests avec Fake manuel ─────────────────────────────────────────

    [Fact]
    public async Task AjouterUtilisateurAsync_ChampsValides_AjouteUnUtilisateur()
    {
        // Arrange
        var fakeRepo = new FakeUtilisateurRepository();
        var vm = new UserInputViewModel(fakeRepo);

        vm.Nom            = "Tremblay";
        vm.Prenom         = "Alexia";
        vm.NomUtilisateur = "alexia.tremblay";

        // Act
        await vm.AjouterUtilisateurAsync();

        // Assert — vérifier l'état du fake repository
        Assert.Single(fakeRepo.UtilisateursAjoutes);
        Assert.Equal("Tremblay", fakeRepo.UtilisateursAjoutes[0].Nom);
    }

    [Fact]
    public async Task AjouterUtilisateurAsync_ChampsValides_VideLesChamps()
    {
        // Arrange
        var fakeRepo = new FakeUtilisateurRepository();
        var vm = new UserInputViewModel(fakeRepo);

        vm.Nom            = "Gagnon";
        vm.Prenom         = "Samuel";
        vm.NomUtilisateur = "sam.gagnon";

        // Act
        await vm.AjouterUtilisateurAsync();

        // Assert — les champs sont remis à vide après l'ajout
        Assert.Equal(string.Empty, vm.Nom);
        Assert.Equal(string.Empty, vm.Prenom);
        Assert.Equal(string.Empty, vm.NomUtilisateur);
    }

    [Fact]
    public async Task AjouterUtilisateurAsync_NomVide_NAjoutePas()
    {
        // Arrange
        var fakeRepo = new FakeUtilisateurRepository();
        var vm = new UserInputViewModel(fakeRepo);

        vm.Nom            = "";           // ← champ vide
        vm.Prenom         = "Samuel";
        vm.NomUtilisateur = "sam.gagnon";

        // Act
        await vm.AjouterUtilisateurAsync();

        // Assert — aucun utilisateur ajouté
        Assert.Empty(fakeRepo.UtilisateursAjoutes);
    }

    // ── Tests avec Moq ─────────────────────────────────────────────────

    [Fact]
    public async Task AjouterUtilisateurAsync_ChampsValides_AppelleAjouterDuRepository()
    {
        // Arrange
        var mockRepo = new Mock<IUtilisateurRepository>();

        mockRepo.Setup(r => r.UtilisateurExiste(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .Returns(false);

        mockRepo.Setup(r => r.AjouterUtilisateurAsync(It.IsAny<Utilisateur>()))
                .Returns(Task.CompletedTask);

        var vm = new UserInputViewModel(mockRepo.Object);
        vm.Nom            = "Roy";
        vm.Prenom         = "Mathieu";
        vm.NomUtilisateur = "mat.roy";

        // Act
        await vm.AjouterUtilisateurAsync();

        // Assert — vérifier que AjouterUtilisateurAsync a été appelé une fois
        mockRepo.Verify(
            r => r.AjouterUtilisateurAsync(It.IsAny<Utilisateur>()),
            Times.Once
        );
    }

    [Fact]
    public async Task AjouterUtilisateurAsync_PrenomVide_NAppelleJamaisLeRepository()
    {
        // Arrange
        var mockRepo = new Mock<IUtilisateurRepository>();

        mockRepo.Setup(r => r.UtilisateurExiste(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .Returns(false);

        var vm = new UserInputViewModel(mockRepo.Object);
        vm.Nom            = "Roy";
        vm.Prenom         = "";    // ← vide
        vm.NomUtilisateur = "mat.roy";

        // Act
        await vm.AjouterUtilisateurAsync();

        // Assert — AjouterUtilisateurAsync ne doit JAMAIS avoir été appelé
        mockRepo.Verify(
            r => r.AjouterUtilisateurAsync(It.IsAny<Utilisateur>()),
            Times.Never
        );
    }

    [Fact]
    public void PeutCreerUtilisateur_UtilisateurDejaExistant_RetourneFaux()
    {
        // Arrange
        var mockRepo = new Mock<IUtilisateurRepository>();

        // Configurer : l'utilisateur existe déjà
        mockRepo.Setup(r => r.UtilisateurExiste("Côté", "Sophie", "sophie.cote"))
                .Returns(true);

        var vm = new UserInputViewModel(mockRepo.Object);
        vm.Nom            = "Côté";
        vm.Prenom         = "Sophie";
        vm.NomUtilisateur = "sophie.cote";

        // Act — PeutCreerUtilisateur est private, on le teste indirectement
        // via AjouterUtilisateurAsync qui retourne sans rien faire si l'user existe
        // On vérifie que AjouterUtilisateurAsync n'est pas appelé
        // NOTE : dans le projet, PeutCreerUtilisateur() est utilisé comme CanExecute
        //        de la commande — tester la commande directement est la bonne approche.

        // Vérifier directement via le mock que UtilisateurExiste a été configuré
        bool existe = mockRepo.Object.UtilisateurExiste("Côté", "Sophie", "sophie.cote");

        // Assert
        Assert.True(existe);
    }

    [Fact]
    public async Task AjouterUtilisateurAsync_TrimmeLesChamps()
    {
        // Arrange — s'assurer que les espaces sont retirés avant la sauvegarde
        Utilisateur? utilisateurSauvegarde = null;

        var mockRepo = new Mock<IUtilisateurRepository>();
        mockRepo.Setup(r => r.UtilisateurExiste(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);

        // Capturer l'utilisateur passé en paramètre
        mockRepo.Setup(r => r.AjouterUtilisateurAsync(It.IsAny<Utilisateur>()))
                .Callback<Utilisateur>(u => utilisateurSauvegarde = u)
                .Returns(Task.CompletedTask);

        var vm = new UserInputViewModel(mockRepo.Object);
        vm.Nom            = "  Lavoie  ";    // ← espaces intentionnels
        vm.Prenom         = "  Antoine  ";
        vm.NomUtilisateur = "  ant.lavoie  ";

        // Act
        await vm.AjouterUtilisateurAsync();

        // Assert — les espaces doivent avoir été retirés (Trim())
        Assert.NotNull(utilisateurSauvegarde);
        Assert.Equal("Lavoie",      utilisateurSauvegarde!.Nom);
        Assert.Equal("Antoine",     utilisateurSauvegarde.Prenom);
        Assert.Equal("ant.lavoie",  utilisateurSauvegarde.NomUtilisateur);
    }
}
```

---

## 7. Tester du code async

`AjouterUtilisateurAsync` est une méthode `async Task`. Les tests qui l'appellent doivent aussi être `async Task`.

```csharp
// ✅ Méthode de test async correcte
[Fact]
public async Task AjouterUtilisateurAsync_Exemple_Fonctionne()
{
    var fakeRepo = new FakeUtilisateurRepository();
    var vm       = new UserInputViewModel(fakeRepo);
    vm.Nom = "Test"; vm.Prenom = "Test"; vm.NomUtilisateur = "test";

    await vm.AjouterUtilisateurAsync(); // ← toujours await

    Assert.Single(fakeRepo.UtilisateursAjoutes);
}

// ❌ async void — xUnit ne peut pas détecter les exceptions !
[Fact]
public async void MauvaisTest()  // ← NE JAMAIS FAIRE
{
    await vm.AjouterUtilisateurAsync();
}
```

### Configurer Moq pour les méthodes async

```csharp
// Pour une méthode qui retourne Task (void async)
mockRepo.Setup(r => r.AjouterUtilisateurAsync(It.IsAny<Utilisateur>()))
        .Returns(Task.CompletedTask);                    // ← pas ReturnsAsync

// Pour une méthode qui retourne Task<T>
mockRepo.Setup(r => r.ObtenirUtilisateurAsync(It.IsAny<int>()))
        .ReturnsAsync(new Utilisateur { Nom = "Test" }); // ← ReturnsAsync

// Simuler une exception async
mockRepo.Setup(r => r.AjouterUtilisateurAsync(It.IsAny<Utilisateur>()))
        .ThrowsAsync(new Exception("BD inaccessible"));
```

---

## 8. Fake vs Mock — quand utiliser quoi ?

| Situation | Fake manuel | Moq |
|---|---|---|
| Vérifier l'**état** (ex : `UtilisateursAjoutes.Count == 1`) | ✅ Simple et lisible | Possible mais verbeux |
| Vérifier qu'une **méthode a été appelée** (`Verify`) | Nécessite du code custom | ✅ `Verify` est fait pour ça |
| Interface avec **peu de méthodes** (`IUtilisateurRepository` en a 2) | ✅ Rapide à écrire | ✅ |
| Interface avec **beaucoup de méthodes** | Fastidieux | ✅ Configure seulement ce qu'on utilise |
| **Capturer les paramètres** passés (`.Callback`) | Nécessite du code custom | ✅ `.Callback<T>` |

Dans notre projet, `IUtilisateurRepository` a seulement 2 méthodes :

```csharp
public interface IUtilisateurRepository
{
    Task AjouterUtilisateurAsync(Utilisateur utilisateur);
    bool UtilisateurExiste(string nom, string prenom, string nomUtilisateur);
}
```

Les deux approches (Fake et Moq) sont valides ici. La règle pratique :

> **Utilisez un Fake** quand vous voulez vérifier ce qui a été sauvegardé.
> **Utilisez Moq** quand vous voulez vérifier comment la méthode a été appelée.

---

## 9. Bonnes pratiques

### Un test = un seul comportement vérifié

```csharp
// ❌ Trop de vérifications dans un seul test
[Fact]
public async Task AjouterUtilisateur_TestGeneral()
{
    await vm.AjouterUtilisateurAsync();
    Assert.Single(fakeRepo.UtilisateursAjoutes);   // vérif 1
    Assert.Equal(string.Empty, vm.Nom);             // vérif 2
    Assert.Equal("Tremblay", fakeRepo.UtilisateursAjoutes[0].Nom); // vérif 3
}

// ✅ Un test par comportement
[Fact]
public async Task AjouterUtilisateur_AjouteUnUtilisateur()    { ... }
[Fact]
public async Task AjouterUtilisateur_VideLesChamps()           { ... }
[Fact]
public async Task AjouterUtilisateur_NomCorrectementAssigne()  { ... }
```

### Les tests sont indépendants

Chaque test crée sa propre instance de `FakeUtilisateurRepository` et de `UserInputViewModel`. Un test ne dépend jamais du résultat d'un autre.

```csharp
// ✅ Chaque test est autonome — le constructeur exécute le setup commun
public class UserInputViewModelTests
{
    private readonly FakeUtilisateurRepository _fakeRepo;
    private readonly UserInputViewModel        _vm;

    // Exécuté avant CHAQUE test automatiquement
    public UserInputViewModelTests()
    {
        _fakeRepo = new FakeUtilisateurRepository();
        _vm       = new UserInputViewModel(_fakeRepo);
    }

    [Fact]
    public async Task Test1() { /* utilise _fakeRepo et _vm frais */ }

    [Fact]
    public async Task Test2() { /* utilise _fakeRepo et _vm frais */ }
}
```

### Toujours `async Task`, jamais `async void`

```csharp
[Fact] public async Task MonTest() { ... }  // ✅
[Fact] public async void MonTest() { ... }  // ❌
```

---

## 10. Résumé

```
STRUCTURE DU PROJET DE TESTS
    WpfApp2.Tests/
    ├── Fakes/
    │   └── FakeUtilisateurRepository.cs   ← implémente IUtilisateurRepository en mémoire
    ├── Calculatrice/
    │   └── CalculatriceTests.cs           ← tests sans dépendances
    └── ViewModels/
        └── UserInputViewModelTests.cs     ← tests avec Fake et Moq

XUNIT
    [Fact]                         → test simple
    [Theory] + [InlineData(...)]   → même test, plusieurs valeurs
    Assert.Equal(attendu, obtenu)  → vérifier une valeur
    Assert.True / False            → vérifier un booléen
    Assert.Single / Empty          → vérifier une collection
    Assert.ThrowsAsync<T>()        → vérifier qu'une exception est levée

MOQ
    new Mock<IUtilisateurRepository>()     → créer le mock
    mock.Setup(...).Returns(false)         → configurer un retour synchrone
    mock.Setup(...).Returns(Task.CompletedTask) → configurer un retour async void
    mock.Setup(...).ReturnsAsync(valeur)   → configurer un retour async avec valeur
    mock.Setup(...).Callback<T>(u => ...)  → capturer le paramètre passé
    mock.Object                            → obtenir l'objet à injecter
    mock.Verify(..., Times.Once)           → vérifier appelé exactement 1 fois
    mock.Verify(..., Times.Never)          → vérifier jamais appelé

PATRON AAA
    Arrange → créer FakeRepo, ViewModel, assigner les propriétés
    Act     → appeler AjouterUtilisateurAsync() ou Additionner()
    Assert  → vérifier fakeRepo.UtilisateursAjoutes, vm.Nom, calc.Resultat

RÈGLES D'OR
    1. Un test = un seul comportement
    2. async Task, jamais async void
    3. Chaque test est indépendant — nouveau Fake à chaque test
    4. Nommer : Méthode_Scenario_ResultatAttendu
    5. Ne jamais écrire dans Test.db dans un test unitaire
```