using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SickleUI : MonoBehaviour
{
    public int startSickleQuantity;
    public Text sickleQuantity;

    public static int CurrentSickleQuantity;
    // Start is called before the first frame update
    void Start()
    {
        CurrentSickleQuantity = startSickleQuantity;
    }

    // Update is called once per frame
    void Update()
    {
        sickleQuantity.text = "x  " + CurrentSickleQuantity.ToString();
    }
}
