using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public string origin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if my animation is complete, destroy me
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GetComponent<Animator>().IsInTransition(0))
        {
            Destroy(gameObject);
        }
    }

    // private void OnCollisionStay2D(Collider2D collision)
    // {
    //     Debug.Log(gameObject.name);
    //     Debug.Log(collision.gameObject.name);
    //     // if I hit an enemy, damage them
    //     if (collision.gameObject.tag == "Enemy")
    //     {
    //         collision.gameObject.GetComponent<Enemy>().TakeDamage(1);
    //     }
    // }

    // collider is not being detected upon collision with non-trigger objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(gameObject.name);
        Debug.Log(collision.gameObject.name);
        // if I hit an enemy, damage them
        if (origin == "player" && collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(1);
        }
    }

}
