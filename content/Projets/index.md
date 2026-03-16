---
title: "Epics et User Stories"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 51
---

---

## 1. Vue d'ensemble des Epics

| Epic ID | Nom | Priorité | Stories | Valeur métier |
|---------|-----|----------|---------|---------------|
| EPIC-01 | Authentification de base | 🔴 Critique | 6 | Fonctionnalité cœur |
| EPIC-02 | Validation et sécurité | 🔴 Critique | 7 | Protection des données |
| EPIC-03 | Expérience utilisateur | 🟡 Haute | 5 | Satisfaction client |
| EPIC-04 | Récupération et aide | 🟢 Moyenne | 3 | Support autonome |
| EPIC-05 | Navigation et flux | 🔴 Critique | 4 | Parcours utilisateur |
| **TOTAL** | | | **25** | |

---

## 2. EPIC-01 : Authentification de base

**Objectif :** Permettre aux utilisateurs de s'authentifier et d'accéder à l'application de manière sécurisée.

**Valeur métier :** Sans cette fonctionnalité, l'application ne peut pas fonctionner. C'est le cœur du système.

### Fonctionnalités de l'Epic

#### F-01.1 : Connexion utilisateur
Permettre à un utilisateur existant de se connecter avec ses identifiants.

#### F-01.2 : Création de compte
Permettre à un nouvel utilisateur de créer un compte dans le système.

#### F-01.3 : Gestion des sessions
Maintenir la session utilisateur active après connexion.

---

### User Stories - EPIC-01

#### US-01.1 : Saisie des identifiants de connexion
**Fonctionnalité :** F-01.1 - Connexion utilisateur  
**En tant qu'** utilisateur enregistré  
**Je veux** saisir mon email et mon mot de passe  
**Afin de** me connecter à mon compte

**Critères d'acceptation :**
- [ ] Un champ "Email" est présent et fonctionnel
- [ ] Un champ "Mot de passe" masque les caractères saisis
- [ ] Les deux champs acceptent la saisie au clavier
- [ ] Le champ email accepte jusqu'à 100 caractères
- [ ] Le champ mot de passe accepte jusqu'à 50 caractères

**Scénarios de test :**
1. ✅ Saisir un email valide
2. ✅ Saisir un mot de passe
3. ✅ Vérifier que le mot de passe est masqué

---

#### US-01.2 : Vérification des identifiants
**Fonctionnalité :** F-01.1 - Connexion utilisateur  
**En tant qu'** utilisateur enregistré  
**Je veux** que le système vérifie mes identifiants  
**Afin de** accéder à mon compte si ils sont corrects

**Critères d'acceptation :**
- [ ] Le système vérifie l'email dans la base de données
- [ ] Le système compare le mot de passe hashé
- [ ] Si correct, l'utilisateur est redirigé vers la page principale
- [ ] Si incorrect, un message d'erreur s'affiche
- [ ] Le message d'erreur ne révèle pas si c'est l'email ou le mot de passe qui est incorrect

**Scénarios de test :**
1. ✅ Connexion avec identifiants valides
2. ❌ Connexion avec email invalide
3. ❌ Connexion avec mot de passe invalide
4. ❌ Connexion avec email inexistant

---

#### US-01.3 : Saisie des informations de création de compte
**Fonctionnalité :** F-01.2 - Création de compte  
**En tant que** nouvel utilisateur  
**Je veux** saisir mes informations personnelles  
**Afin de** créer un nouveau compte

**Critères d'acceptation :**
- [ ] Un champ "Nom complet" est présent
- [ ] Un champ "Email" est présent
- [ ] Un champ "Mot de passe" est présent
- [ ] Un champ "Confirmer mot de passe" est présent
- [ ] Tous les champs sont obligatoires

**Scénarios de test :**
1. ✅ Remplir tous les champs avec des données valides
2. ❌ Tenter de soumettre avec des champs vides
3. ❌ Soumettre avec seulement certains champs remplis

---

#### US-01.4 : Création du compte dans le système
**Fonctionnalité :** F-01.2 - Création de compte  
**En tant que** nouvel utilisateur  
**Je veux** que mon compte soit créé dans le système  
**Afin de** pouvoir me connecter par la suite

