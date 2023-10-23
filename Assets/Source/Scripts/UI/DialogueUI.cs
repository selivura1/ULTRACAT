using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueUI : MonoBehaviour
{
    public Image actorImage;
    public TMPro.TMP_Text messageText;
    public RectTransform dialogueRect;

    Message[] _currentMessages;
    int _activeMessage = 0;
    public bool IsDialogueActive;
    private bool _isMessageEnded = true;
    private int _messageProgress;

    private float _lastTypeTime;
    [SerializeField] private float _textSpeed = 1;
    public void OpenDialogue(Message[] messages)
    {
        _currentMessages = messages;
        _activeMessage = -1;
        _messageProgress = 0;
        IsDialogueActive = true;
        //GameManager.UIManager.OpenDialogue();
        PlayerControl.submitInput += DialogueButton;
        NextMessage();
        UpdateIcon();
    }
    void UpdateMessage()
    {
        var current = _currentMessages[_activeMessage].TextMessage;
        if (_messageProgress >= current.Length)
        {
            _isMessageEnded = true;
            return;
        }
        messageText.text += current[_messageProgress];
        _messageProgress++;
        _lastTypeTime = Time.time;
    }
    private void Update()
    {
        if((Time.time - _lastTypeTime > 1 / _textSpeed) && IsDialogueActive && !_isMessageEnded)
        {
            UpdateMessage();
        }
    }
    public void NextMessage()
    {
        _activeMessage++;
        if (_activeMessage >= _currentMessages.Length)
        {
            _activeMessage = 0;
            //GameManager.UIManager.CloseCurrentMenu(false, true);
            IsDialogueActive = false;
            PlayerControl.submitInput -= DialogueButton;
            return;
        }
        _messageProgress = 0;
        _isMessageEnded = false;
        messageText.text = "";
        UpdateIcon();
    }
    void UpdateIcon()
    {
        var current = _currentMessages[_activeMessage];
        if (current.Icon != null)
        {
            actorImage.gameObject.SetActive(true);
            actorImage.sprite = current.Icon;
        }
        else
        {
            actorImage.gameObject.SetActive(false);
        }
    }
    public void DialogueButton()
    {
        if (!_isMessageEnded)
            Skip();
        else
            NextMessage();
    }
    public void Skip()
    {
        _messageProgress = _currentMessages[_activeMessage].TextMessage.Length + 1;
        messageText.text = _currentMessages[_activeMessage].TextMessage;
        UpdateMessage();
    }
}
