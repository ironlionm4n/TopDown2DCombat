using System;
using System.Collections;
using System.Collections.Generic;
using MISC;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : Singleton<Stamina>
{
    [SerializeField] private Sprite fullStaminaSprite, emptyStaminaImage;
    [SerializeField] private float timeBetweenStaminaRefresh;
    public int CurrentStamina { get; private set; }

    private const string STAMINA_CONTAINER_TEXT = "Stamina Container";
    private readonly int startingStamina = 3;
    private Transform _staminaContainer;
    private int _maxStamina;


    private void Awake()
    {
        _maxStamina = startingStamina;
        CurrentStamina = startingStamina;
    }

    public void Start()
    {
        _staminaContainer = GameObject.Find(STAMINA_CONTAINER_TEXT).transform;
    }

    public void UseStamina()
    {
        CurrentStamina--;
        UpdateStaminaImages();
    }

    public void RefreshStamina()
    {
        if (CurrentStamina < _maxStamina)
        {
            CurrentStamina++;
        }
        UpdateStaminaImages();
    }

    private void UpdateStaminaImages()
    {
        for (int i = 0; i < _maxStamina; i++)
        {
            var currentStaminaImage = _staminaContainer.GetChild(i).GetComponent<Image>();
            if (i <= CurrentStamina - 1)
            {
                currentStaminaImage.sprite = fullStaminaSprite;
            }
            else
            {
                currentStaminaImage.sprite = emptyStaminaImage;
            }
        }

        if (CurrentStamina < _maxStamina)
        {
            StopAllCoroutines();
            StartCoroutine(StaminaGainRoutine());
        }
    }

    private IEnumerator StaminaGainRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenStaminaRefresh);
            RefreshStamina();
        }
    }
}
