using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Public fields:
    public ParticleSystem ps;
    public GameObject render;
    public float health;

    public float forwardSpeed, strafeSpeed, hoverSpeed;
    public float forwardAcceleration, strafeAcceleration, hoverAcceleration;

    public float lookRotateSpeed;
    public float rollSpeed, rollAcceleration;
    public GameObject[] hardpoints;


    //Public properties:
    public static float Health { get; private set; }
    public static Vector3 Position { get; private set; }
    public static float Score { get; set; }
    public static float HighScore { get; set; }

    
    //Private fields:
    private Rigidbody rb;

    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;

    private Vector2 lookInput, screenCenter, mouseDistance;
    private float rollInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;
        StartCoroutine(DieCoroutine());
        ps.Stop();
    }

    void FixedUpdate()
    {
        Health = health;
        Position = transform.position;

        if (health <= 0)
            return;

        Rotate();
        Move();
    }

    void Rotate()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.x;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rb.AddTorque(transform.up * mouseDistance.x * lookRotateSpeed);
        rb.AddTorque(transform.right * -mouseDistance.y * lookRotateSpeed);

        rollInput = Mathf.Lerp(rollInput, -Input.GetAxisRaw("Horizontal") * rollSpeed, rollAcceleration * Time.deltaTime);

        rb.AddTorque(transform.forward * rollInput * rollSpeed);
    }

    void Move()
    {
        activeForwardSpeed = Mathf.Lerp(
             activeForwardSpeed,
             Input.GetAxisRaw("Vertical") * forwardSpeed,
             forwardAcceleration * Time.deltaTime
             );

        activeStrafeSpeed = Mathf.Lerp(
            activeStrafeSpeed,
            -Input.GetAxisRaw("Roll") * forwardSpeed,
            strafeAcceleration * Time.deltaTime
            );

        activeHoverSpeed = Mathf.Lerp(
            activeHoverSpeed,
            Input.GetAxisRaw("Hover") * forwardSpeed,
            hoverAcceleration * Time.deltaTime
            );

        rb.AddForce(transform.forward * activeForwardSpeed);
        rb.AddForce(transform.right * activeStrafeSpeed);
        rb.AddForce(transform.up * activeHoverSpeed);
    }

    IEnumerator DieCoroutine()
    {
        if (health <= 0)
        {
            ps.Play();
            Destroy(render);
            for (int i = 0; i < hardpoints.Length; i++)
                Destroy(hardpoints[i]);
            yield return new WaitForSeconds(2f);
            if (HighScore < Score)
                HighScore = Score;
            Score = 0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        yield return null;
        StartCoroutine(DieCoroutine());
    }
}