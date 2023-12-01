using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBehave : StateMachineBehaviour
{
    private Animator anim;
    private Enemy enemy;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim = animator;
        enemy = anim.GetComponent<Enemy>();
        enemy.canMove = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (GameManager.gameMan.gameOver) anim.ResetTrigger("Attack");
        if (enemy.CheckDistance())
        {
            if (!enemy.onCooldown)
            {
                anim.SetTrigger("Attack");
                return;
            }
            else
            {
                enemy.attackReady = true;
                anim.ResetTrigger("Attack");
            }
        }
        else
        {
            enemy.canMove = true;
            enemy.attackReady = true;
            anim.ResetTrigger("Attack");
        }
    }


}
