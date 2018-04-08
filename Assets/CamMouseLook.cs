using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMouseLook : MonoBehaviour {

    Vector2 m_mouseLook;
    Vector2 m_smoothV;

    [SerializeField]
    private float m_sensitivity = 5.0f;
    [SerializeField]
    private float m_smoothing = 2.0f;

    private GameObject m_character;

	void Start ()
    {
        m_character = this.transform.parent.gameObject;
	}
	
	void Update ()
    {
        Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(m_sensitivity * m_smoothing, m_sensitivity * m_smoothing));
        m_smoothV.x = Mathf.Lerp(m_smoothV.x, md.x, 1f / m_smoothing);
        m_smoothV.y = Mathf.Lerp(m_smoothV.y, md.y, 1f / m_smoothing);
        m_mouseLook += m_smoothV;

        m_mouseLook.y = Mathf.Clamp(m_mouseLook.y, -90f, 90f);

        transform.localRotation = Quaternion.AngleAxis(-m_mouseLook.y, Vector3.right);
        m_character.transform.localRotation = Quaternion.AngleAxis(m_mouseLook.x, m_character.transform.up);
    }
}
