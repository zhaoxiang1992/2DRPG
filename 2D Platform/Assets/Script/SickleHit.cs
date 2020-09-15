using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickleHit : MonoBehaviour
{
    public GameObject sickle;

    private PlayerInputActions controls;


    void Awake()
    {
        controls = new PlayerInputActions();
        controls.GamePlay.Item.started += ctx => Shoot();
    }

    void OnEnable()
    {
        controls.GamePlay.Enable();
    }

    void OnDisable()
    {
        controls.GamePlay.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Shoot();
    }
    void Shoot()
    {
        if (SickleUI.CurrentSickleQuantity > 0)
        {
            //if (Input.GetKeyDown(KeyCode.Q))
            {
                Instantiate(sickle, transform.position, transform.rotation);
                SickleUI.CurrentSickleQuantity--;
            }
        }
    }
}
