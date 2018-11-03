using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour {

	private float timeBtwAttack;
	public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;

    private string joystickID = "1";

    private PlayerControl playerControl;

    void Start()
    {
        playerControl = GetComponent<PlayerControl>();
    }

    void Update () {



        joystickID = playerControl.joystickID;

        if (timeBtwAttack <= 0){
            
            if (Input.GetButtonDown("Attack_"+joystickID)){
                Debug.Log("attack");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                foreach (Collider2D enemyCollider in enemiesToDamage)
                {
                    Debug.Log("hit");

                    PlayerControl enemy = enemyCollider.GetComponent<PlayerControl>();
                    if (enemy.team != playerControl.team)
                    {
                        enemy.knockbackTime = 0.3f; //stun time
                        enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(5f * Mathf.Sign(transform.localScale.x), 2f), ForceMode2D.Impulse); //impulse dir
                    }
                  //  enemiesToDamage[i].GetComponent<Player>().TakeDamage(damage);
                }
                timeBtwAttack = startTimeBtwAttack;
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
