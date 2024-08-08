using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
// using UnityEditorInternal;
// using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
   
    public float maxSpeed = 1.0f;
    public CharacterController charController;
    public Camera camera;
    Animator anim;

    // Start is called before the first frame update

    private float gravityY = 0.0f;
    public float mass = 1.0f;

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
        else
            UnityEngine.Cursor.lockState = CursorLockMode.None;
    }
    void Start()
    {
        charController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetFloat("Alert") > 0.0f)
        {
            anim.SetFloat("Alert", anim.GetFloat("Alert") - 1.0f * Time.deltaTime);
        }
        float dX = 0.0f;
        float dY = 0.0f;


        dX = Input.GetAxis("Horizontal");
        dY = Input.GetAxis("Vertical");



        Vector3 nonNormalizedMovementVector = new Vector3(dX, 0, dY);

        Vector3 movementVector = nonNormalizedMovementVector;
        movementVector = Quaternion.AngleAxis(camera.transform.eulerAngles.y, Vector3.up) * movementVector;
        movementVector.Normalize();
        movementVector = movementVector * maxSpeed;

        gravityY += Physics.gravity.y * mass * Time.deltaTime;

        if (charController.isGrounded)
        {
            gravityY = -0.5f;

        }

        Vector3 newMoveVector = movementVector;
        newMoveVector.y = gravityY;

        Physics.SyncTransforms();
       
        if (movementVector != Vector3.zero)
        {

            if (Input.GetKeyDown(KeyCode.Space) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Jump"))
            {
                anim.SetBool("Jumping", true);
                anim.SetFloat("Alert", anim.GetFloat("Alert") + 20.0f );
            }
            else
            {
                anim.SetBool("Jumping", false);
            }

            anim.SetBool("Moving", true);

            Quaternion rotationDirection = Quaternion.LookRotation(movementVector, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationDirection, 360 * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Moving", false);
        }
    }


    private void OnAnimatorMove()
    {
        Vector3 _move = anim.deltaPosition;
        _move.y = gravityY;
        charController.Move(_move);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "pillar")
        {
            anim.SetBool("Win", true);
        }
    }

}
