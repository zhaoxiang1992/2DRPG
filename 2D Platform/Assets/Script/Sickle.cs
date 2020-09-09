using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickle : MonoBehaviour
{
    public float speed;
    public int damage;
    public float rotateSpeed;
    public float tuning;
    public float sickleStayTime;

    private Rigidbody2D rb2d;
    private Transform playerTransform;
    private Transform sickleTransform;
    private Vector2 startSpeed;
    private bool isSickleStop;

    private CameraShake camShake;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.right * speed;
        startSpeed = rb2d.velocity;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sickleTransform = GetComponent<Transform>();
        camShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
        isSickleStop = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 0, rotateSpeed);
        if (!isSickleStop)
        {
            sickleFlyOut();
        }
        else
        {
            sickleFlyBack();
        }
    }

    void sickleFlyOut()
    {
        rb2d.velocity = rb2d.velocity - startSpeed * Time.deltaTime;
        Invoke("sickleStay", sickleStayTime);
    }

    void sickleStay()
    {
        isSickleStop = true;
    }

    void sickleFlyBack()
    {
        //tuning = 1 / ((transform.position - playerTransform.position).magnitude);
        //transform.position = Vector3.Slerp(transform.position, playerTransform.transform.position, tuning);
        transform.position = Vector3.Lerp(transform.position, playerTransform.position, tuning);
        //float y = Mathf.Lerp(transform.position.y, playerTransform.position.y, tuning);
        //transform.position = new Vector3(transform.position.x, y, 0.0f);
        if (Mathf.Abs(transform.position.x - playerTransform.position.x) < 0.5f &&
            Mathf.Abs(transform.position.y - playerTransform.position.y) < 0.5f)
        {
            SickleUI.CurrentSickleQuantity++;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            camShake.Shake();
        }
    }
}