**Critères d'acceptation :**
- [ ] Le système crée un nouvel enregistrement dans la base de données
- [ ] Le mot de passe est hashé avant d'être stocké
- [ ] Un ID unique est généré pour l'utilisateur
- [ ] La date de création est enregistrée
- [ ] L'utilisateur reçoit une confirmation visuelle
- [ ] L'utilisateur est automatiquement connecté après création

**Scénarios de test :**
1. ✅ Créer un compte avec toutes les informations valides
2. ✅ Vérifier que le compte existe dans la base de données
3. ✅ Vérifier que le mot de passe est hashé

---

#### US-01.5 : Unicité de l'email
**Fonctionnalité :** F-01.2 - Création de compte  
**En tant que** système  
**Je veux** vérifier que l'email n'existe pas déjà  
**Afin de** garantir un email unique par utilisateur

**Critères d'acceptation :**
- [ ] Le système vérifie l'email dans la base avant création
- [ ] Si l'email existe, un message d'erreur s'affiche
- [ ] Le message suggère de se connecter à la place
- [ ] Le message propose un lien "Mot de passe oublié?"

**Scénarios de test :**
1. ❌ Tenter de créer un compte avec un email existant
2. ✅ Créer un compte avec un email non utilisé
3. ✅ Vérifier le message d'erreur approprié

---

#### US-01.6 : Maintien de la session
**Fonctionnalité :** F-01.3 - Gestion des sessions  
**En tant qu'** utilisateur connecté  
**Je veux** que ma session reste active  
**Afin de** ne pas avoir à me reconnecter constamment

**Critères d'acceptation :**
- [ ] Un token de session est généré à la connexion
- [ ] La session reste active pendant 30 minutes d'inactivité
- [ ] L'activité de l'utilisateur prolonge la session
- [ ] L'utilisateur peut se déconnecter manuellement

**Scénarios de test :**
1. ✅ Rester connecté pendant 15 minutes
2. ✅ Vérifier que la session expire après 30 minutes d'inactivité
3. ✅ Utiliser l'application et vérifier la prolongation

---

## 3. EPIC-02 : Validation et sécurité

**Objectif :** Garantir l'intégrité des données et la sécurité des comptes utilisateurs.

**Valeur métier :** Protège l'application contre les failles de sécurité et garantit la qualité des données.

### Fonctionnalités de l'Epic

#### F-02.1 : Validation des champs
Vérifier que tous les champs requis sont remplis et ont le bon format.

#### F-02.2 : Validation du mot de passe
Assurer que les mots de passe respectent les critères de sécurité.

#### F-02.3 : Protection contre les attaques
Implémenter des mesures de sécurité contre les attaques courantes.

---

### User Stories - EPIC-02

#### US-02.1 : Validation champs obligatoires
**Fonctionnalité :** F-02.1 - Validation des champs  
**En tant qu'** utilisateur  
**Je veux** être averti si j'ai oublié de remplir un champ  
**Afin de** corriger mon erreur avant de soumettre

**Critères d'acceptation :**
- [ ] Les champs vides affichent un message d'erreur
- [ ] Le bouton de soumission est désactivé si des champs sont vides
- [ ] Le message d'erreur disparaît quand le champ est rempli
- [ ] Chaque champ a un message d'erreur spécifique

**Scénarios de test :**
1. ❌ Soumettre le formulaire avec email vide
2. ❌ Soumettre le formulaire avec mot de passe vide
3. ❌ Soumettre le formulaire avec tous les champs vides
4. ✅ Remplir tous les champs et voir les erreurs disparaître

---

#### US-02.2 : Validation format email
**Fonctionnalité :** F-02.1 - Validation des champs  
**En tant qu'** utilisateur  
**Je veux** être averti si mon email n'a pas le bon format  
**Afin de** entrer une adresse email valide

**Critères d'acceptation :**
- [ ] L'email doit contenir un @
- [ ] L'email doit contenir un point après le @
- [ ] L'email ne peut pas commencer ou finir par @
- [ ] Un message d'erreur s'affiche pour les formats invalides
- [ ] La validation se fait en temps réel pendant la saisie

**Scénarios de test :**
1. ❌ Entrer "email" (sans @)
2. ❌ Entrer "email@domain" (sans extension)
3. ❌ Entrer "@domain.com" (sans nom)
4. ✅ Entrer "email@domain.com"

---

#### US-02.3 : Force du mot de passe - Longueur
**Fonctionnalité :** F-02.2 - Validation du mot de passe  
**En tant que** nouvel utilisateur  
**Je veux** savoir si mon mot de passe est assez long  
**Afin de** créer un mot de passe sécurisé

