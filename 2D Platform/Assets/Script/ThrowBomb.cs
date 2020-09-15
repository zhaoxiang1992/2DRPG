using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBomb : MonoBehaviour
{
    public GameObject bomb;

    private PlayerInputActions controls;

    void Awake()
    {
        controls = new PlayerInputActions();
        controls.GamePlay.Item.started += ctx => TrowBomb();
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
        //TrowBomb();
    }

    void TrowBomb()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(bomb, transform.position, transform.rotation);
        }
    }
}
