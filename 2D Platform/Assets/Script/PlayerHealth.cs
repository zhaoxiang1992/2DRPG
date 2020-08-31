using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int blinks;
    public float time;
    public float invincibleTime;
    public float dieTime;
    public float hitBoxCDTime;

    private Renderer myRender;
    private bool isInvincible = false;
    private Animator anim;
    private ScreenFlash sf;
    private Rigidbody2D rb2d;
    private PolygonCollider2D polygonCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.HealthMax = health;
        HealthBar.HealthCurrent = health;
        myRender = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        sf = GetComponent <ScreenFlash>();
        rb2d = GetComponent<Rigidbody2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damage) 
    {
        if (!isInvincible)
        {
            sf.FlashScreen();
            health -= damage;
            HealthBar.HealthCurrent = health;
            if (health <= 0)
            {
                health = 0;
                HealthBar.HealthCurrent = health;
                rb2d.velocity = new Vector2(0, 0);
                //rb2d.gravityScale = 0.0f;
                GameController.isGameAlive = false;
                anim.SetTrigger("Die");
                polygonCollider2D.enabled = false;
                Invoke("KillPlayer", dieTime);
            }
            else
            {
                isInvincible = true;
                BlinkPlayer(blinks, time);
                polygonCollider2D.enabled = false;
                StartCoroutine(ShowPlayerHitBox());
                Invoke("Invincible", invincibleTime);
            }
        }
    }

    IEnumerator ShowPlayerHitBox()
    {
        yield return new WaitForSeconds(hitBoxCDTime);
        polygonCollider2D.enabled = true;
    }

    void KillPlayer() 
    {
        Destroy(gameObject);
    }

    void BlinkPlayer(int numBlink, float seconds)
    {
        StartCoroutine(DoBlinks(numBlink, seconds));
    }

    IEnumerator DoBlinks(int numBlink, float second)
    {
        for (int i = 0; i < numBlink * 2; i++)
        {
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(second);
        }
        myRender.enabled = true;
    }

    void Invincible()
    {
        isInvincible = false;
        //Debug.Log("isInvincible"+isInvincible.ToString());
    }
}
