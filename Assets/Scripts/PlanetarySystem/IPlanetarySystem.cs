using System.Collections.Generic;

namespace Cucumba.PlanetarySystem
{
    public interface IPlanetarySystem
    {
        IEnumerable<IPlanetaryObject> PlanetaryObjects { get; }

        void Update(float deltaTime);
    }
}
