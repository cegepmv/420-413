---
title: "Rubrique  - Semaine 2"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 22
---

## Noms Significatifs et Fonctions Claires

---

## 📋 Objectifs de cette semaine

Apprendre à :
1. Identifier et corriger les **noms non significatifs**
2. Reconnaître et résoudre les **fonctions trop longues**
3. Éliminer les **commentaires inutiles** en écrivant du code auto-explicatif

---

## 💡 Citation de la semaine

> "Le code est lu beaucoup plus souvent qu'il n'est écrit. Facilitez la lecture, pas l'écriture."
> 
> — Robert C. Martin (Uncle Bob), Clean Code

---

## 🔴 Code Smell #1 : Noms Non Significatifs

### Qu'est-ce qu'un nom non significatif ?

Un **nom non significatif** est un nom de variable, méthode ou classe qui ne révèle pas clairement son intention ou son rôle. Il force le lecteur à deviner ou à chercher dans le contexte pour comprendre.

### ❌ Exemple de Code Problématique

```csharp
public class Program
{
    static void Main()
    {
        // Que représentent ces variables ???
        var d = 30;
        var temp = new List<int[]>();
        
        for (int i = 0; i < 100; i++)
        {
            var x = new int[3];
            x[0] = i;
            x[1] = i * 2;
            x[2] = i % d;
            
            if (x[2] == 0)
            {
                temp.Add(x);
            }
        }
        
        // Afficher les résultats
        foreach (var t in temp)
        {
            Console.WriteLine($"{t[0]} - {t[1]} - {t[2]}");
        }
    }
}
```

### 🤔 Problèmes identifiés

1. **`d`** : Que signifie "d" ? Jour ? Distance ? Diviseur ?
2. **`temp`** : Temporaire de quoi ? Quelle est sa vraie signification ?
3. **`x`** : Qu'est-ce que ce tableau représente ?
4. **`t`** : Même problème dans la boucle
5. **`i`** : Dans ce contexte, que représente-t-il vraiment ?
6. **Indices magiques** : Que signifient `[0]`, `[1]`, `[2]` ?

### ✅ Code Corrigé (Version Clean Code)

```csharp
public class Program
{
    static void Main()
    {
        // VERSION CLEAN CODE
        const int NombreJoursDansUnMois = 30;
        List<Commande> commandesDivisiblesParTrente = new List<Commande>();
        
        for (int numeroCommande = 0; numeroCommande < 100; numeroCommande++)
        {
            var commande = new Commande
            {
                Numero = numeroCommande,
                MontantDouble = numeroCommande * 2,
                ResteModulo = numeroCommande % NombreJoursDansUnMois
            };
            
            if (commande.EstDivisibleParTrente())
            {
                commandesDivisiblesParTrente.Add(commande);
            }
        }
        
        AfficherCommandes(commandesDivisiblesParTrente);
    }
    
    private static void AfficherCommandes(List<Commande> commandes)
    {
        foreach (var commande in commandes)
        {
            Console.WriteLine(commande.ObtenirDescription());
        }
    }
}

public class Commande
{
    public int Numero { get; set; }
    public int MontantDouble { get; set; }
    public int ResteModulo { get; set; }
    
    public bool EstDivisibleParTrente()
    {
        return ResteModulo == 0;
    }
    
    public string ObtenirDescription()
    {
        return $"Commande #{Numero} - Montant: {MontantDouble} - Reste: {ResteModulo}";
    }
}
```

### 📊 Comparaison Avant/Après

| Avant (😞 Mauvais) | Après (✅ Bon) | Amélioration |
|-------------------|---------------|--------------|
| `d` | `nombreDeJoursDansUnMois` | Intention claire |
| `temp` | `commandesDivisiblesParTrente` | But explicite |
| `x` | `commande` (objet Commande) | Type et rôle clairs |
| `x[0]`, `x[1]`, `x[2]` | `commande.Numero`, `commande.MontantDouble` | Propriétés nommées |
| `i` | `numeroCommande` | Contexte précis |
| `t` | `commande` | Nom significatif |

### 🔍 Exercice Pratique #1

**Corrigez ce code :**

```csharp
public void Process(int a, int b)
{
    var r = 0;
    for (int i = a; i <= b; i++)
    {
        if (i % 2 == 0)
        {
            r += i;
        }
    }
    Console.WriteLine(r);
}
```

**Solution :**

