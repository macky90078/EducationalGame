using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    [SerializeField]
    private float m_speed = 10.0f;

    [SerializeField]
    GameObject m_cam;

	void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Update ()
    {
        float translation = Input.GetAxis("Vertical") * m_speed;
        float straffe = Input.GetAxis("Horizontal") * m_speed;

        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);

        if(Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Target")
        {
            m_cam.SetActive(true);
            Destroy(gameObject);
        }
    }
}
