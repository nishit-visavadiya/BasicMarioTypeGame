using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform;

    public Rigidbody rigidbodyComponent;

    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private int superJumpRemaining;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        
    }

    void FixedUpdate()
    {
        rigidbodyComponent.velocity = new Vector3(horizontalInput * 2, rigidbodyComponent.velocity.y, 0);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length == 1)
        {
            return;
        }

        if (jumpKeyWasPressed)
        {
            float jumpPower = 3f;
            if (superJumpRemaining > 0)
            {
                jumpPower *= 2;
                superJumpRemaining--;
            }

            rigidbodyComponent.AddForce(Vector3.up * 7, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
            Destroy(other.gameObject);
            superJumpRemaining++;
        }

        if(other.gameObject.tag == "EndLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
