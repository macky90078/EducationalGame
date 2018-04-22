using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {

    [SerializeField]
    private Rigidbody m_rb;
    [SerializeField]
    private Transform m_target;

    [SerializeField]
    private GameObject m_bowHObject;

    [SerializeField]
    private GameObject m_arrowObject;

    [SerializeField]
    private float m_h = 1;
    [SerializeField]
    private float m_gravity = -9.81f;

    private float m_moveDist = 0;
    public float m_groundDist;

    public float m_drawTime = 1.2f;
    private float m_alloweBreathTime = 1.75f;

    [SerializeField]
    private bool m_debugPath;

    private bool m_isHoldingBreath = false;

    private Transform m_initBowPos;
    float timer = 0.2f;
    public float pullTimer = 0.5f;

    public float m_distanceFromBow;

    private bool m_scrollTargetBack = false;
    private bool m_aiming = false;
    public bool m_hasPulled = false;


    private void Awake()
    {
        m_initBowPos = transform;
    }

    // Update is called once per frame
    void Update () {

        RaycastHit hit;

        if (Physics.Raycast(m_bowHObject.transform.position, -Vector3.up, out hit, Mathf.Infinity))
        {
            Debug.Log("hit");
            m_groundDist = hit.distance;
            m_h = hit.distance;
        }


        if (Input.GetKey(KeyCode.Mouse0) && m_distanceFromBow <= 40 && !m_scrollTargetBack /*m_drawTime > 0*/)
        {
            m_distanceFromBow = Vector3.Distance(transform.position, m_target.position);
            m_target.position += transform.right * Time.deltaTime * 15f;
        }

        if (Input.GetKey(KeyCode.Mouse1) && m_distanceFromBow >= 1.5f)
        {
            m_distanceFromBow = Vector3.Distance(transform.position, m_target.position);
            m_target.position -= transform.right * Time.deltaTime * 15f;
            m_scrollTargetBack = true;
        }
        else
        {
            m_scrollTargetBack = false;
        }
        if (Input.GetKey(KeyCode.LeftShift) && m_alloweBreathTime > 0)
        {
            m_alloweBreathTime -= Time.deltaTime;
            m_isHoldingBreath = true;
        }
        else
        {
            m_isHoldingBreath = false;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_alloweBreathTime = 1.75f;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            m_aiming = true;
            pullTimer -= Time.deltaTime;
            if(pullTimer < 0)
            {
                m_hasPulled = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (m_hasPulled)
            {
                Launch();
                pullTimer = 0.5f;
                m_hasPulled = false;
            }
            else if(!m_hasPulled)
            {
                BadLaunch();
                pullTimer = 0.5f;
                m_hasPulled = false;
            }
            m_target.position = transform.position + transform.right * 1;
            m_drawTime = 1.2f;
            m_distanceFromBow = 0;
            m_aiming = false;
            if (m_debugPath)
            {
                DrawPath();
            }
        }

	}

    private void FixedUpdate()
    {
        bool hit = Physics.Raycast(m_bowHObject.transform.position, -transform.up, Mathf.Infinity);
        if (m_aiming)
        {
            if (!m_isHoldingBreath)
            {
                SwayBow();
            }
        }
    }

    private void SwayBow()
    {
        timer -= Time.deltaTime;
        if (timer > 0)
        {
            transform.Translate(Vector3.forward * (Time.deltaTime * 0.25f));
            m_target.Translate(Vector3.right * (Time.deltaTime * 5));
        }
        else if(timer < 0 && timer > -0.2f)
        {
            transform.Translate(-Vector3.forward * (Time.deltaTime * 0.25f));
            m_target.Translate(-Vector3.right * (Time.deltaTime * 5));
        }
        else if(timer <= -0.2f)
        {
            timer = 0.2f;
        }
    }

    private void Launch()
    {
        Physics.gravity = Vector3.up * m_gravity;
        GameObject arrow = Instantiate(m_arrowObject, transform.position, m_bowHObject.transform.rotation);
        m_rb = arrow.GetComponent<Rigidbody>();
        m_rb.useGravity = true;
        m_rb.velocity = CalculateLaunchData().initialVelocity;
    }

    private void BadLaunch()
    {
        GameObject arrow = Instantiate(m_arrowObject, transform.position, m_bowHObject.transform.rotation);
        m_rb = arrow.GetComponent<Rigidbody>();
        m_rb.velocity = new Vector3(0, 1, 0);
    }

    LaunchData CalculateLaunchData()
    {
        float displacmentY = m_target.position.y - m_rb.position.y;
        Vector3 displacementXZ = new Vector3(m_target.position.x - m_rb.position.x, 0, m_target.position.z - m_rb.position.z);
        float time = Mathf.Sqrt(-2 * m_h / m_gravity) + Mathf.Sqrt(2 * (displacmentY - m_h) / m_gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * m_gravity * m_h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData (velocityXZ + velocityY * -Mathf.Sign(m_gravity), time);
    }

    void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = m_rb.position;

        int resolution = 30;
        for(int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * m_gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = m_rb.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }

    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData (Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }
}
