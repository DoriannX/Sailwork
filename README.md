# Sailwork

## 🎯 Objectif

Sailwork est un projet Unity 6000.1.1f1 dont le but est d’implémenter un système simple où des matelots effectuent des tâches sur un bateau. Lorsqu’un matelot commence une tâche, une barre de progression apparaît et progresse au-dessus de lui. Une fois la tâche terminée, le matelot redevient disponible pour en commencer une autre.

## 📦 Structure du projet

L’organisation principale du projet se trouve dans `Assets/Project/` :

- **Art** : éléments graphiques
- **Audio** : ressources audio
- **Prefabs** : objets préfabriqués
- **Resources** : ressources Unity accessibles à l’exécution
- **Scenes** : scènes Unity du projet
- **Scripts** : scripts C# du projet
  - `Runtime` : scripts de logique de jeu (matelots, tâches, gestion d’état, etc.)

## 🏗️ Architecture logicielle

L’architecture s’appuie sur les principes suivants :

- **Pattern State** : Chaque matelot possède un système de gestion d’état (Disponible, En cours de tâche, En attente, Fatigué).
- **Barre de progression** : Lorsqu’une tâche est affectée à un matelot, une barre de progression s’affiche grâce à un composant Unity UI (World Space) au-dessus du personnage.
- **Configuration** : La durée des tâches est paramétrable.
- **Gestion de la fatigue** : Possibilité d’ajouter une fatigue temporaire au matelot après un certain nombre de tâches.

## ⚙️ Fonctionnalités principales

- Un ou plusieurs matelots, contrôlés par IA par clic utilisateur.
- Affectation de tâches : à chaque affectation, le matelot passe à l’état "En cours de tâche" et une barre de progression s’affiche.
- Retour à l’état "Disponible" quand il n'a plus de tache dans sa liste.
- Fatigue après X tâches, nécessitant du repos.

## 🛠️ Dépendances et plugins

Le projet utilise les packages/plugins Unity suivants :

- **Demigiant** (DOTween pour les animations et timelines)
- **TextMesh Pro** (texte avancé Unity)
- **SerializedCollections** (gestion de collections sérialisées)
- **Scalable Grid Prototype Materials** (matériaux pour grille/visualisation)

## 🚀 Installation & Utilisation

1. **Cloner le dépôt :**
   ```bash
   git clone https://github.com/DoriannX/Sailwork.git
   cd Sailwork
   ```
2. **Ouvrir avec Unity Hub** :
   - Sélectionner le dossier du projet dans Unity Hub.
   - Ouvre avec Unity 6000.1.1f1 ou version ultérieure.

3. **Configurer les scènes** :
   - Ouvrir la GameScene dans `Assets/Project/Scenes/`.

4. **Lancer le jeu** :
   - Appuie sur Play dans l’éditeur pour tester le fonctionnement des matelots et des tâches.

## ✏️ Personnalisation

- **Ajouter de nouveaux matelots ou tâches** :  
  Ajouter de nouvelles prefabs dans `Assets/Project/Prefabs/` et les relier aux scripts via l’inspecteur Unity.
- **Modifier la durée des tâches** :  
  Parametrer la durée directement dans l’éditeur via les composants associés aux tâches ou dans un script de configuration.

## 📚 Code et documentation

- **Scripts principaux** :  
  Les scripts C# sont organisés dans `Assets/Project/Scripts/Runtime/`.
- **Commentaires** :  
  Le code est propre et commenté pour faciliter la compréhension.

## 🤝 Contribuer

1. Fork le projet
2. Créer une branche (`feature/ma-feature`)
3. Commit et push les changements
4. Ouvrir une Pull Request

## 📖 Auteurs

- Réalisé par [DoriannX](https://github.com/DoriannX)

## 📝 Licence

Ce projet est sous licence MIT.

---

> Pour toute question ou suggestion, ouvrir une issue sur le dépôt !
