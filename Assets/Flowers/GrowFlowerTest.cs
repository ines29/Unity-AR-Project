using UnityEngine;
using System.Collections;
using TMPro;

public class GrowFlowerTest : MonoBehaviour
{
    public float growthRate = 0.01f; // Wachstumsrate der Blume
    public float maxGrowth = 1f; // Maximale Größe der Blume
    public KeyCode growthKey = KeyCode.Space; // Taste, um das Wachstum auszulösen

    private float currentGrowth = -0.35f; // Aktuelle Größe der Blume
    private bool hasStartedGrowing = false; // Hat die Blume schon mit dem Wachsen begonnen?
    public GameObject textObject;

    //public TextMeshProUGUI flowerText;  // Drag & Drop Text Objekt auf dieses Feld im Inspector
    void Update()
    {
        // Wenn die Wachtums-Taste gedrückt wird und das Wachstum noch nicht begonnen hat, beginne mit dem Wachsen
        if (Input.GetKeyDown(growthKey) && !hasStartedGrowing)
        {
            hasStartedGrowing = true;
            StartCoroutine(IncreaseGrowth());
        }
    }

    // Schrittweise erhöhe das Wachstum der Blume, bis es den maximalen Wert erreicht hat
    IEnumerator IncreaseGrowth()
    {
        while (currentGrowth < maxGrowth)
        {
            currentGrowth += growthRate;
            currentGrowth = Mathf.Clamp(currentGrowth, -0.35f, maxGrowth);
            UpdateGrowth();

            yield return new WaitForSeconds(0.1f); // Warte 0.1 Sekunden, bevor das Wachstum erneut erhöht wird
        }
    }



    // Aktualisiere die Größe der Blume im Material
    void UpdateGrowth()
    {
        
        Material material = GetComponent<Renderer>().material;
        material.SetFloat("Grow_", currentGrowth);
        Debug.Log("Current Grow_ value: " + material.GetFloat("Grow_"));

        // Wenn das Wachstum die maximale Größe erreicht hat, zeige den Text auf der Blume an
        if (currentGrowth == maxGrowth)
        {
            textObject.SetActive(true); // Aktiviert das Text-GameObjekt, wenn die Blume vollständig gewachsen ist
        }
        else
        {
            textObject.SetActive(false); // Deaktiviert das Text-GameObjekt, wenn die Blume noch nicht vollständig gewachsen ist
        }

    }
}

//Add Sound Effect?


