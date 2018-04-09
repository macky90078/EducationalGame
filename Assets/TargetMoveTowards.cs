using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMoveTowards : MonoBehaviour {

    private GameObject m_player;

    [SerializeField]
    private float m_speed = 5;

    [SerializeField]
    private float m_score = 2;

    private GameObject m_gameManager;

    private void Awake()
    {
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager");
        m_player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update ()
    {
	}

    private void FixedUpdate()
    {
        /*Vector3 toTarget = m_player.transform.position - transform.position;

        transform.Translate(toTarget * 2 * Time.deltaTime);*/

        if(m_player != null)
        transform.position = Vector3.MoveTowards(transform.position, m_player.transform.position, m_speed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Arrow")
        {
            //Destroy(collision.gameObject);
            m_gameManager.GetComponent<GameManager>().m_score += m_score;
            Destroy(gameObject);
        }
    }
}
