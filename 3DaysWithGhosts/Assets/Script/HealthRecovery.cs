using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRecovery : MonoBehaviour
{
    //回復量
    public int healAmount = 10;

  


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponentInChildren<PlayerHealth>();
            if(playerHealth != null)
            {
                playerHealth.Heal(healAmount);

                
                GameObject medBoxSpawner = GameObject.Find("MedBoxSpawner");  
                medBoxSpawner.GetComponent<MedBoxSpawner>().ItemCollected();

               
            }

        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
