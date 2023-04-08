using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject meleeAttack;

    public int health = 3;

    public void TakeDamage(int damage)
    {
        Debug.Log("ow");
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            //play hurt animation
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2f)
        {
            Vector3 direction = player.transform.position - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (angle > -45f && angle < 45f)
            {
                // Debug.Log("Right");
                // instantiate a new melee attack object to the right of this object
                GameObject newMeleeAttack = Instantiate(meleeAttack, transform.position + new Vector3(0.5f, 0, 0), Quaternion.identity);
                newMeleeAttack.GetComponent<Slash>().origin = "enemy";
            }
            else if (angle > 45f && angle < 135f)
            {
                // Debug.Log("Up");
                // instantiate a new melee attack object above this object and rotate it to face up
                // Instantiate(meleeAttack, transform.position + new Vector3(0, 1, 0), Quaternion.identity);

                // instantiate a new melee attack object above this object and rotate it to face up
                GameObject newMeleeAttack = Instantiate(meleeAttack, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                newMeleeAttack.transform.Rotate(0, 0, 90);
                newMeleeAttack.GetComponent<Slash>().origin = "enemy";
            }
            else if (angle > 135f || angle < -135f)
            {
                // Debug.Log("Left");
                // instantiate a new melee attack object to the left of this object
                // Instantiate(meleeAttack, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);

                GameObject newMeleeAttack = Instantiate(meleeAttack, transform.position + new Vector3(-0.5f, 0, 0), Quaternion.identity);
                newMeleeAttack.transform.Rotate(0, 0, 180);
                newMeleeAttack.GetComponent<Slash>().origin = "enemy";
            }
            else if (angle > -135f && angle < -45f)
            {
                // Debug.Log("Down");
                // instantiate a new melee attack object below this object
                // Instantiate(meleeAttack, transform.position + new Vector3(0, -1, 0), Quaternion.identity);

                GameObject newMeleeAttack = Instantiate(meleeAttack, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
                newMeleeAttack.transform.Rotate(0, 0, 270);
                newMeleeAttack.GetComponent<Slash>().origin = "enemy";
            }
        }
    }
}
