using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{


    [SerializeField] float lengthOfDay = 60f;//1日の長さ
    //時間(0.0 = 0時,1,0 = 24時)
    [Range(0, 1)]
    public float currentTimeOfDay = 0.0f;
    //1日の進行度
    private float timeMultiplier = 1f;

    //現在の経過日数
    public static int currentDay;

    //経過時間を表示するテキスト
    [SerializeField] GameObject timeText;
    [SerializeField] GameObject dayText;
    //ゲーム内で経過した時間
    private float timeProgress = 0f;

    [SerializeField] GameObject scoreText;//得点UI
    public static int score;//得点管理用

    [SerializeField] GameObject ammoCountText;//残弾数UI
    [SerializeField] GunManeger gunManeger;//残弾数を取得する用
    int magazineCount;

    [SerializeField]Image　GameOverUI;//ゲームオーバー用のUI 
    [SerializeField] TextMeshProUGUI GameOvertext;

    [SerializeField] Image GameClearUI;//ゲームオーバー用のUI 
    [SerializeField] TextMeshProUGUI GameCleartext;

    private bool isGameOver = false;




    void Start()
    {
        GameOverUI.enabled = false;
        GameOvertext.enabled = false;
        GameClearUI.enabled = false;
        GameCleartext.enabled = false;
        //ゲームスコアの初期化
        score = 0;
        currentDay = 1;


        AddScore(score);
        PlayerScoreData.ResetScore();
    }

    // Update is called once per frame
    void Update()
    {
        //時間管理
        UpdateTime();

        //残弾管理
        magazineCount = gunManeger.MagazineCount;

        if (magazineCount == 0 || gunManeger.reloading)
        {
            this.ammoCountText.GetComponent<TextMeshProUGUI>().text = "残弾数 : リロード中";
        }
        else
        {
            this.ammoCountText.GetComponent<TextMeshProUGUI>().text = "残弾数 : " + this.magazineCount.ToString();
        }
    }

    void UpdateTime()
    {
        //現実の時間経過
        timeProgress += Time.deltaTime;

        //１日の進行具合
        currentTimeOfDay += (Time.deltaTime / lengthOfDay) * timeMultiplier;

        //1日が終了したらリセットして次の日に進む
        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
            currentDay++;
        }

        //3日が終了したらゲーム終了

        if (currentDay >= 3)
        {
            //ゲーム終了処理

            EndGame();
        }

        if (timeText != null && dayText != null)
        {
            int hours = Mathf.FloorToInt(currentTimeOfDay * 24);
            timeText.GetComponent<TextMeshProUGUI>().text = "時間 : " + hours + ":00";
            dayText.GetComponent<TextMeshProUGUI>().text = currentDay.ToString() + "日目";
        }
    }

    public void PlayerDeath()
    {
        if (isGameOver) return;
        isGameOver = true;
        GameOver();
    }


    void EndGame()
    {
        PlayerScoreData.enemiesKilled = score;
        PlayerScoreData.daySurvived = currentDay;

        enabled = false;
        Cursor.lockState = CursorLockMode.None; //マウスカーソルの設定を初期化して、
        Cursor.visible = true; //マウスカーソルを表示する
                               //リザルトシーンへ
        StartCoroutine(FadeOutAndLoadScene(GameClearUI, GameCleartext));
    }

    void GameOver()
    {



        currentDay -= 1;

        PlayerScoreData.enemiesKilled = score;
        PlayerScoreData.daySurvived = currentDay;


        enabled = false;

        Cursor.lockState = CursorLockMode.None; //マウスカーソルの設定を初期化して、
        Cursor.visible = true; //マウスカーソルを表示する

        //リザルトシーンへ
        StartCoroutine(FadeOutAndLoadScene(GameOverUI,GameOvertext));
        
      
    }


    public void AddScore(int enemiesKilled)
    {
        //score管理
        score += enemiesKilled;
        scoreText.GetComponent<TextMeshProUGUI>().text = ("倒した敵 : ") + score.ToString() + "体";

    }

    public IEnumerator FadeOutAndLoadScene(Image ui ,TextMeshProUGUI text)
    {
         float fadeDuration = 3.0f;
        ui.enabled = true;
        text.enabled = true; 
        float elapsedTime = 0.0f;                 // 経過時間を初期化
        Color startUiColor = ui.color;       // フェードパネルの開始色を取得
        Color endUiColor = new Color(startUiColor.r, startUiColor.g, startUiColor.b, 1.0f); // フェードパネルの最終色を設定

        Color startTextColor = text.color;       // フェードパネルの開始色を取得
        Color endTextColor = new Color(startTextColor.r, startTextColor.g, startTextColor.b, 1.0f); // フェードパネルの最終色を設定

        // フェードアウトアニメーションを実行
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;                        // 経過時間を増やす
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);  // フェードの進行度を計算
            ui.color = Color.Lerp(startUiColor, endUiColor, t); // パネルの色を変更してフェードアウト
            text.color = Color.Lerp(startTextColor, startTextColor, t);
            yield return null;                                     // 1フレーム待機
        }

        text.color = endUiColor;  // フェードが完了したら最終色に設定
        SceneManager.LoadScene("ResultScene"); // シーンをロードしてメニューシーンに遷移
    }

 

}
