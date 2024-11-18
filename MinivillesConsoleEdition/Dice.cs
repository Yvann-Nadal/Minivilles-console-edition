namespace MinivillesConsoleEdition
{
    /// <summary>
    /// Classe manipulant plusieurs dés
    /// </summary>
    public class Dice
    {
        Random rand = new Random();
        /// <summary>
        /// Nombre de faces d'un dé
        /// </summary>
        private int faceCount;
        /// <summary>
        /// Résultat de ou des dés
        /// </summary>
        private int[] face;
        /// <summary>
        /// Initialise le nombre de faces d'un dé
        /// </summary>
        /// <param name="faces">Nombre de faces</param>
        public Dice(int faces = 6)
        {
            faceCount = faces;
        }

        /// <summary>
        /// Permet de lancer un ou plusieurs dés
        /// </summary>
        /// <param name="diceCount">Le nombre de dés à lancer</param>
        /// <returns>La somme des faces des dés</returns>
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