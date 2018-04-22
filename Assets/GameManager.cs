using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public float m_score;

    [SerializeField]
    private Text m_scoreText;

    [SerializeField]
    private Text m_finalTime;

    private float m_startTime;

   // [HideInInspector]
    public int m_targetsHit = 0;

    [SerializeField]
    private GameObject m_Cows;

    [SerializeField]
    private bool m_gameWin = false;

	// Use this for initialization
	void Start () {
        m_startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!m_gameWin)
        {
            m_score = Mathf.RoundToInt(Time.time - m_startTime);
            m_scoreText.text = "Time: " + m_score;
        }

        if(m_gameWin)
        {
            m_finalTime.gameObject.SetActive(true);
            m_finalTime.text = "Final time:" + m_score;
        }

        if(m_targetsHit >= 3)
        {
            m_Cows.SetActive(true);
        }
        if(m_targetsHit >= 5)
        {
            m_gameWin = true;
        }
	}
}
