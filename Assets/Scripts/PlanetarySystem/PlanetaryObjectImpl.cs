using UnityEngine;

namespace Cucumba.PlanetarySystem
{
    public sealed class PlanetaryObjectModel
    {
        public MassClassEnum PlanetType;
        public double Mass;
        public float Radius;
        public Color Color;
    }

    public sealed class PlanetaryObjectImpl : IPlanetaryObject
    {
        private readonly PlanetaryObjectModel _model;
        private readonly PlanetaryObjectView _view;

        private Vector3 _orbitCenter;
        private float _orbitRadius;
        private float _angle;
        private float _angleRad;
        private Vector3 _position;

        public PlanetaryObjectModel Model => _model;

        public MassClassEnum MassClass => _model.PlanetType;

        public double Mass => _model.Mass;

        public PlanetaryObjectImpl(PlanetaryObjectModel model, PlanetaryObjectView view)
        {
            _model = model;
            _view = view;

            _view.UpdateVisual(_model.Radius, _model.Color);
        }

        public void Update(float deltaTime)
        {
            _angle += deltaTime;
            UpdatePosition();
        }

        public void PlaceAtOrbit(float radius, Vector3 orbitCenter)
        {
            _orbitRadius = radius;
            _orbitCenter = orbitCenter;

            _view.VisualizeOrbitTrajectory(_orbitRadius, _orbitCenter);

            UpdatePosition();
        }

        public void PlaceAtAngle(float angle360)
        {
            _angle = angle360;
            
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            _angleRad = _angle * Mathf.Deg2Rad;
            _position.x = Mathf.Sin(_angleRad) * _orbitRadius + _orbitCenter.x;
            _position.z = Mathf.Cos(_angleRad) * _orbitRadius + _orbitCenter.z;
            _view.UpdatePosition(_position);
        }
    }
}
