using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickleHit : MonoBehaviour
{
    public GameObject sickle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SickleUI.CurrentSickleQuantity > 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Instantiate(sickle, transform.position, transform.rotation);
                SickleUI.CurrentSickleQuantity --;
            }
        }
    }
    //void Shoot()
    //{
    //    Instantiate(sickle, transform.position, transform.rotation);
    //}
}
