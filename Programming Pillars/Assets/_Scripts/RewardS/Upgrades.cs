using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    [SerializeField] private int upgradeType;
    [SerializeField] private float attackTimeReduction;
    [SerializeField] private float movementSpeedIncrease;
    [SerializeField] private float damageIncrease;

    [SerializeField] private AudioClip pickupAudio;
    [SerializeField] private GameObject pickupParticles;





    

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Floor"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("pickup");
            switch (upgradeType)
            {
                case 1:
                    other.gameObject.GetComponent<PlayerAttack>().ChangeCooldown(attackTimeReduction);
                    break;

                case 2:
                    other.gameObject.GetComponent<PlayerAttack>().ChangeDamage(damageIncrease);
                    break;

                case 3:
                    other.gameObject.GetComponent<PlayerMovement>().ChangeMoveSpeed(movementSpeedIncrease);
                    break;


                default: return;
            }
            Instantiate(pickupParticles, transform.position, pickupParticles.transform.rotation);
            GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>().PlayOneShot(pickupAudio);
            Destroy(gameObject);
        }
    }


}
