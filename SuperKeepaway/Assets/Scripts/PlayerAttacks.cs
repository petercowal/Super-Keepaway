using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour {

	private float attackDelay;
	private float delayReset;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(attackDelay <= 0){
			if(Input.GetButtonDown("Fire1")){
              //  Collider2D[] toDamage = Physic2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
             //   for (int i = 0; i < toDamage.length; i++)
             //   {
                    //toDamage[i].GetComponent<Player>().TakeDamage(damage);
              //  }
			}

			attackDelay = delayReset;
		} else{
			attackDelay -= Time.deltaTime;
		}
	}
}
