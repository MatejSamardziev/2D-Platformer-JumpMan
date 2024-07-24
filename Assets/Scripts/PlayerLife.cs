using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private AudioSource DeathSoundEffect;
    private bool collidedWithWall = false;
    private bool collidedWithBlock = false;
 

    void Start()
    {

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Trap"))
        {
            Die();
            
            
        }

        AngryBlockChecker(collision);


        if (collision.gameObject.tag.Equals("Wall"))
        {
            collidedWithWall = true;
        }
        if(collidedWithWall && collidedWithBlock)
        {
            Die();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            collidedWithWall = false;
        }
        if (collision.gameObject.tag.Equals("AngryBlockUD")||collision.gameObject.tag.Equals("AngryBlockLR"))
        {
            collidedWithBlock = false;
        }
    }

    private void Die()
    {
        anim.SetTrigger("death");
        rb.bodyType = RigidbodyType2D.Static;
        //sprite.enabled = false;
        DeathSoundEffect.Play();
        
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);// go restartira levelot
    }


    private void AngryBlockChecker(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("AngryBlockLR"))
        {
            // Check if the collision normal is pointing towards the side of the block
            Vector2 contactPoint = collision.GetContact(0).point;
            Vector2 collisionNormal = collision.GetContact(0).normal;

            if (Mathf.Abs(Vector2.Dot(collisionNormal, Vector2.up)) < 0.5f)
            {
                // The collision is not from the top, so the player is touching the side of the block
                collidedWithBlock = true;
            }
        }

        else if (collision.gameObject.tag.Equals("AngryBlockUD"))
        {
            // Check if the collision normal is pointing towards the side of the block
            Vector2 contactPoint = collision.GetContact(0).point;
            Vector2 collisionNormal = collision.GetContact(0).normal;

            if (Mathf.Abs(Vector2.Dot(collisionNormal, Vector2.up)) > 0.5f)
            {
                // The collision is not from the top, so the player is touching the side of the block
                collidedWithBlock = true;
            }
        }
    }

}

    
