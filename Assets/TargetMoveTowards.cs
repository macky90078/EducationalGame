using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMoveTowards : MonoBehaviour {

    [SerializeField]
    private GameObject m_target1;
    [SerializeField]
    private GameObject m_target2;

    [SerializeField]
    private float m_speed = 5;

    private bool m_moveToTarget1 = true;
    private bool m_moveToTarget2 = false;

    private void FixedUpdate()
    {
        if (m_target1 != null && m_moveToTarget1)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_target1.transform.position, m_speed * Time.deltaTime);
            transform.LookAt(m_target1.transform);
        }
        else if(m_target2 != null && m_moveToTarget2)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_target2.transform.position, m_speed * Time.deltaTime);
            transform.LookAt(m_target2.transform);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Arrow")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Target1")
        {
            m_moveToTarget1 = false;
            m_moveToTarget2 = true;
        }
        if(other.tag == "Target2")
        {
            m_moveToTarget2 = false;
            m_moveToTarget1 = true;
        }
    }
}
