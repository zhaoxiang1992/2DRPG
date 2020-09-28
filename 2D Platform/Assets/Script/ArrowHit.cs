using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHit : MonoBehaviour
{
    public GameObject arrowPrefeb;

    private PlayerInputActions controls;


    void Awake()
    {
        controls = new PlayerInputActions();
        controls.GamePlay.ArrowHit.started += ctx => Shoot();
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
        
    }
    void Shoot()
    {
        Instantiate(arrowPrefeb, transform.position,transform.rotation);
    }
}
