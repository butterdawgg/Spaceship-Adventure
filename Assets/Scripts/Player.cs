using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Public fields:
    public ParticleSystem ps;
    public GameObject render;
    public float maxHealth;
    public float maxEnergy;

    public float forwardSpeed, strafeSpeed, hoverSpeed;
    public float forwardAcceleration, strafeAcceleration, hoverAcceleration;

    public float lookRotateSpeed;
    public float rollSpeed, rollAcceleration;
    public GameObject[] hardpoints;


    //Public properties:
    public static float Health { get { return _health; } set { if (value > 0) _health = value; else _health = 0; } }
    public static float Energy { get { return _energy; } set { if (value > 0) _energy = value; else _energy = 0; } }
    public static float Score { get { return _score; } set { if (value > 0) _score = value; else _score = 0; } }
    public static float HighScore { get { return _highScore; } set { if (value > 0) _highScore = value; else _highScore = 0; } }
    public static Vector3 Position { get; private set; }


    //Private fields:
    private Rigidbody rb;

    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;

    private Vector2 lookInput, screenCenter, mouseDistance;
    private float rollInput;

    private static float _health;
    private static float _energy;
    private static float _score;
    private static float _highScore;

    private float isMouseInverted;
    private int isRollAndRightLeftMovementSwapped;

    void Awake()
    {
        ps.Stop();
        rb = GetComponent<Rigidbody>();

        if (PlayerPrefs.GetInt("IsMouseInverted") == 0)
            isMouseInverted = 1f;
        else
            isMouseInverted = -1f;

        isRollAndRightLeftMovementSwapped = PlayerPrefs.GetInt("IsRollAndRightLeftMovementSwapped");
        Health = maxHealth;
        Energy = maxEnergy;
        HighScore = PlayerPrefs.GetFloat("HighScore");

        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;

        StartCoroutine(DieCoroutine());
    }

    void FixedUpdate()
    {
        Position = transform.position;

        if (Energy > maxEnergy)
            Energy = maxEnergy;

        if (Health <= 0)
            return;
        else if (Health > maxHealth)
            Health = maxHealth;

        Rotate();
        Move();
    }

     public void Rotate()
     {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;
        

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.x;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rb.AddTorque(transform.up * mouseDistance.x * lookRotateSpeed);
        rb.AddTorque(transform.right * (isMouseInverted * -mouseDistance.y) * lookRotateSpeed);

        if(isRollAndRightLeftMovementSwapped == 0)
            rollInput = Mathf.Lerp(rollInput, -Input.GetAxisRaw("Horizontal") * rollSpeed, rollAcceleration * Time.deltaTime);
        else
            rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll") * rollSpeed, rollAcceleration * Time.deltaTime);

        rb.AddTorque(transform.forward * rollInput * rollSpeed);
     }

    void Move()
    {
        activeForwardSpeed = Mathf.Lerp(
             activeForwardSpeed,
             Input.GetAxisRaw("Vertical") * forwardSpeed,
             forwardAcceleration * Time.deltaTime
             );

        if(isRollAndRightLeftMovementSwapped == 0)
        {
            activeStrafeSpeed = Mathf.Lerp(
            activeStrafeSpeed,
            -Input.GetAxisRaw("Roll") * forwardSpeed,
            strafeAcceleration * Time.deltaTime
            );
        }
        else
        {
            activeStrafeSpeed = Mathf.Lerp(
            activeStrafeSpeed,
            Input.GetAxisRaw("Horizontal") * forwardSpeed,
            strafeAcceleration * Time.deltaTime
            );
        }

        activeHoverSpeed = Mathf.Lerp(
            activeHoverSpeed,
            Input.GetAxisRaw("Hover") * forwardSpeed,
            hoverAcceleration * Time.deltaTime
            );

        rb.AddForce(transform.forward * activeForwardSpeed);
        rb.AddForce(transform.right * activeStrafeSpeed);
        rb.AddForce(transform.up * activeHoverSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "powerCell")
        {
            Health += 50f;
            Destroy(other.gameObject);
        }
    }
    
    IEnumerator DieCoroutine()
    {
        if (Health <= 0)
        {
            ps.Play();
            Destroy(render);
            for (int i = 0; i < hardpoints.Length; i++)
                Destroy(hardpoints[i]);
            FindObjectOfType<AudioManager>().PlaySound("Explosion");
            yield return new WaitForSeconds(2f);
            if (HighScore < Score)
                HighScore = Score;
            Score = 0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        yield return null;
        StartCoroutine(DieCoroutine());
    }

    public void ResetHighScore()
    {
        HighScore = 0f;
        PlayerPrefs.SetFloat("HighScore", HighScore);
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat("HighScore", HighScore);
    }
}