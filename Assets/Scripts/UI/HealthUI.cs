using UnityEngine;
using TMPro;


public class HealthUI : MonoBehaviour
{
    [SerializeField] TextMeshPro text;


    public void UpdateText(float health)
    {
        text.text = health.ToString();
    }

}
