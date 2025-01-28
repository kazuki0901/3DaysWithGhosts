using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutolialUI : MonoBehaviour
{
    [SerializeField] GameObject Tutolial1;
    [SerializeField] GameObject Tutolial2;
    [SerializeField] GameObject Tutolial3;
    [SerializeField] GameObject cursor;

    private void Awake()
    {
        Time.timeScale = 0f;
    }

    void Start()
    {

        Tutolial1.SetActive(true);
        Tutolial2.SetActive(false);
        Tutolial3.SetActive(false);
        cursor .SetActive(false);
    }
    public void ActiveTutolial1()
    {

        Tutolial1.SetActive(true);
        Tutolial2.SetActive(false);
    }
    public void ActiveTutolial2()
    {


        Tutolial2.SetActive(true);
        Tutolial1.SetActive(false);
        Tutolial3.SetActive(false);

    }

    public void ActiveTutolial3()
    {


        Tutolial3.SetActive(true);
        Tutolial2.SetActive(false);
    }
    public void GameStart()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
        cursor.SetActive(true);
    }
}
