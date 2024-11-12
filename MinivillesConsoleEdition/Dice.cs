namespace MinivillesConsoleEdition
{
    internal class Dice
    {
        Random rand = new Random();
        private int faceCount;
        private int[] face;
        public Dice(int faces = 6)
        {
            faceCount = faces;
        }

        public int Roll(int diceCount)
        {
            face = new int[diceCount];
            int total = 0;
            for (int i = 0; i < diceCount; i++)
            {
                int result = rand.Next(faceCount) + 1;
                face[i] = result;
                total += result;
            }
            return total;
        }
    }
}