using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

/// <summary>
/// レイヤー名を定数で管理するクラスを作成するクラス
/// </summary>
public static class LayerNameCreater
{
    //無効な文字を管理する配列
    private static readonly string[] INVALUD_CHARS =
    {
        " ", "!", "\"", "#", "$",
        "%", "&", "\'", "(", ")",
        "-", "=", "^",  "~", "\\",
        "|", "[", "{",  "@", "`",
        "]", "}", ":",  "*", ";",
        "+", "/", "?",  ".", ">",
        ",", "<"
    };

    // コマンド名
    private const string ITEM_NAME = "Tools/Create/Layer Name";

    // ファイルパス
    private const string LAYER_NAME_PATH = "Assets/TagAndLayer/LayerNames/LayerName.cs";
    
    //ファイル名(拡張子あり)
    private static readonly string LAYER_FILE_NAME = Path.GetFileName(LAYER_NAME_PATH);

    //ファイル名(拡張子無し)
    private static readonly string LAYER_FILE_NAME_WITHOUT_EXTENSION = Path.GetFileNameWithoutExtension(LAYER_NAME_PATH);

    // ファイルパス
    private const string LAYERMASK_NAME_PATH = "Assets/TagAndLayer/LayerNames/LayerNameMask.cs";

    //ファイル名(拡張子あり)
    private static readonly string LAYERMASK_FILE_NAME = Path.GetFileName(LAYERMASK_NAME_PATH);

    //ファイル名(拡張子無し)
    private static readonly string LAYERMASK_FILE_NAME_WITHOUT_EXTENSION = Path.GetFileNameWithoutExtension(LAYERMASK_NAME_PATH);
    private static string[] layerNames = InternalEditorUtility.layers;
    /// <summary>
    /// レイヤー名を定数で管理するクラスを作成する
    /// </summary>
    [MenuItem(ITEM_NAME)]
    public static void Create()
    {
        if (!CanCreate())
        {
            return;
        }

        CreateScript();

        EditorUtility.DisplayDialog(LAYER_FILE_NAME+ LAYERMASK_FILE_NAME, "作成が完了しました", "OK");
    }

    /// <summary>
    /// スクリプトを作成
    /// </summary>
    public static void CreateScript()
    {
        layerNames = InternalEditorUtility.layers;
        CreateLayerNameClass();
        CreateLayerMaskNameClass();
        AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
    }

    private static void CreateLayerNameClass()
    {
        var builder = new StringBuilder();

        //レイヤー名用クラス
        builder.AppendLine("/// <summary>");
        builder.AppendLine("/// レイヤー名を定数で管理するクラス");
        builder.AppendLine("/// </summary>");
        builder.AppendFormat("public static class {0}", LAYER_FILE_NAME_WITHOUT_EXTENSION).AppendLine();
        builder.AppendLine("{");

        foreach (var n in layerNames.
            Select(c => new { var = Utility.RemoveInvalidChars(c), val = LayerMask.NameToLayer(c) }))
        {
            builder.Append("\t").AppendFormat(@"public const int {0} = {1};", n.var, n.val).AppendLine();
        }

        builder.AppendLine("}");

        var directoryName = Path.GetDirectoryName(LAYER_NAME_PATH);
        //if (!Directory.Exists(directoryName))
        //{
        //    Directory.CreateDirectory(directoryName);
        //}
        Directory.CreateDirectory(directoryName);
        File.WriteAllText(LAYER_NAME_PATH, builder.ToString(), Encoding.UTF8);
    }

    private static void CreateLayerMaskNameClass()
    {
        var builder = new StringBuilder();

        //レイヤーマスク名用クラス
        builder.AppendLine("/// <summary>");
        builder.AppendLine("/// レイヤーマスク名を定数で管理するクラス");
        builder.AppendLine("/// </summary>");
        builder.AppendFormat("public static class {0}", LAYERMASK_FILE_NAME_WITHOUT_EXTENSION).AppendLine();
        builder.AppendLine("{");

        foreach (var n in layerNames.
            Select(c => new { var = Utility.RemoveInvalidChars(c), val = 1 << LayerMask.NameToLayer(c) }))
        {
            builder.Append("\t").AppendFormat(@"public const int {0}Mask = {1};", n.var, n.val).AppendLine();
        }

        builder.AppendLine("}");

        var directoryName = Path.GetDirectoryName(LAYERMASK_NAME_PATH);
        //if (!Directory.Exists(directoryName))
        //{
        //    Directory.CreateDirectory(directoryName);
        //}
        Directory.CreateDirectory(directoryName);
        File.WriteAllText(LAYERMASK_NAME_PATH, builder.ToString(), Encoding.UTF8);
    }

    /// <summary>
    ///  レイヤー名を定数で管理するクラスを作成できるかどうかを取得する
    /// </summary>
    /// <returns></returns>
    [MenuItem(ITEM_NAME, true)]
    public static bool CanCreate()
    {
        return !EditorApplication.isPlaying && !Application.isPlaying && !EditorApplication.isCompiling;
    }
}