**Critères d'acceptation :**
- [ ] Le mot de passe doit contenir minimum 8 caractères
- [ ] Un message d'erreur s'affiche si trop court
- [ ] Le compteur de caractères est visible
- [ ] Le message disparaît quand la longueur est suffisante

**Scénarios de test :**
1. ❌ Entrer un mot de passe de 5 caractères
2. ❌ Entrer un mot de passe de 7 caractères
3. ✅ Entrer un mot de passe de 8 caractères
4. ✅ Entrer un mot de passe de 20 caractères

---

#### US-02.4 : Force du mot de passe - Complexité
**Fonctionnalité :** F-02.2 - Validation du mot de passe  
**En tant que** nouvel utilisateur  
**Je veux** voir la force de mon mot de passe  
**Afin de** créer un mot de passe robuste

**Critères d'acceptation :**
- [ ] Un indicateur visuel montre la force (Faible/Moyen/Fort/Excellent)
- [ ] La force augmente avec les majuscules
- [ ] La force augmente avec les chiffres
- [ ] La force augmente avec les caractères spéciaux
- [ ] Un code couleur accompagne l'indicateur (rouge/orange/vert/bleu)

**Scénarios de test :**
1. Mot de passe "password" → Faible (rouge)
2. Mot de passe "Password1" → Moyen (orange)
3. Mot de passe "Password1!" → Fort (vert)
4. Mot de passe "P@ssw0rd!2024" → Excellent (bleu)

---

#### US-02.5 : Confirmation du mot de passe
**Fonctionnalité :** F-02.2 - Validation du mot de passe  
**En tant que** nouvel utilisateur  
**Je veux** confirmer mon mot de passe  
**Afin de** m'assurer de ne pas avoir fait de faute de frappe

**Critères d'acceptation :**
- [ ] Un champ "Confirmer le mot de passe" est présent
- [ ] Un message d'erreur s'affiche si les mots de passe diffèrent
- [ ] La validation se fait en temps réel
- [ ] Une icône de validation (✓) apparaît quand ils correspondent

**Scénarios de test :**
1. ❌ Entrer deux mots de passe différents
2. ✅ Entrer deux mots de passe identiques
3. ✅ Modifier le premier et voir l'erreur apparaître

---

#### US-02.6 : Exigences du mot de passe visibles
**Fonctionnalité :** F-02.2 - Validation du mot de passe  
**En tant que** nouvel utilisateur  
**Je veux** voir les exigences du mot de passe  
**Afin de** savoir ce qui est attendu

**Critères d'acceptation :**
- [ ] Une liste des exigences est visible près du champ mot de passe
- [ ] Chaque exigence est cochée (✓) quand respectée
- [ ] Les exigences incluent : longueur, majuscule, minuscule, chiffre, spécial
- [ ] Les exigences sont mises à jour en temps réel

**Scénarios de test :**
1. Champ vide → Aucune exigence cochée
2. Entrer "password" → Longueur et minuscule cochées
3. Entrer "Password1!" → Toutes les exigences cochées

---

#### US-02.7 : Limitation des tentatives de connexion
**Fonctionnalité :** F-02.3 - Protection contre les attaques  
**En tant que** système  
**Je veux** limiter les tentatives de connexion échouées  
**Afin de** protéger contre les attaques par force brute

**Critères d'acceptation :**
- [ ] Après 5 tentatives échouées, le compte est temporairement verrouillé
- [ ] Le verrouillage dure 15 minutes
- [ ] Un message informe l'utilisateur du verrouillage
- [ ] Un compteur de tentatives restantes est visible

**Scénarios de test :**
1. ❌ Échouer 3 fois → Voir "2 tentatives restantes"
2. ❌ Échouer 5 fois → Compte verrouillé
3. ⏱️ Attendre 15 minutes → Pouvoir réessayer

---

## 4. EPIC-03 : Expérience utilisateur

**Objectif :** Améliorer le confort et la satisfaction des utilisateurs lors de l'authentification.

**Valeur métier :** Augmente la rétention utilisateur et réduit les frustrations.

### Fonctionnalités de l'Epic

#### F-03.1 : Mémorisation des informations
Permettre aux utilisateurs de sauvegarder leurs préférences.

#### F-03.2 : Feedback visuel
Fournir des retours visuels clairs sur les actions.