```csharp
public void AfficherSommeNombresPairs(int nombreDebut, int nombreFin)
{
    int sommePairs = 0;
    
    for (int nombre = nombreDebut; nombre <= nombreFin; nombre++)
    {
        if (NombreEstPair(nombre))
        {
            sommePairs += nombre;
        }
    }
    
    Console.WriteLine($"Somme des nombres pairs : {sommePairs}");
}

private bool NombreEstPair(int nombre)
{
    return nombre % 2 == 0;
}
```

---

## 🔴 Code Smell #2 : Fonctions Trop Longues

### Qu'est-ce qu'une fonction trop longue ?

Une fonction qui fait **trop de choses**, contient **trop de lignes** de code, ou a **trop de niveaux d'indentation**. Règle générale : une fonction devrait tenir sur un écran sans défilement.

> **Règle d'or** : Une fonction = Une responsabilité

### ❌ Exemple de Code Problématique

```csharp
public class GestionnaireCommandes
{
    public void TraiterCommande(int idCommande)
    {
        // Cette fonction fait BEAUCOUP TROP de choses !
        
        // 1. Récupération des données
        var commande = new Commande();
        using (var connection = new SqlConnection("connectionString"))
        {
            connection.Open();
            var cmd = new SqlCommand($"SELECT * FROM Commandes WHERE Id = {idCommande}", connection);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                commande.Id = (int)reader["Id"];
                commande.ClientId = (int)reader["ClientId"];
                commande.MontantTotal = (decimal)reader["MontantTotal"];
                commande.DateCommande = (DateTime)reader["DateCommande"];
            }
        }
        
        // 2. Validation
        if (commande.MontantTotal <= 0)
        {
            Console.WriteLine("Erreur: Montant invalide");
            return;
        }
        
        if (commande.DateCommande > DateTime.Now)
        {
            Console.WriteLine("Erreur: Date future");
            return;
        }
        
        // 3. Calcul des taxes
        decimal tps = commande.MontantTotal * 0.05m;
        decimal tvq = commande.MontantTotal * 0.09975m;
        decimal total = commande.MontantTotal + tps + tvq;
        
        // 4. Vérification du client
        var client = new Client();
        using (var connection = new SqlConnection("connectionString"))
        {
            connection.Open();
            var cmd = new SqlCommand($"SELECT * FROM Clients WHERE Id = {commande.ClientId}", connection);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                client.Nom = (string)reader["Nom"];
                client.Email = (string)reader["Email"];
                client.Actif = (bool)reader["Actif"];
            }
        }
        
        if (!client.Actif)
        {
            Console.WriteLine("Erreur: Client inactif");
            return;
        }
        
        // 5. Application de rabais
        decimal rabais = 0;
        if (commande.MontantTotal > 1000)
        {
            rabais = total * 0.10m;
        }
        else if (commande.MontantTotal > 500)
        {
            rabais = total * 0.05m;
        }
        
        total -= rabais;
        
        // 6. Enregistrement de la facture
        using (var connection = new SqlConnection("connectionString"))
        {
            connection.Open();
            var cmd = new SqlCommand(
                $"INSERT INTO Factures (CommandeId, MontantHT, TPS, TVQ, Rabais, Total) " +
                $"VALUES ({commande.Id}, {commande.MontantTotal}, {tps}, {tvq}, {rabais}, {total})", 
                connection);
            cmd.ExecuteNonQuery();
        }
        
        // 7. Envoi d'email
        var smtp = new SmtpClient("smtp.server.com");
        var message = new MailMessage();
        message.To.Add(client.Email);
        message.Subject = "Facture";
        message.Body = $"Votre facture de {total:C} est prête";
        smtp.Send(message);
        
        // 8. Logging
        Console.WriteLine($"Commande {idCommande} traitée avec succès");
        File.AppendAllText("log.txt", $"{DateTime.Now}: Commande {idCommande} OK\n");
    }
}
```

### 🤔 Problèmes identifiés

1. **91 lignes** dans une seule méthode !
2. **8 responsabilités différentes** dans une fonction
3. **Violation du principe SRP** (Single Responsibility Principle)
4. **Difficile à tester** : comment tester juste une partie ?
5. **Difficile à maintenir** : modifier une partie risque de casser le reste
6. **Code dupliqué** : connexion DB répétée 3 fois
7. **Mélange de niveaux d'abstraction** : détails DB + logique métier

### ✅ Code Corrigé (Version Clean Code)

