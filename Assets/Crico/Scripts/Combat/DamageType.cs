namespace Crico.Combat
{
    [System.Flags]
    public enum DamageType
    {
        NONE = 0,
        PLAYER_MELEE = 1 << 0,
        PLAYER_RANGED = 1 << 1,
        ENEMY_MELEE = 1 << 2,
        ENEMY_RANGED = 1 << 3,
    }

}
