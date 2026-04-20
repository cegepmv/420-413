---
title: "Mini-projets"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 24
---

# Mini-projets progressifs

## Mini-projet 1 : Calculatrice avec historique
**Niveau : ⭐⭐ Intermédiaire**

### Objectifs pédagogiques
- Patron Commande (annuler/refaire)
- INotifyPropertyChanged
- Data Binding de base

### Spécifications

**Fonctionnalités :**
1. Opérations : Addition, Soustraction, Multiplication, Division
2. Historique des calculs
3. Annuler/Refaire les opérations
4. Effacer l'historique

**Interface :**
```
┌──────────────────────────────┐
│    CALCULATRICE              │
├──────────────────────────────┤
│  Résultat: [0]               │
│                              │
│  [7] [8] [9] [÷]             │
│  [4] [5] [6] [×]             │
│  [1] [2] [3] [-]             │
│  [0] [.] [=] [+]             │
│                              │
│  [↩ Annuler] [↪ Refaire] [C] │
├──────────────────────────────┤
│  HISTORIQUE                  │
│  - 5 + 3 = 8                 │
│  - 8 × 2 = 16                │
│  - 16 ÷ 4 = 4                │
└──────────────────────────────┘
```

### Structure du projet

```csharp
// Models/Operation.cs
public class Operation
{
    public double Operande1 { get; set; }
    public double Operande2 { get; set; }
    public string Operateur { get; set; }
    public double Resultat { get; set; }
    
    public override string ToString()
    {
        return $"{Operande1} {Operateur} {Operande2} = {Resultat}";
    }
}

// Commands/ICommande.cs
public interface ICommande
{
    void Executer();
    void Annuler();
}

// Commands/CommandeCalculer.cs
public class CommandeCalculer : ICommande
{
    private CalculatriceViewModel viewModel;
    private Operation operation;
    private double resultatPrecedent;
    
    public CommandeCalculer(CalculatriceViewModel vm, Operation op)
    {
        viewModel = vm;
        operation = op;
        resultatPrecedent = vm.Resultat;
    }
    
    public void Executer()
    {
        // Calculer selon l'opérateur
        switch (operation.Operateur)
        {
            case "+":
                operation.Resultat = operation.Operande1 + operation.Operande2;
                break;
            // ... autres opérations
        }
        
        viewModel.Resultat = operation.Resultat;
        viewModel.Historique.Add(operation);
    }
    
    public void Annuler()
    {
        viewModel.Resultat = resultatPrecedent;
        viewModel.Historique.Remove(operation);
    }
}

// ViewModels/CalculatriceViewModel.cs
public class CalculatriceViewModel : BaseViewModel
{
    private double resultat;
    public double Resultat
    {
        get => resultat;
        set => SetProperty(ref resultat, value);
    }
    
    public ObservableCollection<Operation> Historique { get; set; }
    
    private Stack<ICommande> historiqueCommandes = new Stack<ICommande>();
    private Stack<ICommande> commandesAnnulees = new Stack<ICommande>();
    
    public ICommand CalculerCommand { get; }
    public ICommand AnnulerCommand { get; }
    public ICommand RefaireCommand { get; }
    
    // ... implémentation
}
```

### Critères de réussite
- [ ] Les 4 opérations fonctionnent correctement
- [ ] L'historique se met à jour automatiquement
- [ ] Annuler restaure le résultat précédent
- [ ] Refaire réapplique l'opération annulée
- [ ] Gestion des erreurs (division par zéro)

---

## Mini-projet 2 : Gestionnaire de tâches avec priorités
**Niveau : ⭐⭐⭐ Intermédiaire-Avancé**

### Objectifs pédagogiques
- MVVM complet
- ObservableCollection
- Filtrage et tri
- Sauvegarde/chargement (JSON)

### Spécifications

**Fonctionnalités :**
1. Ajouter/modifier/supprimer des tâches
2. Marquer comme terminé
3. Assigner des priorités (Haute, Moyenne, Basse)
4. Filtrer par statut et priorité
5. Trier par date, priorité, ou titre
6. Recherche par mots-clés
7. Statistiques (nombre de tâches par statut/priorité)
8. Sauvegarder/charger depuis un fichier

**Interface :**
```
┌────────────────────────────────────────────────────┐
│  GESTIONNAIRE DE TÂCHES                            │
├────────────────────────────────────────────────────┤
│  [🔍 Rechercher...] [Filtre: Tous ▼] [Tri: Date ▼]│
├────────────────────────────────────────────────────┤
│  ☐ Terminer le rapport    [🔴 Haute]  2024-02-20  │
│  ☑ Répondre aux emails   [🟡 Moyenne] 2024-02-19  │
│  ☐ Réviser pour l'examen  [🔴 Haute]  2024-02-25  │
├────────────────────────────────────────────────────┤
│  Nouvelle tâche:                                   │
│  Titre: [____________________]                     │
│  Priorité: [Moyenne ▼]                            │
│  Date limite: [📅 2024-02-20]                     │
│  [➕ Ajouter]  [✏️ Modifier]  [🗑️ Supprimer]      │
├────────────────────────────────────────────────────┤
│  📊 STATISTIQUES                                   │
│  Total: 3  | En cours: 2  | Terminées: 1          │
│  🔴 Haute: 2  🟡 Moyenne: 1  🟢 Basse: 0          │
└────────────────────────────────────────────────────┘
```

### Structure du projet

