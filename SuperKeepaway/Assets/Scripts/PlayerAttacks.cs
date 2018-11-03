using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Attack
{
    public Transform location;

    public Vector2 momentum = new Vector2(5f, 0f);

    public Vector2 knockback = new Vector2(5f, 2f);
    public float knockbackTime = 0.3f;
    public float damage = 10;

    public float initTime = 0.1f;
    public float activeTime = 0.1f;
    public float recoveryTime = 0.3f;
    public int animationState;
}

public class PlayerAttacks : MonoBehaviour {

    public AudioClip hitClip;
    public AudioClip missClip;

    public AudioSource hitSource;
    public AudioSource missSource;

    private float timeBtwAttack;
	public float startTimeBtwAttack;

    public Transform attackPos;
    public Transform lowAttackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;

    private string joystickID = "1";

    private PlayerControl playerControl;

    Attack neutralGround;
    Attack slide;

    Attack currentAttack;
    int attackState;

    void Start()
    {
        hitSource.clip = hitClip;
        missSource.clip = missClip;

        playerControl = GetComponent<PlayerControl>();

        neutralGround = new Attack();
        neutralGround.location = attackPos;
        neutralGround.knockback = new Vector2(5f, 2f);
        neutralGround.knockbackTime = 0.3f;
        neutralGround.damage = 10;

        neutralGround.initTime = 0.1f;
        neutralGround.recoveryTime = 0.3f;
        neutralGround.animationState = AnimationStates.PUNCH;


        slide = new Attack();
        slide.location = lowAttackPos;
        slide.momentum = new Vector2(15f, 0f);
        slide.knockback = new Vector2(4f, 10f);
        slide.knockbackTime = 0.3f;
        slide.damage = 5;

        slide.initTime = 0.05f;
        slide.activeTime = 0.3f;
        slide.recoveryTime = 0.5f;
        slide.animationState = AnimationStates.SLIDE;

    }

    void Update () {

        joystickID = playerControl.joystickID;
        Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
        if (timeBtwAttack <= 0){
            
            if (attackState == 1 || attackState == 2)
            {
                if (attackState == 1)
                {
                    attackState = 2;
                    rb.AddForce(new Vector2(currentAttack.momentum.x * Mathf.Sign(transform.localScale.x),
                        currentAttack.momentum.y), ForceMode2D.Impulse);
                }

                Debug.Log("active");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(currentAttack.location.position, attackRange, whatIsEnemies);
                foreach (Collider2D enemyCollider in enemiesToDamage)
                {
                    PlayerControl enemy = enemyCollider.GetComponent<PlayerControl>();
                    if (enemy.team != playerControl.team && enemy.knockbackTime < currentAttack.knockbackTime - 0.2f)
                    {
                        hitSource.Play();
                        Debug.Log("hit");

                        enemy.transform.localScale = new Vector2(Mathf.Abs(enemy.transform.localScale.x) * Mathf.Sign(-transform.localScale.x), enemy.transform.localScale.y);

                        enemy.knockbackTime = currentAttack.knockbackTime; //stun time
                        enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(currentAttack.knockback.x * Mathf.Sign(transform.localScale.x),
                            currentAttack.knockback.y), ForceMode2D.Impulse); //impulse dir
                    }
                    else
                    {
                        missSource.Play();
                    }
                    //  enemiesToDamage[i].GetComponent<Player>().TakeDamage(damage);
                }
                timeBtwAttack -= Time.deltaTime;
                if (timeBtwAttack < -currentAttack.activeTime)
                {
                    attackState = 3;
                    timeBtwAttack = currentAttack.recoveryTime;
                }
            }

            else {
                attackState = 0;
                playerControl.canMove = true;
                if (Input.GetButtonDown("Attack_" + joystickID))
                {
                    attackState = 1;

                    if (Input.GetAxis("Vertical_" + joystickID) < -0.5)
                    {
                        currentAttack = slide;
                    }
                    else
                    {
                        currentAttack = neutralGround;
                    }
                    



                    timeBtwAttack = currentAttack.initTime;
                    playerControl.canMove = false;
                    playerControl.animationState = currentAttack.animationState;
                }
            }
			
		} else{
			timeBtwAttack -= Time.deltaTime;
		}

        if (attackState != 0 && playerControl.grounded && Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            rb.AddForce(Vector2.left * Mathf.Sign(rb.velocity.x) * 20);
        }

        if (playerControl.knockbackTime > 0)
        {
            attackState = 0;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (currentAttack != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(currentAttack.location.position, attackRange);
        }
    }
}
