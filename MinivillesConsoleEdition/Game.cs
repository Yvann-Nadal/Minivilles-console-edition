using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinivillesConsoleEdition
{
    public class Game
    {
        /// <summary>
        /// Liste des joueurs de la partie
        /// </summary>
        public static List<Player> Players = new List<Player>();
        /// <summary>
        /// La pile contenant toutes les cartes du jeu initialement
        /// </summary>
        public static Piles pile = new Piles();
        /// <summary>
        /// L'id du joueur actuel
        /// </summary>
        private int playerTurn;
        /// <summary>
        /// Dictionnaire attribuant un nombre de pièces à obtenir selon le mode de jeu
        /// </summary>
        private Dictionary<string, int> gamemodeEnd = new Dictionary<string, int>()
        {
            {"rapide", 10},{"standard",20},{"longue",30},{"expert",30}
        };
        /// <summary>
        /// Définit si la partie est terminée
        /// </summary>
        private bool isEnd = false;
        /// <summary>
        /// Le mode de jeu actuel
        /// </summary>
        private readonly string gamemode;

        /// <summary>
        /// Initialise une partie
        /// </summary>
        /// <param name="players">Liste des noms de chaque joueur</param>
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
            ProcessGame();
        }

        /// <summary>
        /// Permet à l'utilisateur de choisir un mode de jeu
        /// </summary>
        /// <returns>Le mode de jeu choisit</returns>
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

        /// <summary>
        /// Boucle de tour de jeu tant que la partie n'est pas terminée
        /// </summary>
        private void ProcessGame()
        {
            while (!isEnd)
            {
                // Affiche un état global du jeu avec le nombre de pièces de chaque joueur
                Console.Clear();
                Console.WriteLine("État du jeu : ");
                foreach (Player player in Players)
                    Console.WriteLine($"{player.Name} a {pile.CoinText(player.CoinCount)}");

                // Procède au roulement du ou des dés
                int diceResult = Players[playerTurn].RollDice();
                Console.WriteLine($"{Players[playerTurn].Name} a fait {diceResult}\n");
                Thread.Sleep(500);

                // Active les potentiels effets de chaque carte de chaque joueur
                foreach (Player player in Players)
                    player.CheckCardEffects(diceResult, playerTurn);
                Thread.Sleep(500);

                // On regarde si la partie prend fin
                if (IsEnding())
                    isEnd = true;

                // Sinon, on lance la phase d'achat et on passe au joueur suivant
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

        /// <summary>
        /// Vérifie si les conditions de fin de partie sont atteintes par un joueur ou plus
        /// </summary>
        /// <returns>True si les conditions sont atteintes</returns>
        private bool IsEnding()
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
