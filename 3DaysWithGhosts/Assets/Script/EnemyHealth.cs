using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int MaxHp = 100;//敵の最大HP 
    private int currentHp;//敵の現在のHP

    [SerializeField] int score;

    [SerializeField] GameDirector gameDirector;

    [SerializeField] EnemySpawner enemySpawner;

    [SerializeField] Transform[] respawnPoints;//リスポーン位置 

    private AudioSource audioSource;
    public AudioClip deathSound;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = MaxHp;
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {

        currentHp -= damage;



        if (currentHp <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        Vector3 vec3 = GetComponent<Transform>().position;
        AudioSource.PlayClipAtPoint(deathSound, vec3);
        gameDirector.AddScore(score);
        enemySpawner.EnemyKilled(gameObject);
        OnDeath();
    }

    void OnDeath()
    {


        currentHp = MaxHp;

    }


    public void SetEnemySpawner(EnemySpawner spawner)
    {
        enemySpawner = spawner;

    }

    public void SetGameDirecter(GameDirector director)
    {
        gameDirector = director;

    }

}
