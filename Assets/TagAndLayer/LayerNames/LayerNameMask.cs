/// <summary>
/// レイヤーマスク名を定数で管理するクラス
/// </summary>
public static class LayerNameMask
{
	public const int DefaultMask = 1;
	public const int TransparentFXMask = 2;
	public const int IgnoreRaycastMask = 4;
	public const int WaterMask = 16;
	public const int UIMask = 32;
	public const int FragmentMask = 64;
	public const int CharacterMask = 128;
	public const int GroundMask = 256;
	public const int GoalMask = 512;
	public const int MiniMapIconMask = 1024;
	public const int RockMask = 2048;
}
