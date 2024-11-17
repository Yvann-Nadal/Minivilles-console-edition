namespace MinivillesConsoleEdition
{
    public class Player
    {
        public readonly string Name;
        public readonly int Id;
        public List<Card> Deck { get; set; } = new List<Card>();
        public int CoinCount { get; set; }
        protected Dice dice = new Dice();

        public Player(string name, int id, int coins = 3)
        {
            Name = name;
            Id = id;
            CoinCount = coins;
            Deck.Add(Game.pile.PickCard("Boulangerie"));
            Deck.Add(Game.pile.PickCard("Champs de blé"));
        }

        public virtual int RollDice()
        {
            int diceCount = 0;
            while (diceCount == 0)
            {
                Console.WriteLine($"{Name} : Combien de dé voulez vous lancer (1 ou 2)");
                diceCount = int.TryParse(Console.ReadLine(), out diceCount) ? diceCount : 0;
                if (diceCount < 1 || diceCount > 2)
                    diceCount = 0;
            }
            return dice.Roll(diceCount);
        }

        public void CheckCardEffects(int diceResult, int actualPlayer)
        {
            List<int> gains = new List<int>();
            List<(int, int)> payments = new List<(int, int)>();
            foreach (var card in Deck)
            {
                if (card.Data.Dice.Contains(diceResult))
                {
                    switch (card.Data.Color)
                    {
                        case ConsoleColor.Blue:
                            gains.Add(card.Data.Gain);
                            break;
                        case ConsoleColor.Green:
                            if(actualPlayer==Id)
                                gains.Add(card.Data.Gain);
                            break;
                        case ConsoleColor.Red:
                            if (actualPlayer != Id)
                                payments.Add((card.Data.Gain, actualPlayer));
                            break;
                    }
                }
            }
            ProcessTransactions(gains, payments);
        }

        private void ProcessTransactions(List<int> gains, List<(int,int)> payments)
        {
            foreach((int,int) p in payments)
            {
                int amount = Game.Players[p.Item2].CoinCount - p.Item1 < 0 ? Game.Players[p.Item2].CoinCount : p.Item1;
                Game.Players[p.Item2].CoinCount -= amount;
                CoinCount += amount;
                Console.WriteLine($"{Game.Players[p.Item2].Name} a donné {amount} pièce{(amount>1?"s":"")} à {Name}.");
                Thread.Sleep(400);

            }
            foreach (int g in gains)
            {
                CoinCount += g;
                Console.WriteLine($"{Name} gagne {g} pièce{(g > 1 ? "s" : "")}");
                Thread.Sleep(400);

            }
        }

        public int GetCardCount(Card card)
        {
            int count = 0;
            foreach(Card c in Deck)
                if(card == c)
                    count++;
            return count;
        }

        public virtual void PurchasePhase()
        {
            if(Game.pile.DisplayShop(this))
            {
                string wanted = "";
                do
                {
                    Console.WriteLine("Écrivez le numéro ou le nom du bâtiment que vous voulez acheter. Écrivez \"Rien\" ou \"0\" pour ne rien acheter.");
                    wanted = Console.ReadLine().TrimEnd();
                    if(int.TryParse(wanted, out int id) && id >= 0 && id <= Game.pile.DrawPile.Count)
                    {
                        if (id == 0)
                            wanted = "rien";
                        else
                            wanted = Game.pile.DrawPile.Keys.ToArray()[id-1].Data.Name;
                    }
                } while (!Game.pile.HasCard(wanted, CoinCount) && wanted.ToLower() != "rien");

                if (wanted.ToLower() != "rien")
                {
                    Card newCard = Game.pile.PickCard(wanted);
                    Deck.Add(newCard);
                    CoinCount -= newCard.Data.Cost;
                    Console.WriteLine($"{Name} achète la carte {newCard.Data.Name}");
                    Thread.Sleep(1000);
                }
            }
        }
    }

    public class AI : Player
    {
        Random rand = new Random();
        public AI(int id) : base("L'ordinateur",id)
        {
            
        }

        public override int RollDice()
        {
            return base.dice.Roll(rand.Next(1) + 1);
        }

        public override void PurchasePhase()
        {
            List<Card> cardsPurchasable = Game.pile.CardsPurchasable(CoinCount);
            if (cardsPurchasable.Count > 0)
            {
                Card newCard = Game.pile.PickCard(cardsPurchasable[rand.Next(cardsPurchasable.Count)].Data.Name);
                Deck.Add(newCard);
                CoinCount -= newCard.Data.Cost;
                Console.WriteLine($"{Name} achète {newCard.Data.Name}");
                Thread.Sleep(1000);
            }
        }
    }
}