```csharp
public class GestionnaireCommandes
{
    private readonly ICommandeRepository _commandeRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IFactureRepository _factureRepository;
    private readonly ICalculateurTaxes _calculateurTaxes;
    private readonly ICalculateurRabais _calculateurRabais;
    private readonly IServiceEmail _serviceEmail;
    private readonly ILogger _logger;
    
    public GestionnaireCommandes(
        ICommandeRepository commandeRepository,
        IClientRepository clientRepository,
        IFactureRepository factureRepository,
        ICalculateurTaxes calculateurTaxes,
        ICalculateurRabais calculateurRabais,
        IServiceEmail serviceEmail,
        ILogger logger)
    {
        _commandeRepository = commandeRepository;
        _clientRepository = clientRepository;
        _factureRepository = factureRepository;
        _calculateurTaxes = calculateurTaxes;
        _calculateurRabais = calculateurRabais;
        _serviceEmail = serviceEmail;
        _logger = logger;
    }
    
    // ==========================================
    // MÉTHODE PRINCIPALE - Claire et concise
    // ==========================================
    public void TraiterCommande(int idCommande)
    {
        var commande = RecupererCommande(idCommande);
        ValiderCommande(commande);
        
        var client = RecupererClient(commande.ClientId);
        ValiderClient(client);
        
        var facture = CreerFacture(commande);
        EnregistrerFacture(facture);
        
        EnvoyerEmailConfirmation(client, facture);
        LoggerTraitement(idCommande);
    }
    
    // ==========================================
    // MÉTHODES PRIVÉES - Une responsabilité chacune
    // ==========================================
    
    private Commande RecupererCommande(int idCommande)
    {
        var commande = _commandeRepository.ObtenirParId(idCommande);
        
        if (commande == null)
        {
            throw new CommandeIntrouvableException($"Commande {idCommande} introuvable");
        }
        
        return commande;
    }
    
    private void ValiderCommande(Commande commande)
    {
        if (commande.MontantTotal <= 0)
        {
            throw new CommandeInvalideException("Le montant doit être positif");
        }
        
        if (commande.DateCommande > DateTime.Now)
        {
            throw new CommandeInvalideException("La date ne peut être future");
        }
    }
    
    private Client RecupererClient(int clientId)
    {
        var client = _clientRepository.ObtenirParId(clientId);
        
        if (client == null)
        {
            throw new ClientIntrouvableException($"Client {clientId} introuvable");
        }
        
        return client;
    }
    
    private void ValiderClient(Client client)
    {
        if (!client.Actif)
        {
            throw new ClientInactifException($"Le client {client.Nom} est inactif");
        }
    }
    
    private Facture CreerFacture(Commande commande)
    {
        var montantHT = commande.MontantTotal;
        var taxes = _calculateurTaxes.CalculerTaxes(montantHT);
        var montantAvecTaxes = montantHT + taxes.TPS + taxes.TVQ;
        var rabais = _calculateurRabais.CalculerRabais(montantAvecTaxes);
        var montantFinal = montantAvecTaxes - rabais;
        
        return new Facture
        {
            CommandeId = commande.Id,
            MontantHorsTaxes = montantHT,
            TPS = taxes.TPS,
            TVQ = taxes.TVQ,
            Rabais = rabais,
            MontantTotal = montantFinal
        };
    }
    
    private void EnregistrerFacture(Facture facture)
    {
        _factureRepository.Enregistrer(facture);
    }
    
    private void EnvoyerEmailConfirmation(Client client, Facture facture)
    {
        var contenuEmail = new EmailFacture
        {
            Destinataire = client.Email,
            NomClient = client.Nom,
            MontantTotal = facture.MontantTotal
        };
        
        _serviceEmail.EnvoyerEmailFacture(contenuEmail);
    }
    
    private void LoggerTraitement(int idCommande)
    {
        _logger.LogInformation($"Commande {idCommande} traitée avec succès");
    }
}

// ==========================================
// CLASSES DE SUPPORT (extraites)
// ==========================================

public class CalculateurTaxes : ICalculateurTaxes
{
    private const decimal TauxTPS = 0.05m;      // Taxe fédérale (5%)
    private const decimal TauxTVQ = 0.09975m;   // Taxe provinciale Québec (9.975%)
    
    public TaxesCalculees CalculerTaxes(decimal montantHorsTaxes)
    {
        return new TaxesCalculees
        {
            TPS = montantHorsTaxes * TauxTPS,
            TVQ = montantHorsTaxes * TauxTVQ
        };
    }
}

public class CalculateurRabais : ICalculateurRabais
{
    private const decimal SeuilRabaisEleve = 1000m;
    private const decimal SeuilRabaisMoyen = 500m;
    private const decimal TauxRabaisEleve = 0.10m;
    private const decimal TauxRabaisMoyen = 0.05m;
    
    public decimal CalculerRabais(decimal montant)
    {
        if (montant > SeuilRabaisEleve)
        {
            return montant * TauxRabaisEleve;
        }
        
        if (montant > SeuilRabaisMoyen)
        {
            return montant * TauxRabaisMoyen;
        }
        
        return 0;
    }
}
```

