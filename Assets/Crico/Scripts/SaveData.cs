namespace Crico
{
    [System.Serializable]
    public class SaveData
    {
        public int stageIndex = 0;
        public int numCoins = 0;

        public override string ToString()
        {
            string desc = "SAVEDATA - Level Index: " + stageIndex + ","
            + "numCoins: " + numCoins;
            return desc;
        }

    }

}
