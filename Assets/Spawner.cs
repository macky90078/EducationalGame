using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField]
    private float m_spawnTime = 5f;

    [SerializeField]
    private float m_overallTime = 0;

    [SerializeField]
    private GameObject m_gSpawnObject1;
    [SerializeField]
    private GameObject m_gSpawnObject2;
    [SerializeField]
    private GameObject m_gSpawnObject3;

    public bool test1 = false;
    public bool test2 = false;
    public bool test3 = false;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_spawnTime -= Time.deltaTime;
        m_overallTime -= Time.deltaTime;

        if(m_spawnTime <= 0 && m_overallTime > -25)
        {
            test1 = true;
            Instantiate(m_gSpawnObject1, transform.position, m_gSpawnObject1.transform.rotation);
            m_spawnTime = 6f;
        }
        if (m_spawnTime <= 0 && (m_overallTime > -65 && m_overallTime < -25))
        {
            test1 = false;
            test2 = true;
            Instantiate(m_gSpawnObject2, transform.position, m_gSpawnObject2.transform.rotation);
            m_spawnTime = 4f;
        }
        if (m_spawnTime <= 0 && m_overallTime < -65)
        {
            test2 = false;
            test3 = true;
            Instantiate(m_gSpawnObject3, transform.position, m_gSpawnObject3.transform.rotation);
            m_spawnTime = 2f;
        }
    }
}
