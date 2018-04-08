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

    [SerializeField]
    private bool m_debugPath;

	// Use this for initialization
	void Start ()
    {
      //  m_rb.useGravity = false;	
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


        if (Input.GetKey(KeyCode.Mouse0) && m_drawTime > 0)
        {
            //m_moveDist += 0.05f * Time.deltaTime;
            m_drawTime -= Time.deltaTime;
            m_target.position += transform.forward * Time.deltaTime * 30f;
  
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Launch();
            m_target.position = transform.position;
            m_drawTime = 1.2f;
            if (m_debugPath)
            {
                DrawPath();
            }
        }

	}

    private void FixedUpdate()
    {
        bool hit = Physics.Raycast(m_bowHObject.transform.position, -transform.up, Mathf.Infinity);
    }

    private void Launch()
    {
        Physics.gravity = Vector3.up * m_gravity;
        GameObject arrow = Instantiate(m_arrowObject, transform.position - new Vector3(0, 0.3f, 0), transform.rotation);
        m_rb = arrow.GetComponent<Rigidbody>();
        m_rb.useGravity = true;
        m_rb.velocity = CalculateLaunchData().initialVelocity;
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
