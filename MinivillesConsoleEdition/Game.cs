using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinivillesConsoleEdition
{
    public class Game
    {
        public static List<Player> Players = new List<Player>();
        public static Piles pile = new Piles();
        private int playerTurn;
        private Dictionary<string, int> gamemodeEnd = new Dictionary<string, int>()
        {
            {"rapide", 10},{"standard",20},{"longue",30},{"expert",30}
        };
        private bool isEnd = false;
        private readonly string gamemode;

        public Game(string[] players)
        {
            gamemode = ChooseGamemode();
            for (int id = 0; id < players.Length; id++)
            {
                if (players[id] == "AI")
                    Players.Add(new AI(id));
                else
                    Players.Add(new Player(players[id], id));
            }
            //foreach
            ProcessGame();
        }

        private string ChooseGamemode()
        {
            Console.WriteLine("Choisissez un mode de jeu :");
            Console.WriteLine("1. Rapide");
            Console.WriteLine("2. Standard");
            Console.WriteLine("3. Longue");
            Console.WriteLine("4. Expert");
            Console.Write("Votre choix (1-4) : ");
            string choice = Console.ReadLine();

            return choice switch
            {
                "1" => "rapide",
                "2" => "standard",
                "3" => "longue",
                "4" => "expert",
                _ => "standard"
            };
        }

        private void ProcessGame()
        {
            while (!isEnd)
            {
                Console.Clear();
                Console.WriteLine("État du jeu : ");
                foreach (Player player in Players)
                    Console.WriteLine($"{player.Name} a {pile.CoinText(player.CoinCount)}");
                int diceResult = Players[playerTurn].RollDice();
                Console.WriteLine($"{Players[playerTurn].Name} a fait {diceResult}\n");
                Thread.Sleep(500);

                foreach (Player player in Players)
                    player.CheckCardEffects(diceResult, playerTurn);
                Thread.Sleep(500);

                if (isEnding())
                    isEnd = true;
                else
                {
                    Players[playerTurn].PurchasePhase();
                    playerTurn++;
                    if (playerTurn >= Players.Count)
                        playerTurn = 0;
                    Thread.Sleep(500);
                }

            }
        }

        private bool isEnding()
        {
            bool end = false;
            foreach (Player p in Players)
            {
                if (p.CoinCount >= gamemodeEnd[gamemode])
                {
                    if (gamemode == "expert")
                    {
                        // Vérifie si le joueur possède au moins une carte de chaque type
                        bool hasAllCards = Game.pile.DrawPile.Keys.All(card => p.GetCardCount(card) > 0);
                        if (hasAllCards)
                        {
                            Console.WriteLine($"{p.Name} a gagné ! (30 pièces et toutes les cartes)");
                            end = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{p.Name} a gagné !");
                        end = true;
                    }
                }
            }
            return end;
        }
    }
}
