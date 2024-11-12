namespace MinivillesConsoleEdition
{
    public class Card
    {
        public readonly string Name;
        public readonly string Color;
        public readonly string EffectDesc;
        public readonly int Price;
        public readonly int Income;
        public readonly int[] ActivationNum;

        public Card(string name, string color, string desc, int price, int income, int[] actNum)
        {
            Name = name;
            Color = color;
            EffectDesc = desc;
            Price = price;
            Income = income;
            ActivationNum = actNum;
        }

        public int Effect()
        {
            return Income;
        }

        public override string ToString()
        {
            string actNum = "";
            if (ActivationNum.Length == 1)
                actNum = ActivationNum[0].ToString();
            else
            {
                foreach (int i in ActivationNum)
                    actNum += $"{i}/";
                actNum.Remove(actNum.Length - 1, 1);
            }
            return $"Card : {Name} ({Color} - {actNum})\n\"{EffectDesc}\"\nCost {Price} and earns {Income}";
        }
    }
}