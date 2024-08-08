using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameEndText : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Bryce").GetComponent<Animator>().GetInteger("Lives") == 0)
        {
            text.text = "You Died!";
        }
        else if (GameObject.Find("Bryce").GetComponent<Animator>().GetBool("Win"))
        {
            text.text = "Victory!";
        }
       
    }
}
