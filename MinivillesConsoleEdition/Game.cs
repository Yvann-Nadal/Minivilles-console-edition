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
        private readonly string gamemode = "standard";

        public Game(string[] players)
        {
            for(int id=0; id<players.Length; id++)
            {
                if (players[id] == "AI")
                    Players.Add(new AI(id));
                else
                    Players.Add(new Player(players[id], id));
            }
            //foreach
            ProcessGame();
        }

        private void ProcessGame()
        {
            while(!isEnd)
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
            foreach(Player p in Players)
                if (p.CoinCount >= gamemodeEnd[gamemode])
                {
                    Console.WriteLine($"{p.Name} a gagné !");
                    end = true;
                }
            return end;
        }
    }
}
