using TMPro;
using UnityEngine;

public class PanelUpdater : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI detailsText;
    public TextMeshProUGUI feedbackText;

    public void UpdatePanel(FoodItem food)
    {
        nameText.text = food.name;
        detailsText.text = $"Protein: {food.proteinPerPortion} g\nKcal: {food.kcalPerPortion}\nAllergene: {food.allergens}";
        feedbackText.text = food.feedback;
    }
}
