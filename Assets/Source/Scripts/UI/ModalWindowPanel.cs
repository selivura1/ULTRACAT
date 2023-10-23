using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModalWindowPanel : MonoBehaviour
{
    [SerializeField] protected RectTransform _box;
    [Header("Header")]
    [SerializeField] protected RectTransform _headerArea;
    [SerializeField] protected TMP_Text _titleText;
    [Header("Content")]
    [SerializeField] protected RectTransform _contentArea;
    [SerializeField] protected RectTransform _verticalLayoutArea;
    [SerializeField] protected TMP_Text _verticalText;
    [SerializeField] protected Image _verticalImage;

    [SerializeField] protected RectTransform _horizontalLayoutArea;
    [SerializeField] protected TMP_Text _horizontalLayoutText;
    [SerializeField] protected Image _horizontalImage;

    [Header("Footer")]
    [SerializeField] protected RectTransform _footerArea;
    [SerializeField] protected Button _confirmButton;
    [SerializeField] protected TMP_Text _confirmText;
    [SerializeField] protected Button _cancelButton;
    [SerializeField] protected TMP_Text _cancelText;
    [SerializeField] protected Button _altButton;
    [SerializeField] protected TMP_Text _altText;

    private Action onConfirm;
    private Action onCancel;
    private Action onAlterntative;

    private void Start()
    {
        Close();
    }
    public void Confirm()
    {
        onConfirm?.Invoke();
        Close();
    }

    public void Cancel()
    {
        onCancel?.Invoke();
        Close();
    }
    public void Alternative()
    {
        onAlterntative?.Invoke();
        Close();
    }
    private void Close()
    {
        LeanTween.scale(gameObject, Vector3.zero, 1).setIgnoreTimeScale(true); 
    }

    public void ShowAsItemUnlock(string title, Sprite imageToShowm, string message, 
        string confirmText, string cancelText, string altText, 
        Action confirmAction, Action cancelAction, Action alterAction)
    {
        _horizontalLayoutArea.gameObject.SetActive(false);

        bool hasTitle = string.IsNullOrEmpty(title);
        _headerArea.gameObject.SetActive(hasTitle);
        _titleText.text = title;

        _verticalImage.sprite = imageToShowm;
        _verticalText.text = message;

        onConfirm = confirmAction;

        bool hasCancel = (cancelAction != null);
        _altButton.gameObject.SetActive(hasCancel);
        onCancel = cancelAction;

        bool hasAlt = (alterAction != null);
        _altButton.gameObject.SetActive(hasAlt);
        onAlterntative = alterAction;

        _confirmText.text = confirmText;
        _cancelText.text = cancelText;
        _altText.text = altText;
    }
    public void ShowAsItemUnlock(string title, Sprite imageToShowm, string message, Action confirmAction)
    {
        ShowAsItemUnlock(title, imageToShowm, message, "OK", "", "", confirmAction, null, null);
    }

    public void ShowAsItemUnlock(string title, Sprite imageToShowm, string message, Action confirmAction, Action cancelAction)
    {
        ShowAsItemUnlock(title, imageToShowm, message, "OK", "Decline", "", confirmAction, cancelAction, null);
    }
}
