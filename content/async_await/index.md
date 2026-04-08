---
title: "Async/Await"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 21
---

# Programmation asynchrone — `async` / `await`
---

## 1. Pourquoi la programmation asynchrone ?

### Le problème du code synchrone

Dans une application WPF, tout le code s'exécute sur le **thread UI**. Si ce thread est occupé (ex : attente d'une réponse réseau), l'interface **gèle** complètement — l'utilisateur ne peut plus rien faire.

```csharp
// ❌ Code synchrone — l'interface gèle pendant 3 secondes
public void ChargerDonnees()
{
    Thread.Sleep(3000); // Simule une requête réseau lente
    Console.WriteLine("Données chargées !");
    // Pendant ce temps : boutons bloqués, fenêtre ne répond plus
}
```

**Analogie — le guichet de la banque :**

Imaginons un guichetier (le thread UI) qui doit photocopier un document pour un client. S'il va lui-même faire la photocopie (synchrone), tous les autres clients attendent, immobiles. S'il envoie un assistant faire la copie et continue à servir d'autres clients (asynchrone), tout le monde avance.

### La solution asynchrone

```csharp
// ✅ Code asynchrone — l'interface reste réactive
public async Task ChargerDonneesAsync()
{
    await Task.Delay(3000); // "Attends, mais laisse le thread UI libre"
    Console.WriteLine("Données chargées !");
    // Pendant l'attente : boutons actifs, fenêtre réactive
}
```

### Quand utiliser l'asynchrone ?

| Opération | Synchrone ❌ | Asynchrone ✅ |
|---|---|---|
| Requête HTTP / API REST | Gèle l'UI | `await httpClient.GetAsync(...)` |
| Lecture / écriture de fichier | Gèle l'UI | `await File.ReadAllTextAsync(...)` |
| Requête base de données | Gèle l'UI | `await context.Taches.ToListAsync()` |
| Attente d'un délai | Gèle l'UI | `await Task.Delay(...)` |
| Calcul CPU intensif | Thread UI bloqué | `await Task.Run(...)` |

---

## 2. Les bases : `Task`, `async`, `await`

### Le type `Task`

Un `Task` représente une **opération asynchrone en cours**. C'est une promesse : « Je vais faire quelque chose, et je te dirai quand c'est terminé. »

```csharp
Task maPromesse = FaireQuelqueChoseAsync();
// → maPromesse est comme un ticket de vestiaire :
//   on continue à vivre sa vie, et on revient chercher le manteau plus tard
```

### Les mots-clés `async` et `await`

- `async` — déclare qu'une méthode est asynchrone (doit retourner `Task` ou `Task<T>`)
- `await` — « attends le résultat sans bloquer le thread »

```csharp
//         ↓ déclare la méthode asynchrone
public async Task TelechargerPageAsync()
{
    var client = new HttpClient();

    //    ↓ attend la réponse SANS bloquer le thread UI
    string contenu = await client.GetStringAsync("https://example.com");

    Console.WriteLine($"Page reçue : {contenu.Length} caractères");
}
```

### Visualiser l'exécution

```
Thread UI :  ──────[début]──[await]············[suite]──────
                              ↑                    ↑
                    Thread UI libéré       Thread UI reprend
                    (UI reste réactive)    (quand la tâche est finie)
```

### Chaîner les `await`

On peut enchaîner plusieurs opérations asynchrones dans la même méthode :

```csharp
public async Task SauvegarderEtNotifierAsync(string texte)
{
    // Étape 1 — écrire dans un fichier
    await File.WriteAllTextAsync("rapport.txt", texte);

    // Étape 2 — envoyer une notification HTTP
    var client = new HttpClient();
    await client.PostAsync("https://api.notif.com/envoyer",
        new StringContent("Rapport sauvegardé"));

    Console.WriteLine("Tout est terminé !");
}
```

Chaque `await` attend la fin de l'opération avant de passer à la suivante.

---

## 3. Retourner une valeur avec `Task<T>`

Quand la méthode asynchrone doit **retourner un résultat**, on utilise `Task<T>` :

| Méthode synchrone | Équivalent asynchrone |
|---|---|
| `string Lire()` | `Task<string> LireAsync()` |
| `List<Tache> Obtenir()` | `Task<List<Tache>> ObtenirAsync()` |
| `int Calculer()` | `Task<int> CalculerAsync()` |
| `void Faire()` | `Task FaireAsync()` |

### Exemple : appel HTTP avec retour de valeur

```csharp
public async Task<string> ObtenirMeteoAsync(string ville)
{
    var client = new HttpClient();

    // Attend la réponse HTTP — retourne un string
    string json = await client.GetStringAsync($"https://api.meteo.com/{ville}");

    return json; // ← retourner normalement, le compilateur s'occupe du reste
}

// Utilisation :
string meteo = await ObtenirMeteoAsync("Montréal");
Console.WriteLine(meteo);
```

