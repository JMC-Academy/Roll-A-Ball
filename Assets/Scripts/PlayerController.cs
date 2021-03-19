using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;

    [Header("UI Stuff")]             
    public GameObject gameOverScreen;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;
    private int pickupCount;
    GameObject resetPoint;
    bool resetting = false;
    Color originalColour;
    bool grounded = true;

    //Controllers
    SoundController soundController;
    GameController gameController;
    Timer timer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        pickupCount = GameObject.FindGameObjectsWithTag("Pick Up").Length
                    + GameObject.FindGameObjectsWithTag("Bowling Pin").Length;
        gameOverScreen.SetActive(false);
        SetCountText();
        winText.text = "";
        resetPoint = GameObject.Find("Reset Point");
        originalColour = GetComponent<Renderer>().material.color;

        soundController = FindObjectOfType<SoundController>();

        gameController = FindObjectOfType<GameController>();
        timer = FindObjectOfType<Timer>();
        if (gameController.gameType == GameType.SpeedRun)
            StartCoroutine(timer.StartCountdown());
    }

    void FixedUpdate()
    {
        if (resetting)
            return;

        if (gameController.gameType == GameType.SpeedRun && !timer.IsTiming())
            return;

        if (gameController.controlType == ControlType.WorldTilt)
            return;

        if (grounded)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.AddForce(movement * speed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.GetComponent<Particles>().CreateParticles();
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            soundController.PlayPickupSound();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(ResetPlayer());
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            soundController.PlayCollisionSound(collision.gameObject);
            if (gameController.wallType == WallType.Punishing)
                StartCoroutine(ResetPlayer());
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            grounded = false;
    }

    IEnumerator ResetPlayer()
    {
        resetting = true;
        GetComponent<Renderer>().material.color = Color.black;
        rb.velocity = Vector3.zero;
        Vector3 startPos = transform.position;
        float resetSpeed = 2f;
        var i = 0.0f;
        var rate = 1.0f / resetSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startPos, resetPoint.transform.position, i);
            yield return null;
        }
        GetComponent<Renderer>().material.color = originalColour;
        resetting = false;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= pickupCount)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        gameOverScreen.SetActive(true);
        winText.text = "You Win!";
        soundController.PlayWinSound();

        if(gameController.gameType == GameType.SpeedRun)
            timer.StopTimer();

    }

    public void PinFall()
    {
        count += 1;
        SetCountText();
    }
}
