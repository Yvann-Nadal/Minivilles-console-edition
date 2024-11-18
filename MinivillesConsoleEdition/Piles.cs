using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinivillesConsoleEdition
{
    /// <summary>
    /// Classe représentant la pioche du jeu
    /// </summary>
    public class Piles
    {
        /// <summary>
        /// Associe un type de cartes et son nombre d'exemplaires dans le jeu
        /// </summary>
        public Dictionary<Card, int> DrawPile;

        public Piles()
        {
            DrawPile = new Dictionary<Card, int>()
            {
                {new Card("Champs de blé",ConsoleColor.Blue,"Recevez 1 pièce",1,1,[1]),6},
                {new Card("Ferme",ConsoleColor.Blue,"Recevez 1 pièce",2,1,[1]),6},
                {new Card("Boulangerie",ConsoleColor.Green,"Recevez 2 pièces",1,2,[2,3]),6},
                {new Card("Café",ConsoleColor.Red,"Recevez 1 pièce du joueur qui a lancé les dés",2,1,[3]),6},
                {new Card("Superette",ConsoleColor.Green,"Recevez 3 pièces",2,3,[4]),6},
                {new Card("Forêt",ConsoleColor.Blue,"Recevez 1 pièce",2,1,[5]),6},
                {new Card("Restaurant",ConsoleColor.Red,"Recevez 2 pièces du joueur qui a lancé les dés",4,2,[5]),6},
                {new Card("Stade",ConsoleColor.Blue,"Recevez 4 pièces",6,4,[6]),6},
                {new Card("Tour",ConsoleColor.Green,"Recevez 3 pièces",6,3,[6,7]),6},
                {new Card("Atelier",ConsoleColor.Green,"Recevez 4 pièces",7,4,[7]),6},
                {new Card("Bazar",ConsoleColor.Red,"Recevez 3 pièces du joueur qui a lancé les dés",7,3,[8]),6},
                {new Card("Beffroi",ConsoleColor.Blue,"Recevez 5 pièces",7,5,[8,9]),6},
                {new Card("Forge",ConsoleColor.Red,"Recevez 4 pièces du joueur qui a lancé les dés",8,4,[9]),6},
                {new Card("Moulin",ConsoleColor.Blue,"Recevez 6 pièces",8,6,[9]),6},
                {new Card("Rotisserie",ConsoleColor.Green,"Recevez 6 pièces",8,6,[10]),6},
                {new Card("Laboratoire",ConsoleColor.Green,"Recevez 6 pièces",9,6,[10,11]),6},
                {new Card("Cinéma",ConsoleColor.Red,"Recevez 6 pièces du joueur qui a lancé les dés",9,6,[11]),6},
                {new Card("Bureau",ConsoleColor.Green,"Recevez 8 pièces",10,8,[11]),6},
                {new Card("Le NIL",ConsoleColor.Green,"Recevez 10 pièces",10,10,[12]),6}
            };
        }

        /// <summary>
        /// Enlève la carte achetée de la pioche
        /// </summary>
        /// <param name="wantedCard"></param>
        /// <returns>La carte achetée</returns>
        public Card PickCard(string wantedCard)
        {
            wantedCard = CorrectCardName(wantedCard);
            foreach (Card card in DrawPile.Keys)
                if (card.Data.Name == wantedCard)
                {
                    DrawPile[card]--;
                    return card;
                }
            return null;
        }

        /// <summary>
        /// Vérifie si le joueur peut bien acheter la carte
        /// </summary>
        /// <param name="wantedCard">La carte demandée</param>
        /// <param name="playerCoins">Le nombre de pièces du demandeur</param>
        /// <returns>True si le joueur peut acheter la carte qu'il demande</returns>
        public bool HasCard(string wantedCard, int playerCoins)
        {
            if (wantedCard.ToLower() == "rien") // Si le joueur demande aucune carte
                return false;

            wantedCard = CorrectCardName(wantedCard);
            if (DrawPile.Any(card => card.Key.Data.Name == wantedCard)) // Si la pioche contient bien la carte
            {
                Card card = DrawPile.First(card=>card.Key.Data.Name == wantedCard).Key;
                if (DrawPile[card] > 0) // Si il reste des exemplaires de cette carte
                    if (playerCoins >= card.Data.Cost) // Si le joueur a assez de pièces pour l'acheter
                    {
                        return true;
                    }
                    else
                        Console.WriteLine("Vous n'avez pas assez de pièces pour acheter cette carte");
                else
                    Console.WriteLine($"\"{wantedCard}\" n'est plus disponible.");

            }
            else
                Console.WriteLine("La carte demandée n'existe pas.");

            return false;
        }

        /// <summary>
        /// Aide automatique à l'écriture du nom d'une carte
        /// </summary>
        /// <param name="askCard">Le nom de la carte écrit par le joueur</param>
        /// <returns>Le nom de la carte avec une majuscule au début</returns>
        private string CorrectCardName(string askCard)
        {
            if (askCard.Length > 0)
                return char.ToUpper(askCard[0]) + askCard.Substring(1);
            else
                return askCard;
        }

        /// <summary>
        /// Affiche le magasin dans la console
        /// </summary>
        /// <param name="client">Le joueur qui consulte le magasin</param>
        /// <returns>True si le joueur peut au moins acheter une carte</returns>
        public bool DisplayShop(Player client)
        {
            bool canPurchase = false;
            // Rappel du nombre de pièces du joueur
            Console.Write($"{client.Name} a ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(CoinText(client.CoinCount));

            // Parcours de chaque type de carte du jeu
            for(int i =0; i < DrawPile.Keys.Count; i++)
            {
                Card c = DrawPile.Keys.ToArray()[i];
                Thread.Sleep(200);
                if (CardsPurchasable(client.CoinCount).Contains(c)) // Si le joueur peut acheter la carte
                {
                    c.ShowData(i+1,client.GetCardCount(c), DrawPile[c],true);
                    canPurchase = true;
                }
                else
                {
                    c.ShowData(i+1,client.GetCardCount(c), DrawPile[c],false);
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            return canPurchase;
        }

        /// <summary>
        /// Renvoie une liste des cartes achetables par le joueur
        /// </summary>
        /// <param name="playerCoins">Nombre de pièces du joueur</param>
        /// <returns></returns>
        public List<Card> CardsPurchasable(int playerCoins)
        {
            List<Card> cards = new List<Card>();
            foreach (Card c in DrawPile.Keys)
                if (DrawPile[c] > 0 && playerCoins >= c.Data.Cost)
                    cards.Add(c);
            return cards;
        }

        /// <summary>
        /// Permet d'écrire "X pièce" en prenant soin de mettre pièce au pluriel s'il le faut
        /// </summary>
        /// <param name="count">Le nombre de pièces</param>
        /// <param name="withNumber">Faut il renvoyer juste pièces ou aussi le nombre</param>
        /// <returns>Un string de la forme "X pièce(s)"</returns>
        public string CoinText(int count)
        {
            return $"{count} pièce{(count > 1 ? "s" : "")}";
        }
    }
}
