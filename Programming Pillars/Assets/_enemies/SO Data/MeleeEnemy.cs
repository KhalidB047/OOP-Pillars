using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Melee")]
public class MeleeEnemy : EnemyScriptableObject
{
    [SerializeField] private GameObject attackParticles;

    public override void Attack(Enemy enemy)
    {
        if (GameManager.gameMan.gameOver) return;
        foreach (Collider collider in Physics.OverlapSphere(enemy.attackPoint.position, attackRadius))
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                collider.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                enemy.source.PlayOneShot(attackSound);
                Instantiate(attackParticles, enemy.attackPoint.position, Quaternion.identity);
            }
        }

    }




}
