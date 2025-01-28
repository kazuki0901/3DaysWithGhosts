using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    public float lifetime;//生成されてから消えるまでの時間
    [SerializeField] GameObject gameDirector;

    void Start()
    {
       
        //生成されてから消えるまで
        Destroy(gameObject, lifetime);

        this.gameDirector = GameObject.Find("GameDirector");
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (gameObject.CompareTag("Bullet") && collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(20);
                
            }
            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("EnemyBullet") && collision.gameObject.CompareTag("Player"))
        {

            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                //playerHealth.TakeDamage(20);
                //Debug.Log("攻撃を食らった！");

            }
            Destroy(gameObject);
        }



        /**
        if (this.gameObject.CompareTag("Bullet")&& collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("命中!");
            
            this.gameDirector.GetComponent<GameDirector>().EnemyDown();
            Destroy(collision.gameObject);
        }
        

        if (this.gameObject.CompareTag("EnemyBullet")&& collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("攻撃を食らった！");
            this.gameDirector.GetComponent<GameDirector>().PlayerDown();
            Destroy(this.gameObject);
        }
        */




    }
}
