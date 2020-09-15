using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBinItem : MonoBehaviour
{
    private bool isPlayerInTrashBin;
    private PlayerInputActions controls;
    void Awake()
    {
        controls = new PlayerInputActions();
        controls.GamePlay.Interactive.started += ctx => TrowCoin();
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
        //TrowCoin();
    }

    void TrowCoin()
    { 
        //if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPlayerInTrashBin)
            {
                if (CoinUI.CurrentCoinQuantity > 0)
                {
                    SoundManager.PlayTrowCoinClip();
                    TrashBinCoin.coinCurrent++;
                    CoinUI.CurrentCoinQuantity--;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") &&
            other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInTrashBin = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") &&
            other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInTrashBin = false;
        }
    }
}
