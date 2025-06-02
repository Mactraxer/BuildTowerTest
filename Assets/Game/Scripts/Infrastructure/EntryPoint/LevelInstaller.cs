using UnityEngine.UIElements;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private RectTransform _trashZone;
    [SerializeField] private RectTransform _towerParent;
    [SerializeField] private float _maxTowerHeight = 1000f;

    public override void InstallBindings()
    {
        Container.Bind<ITowerState>().To<TowerState>().AsSingle().WithArguments(_maxTowerHeight);
        
        Container.Bind<ICubePlacer>().To<TowerCubePlacer>().AsSingle().WithArguments(_towerParent);
        Container.Bind<ICubeRemover>().To<TowerCubeRemover>().AsSingle();
        Container.Bind<ITrashZoneValidator>().To<EllipseTrashZoneValidator>().AsSingle().WithArguments(_trashZone);
        Container.Bind<ITowerPlacementValidator>()
         .To<SimpleTowerPlacementValidator>()
         .AsSingle()
         .WithArguments(0.5f);
    }
}
