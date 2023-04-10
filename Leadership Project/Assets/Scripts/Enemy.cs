using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject meleeAttack;
    public AIDestinationSetter destinationSetter;

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
            StartCoroutine(BlinkRed());
        }
    }

    IEnumerator BlinkRed()
    {
        // get the sprite renderer component
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        // store the original color
        Color originalColor = spriteRenderer.color;
        // set the color to red
        spriteRenderer.color = Color.red;
        // wait for 0.1 seconds
        yield return new WaitForSeconds(0.1f);
        // set the color back to the original color
        spriteRenderer.color = originalColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player has entered the trigger");
            player = other.gameObject;
            destinationSetter.target = player.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player has exited the trigger");
            player = null;
            destinationSetter.target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.transform.position) < 2f)
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