### 📊 Comparaison Avant/Après

| Aspect | Avant (😞) | Après (✅) |
|--------|-----------|-----------|
| **Lignes par méthode** | 91 lignes | 5-15 lignes max |
| **Responsabilités** | 8 dans une méthode | 1 par méthode |
| **Testabilité** | Très difficile | Facile (chaque partie) |
| **Lisibilité** | Nécessite défilement | Chaque méthode visible d'un coup |
| **Maintenance** | Risqué | Sûr et ciblé |
| **Réutilisabilité** | Aucune | Chaque méthode réutilisable |

### 🎯 Règles pour les Fonctions Propres

#### ✅ Principes à suivre :

1. **Une fonction = Une responsabilité**
```csharp
// ✅ BON - Fait une seule chose
public decimal CalculerPrixAvecTaxes(decimal prixHT)
{
    return prixHT * 1.14975m; // TPS + TVQ du Québec
}

// ❌ MAUVAIS - Fait plusieurs choses
public decimal CalculerEtEnregistrerPrixAvecTaxes(decimal prixHT)
{
    var prixTTC = prixHT * 1.14975m;
    // Enregistrement en DB
    // Envoi d'email
    // Logging
    return prixTTC;
}
```

2. **Petite taille** : 5-20 lignes idéalement
```csharp
// ✅ BON - Court et clair
public bool UtilisateurEstEligibleAuRabais(Utilisateur user)
{
    return user.EstMembre 
        && user.AncienneteEnMois >= 6 
        && user.MontantAchatsTotal > 1000;
}
```

3. **Un seul niveau d'abstraction**
```csharp
// ✅ BON - Même niveau d'abstraction
public void TraiterInscription(Utilisateur user)
{
    ValiderDonneesUtilisateur(user);
    EnregistrerUtilisateur(user);
    EnvoyerEmailBienvenue(user);
}

// ❌ MAUVAIS - Niveaux d'abstraction mélangés
public void TraiterInscription(Utilisateur user)
{
    // Haut niveau
    ValiderDonneesUtilisateur(user);
    
    // Bas niveau (détails d'implémentation)
    using (var connection = new SqlConnection("..."))
    {
        connection.Open();
        var cmd = new SqlCommand("INSERT...", connection);
        cmd.ExecuteNonQuery();
    }
    
    // Haut niveau
    EnvoyerEmailBienvenue(user);
}
```

4. **Peu de paramètres** : 0-3 idéalement
```csharp
// ✅ BON - 2 paramètres
public decimal CalculerRemise(decimal montant, TypeClient typeClient)
{
    // ...
}

// ⚠️ ACCEPTABLE - 3 paramètres
public void CreerCommande(int clientId, List<Produit> produits, AdresseLivraison adresse)
{
    // ...
}

// ❌ MAUVAIS - Trop de paramètres
public void CreerCommande(int clientId, string nom, string prenom, 
    string email, string telephone, string rue, string ville, 
    string codePostal, List<Produit> produits)
{
    // Utilisez plutôt un objet
}

// ✅ MIEUX - Objet de paramètres
public void CreerCommande(InformationsCommande infos)
{
    // ...
}
```

5. **Pas d'effets de bord**
```csharp
// ❌ MAUVAIS - Effet de bord caché
private int compteur = 0;  // Variable de classe

public bool ValiderUtilisateur(Utilisateur user)
{
    compteur++;  // ⚠️ Effet de bord : modifie l'état
    return user.Email != null && user.Age >= 18;
}

// ✅ BON - Fonction pure
public bool ValiderUtilisateur(Utilisateur user)
{
    return user.Email != null && user.Age >= 18;
}
```

### 🔍 Exercice Pratique #2

**Décomposez cette fonction :**