#### F-03.3 : Accessibilité
Assurer que l'interface est accessible à tous.

---

### User Stories - EPIC-03

#### US-03.1 : Se souvenir de moi
**Fonctionnalité :** F-03.1 - Mémorisation des informations  
**En tant qu'** utilisateur régulier  
**Je veux** cocher "Se souvenir de moi"  
**Afin de** ne pas avoir à saisir mon email à chaque connexion

**Critères d'acceptation :**
- [ ] Une case à cocher "Se souvenir de moi" est présente
- [ ] Si cochée, l'email est sauvegardé localement (de façon sécurisée)
- [ ] À la prochaine ouverture, l'email est pré-rempli
- [ ] L'utilisateur peut décocher pour supprimer l'email sauvegardé

**Scénarios de test :**
1. ✅ Cocher "Se souvenir" et se connecter
2. ✅ Fermer et rouvrir → Email pré-rempli
3. ✅ Se connecter sans cocher → Email non sauvegardé

---

#### US-03.2 : Indicateur de chargement
**Fonctionnalité :** F-03.2 - Feedback visuel  
**En tant qu'** utilisateur  
**Je veux** voir un indicateur pendant la connexion  
**Afin de** savoir que ma requête est en cours de traitement

**Critères d'acceptation :**
- [ ] Un indicateur de chargement (spinner) apparaît lors de la connexion
- [ ] Le bouton "Se connecter" est désactivé pendant le chargement
- [ ] Un overlay empêche les clics pendant le traitement
- [ ] Un texte "Connexion en cours..." est affiché

**Scénarios de test :**
1. ✅ Cliquer sur "Se connecter" et voir le spinner
2. ✅ Vérifier que le bouton est grisé
3. ✅ Tenter de cliquer pendant le chargement → Aucun effet

---

#### US-03.3 : Messages d'erreur clairs
**Fonctionnalité :** F-03.2 - Feedback visuel  
**En tant qu'** utilisateur  
**Je veux** des messages d'erreur compréhensibles  
**Afin de** savoir exactement ce qui ne va pas

**Critères d'acceptation :**
- [ ] Les messages d'erreur sont en français clair
- [ ] Chaque erreur a un message spécifique (pas de "Erreur générique")
- [ ] Les messages sont affichés dans une boîte colorée (rouge)
- [ ] Une icône d'avertissement accompagne le message

**Scénarios de test :**
1. Email invalide → "Format d'email invalide"
2. Mot de passe trop court → "Le mot de passe doit contenir au moins 8 caractères"
3. Mots de passe différents → "Les mots de passe ne correspondent pas"

---

#### US-03.4 : Acceptation des conditions
**Fonctionnalité :** F-03.1 - Mémorisation des informations  
**En tant que** nouvel utilisateur  
**Je veux** accepter les conditions d'utilisation  
**Afin de** créer mon compte en toute transparence

**Critères d'acceptation :**
- [ ] Une case "J'accepte les conditions d'utilisation" est présente
- [ ] Le bouton de création est désactivé si non cochée
- [ ] Un lien "Voir les conditions" ouvre une fenêtre
- [ ] Les conditions sont affichées de manière lisible

**Scénarios de test :**
1. ❌ Tenter de créer un compte sans cocher
2. ✅ Cocher la case → Bouton activé
3. ✅ Cliquer sur "Voir les conditions" → Fenêtre s'ouvre

---

#### US-03.5 : Affichage/masquage du mot de passe
**Fonctionnalité :** F-03.2 - Feedback visuel  
**En tant qu'** utilisateur  
**Je veux** pouvoir afficher temporairement mon mot de passe  
**Afin de** vérifier ce que j'ai tapé

**Critères d'acceptation :**
- [ ] Une icône "œil" est présente à côté du champ mot de passe
- [ ] Cliquer sur l'icône affiche le mot de passe en clair
- [ ] Cliquer à nouveau le masque
- [ ] Un tooltip indique "Afficher le mot de passe"

**Scénarios de test :**
1. ✅ Entrer un mot de passe (masqué)
2. ✅ Cliquer sur l'œil → Voir le texte en clair
3. ✅ Cliquer à nouveau → Masqué de nouveau

---

## 5. EPIC-04 : Récupération et aide

**Objectif :** Aider les utilisateurs qui rencontrent des problèmes d'accès à leur compte.

