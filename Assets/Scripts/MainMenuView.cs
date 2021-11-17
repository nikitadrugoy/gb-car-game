using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _buttonStart;
    [SerializeField] private TrailView _trailView;

    public void Init(UnityAction startGame)
    {
        _trailView.Init();
        
        _buttonStart.onClick.AddListener(startGame);
    }

    protected void OnDestroy()
    {
        _buttonStart.onClick.RemoveAllListeners();
    }
}