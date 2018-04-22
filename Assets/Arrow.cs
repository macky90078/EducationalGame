using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    [SerializeField]
    private float m_lifeTime = 10f;

    [SerializeField]
    private GameObject m_gameManager;


    private void Awake()
    {
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update ()
    {
        m_lifeTime -= Time.deltaTime;
        if (m_lifeTime <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Target")
        {
            Destroy(collision.gameObject);
            m_gameManager.GetComponent<GameManager>().m_targetsHit += 1;
        }
    }
}
