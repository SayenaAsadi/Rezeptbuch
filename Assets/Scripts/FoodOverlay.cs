using TMPro;
using UnityEngine;

public class FoodOverlay : MonoBehaviour
{
    public string foodId; // z. B. "quark_creme"
    public TextMeshProUGUI infoText;

    void Start()
    {
        FoodItem food = FoodDatabase.Instance.GetFoodById(foodId);
        if (food != null)
        {
            infoText.text =
                food.name + "\n" +
                "Protein: " + food.proteinPerPortion + " g\n" +
                "Kcal: " + food.kcalPerPortion + "\n" +
                "Allergene: " + food.allergens + "\n" +
                food.feedback;
        }
        else
        {
            infoText.text = "Keine Daten gefunden für " + foodId;
        }
    }
}
