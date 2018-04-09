using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    [SerializeField]
    private float m_lifeTime = 1f;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_lifeTime -= Time.deltaTime;
        if(m_lifeTime <= 0)
        {
            Destroy(gameObject);
        }
		
	}
}
