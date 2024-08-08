using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesText : MonoBehaviour
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
        text.text = GameObject.Find("Bryce").GetComponent<Animator>().GetInteger("Lives").ToString();
    }
}
