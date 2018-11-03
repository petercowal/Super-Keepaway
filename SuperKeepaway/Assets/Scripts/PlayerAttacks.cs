using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Attack
{
    public Transform location;
    public Vector2 knockback = new Vector2(5f, 2f);
    public float knockbackTime = 0.3f;
    public float damage = 10;

    public float initTime = 0.1f;
    public float activeTime = 0.1f;
    public float recoveryTime = 0.3f;
    public int animationState;
}

public class PlayerAttacks : MonoBehaviour {

	private float timeBtwAttack;
	public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;

    private string joystickID = "1";

    private PlayerControl playerControl;

    Attack neutralGround;

    Attack currentAttack;
    bool attacking;

    void Start()
    {
        playerControl = GetComponent<PlayerControl>();

        neutralGround = new Attack();
        neutralGround.location = attackPos;
        neutralGround.knockback = new Vector2(5f, 2f);
        neutralGround.knockbackTime = 0.3f;
        neutralGround.damage = 10;

        neutralGround.initTime = 0.1f;
        neutralGround.recoveryTime = 0.5f;
        neutralGround.animationState = 1;

    }

    void Update () {

        joystickID = playerControl.joystickID;

        if (timeBtwAttack <= 0){
            
            if (attacking)
            {
                Debug.Log("active");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                foreach (Collider2D enemyCollider in enemiesToDamage)
                {
                    PlayerControl enemy = enemyCollider.GetComponent<PlayerControl>();
                    if (enemy.team != playerControl.team && enemy.knockbackTime < currentAttack.knockbackTime - currentAttack.activeTime - 0.1f)
                    {
                        Debug.Log("hit");

                        enemy.transform.localScale = new Vector2(Mathf.Abs(enemy.transform.localScale.x) * Mathf.Sign(-transform.localScale.x), enemy.transform.localScale.y);

                        enemy.knockbackTime = currentAttack.knockbackTime; //stun time
                        enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(currentAttack.knockback.x * Mathf.Sign(transform.localScale.x),
                            currentAttack.knockback.y), ForceMode2D.Impulse); //impulse dir
                    }
                    //  enemiesToDamage[i].GetComponent<Player>().TakeDamage(damage);
                }
                timeBtwAttack -= Time.deltaTime;
                if (timeBtwAttack < -currentAttack.activeTime)
                {
                    attacking = false;
                    timeBtwAttack = currentAttack.recoveryTime;
                }
            }

            else if (Input.GetButtonDown("Attack_"+joystickID)){
                attacking = true;
                currentAttack = neutralGround;
                timeBtwAttack = currentAttack.initTime;
            }
			
		} else{
			timeBtwAttack -= Time.deltaTime;
		}
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
