﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinivillesConsoleEdition
{
    public class Piles
    {
        private Dictionary<string, Card> cardName = new Dictionary<string, Card>()
        {
            {"Champs de blé",new Card("Champs de blé",ConsoleColor.Blue,"Recevez 1 pièce",1,1,[1])},
            {"Ferme",new Card("Ferme",ConsoleColor.Blue,"Recevez 1 pièce",2,1,[1])},
            {"Boulangerie",new Card("Boulangerie",ConsoleColor.Green,"Recevez 2 pièces",1,2,[2])},
            {"Café",new Card("Café",ConsoleColor.Red,"Recevez 1 pièce du joueur qui a lancé le dé",2,1,[3])},
            {"Superette",new Card("Superette",ConsoleColor.Green,"Recevez 3 pièces",2,3,[4])},
            {"Forêt",new Card("Forêt",ConsoleColor.Blue,"Recevez 1 pièce",2,1,[5])},
            {"Restaurant",new Card("Restaurant",ConsoleColor.Red,"Recevez 2 pièces du joueur qui a lancé le dé",4,2,[5])},
            {"Stade",new Card("Stade",ConsoleColor.Blue,"Recevez 4 pièces",6,4,[6])}
        };
        public Dictionary<Card, int> DrawPile;

        public Piles()
        {
            DrawPile = new Dictionary<Card, int>()
            {
                {cardName["Champs de blé"], 6},
                {cardName["Ferme"],6 },
                {cardName["Boulangerie"],6 },
                {cardName["Café"],6 },
                {cardName["Superette"],6 },
                {cardName["Forêt"],6 },
                {cardName["Restaurant"],6 },
                {cardName["Stade"],6 }
            };
        }

        public Card PickCard(string wantedCard)
        {
            wantedCard = CorrectCardName(wantedCard);
            DrawPile[cardName[wantedCard]]--;
            return cardName[wantedCard];
        }

        public bool HasCard(string wantedCard, int playerCoins)
        {
            if (wantedCard.ToLower() == "rien")
                return false;
            wantedCard = CorrectCardName(wantedCard);
            if (cardName.ContainsKey(wantedCard))
            {
                if (DrawPile[cardName[wantedCard]] > 0)
                    if (playerCoins >= cardName[wantedCard].Data.Cost)
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

        private string CorrectCardName(string cardName)
        {
            if (cardName.Length > 0)
                return char.ToUpper(cardName[0]) + cardName.Substring(1);
            else
                return cardName;
        }

        public bool DisplayShop(Player client)
        {
            bool canPurchase = false;
            Console.Write($"{client.Name} a ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(CoinText(client.CoinCount));
            for(int i =0; i < DrawPile.Keys.Count; i++)
            {
                Card c = DrawPile.Keys.ToArray()[i];
                Thread.Sleep(400);
                if (CardsPurchasable(client.CoinCount).Contains(c))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    c.ShowData(i+1,client.GetCardCount(c), DrawPile[c],true);
                    canPurchase = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    c.ShowData(i+1,client.GetCardCount(c), DrawPile[c],false);
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            return canPurchase;
        }

        public List<Card> CardsPurchasable(int playerCoins)
        {
            List<Card> cards = new List<Card>();
            foreach (Card c in DrawPile.Keys)
                if (DrawPile[c] > 0 && playerCoins >= c.Data.Cost)
                    cards.Add(c);
            return cards;
        }

        public string CoinText(int count, bool withNumber = true)
        {
            return $"{(withNumber?$"{count} " : "")}pièce{(count > 1 ? "s" : "")}";
        }
    }
}
