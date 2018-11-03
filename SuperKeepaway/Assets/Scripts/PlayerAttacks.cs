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

    public string joystickID = "1";

	void Update () {
        
        if (timeBtwAttack <= 0){
            
            if (Input.GetButtonDown("Attack_"+joystickID)){
                Debug.Log("attack");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
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