```csharp
// Models/Tache.cs
public class Tache : BaseViewModel
{
    public int Id { get; set; }
    
    private string titre;
    public string Titre
    {
        get => titre;
        set => SetProperty(ref titre, value);
    }
    
    private bool estTerminee;
    public bool EstTerminee
    {
        get => estTerminee;
        set => SetProperty(ref estTerminee, value);
    }
    
    private Priorite priorite;
    public Priorite Priorite
    {
        get => priorite;
        set => SetProperty(ref priorite, value);
    }
    
    private DateTime dateLimite;
    public DateTime DateLimite
    {
        get => dateLimite;
        set => SetProperty(ref dateLimite, value);
    }
    
    public DateTime DateCreation { get; set; }
}

// Models/Priorite.cs (enum)
public enum Priorite
{
    Basse,
    Moyenne,
    Haute
}

// Services/ITacheRepository.cs
public interface ITacheRepository
{
    List<Tache> ChargerTaches();
    void SauvegarderTaches(List<Tache> taches);
}

// Services/TacheRepository.cs
public class TacheRepository : ITacheRepository
{
    private readonly string cheminFichier = "taches.json";
    
    public List<Tache> ChargerTaches()
    {
        if (!File.Exists(cheminFichier))
            return new List<Tache>();
        
        string json = File.ReadAllText(cheminFichier);
        return JsonSerializer.Deserialize<List<Tache>>(json);
    }
    
    public void SauvegarderTaches(List<Tache> taches)
    {
        string json = JsonSerializer.Serialize(taches, new JsonSerializerOptions 
        { 
            WriteIndented = true 
        });
        File.WriteAllText(cheminFichier, json);
    }
}

// ViewModels/TachesViewModel.cs
public class TachesViewModel : BaseViewModel
{
    private readonly ITacheRepository repository;
    
    private ObservableCollection<Tache> toutesLesTaches;
    public ObservableCollection<Tache> TachesAffichees { get; set; }
    
    // Propriétés de filtrage
    private string rechercheTexte;
    public string RechercheTexte
    {
        get => rechercheTexte;
        set
        {
            SetProperty(ref rechercheTexte, value);
            AppliquerFiltres();
        }
    }
    
    private string filtreStatut = "Tous";
    public string FiltreStatut
    {
        get => filtreStatut;
        set
        {
            SetProperty(ref filtreStatut, value);
            AppliquerFiltres();
        }
    }
    
    // Statistiques
    public int NombreTotal => toutesLesTaches.Count;
    public int NombreEnCours => toutesLesTaches.Count(t => !t.EstTerminee);
    public int NombreTerminees => toutesLesTaches.Count(t => t.EstTerminee);
    public int NombreHaute => toutesLesTaches.Count(t => t.Priorite == Priorite.Haute);
    
    // Commandes
    public ICommand AjouterCommand { get; }
    public ICommand ModifierCommand { get; }
    public ICommand SupprimerCommand { get; }
    public ICommand SauvegarderCommand { get; }
    
    // Constructeur avec injection de dépendances
    public TachesViewModel(ITacheRepository repository)
    {
        this.repository = repository;
        
        TachesAffichees = new ObservableCollection<Tache>();
        toutesLesTaches = new ObservableCollection<Tache>();
        
        // S'abonner aux changements pour mettre à jour les stats
        toutesLesTaches.CollectionChanged += (s, e) => MettreAJourStatistiques();
        
        // Initialiser les commandes
        AjouterCommand = new RelayCommand(AjouterTache, PeutAjouter);
        SupprimerCommand = new RelayCommand(SupprimerTache, PeutSupprimer);
        SauvegarderCommand = new RelayCommand(Sauvegarder);
        
        // Charger les tâches
        ChargerTaches();
    }
    
    private void ChargerTaches()
    {
        var taches = repository.ChargerTaches();
        toutesLesTaches.Clear();
        foreach (var tache in taches)
        {
            toutesLesTaches.Add(tache);
        }
        AppliquerFiltres();
    }
    
    private void AppliquerFiltres()
    {
        TachesAffichees.Clear();
        
        var resultats = toutesLesTaches.AsEnumerable();
        
        // Filtre de recherche
        if (!string.IsNullOrWhiteSpace(RechercheTexte))
        {
            resultats = resultats.Where(t => 
                t.Titre.Contains(RechercheTexte, StringComparison.OrdinalIgnoreCase));
        }
        
        // Filtre de statut
        if (FiltreStatut == "En cours")
            resultats = resultats.Where(t => !t.EstTerminee);
        else if (FiltreStatut == "Terminées")
            resultats = resultats.Where(t => t.EstTerminee);
        
        foreach (var tache in resultats)
        {
            TachesAffichees.Add(tache);
        }
    }
    
    private void MettreAJourStatistiques()
    {
        OnPropertyChanged(nameof(NombreTotal));
        OnPropertyChanged(nameof(NombreEnCours));
        OnPropertyChanged(nameof(NombreTerminees));
        OnPropertyChanged(nameof(NombreHaute));
    }
    
    private void Sauvegarder()
    {
        repository.SauvegarderTaches(toutesLesTaches.ToList());
    }
}
```

### Critères de réussite
- [ ] CRUD complet (Create, Read, Update, Delete)
- [ ] Filtres et recherche fonctionnels
- [ ] Statistiques mises à jour automatiquement
- [ ] Sauvegarde/chargement persistant
- [ ] Interface responsive
- [ ] Validation des données

---

## Mini-projet 3 : Application de chat avec patron Observateur
**Niveau : ⭐⭐⭐ Avancé**

### Objectifs pédagogiques
- Patron Observateur avancé
- Services avec injection de dépendances
- Gestion d'état complexe
- Temps réel (simulation)

### Spécifications

**Fonctionnalités :**
1. Salons de discussion multiples
2. Envoi/réception de messages
3. Notifications en temps réel
4. Liste des utilisateurs connectés
5. Indicateur "en train d'écrire..."
6. Historique des messages
7. Recherche dans les messages

**Architecture :**
```
- IChatService (abstraction)
  - ChatService (implémentation réelle)
  - FakeChatService (pour tests)
  
- Observateurs:
  - IMessageObservateur
  - IUtilisateurObservateur
  - INotificationObservateur
```

### Structure du projet

