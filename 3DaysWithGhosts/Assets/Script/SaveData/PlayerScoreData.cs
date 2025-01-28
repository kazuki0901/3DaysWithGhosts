using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using unityroom.Api;

public class PlayerScoreData : MonoBehaviour
{

    //生存日数の結果表示
    [SerializeField] TextMeshProUGUI resurtDayText;
    //敵を倒した数の表示
    [SerializeField] TextMeshProUGUI resultEnemiesText;
    //トータルスコアの表示 日数x100+倒した敵x10
    [SerializeField] TextMeshProUGUI resultScore;

    //生き残った日数
    public static int daySurvived;
    //倒した敵の数
    public static int enemiesKilled;




    // Start is called before the first frame update
    void Awake()
    {


        //Result開始時に読み込み
        ResultScore();

    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    void ResultScore()
    {
        resurtDayText.GetComponent<TextMeshProUGUI>().text = daySurvived.ToString() + "日間生き残った!!!";
        resultEnemiesText.GetComponent<TextMeshProUGUI>().text = enemiesKilled.ToString() + "体敵を倒した!!!";

        int totalScore = ((daySurvived * 100) + enemiesKilled * 10);
        resultScore.GetComponent<TextMeshProUGUI>().text = totalScore.ToString() + "points";
        UnityroomApiClient.Instance.SendScore(1, totalScore, ScoreboardWriteMode.Always);
    }



    public static void ResetScore()
    {
        //値の初期化
        daySurvived = 0;
        enemiesKilled = 0;
    }

    public void AddSurvivedData(int days)
    {
        //生き残った日数を入れる
        daySurvived = days;
    }

    public void AddEnemiesLilled(int enemies)
    {
        //倒した数を取得
        enemiesKilled = enemies;
    }
}
