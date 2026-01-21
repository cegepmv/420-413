+++
date = '2025-11-13T08:33:04-05:00'
title = 'Gestion des Entrées'
+++
---

Quand tu lis des données avec `Scanner`, l’utilisateur peut **taper n’importe quoi** 😅
→ Tu dois donc **vérifier le type de donnée** avant de la lire.

C’est ici que les méthodes **`hasNext...()`** entrent en jeu.

---

## 🕵️ Les méthodes de vérification

| Méthode            | Vérifie si…                              | Exemple de lecture |
| ------------------ | ---------------------------------------- | ------------------ |
| `hasNextInt()`     | la prochaine donnée est un **int** ?     | `42`               |
| `hasNextDouble()`  | la prochaine donnée est un **double** ?  | `3.14`             |
| `hasNextBoolean()` | la prochaine donnée est un **booléen** ? | `true` / `false`   |
| `hasNext()`        | il reste **du texte** à lire ?           | n’importe quoi     |

---

## ⚡ Exemple : validation d’un entier

Voici un exemple de programme qui **demande un entier** et **vérifie la saisie** :

```java
import java.util.Scanner;

public class LectureSecurisee {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);

        System.out.print("Entre un nombre entier : ");

        while (!sc.hasNextInt()) {
            System.out.println("❌ Ce n’est pas un entier !");
            System.out.print("Essaie encore : ");
            sc.next(); // on ignore la mauvaise entrée
        }

        int nombre = sc.nextInt();
        System.out.println("✅ Merci ! Tu as entré : " + nombre);

        sc.close();
    }
}
```

🧠 **Explication :**

* `hasNextInt()` vérifie la prochaine donnée
* Tant que ce n’est **pas** un entier → on affiche un message d’erreur
* `sc.next()` sert à **vider** l’entrée incorrecte avant de recommencer

---

## 🧮 Exemple avec un double

```java
System.out.print("Entre un nombre décimal : ");

while (!sc.hasNextDouble()) {
    System.out.println("❌ Ce n’est pas un nombre décimal !");
    System.out.print("Essaie encore : ");
    sc.next();
}

double valeur = sc.nextDouble();
System.out.println("✅ Tu as entré : " + valeur);
```

---

## 🧠 Astuce : regrouper la validation dans une fonction

Pour éviter de répéter le même code, tu peux créer une **méthode utilitaire** :

```java
public static int lireEntier(Scanner sc) {
    while (!sc.hasNextInt()) {
        System.out.println("❌ Erreur : entre un entier valide !");
        sc.next();
    }
    return sc.nextInt();
}
```

Et l’utiliser simplement :

```java
System.out.print("Âge : ");
int age = lireEntier(sc);
```

---

## 🧪 Exercice 1 : âge valide

**Objectif :** Lire un âge (entier positif).

🧾 **Consignes :**

* Utilise `hasNextInt()` pour t’assurer que c’est un entier.
* Si ce n’est pas un entier, redemande.
* Si l’âge est négatif, redemande aussi.
* Affiche : `"Tu as X ans."`

---

## 🎯 Exercice 2 : note sur 100

**Objectif :** Lire une note décimale entre 0 et 100.

🧾 **Consignes :**

* Utilise `hasNextDouble()`
* Vérifie que la note est entre 0 et 100
* Si la saisie est invalide, redemande

---

## 📦 À retenir

* `hasNext...()` permet de **vérifier le type de la donnée suivante**
* Toujours **vider** l’entrée incorrecte avec `next()`
* Combine avec une **boucle `while`** pour redemander une saisie valide
* 🔒 Rend ton programme **plus robuste** et **plus convivial**

---
