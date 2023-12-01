using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class EnemyScriptableObject : ScriptableObject
{
    [SerializeField] private float movementSpeed;
    public float maxHealth;
    public int score;
    public float attackingRange;
    public float attackRadius;
    public float attackDamage;
    public float attackCooldownTime;


    public AudioClip attackSound;

















    public abstract void Attack(Enemy enemy);


    public virtual bool CheckDistance(Enemy enemy)
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.playerT.position);
        return distance <= attackingRange;
    }


    public virtual void LookAtPlayer(Enemy enemy)
    {
        Vector3 lookDir = enemy.playerT.position;
        lookDir.y = enemy.transform.position.y;
        enemy.transform.LookAt(lookDir, Vector3.up);
    }


    public virtual void Move(Enemy enemy)
    {
        Vector3 playerPos = enemy.playerT.transform.position;
        playerPos.y = enemy.transform.position.y;
        enemy.transform.Translate((playerPos - enemy.transform.position).normalized
            * movementSpeed * Time.deltaTime, Space.World);
    }
}