```csharp
public void ProcessOrder(int orderId)
{
    var order = db.Orders.Find(orderId);
    if (order == null) return;
    
    var customer = db.Customers.Find(order.CustomerId);
    if (customer == null) return;
    
    var total = 0m;
    foreach (var item in order.Items)
    {
        total += item.Price * item.Quantity;
    }
    
    var tax = total * 0.15m;
    var finalTotal = total + tax;
    
    if (finalTotal > 1000)
    {
        finalTotal *= 0.9m;
    }
    
    order.Total = finalTotal;
    db.SaveChanges();
    
    SendEmail(customer.Email, $"Your order total: {finalTotal:C}");
}
```

**Solution :**

```csharp
public void TraiterCommande(int idCommande)
{
    var commande = RecupererCommande(idCommande);
    var client = RecupererClient(commande.ClientId);
    
    var montantTotal = CalculerMontantTotal(commande);
    
    EnregistrerMontantCommande(commande, montantTotal);
    EnvoyerEmailConfirmation(client, montantTotal);
}

private Commande RecupererCommande(int idCommande)
{
    var commande = db.Orders.Find(idCommande);
    if (commande == null)
    {
        throw new CommandeIntrouvableException($"Commande {idCommande} introuvable");
    }
    return commande;
}

private Client RecupererClient(int clientId)
{
    var client = db.Customers.Find(clientId);
    if (client == null)
    {
        throw new ClientIntrouvableException($"Client {clientId} introuvable");
    }
    return client;
}

private decimal CalculerMontantTotal(Commande commande)
{
    var sousTotal = CalculerSousTotal(commande.Items);
    var montantAvecTaxes = AjouterTaxes(sousTotal);
    var montantFinal = AppliquerRabaisSiEligible(montantAvecTaxes);
    
    return montantFinal;
}

private decimal CalculerSousTotal(List<Item> items)
{
    return items.Sum(item => item.Price * item.Quantity);
}

private decimal AjouterTaxes(decimal montant)
{
    const decimal TauxTaxe = 0.15m;
    return montant * (1 + TauxTaxe);
}

private decimal AppliquerRabaisSiEligible(decimal montant)
{
    const decimal SeuilRabais = 1000m;
    const decimal TauxRabais = 0.10m;
    
    if (montant > SeuilRabais)
    {
        return montant * (1 - TauxRabais);
    }
    
    return montant;
}

private void EnregistrerMontantCommande(Commande commande, decimal montant)
{
    commande.Total = montant;
    db.SaveChanges();
}

private void EnvoyerEmailConfirmation(Client client, decimal montant)
{
    var message = $"Votre commande : {montant:C}";
    SendEmail(client.Email, message);
}
```

---

## 🔴 Code Smell #3 : Commentaires Inutiles ou Mensongers

### Qu'est-ce qu'un commentaire inutile ?

Un commentaire qui :
- Décrit ce que le code fait déjà clairement
- Est obsolète ou faux
- Compense un code mal écrit
- Pourrait être remplacé par un bon nom de variable/méthode

> **Règle d'or** : Le meilleur commentaire est celui que vous n'avez pas besoin d'écrire

### ❌ Exemple de Code Problématique

```csharp
public class GestionnaireUtilisateurs
{
    // Cette méthode calcule l'âge de l'utilisateur
    public int CalculerAge(DateTime dateNaissance)
    {
        // Obtenir la date d'aujourd'hui
        DateTime aujourd'hui = DateTime.Today;
        
        // Calculer l'âge
        int age = aujourd'hui.Year - dateNaissance.Year;
        
        // Vérifier si l'anniversaire est passé cette année
        if (dateNaissance.Date > aujourd'hui.AddYears(-age))
        {
            age--; // Soustraire 1 si anniversaire pas encore passé
        }
        
        // Retourner l'âge
        return age;
    }
    
    // TODO: Corriger ce bug (écrit il y a 2 ans...)
    // HACK: Solution temporaire en attendant le refactoring
    // NOTE: Ne fonctionne pas pour les clients VIP
    public decimal CalculerPrix(Produit p, Client c)
    {
        decimal prix = p.PrixBase;
        
        // Appliquer le rabais
        if (c.Type == "VIP")
        {
            prix = prix * 0.8m; // 20% de rabais
        }
        else if (c.Type == "Premium")
        {
            prix = prix * 0.9m; // 10% de rabais
        }
        // Sinon, pas de rabais
        
        return prix;
    }
    
    // ==========================================
    // SECTION: Validation
    // ==========================================
    
    // Valide l'email
    public bool ValiderEmail(string email)
    {
        // Vérifier si email est null
        if (email == null)
        {
            return false; // Retourner faux
        }
        
        // Vérifier si email contient @
        if (!email.Contains("@"))
        {
            return false; // Retourner faux
        }
        
        // L'email est valide
        return true; // Retourner vrai
    }
    
    /**
     * Cette méthode envoie un email à l'utilisateur
     * Paramètres:
     *   - destinataire: l'adresse email du destinataire
     *   - sujet: le sujet de l'email
     *   - corps: le corps du message
     * Retour:
     *   - bool: true si envoyé, false sinon
     * Auteur: Jean Dupont
     * Date: 15/01/2022
     * Modifié par: Marie Martin
     * Date: 03/05/2023
     */
    public bool EnvoyerEmail(string destinataire, string sujet, string corps)
    {
        // Code d'envoi d'email
        return true;
    }
    
    // Méthode obsolète - NE PLUS UTILISER
    // Utiliser NouvelleMethode() à la place
    public void AncienneMethode()
    {
        // Cette méthode ne devrait plus être utilisée
        // mais on la garde pour compatibilité
    }
}
```

