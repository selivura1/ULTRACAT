using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SecretDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text _header;
    [SerializeField] TMP_Text _description;
    [SerializeField] Image _image;

    public void Initialize(string name, string desc, Sprite image)
    {
        _header.text = name;
        _description.text = desc;
        _image.sprite = image;
    }
}
