using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 5;
    public int health = 5;
    public int attack = 1;
    public int defense = 0;
    public float cooldown = 1f;
    public float invincibilityTime = 1f;
    float currentCooldown = 0f;
    float currentInvincibilityTime = 0f;

    [Header("Attack")]
    public GameObject meleeAttack;
    public bool canAttack = true;

    [Header("UI")]
    public GameObject gameOverUI;
    public Transform healthBar;
    public GameObject buffUI;

    [HideInInspector]
    public int armorValue;
    [HideInInspector]
    public int weaponValue;
    [HideInInspector]
    public int shieldValue;


    public void TakeDamage(int damage)
    {
        if (currentInvincibilityTime > 0)
        {
            return;
        }

        Debug.Log("ow");
        health -= damage;
        currentInvincibilityTime = invincibilityTime + shieldValue;

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
        health = maxHealth;
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

    public void Heal(int healAmount, bool regeneration = false, float interval = 10f, int limit = 1)
    {
        if (regeneration)
        {
            StartCoroutine(RegenerateHealth(healAmount, interval, limit));
        }
        else
        {
            health += healAmount;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }
        
    }

    public void BuffInterface(string buffType)
    {
        if (buffType == "attack")
        {
            StartCoroutine(BuffAttack());
        }
        else if (buffType == "invincibility")
        {
            StartCoroutine(BuffInvincibility());
        }
        else if (buffType == "regeneration")
        {
            StartCoroutine(RegenerateHealth(1, 10f, 999));
        }
    }

    public IEnumerator RegenerateHealth(int healAmount=1, float interval=10f, int limit=1)
    {
        buffUI.transform.GetChild(0).gameObject.SetActive(true);
        while (limit > 0)
        {
            Heal(healAmount);
            limit--;
            yield return new WaitForSeconds(interval);
        }
        
        // deactivate the buff UI's Regen child
        buffUI.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Buff a player's attack by 2 for 1 minute
    public IEnumerator BuffAttack()
    {
        buffUI.transform.GetChild(2).gameObject.SetActive(true);
        attack += 2;
        yield return new WaitForSeconds(60f);
        attack -= 2;
        buffUI.transform.GetChild(2).gameObject.SetActive(false);
    }

    // Raise invincibility time by 3 seconds
    public IEnumerator BuffInvincibility()
    {
        buffUI.transform.GetChild(1).gameObject.SetActive(true);
        invincibilityTime += 3f;
        yield return new WaitForSeconds(60f);
        invincibilityTime -= 3f;
        buffUI.transform.GetChild(1).gameObject.SetActive(false);
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

            if (angle > -45f && angle < 45f)
            {
                // Debug.Log("Right");
                // instantiate a new melee attack object to the right of this object
                GameObject newMeleeAttack = Instantiate(meleeAttack, transform.position + new Vector3(0.5f, 0, 0), Quaternion.identity);
                newMeleeAttack.GetComponent<Slash>().origin = "player";
                newMeleeAttack.GetComponent<Slash>().source = gameObject;
            }
            else if (angle > 45f && angle < 135f)
            {
                // Debug.Log("Up");
                // instantiate a new melee attack object above this object and rotate it to face up
                GameObject newMeleeAttack = Instantiate(meleeAttack, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                newMeleeAttack.transform.Rotate(0, 0, 90);
                newMeleeAttack.GetComponent<Slash>().origin = "player";
                newMeleeAttack.GetComponent<Slash>().source = gameObject;
            }
            else if (angle > 135f || angle < -135f)
            {
                // Debug.Log("Left");
                // instantiate a new melee attack object to the left of this object
                GameObject newMeleeAttack = Instantiate(meleeAttack, transform.position + new Vector3(-0.5f, 0, 0), Quaternion.identity);
                newMeleeAttack.transform.Rotate(0, 0, 180);
                newMeleeAttack.GetComponent<Slash>().origin = "player";
                newMeleeAttack.GetComponent<Slash>().source = gameObject;
            }
            else if (angle > -135f && angle < -45f)
            {
                // Debug.Log("Down");
                // instantiate a new melee attack object below this object
                GameObject newMeleeAttack = Instantiate(meleeAttack, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
                newMeleeAttack.transform.Rotate(0, 0, 270);
                newMeleeAttack.GetComponent<Slash>().origin = "player";
                newMeleeAttack.GetComponent<Slash>().source = gameObject;
            }
        }

        for (int i = 0; i < maxHealth; i++)
        {
            if (i >= health)
            {
                healthBar.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                healthBar.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
