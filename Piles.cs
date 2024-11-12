using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinivillesConsoleEdition
{
    internal class Piles
    {
        public Dictionary<Card, int> DrawPile = new Dictionary<Card, int>()
        {
            {new Card("Champs de blé","Bleu","Recevez 1 pièce",1,1,[1]), 6},
            {new Card("Ferme","Bleu","Recevez 1 pièce",2,1,[1]),6 },
            {new Card("Boulangerie","Vert","Recevez 2 pièces",1,2,[2]),6 },
            {new Card("Café","Rouge","Recevez 1 pièce du joueur qui a lancé le dé",2,1,[3]),6 },
            {new Card("Superette","Vert","Recevez 3 pièces",2,3,[4]),6 },
            {new Card("Forêt","Bleu","Recevez 1 pièce",2,1,[5]),6 },
            {new Card("Restaurant","Rouge","Recevez 2 pièces du joueur qui a lancé le dé",4,2,[5]),6 },
            {new Card("Stade","Bleu","Recevez 4 pièces",6,4,[6]),6 }

        };

        public Card PickCard(Card wantedCard)
        {
            DrawPile[wantedCard]--;
            return wantedCard;
        }

        public bool HasCard(Card wantedCard)
        {
            if (DrawPile.ContainsKey(wantedCard) && DrawPile[wantedCard] > 0)
                return true;
            else
                return false;
        }
    }
}
