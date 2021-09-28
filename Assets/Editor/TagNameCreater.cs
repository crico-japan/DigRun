using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public static class TagNameCreater
{
    // コマンド名
    private const string ITEM_NAME = "Tools/Create/Tag Name";

    // ファイルパス
    private const string TAG_NAME_PATH = "Assets/TagAndLayer/TagNames/TagName.cs";

    //ファイル名(拡張子あり)
    private static readonly string TAG_FILE_NAME = Path.GetFileName(TAG_NAME_PATH);

    //ファイル名(拡張子無し)
    private static readonly string TAG_FILE_NAME_WITHOUT_EXTENSION = Path.GetFileNameWithoutExtension(TAG_NAME_PATH);

    private static string[] tagNames = InternalEditorUtility.tags;

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

        EditorUtility.DisplayDialog(TAG_FILE_NAME, "作成が完了しました", "OK");
    }

    /// <summary>
    /// スクリプトを作成
    /// </summary>
    public static void CreateScript()
    {
        CreateLayerNameClass();
        AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
    }

    private static void CreateLayerNameClass()
    {
        tagNames = InternalEditorUtility.tags;
        var builder = new StringBuilder();

        //レイヤー名用クラス
        builder.AppendLine("/// <summary>");
        builder.AppendLine("/// レイヤー名を定数で管理するクラス");
        builder.AppendLine("/// </summary>");
        builder.AppendFormat("public static class {0}", TAG_FILE_NAME_WITHOUT_EXTENSION).AppendLine();
        builder.AppendLine("{");

        foreach (var n in tagNames.
            Select(c => new { var = Utility.RemoveInvalidChars(c), val = c }))
        {
            builder.Append("\t").AppendFormat(@"public const string {0} = ""{1}"";", n.var, n.val).AppendLine();
        }

        //クラス版
        //builder.AppendLine("/// <summary>");
        //builder.AppendLine("/// レイヤー名を定数で管理するクラス");
        //builder.AppendLine("/// </summary>");
        //builder.AppendFormat("public static class {0}", TAG_FILE_NAME_WITHOUT_EXTENSION).AppendLine();
        //builder.AppendLine("{");
        //foreach (var n in tagNames.
        //    Select(c => new { var = Utility.RemoveInvalidChars(c), val = c }))
        //{
        //    builder.Append("\t").AppendFormat(@"public const string {0} = ""{1}"";", n.var, n.val).AppendLine();
        //}

        builder.AppendLine("}");

        var directoryName = Path.GetDirectoryName(TAG_NAME_PATH);
        //if (!Directory.Exists(directoryName))
        //{
        //    Directory.CreateDirectory(directoryName);
        //}
        Directory.CreateDirectory(directoryName);

        File.WriteAllText(TAG_NAME_PATH, builder.ToString(), Encoding.UTF8);
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
