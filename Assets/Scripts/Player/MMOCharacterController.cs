using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMOCharacterController : MonoBehaviour
{
    public Transform character, centerPoint;


    private float moveFB, moveLR;
    public float moveSpeed = 2f;

    public float rotationSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        moveFB = Input.GetAxis("Vertical");
        moveLR = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveLR, 0, moveFB);
        movement = character.rotation * movement;

        character.GetComponent<CharacterController>().Move(movement * Time.deltaTime);
        centerPoint.position = new Vector3(character.position.x, centerPoint.position.y, character.position.z);

        float inputV = Input.GetAxis("Vertical");
        if (inputV < 0 | inputV > 0)
        {
            Quaternion turnAngle = Quaternion.Euler(0, centerPoint.eulerAngles.y, 0);
            character.rotation = Quaternion.Slerp(character.rotation, turnAngle, Time.deltaTime * rotationSpeed);
        }

    }
}
