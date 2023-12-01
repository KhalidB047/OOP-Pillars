using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemy;
    public Transform playerT { get; private set; }
    public Transform attackPoint;

    public bool canMove;
    public bool canLook = true;
    public bool attackReady = true;
    public bool onCooldown = false;
    public bool spawning = true;

    private float maxHealth;
    private float CurrentHealth;
    [SerializeField] private Slider healthSlider;

    public AudioSource source { get; private set; }
    private Animator anim;


    private void Start()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        source = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        maxHealth = enemy.maxHealth;
        CurrentHealth = maxHealth;
        healthSlider.value = CurrentHealth / maxHealth;
    }




    void Update()
    {
        if (GameManager.gameMan.gameOver) return;
        if (spawning) return;

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

    public void TakeDamage(float damage)
    {
        if (CurrentHealth == maxHealth) healthSlider.gameObject.SetActive(true);
        CurrentHealth -= damage;
        if (CurrentHealth <= 0f)
        {
            healthSlider.value = 0f;
            GameManager.gameMan.UpdateScore(enemy.score);
            Destroy(gameObject);
        }
        else
        {
            healthSlider.value = CurrentHealth / maxHealth;
        }
    }

    public void EndSpawn()
    {
        spawning = false;
    }
}