**Valeur métier :** Réduit les demandes de support et améliore l'autonomie des utilisateurs.

### Fonctionnalités de l'Epic

#### F-04.1 : Récupération de mot de passe
Permettre aux utilisateurs de réinitialiser leur mot de passe oublié.

#### F-04.2 : Support et aide
Fournir de l'aide contextuelle aux utilisateurs.

---

### User Stories - EPIC-04

#### US-04.1 : Lien mot de passe oublié
**Fonctionnalité :** F-04.1 - Récupération de mot de passe  
**En tant qu'** utilisateur ayant oublié son mot de passe  
**Je veux** cliquer sur "Mot de passe oublié?"  
**Afin de** accéder à la récupération

**Critères d'acceptation :**
- [ ] Un lien "Mot de passe oublié?" est visible sur la page de connexion
- [ ] Le lien est clairement identifiable (souligné ou en couleur)
- [ ] Le clic ouvre une nouvelle fenêtre de récupération
- [ ] Le lien est accessible au clavier (Tab + Enter)

**Scénarios de test :**
1. ✅ Cliquer sur le lien → Fenêtre s'ouvre
2. ✅ Naviguer au clavier et appuyer sur Enter
3. ✅ Vérifier que la fenêtre de récupération s'affiche

---

#### US-04.2 : Demande de réinitialisation
**Fonctionnalité :** F-04.1 - Récupération de mot de passe  
**En tant qu'** utilisateur  
**Je veux** entrer mon email pour demander une réinitialisation  
**Afin de** recevoir un lien de réinitialisation

**Critères d'acceptation :**
- [ ] Un champ email est présent dans la fenêtre de récupération
- [ ] Un bouton "Envoyer le lien" est présent
- [ ] Le système vérifie que l'email existe dans la base
- [ ] Un email est envoyé avec un lien temporaire (valide 1 heure)
- [ ] Un message de confirmation s'affiche

**Scénarios de test :**
1. ✅ Entrer un email existant → Email envoyé
2. ❌ Entrer un email inexistant → Message "Email non trouvé"
3. ✅ Vérifier le message de confirmation

---

#### US-04.3 : Aide contextuelle
**Fonctionnalité :** F-04.2 - Support et aide  
**En tant qu'** utilisateur confus  
**Je veux** voir des tooltips d'aide  
**Afin de** comprendre ce qu'on attend de moi

**Critères d'acceptation :**
- [ ] Des icônes "?" sont présentes près des champs complexes
- [ ] Survoler l'icône affiche un tooltip explicatif
- [ ] Les tooltips expliquent les exigences en langage simple
- [ ] Les tooltips restent affichés tant que la souris est dessus

**Scénarios de test :**
1. ✅ Survoler "?" près du mot de passe → Voir les exigences
2. ✅ Survoler "?" près de l'email → Voir le format attendu
3. ✅ Déplacer la souris → Tooltip disparaît

---

## 6. EPIC-05 : Navigation et flux

**Objectif :** Assurer une navigation fluide et intuitive entre les différentes pages du système d'authentification.

**Valeur métier :** Améliore l'expérience utilisateur et réduit les abandons.

### Fonctionnalités de l'Epic

#### F-05.1 : Navigation entre pages
Permettre aux utilisateurs de naviguer facilement entre connexion et inscription.

#### F-05.2 : Raccourcis clavier
Faciliter les actions avec le clavier.

---

### User Stories - EPIC-05

#### US-05.1 : Navigation vers création de compte
**Fonctionnalité :** F-05.1 - Navigation entre pages  
**En tant que** nouvel utilisateur  
**Je veux** cliquer sur "Créer un compte"  
**Afin de** accéder à la page d'inscription

**Critères d'acceptation :**
- [ ] Un lien/bouton "Créer un compte" est visible en bas de la page de connexion
- [ ] Le texte est clair : "Pas encore de compte? Créer un compte"
- [ ] Le clic charge la page de création de compte
- [ ] La transition est fluide (pas de flash)

**Scénarios de test :**
1. ✅ Cliquer sur "Créer un compte" → Page d'inscription s'affiche
2. ✅ Vérifier que les champs de connexion sont réinitialisés
3. ✅ Vérifier la fluidité de la transition

---

#### US-05.2 : Navigation vers connexion
**Fonctionnalité :** F-05.1 - Navigation entre pages  
**En tant qu'** utilisateur ayant déjà un compte  
**Je veux** cliquer sur "Se connecter"  
**Afin de** revenir à la page de connexion

