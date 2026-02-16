---
title: "WPF - intro"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 14
---

# Développement d'applications WPF avec C#

---

## Partie 1 : Installation de .NET Desktop Development

### 1.1 Pourquoi Visual Studio 2022 ?

**Visual Studio 2022** est l'IDE (environnement de développement intégré) officiel de Microsoft pour créer des applications .NET.

**Avantages :**
- Designer visuel pour WPF (glisser-déposer de contrôles)
- IntelliSense (auto-complétion intelligente)
- Débogueur puissant
- Gratuit dans la version Community

### 1.2 Étapes d'installation

#### Étape 1 : Télécharger Visual Studio 2022

1. Allez sur : https://visualstudio.microsoft.com/fr/
2. Téléchargez **Visual Studio 2022 Community** (gratuit)
3. Lancez le programme d'installation téléchargé

#### Étape 2 : Installer la charge de travail .NET Desktop

Quand le **Visual Studio Installer** s'ouvre :

1. **Dans l'onglet "Charges de travail"**, cochez :
   - ✅ **Développement .NET Desktop** (en français)
   - ✅ **".NET desktop development"** (en anglais)

2. Cette charge de travail installe automatiquement :
   - .NET SDK (kit de développement)
   - Windows Forms
   - **WPF (Windows Presentation Foundation)**
   - Templates de projets pour applications de bureau

3. Cliquez sur **"Installer"** ou **"Modifier"** (si VS est déjà installé)

#### Étape 3 : Vérifier l'installation

Une fois l'installation terminée :

1. Lancez **Visual Studio 2022**
2. Cliquez sur **"Créer un projet"**
3. Dans la barre de recherche, tapez **"WPF"**
4. Vous devriez voir : **"Application WPF (.NET)"**

Si vous voyez ce template, **l'installation a réussi** ! ✅

### 1.3 Créer votre premier projet WPF

**Procédure complète :**

