using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class FoodItem
{
    public string id;
    public string name;
    public float proteinPerPortion;
    public float kcalPerPortion;
    public string allergens;
    public string feedback;
}

[Serializable]
public class FoodItemList
{
    public List<FoodItem> foods;
}

public class FoodDatabase : MonoBehaviour
{
    public static FoodDatabase Instance;

    private Dictionary<string, FoodItem> foodMap = new Dictionary<string, FoodItem>();

    void Awake()
{
    Debug.Log("FoodDatabase Awake läuft");

    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }
    else
    {
        Destroy(gameObject);
    }
}

    private void LoadData()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("Data/foods");
        Debug.Log("JSON gefunden? " + (jsonData != null));

        if (jsonData == null)
        {
            Debug.LogError("foods.json not found!");
            return;
        }

        FoodItemList list = JsonUtility.FromJson<FoodItemList>(jsonData.text);

        if (list == null || list.foods == null)
        {
            Debug.LogError("foods.json konnte nicht gelesen werden oder foods-Liste fehlt!");
            return;
        }

        Debug.Log("Foods geladen: " + list.foods.Count);

        foodMap = list.foods.ToDictionary(f => f.id, f => f);

        Debug.Log("Food IDs geladen: " + string.Join(", ", foodMap.Keys));
    }

    public FoodItem GetFoodById(string id)
    {
        if (foodMap.TryGetValue(id, out FoodItem item))
            return item;
        return null;
    }
}
