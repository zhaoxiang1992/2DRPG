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

    private Renderer myRender;
    private bool isInvincible = false;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.HealthMax = health;
        HealthBar.HealthCurrent = health;
        myRender = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damage) 
    {
        if (health < 0)
        {
            health = 0;
        }

        if (!isInvincible)
        {
            health -= damage;
            HealthBar.HealthCurrent = health;
            if (health <= 0)
            {
                anim.SetTrigger("Die");
                Invoke("KillPlayer", dieTime);
            }
            else
            {
                isInvincible = true;
                BlinkPlayer(blinks, time);
                Invoke("Invincible", invincibleTime);
            }
        }
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
