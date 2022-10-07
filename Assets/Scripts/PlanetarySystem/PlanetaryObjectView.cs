using UnityEngine;

namespace Cucumba.PlanetarySystem
{
    public class PlanetaryObjectView : MonoBehaviour
    {
        [SerializeField]
        private Material _materialRef;

        [SerializeField]
        private Transform _container;

        [SerializeField]
        private MeshRenderer _meshRenderer;

        [Header("Orbit")]

        [SerializeField]
        private bool _doVisualizeOrbit;

        [SerializeField]
        private LineRenderer _lineRenderer;

        [SerializeField]
        private int _trajectoryPointsCount = 90;

        private Material _material;

        public void Awake()
        {
            _material = new Material(_materialRef);

            _meshRenderer.material = _material;
        }

        public void OnDestroy()
        {
            Destroy(_material);
        }

        public void UpdateVisual(float radius, Color color)
        {
            _container.localScale = Vector3.one * radius;
            _material.color = color;
        }

        public void UpdatePosition(Vector3 newPosition)
        {
            transform.position = newPosition;
        }

        public void VisualizeOrbitTrajectory(float orbitRadius, Vector3 orbitCenter)
        {
            _lineRenderer.gameObject.SetActive(_doVisualizeOrbit);

            if (!_doVisualizeOrbit)
            {
                return;
            }

            var positions = new Vector3[_trajectoryPointsCount];
            
            var stepAngle = 360f / _trajectoryPointsCount;

            for (int i = 0, length = _trajectoryPointsCount; i < length; i++)
            {
                var angle = i * stepAngle;
                positions[i] = PointOnCircle(angle * Mathf.Deg2Rad, orbitRadius);
                positions[i].x += orbitCenter.x;
                positions[i].z += orbitCenter.z;
            }

            _lineRenderer.positionCount = positions.Length;
            _lineRenderer.SetPositions(positions);
            _lineRenderer.loop = true;
        }

        private static Vector3 PointOnCircle(float angleRad, float radius)
        {
            var p = Vector3.zero;
            p.x = Mathf.Sin(angleRad) * radius;
            p.z = Mathf.Cos(angleRad) * radius;
            return p;
        }

        [ContextMenu(nameof(VisualizeOrbitTrajectory_Test))]
        private void VisualizeOrbitTrajectory_Test()
        {
            VisualizeOrbitTrajectory(3, Vector3.zero);
        }
    }
}
