---
title: "Introduction à Visual Studio 2022"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 1
---

![Visual Studio 2022](/420-413/images/visual_studio.png)

## 1. Introduction à Visual Studio 2022

### Qu'est-ce que Visual Studio ?
Visual Studio 2022 est l'**Environnement de Développement Intégré (IDE)** de Microsoft dédié au développement d'applications sur les plateformes **.NET**. Il offre un ensemble complet d'outils pour coder, tester, déboguer, déployer et collaborer efficacement. 

### Les éditions de Visual Studio 2022
| Édition | Public cible |
| :--- | :--- |
| **Community** | Gratuit : étudiants, développeurs open source et particuliers. |
| **Professional** | Développeurs professionnels et petites équipes. |
| **Enterprise** | Grandes équipes : outils avancés de test, performance et intégration continue. |

---

## 2. Installation et Configuration

### Étapes d'installation
1. **Téléchargement :** Se rendre sur [visualstudio.microsoft.com/fr](https://visualstudio.microsoft.com/fr) pour choisir sa version.
2. **Sélection des charges de travail :** Pour le développement C# (Windows Forms ou WPF), il est impératif de sélectionner **.NET Desktop Development**.
![.NET Desktop](/420-413/images/dot_net_desktop.png)


**Si vous disposez déjà de Visual Studio**, vous pouvez ajouter la charge de travail de développement de bureau .NET comme suit :

* Lancez Visual Studio Installer.
* Si vous y êtes invité, autorisez le programme d’installation à se mettre à jour.
* Si une mise à jour pour Visual Studio est disponible, un bouton Mettre à jour s’affiche. Sélectionnez-la pour la mettre à jour avant de modifier l’installation.
* Recherchez votre installation de Visual Studio et sélectionnez le bouton Modifier .
* S’il n’est pas déjà sélectionné, sélectionnez la charge de travail de développement du bureau .NET , puis sélectionnez le bouton Modifier . Sinon, fermez simplement la fenêtre de dialogue.


### Personnalisation
L'interface est entièrement personnalisable : thèmes, raccourcis clavier et extensions. Pour un environnement complet, il est recommandé d'installer **Git** pour la gestion de versions.

> [!CAUTION]
> **Note sur l'IA :** Des outils comme *GitHub Copilot* sont disponibles, mais ils **ne doivent pas être utilisés** dans le cadre de ce cours afin de ne pas nuire à l'apprentissage des concepts fondamentaux.

---

## 3. Fonctionnalités principales

![Cycle de développement](https://learn.microsoft.com/fr-fr/visualstudio/get-started/media/visual-studio-overview.png?view=vs-2022)
*Source : https://learn.microsoft.com/fr-fr/visualstudio/get-started/media/visual-studio-overview.png?view=vs-2022*

### L’éditeur de texte

L’éditeur de texte de Visual Studio est un puissant outil permettant de saisir le code de l’application.

**Voici quelques fonctionnalités de l’éditeur de texte:**
* Les mots-clés et les types sont colorés pour faciliter la lecture et la compréhension du code.
* La qualité du document en cours de visualisation est annoncée grâce à l’icône en bas du document pour indiquer s’il contient des suggestions, des avertissements ou des erreurs.
* **L’intellisense** permet d’afficher les classes et leurs membres en rapport avec le code saisi ainsi que les paramètres et les surcharges possibles pour les méthodes
* **L’intellicode** vous permet d’assurer la précision et la cohérence de l’exécution du code qui peut remplir une ligne entière à la fois. L’IA détecte votre contexte de code, notamment les noms de variables, les fonctions et le type de code que vous écrivez, pour vous donner les meilleures suggestions. Encore mieux : IntelliCode s’exécute sur votre ordinateur, ce qui garantit que votre code privé reste privé.

### Autres outils de Visual Studio Code 2022
* **Gestion de builds :** Création de configurations adaptées aux environnements de développement ou de production.
* **Débogage et tests :** Débogueur intégré pour l'exécution pas à pas et outils de tests unitaires (NUnit, xUnit). 
Le mode débogage peut être lancé en allant sur Déboguer -> Démarrer le débogage ou juste F5

* **Collaboration :** Intégration native de Git/GitHub et support de **Live Share** pour le développement collaboratif en temps réel.

---

## 4. Exploration de l'interface

Lors du lancement, la **fenêtre de démarrage** propose quatre options principales:
1. **Clone a repository :** Récupérer du code depuis GitHub ou Azure DevOps.
2. **Open a project or solution :** Ouvrir un fichier projet local.
3. **Open a local folder :** Naviguer et éditer du code dans n'importe quel dossier.
4. **Create a new project :** Commencer à partir d'un modèle prédéfini.
`
![Écran de démarrage](/420-413/images/demarrage.png)

---

## 5. Projets et Solutions

### Créer votre solution et projet
* 1. Démarrez Visual Studio et sélectionnez Créer un projet.

![Créer nouveau projet](/420-413/images/nouveau_projet.png)
 
* 2. Dans la fenêtre **Créer un projet**, recherchez et sélectionnez un modèle d’application console C#, puis sélectionnez Suivant..
* 3. Configurez votre projet (nom, emplacement). À cette étape, vous pouvez décidez de placer la solution **.sln** et le projet **.csproj**. Puis sélectionnez Suivant.
* 4. Dans la fenêtre Informations supplémentaires , vérifiez que .NET 8.0 apparaît dans le menu déroulant Framework, puis sélectionnez Créer.

### Différences fondamentales entre projet et solution
* Un projet est un ensemble de fichiers qui seront compilés en un seul assemblage.
* Une solution est un ensemble d’un ou plusieurs projets.
Un projet d’application exécutable possède un point d’entrée. C’est la méthode **Main**. Cette méthode doit être publique et statique en utilisant les mots-clés **public** et **static**, qui spécifient respectivement que la méthode est accessible depuis l’application et en dehors, que la méthode est globale et que la classe n’a pas besoin d’être instanciée pour pouvoir l’appeler.
* Une solution possède aussi un projet de démarrage. Ce projet est identifiable dans l’explorateur de solutions, car son nom est en gras. Pour modifier cette propriété, un clic droit sur le projet permet de sélectionner Définir en tant que projet de démarrage dans le menu contextuel.


##### Le point d'entrée (Méthode Main)
Un projet exécutable possède une méthode `Main`. Elle doit être définie comme suit:
* `public` : Accessible depuis l'application et l'extérieur.
* `static` : La méthode est globale et la classe n'a pas besoin d'être instanciée pour l'appeler.

**Projet de démarrage :** Dans une solution, le projet de démarrage apparaît en **gras** dans l'explorateur de solutions. On peut le modifier via un clic droit -> *Définir en tant que projet de démarrage*.

---

## 6. Exécution et Débogage

* **Lancement :** Cliquez sur la flèche verte ou appuyez sur **F5**.
![Exécution](/420-413/images/demarrer_app.png)

* **Points d'arrêt (Breakpoints) :** Permettent d'interrompre l'exécution pour examiner l'état des variables à des endroits précis.


##### Les différents fichiers compris dans la solution

* Le fichier  `program.cs ` qui contient le code qui est compilé et exécuté
* Quand on fait un clic droit sur le projet, ensuite on choisit **"Ouvrir le dossier dans l'Explorateur de fichiers"**, on peut voir le fichier du projet dont l'extension est **csproj**.
* Un niveau plus haut, on voit le fichier de la solution dont l'extension est sln.
* Plus bas, dans le dossier bin -> Debug -> net8.0, on peut voir le fichier exécutable .exe qui est l'application qui vient d'être créée.
* Si on double-clique sur l'exécutable, on ouvre l'application.
On peut aussi ouvrir la ligne de commande cmd et exécuter l'application en tapant le nom de l'exécutable.


---

### Ressources supplémentaires
* [Documentation Visual Studio](https://learn.microsoft.com/fr-fr/visualstudio/)
* [Documentation IntelliCode](https://learn.microsoft.com/fr-fr/visualstudio/intellicode/)