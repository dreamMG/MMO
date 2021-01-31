public static class DBManager
{
    public static string name;
    public static int lvl;

    public static int exp;

    public static int CON;
    public static int INE;
    public static int STR;
    public static int DEX;

    public static void InitDBManager(string namePlayer, int lvlPlayer, int expPlayer, int CONPlayer, int INEPlayer, int STRPlayer, int DEXPlayer)
    {
        name = namePlayer;
        lvl = lvlPlayer;
        exp = expPlayer;
        CON = CONPlayer;
        INE = INEPlayer;
        STR = STRPlayer;
        DEX = DEXPlayer;
    }
}