```csharp
// Models/Message.cs
public class Message
{
    public int Id { get; set; }
    public string Expediteur { get; set; }
    public string Contenu { get; set; }
    public DateTime Horodatage { get; set; }
    public string SalonId { get; set; }
}

// Models/Salon.cs
public class Salon
{
    public string Id { get; set; }
    public string Nom { get; set; }
    public ObservableCollection<Message> Messages { get; set; }
    public ObservableCollection<string> UtilisateursConnectes { get; set; }
}

// Services/IChatService.cs
public interface IChatService
{
    void AbonnerMessages(IMessageObservateur observateur);
    void DesabonnerMessages(IMessageObservateur observateur);
    
    void EnvoyerMessage(string salonId, string expediteur, string contenu);
    List<Message> ObtenirHistorique(string salonId);
    
    void RejoindreSalon(string salonId, string utilisateur);
    void QuitterSalon(string salonId, string utilisateur);
}

// Services/ChatService.cs
public class ChatService : IChatService
{
    private List<IMessageObservateur> observateursMessages = new List<IMessageObservateur>();
    private Dictionary<string, Salon> salons = new Dictionary<string, Salon>();
    
    public void AbonnerMessages(IMessageObservateur observateur)
    {
        observateursMessages.Add(observateur);
    }
    
    public void EnvoyerMessage(string salonId, string expediteur, string contenu)
    {
        var message = new Message
        {
            Id = GenererId(),
            SalonId = salonId,
            Expediteur = expediteur,
            Contenu = contenu,
            Horodatage = DateTime.Now
        };
        
        if (!salons.ContainsKey(salonId))
            salons[salonId] = new Salon { Id = salonId, Messages = new ObservableCollection<Message>() };
        
        salons[salonId].Messages.Add(message);
        
        // Notifier tous les observateurs
        foreach (var obs in observateursMessages)
        {
            obs.NouveauMessage(message);
        }
    }
    
    // Simulation de messages entrants (autre utilisateur)
    public void SimulerReception(string salonId)
    {
        var random = new Random();
        var expediteurs = new[] { "Alice", "Bob", "Charlie" };
        var messages = new[] 
        { 
            "Salut!", 
            "Comment ça va?", 
            "Avez-vous vu le dernier rapport?" 
        };
        
        string expediteur = expediteurs[random.Next(expediteurs.Length)];
        string contenu = messages[random.Next(messages.Length)];
        
        EnvoyerMessage(salonId, expediteur, contenu);
    }
}

// Observateurs/IMessageObservateur.cs
public interface IMessageObservateur
{
    void NouveauMessage(Message message);
}

// ViewModels/ChatViewModel.cs
public class ChatViewModel : BaseViewModel, IMessageObservateur
{
    private readonly IChatService chatService;
    
    private Salon salonActuel;
    public Salon SalonActuel
    {
        get => salonActuel;
        set => SetProperty(ref salonActuel, value);
    }
    
    public ObservableCollection<Salon> Salons { get; set; }
    
    private string messageActuel;
    public string MessageActuel
    {
        get => messageActuel;
        set => SetProperty(ref messageActuel, value);
    }
    
    private string utilisateurActuel;
    public string UtilisateurActuel { get; set; }
    
    public ICommand EnvoyerMessageCommand { get; }
    public ICommand ChangerSalonCommand { get; }
    
    public ChatViewModel(IChatService chatService)
    {
        this.chatService = chatService;
        
        // S'abonner aux nouveaux messages
        chatService.AbonnerMessages(this);
        
        Salons = new ObservableCollection<Salon>();
        
        EnvoyerMessageCommand = new RelayCommand(EnvoyerMessage, PeutEnvoyer);
        ChangerSalonCommand = new RelayCommand<Salon>(ChangerSalon);
        
        InitialiserSalons();
    }
    
    private void InitialiserSalons()
    {
        Salons.Add(new Salon 
        { 
            Id = "general", 
            Nom = "Général",
            Messages = new ObservableCollection<Message>()
        });
        Salons.Add(new Salon 
        { 
            Id = "technique", 
            Nom = "Technique",
            Messages = new ObservableCollection<Message>()
        });
        
        SalonActuel = Salons[0];
    }
    
    // Implémentation de IMessageObservateur
    public void NouveauMessage(Message message)
    {
        // Trouver le salon correspondant
        var salon = Salons.FirstOrDefault(s => s.Id == message.SalonId);
        if (salon != null)
        {
            // WPF nécessite d'être sur le thread UI
            Application.Current.Dispatcher.Invoke(() =>
            {
                salon.Messages.Add(message);
                
                // Notification si ce n'est pas le salon actuel
                if (salon != SalonActuel)
                {
                    // Afficher une notification
                }
            });
        }
    }
    
    private void EnvoyerMessage()
    {
        chatService.EnvoyerMessage(SalonActuel.Id, UtilisateurActuel, MessageActuel);
        MessageActuel = "";
    }
    
    private bool PeutEnvoyer()
    {
        return !string.IsNullOrWhiteSpace(MessageActuel);
    }
}
```

### XAML suggéré

```xml
<Grid>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="200"/>
        <ColumnDefinition Width="*"/>
    </Grid.ColumnDefinitions>
    
    <!-- Liste des salons -->
    <ListBox Grid.Column="0" 
             ItemsSource="{Binding Salons}"
             SelectedItem="{Binding SalonActuel}"
             DisplayMemberPath="Nom"/>
    
    <!-- Zone de chat -->
    <Grid Grid.Column="1">
        <Grid.RowDefinitions>
            <RowDefinition Height="*"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>
        
        <!-- Messages -->
        <ListBox Grid.Row="0" ItemsSource="{Binding SalonActuel.Messages}">
            <ListBox.ItemTemplate>
                <DataTemplate>
                    <StackPanel Margin="5">
                        <TextBlock Text="{Binding Expediteur}" FontWeight="Bold"/>
                        <TextBlock Text="{Binding Contenu}"/>
                        <TextBlock Text="{Binding Horodatage}" 
                                   FontSize="10" 
                                   Foreground="Gray"/>
                    </StackPanel>
                </DataTemplate>
            </ListBox.ItemTemplate>
        </ListBox>
        
        <!-- Zone de saisie -->
        <StackPanel Grid.Row="1" Orientation="Horizontal" Margin="5">
            <TextBox Text="{Binding MessageActuel, UpdateSourceTrigger=PropertyChanged}"
                     Width="400"/>
            <Button Content="Envoyer" 
                    Command="{Binding EnvoyerMessageCommand}"
                    Width="80"
                    Margin="5,0,0,0"/>
        </StackPanel>
    </Grid>
</Grid>
```

