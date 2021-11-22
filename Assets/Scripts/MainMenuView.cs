using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _buttonStart;
    
    private readonly ResourcePath _trailViewPath = new ResourcePath {PathResource = "Prefabs/Trail"};

    private TrailView _trailView;

    public void Init(UnityAction startGame)
    {
        _trailView = Instantiate(ResourceLoader.LoadPrefab(_trailViewPath)).GetComponent<TrailView>();
        
        _trailView.Init();
        
        _buttonStart.onClick.AddListener(startGame);
    }

    protected void OnDestroy()
    {
        _buttonStart.onClick.RemoveAllListeners();

        if (_trailView != null)
        {
            Destroy(_trailView.gameObject);
        }
    }
}