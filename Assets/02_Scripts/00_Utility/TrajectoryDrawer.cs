using System.Collections.Generic;
using UnityEngine;

namespace LoxiGames.Utility
{
    public class TrajectoryDrawer : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private int lineSegmentCount;

        private readonly List<Vector3> _linePoints = new List<Vector3>();

        public void UpdateTrajectory(Vector3 force, float mass, Vector3 startPoint)
        {
            var velocity = (force / mass) * Time.fixedDeltaTime;
            var flightDuration = (velocity.y * 2) / Physics.gravity.y;
            var stepTime = flightDuration / lineSegmentCount;

            if (stepTime >= -0.01f) stepTime = -0.1f;

            _linePoints.Clear();

            for (var i = 0; i < lineSegmentCount; i++)
            {
                var stepTimePassed = stepTime * i;

                var movementVector = new Vector3(
                    velocity.x * stepTimePassed,
                    velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                    velocity.z * stepTimePassed);

                _linePoints.Add(startPoint - movementVector);
            }

            lineRenderer.positionCount = _linePoints.Count;
            lineRenderer.SetPositions(_linePoints.ToArray());

            SetMaterialTiling();
        }

        private void SetMaterialTiling()
        {
            var distance = 0f;
            for (var i = 1; i < _linePoints.Count; i++)
            {
                distance += Vector3.Distance(_linePoints[i - 1], _linePoints[i]);
            }

            var lineMat = lineRenderer.material;
            var tiling = lineMat.mainTextureScale;
            tiling.x = distance / 5;
            lineMat.mainTextureScale = tiling;
        }

        public void HideLine()
        {
            lineRenderer.positionCount = 0;
        }
    }
}