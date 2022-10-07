using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Cucumba.PlanetarySystem
{
    public sealed class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Header("Inspector")]
        
        [SerializeField]
        private TextAsset _configAsset;

        [SerializeField]
        private PlanetarySystemFactoryImpl _planetarySystemFactoryImpl;

        private GameConfig _config;

        private IPlanetarySystemFactory _planetarySystemFactory;
        private IPlanetarySystem _planetarySystem;

        public readonly Dictionary<MassClassEnum, ConfigPlanet> PlanetsByType = new Dictionary<MassClassEnum, ConfigPlanet>();
        
        public GameConfig Config => _config;

        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        private void Start()
        {
            _config = LoadConfig();

            for (int i = 0, length = _config.Planets.Length; i < length; i++)
            {
                var planet = _config.Planets[i];
                PlanetsByType.Add(planet.PlanetType, planet);
            }

            _planetarySystemFactory = _planetarySystemFactoryImpl;

            _planetarySystem = _planetarySystemFactory.Create(-1);
        }

        private void Update()
        {
            _planetarySystem.Update(Time.deltaTime);
        }

        private GameConfig LoadConfig()
        {
            var json = _configAsset.text;
            return JsonConvert.DeserializeObject<GameConfig>(json);
        }
    }
}