### 🤔 Problèmes identifiés

1. **Commentaires redondants** : "Cette méthode calcule l'âge" → Le nom le dit déjà !
2. **Commentaires évidents** : "Obtenir la date d'aujourd'hui" → Le code est clair
3. **TODO/HACK vieux** : Jamais résolus, induisent en erreur
4. **Commentaires qui mentent** : "Ne fonctionne pas pour VIP" mais le code le gère !
5. **Commentaires de séparation** : Headers inutiles
6. **Documentation excessive** : Javadoc inutile pour méthode simple
7. **Code commenté** : Méthode obsolète devrait être supprimée
8. **Explications du "comment"** : Au lieu du "pourquoi"

### ✅ Code Corrigé (Version Clean Code)

```csharp
public class GestionnaireUtilisateurs
{
    // ==========================================
    // MÉTHODE 1 : Calcul d'âge
    // Aucun commentaire nécessaire - le code parle de lui-même
    // ==========================================
    public int CalculerAge(DateTime dateNaissance)
    {
        var aujourdHui = DateTime.Today;
        var age = aujourdHui.Year - dateNaissance.Year;
        
        if (AnniversairePasEncorePasseCetteAnnee(dateNaissance, aujourdHui, age))
        {
            age--;
        }
        
        return age;
    }
    
    private bool AnniversairePasEncorePasseCetteAnnee(DateTime dateNaissance, DateTime aujourdHui, int age)
    {
        return dateNaissance.Date > aujourdHui.AddYears(-age);
    }
    
    // ==========================================
    // MÉTHODE 2 : Calcul de prix
    // Code auto-documenté avec extraction de méthodes
    // ==========================================
    public decimal CalculerPrix(Produit produit, Client client)
    {
        var prixBase = produit.PrixBase;
        var tauxRabais = ObtenirTauxRabais(client.Type);
        
        return AppliquerRabais(prixBase, tauxRabais);
    }
    
    private decimal ObtenirTauxRabais(TypeClient typeClient)
    {
        return typeClient switch
        {
            TypeClient.VIP => 0.20m,      // 20% pour VIP
            TypeClient.Premium => 0.10m,  // 10% pour Premium
            _ => 0m                        // Pas de rabais par défaut
        };
    }
    
    private decimal AppliquerRabais(decimal prixBase, decimal tauxRabais)
    {
        return prixBase * (1 - tauxRabais);
    }
    
    // ==========================================
    // MÉTHODE 3 : Validation email
    // Nom de méthode explicite + extraction de conditions
    // ==========================================
    public bool EmailEstValide(string email)
    {
        return email != null && email.Contains("@");
    }
    
    // OU version plus robuste
    public bool EmailEstValide_Version2(string email)
    {
        if (EstVideOuNull(email))
        {
            return false;
        }
        
        return ContientArobase(email) && ContientPoint(email);
    }
    
    private bool EstVideOuNull(string texte) => string.IsNullOrWhiteSpace(texte);
    private bool ContientArobase(string email) => email.Contains("@");
    private bool ContientPoint(string email) => email.Contains(".");
    
    // ==========================================
    // MÉTHODE 4 : Envoi email
    // Signature claire = documentation suffisante
    // ==========================================
    public bool EnvoyerEmail(string destinataire, string sujet, string corps)
    {
        // Code d'envoi
        return true;
    }
    
    // ==========================================
    // MÉTHODE OBSOLÈTE : Marquée avec attribut, pas commentaire
    // ==========================================
    [Obsolete("Utilisez NouvelleMethode() à la place", error: true)]
    public void AncienneMethode()
    {
        throw new NotSupportedException("Cette méthode est obsolète");
    }
    
    public void NouvelleMethode()
    {
        // Nouvelle implémentation
    }
}
```

