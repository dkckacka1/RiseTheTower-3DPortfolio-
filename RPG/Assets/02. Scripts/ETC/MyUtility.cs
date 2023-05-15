using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyUtility
{
    public static string returnColorText(string text, Color color)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{text}</color>"; ;
    }

    public static string returnAlignmentText(string text, alignmentType type)
    {
        return $"<align=\"{type}\">{text}</align>"; ;
    }

    public static string returnSideText(string leftText, string rightText)
    {
        return $"<align=left>{leftText}<line-height=0>\n<align=right>{rightText}<line-height=1em></align>";
    }

    /// <summary>
    /// 성공 확률 계산기
    /// </summary>
    /// <param name="successPoint">성공 확률(successPoint를 넘으면 성공)</param>
    /// <param name="minPoint">최소 랜덤값</param>
    /// <param name="maxPoint">최대 랜덤값(incluesive)</param>
    /// <returns>true 면 성공 flase 면 실패</returns>
    public static bool ProbailityCalc(float successPoint, float minPoint, float maxPoint)
    {
        float random = Random.Range(minPoint, maxPoint);

        return (random > successPoint);
    }
}
