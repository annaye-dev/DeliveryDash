using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;

public class Driver : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1;
    [SerializeField] float rotationSpeed = 1;
    [SerializeField] float moveSpeed = 0;
    [SerializeField] float steerSpeed = 0;
    [SerializeField] float boostSpeed = 0;
    [SerializeField] float boostSpeedSaver = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] TMP_Text boostText;

    void Start()
    {
        if (boostText == null)
        {
            Debug.LogError(
                $"Assign Boost Text on the Driver component attached to '{gameObject.name}'.",
                this
            );
            return;
        }

        boostText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("House") && boostSpeed > 0)
        {
            boostSpeedSaver -= 1;
        }
        else
        {
            boostSpeedSaver = 0;
        }

        if (boostSpeedSaver == 0)
        {
            boostText.gameObject.SetActive(false);
        }
        Debug.Log("You collided!");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Speed Boost"))
        {
            boostSpeedSaver += 3;
            Destroy(collision.gameObject, 0.5f);
            boostText.gameObject.SetActive(true);
            Debug.Log("Got Speed Boost");
        }
    }

    void Update()
    {
        moveSpeed = 0;
        steerSpeed = 0;
        boostSpeed = 0;
        if (Keyboard.current.upArrowKey.isPressed)
        {
            moveSpeed = 5;
            boostSpeed = boostSpeedSaver;
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            moveSpeed = -5;
            boostSpeed = boostSpeedSaver;
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            steerSpeed = 150f;
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            steerSpeed = -150f;
        }
        float move = (moveSpeed * movementSpeed + boostSpeed) * Time.deltaTime;
        float steer = steerSpeed * rotationSpeed * Time.deltaTime;
        transform.Translate(0, move, 0);
        transform.Rotate(0, 0, steer);

        
    }
}
