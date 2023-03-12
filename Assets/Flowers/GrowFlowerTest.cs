using UnityEngine;
using System.Collections;

public class GrowFlowerTest : MonoBehaviour
{
    public float growthRate = 0.01f; // Wachstumsrate der Blume
    public float maxGrowth = 1f; // Maximale Gr��e der Blume
    public KeyCode growthKey = KeyCode.Space; // Taste, um das Wachstum auszul�sen

    private float currentGrowth = -0.35f; // Aktuelle Gr��e der Blume
    private bool hasStartedGrowing = false; // Hat die Blume schon mit dem Wachsen begonnen?

    void Update()
    {
        // Wenn die Wachtums-Taste gedr�ckt wird und das Wachstum noch nicht begonnen hat, beginne mit dem Wachsen
        if (Input.GetKeyDown(growthKey) && !hasStartedGrowing)
        {
            hasStartedGrowing = true;
            StartCoroutine(IncreaseGrowth());
        }
    }

    // Schrittweise erh�he das Wachstum der Blume, bis es den maximalen Wert erreicht hat
    IEnumerator IncreaseGrowth()
    {
        while (currentGrowth < maxGrowth)
        {
            currentGrowth += growthRate;
            currentGrowth = Mathf.Clamp(currentGrowth, -0.35f, maxGrowth);
            UpdateGrowth();

            yield return new WaitForSeconds(0.1f); // Warte 0.1 Sekunden, bevor das Wachstum erneut erh�ht wird
        }
    }

    // Aktualisiere die Gr��e der Blume im Material
    void UpdateGrowth()
    {
        
        Material material = GetComponent<Renderer>().material;
        material.SetFloat("Grow_", currentGrowth);
        Debug.Log("Current Grow_ value: " + material.GetFloat("Grow_"));

    }
}