**Critères d'acceptation :**
- [ ] Un lien "Déjà un compte? Se connecter" est visible sur la page d'inscription
- [ ] Le clic charge la page de connexion
- [ ] Les données saisies dans l'inscription ne sont pas perdues si on revient

**Scénarios de test :**
1. ✅ Cliquer sur "Se connecter" → Page de connexion s'affiche
2. ✅ Remplir un formulaire, naviguer, revenir → Données préservées
3. ✅ Vérifier la fluidité de la transition

---

#### US-05.3 : Touche Enter pour soumettre
**Fonctionnalité :** F-05.2 - Raccourcis clavier  
**En tant qu'** utilisateur  
**Je veux** appuyer sur Enter pour me connecter  
**Afin de** soumettre rapidement le formulaire

**Critères d'acceptation :**
- [ ] Appuyer sur Enter dans n'importe quel champ soumet le formulaire
- [ ] Le comportement est identique à cliquer sur le bouton
- [ ] La validation se fait avant la soumission
- [ ] Si des erreurs existent, Enter ne soumet pas

**Scénarios de test :**
1. ✅ Remplir email et mot de passe, appuyer sur Enter → Connexion
2. ❌ Laisser un champ vide, appuyer sur Enter → Erreur affichée
3. ✅ Vérifier que ça fonctionne depuis les deux champs

---

#### US-05.4 : Navigation au clavier
**Fonctionnalité :** F-05.2 - Raccourcis clavier  
**En tant qu'** utilisateur utilisant le clavier  
**Je veux** naviguer entre les champs avec Tab  
**Afin de** remplir le formulaire sans la souris

**Critères d'acceptation :**
- [ ] Tab passe au champ suivant dans l'ordre logique
- [ ] Shift+Tab revient au champ précédent
- [ ] Les boutons et liens sont accessibles avec Tab
- [ ] Le focus est visuellement visible (contour)

**Scénarios de test :**
1. ✅ Appuyer sur Tab répétitivement → Parcourt tous les champs
2. ✅ Utiliser Shift+Tab → Retour en arrière
3. ✅ Vérifier que le focus est visible
4. ✅ Atteindre le bouton et appuyer sur Enter → Soumission

---

## 7. Tableau récapitulatif

### Vue d'ensemble par Epic

| Epic | Fonctionnalités | User Stories | Points estimés | Priorité |
|------|-----------------|--------------|----------------|----------|
| **EPIC-01: Authentification de base** | 3 | 6 | 21 | 🔴 Critique |
| **EPIC-02: Validation et sécurité** | 3 | 7 | 28 | 🔴 Critique |
| **EPIC-03: Expérience utilisateur** | 3 | 5 | 13 | 🟡 Haute |
| **EPIC-04: Récupération et aide** | 2 | 3 | 8 | 🟢 Moyenne |
| **EPIC-05: Navigation et flux** | 2 | 4 | 8 | 🔴 Critique |
| **TOTAL** | **13** | **25** | **78** | |

### Détail des User Stories

