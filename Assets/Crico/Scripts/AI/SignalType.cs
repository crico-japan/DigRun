namespace Crico.AI
{
    [System.Flags]
    public enum SignalType
    {
        NONE = 0,
        DUMMY = 1 << 0,
        NEAR_ENEMY = 1 << 1,
        NEAR_PLAYER = 1 << 2,
        LAST_SIGNAL = 1 << 3,
        NUM_SIGNALS = 4
    }

}
