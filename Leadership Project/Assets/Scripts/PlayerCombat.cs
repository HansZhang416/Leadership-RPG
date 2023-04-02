using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Stats")]
    public int health = 5;

    public GameObject meleeAttack;



    public void TakeDamage(int damage)
    {
        Debug.Log("ow");
        health -= damage;
        if (health <= 0)
        {
            // Destroy(gameObject);
            // TODO: handle gameover
        }
    }

    // Update is called once per frame
    void Update()
    {
        // detect a mouse click and determine if it happened to the left, right, top, or bottom of this object
        if (Input.GetMouseButtonDown(0))
        {
            // get the mouse position in world space
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // get the direction from this object to the mouse position
            Vector3 direction = mousePosition - transform.position;
            // get the angle of the direction
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // // create a quaternion from the angle
            // Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            // // set the rotation of this object to the new rotation
            // transform.rotation = rotation;
            // Debug.Log(angle);

            if (angle > -45f && angle < 45f)
            {
                // Debug.Log("Right");
                // instantiate a new melee attack object to the right of this object
                GameObject newMeleeAttack = Instantiate(meleeAttack, transform.position + new Vector3(0.5f, 0, 0), Quaternion.identity);
                newMeleeAttack.GetComponent<Slash>().origin = "player";
            }
            else if (angle > 45f && angle < 135f)
            {
                // Debug.Log("Up");
                // instantiate a new melee attack object above this object and rotate it to face up
                // Instantiate(meleeAttack, transform.position + new Vector3(0, 1, 0), Quaternion.identity);

                // instantiate a new melee attack object above this object and rotate it to face up
                GameObject newMeleeAttack = Instantiate(meleeAttack, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                newMeleeAttack.transform.Rotate(0, 0, 90);
                newMeleeAttack.GetComponent<Slash>().origin = "player";
            }
            else if (angle > 135f || angle < -135f)
            {
                // Debug.Log("Left");
                // instantiate a new melee attack object to the left of this object
                // Instantiate(meleeAttack, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);

                GameObject newMeleeAttack = Instantiate(meleeAttack, transform.position + new Vector3(-0.5f, 0, 0), Quaternion.identity);
                newMeleeAttack.transform.Rotate(0, 0, 180);
                newMeleeAttack.GetComponent<Slash>().origin = "player";
            }
            else if (angle > -135f && angle < -45f)
            {
                // Debug.Log("Down");
                // instantiate a new melee attack object below this object
                // Instantiate(meleeAttack, transform.position + new Vector3(0, -1, 0), Quaternion.identity);

                GameObject newMeleeAttack = Instantiate(meleeAttack, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
                newMeleeAttack.transform.Rotate(0, 0, 270);
                newMeleeAttack.GetComponent<Slash>().origin = "player";
            }
        }
    }
}
