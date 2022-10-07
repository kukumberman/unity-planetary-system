namespace Cucumba.PlanetarySystem
{
    public sealed class GameConfig
    {
        public ConfigPlanet[] Planets;
    }

    public sealed class ConfigPlanet
    {
        public MassClassEnum PlanetType;
        public double MassMin;
        public double MassMax;
        public float RadiusMin;
        public float RadiusMax;
        public string Color;
    }
}