### Critères de réussite
- [ ] Plusieurs salons fonctionnent indépendamment
- [ ] Messages apparaissent en temps réel
- [ ] Notification pour nouveau message
- [ ] Changement de salon fluide
- [ ] Historique conservé
- [ ] Tests unitaires du ChatService

---

## Mini-projet 4 : Gestion d'inventaire avec commandes et injection
**Niveau : ⭐⭐⭐⭐ Avancé**

### Objectifs pédagogiques
- Architecture complète MVVM
- Patron Commande complet
- Injection de dépendances avancée
- Repository pattern
- Validation
- Export/Import de données

### Spécifications

**Fonctionnalités :**
1. Gestion complète de produits (CRUD)
2. Catégories de produits
3. Gestion des stocks (entrées/sorties)
4. Alertes de stock faible
5. Historique des mouvements
6. Recherche et filtres avancés
7. Export Excel/CSV
8. Rapports statistiques

**Architecture :**
```
Services/
  - IProduitRepository
  - ICategorieRepository  
  - IExportService
  - IValidationService

Commands/
  - CommandeAjouterProduit
  - CommandeModifierProduit
  - CommandeSupprimerProduit
  - CommandeAjusterStock

ViewModels/
  - MainViewModel (orchestrateur)
  - ProduitsViewModel
  - CategoriesViewModel
  - StockViewModel
  - RapportsViewModel
```

### Exemple de structure partielle

```csharp
// Models/Produit.cs
public class Produit : BaseViewModel
{
    public int Id { get; set; }
    
    private string nom;
    public string Nom
    {
        get => nom;
        set => SetProperty(ref nom, value);
    }
    
    private decimal prix;
    public decimal Prix
    {
        get => prix;
        set => SetProperty(ref prix, value);
    }
    
    private int quantiteStock;
    public int QuantiteStock
    {
        get => quantiteStock;
        set 
        { 
            SetProperty(ref quantiteStock, value);
            OnPropertyChanged(nameof(EstEnRupture));
            OnPropertyChanged(nameof(EstStockFaible));
        }
    }
    
    public int SeuilAlerte { get; set; } = 10;
    
    public bool EstEnRupture => QuantiteStock == 0;
    public bool EstStockFaible => QuantiteStock > 0 && QuantiteStock <= SeuilAlerte;
    
    private Categorie categorie;
    public Categorie Categorie
    {
        get => categorie;
        set => SetProperty(ref categorie, value);
    }
}

// Services/IProduitRepository.cs
public interface IProduitRepository
{
    Task<List<Produit>> ObtenirTousAsync();
    Task<Produit> ObtenirParIdAsync(int id);
    Task<Produit> AjouterAsync(Produit produit);
    Task ModifierAsync(Produit produit);
    Task SupprimerAsync(int id);
    Task<List<Produit>> RechercherAsync(string critere);
}

// Services/IExportService.cs
public interface IExportService
{
    void ExporterCSV(List<Produit> produits, string cheminFichier);
    void ExporterExcel(List<Produit> produits, string cheminFichier);
}

// Commands/CommandeAjouterProduit.cs
public class CommandeAjouterProduit : ICommande
{
    private readonly IProduitRepository repository;
    private readonly ObservableCollection<Produit> collection;
    private Produit produit;
    private Produit produitAjoute;
    
    public CommandeAjouterProduit(IProduitRepository repository, 
                                   ObservableCollection<Produit> collection,
                                   Produit produit)
    {
        this.repository = repository;
        this.collection = collection;
        this.produit = produit;
    }
    
    public async void Executer()
    {
        produitAjoute = await repository.AjouterAsync(produit);
        collection.Add(produitAjoute);
    }
    
    public async void Annuler()
    {
        await repository.SupprimerAsync(produitAjoute.Id);
        collection.Remove(produitAjoute);
    }
}

// ViewModels/ProduitsViewModel.cs
public class ProduitsViewModel : BaseViewModel
{
    private readonly IProduitRepository repository;
    private readonly IValidationService validationService;
    private readonly GestionnaireCommandes gestionnaireCommandes;
    
    public ObservableCollection<Produit> Produits { get; set; }
    public ObservableCollection<Produit> ProduitsAffichees { get; set; }
    public ObservableCollection<Categorie> Categories { get; set; }
    
    // Propriétés pour le formulaire
    private Produit produitSelectionne;
    public Produit ProduitSelectionne
    {
        get => produitSelectionne;
        set => SetProperty(ref produitSelectionne, value);
    }
    
    // Statistiques
    public int NombreProduits => Produits.Count;
    public int NombreRuptures => Produits.Count(p => p.EstEnRupture);
    public int NombreAlertes => Produits.Count(p => p.EstStockFaible);
    public decimal ValeurTotaleStock => Produits.Sum(p => p.Prix * p.QuantiteStock);
    
    // Commandes
    public ICommand AjouterCommand { get; }
    public ICommand ModifierCommand { get; }
    public ICommand SupprimerCommand { get; }
    public ICommand AnnulerCommand { get; }
    public ICommand RefaireCommand { get; }
    public ICommand ExporterCommand { get; }
    
    // Constructeur avec injection
    public ProduitsViewModel(IProduitRepository repository, 
                            IValidationService validationService)
    {
        this.repository = repository;
        this.validationService = validationService;
        this.gestionnaireCommandes = new GestionnaireCommandes();
        
        Produits = new ObservableCollection<Produit>();
        ProduitsAffichees = new ObservableCollection<Produit>();
        
        AjouterCommand = new RelayCommand(AjouterProduit, PeutAjouter);
        AnnulerCommand = new RelayCommand(() => gestionnaireCommandes.Annuler());
        
        ChargerProduitsAsync();
    }
    
    private async void ChargerProduitsAsync()
    {
        var produits = await repository.ObtenirTousAsync();
        Produits.Clear();
        foreach (var p in produits)
        {
            Produits.Add(p);
        }
        AppliquerFiltres();
    }
    
    private void AjouterProduit()
    {
        // Valider d'abord
        var erreurs = validationService.ValiderProduit(ProduitSelectionne);
        if (erreurs.Any())
        {
            // Afficher les erreurs
            return;
        }
        
        // Créer et exécuter la commande
        var commande = new CommandeAjouterProduit(repository, Produits, ProduitSelectionne);
        gestionnaireCommandes.ExecuterCommande(commande);
        
        MettreAJourStatistiques();
    }
}
```