### 🎯 Quand les Commentaires SONT Utiles

Certains commentaires sont précieux et doivent être gardés :

#### ✅ 1. Explication du "POURQUOI" (pas du "QUOI")

```csharp
// ✅ BON - Explique une décision non-évidente
public decimal CalculerFraisLivraison(decimal montantCommande)
{
    // Nous offrons la livraison gratuite au-dessus de 50$ pour encourager
    // les achats plus importants et améliorer la satisfaction client
    // (décision marketing du 15/01/2024)
    if (montantCommande >= 50)
    {
        return 0;
    }
    
    return 5.99m;
}

// ❌ MAUVAIS - Décrit ce que le code fait déjà
public decimal CalculerFraisLivraison(decimal montantCommande)
{
    // Vérifier si le montant est supérieur ou égal à 50
    if (montantCommande >= 50)
    {
        return 0; // Retourner 0
    }
    
    return 5.99m; // Sinon retourner 5.99
}
```

#### ✅ 2. Avertissements sur les Conséquences

```csharp
// ✅ BON - Avertit d'un comportement important
public void SupprimerToutesLesDonnees()
{
    // ATTENTION: Cette opération est IRRÉVERSIBLE et supprime
    // toutes les données de production. À utiliser uniquement
    // dans le cadre de la procédure de réinitialisation annuelle.
    database.DeleteAll();
}
```

#### ✅ 3. TODO Légitimes (avec date et contexte)

```csharp
// ✅ BON - TODO avec contexte et échéance
public class ServicePaiement
{
    // TODO (2024-02-15): Migrer vers la nouvelle API Stripe v3
    // Dépendance: Attente de la mise à jour du SDK
    // Responsable: équipe-backend@example.com
    public bool ProcesserPaiement(CarteBancaire carte)
    {
        // Utilisation de l'ancienne API (v2)
        return StripeV2.Charge(carte);
    }
}

// ❌ MAUVAIS - TODO vague et sans contexte
public bool ProcesserPaiement(CarteBancaire carte)
{
    // TODO: améliorer
    return StripeV2.Charge(carte);
}
```

#### ✅ 4. Explication d'un Algorithme Complexe

```csharp
// ✅ BON - Explique un algorithme non-trivial
public int CalculerJourPaques(int annee)
{
    // Algorithme de Gauss pour calculer la date de Pâques
    // Formule mathématique complexe basée sur les cycles lunaires
    // Référence: https://fr.wikipedia.org/wiki/Calcul_de_la_date_de_Pâques
    
    int a = annee % 19;
    int b = annee / 100;
    int c = annee % 100;
    // ... suite de l'algorithme complexe
}
```

#### ✅ 5. Exigences Légales ou Métier

```csharp
// ✅ BON - Référence légale importante
public decimal CalculerTaxes(decimal montant)
{
    // Conformément à la loi C-45 (Québec), les taux de taxes sont:
    // TPS fédérale: 5% (depuis 01/01/2008)
    // TVQ provinciale: 9.975% (depuis 01/01/2013)
    // Ces taux peuvent changer - voir: revenuquebec.ca
    
    const decimal TauxTPS = 0.05m;
    const decimal TauxTVQ = 0.09975m;
    
    return montant * (1 + TauxTPS + TauxTVQ);
}
```

### 📊 Guide de Décision : Commentaire ou Refactoring ?

```
Vous voulez écrire un commentaire ?
│
├─ Décrit-il ce que le code fait ?
│  └─ OUI → ❌ Ne commentez pas, améliorez le code
│      └─ Renommez variables/méthodes pour être auto-documenté
│
├─ Explique-t-il POURQUOI (décision métier, contrainte) ?
│  └─ OUI → ✅ Commentaire utile, gardez-le
│
├─ Compense-t-il un code mal écrit ?
│  └─ OUI → ❌ Ne commentez pas, refactorez
│
├─ Contient-il un TODO/FIXME ?
│  ├─ Avec date, contexte et responsable → ✅ OK
│  └─ Vague ou ancien → ❌ Supprimez ou corrigez
│
└─ Explique-t-il un algorithme complexe ?
   └─ OUI → ✅ Commentaire utile avec référence
```

