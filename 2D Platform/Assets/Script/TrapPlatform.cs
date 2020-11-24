using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlatform : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D bx2d;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        bx2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            anim.SetTrigger("Collepse");
        }
    }

    void DisableBoxCollider2D() 
    {
        bx2d.enabled = false;
    }

    void DestroyTrapPlatform()
    {
        Destroy(gameObject);
    }
}
