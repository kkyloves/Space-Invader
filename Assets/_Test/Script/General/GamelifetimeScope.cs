using _Test.Script.Data;
using _Test.Script.Enemy;
using _Test.Script.Player;
using _Test.Script.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Test.Script.General
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private UIManager uiManager;
        [SerializeField] private GameData gameData;
        [SerializeField] private AudioManager audioManager;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(uiManager);
            builder.RegisterComponent(gameData);
            builder.RegisterComponent(audioManager);

            builder.RegisterComponentInHierarchy<PlayerHealth>();
            builder.RegisterComponentInHierarchy<PlayerShooter>();
            builder.RegisterComponentInHierarchy<EnemyBrainShooter>();
        }
    }
}