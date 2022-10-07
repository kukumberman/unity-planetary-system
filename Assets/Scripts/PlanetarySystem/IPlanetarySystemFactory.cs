namespace Cucumba.PlanetarySystem
{
    public interface IPlanetarySystemFactory
    {
        IPlanetarySystem Create(double mass);
    }
}
