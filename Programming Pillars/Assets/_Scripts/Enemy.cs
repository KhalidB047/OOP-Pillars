using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemy;
    public Transform playerT { get; private set; }
    public Transform attackPoint;

    public bool canMove = true;
    public bool canLook = true;
    public bool attackReady = true;
    public bool onCooldown = false;


    public AudioSource source { get; private set; }
    private Animator anim;


    private void Start()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        source = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }




    void Update()
    {
        if (attackReady && !onCooldown)
        {
            if (CheckDistance())
            {
                attackReady = false;
                onCooldown = true;
                anim.SetTrigger("Attack");
                StartCoroutine(AttackCooldown());
            }
            else if (!canMove) canMove = true;
        }



        if (canMove) enemy.Move(this);
        if (canLook) enemy.LookAtPlayer(this);
    }


    public void AttackPlayer()
    {
        enemy.Attack(this);
    }

    public bool CheckDistance()
    {
        return enemy.CheckDistance(this);
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(enemy.attackCooldownTime);
        onCooldown = false;
    }
}
