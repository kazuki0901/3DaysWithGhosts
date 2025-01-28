using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;
    public Transform CamHolder;

    float xRotation;
    float yRotation;

    public Image aimImage;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Cursor.visible)
            {
           
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        //マウスの入力を取得
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;

        //Y軸だけ視点に上限を設ける
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        //カメラとプレイヤーの向きを動かす
        CamHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        AimCursor();
    }

    void AimCursor()
    {
        //レーザー（ray)を飛ばす起点と方向
        Ray ray = new Ray(transform.position, transform.forward);

        //ray当たり判定の情報を取得する用
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 60))
        {
            string hitName = hit.transform.gameObject.tag;

            if (hitName == ("Enemy"))
            {
                // 照準器の色を「赤」に変える（色は自由に変更してください。）
                aimImage.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            }
            else
            {
                // 照準器の色を「水色」（色は自由に変更してください。）
                aimImage.color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
            }
        }
        else
        {
            // 照準器の色を「水色」（色は自由に変更してください。）
            aimImage.color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}
