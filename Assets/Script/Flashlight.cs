using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Для использования IPointerClickHandler
using System.Collections; // Необходимо для работы с корутинами

public class Flashlight : MonoBehaviour, IPointerClickHandler
{
    public Image chargeIndicator;
    public Sprite greenCharge, yellowCharge, redCharge;
    public UnityEngine.Rendering.Universal.Light2D flashlightLight;
    public Text chargeAddedText; // Текст для отображения количества добавленной зарядки
    public float maxCharge = 100f;
    public float currentCharge;
    public float dischargeRate = 1f;
    public float maxLightIntensity = 157f; // Максимальная интенсивность света

    void Start()
    {
        currentCharge = maxCharge;
        UpdateChargeIndicator();
        chargeAddedText.text = ""; // Сбрасываем текст при старте
        flashlightLight.intensity = maxLightIntensity; // Инициализируем максимальную интенсивность
    }

    void Update()
    {
        DischargeLight();
        UpdateChargeIndicator();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        RechargeFlashlight(20); // Пример добавления зарядки
    }

    void DischargeLight()
    {
        if (currentCharge > 0)
        {
            currentCharge -= dischargeRate * Time.deltaTime;
            currentCharge = Mathf.Max(currentCharge, 0);
            // Расчет интенсивности света на основе текущего заряда от максимального значения интенсивности
            flashlightLight.intensity = maxLightIntensity * (currentCharge / maxCharge);

            if (currentCharge <= 0)
            {
                flashlightLight.enabled = false; // Выключаем свет, когда зарядка достигла 0
            }
        }
    }

    void UpdateChargeIndicator()
    {
        if (currentCharge > 66f)
        {
            chargeIndicator.sprite = greenCharge;
        }
        else if (currentCharge > 33f)
        {
            chargeIndicator.sprite = yellowCharge;
        }
        else
        {
            chargeIndicator.sprite = redCharge;
        }
    }

    public void RechargeFlashlight(float amount)
    {
        currentCharge += amount;
        if (currentCharge > maxCharge)
        {
            currentCharge = maxCharge;
        }
        UpdateChargeIndicator();
        chargeAddedText.text = $"+{amount}";
        StartCoroutine(HideTextAfterDelay(1f));
    }

    IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        chargeAddedText.text = ""; // Очищаем текст после задержки
    }
}

