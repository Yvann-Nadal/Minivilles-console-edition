using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinivillesConsoleEdition
{
    internal class Card
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

        public void Effect()
        {

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
