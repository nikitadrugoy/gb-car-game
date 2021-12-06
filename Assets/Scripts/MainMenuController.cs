using Profile;
using UnityEngine;

public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/mainMenu"};
    private readonly ResourcePath _trailViewPath = new ResourcePath {PathResource = "Prefabs/Trail"};
    private readonly ProfilePlayer _profilePlayer;
    private readonly MainMenuView _view;
    private TrailView _trailView;

    public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        
        _view = LoadView(placeForUi);
        _view.Init(StartGame);
        
        _trailView = LoadTrailView();
        _trailView.Init();
    }

    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);
        
        return objectView.GetComponent<MainMenuView>();
    }

    private TrailView LoadTrailView()
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_trailViewPath));
        AddGameObjects(objectView);
        
        return objectView.GetComponent<TrailView>();
    }

    private void StartGame()
    {
        _profilePlayer.CurrentState.Value = GameState.Game;
    }
}

