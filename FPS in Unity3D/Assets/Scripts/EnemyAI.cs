using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public float walkSpeed = 5.0f;
    public float attackDistance = 2.0f;
    public float attackDemage = 10.0f;
    public float attackDelay = 1.0f;
    public float hp = 20.0f;
    public Transform[] transforms;

    private float timer = 0;
    private string currentState;
    private Animator animator;
    private AnimatorStateInfo stateInfo;

    void Start()
    {
        animator = transforms[0].GetComponent<Animator>();
        currentState = "";
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player") && hp > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(other.transform.position - transform.position);
            

            Quaternion finalRotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.0f * Time.deltaTime);
            transform.rotation = finalRotation;

            float distance = Vector3.Distance(transform.position, other.transform.position);
            if (distance > attackDistance && !stateInfo.IsName("Base Layer.wound"))
            {
                animationSet("run");
                transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            }
            else
            {
                if (timer <= 0)
                {
                    animationSet("attack0");
                    other.SendMessage("takeHit", attackDemage);
                    timer = attackDelay;
                }
            }

            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            animationSet("idle0");
        }
    }

    private void animationSet(string animationToPlay)
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        animationReset();

        if (currentState == "")
        {
            currentState = animationToPlay;

            if (stateInfo.IsName("Base Layer.run") && currentState != "run")
            {
                animator.SetBool("runToIdle0", true);
            }

            string state = "idle0To" + currentState.Substring(0, 1).ToUpper() + currentState.Substring(1);
            animator.SetBool(state, true);
            currentState = "";
        }
    }

    private void animationReset()
    {
        if (!stateInfo.IsName("Base Layer.idle0"))
        {
            animator.SetBool("idle0ToIdle1", false);
            animator.SetBool("idle0ToWalk", false);
            animator.SetBool("idle0ToRun", false);
            animator.SetBool("idle0ToWound", false);
            animator.SetBool("idle0ToSkill0", false);
            animator.SetBool("idle0ToAttack1", false);
            animator.SetBool("idle0ToAttack0", false);
            animator.SetBool("idle0ToDeath", false);
        }
        else
        {
            animator.SetBool("runToIdle0", false);
        }
    }

    void takeHit(float demage)
    {
        hp -= demage;
        if (hp <= 0)
        {
            animationSet("death");
        }
        else
        {
            animator.CrossFade("wound", 0.5f);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
