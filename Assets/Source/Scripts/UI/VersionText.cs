using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(TMP_Text))]
[ExecuteInEditMode]
public class VersionText : MonoBehaviour
{
    TMP_Text text;
    private void OnEnable()
    {
        text = GetComponent<TMP_Text>();
        text.text = Application.version + " by selivura";
    }
}
