using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ObjectInteraction : MonoBehaviour
{
    public string englishName;
    public string germanName;
    AudioSource audioData;
    public float floatingAmplitude = 0.05f; // Height of the floating effect
    public TMP_Text textComponent;

    private Vector3 originalScale;
    private Vector3 zoomedScale;
    private Quaternion originalRotation; // Variable to store the original rotation

    private Vector3 initialPosition; // Initial position of the object
    private Vector3 targetPosition;  // Target position for floating
    private float lerpTime = 0f;     // Timer for Lerp
    public float lerpDuration = 2f;  // Duration to reach the target position

    public float rotationSpeed = 90f; // Speed of rotation in degrees per second

    private bool isInteracting = false;
    private bool isTriggered = false; // To track whether the object is triggered

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        originalScale = transform.localScale;
        zoomedScale = originalScale * 1.5f; // Adjust the multiplier to control zoom level
        initialPosition = transform.position; // Store the initial position
        targetPosition = new Vector3(initialPosition.x, initialPosition.y + floatingAmplitude, initialPosition.z); // Set the target position
        originalRotation = transform.rotation; // Store the initial rotation
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isInteracting)
        {
            StartInteraction();
        }
        else if (isTriggered)
        {
            // Allow detriggering only if the object has reached the top height (isTriggered is true)
            DetriggerInteraction();
        }
        // Else, do nothing if the object is still in the process of reaching the top height
    }

    void Update()
    {
        if (isInteracting)
        {
            textComponent.transform.LookAt(Camera.main.transform);
            textComponent.transform.RotateAround(textComponent.transform.position, textComponent.transform.up, 180f);
            if (!isTriggered)
            {
                // If not yet triggered, float to target position
                lerpTime += Time.deltaTime;
                float progress = lerpTime / lerpDuration;
                transform.position = Vector3.Lerp(initialPosition, targetPosition, progress);

                if (progress >= 1f)
                {
                    isTriggered = true; // Start rotating and scaling once reached target position
                    lerpTime = 0f; // Reset lerp time for rotation phase
                }
            }
            else
            {
                // Rotate and scale once triggered
                transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
                transform.localScale = Vector3.Lerp(originalScale, zoomedScale, Mathf.PingPong(Time.time, 1));
            }
        }
    }

    private void StartInteraction()
    {
        Debug.Log("Enter detected");
        isInteracting = true;
        lerpTime = 0f; // Reset the lerp timer
        textComponent.transform.LookAt(Camera.main.transform);
        textComponent.transform.RotateAround(textComponent.transform.position, textComponent.transform.up, 180f);
        Debug.Log("Touched object name: " + englishName);
        textComponent.text = englishName + "\n" + germanName;
        audioData.Play(0);
    }

    private void DetriggerInteraction()
    {
        isInteracting = false;
        isTriggered = false;
        transform.position = initialPosition; // Return to the original position
        transform.localScale = originalScale; // Return to the original scale
        transform.rotation = originalRotation; // Return to the original rotation
        textComponent.text = ""; // Clear text
    }
}