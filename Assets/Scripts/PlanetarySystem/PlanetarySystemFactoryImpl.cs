using System;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Cucumba.PlanetarySystem
{
    public sealed class PlanetarySystemFactoryImpl : MonoBehaviour, IPlanetarySystemFactory
    {
        private const int MIN_PLANETS_IN_SYSTEM = 2;
        private const float PLANET_RADIUS_TO_UNITY_UNITS = 2;

        [SerializeField]
        private PlanetaryObjectView _planetPrefab;

        public IPlanetarySystem Create(double mass)
        {
            var planetsParent = transform;

            var allPlanetTypes = Enum.GetValues(typeof(MassClassEnum))
                .Cast<MassClassEnum>()
                .ToArray();

            var rnd = new Random((int)DateTime.Now.Ticks);
            
            var planetsCount = rnd.Next(MIN_PLANETS_IN_SYSTEM, allPlanetTypes.Length + 1);
            
            allPlanetTypes.ShuffleSelf(rnd);

            var randomizedPlanetTypes = allPlanetTypes
                .Take(planetsCount);

            var selectedCfgPlanets = randomizedPlanetTypes
                .Select(planetType => GameManager.Instance.PlanetsByType[planetType])
                .ToArray();

            var planets = new PlanetaryObjectImpl[selectedCfgPlanets.Length];

            for (int i = 0, length = planets.Length; i < length; i++)
            {
                var cfgPlanet = selectedCfgPlanets[i];

                var planetModel = new PlanetaryObjectModel
                {
                    PlanetType = cfgPlanet.PlanetType,
                    Mass = rnd.NextDouble(cfgPlanet.MassMin, cfgPlanet.MassMax),
                    Radius = (float)rnd.NextDouble(cfgPlanet.RadiusMin, cfgPlanet.RadiusMin) * PLANET_RADIUS_TO_UNITY_UNITS,
                };

                //! sometimes planet is completely invisible (has zero radius)
                planetModel.Radius = Mathf.Max(planetModel.Radius, 0.00001f);

                //! assigns color from config based on planet entity
                planetModel.Color = GetPlanetColor(cfgPlanet.Color);

                var planetView = Instantiate(_planetPrefab, Vector3.zero, Quaternion.identity, planetsParent);

                var planet = new PlanetaryObjectImpl(planetModel, planetView);
                planets[i] = planet;
            }

            var planetarySystem = new PlanetarySystemImpl(planets);

            return planetarySystem;
        }

        private Color GetPlanetColor(string htmlString)
        {
            if (ColorUtility.TryParseHtmlString(htmlString, out var parsedColor))
            {
                return parsedColor;
            }
            else
            {
                Debug.LogWarning($"Unable to parse [{htmlString}] color");
                return Color.magenta;
            }
        }
    }
}