| ID | Epic | Titre | Priorité | Complexité | Effort |
|----|------|-------|----------|------------|--------|
| US-01.1 | EPIC-01 | Saisie des identifiants de connexion | 🔴 | Faible | 2 |
| US-01.2 | EPIC-01 | Vérification des identifiants | 🔴 | Moyenne | 5 |
| US-01.3 | EPIC-01 | Saisie des informations de création | 🔴 | Faible | 2 |
| US-01.4 | EPIC-01 | Création du compte dans le système | 🔴 | Haute | 8 |
| US-01.5 | EPIC-01 | Unicité de l'email | 🔴 | Moyenne | 3 |
| US-01.6 | EPIC-01 | Maintien de la session | 🔴 | Faible | 1 |
| US-02.1 | EPIC-02 | Validation champs obligatoires | 🔴 | Faible | 3 |
| US-02.2 | EPIC-02 | Validation format email | 🔴 | Moyenne | 3 |
| US-02.3 | EPIC-02 | Force mot de passe - Longueur | 🔴 | Faible | 2 |
| US-02.4 | EPIC-02 | Force mot de passe - Complexité | 🔴 | Moyenne | 5 |
| US-02.5 | EPIC-02 | Confirmation du mot de passe | 🔴 | Faible | 2 |
| US-02.6 | EPIC-02 | Exigences mot de passe visibles | 🔴 | Moyenne | 5 |
| US-02.7 | EPIC-02 | Limitation tentatives connexion | 🔴 | Haute | 8 |
| US-03.1 | EPIC-03 | Se souvenir de moi | 🟡 | Moyenne | 3 |
| US-03.2 | EPIC-03 | Indicateur de chargement | 🟡 | Faible | 2 |
| US-03.3 | EPIC-03 | Messages d'erreur clairs | 🟡 | Faible | 2 |
| US-03.4 | EPIC-03 | Acceptation des conditions | 🟡 | Faible | 2 |
| US-03.5 | EPIC-03 | Affichage/masquage mot de passe | 🟡 | Moyenne | 4 |
| US-04.1 | EPIC-04 | Lien mot de passe oublié | 🟢 | Faible | 1 |
| US-04.2 | EPIC-04 | Demande de réinitialisation | 🟢 | Haute | 5 |
| US-04.3 | EPIC-04 | Aide contextuelle | 🟢 | Faible | 2 |
| US-05.1 | EPIC-05 | Navigation vers création compte | 🔴 | Faible | 2 |
| US-05.2 | EPIC-05 | Navigation vers connexion | 🔴 | Faible | 2 |
| US-05.3 | EPIC-05 | Touche Enter pour soumettre | 🔴 | Faible | 2 |
| US-05.4 | EPIC-05 | Navigation au clavier | 🔴 | Faible | 2 |

---

## 8. Roadmap recommandée

### Sprint 1 (2 semaines) - MVP Core
**Objectif :** Connexion et création de compte fonctionnels

**Stories incluses :**
- EPIC-01 : US-01.1, US-01.2, US-01.3, US-01.4, US-01.5 (5 stories)
- EPIC-05 : US-05.1, US-05.2 (2 stories)

**Effort total :** 22 points  
**Livrable :** Utilisateurs peuvent créer un compte et se connecter

---

### Sprint 2 (2 semaines) - Sécurité et Validation
**Objectif :** Sécuriser l'application et valider les données

**Stories incluses :**
- EPIC-02 : US-02.1, US-02.2, US-02.3, US-02.4, US-02.5, US-02.6, US-02.7 (7 stories)
- EPIC-01 : US-01.6 (1 story)

**Effort total :** 29 points  
**Livrable :** Application sécurisée avec validation complète

---

### Sprint 3 (1 semaine) - Expérience utilisateur
**Objectif :** Améliorer le confort d'utilisation

**Stories incluses :**
- EPIC-03 : US-03.1, US-03.2, US-03.3, US-03.4, US-03.5 (5 stories)
- EPIC-05 : US-05.3, US-05.4 (2 stories)

**Effort total :** 17 points  
**Livrable :** Application polie avec excellente UX

---

### Sprint 4 (1 semaine) - Support et finition
**Objectif :** Ajouter les fonctionnalités de support

**Stories incluses :**
- EPIC-04 : US-04.1, US-04.2, US-04.3 (3 stories)

**Effort total :** 8 points  
**Livrable :** Application complète et autonome

---

### Timeline visuelle

```
Sprint 1 (2 sem)  |  Sprint 2 (2 sem)  |  Sprint 3 (1 sem)  |  Sprint 4 (1 sem)
──────────────────────────────────────────────────────────────────────────────
MVP Core          |  Sécurité          |  UX                |  Support
7 stories         |  8 stories         |  7 stories         |  3 stories
22 points         |  29 points         |  17 points         |  8 points
──────────────────────────────────────────────────────────────────────────────
                              TOTAL: 6 semaines, 25 stories, 76 points
```

---

### Critères de succès du projet

**Techniques :**
- [ ] 100% des User Stories implémentées
- [ ] Tests unitaires > 80% de couverture
- [ ] Temps de réponse < 2 secondes
- [ ] Aucune faille de sécurité critique

**Utilisateur :**
- [ ] Taux de création de compte réussie > 95%
- [ ] Taux de connexion réussie > 98%
- [ ] Satisfaction utilisateur > 4/5
- [ ] Support tickets < 5% des utilisateurs

**Métier :**
- [ ] Authentification fonctionnelle
- [ ] Sécurité conforme aux standards
- [ ] Expérience utilisateur fluide
- [ ] Application maintenable et évolutive