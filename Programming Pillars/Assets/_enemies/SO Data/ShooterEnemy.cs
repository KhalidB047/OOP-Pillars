using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemies/Shooter")]
public class ShooterEnemy : EnemyScriptableObject
{
    [SerializeField] private GameObject bullet;


    public override void Attack(Enemy enemy)
    {
        if (GameManager.gameMan.gameOver) return;

        Instantiate(bullet, enemy.attackPoint.position, enemy.attackPoint.transform.rotation);
        enemy.source.PlayOneShot(attackSound);

    }
}
