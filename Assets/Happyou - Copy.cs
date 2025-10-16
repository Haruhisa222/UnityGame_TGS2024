using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Happyou : MonoBehaviour
{
    public Text stext; // スコアを表示するTextオブジェクト
    public static List<int> leaderboard = new List<int>();
    void Start()
    {
        // Scoreクラスからスコアを取得
        int sscore = Score.score;
        stext.text = sscore + "点";

        // ランキングを更新
        UpdateLeaderboard(sscore);
        
        // ランキング表示を更新
        GetComponent<RankingDisplay>().UpdateRankingDisplay();
    }

    public void UpdateLeaderboard(int sscore)
    {
        // ランキングにスコアを追加し、降順にソート
        leaderboard.Add(sscore); // Scoreクラスのleaderboardを使用
        leaderboard.Sort((a, b) => b.CompareTo(a)); // 降順
    }

    public static List<int> GetLeaderboard()
    {
        return leaderboard; // Scoreクラスからリーダーボードを取得
    }
}