### Exemple : lecture d'un fichier

```csharp
public async Task<List<string>> LireLignesAsync(string chemin)
{
    string contenu = await File.ReadAllTextAsync(chemin);
    return contenu.Split('\n').ToList();
}
```

---

## 4. Gestion des erreurs

Les exceptions dans les méthodes async se comportent **exactement comme en synchrone** — on utilise `try/catch` normalement.

```csharp
public async Task<string> TelechargerAsync(string url)
{
    try
    {
        var client = new HttpClient();
        string resultat = await client.GetStringAsync(url);
        return resultat;
    }
    catch (HttpRequestException ex)
    {
        Console.WriteLine($"Erreur réseau : {ex.Message}");
        return string.Empty;
    }
    catch (TaskCanceledException)
    {
        Console.WriteLine("La requête a expiré (timeout)");
        return string.Empty;
    }
}
```

### Annulation avec `CancellationToken`

On peut annuler une opération asynchrone en cours avec un `CancellationToken` :

```csharp
public async Task TelechargerAvecAnnulationAsync(CancellationToken token)
{
    var client = new HttpClient();

    try
    {
        // Le token permet d'annuler la requête si l'utilisateur clique "Annuler"
        string resultat = await client.GetStringAsync("https://example.com", token);
        Console.WriteLine("Téléchargement terminé !");
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("Téléchargement annulé par l'utilisateur.");
    }
}

// Dans le ViewModel :
private CancellationTokenSource _cts;

public async Task Demarrer()
{
    _cts = new CancellationTokenSource();
    await TelechargerAvecAnnulationAsync(_cts.Token);
}

public void Annuler()
{
    _cts?.Cancel(); // Déclenche l'annulation
}
```

---

## 5. Exécution parallèle

### `await` en séquence vs en parallèle

Par défaut, les `await` s'exécutent **en séquence** (l'un après l'autre) :

```csharp
// Séquentiel — total : ~3 secondes (1s + 2s)
string a = await TacheUneAsync();   // attend 1s
string b = await TacheDeuxAsync();  // attend 2s ensuite
```

Pour exécuter en **parallèle**, on utilise `Task.WhenAll` :

```csharp
// Parallèle — total : ~2 secondes (le plus long des deux)
Task<string> tacheA = TacheUneAsync();   // démarre immédiatement
Task<string> tacheB = TacheDeuxAsync();  // démarre immédiatement

string[] resultats = await Task.WhenAll(tacheA, tacheB); // attend les deux
string a = resultats[0];
string b = resultats[1];
```

### Exemple concret : charger plusieurs ressources en même temps

```csharp
public async Task ChargerTableauDeBordAsync()
{
    // Lancer les 3 requêtes en même temps
    Task<List<Tache>>      tachesTache   = _repo.ObtenirTachesAsync();
    Task<List<Utilisateur>> tachesUsers  = _repo.ObtenirUtilisateursAsync();
    Task<Statistiques>      tachesStats  = _repo.ObtenirStatistiquesAsync();

    // Attendre que les 3 soient terminées
    await Task.WhenAll(tachesTache, tachesUsers, tachesStats);

    // Lire les résultats (déjà disponibles)
    Taches        = tachesTache.Result;
    Utilisateurs  = tachesUsers.Result;
    Stats         = tachesStats.Result;
}
```

### `Task.WhenAny` — prendre le premier qui répond

```csharp
// Prend le résultat du serveur le plus rapide
Task<string> serveur1 = RequeteAsync("https://serveur1.com");
Task<string> serveur2 = RequeteAsync("https://serveur2.com");

Task<string> lePlusRapide = await Task.WhenAny(serveur1, serveur2);
string resultat = await lePlusRapide;
```

---

## 6. Async dans WPF et MVVM

### Règle d'or WPF

> Toute opération longue doit être `async`. On ne bloque **jamais** le thread UI.

### ViewModel avec commande asynchrone

```csharp
public class TachesViewModel : INotifyPropertyChanged
{
    private readonly ITacheService _service;
    private bool _estEnChargement;

    public bool EstEnChargement
    {
        get => _estEnChargement;
        set { _estEnChargement = value; OnPropertyChanged(); }
    }

    public ObservableCollection<Tache> Taches { get; set; } = new();

    // Commande asynchrone
    public ICommand ChargerCommand { get; }

    public TachesViewModel(ITacheService service)
    {
        _service = service;
        ChargerCommand = new AsyncRelayCommand(ChargerTachesAsync);
    }

    private async Task ChargerTachesAsync()
    {
        EstEnChargement = true;

        try
        {
            // Ne bloque pas l'UI — le spinner tourne pendant ce temps
            var taches = await _service.ObtenirToutesAsync();

            Taches.Clear();
            foreach (var t in taches)
                Taches.Add(t);
        }
        finally
        {
            EstEnChargement = false;
        }
    }
}
```

