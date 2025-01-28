using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //プレイヤーの体力
    public int maxHealth = 100;
    public int currentHealth;
    //UI用
    [SerializeField] Slider hpSlider;//HPUI
    [SerializeField] GameDirector gameDirector;

    [SerializeField] Image DamageImg;//ダメージを食らったら赤くなる

    [SerializeField] AudioClip healSound;
    [SerializeField] AudioClip damageSound;
    public static bool aliveflag;

    private bool invincibleflag = false;

    // Start is called before the first frame update
    void Start()
    {
        aliveflag = true;
        currentHealth = maxHealth;
        //ダメージ用のUIを透明化
        DamageImg.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = currentHealth;
        //赤くなった後透明に戻るようにする
        DamageImg.color = Color.Lerp(DamageImg.color, Color.clear, Time.deltaTime);

    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = healSound;
        audioSource.Play();
        if (currentHealth > maxHealth)
        {
            //HPが最大を超えないようにする
            currentHealth = maxHealth;
        }


    }

    public void TakeDamage(int damage)
    {
        if (invincibleflag)
        {
            return;
        }
        

        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = damageSound;
        audioSource.Play();
        currentHealth -= damage;
        //ダメージを受けたとき赤くなる
        DamageImg.color = new Color(0.7f, 0, 0, 0.7f);
        if (currentHealth <= 0)
        {
            Die();
        }

        StartCoroutine(invincible());

    }

    IEnumerator invincible()
    {
        invincibleflag = true;
        yield return new WaitForSeconds(1.0f);
        invincibleflag = false;
    }

    private void Die()
    {

        aliveflag = false;
        gameDirector.PlayerDeath();
    }

}
