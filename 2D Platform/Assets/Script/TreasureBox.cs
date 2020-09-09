using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public GameObject treasure;
    public float dropTime;
    public float treasureXSpeed;
    public float treasureYSpeed;
    private bool canOpen;
    private bool isOpened;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        isOpened = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canOpen && !isOpened)
            {
                anim.SetTrigger("Opening");
                isOpened = true;
                Invoke("DropTreasure", dropTime);
            }
        }
    }

    void DropTreasure() 
    {
        GameObject gb = Instantiate(treasure, transform.position, Quaternion.identity) as GameObject;
        gb.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-treasureXSpeed, treasureXSpeed), treasureYSpeed), ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = false;
        }
    }
}