### Critères de réussite
- [ ] Architecture propre avec injection de dépendances
- [ ] Toutes les opérations CRUD avec annuler/refaire
- [ ] Validation complète des données
- [ ] Export fonctionnel
- [ ] Statistiques en temps réel
- [ ] Tests unitaires complets
- [ ] Documentation du code

---


## Mini-projet 5 : Application WPF avec MVVM et Repository

### Contexte

Vous devez créer une application de bureau en **WPF** permettant d'ajouter un utilisateur dans une base de données. Le projet doit respecter les bonnes pratiques d'architecture logicielle en séparant les responsabilités dans **plusieurs projets distincts**.

---

### Objectifs pédagogiques

- Appliquer le patron d'architecture **MVVM** (Model-View-ViewModel)
- Implémenter le **patron Repository** pour abstraire l'accès aux données
- Utiliser les **attributs de validation** (`[Required]`, `[MaxLength]`, `[Key]`) plutôt que la Fluent API
- Utiliser **CommunityToolkit.Mvvm** (`[ObservableProperty]`, `[RelayCommand]`, `ObservableObject`)

---

### Structure de la solution

La solution doit être divisée en **4 projets** :

| Projet | Type | Rôle |
|---|---|---|
| `UserApp.Domain` | `net8.0` | Entités et interfaces |
| `UserApp.Infrastructure` | `net8.0` | Accès aux données (EF Core + SQLite) |
| `UserApp.Application` | `net8.0` | Services et logique métier |
| `UserApp.WPF` | `net8.0-windows` | Interface utilisateur WPF |

#### Dépendances entre projets

```
UserApp.WPF
  ├── UserApp.Application
  └── UserApp.Infrastructure
        └── UserApp.Domain
UserApp.Application
  └── UserApp.Domain
```

---

### Travail demandé

### 1. `UserApp.Domain`

Créez la classe `Utilisateur` avec les propriétés suivantes :

- `Id` (int) — clé primaire
- `Nom` (string) — obligatoire, max 100 caractères
- `Prenom` (string) — obligatoire, max 100 caractères
- `NomUtilisateur` (string) — obligatoire, max 50 caractères
- `DateCreation` (DateTime) — valeur par défaut : `DateTime.Now`

> Utilisez les attributs `[Key]`, `[Required]` et `[MaxLength]` de `System.ComponentModel.DataAnnotations`.

Créez ensuite l'interface `IUtilisateurRepository` avec les méthodes :

```csharp
Task AjouterAsync(Utilisateur utilisateur);
Task<bool> NomUtilisateurExisteAsync(string nomUtilisateur);
```

---

### 2. `UserApp.Infrastructure`

- Créez un `AppDbContext` héritant de `DbContext` avec un `DbSet<Utilisateur>`
- Aucune Fluent API n'est nécessaire : toutes les contraintes sont portées par les attributs de l'entité
- Implémentez `UtilisateurRepository` qui implémente `IUtilisateurRepository`

**Packages NuGet requis :**
```
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Sqlite
```

---

### 3. `UserApp.Application`

Créez l'interface `IUtilisateurService` :

```csharp
/// <exception cref="InvalidOperationException">Si la validation échoue ou si le nom d'utilisateur est déjà pris.</exception>
Task AjouterUtilisateurAsync(string nom, string prenom, string nomUtilisateur);
```

Implémentez `UtilisateurService` qui :

- Valide que les champs ne sont pas vides
- Vérifie que `NomUtilisateur` fait au moins 3 caractères et ne contient pas d'espaces
- Vérifie l'unicité du `NomUtilisateur` via le repository
- Lance une **`InvalidOperationException`** avec un message explicite en cas d'erreur

---

### 4. `UserApp.WPF`

Créez un ViewModel `AjoutUtilisateurViewModel` qui :

- Hérite de **`ObservableObject`** (CommunityToolkit.Mvvm)
- Expose trois propriétés bindables avec **`[ObservableProperty]`** : `Nom`, `Prenom`, `NomUtilisateur`
- Expose une commande **`[RelayCommand]`** `AjouterUtilisateur` qui appelle le service et affiche un `MessageBox`

Créez la vue `MainWindow.xaml` avec :

- Un champ texte pour chaque propriété
- **Un seul bouton** « Ajouter l'utilisateur » lié à la commande
- Un résultat affiché via `MessageBox` (succès ou erreur)

Dans le constructeur de `MainWindow`, instanciez manuellement le `DbContext`, le `Repository`, le `Service` et le `ViewModel` dans l'ordre, puis assignez le ViewModel au `DataContext`.

**Package NuGet requis :**
```
CommunityToolkit.Mvvm
```

---

### Question de réflexion

La méthode `AjouterUtilisateurAsync` du service doit communiquer un succès ou une erreur à l'appelant. Trois approches sont possibles :

