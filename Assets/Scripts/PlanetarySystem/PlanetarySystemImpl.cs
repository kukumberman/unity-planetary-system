using System.Collections.Generic;

namespace Cucumba.PlanetarySystem
{
    public sealed class PlanetarySystemImpl : IPlanetarySystem
    {
        private const float START_RADIUS_OFFSET = 5;
        private const float BASE_DISTANCE_BETWEEN_PLANETS = 1;
        private const bool RANDOMIZE_INITIAL_ANGLE = true;

        private readonly PlanetaryObjectImpl[] _planetaryObjects;

        public IEnumerable<IPlanetaryObject> PlanetaryObjects => _planetaryObjects;

        public PlanetarySystemImpl(PlanetaryObjectImpl[] planetaryObjects)
        {
            _planetaryObjects = planetaryObjects;

            PlacePlanetsAtUniqueOrbit(RANDOMIZE_INITIAL_ANGLE);
        }

        public void Update(float deltaTime)
        {
            for (int i = 0, length = _planetaryObjects.Length; i < length; i++)
            {
                _planetaryObjects[i].Update(deltaTime);
            }
        }

        private void PlacePlanetsAtUniqueOrbit(bool randomizeAngle)
        {
            var orbitCenter = UnityEngine.Vector3.zero;
            var orbitRadius = START_RADIUS_OFFSET;

            for (int i = 0, length = _planetaryObjects.Length; i < length; i++)
            {
                var planet = _planetaryObjects[i];

                orbitRadius += planet.Model.Radius * 0.5f;

                planet.PlaceAtOrbit(orbitRadius, orbitCenter);

                orbitRadius += planet.Model.Radius * 0.5f;

                orbitRadius += BASE_DISTANCE_BETWEEN_PLANETS;
            }

            if (randomizeAngle)
            {
                for (int i = 0, length = _planetaryObjects.Length; i < length; i++)
                {
                    var planet = _planetaryObjects[i];

                    var angle = UnityEngine.Random.Range(0, 360);

                    planet.PlaceAtAngle(angle);
                }
            }
        }
    }
}
