# Minivilles Console Edition

Minivilles est un jeu de gestion de ressources où les joueurs doivent acheter des bâtiments pour accumuler des pièces et atteindre un certain objectif. Ce projet est une version simplifiée et jouée en console du jeu **Minivilles**.

## Prérequis

Avant de pouvoir jouer à Minivilles, assurez-vous d'avoir les éléments suivants :

- **.NET 7.0 ou supérieur** installé sur votre machine.
- Un éditeur de texte ou un IDE compatible avec C# (Visual Studio, Visual Studio Code, etc.).

## Installation

1. Clonez ce dépôt ou téléchargez le code source de la manière suivante :
    ```bash
    git clone https://github.com/Yvann-Nadal/Minivilles-console-edition.git
    ```
2. Ouvrez le projet dans votre éditeur ou IDE préféré.
3. Assurez-vous que le projet cible .NET 7.0 ou supérieur.

## Comment jouer

### Objectif du jeu

Le but du jeu est d'accumuler un certain nombre de **pièces** et de remplir des conditions spécifiques pour remporter la partie. Selon le mode de jeu choisi, les règles de victoire peuvent varier.

### Modes de jeu disponibles

- **Rapide** : Le premier joueur à atteindre 10 pièces gagne.
- **Standard** : Le premier joueur à atteindre 20 pièces gagne.
- **Longue** : Le premier joueur à atteindre 30 pièces gagne.
- **Expert** : Le premier joueur à atteindre 30 pièces ET posséder un exemplaire de chaque carte gagne.

### Règles du jeu

1. **Lancer les dés :** Chaque joueur lance des dés à chaque tour pour déterminer ses actions. Le résultat des dés détermine les effets des cartes et les actions possibles.
   
2. **Cartes :** Chaque joueur peut acheter des cartes pour augmenter ses revenus et obtenir des avantages. Les cartes coûtent des pièces et génèrent des revenus selon les résultats des dés. Certaines cartes affectent également les autres joueurs.
   
3. **Phase d'achat :** Après avoir lancé les dés, chaque joueur peut acheter des cartes disponibles dans le magasin. Si un joueur a assez de pièces, il peut acheter autant de cartes qu'il veut.
   
4. **Transactions :** Certaines cartes permettent à un joueur de voler des pièces à d'autres joueurs ou de recevoir des pièces en fonction du résultat des dés.
   
5. **Fin de la partie :** La partie se termine lorsque l'un des joueurs remplit les conditions de victoire définies par le mode de jeu sélectionné.

### Commandes

1. **Lancer les dés** : Vous serez invité à choisir combien de dés vous voulez lancer (1 ou 2).
   
2. **Acheter des cartes** : Une fois la phase de lancement des dés terminée, vous pourrez acheter des cartes. Pour cela, tapez le numéro de la carte ou son nom. Tapez `"Rien"` ou `"0"` pour ne rien acheter.
   
3. **Indications à l'écran** : Le jeu vous guidera tout au long de votre tour. Vous verrez des informations sur les cartes disponibles, vos pièces et celles de vos adversaires.
