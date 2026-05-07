// Befüllt ein InfoPanel mit Name, Nährwerten und Feedback eines Lebensmittels.

using TMPro;
using UnityEngine;

public class PanelUpdater : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI detailsText;
    public TextMeshProUGUI feedbackText;
    public FoodItem CurrentFood { get; private set; }

    public void UpdatePanel(FoodItem food)
    {
        
        CurrentFood = food;

        nameText.text = food.name;
        detailsText.text = $"Protein: {food.proteinPerPortion} g\nKcal: {food.kcalPerPortion}\nAllergene: {food.allergens}";
        feedbackText.text = food.feedback;
    }
}
