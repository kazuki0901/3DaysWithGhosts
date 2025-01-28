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
        //�����c���������̕\��
        if (GameDirector.currentDay == 3)
        {
            //3���Ԑ����c����
            resultDayText.GetComponent<TextMeshProUGUI>().text
            = GameDirector.currentDay.ToString() + "�������c����!!";
        }
        else
        {
            //0,1,2���Ԑ����c����
            GameDirector.currentDay -= 1;
            resultDayText.GetComponent<TextMeshProUGUI>().text
            = GameDirector.currentDay.ToString() + "�������c����!!";
        }
        
        //�|�������̕\��
        resultScoreText.GetComponent<TextMeshProUGUI>().text
            = GameDirector.score.ToString() + "�̓|����!!";
       
    }


}
    

    

