using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resultManager : MonoBehaviour
{
    [SerializeField] GameObject resultDayText;
    [SerializeField] GameObject resultScoreText;



    // Start is called before the first frame update
    void Start()
    {
        //生き残った日数の表示
        if (GameDirector.currentDay == 3)
        {
            //3日間生き残った
            resultDayText.GetComponent<TextMeshProUGUI>().text
            = GameDirector.currentDay.ToString() + "日生き残った!!";
        }
        else
        {
            //0,1,2日間生き残った
            GameDirector.currentDay -= 1;
            resultDayText.GetComponent<TextMeshProUGUI>().text
            = GameDirector.currentDay.ToString() + "日生き残った!!";
        }
        
        //倒した数の表示
        resultScoreText.GetComponent<TextMeshProUGUI>().text
            = GameDirector.score.ToString() + "体倒した!!";
       
    }


}
    

    

