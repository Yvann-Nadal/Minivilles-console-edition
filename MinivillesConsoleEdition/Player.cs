namespace MinivillesConsoleEdition
{
    /// <summary>
    /// Classe d'un joueur
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Le nom du joueur
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// L'id du joueur (qui définit si c'est le joueur actuel ou non)
        /// </summary>
        public readonly int Id;
        /// <summary>
        /// Liste de cartes qu'a le joueur
        /// </summary>
        public List<Card> Deck { get; set; } = new List<Card>();
        /// <summary>
        /// Nombre de pièces
        /// </summary>
        public int CoinCount { get; set; }
        /// <summary>
        /// Classe dé qui va permettre au joueur d'effectuer des lancés
        /// </summary>
        protected Dice dice = new Dice();

        /// <summary>
        /// Initialise un joueur
        /// </summary>
        /// <param name="name">Son nom</param>
        /// <param name="id">Son id</param>
        /// <param name="coins">Son nombre de pièces initial</param>
        public Player(string name, int id, int coins = 3)
        {
            Name = name;
            Id = id;
            CoinCount = coins;
            Deck.Add(Game.Pile.PickCard("Boulangerie"));
            Deck.Add(Game.Pile.PickCard("Champs de blé"));
        }

        /// <summary>
        /// Permet au joueur de lancer le(s) dé(s)
        /// </summary>
        /// <returns>Le résultat du/des dé(s)</returns>
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

        /// <summary>
        /// Active l'effet des cartes du joueur
        /// </summary>
        /// <param name="diceResult">Le résultat des dés</param>
        /// <param name="actualPlayer">L'id du joueur qui a lancé les dés</param>
        public void CheckCardEffects(int diceResult, int actualPlayer)
        {
            // Liste de tous les gains du joueur pendant le tour
            List<int> gains = new List<int>();
            // Liste de tous les paiements que va effectuer le joueur et à qui
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

        /// <summary>
        /// Effectue les gains et paiements du tour
        /// </summary>
        /// <param name="gains">Liste des gains</param>
        /// <param name="payments">Liste des paiements</param>
        private void ProcessTransactions(List<int> gains, List<(int,int)> payments)
        {
            foreach((int,int) p in payments)
            {
                // Le montant du paiement selon le nombre de pièces du donneur
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

        /// <summary>
        /// Retourne le nombre d'exemplaires d'une carte dans la main du joueur
        /// </summary>
        /// <param name="card">Le type de carte à compter</param>
        /// <returns>Le nombre d'exemplaires</returns>
        public int GetCardCount(Card card)
        {
            int count = 0;
            foreach(Card c in Deck)
                if(card == c)
                    count++;
            return count;
        }

        /// <summary>
        /// Permet au joueur d'acheter une carte
        /// </summary>
        public virtual void PurchasePhase()
        {
            if(Game.Pile.DisplayShop(this))
            {
                // Définit l'action du joueur
                string wanted = "";
                do
                {
                    Console.WriteLine("Écrivez le numéro ou le nom du bâtiment que vous voulez acheter. Écrivez \"Rien\" ou \"0\" pour ne rien acheter.");
                    wanted = Console.ReadLine().TrimEnd();
                    if(int.TryParse(wanted, out int id) && id >= 0 && id <= Game.Pile.DrawPile.Count)
                    {
                        if (id == 0)
                            wanted = "rien";
                        else
                            wanted = Game.Pile.DrawPile.Keys.ToArray()[id-1].Data.Name;
                    }
                } while (!Game.Pile.HasCard(wanted, CoinCount) && wanted.ToLower() != "rien");

                // Si le joueur a acheté une carte
                if (wanted.ToLower() != "rien")
                {
                    Card newCard = Game.Pile.PickCard(wanted);
                    Deck.Add(newCard);
                    CoinCount -= newCard.Data.Cost;
                    Console.WriteLine($"{Name} achète la carte {newCard.Data.Name}");
                    Thread.Sleep(1000);
                }
            }
        }
    }

    /// <summary>
    /// Classe de l'ordinateur
    /// </summary>
    public class AI : Player
    {
        Random rand = new Random();
        /// <summary>
        /// Initialise un joueur ordinateur
        /// </summary>
        /// <param name="id">L'id du joueur</param>
        public AI(int id) : base("L'ordinateur",id)
        {
            
        }

        /// <summary>
        /// Permet à l'ordinateur de lancer aléatoirement 1 ou 2 dé
        /// </summary>
        /// <returns></returns>
        public override int RollDice()
        {
            return base.dice.Roll(rand.Next(1,3));
        }

        /// <summary>
        /// Permet à l'ordinateur d'acheter une carte aléatoire
        /// </summary>
        public override void PurchasePhase()
        {
            List<Card> cardsPurchasable = Game.Pile.CardsPurchasable(CoinCount);
            if (cardsPurchasable.Count > 0 && rand.Next(1)==1) // L'ordinateur a une chance sur deux d'acheter une carte s'il le peut
            {
                Card newCard = Game.Pile.PickCard(cardsPurchasable[rand.Next(cardsPurchasable.Count)].Data.Name);
                Deck.Add(newCard);
                CoinCount -= newCard.Data.Cost;
                Console.WriteLine($"{Name} achète {newCard.Data.Name}");
                Thread.Sleep(1000);
            }
        }
    }
}