using UnityEngine;

public class ResourceCounter : MonoBehaviour
{
    [SerializeField] private Sprite[] digitSprites;
    [SerializeField] private SpriteRenderer[] digitImages;

    private int _count;

    public int Count => _count;

    public void AddResource()
    {
        _count++;
        UpdateDigitSprites();
    }
    
    public bool TryBuy(int price)
    {
        if (_count >= price)
        {
            _count -= price;
            UpdateDigitSprites();
            return true;
        }
        else
        {
            Debug.Log("not enough resources");
            return false;
        }
    }

    private void UpdateDigitSprites()
    {
        var countString = _count.ToString().PadLeft(digitImages.Length, '0');

        for (int i = 0; i < digitImages.Length; i++)
        {
            char digitChar = countString[i];

            int digit = int.Parse(digitChar.ToString());

            digitImages[i].sprite = digitSprites[digit];
        }
    }
}