using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(Random.Range(50, 200), 0, Random.Range(60, 100));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
