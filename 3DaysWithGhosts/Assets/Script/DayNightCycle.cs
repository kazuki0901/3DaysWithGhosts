using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    //太陽光を設定
    public Light sun;
    //1日の長さ
    public float dayLengthInSeconds = 60f;
    //太陽の初期の角度
    public float initialSunRotation = 0f;

    //現在の時刻
    [Range(0, 1)]
    public float currentTimeOfDay = 0f;
    //1日の進行速度
    private float timeMultiplier = 1f;
    //太陽の強さ
    public float maxSunIntensity = 1f;
    public float minSunInsensity = 0f;

    //空の色を変えるためのスカイボックス
    public Material SkyBox;
    public Color daySkyBoxColor;
    public Color nightSkyBoxColor;

    //環境ライトの色
    public Color dayAmbientColor;
    public Color nightAmbientColor;

    //現在の経過時間を管理するための変数
    private float timeElapsed = 0f;

    public int currentDay = 1;





    // Update is called once per frame
    void Update()
    {
        UpdateTime();
        UpdateLighting();
    }

    void UpdateTime()
    {
        //現実の経過時間
        timeElapsed += Time.deltaTime;

        //1日の進行割合
        currentTimeOfDay += (Time.deltaTime / dayLengthInSeconds) * timeMultiplier;
        
        //1日が終了したらリセットして次の日に進む
        if (currentTimeOfDay >= 1) 
            {
                currentTimeOfDay = 0;
                currentDay++;
            }

        if (currentDay > 3)
        {
            //3日経過したらここに終了
        }
    }

    void UpdateLighting()
    {
        //現在の時間に応じて太陽の強さを変更
        float sunIntensity = Mathf.Lerp(minSunInsensity, maxSunIntensity, CalclateSunIntensity());
        sun.intensity = sunIntensity;

        //太陽の角度を調整 0～360の調整
        sun.transform.rotation = Quaternion.Euler((currentTimeOfDay * 360f)-90,170,0);

        //環境ライトの色を変化させる
        RenderSettings.ambientLight = Color.Lerp(nightAmbientColor, dayAmbientColor, CalclateSunIntensity());

        if(SkyBox != null)
        {
            SkyBox.SetColor("_Tint", Color.Lerp(nightSkyBoxColor, daySkyBoxColor, CalclateSunIntensity()));
        }

        float CalclateSunIntensity()
        {
            //日中の間は明るく、それ以外は暗くする
            if (currentTimeOfDay <= 0.25f || currentTimeOfDay >= 0.75f)
            {
                //夜
                return 0f;
            }
            else
            {
                //朝と夕方に徐々に明るくなったり暗くなったりするように
                if(currentTimeOfDay < 0.5f)
                {
                    //朝
                    return Mathf.InverseLerp(0.25f, 0.5f, currentTimeOfDay);
                }
                else
                {
                    //夕方
                    return Mathf.InverseLerp(0.75f, 0.5f, currentTimeOfDay);
                }
            }


        }
    }
}


        

