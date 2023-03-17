using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DieController : MonoBehaviour
{
    [SerializeField] private DieUIController m_UIController;
    [SerializeField] private Transform m_MarkersParent;
    [SerializeField] private Rigidbody m_Rigidbody;
    [SerializeField] private TextMeshProUGUI m_DisplayTxt;

    private void Start()
    {
        m_UIController.DisplayUI(false);
    }

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
        m_UIController.DisplayUI(false);
        m_Rigidbody.AddForce(Vector3.up * Random.Range(3f, 8f), ForceMode.Impulse);
        m_Rigidbody.AddTorque(new Vector3(Random.Range(-5f, 5), Random.Range(-5f, 5), Random.Range(-5f, 5)));

        StartCoroutine(DetectRollRoutine());
    }

    private IEnumerator DetectRollRoutine()
    {
        yield return new WaitForSeconds(1);

        do
        {
            print("Mag: " + m_Rigidbody.velocity.magnitude);
            yield return new WaitForFixedUpdate();
        } while (m_Rigidbody.velocity.magnitude != 0);

        m_UIController.DisplayUIWithAnimation(true);
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