| Approche | Signature | Avantages | Inconvénients |
|---|---|---|---|
| **Exception** | `Task` | Simple, flux clair, pas de valeur de retour à inspecter | Les exceptions ne devraient pas gérer le flux normal |
| **Tuple** | `Task<(bool Succes, string Message)>` | Explicite, pas d'exception | Verbeux, pas orienté objet |
| **Classe Result** | `Task<Result>` | Extensible, expressif, pattern moderne | Plus de code à écrire |

**Laquelle choisiriez-vous et pourquoi ?** Discutez des compromis selon le contexte (application interne, API publique, taille de l'équipe, etc.).

---

### Critères d'évaluation

- [ ] Les 4 projets sont créés et les références entre eux sont correctes
- [ ] Les attributs `[Key]`, `[Required]`, `[MaxLength]` sont utilisés dans l'entité
- [ ] Le `DbContext` n'utilise pas de Fluent API, tout est géré par les attributs
- [ ] `IUtilisateurRepository` est défini dans `Domain` et implémenté dans `Infrastructure`
- [ ] La validation métier est dans `Application`, pas dans le ViewModel
- [ ] `ObservableObject`, `[ObservableProperty]` et `[RelayCommand]` sont utilisés
- [ ] Un seul bouton dans l'interface, le résultat est affiché en `MessageBox`
- [ ] Le `DbContext`, le `Repository`, le `Service` et le `ViewModel` sont instanciés manuellement dans le constructeur de `MainWindow`

---

<details>
<summary><strong>💡 Solution complète (cliquez pour afficher)</strong></summary>

## Solution

### `UserApp.Domain` — Entité et interface

**`Entities/Utilisateur.cs`**
```csharp
using System.ComponentModel.DataAnnotations;

namespace UserApp.Domain.Entities;

public class Utilisateur
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nom { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Prenom { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string NomUtilisateur { get; set; } = string.Empty;

    public DateTime DateCreation { get; set; } = DateTime.Now;
}
```

**`Interfaces/IUtilisateurRepository.cs`**
```csharp
using UserApp.Domain.Entities;

namespace UserApp.Domain.Interfaces;

public interface IUtilisateurRepository
{
    Task AjouterAsync(Utilisateur utilisateur);
    Task<bool> NomUtilisateurExisteAsync(string nomUtilisateur);
}
```

---

### `UserApp.Infrastructure` — Données

**`Data/AppDbContext.cs`**
```csharp
using Microsoft.EntityFrameworkCore;
using UserApp.Domain.Entities;

namespace UserApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Utilisateur> Utilisateurs { get; set; }

    public AppDbContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=utilisateurs.db");
    }
}
```

**`Repositories/UtilisateurRepository.cs`**
```csharp
using Microsoft.EntityFrameworkCore;
using UserApp.Domain.Entities;
using UserApp.Domain.Interfaces;
using UserApp.Infrastructure.Data;

namespace UserApp.Infrastructure.Repositories;

public class UtilisateurRepository : IUtilisateurRepository
{
    private readonly AppDbContext _context;

    public UtilisateurRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AjouterAsync(Utilisateur utilisateur)
    {
        await _context.Utilisateurs.AddAsync(utilisateur);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> NomUtilisateurExisteAsync(string nomUtilisateur)
    {
        return await _context.Utilisateurs
            .AnyAsync(u => u.NomUtilisateur == nomUtilisateur);
    }
}
```

---

### `UserApp.Application` — Service

**`Interfaces/IUtilisateurService.cs`**
```csharp
namespace UserApp.Application.Interfaces;

public interface IUtilisateurService
{
    /// <exception cref="InvalidOperationException">Si la validation échoue ou si le nom d'utilisateur est déjà pris.</exception>
    Task AjouterUtilisateurAsync(string nom, string prenom, string nomUtilisateur);
}
```

**`Services/UtilisateurService.cs`**
```csharp
using UserApp.Application.Interfaces;
using UserApp.Domain.Entities;
using UserApp.Domain.Interfaces;

namespace UserApp.Application.Services;

public class UtilisateurService : IUtilisateurService
{
    private readonly IUtilisateurRepository _repository;

    public UtilisateurService(IUtilisateurRepository repository)
    {
        _repository = repository;
    }

    /// <exception cref="InvalidOperationException">Si la validation échoue ou si le nom d'utilisateur est déjà pris.</exception>
    public async Task AjouterUtilisateurAsync(string nom, string prenom, string nomUtilisateur)
    {
        if (string.IsNullOrWhiteSpace(nom))
            throw new InvalidOperationException("Le nom est obligatoire.");

        if (string.IsNullOrWhiteSpace(prenom))
            throw new InvalidOperationException("Le prénom est obligatoire.");

        if (string.IsNullOrWhiteSpace(nomUtilisateur))
            throw new InvalidOperationException("Le nom d'utilisateur est obligatoire.");

        if (nomUtilisateur.Length < 3)
            throw new InvalidOperationException("Le nom d'utilisateur doit contenir au moins 3 caractères.");

        if (nomUtilisateur.Contains(' '))
            throw new InvalidOperationException("Le nom d'utilisateur ne peut pas contenir d'espaces.");

        if (await _repository.NomUtilisateurExisteAsync(nomUtilisateur))
            throw new InvalidOperationException($"Le nom d'utilisateur '{nomUtilisateur}' est déjà pris.");

        var utilisateur = new Utilisateur
        {
            Nom = nom.Trim(),
            Prenom = prenom.Trim(),
            NomUtilisateur = nomUtilisateur.Trim()
        };

        await _repository.AjouterAsync(utilisateur);
    }
}
```

---

### `UserApp.WPF` — Interface utilisateur

**`ViewModels/AjoutUtilisateurViewModel.cs`**
```csharp
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using UserApp.Application.Interfaces;

namespace UserApp.WPF.ViewModels;

public partial class AjoutUtilisateurViewModel : ObservableObject
{
    private readonly IUtilisateurService _service;

    [ObservableProperty]
    private string _nom = string.Empty;

    [ObservableProperty]
    private string _prenom = string.Empty;

    [ObservableProperty]
    private string _nomUtilisateur = string.Empty;

    public AjoutUtilisateurViewModel(IUtilisateurService service)
    {
        _service = service;
    }

    [RelayCommand]
    private async Task AjouterUtilisateurAsync()
    {
        try
        {
            await _service.AjouterUtilisateurAsync(Nom, Prenom, NomUtilisateur);

            MessageBox.Show(
                $"L'utilisateur '{NomUtilisateur}' a été créé avec succès.",
                "Succès",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            Nom = string.Empty;
            Prenom = string.Empty;
            NomUtilisateur = string.Empty;
        }
        catch (InvalidOperationException ex)
        {
            MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
```

**`Views/MainWindow.xaml`** *(extrait simplifié)*
```xml
<Window ...>
    <Border Background="White" Margin="28" CornerRadius="12"
            BorderBrush="#E2E8F0" BorderThickness="1" Padding="28">
        <StackPanel>
            <TextBlock Text="Nom *" FontWeight="SemiBold" Margin="0,0,0,6"/>
            <TextBox Text="{Binding Nom, UpdateSourceTrigger=PropertyChanged}"
                     Margin="0,0,0,14"/>

            <TextBlock Text="Prénom *" FontWeight="SemiBold" Margin="0,0,0,6"/>
            <TextBox Text="{Binding Prenom, UpdateSourceTrigger=PropertyChanged}"
                     Margin="0,0,0,14"/>

            <TextBlock Text="Nom d'utilisateur *" FontWeight="SemiBold" Margin="0,0,0,6"/>
            <TextBox Text="{Binding NomUtilisateur, UpdateSourceTrigger=PropertyChanged}"
                     Margin="0,0,0,28"/>

            <Button Content="Ajouter l'utilisateur"
                    Command="{Binding AjouterUtilisateurCommand}"/>
        </StackPanel>
    </Border>
</Window>
```

**`App.xaml.cs`**
```csharp
using System.Windows;
using UserApp.WPF.Views;

namespace UserApp.WPF;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        new MainWindow().Show();
    }
}
```

**`Views/MainWindow.xaml.cs`**
```csharp
using System.Windows;
using UserApp.Application.Services;
using UserApp.Infrastructure.Data;
using UserApp.Infrastructure.Repositories;
using UserApp.WPF.ViewModels;

namespace UserApp.WPF.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var context = new AppDbContext();
        context.Database.EnsureCreated();

        var repository = new UtilisateurRepository(context);
        var service = new UtilisateurService(repository);

        DataContext = new AjoutUtilisateurViewModel(service);
    }
}
```

</details>

## Mini-projet 6 : Application de blog avec architecture complète
**Niveau : ⭐⭐⭐⭐⭐ Expert**

### Objectifs pédagogiques
- Architecture N-tiers complète
- Tous les patrons intégrés
- API REST simulée
- Navigation multi-vues
- Authentification
- Gestion des erreurs avancée

### Spécifications

**Fonctionnalités :**
1. Authentification (login/logout)
2. Gestion d'articles (CRUD)
3. Commentaires
4. Catégories et tags
5. Recherche full-text
6. Draft/Publié
7. Rôles (Admin, Éditeur, Lecteur)
8. Historique des modifications
9. Upload d'images
10. Export PDF d'article

**Architecture complète :**
```
Presentation/
  - Views/
    - LoginView
    - ArticlesListView
    - ArticleDetailView
    - EditorView
  - ViewModels/
    - LoginViewModel
    - ArticlesViewModel
    - ArticleDetailViewModel
  
Business/
  - Services/
    - IAuthenticationService
    - IArticleService
    - ICommentaireService
  - Commands/
  - Validators/
  
Data/
  - Repositories/
    - IArticleRepository
    - IUtilisateurRepository
  - Models/
  
Infrastructure/
  - API/
    - ApiClient
  - Storage/
  - Logging/
```

### Je vous fournis un squelette de départ...

```csharp
// Models/Article.cs
public class Article : BaseViewModel
{
    public int Id { get; set; }
    
    private string titre;
    public string Titre
    {
        get => titre;
        set => SetProperty(ref titre, value);
    }
    
    private string contenu;
    public string Contenu
    {
        get => contenu;
        set => SetProperty(ref contenu, value);
    }
    
    private StatutArticle statut;
    public StatutArticle Statut
    {
        get => statut;
        set => SetProperty(ref statut, value);
    }
    
    public Utilisateur Auteur { get; set; }
    public DateTime DateCreation { get; set; }
    public DateTime? DatePublication { get; set; }
    public DateTime DateModification { get; set; }
    
    public ObservableCollection<Commentaire> Commentaires { get; set; }
    public ObservableCollection<Tag> Tags { get; set; }
    public Categorie Categorie { get; set; }
}

public enum StatutArticle
{
    Brouillon,
    EnRevision,
    Publie,
    Archive
}

// Services/IArticleService.cs
public interface IArticleService
{
    Task<List<Article>> ObtenirArticlesAsync(FiltreBlog filtre);
    Task<Article> ObtenirArticleAsync(int id);
    Task<Article> CreerArticleAsync(Article article);
    Task ModifierArticleAsync(Article article);
    Task SupprimerArticleAsync(int id);
    Task<bool> PublierArticleAsync(int id);
    Task<List<Article>> RechercherAsync(string termes);
}

// ViewModels/MainViewModel.cs
public class MainViewModel : BaseViewModel
{
    private readonly INavigationService navigationService;
    private readonly IAuthenticationService authService;
    
    private BaseViewModel vueActuelle;
    public BaseViewModel VueActuelle
    {
        get => vueActuelle;
        set => SetProperty(ref vueActuelle, value);
    }
    
    private Utilisateur utilisateurConnecte;
    public Utilisateur UtilisateurConnecte
    {
        get => utilisateurConnecte;
        set => SetProperty(ref utilisateurConnecte, value);
    }
    
    public ICommand NaviguerVersArticlesCommand { get; }
    public ICommand NaviguerVersNouvelArticleCommand { get; }
    public ICommand DeconnecterCommand { get; }
    
    public MainViewModel(INavigationService navigationService, 
                        IAuthenticationService authService)
    {
        this.navigationService = navigationService;
        this.authService = authService;
        
        NaviguerVersArticlesCommand = new RelayCommand(() => 
            navigationService.NaviguerVers<ArticlesViewModel>());
        
        DeconnecterCommand = new RelayCommand(Deconnecter);
        
        // Vérifier si l'utilisateur est déjà connecté
        UtilisateurConnecte = authService.ObtenirUtilisateurActuel();
        
        if (UtilisateurConnecte == null)
            navigationService.NaviguerVers<LoginViewModel>();
        else
            navigationService.NaviguerVers<ArticlesViewModel>();
    }
}

// Services/NavigationService.cs
public interface INavigationService
{
    void NaviguerVers<TViewModel>() where TViewModel : BaseViewModel;
    void NaviguerVers<TViewModel>(object parametre) where TViewModel : BaseViewModel;
    void NaviguerRetour();
}

public class NavigationService : INavigationService
{
    private readonly Func<Type, BaseViewModel> viewModelFactory;
    private readonly MainViewModel mainViewModel;
    private Stack<BaseViewModel> historique = new Stack<BaseViewModel>();
    
    public NavigationService(Func<Type, BaseViewModel> viewModelFactory, 
                            MainViewModel mainViewModel)
    {
        this.viewModelFactory = viewModelFactory;
        this.mainViewModel = mainViewModel;
    }
    
    public void NaviguerVers<TViewModel>() where TViewModel : BaseViewModel
    {
        var viewModel = viewModelFactory(typeof(TViewModel));
        
        if (mainViewModel.VueActuelle != null)
            historique.Push(mainViewModel.VueActuelle);
        
        mainViewModel.VueActuelle = viewModel;
    }
    
    public void NaviguerRetour()
    {
        if (historique.Count > 0)
            mainViewModel.VueActuelle = historique.Pop();
    }
}
```

### XAML avec navigation

```xml
<!-- MainWindow.xaml -->
<Window>
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>
        
        <!-- Barre de navigation -->
        <StackPanel Grid.Row="0" Orientation="Horizontal" Background="#2196F3">
            <Button Content="Articles" 
                    Command="{Binding NaviguerVersArticlesCommand}"/>
            <Button Content="Nouvel Article" 
                    Command="{Binding NaviguerVersNouvelArticleCommand}"/>
            <TextBlock Text="{Binding UtilisateurConnecte.Nom}" 
                       Foreground="White"
                       VerticalAlignment="Center"
                       Margin="20,0"/>
            <Button Content="Déconnexion" 
                    Command="{Binding DeconnecterCommand}"/>
        </StackPanel>
        
        <!-- Zone de contenu dynamique -->
        <ContentControl Grid.Row="1" Content="{Binding VueActuelle}">
            <ContentControl.Resources>
                <!-- Mapper les ViewModels aux Views -->
                <DataTemplate DataType="{x:Type vm:ArticlesViewModel}">
                    <views:ArticlesView />
                </DataTemplate>
                <DataTemplate DataType="{x:Type vm:ArticleDetailViewModel}">
                    <views:ArticleDetailView />
                </DataTemplate>
                <DataTemplate DataType="{x:Type vm:LoginViewModel}">
                    <views:LoginView />
                </DataTemplate>
            </ContentControl.Resources>
        </ContentControl>
    </Grid>
</Window>
```

### Configuration de l'injection de dépendances

```csharp
// App.xaml.cs
public partial class App : Application
{
    private ServiceProvider serviceProvider;
    
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        var services = new ServiceCollection();
        
        // Repositories
        services.AddSingleton<IArticleRepository, ArticleRepository>();
        services.AddSingleton<IUtilisateurRepository, UtilisateurRepository>();
        
        // Services
        services.AddSingleton<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IArticleService, ArticleService>();
        services.AddTransient<IExportService, ExportService>();
        
        // ViewModels
        services.AddSingleton<MainViewModel>();
        services.AddTransient<ArticlesViewModel>();
        services.AddTransient<ArticleDetailViewModel>();
        services.AddTransient<LoginViewModel>();
        
        // Navigation
        services.AddSingleton<INavigationService>(provider =>
        {
            var mainVM = provider.GetRequiredService<MainViewModel>();
            return new NavigationService(
                type => (BaseViewModel)provider.GetRequiredService(type),
                mainVM
            );
        });
        
        serviceProvider = services.BuildServiceProvider();
        
        // Lancer la fenêtre principale
        var mainWindow = new MainWindow
        {
            DataContext = serviceProvider.GetRequiredService<MainViewModel>()
        };
        mainWindow.Show();
    }
}
```

### Critères de réussite
- [ ] Architecture N-tiers complète
- [ ] Navigation fluide entre vues
- [ ] Authentification fonctionnelle
- [ ] CRUD complet avec validation
- [ ] Gestion des rôles
- [ ] Export PDF
- [ ] Gestion des erreurs
- [ ] Logging
- [ ] Tests unitaires complets
- [ ] Tests d'intégration

---

## Conclusion des mini-projets

Ces 5 mini-projets couvrent progressivement tous les concepts enseignés :

1. **Calculatrice** : Introduction aux commandes et historique
2. **Tâches** : MVVM complet avec sauvegarde
3. **Chat** : Observateur en temps réel
4. **Inventaire** : Architecture complète avec injection
5. **Blog** : Application professionnelle complète

Chaque projet construit sur les acquis du précédent et introduit de nouveaux concepts.