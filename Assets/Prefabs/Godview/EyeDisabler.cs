using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeDisabler : MonoBehaviour
{
    [SerializeField] Sprite availableEye;
    [SerializeField] Sprite usedEye;
    [SerializeField] int eyeIndex;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        image.sprite = availableEye;
        Player.useEye += UseEye;
    }

    void UseEye(int numEyesUsed)
    {
        if(numEyesUsed >= eyeIndex)
            image.sprite = usedEye;

        if (numEyesUsed == -1) gameObject.SetActive(false);
    }
}
