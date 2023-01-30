using System;
using LoxiGames.Manager;
using UnityEngine;

namespace LoxiGames.Utility
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class FieldOfView : MonoBehaviour
    {
        [SerializeField] private string targetTag = "Player";
        [SerializeField] private float viewAngle = 60;
        [SerializeField] private float viewDistance = 5;
        [SerializeField] private int rayCount = 100;
        [SerializeField] private LayerMask ignoreLayer;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material detectedMaterial;

        public static event Action<Transform> OnAllHitChecked;
        public event Action<Transform> OnHitChecked;
        private Mesh _mesh;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _mesh;
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void FixedUpdate()
        {
            if (!GameManager.IsPlaying) return;

            CheckHit();
        }

        private void CheckHit()
        {
            var origin = Vector3.zero;
            var angle = viewAngle / 2;
            var angleIncrease = viewAngle / rayCount;

            var vertices = new Vector3[rayCount + 1 + 1];
            var triangles = new int[rayCount * 3];

            vertices[0] = origin;
            var vertexIndex = 1;
            var triangleIndex = 0;
            Transform target = null;

            for (var i = 0; i <= rayCount; i++)
            {
                Vector3 vertex;
                var tr = transform;
                var isHit = Physics.Raycast(tr.position, GetVectorFromAngle(angle - tr.rotation.eulerAngles.y),
                    out var hit, viewDistance, ~ignoreLayer);

                if (isHit)
                {
                    var hitTransform = hit.transform;
                    vertex = transform.InverseTransformPoint(hit.point);
                    if (hitTransform.CompareTag(targetTag))
                    {
                        target = hitTransform;
                    }
                }
                else
                {
                    vertex = origin + GetVectorFromAngle(angle) * viewDistance;
                }

                vertices[vertexIndex] = vertex;

                if (i > 0)
                {
                    triangles[triangleIndex] = 0;
                    triangles[triangleIndex + 1] = vertexIndex - 1;
                    triangles[triangleIndex + 2] = vertexIndex;

                    triangleIndex += 3;
                }

                vertexIndex++;
                angle -= angleIncrease;
            }

            _meshRenderer.material = target ? detectedMaterial : defaultMaterial;
            _mesh.vertices = vertices;
            _mesh.triangles = triangles;

            OnAllHitChecked?.Invoke(target);
            OnHitChecked?.Invoke(target);
        }

        private Vector3 GetVectorFromAngle(float angle)
        {
            var angleRad = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad));
        }
    }
}