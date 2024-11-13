namespace MinivillesConsoleEdition
{
    public class Card
    {
        public CardsInfo Data;

        public Card(string name, ConsoleColor color, string desc, int price, int income, int[] actNum)
        {
            Data = new CardsInfo(name, color, desc, price, income, actNum);
        }

        public void ShowData(int id, int own, int amount, bool affordable)
        {
            string actNum = "";
            for(int i=0; i < Data.Dice.Length; i++)
                actNum += $"{Data.Dice[i]}" + (i < Data.Dice.Length-1 ? " ou " : "");

            Console.ForegroundColor = Data.Color;
            Console.Write($"[{id}] - \"{Data.Name}\"  Possédés : {own}  Disponible : {amount}\n{Data.Effect}\nS'active quand le total des dés est {actNum}\n" +
                $"Prix : {Game.pile.CoinText(Data.Cost)}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (!affordable) { Console.Write(" (Trop cher !)"); }
            Console.WriteLine();
        }
    }

    public struct CardsInfo
    {
        public readonly ConsoleColor Color;
        public readonly int Cost;
        public readonly string Name;
        public readonly string Effect;
        public readonly int[] Dice;
        public readonly int Gain;
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
