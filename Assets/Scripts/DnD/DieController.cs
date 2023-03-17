using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DieController : MonoBehaviour
{
    [SerializeField] private Transform m_MarkersParent;
    [SerializeField] private Rigidbody m_Rigidbody;
    [SerializeField] private TextMeshProUGUI m_DisplayTxt;

    private void Update()
    {
        m_DisplayTxt.text = GetCurrentRoll().ToString();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            RollDie();
        }
    }

    private void RollDie()
    {
        m_Rigidbody.AddForce(Vector3.up * Random.Range(1f, 5f), ForceMode.Impulse);
        m_Rigidbody.AddTorque(new Vector3(Random.Range(-5f, 5), Random.Range(-5f, 5), Random.Range(-5f, 5)));
    }

    private int GetCurrentRoll()
    {
        int upMostValue = 0;

        float highestHeight = 0;

        foreach (Transform child in m_MarkersParent)
        {
            if (child.transform.position.y > highestHeight)
            {
                highestHeight = child.transform.position.y;

                string[] expanded = child.name.Split('_');

                upMostValue = int.Parse(expanded[1]);
            }
        }

        return upMostValue;
    }
}
