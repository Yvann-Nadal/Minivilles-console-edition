namespace MinivillesConsoleEdition
{
    /// <summary>
    /// Classe représentant une carte du jeu
    /// </summary>
    public class Card
    {
        /// <summary>
        /// La struct de données liée à la carte
        /// </summary>
        public CardsInfo Data;

        /// <summary>
        /// Initialise la carte et ses données
        /// </summary>
        /// <param name="name">Nom</param>
        /// <param name="color">Couleur</param>
        /// <param name="desc">Effet description</param>
        /// <param name="price">Prix</param>
        /// <param name="income">Gain</param>
        /// <param name="actNum">Numéro d'activation du dé</param>
        public Card(string name, ConsoleColor color, string desc, int price, int income, int[] actNum)
        {
            Data = new CardsInfo(name, color, desc, price, income, actNum);
        }

        /// <summary>
        /// Permet d'afficher les attributs de la carte et si elle est disponible à l'achat
        /// </summary>
        /// <param name="id">Id de la carte (pour permettre au joueur de l'acheter sans écrire son nom)</param>
        /// <param name="own">Nombre d'exemplaires possédés par le joueur</param>
        /// <param name="amount">Nombre d'exemplaires restant dans le magasin</param>
        /// <param name="canBuy">Est-ce que le joueur peut l'acheter</param>
        public void ShowData(int id, int own, int amount, bool canBuy)
        {
            string actNum = "";
            for(int i=0; i < Data.Dice.Length; i++)
                actNum += $"{Data.Dice[i]}" + (i < Data.Dice.Length-1 ? " ou " : "");

            Console.ForegroundColor = Data.Color;
            Console.Write($"[{id}] - \"{Data.Name}\"  Possédés : {own}  {(amount==0 ? "Indisponible" : $"Disponible : {amount}")}\n{Data.Effect}\nS'active quand le total des dés est {actNum}\n" +
                $"Prix : {Game.Pile.CoinText(Data.Cost)}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (!canBuy) { Console.Write(" (Trop cher ou indisponible !)"); }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Struct regroupant les données d'une carte
    /// </summary>
    public struct CardsInfo
    {
        /// <summary>
        /// Couleur
        /// </summary>
        public readonly ConsoleColor Color;
        /// <summary>
        /// Prix
        /// </summary>
        public readonly int Cost;
        /// <summary>
        /// Nom
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// Description de la carte
        /// </summary>
        public readonly string Effect;
        /// <summary>
        /// Numéro d'activation du dé
        /// </summary>
        public readonly int[] Dice;
        /// <summary>
        /// Gain
        /// </summary>
        public readonly int Gain;
        /// <summary>
        /// Initialise les données de la struct
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="desc"></param>
        /// <param name="price"></param>
        /// <param name="income"></param>
        /// <param name="actNum"></param>
        public CardsInfo(string name, ConsoleColor color, string desc, int price, int income, int[] actNum)
        {
            Name = name;
            Color = color;
            Effect = desc;
            Cost = price;
            Gain = income;
            Dice = actNum;
        }
    }
}