### Repository avec Entity Framework async

```csharp
public class TacheRepository : ITacheRepository
{
    private readonly ApplicationDbContext _context;

    public TacheRepository(ApplicationDbContext context)
        => _context = context;

    public async Task<List<Tache>> ObtenirToutesAsync()
        => await _context.Taches.ToListAsync();

    public async Task AjouterAsync(Tache tache)
    {
        _context.Taches.Add(tache);
        await _context.SaveChangesAsync();
    }

    public async Task SupprimerAsync(int id)
    {
        var tache = await _context.Taches.FindAsync(id);
        if (tache != null)
        {
            _context.Taches.Remove(tache);
            await _context.SaveChangesAsync();
        }
    }
}
```

### Initialisation asynchrone au démarrage

On ne peut pas mettre `await` dans un constructeur. On utilise une méthode `InitialiserAsync` appelée depuis la commande ou l'événement `Loaded` :

```csharp
public class MainViewModel
{
    public ICommand InitialiserCommand { get; }

    public MainViewModel()
    {
        // ✅ Commande asynchrone appelée depuis la vue
        InitialiserCommand = new AsyncRelayCommand(InitialiserAsync);
    }

    private async Task InitialiserAsync()
    {
        await ChargerDonneesAsync();
        await VerifierMisesAJourAsync();
    }
}
```

```xml
<!-- MainWindow.xaml -->
<Window ...
        Loaded="{Binding InitialiserCommand}">
```

---

## 7. Bonnes pratiques et pièges courants

### ✅ À faire

```csharp
// 1. Toujours suffixer les méthodes async avec "Async"
public async Task ChargerAsync() { }

// 2. Toujours await une Task — ne jamais l'ignorer
await SauvegarderAsync(); // ✅
SauvegarderAsync();       // ❌ Exception ignorée silencieusement !

// 3. Propager async jusqu'en haut (async "tout du long")
public async Task<List<Tache>> ObtenirAsync()      // Service
    => await _repo.ObtenirToutesAsync();
public async Task<List<Tache>> ObtenirToutesAsync() // Repository
    => await _context.Taches.ToListAsync();
```

### ❌ Pièges courants

```csharp
// PIÈGE 1 : .Result ou .Wait() — bloque le thread et risque de deadlock
var taches = _service.ObtenirAsync().Result;  // ❌ Deadlock possible en WPF !
var taches = await _service.ObtenirAsync();   // ✅

// PIÈGE 2 : async void — impossible à await, exceptions non gérées
public async void ChargerDonnees() { }        // ❌ (sauf gestionnaire d'événements)
public async Task ChargerDonneesAsync() { }   // ✅

// PIÈGE 3 : oublier await dans une boucle
foreach (var tache in taches)
    EnvoyerNotificationAsync(tache);          // ❌ Toutes les tâches ignorées !

foreach (var tache in taches)
    await EnvoyerNotificationAsync(tache);    // ✅
```

### Exception : `async void` dans les gestionnaires d'événements WPF

C'est le **seul cas acceptable** pour `async void` :

```csharp
// ✅ Acceptable — les event handlers WPF doivent retourner void
private async void BoutonCharger_Click(object sender, RoutedEventArgs e)
{
    await ChargerDonneesAsync();
}
```

### Tableau récapitulatif

| Situation | À utiliser |
|---|---|
| Méthode async sans retour | `async Task` |
| Méthode async avec retour | `async Task<T>` |
| Gestionnaire d'événement WPF | `async void` (exception acceptée) |
| Attendre N tâches | `await Task.WhenAll(t1, t2, ...)` |
| Prendre la 1re tâche terminée | `await Task.WhenAny(t1, t2, ...)` |
| Calcul CPU intensif | `await Task.Run(() => CalculLong())` |
| Annuler une opération | `CancellationTokenSource` + `token` |

---

## 8. Résumé

```
PROBLÈME
    Code synchrone → thread UI bloqué → interface gelée

SOLUTION
    async / await → thread UI libéré pendant l'attente

TYPES DE RETOUR
    void             → Task          (méthode sans valeur)
    string           → Task<string>  (méthode avec valeur)
    List<T>          → Task<List<T>>

RÈGLES D'OR
    1. Toujours suffixer les méthodes async avec "Async"
    2. Toujours await une Task — jamais .Result ni .Wait()
    3. async void uniquement pour les event handlers WPF
    4. Propager async tout du long (service → repo → contexte)

PARALLÉLISME
    await Task.WhenAll(t1, t2)  → attend TOUS les résultats
    await Task.WhenAny(t1, t2)  → prend LE PREMIER résultat
    await Task.Run(...)          → délègue un calcul CPU lourd
```