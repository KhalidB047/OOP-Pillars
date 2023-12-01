using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] rewards;
    [SerializeField] private Transform rewardSpawnPoint;
    [SerializeField] private float force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void SpawnReward()
    {
        GameObject reward = (GameObject)Instantiate(rewards[Random.Range(0, rewards.Length)], rewardSpawnPoint.position, rewardSpawnPoint.localRotation);
        reward.GetComponent<Rigidbody>().AddForce(rewardSpawnPoint.forward * force, ForceMode.Impulse);
    }
}
