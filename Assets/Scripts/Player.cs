using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Public fields:
    [SerializeField] float maxHealth;
    [SerializeField] float maxEnergy;

    [SerializeField] float forwardSpeed, strafeSpeed, hoverSpeed;
    [SerializeField] float forwardAcceleration, strafeAcceleration, hoverAcceleration;

    [SerializeField] float lookRotateSpeed;
    [SerializeField] float rollSpeed, rollAcceleration;

    [SerializeField] ParticleSystem ps;
    [SerializeField] GameObject render;
    public GameObject[] hardpoints;


    //Public properties:
    public static float Health { get { return _health; } set { if (value > 0) _health = value; else _health = 0; } }
    public static float Energy { get { return _energy; } set { if (value > 0) _energy = value; else _energy = 0; } }
    public static float Score { get { return _score; } set { if (value > 0) _score = value; else _score = 0; } }
    public static float HighScore { get { return _highScore; } set { if (value > 0) _highScore = value; else _highScore = 0; } }
    public static Vector3 Position { get; private set; }
    public static Camera playerCamera { get; private set; }


    //Private fields:
    private Rigidbody rb;

    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;

    private Vector2 lookInput, screenCenter, mouseDistance;
    private float rollInput;

    private static float _health;
    private static float _energy;
    private static float _score;
    private static float _highScore;

    private float isMouseInvertedYAxis;
    private float isMouseInvertedXAxis;

    void Awake()
    {
        ps.Stop();
        rb = GetComponent<Rigidbody>();

        if (SerializeManager.Instance.GetBool(BoolType.MouseInversionYAxis))
            isMouseInvertedYAxis = -1f;
        else
            isMouseInvertedYAxis = 1f;

        if (SerializeManager.Instance.GetBool(BoolType.MouseInversionXAxis))
            isMouseInvertedXAxis = -1f;
        else
            isMouseInvertedXAxis = 1f;

        Health = maxHealth;
        Energy = maxEnergy;
        HighScore = SerializeManager.Instance.GetFloat(FloatType.HighScore);

        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;

        playerCamera = GetComponentInChildren<Camera>();

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

        rb.AddTorque(transform.up * (isMouseInvertedXAxis * mouseDistance.x) * lookRotateSpeed);
        rb.AddTorque(transform.right * (isMouseInvertedYAxis * -mouseDistance.y) * lookRotateSpeed);

        if (Input.GetKey(SerializeManager.Instance.GetControls(ControlsType.RollRight)) & !Input.GetKey(SerializeManager.Instance.GetControls(ControlsType.RollLeft)))
            rollInput = Mathf.Lerp(rollInput, -rollSpeed, rollAcceleration);
        else if (Input.GetKey(SerializeManager.Instance.GetControls(ControlsType.RollLeft)) & !Input.GetKey(SerializeManager.Instance.GetControls(ControlsType.RollRight)))
            rollInput = Mathf.Lerp(rollInput, rollSpeed, rollAcceleration);
        else
            rollInput = Mathf.Lerp(rollInput, 0f, rollAcceleration);

        rb.AddTorque(transform.forward * rollInput);
    }

    void Move()
    {
        if(Input.GetKey(SerializeManager.Instance.GetControls(ControlsType.FlyForward)) & !Input.GetKey(SerializeManager.Instance.GetControls(ControlsType.FlyBackward)))
            activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, forwardSpeed, forwardAcceleration);
        else if (Input.GetKey(SerializeManager.Instance.GetControls(ControlsType.FlyBackward)) & !Input.GetKey(SerializeManager.Instance.GetControls(ControlsType.FlyForward)))
            activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, -forwardSpeed, forwardAcceleration);
        else
            activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, 0f, forwardAcceleration);

        if (Input.GetKey(SerializeManager.Instance.GetControls(ControlsType.FlyRight)) & !Input.GetKey(SerializeManager.Instance.GetControls(ControlsType.FlyLeft)))
            activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, strafeSpeed, strafeAcceleration);
        else if (Input.GetKey(SerializeManager.Instance.GetControls(ControlsType.FlyLeft)) & !Input.GetKey(SerializeManager.Instance.GetControls(ControlsType.FlyRight)))
            activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, -strafeSpeed, strafeAcceleration);
        else
            activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, 0f, strafeAcceleration);

        if (Input.GetKey(SerializeManager.Instance.GetControls(ControlsType.FlyUp)) & !Input.GetKey(SerializeManager.Instance.GetControls(ControlsType.FlyDown)))
            activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, hoverSpeed, hoverAcceleration);
        else if (Input.GetKey(SerializeManager.Instance.GetControls(ControlsType.FlyDown)) & !Input.GetKey(SerializeManager.Instance.GetControls(ControlsType.FlyUp)))
            activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, -hoverSpeed, hoverAcceleration);
        else
            activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, 0f, hoverAcceleration);

        rb.AddForce(transform.forward * activeForwardSpeed);
        rb.AddForce(transform.right * activeStrafeSpeed);
        rb.AddForce(transform.up * activeHoverSpeed);
    }
    
    IEnumerator DieCoroutine()
    {
        if (Health <= 0)
        {
            ps.Play();
            Destroy(render);
            foreach (GameObject h in hardpoints)
                Destroy(h);
            AudioManager.Instance.PlaySound("Explosion");
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
        SerializeManager.Instance.SetFloat(FloatType.HighScore, HighScore);
    }

    void OnDisable()
    {
        SerializeManager.Instance.SetFloat(FloatType.HighScore, HighScore);
    }
}