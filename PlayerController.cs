using UnityEngine;





public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f; // Speed at which the player moves
    public float rotationSpeed = 55.0f; // Speed at which the player rotates

    private void Update()
    {
        // Action Buttons on Right Controller
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            // Insert the action to be performed when the button is pressed
        }


        //-----------------------------------
        // Player Movement Code Begins
        //-----------------------------------


        Vector3 forwardDirection = transform.forward;
        // Gather input from the right thumbstick for movement
        Vector2 movementThumbstick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick); // Right-hand controller
        Vector3 moveDirection = (forwardDirection * movementThumbstick.y) + (transform.right * movementThumbstick.x);
        float moveMagnitude = Mathf.Clamp01(movementThumbstick.magnitude);

        // Gather input from the left thumbstick for rotation
        Vector2 rotationThumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick); // Left-hand controller
        float rotateHorizontal = rotationThumbstick.x;

        // Move the player forward, backward, left, or right
        Vector3 movement = moveDirection.normalized * speed * moveMagnitude * Time.deltaTime;
        transform.Translate(movement, Space.World);

        // Rotate the player left or right based on the horizontal input from the left thumbstick
        float rotation = rotateHorizontal * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);

        //-----------------------------------
        // Player Movement Code Ends
        //-----------------------------------
    }
}