namespace Cucumba.PlanetarySystem
{
    public interface IPlanetaryObject 
    {
        MassClassEnum MassClass { get; }

        double Mass { get; }
    }
}
