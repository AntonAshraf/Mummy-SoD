using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MummyMove : MonoBehaviour
{
    public float timer = 0.0f;
    Animator anim;
    System.Random rand = new System.Random();
    public Animator bryceAnim;
    public float rotationSpeed = 90f; // Adjust the rotation speed here
    private bool isRotating = false; // Flag to indicate if rotation is in progress
    public Light light;
    bool GoRotate = false;
    int enterCoroutine = 0;
    bool found = false;
    bool first = true;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (bryceAnim.GetBool("Win") == true)
        {
            anim.Play("Die");
            return;
        }
        
        timer -= Time.deltaTime;

        if (GoRotate == false)   // time for rotation
        {
            if (found == true)
            {
                bryceAnim.SetFloat("Alert", bryceAnim.GetFloat("Alert") + 100.0f);
                bryceAnim.SetInteger("Lives", bryceAnim.GetInteger("Lives") - 1);
                found = false;
            }
            int x = rand.Next(10, 60);
            // int x = 5; // for testing
            timer = x;
            anim.SetBool("Idle", true);
            first = true;
            GoRotate = true;
        }
        if (GoRotate == true && timer < 1.0f)
        {
            anim.SetBool("Idle", false);
            rotationSpeed = 90f + bryceAnim.GetFloat("Alert");
            if (enterCoroutine == 0)
            {
                StartCoroutine(RotateObjectBy180Degrees(rotationSpeed));
                enterCoroutine = 1;
            }
            if (!isRotating)
            {
                if (first == true)
                {
                    anim.Play("Look_Around");
                    first = false;
                }

                if (!found)
                {
                    if (bryceAnim.GetBool("Moving"))
                    {
                        anim.SetBool("dance", true);
                        light.color = Color.red;
                        found = true;
                        bryceAnim.SetBool("Hit", true);
                        if (bryceAnim.GetInteger("Lives") == 1)
                        {
                            bryceAnim.SetInteger("Lives", 0);
                        }
                        
                    }
                }

                if (enterCoroutine == 1 && timer < -7.0f)
                {
                    bryceAnim.SetBool("Hit", false);
                    light.color = Color.green;
                    StartCoroutine(RotateObjectBy180Degrees(rotationSpeed));
                    GoRotate = false;
                    enterCoroutine = 0;
                }
            }
        }
    }
    IEnumerator RotateObjectBy180Degrees(float rotationSpeed)
    {
        if (!isRotating)
        {
            isRotating = true;

            float startRotation = transform.rotation.eulerAngles.y; // Get the initial rotation
            float targetRotation = startRotation + 180f; // Calculate the target rotation (180 degrees)
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime * (rotationSpeed / 180f); // Calculate the interpolation factor

                // Interpolate the rotation smoothly
                float newRotation = Mathf.Lerp(startRotation, targetRotation, t);
                transform.rotation = Quaternion.Euler(0f, newRotation, 0f);

                yield return null;
            }

            isRotating = false;
        }
    }


}