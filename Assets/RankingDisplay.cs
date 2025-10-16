using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingDisplay : MonoBehaviour
{
    public Text rankingText; // ランキングを表示するTextオブジェクト
    public static List<int> juni;

    void Start()
    {
        juni = new List<int>(); // juniリストを初期化
        UpdateRankingDisplay();
    }

    public void UpdateRankingDisplay()
    {
        List<int> leaderboard = Happyou.GetLeaderboard(); // Scoreクラスからリーダーボードを取得
        rankingText.text = "\n";

        // 上位5位を表示
        if (leaderboard.Count > 0)
        {
            juni.Clear(); // 既存の順位をクリア
            int currentRank = 1;
            int previousScore = leaderboard[0];

            for (int i = 0; i < leaderboard.Count && i < 7; i++)
            {
                if (leaderboard[i] != previousScore)
                {
                    currentRank = i + 1; // 新しいスコアの順位を設定
                    previousScore = leaderboard[i]; // 現在のスコアを更新
                }
                juni.Add(currentRank); // 現在の順位を追加
            }

            // 上位7位を表示
            for (int i = 0; i < leaderboard.Count && i < 7; i++)
            {
                rankingText.text += juni[i] + "位: " + leaderboard[i] + "点\n";
            }
        }
        else
        {
            rankingText.text = "ランキングはありません。";
        }
    }
}