1. **Fichier** → **Nouveau** → **Projet**
2. Recherchez **"WPF"**
3. Sélectionnez **"Application WPF"** (assurez-vous que c'est bien .NET et pas .NET Framework)
4. Cliquez sur **"Suivant"**
5. Donnez un nom au projet : `MonPremierWPF`
6. Choisissez un emplacement sur votre disque
7. Cliquez sur **"Suivant"**
8. Sélectionnez **".NET 8.0"** ou **".NET 7.0"** (la version la plus récente)
9. Cliquez sur **"Créer"**

**Visual Studio va créer :**
- Un fichier `MainWindow.xaml` (l'interface)
- Un fichier `MainWindow.xaml.cs` (le code C#)
- Un fichier `App.xaml` (configuration de l'application)

Appuyez sur **F5** pour lancer l'application. Une fenêtre vide s'ouvrira — c'est votre première app WPF ! 🎉

---

## Partie 2 : Introduction à la programmation de bureau

### 2.1 Qu'est-ce qu'une application de bureau ?

Jusqu'à présent, vous avez probablement écrit des **applications console** (avec `Console.WriteLine`).

**Application console :**
```
> Entrez votre nom: Alice
> Bonjour Alice!
```

**Application de bureau (WPF) :**
- Fenêtres avec boutons, champs de texte, images
- Interface graphique (GUI)
- Interaction à la souris
- Exemple : Microsoft Word, Excel, calculatrice Windows

### 2.2 Différence entre Console et WPF

| Aspect | Application Console | Application WPF |
|--------|---------------------|-----------------|
| Interface | Texte uniquement | Graphique (boutons, images, etc.) |
| Interaction | Clavier (input/output) | Souris + clavier |
| Apparence | Noire et blanche | Couleurs, polices, animations |
| Complexité | Simple | Plus complexe mais plus puissante |
| Utilisation | Scripts, outils admin | Logiciels utilisateur final |

### 2.3 Les 3 technologies de bureau en .NET

| Technologie | Année | Utilisation |
|-------------|-------|-------------|
| **Windows Forms** | 2002 | Ancienne, simple mais limitée |
| **WPF** | 2006 | Moderne, puissante, flexible |
| **WinUI 3** | 2021 | Très récente, encore en développement |

Dans ce cours, on se concentre sur **WPF** car c'est le standard de l'industrie.

### 2.4 Architecture d'une application WPF

```
MonProjet/
├── App.xaml                  ← Configuration de l'application
├── App.xaml.cs              ← Code-behind de App
├── MainWindow.xaml          ← Interface de la fenêtre principale
├── MainWindow.xaml.cs       ← Logique de la fenêtre principale
└── (Autres fenêtres, classes, ressources...)
```

**Principe fondamental :** WPF sépare l'interface (XAML) de la logique (C#).

---

## Partie 3 : Premiers pas avec WPF

### 3.1 Structure d'un projet WPF

Quand vous créez un projet WPF, Visual Studio génère automatiquement :

**App.xaml**
```xml
<Application x:Class="MonPremierWPF.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
         
    </Application.Resources>
</Application>
```

- `StartupUri="MainWindow.xaml"` → Quelle fenêtre s'ouvre au démarrage

**MainWindow.xaml**
```xml
<Window x:Class="MonPremierWPF.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="MainWindow" Height="450" Width="800">
    <Grid>
        
    </Grid>
</Window>
```

- `Title` → Titre de la fenêtre
- `Height` et `Width` → Dimensions en pixels
- `<Grid>` → Conteneur pour placer les contrôles

**MainWindow.xaml.cs**
```csharp
using System.Windows;

namespace MonPremierWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
```

- `InitializeComponent()` → Charge le XAML et construit l'interface

### 3.2 Le Designer visuel

Dans Visual Studio, quand vous ouvrez `MainWindow.xaml`, vous voyez **deux panneaux** :

1. **En haut : Le Designer** (aperçu visuel)
2. **En bas : Le code XAML**

Vous pouvez :
- **Glisser-déposer** des contrôles depuis la **Boîte à outils** (View → Toolbox)
- **Modifier le XAML** directement
- Les deux sont synchronisés !

### 3.3 Votre premier bouton

**Modifiez MainWindow.xaml :**

```xml
<Window x:Class="MonPremierWPF.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Ma première application" 
        Height="300" 
        Width="400">
    <Grid>
        <Button Content="Cliquez-moi !" 
                Width="150" 
                Height="50"
                Click="Button_Click" />
    </Grid>
</Window>
```

**Dans MainWindow.xaml.cs, ajoutez la méthode :**

```csharp
using System.Windows;

namespace MonPremierWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bonjour du monde WPF !");
        }
    }
}
```

**Appuyez sur F5** et cliquez sur le bouton. Un message apparaît ! 🎉

**Explications :**

1. `Click="Button_Click"` dans le XAML crée un lien vers la méthode C#
2. Visual Studio peut créer cette méthode automatiquement : double-cliquez sur le bouton dans le Designer
3. `MessageBox.Show()` affiche une boîte de dialogue

---

## Partie 4 : XAML — Le langage de l'interface

### 4.1 Qu'est-ce que XAML ?

**XAML** (prononcez "zamel") = **eXtensible Application Markup Language**

C'est un langage basé sur **XML** pour décrire des interfaces graphiques.

**Analogie :**
- XAML est à WPF ce que HTML est aux sites web
- C# est à WPF ce que JavaScript est aux sites web

### 4.2 Syntaxe de base XAML

#### Les balises (éléments)

```xml
<Button />  <!-- Balise auto-fermante -->

<Button>    <!-- Balise avec contenu -->
    Texte du bouton
</Button>
```

#### Les attributs (propriétés)

```xml
<Button Content="Mon bouton" 
        Width="100" 
        Height="40" 
        Background="Blue" />
```

Chaque attribut configure une **propriété** de l'objet C#.

**Équivalent en C# pur :**

```csharp
Button monBouton = new Button();
monBouton.Content = "Mon bouton";
monBouton.Width = 100;
monBouton.Height = 40;
monBouton.Background = Brushes.Blue;
```

### 4.3 Les namespaces XML

En haut de chaque fichier XAML :

```xml
xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
```

- **Premier namespace (par défaut)** : Contient tous les contrôles WPF (Button, TextBox, Grid, etc.)
- **Namespace `x:`** : Contient les éléments spéciaux XAML (x:Name, x:Class, etc.)

### 4.4 L'attribut x:Name

Pour accéder à un contrôle depuis le code C#, donnez-lui un **nom** :

```xml
<TextBox x:Name="txtNom" Width="200" Height="30" />
```

Ensuite, dans le C# :

```csharp
private void Button_Click(object sender, RoutedEventArgs e)
{
    string nom = txtNom.Text;  // Accès direct !
    MessageBox.Show($"Bonjour {nom}");
}
```

**Convention de nommage :**
- TextBox → `txt` prefix (ex: `txtNom`, `txtCourriel`)
- Button → `btn` prefix (ex: `btnEnvoyer`, `btnAnnuler`)
- Label → `lbl` prefix
- ListBox → `lst` prefix

### 4.5 Propriétés complexes

Certaines propriétés sont trop complexes pour un simple attribut.

**Syntaxe avec attribut (simple) :**
```xml
<Button Background="Blue" />
```

**Syntaxe avec balise (complexe) :**
```xml
<Button>
    <Button.Background>
        <LinearGradientBrush>
            <GradientStop Color="LightBlue" Offset="0" />
            <GradientStop Color="DarkBlue" Offset="1" />
        </LinearGradientBrush>
    </Button.Background>
    <Button.Content>
        Mon bouton avec dégradé
    </Button.Content>
</Button>
```

### 4.6 Les événements

Les événements relient l'interface au code C#.

**Événements courants :**

```xml
<Button Click="MonBouton_Click" />
<TextBox TextChanged="MonTexte_Changed" />
<CheckBox Checked="MaCase_Checked" />
<ListBox SelectionChanged="MaListe_SelectionChanged" />
```

**Dans le C# :**

```csharp
private void MonBouton_Click(object sender, RoutedEventArgs e)
{
    // Code exécuté quand on clique
}

private void MonTexte_Changed(object sender, TextChangedEventArgs e)
{
    // Code exécuté quand le texte change
}
```

---

## Partie 5 : Les Layouts (mise en page)

Les **Layouts** sont des conteneurs qui **organisent** les contrôles à l'écran.

### 5.1 Grid — Le layout principal

Le `Grid` divise l'espace en **lignes** et **colonnes**, comme un tableau Excel.

#### Exemple de base

```xml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
        <RowDefinition Height="*" />
        <RowDefinition Height="50" />
    </Grid.RowDefinitions>
    
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="200" />
        <ColumnDefinition Width="*" />
    </Grid.ColumnDefinitions>
    
    <!-- Placer les contrôles -->
    <Button Grid.Row="0" Grid.Column="0" Content="Haut gauche" />
    <Button Grid.Row="0" Grid.Column="1" Content="Haut droite" />
    <TextBox Grid.Row="1" Grid.Column="0" Grid.ColumnSpan="2" />
</Grid>
```

#### Types de dimensions

| Valeur | Signification | Exemple |
|--------|---------------|---------|
| `100` | Pixels fixes | `Height="100"` |
| `Auto` | S'adapte au contenu | `Height="Auto"` |
| `*` | Prend l'espace restant | `Height="*"` |
| `2*` | Prend 2 fois plus d'espace | `Height="2*"` |

**Exemple avec proportions :**

```xml
<Grid.ColumnDefinitions>
    <ColumnDefinition Width="*" />    <!-- 1/3 de l'espace -->
    <ColumnDefinition Width="2*" />   <!-- 2/3 de l'espace -->
</Grid.ColumnDefinitions>
```

Si la fenêtre fait 900 pixels de large :
- Colonne 0 : 300 pixels
- Colonne 1 : 600 pixels

#### Fusionner des cellules

```xml
<!-- Prendre 2 colonnes -->
<Button Grid.Row="0" Grid.Column="0" Grid.ColumnSpan="2" Content="Large" />

<!-- Prendre 2 rangées -->
<Button Grid.Row="0" Grid.Column="0" Grid.RowSpan="2" Content="Haut" />
```

### 5.2 StackPanel — Empilement

Le `StackPanel` empile les éléments **verticalement** (défaut) ou **horizontalement**.

#### Vertical (défaut)

```xml
<StackPanel>
    <Button Content="Bouton 1" Height="40" />
    <Button Content="Bouton 2" Height="40" />
    <Button Content="Bouton 3" Height="40" />
</StackPanel>
```

Les boutons sont empilés du haut vers le bas.

#### Horizontal

```xml
<StackPanel Orientation="Horizontal">
    <Button Content="Bouton 1" Width="100" />
    <Button Content="Bouton 2" Width="100" />
    <Button Content="Bouton 3" Width="100" />
</StackPanel>
```

Les boutons sont côte à côte.

#### Avec Margin (espacement)

```xml
<StackPanel Margin="20">
    <TextBlock Text="Nom :" FontWeight="Bold" />
    <TextBox Height="30" Margin="0,5,0,10" />
    
    <TextBlock Text="Courriel :" FontWeight="Bold" />
    <TextBox Height="30" Margin="0,5,0,10" />
    
    <Button Content="Envoyer" Height="40" />
</StackPanel>
```

**Margin** : `Left, Top, Right, Bottom`
- `Margin="10"` → 10 pixels de tous les côtés
- `Margin="10,20"` → 10 gauche/droite, 20 haut/bas
- `Margin="10,20,30,40"` → Gauche, Haut, Droite, Bas

### 5.3 WrapPanel — Empilement avec retour automatique

Comme `StackPanel`, mais retourne à la ligne si pas assez d'espace.

```xml
<WrapPanel>
    <Button Content="1" Width="100" Height="40" Margin="5" />
    <Button Content="2" Width="100" Height="40" Margin="5" />
    <Button Content="3" Width="100" Height="40" Margin="5" />
    <Button Content="4" Width="100" Height="40" Margin="5" />
    <Button Content="5" Width="100" Height="40" Margin="5" />
</WrapPanel>
```

Si la fenêtre est étroite, les boutons passent à la ligne suivante.

### 5.4 DockPanel — Ancrage sur les bords

```xml
<DockPanel>
    <Menu DockPanel.Dock="Top" Height="30" Background="LightGray">
        <MenuItem Header="Fichier" />
        <MenuItem Header="Edition" />
    </Menu>
    
    <StatusBar DockPanel.Dock="Bottom" Height="25" Background="LightGray">
        <TextBlock Text="Prêt" />
    </StatusBar>
    
    <TreeView DockPanel.Dock="Left" Width="200" />
    
    <TextBox />  <!-- Remplit le centre -->
</DockPanel>
```

**Résultat :** Interface classique avec menu en haut, barre de statut en bas, arbre à gauche, et zone centrale.

### 5.5 Canvas — Positionnement absolu

```xml
<Canvas>
    <Button Content="Bouton 1" 
            Canvas.Left="50" 
            Canvas.Top="100" 
            Width="100" 
            Height="40" />
    
    <Ellipse Fill="Red" 
             Canvas.Left="200" 
             Canvas.Top="150" 
             Width="80" 
             Height="80" />
</Canvas>
```

**Rarement utilisé** car pas responsive.

### 5.6 Exemple : Interface complète

**Application de calculatrice simple :**

```xml
<Window x:Class="Calculatrice.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Calculatrice" 
        Height="400" 
        Width="350">
    
    <Grid Margin="10">
        <Grid.RowDefinitions>
            <RowDefinition Height="80" />
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>
        
        <!-- Affichage -->
        <Border Grid.Row="0" 
                BorderBrush="Black" 
                BorderThickness="2" 
                Background="White"
                Padding="10">
            <TextBlock x:Name="txtAffichage" 
                       Text="0" 
                       FontSize="36" 
                       HorizontalAlignment="Right"
                       VerticalAlignment="Center" />
        </Border>
        
        <!-- Boutons -->
        <Grid Grid.Row="1" Margin="0,10,0,0">
            <Grid.RowDefinitions>
                <RowDefinition Height="*" />
                <RowDefinition Height="*" />
                <RowDefinition Height="*" />
                <RowDefinition Height="*" />
            </Grid.RowDefinitions>
            
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="*" />
                <ColumnDefinition Width="*" />
                <ColumnDefinition Width="*" />
                <ColumnDefinition Width="*" />
            </Grid.ColumnDefinitions>
            
            <!-- Ligne 0 -->
            <Button Grid.Row="0" Grid.Column="0" Content="7" FontSize="24" Margin="2" />
            <Button Grid.Row="0" Grid.Column="1" Content="8" FontSize="24" Margin="2" />
            <Button Grid.Row="0" Grid.Column="2" Content="9" FontSize="24" Margin="2" />
            <Button Grid.Row="0" Grid.Column="3" Content="÷" FontSize="24" Margin="2" Background="LightBlue" />
            
            <!-- Ligne 1 -->
            <Button Grid.Row="1" Grid.Column="0" Content="4" FontSize="24" Margin="2" />
            <Button Grid.Row="1" Grid.Column="1" Content="5" FontSize="24" Margin="2" />
            <Button Grid.Row="1" Grid.Column="2" Content="6" FontSize="24" Margin="2" />
            <Button Grid.Row="1" Grid.Column="3" Content="×" FontSize="24" Margin="2" Background="LightBlue" />
            
            <!-- Ligne 2 -->
            <Button Grid.Row="2" Grid.Column="0" Content="1" FontSize="24" Margin="2" />
            <Button Grid.Row="2" Grid.Column="1" Content="2" FontSize="24" Margin="2" />
            <Button Grid.Row="2" Grid.Column="2" Content="3" FontSize="24" Margin="2" />
            <Button Grid.Row="2" Grid.Column="3" Content="-" FontSize="24" Margin="2" Background="LightBlue" />
            
            <!-- Ligne 3 -->
            <Button Grid.Row="3" Grid.Column="0" Content="0" FontSize="24" Margin="2" Grid.ColumnSpan="2" />
            <Button Grid.Row="3" Grid.Column="2" Content="=" FontSize="24" Margin="2" Background="Orange" />
            <Button Grid.Row="3" Grid.Column="3" Content="+" FontSize="24" Margin="2" Background="LightBlue" />
        </Grid>
    </Grid>
    
</Window>
```

---

## Partie 6 : Les contrôles de base

### 6.1 TextBox — Saisie de texte

```xml
<TextBox x:Name="txtNom" 
         Width="200" 
         Height="30"
         Text="Valeur par défaut" />
```

**Propriétés importantes :**
- `Text` : Le contenu
- `MaxLength` : Limite de caractères
- `IsReadOnly` : Lecture seule
- `AcceptsReturn` : Multi-lignes

**Accès en C# :**
```csharp
string texte = txtNom.Text;
txtNom.Text = "Nouveau texte";
```

### 6.2 TextBlock — Texte non modifiable

```xml
<TextBlock Text="Ceci est un texte"
           FontSize="16"
           FontWeight="Bold"
           Foreground="DarkBlue" />
```

**Différence avec TextBox :**
- `TextBlock` : Affichage seulement (comme un label HTML)
- `TextBox` : Saisie utilisateur (comme un input HTML)

### 6.3 Button — Bouton

```xml
<Button Content="Cliquer ici" 
        Width="120" 
        Height="40"
        Click="MonBouton_Click" />
```

**Propriétés :**
- `Content` : Texte ou contenu
- `IsEnabled` : Actif/désactivé
- `Background` : Couleur de fond

### 6.4 CheckBox — Case à cocher

```xml
<CheckBox Content="J'accepte les conditions" 
          IsChecked="True"
          Checked="Case_Checked" />
```

**Accès en C# :**
```csharp
bool estCoche = maCaseACocher.IsChecked == true;
```

### 6.5 RadioButton — Bouton radio

```xml
<StackPanel>
    <RadioButton Content="Option 1" GroupName="Options" IsChecked="True" />
    <RadioButton Content="Option 2" GroupName="Options" />
    <RadioButton Content="Option 3" GroupName="Options" />
</StackPanel>
```

**GroupName** : Regroupe les RadioButtons (un seul sélectionnable par groupe)

### 6.6 ComboBox — Liste déroulante

```xml
<ComboBox x:Name="cbVilles" Width="200" Height="30">
    <ComboBoxItem Content="Montréal" />
    <ComboBoxItem Content="Québec" />
    <ComboBoxItem Content="Laval" />
</ComboBox>
```

**Accès en C# :**
```csharp
ComboBoxItem item = (ComboBoxItem)cbVilles.SelectedItem;
string ville = item.Content.ToString();
```

### 6.7 ListBox — Liste

```xml
<ListBox x:Name="lstNoms" Height="200">
    <ListBoxItem Content="Alice" />
    <ListBoxItem Content="Bob" />
    <ListBoxItem Content="Charlie" />
</ListBox>
```

**Ajouter des éléments en C# :**
```csharp
lstNoms.Items.Add("David");
```

### 6.8 Image — Afficher une image

```xml
<Image Source="logo.png" 
       Width="100" 
       Height="100" />
```

**Note :** L'image doit être dans le dossier du projet et configurée en **"Copy to Output Directory"**.

### 6.9 ProgressBar — Barre de progression

```xml
<ProgressBar x:Name="barreProgression" 
             Height="20" 
             Minimum="0" 
             Maximum="100" 
             Value="50" />
```

### 6.10 Slider — Curseur

```xml
<Slider x:Name="sliderVolume" 
        Minimum="0" 
        Maximum="100" 
        Value="50" 
        TickFrequency="10" 
        IsSnapToTickEnabled="True" />
```

---

## Partie 7 : Data Binding (Liaison de données)

Le **Data Binding** est la fonctionnalité qui rend WPF **vraiment puissant**. Il synchronise automatiquement l'interface avec les données.

### 7.1 Pourquoi le Data Binding ?

**Sans Data Binding (approche manuelle) :**

```csharp
private string nom = "Alice";

private void ChangerNom()
{
    nom = "Bob";
    txtAffichage.Text = nom;  // Mise à jour manuelle !
}
```

**Problème :** À chaque changement, il faut **manuellement** mettre à jour l'interface.

**Avec Data Binding :**

```xml
<TextBox Text="{Binding Nom}" />
```

Maintenant, quand `Nom` change, le TextBox se met à jour **automatiquement** !

### 7.2 Le DataContext

Le **DataContext** est l'objet source auquel les contrôles se lient.

**Exemple simple :**

**Classe Personne.cs :**
```csharp
public class Personne
{
    public string Nom { get; set; }
    public int Age { get; set; }
}
```

**MainWindow.xaml :**
```xml
<Window x:Class="BindingDemo.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Data Binding Demo" 
        Height="200" 
        Width="350">
    
    <StackPanel Margin="20">
        <TextBlock Text="Nom :" FontWeight="Bold" />
        <TextBox Text="{Binding Nom}" Height="30" Margin="0,5,0,10" />
        
        <TextBlock Text="Âge :" FontWeight="Bold" />
        <TextBox Text="{Binding Age}" Height="30" Margin="0,5,0,10" />
        
        <TextBlock Text="{Binding Nom}" FontSize="16" />
    </StackPanel>
    
</Window>
```

**MainWindow.xaml.cs :**
```csharp
using System.Windows;

namespace BindingDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // Créer une personne
            Personne p = new Personne 
            { 
                Nom = "Alice", 
                Age = 25 
            };
            
            // Définir le DataContext
            this.DataContext = p;
        }
    }
}
```

**Résultat :** Les TextBox affichent "Alice" et "25".

### 7.3 Les modes de Binding

| Mode | Direction | Usage |
|------|-----------|-------|
| `OneWay` | Source → UI | Affichage (lecture seule) |
| `TwoWay` | Source ↔ UI | Édition (bidirectionnel) |
| `OneTime` | Source → UI (une seule fois) | Valeurs constantes |
| `OneWayToSource` | UI → Source | Rare |

**Exemples :**

```xml
<!-- TwoWay : modifications dans les deux sens -->
<TextBox Text="{Binding Nom, Mode=TwoWay}" />

<!-- OneWay : lecture seule -->
<TextBlock Text="{Binding Nom, Mode=OneWay}" />
```

**Par défaut :**
- TextBox : `TwoWay`
- TextBlock : `OneWay`

---