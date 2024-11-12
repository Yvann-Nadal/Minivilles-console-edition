namespace MinivillesConsoleEdition
{
    public class Player
    {
        public List<Card> Deck { get; set; } = new List<Card>();
        public int CoinCount { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public bool IsAI { get; set; }

        public void Turn()
        {

        }

        public int RollDice(int diceCount)
        {
            Dice dice = new Dice();
            return dice.Roll(diceCount);
        }

        public void CheckCardEffects(int diceResult, int actualPlayer)
        {
            foreach (var card in Deck)
            {
                if (Array.Exists(card.ActivationNum, num => num == diceResult))
                {
                    CoinCount += card.Effect();
                }
            }
        }

        public void Purchase(Card card)
        {
            if (CoinCount >= card.Price)
            {
                CoinCount -= card.Price;
                Deck.Add(card);
            }
        }
    }
}