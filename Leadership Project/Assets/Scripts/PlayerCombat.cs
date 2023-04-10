using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Stats")]
    public int health = 5;
    public float cooldown = 1f;
    public float invincibilityTime = 1f;
    float currentCooldown = 0f;
    float currentInvincibilityTime = 0f;

    [Header("Attack")]
    public GameObject meleeAttack;
    public bool canAttack = true;

    [Header("UI")]
    public GameObject gameOverUI;


    public void TakeDamage(int damage)
    {
        if (currentInvincibilityTime > 0)
        {
            return;
        }

        Debug.Log("ow");
        health -= damage;
        currentInvincibilityTime = invincibilityTime;

        if (health <= 0)
        {
            
            // TODO: handle gameover
            gameOverUI.SetActive(true);
            gameObject.SetActive(false);

        }
        else
        {
            // make the character sprite blink red
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

    public void Respawn()
    {
        health = 5;
        gameObject.SetActive(true);
        gameOverUI.SetActive(false);
        gameObject.transform.position = new Vector3(0.5f, 0.5f, 0);
    }

    public void BackToMenu()
    {
        gameOverUI.SetActive(false);
        gameObject.SetActive(false);
        //go back to main menu
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        // if cooldown is not zero, reduce it by the time since the last frame
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (currentInvincibilityTime > 0)
        {
            currentInvincibilityTime -= Time.deltaTime;
        }

        // detect a mouse click and determine if it happened to the left, right, top, or bottom of this object
        if (Input.GetMouseButtonDown(0) && canAttack && currentCooldown <= 0)
        {
            currentCooldown = cooldown;
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
