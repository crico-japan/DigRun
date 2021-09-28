using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class Utility
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

    /// <summary>
    /// 無効な文字を削除する
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string RemoveInvalidChars(string str)
    {
        //空白削除
        string newstr = Regex.Replace(str, @"\s", "");

        //無効な文字削除
        Array.ForEach(INVALUD_CHARS, c => newstr.Replace(c, string.Empty));
        //Regex.Replace(str, @"\s", "");
        return newstr;
    }

    /// <summary>
    /// 対象がビューポート内にいるか判定する
    /// </summary>
    /// <param name="target"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static bool ChechInsideViewPortToWorldPoint(Transform target, float offset)
    {
        float dist = target.position.z - Camera.main.transform.position.z;
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist));
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, dist));

        if (target.position.x < min.x - offset
            || target.position.x > max.x + offset
            || target.position.y < min.y - offset
            || target.position.y > max.y + offset)
        {
            return false;
        }

        return true;
    }
}