### 🔍 Exercice Pratique #3

**Nettoyez ce code :**

```csharp
// Classe pour gérer les utilisateurs
public class UserManager
{
    // Constructeur
    public UserManager()
    {
        // Initialiser
    }
    
    // Cette méthode vérifie si l'utilisateur peut se connecter
    public bool CanLogin(User u)
    {
        // Vérifier si l'utilisateur n'est pas null
        if (u == null)
        {
            return false; // Retourner faux
        }
        
        // Vérifier si l'utilisateur est actif
        if (!u.IsActive)
        {
            return false; // Retourner faux
        }
        
        // Vérifier le mot de passe
        // TODO: ajouter vérification 2FA
        if (u.Password == null || u.Password.Length < 8)
        {
            return false; // Mot de passe trop court
        }
        
        // Tout est OK
        return true; // Retourner vrai
    }
    
    // HACK: Temporaire jusqu'à refonte
    // Ne fonctionne pas pour admin
    public void DeleteUser(int id)
    {
        // Code de suppression
    }
}
```

**Solution :**

```csharp
public class GestionnaireUtilisateurs
{
    private const int LongueurMinimaleMotDePasse = 8;
    
    public bool UtilisateurPeutSeConnecter(Utilisateur utilisateur)
    {
        return UtilisateurExiste(utilisateur)
            && UtilisateurEstActif(utilisateur)
            && MotDePasseEstValide(utilisateur.MotDePasse);
    }
    
    private bool UtilisateurExiste(Utilisateur utilisateur)
    {
        return utilisateur != null;
    }
    
    private bool UtilisateurEstActif(Utilisateur utilisateur)
    {
        return utilisateur.EstActif;
    }
    
    private bool MotDePasseEstValide(string motDePasse)
    {
        return !string.IsNullOrEmpty(motDePasse) 
            && motDePasse.Length >= LongueurMinimaleMotDePasse;
    }
    
    // TODO (2024-02-15): Ajouter authentification à deux facteurs (2FA)
    // Ticket: SECURITY-123
    // Responsable: équipe-sécurité@example.com
    
    public void SupprimerUtilisateur(int idUtilisateur)
    {
        // Implémentation
    }
}
```

---

## 📝 Résumé de la Semaine

### Les 3 Code Smells Étudiés

1. **Noms Non Significatifs** 
   - ❌ Variables d'une lettre, abréviations obscures
   - ✅ Noms révélant l'intention, prononçables, cherchables

2. **Fonctions Trop Longues**
   - ❌ Fonction qui fait tout, 100+ lignes
   - ✅ Petites fonctions (5-20 lignes), une responsabilité, un niveau d'abstraction

3. **Commentaires Inutiles**
   - ❌ Commentaires redondants, obsolètes, compensant du mauvais code
   - ✅ Code auto-documenté, commentaires expliquant le POURQUOI

### Citations à Retenir

> "Le code propre est simple et direct. Il se lit comme une prose bien écrite."
> — Grady Booch

> "Un commentaire mensonger est pire que pas de commentaire du tout."
> — Robert C. Martin

### Checklist Avant de Commiter du Code

- [ ] Mes variables ont des noms significatifs et révèlent leur intention
- [ ] Mes méthodes font UNE SEULE chose
- [ ] Mes méthodes ont moins de 20 lignes
- [ ] Mes méthodes ont 3 paramètres ou moins
- [ ] J'ai éliminé les commentaires qui décrivent ce que le code fait
- [ ] Mes commentaires (s'il y en a) expliquent le POURQUOI
- [ ] Mon code se lit comme une histoire claire

---

## 🎯 Défi de la Semaine

**Trouvez dans votre propre code :**
1. **3 variables** avec des noms non significatifs → Renommez-les
2. **1 fonction longue** (>30 lignes) → Décomposez-la
3. **5 commentaires inutiles** → Supprimez-les en rendant le code auto-documenté

Partagez vos avant/après avec la classe la semaine prochaine !

---

## 🔜 Aperçu Semaine Prochaine

La semaine prochaine, nous aborderons :
- **Code Duplication** (DRY Principle)
- **Classes trop grandes**
- **Couplage fort vs Couplage faible**
---

*"Laissez le code plus propre que vous ne l'avez trouvé."* — Règle du Boy Scout