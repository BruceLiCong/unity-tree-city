namespace TreeCity
{
    using UnityEngine;
    using Mapbox.Unity.MeshGeneration.Components;
    using Mapbox.Unity.MeshGeneration.Interfaces;
    using Mapbox.Unity.MeshGeneration.Modifiers;

    [CreateAssetMenu(menuName = "TreeCity/Modifiers/Tree Prefab Modifier")]
    public class TreePrefabModifier : GameObjectModifier
    {
        [SerializeField]
        private GameObject[] _prefabs;

        [SerializeField]
        private float _scale = 1.0f;

        public override void Run(FeatureBehaviour fb)
        {
            int selpos = fb.Data.Points[0].Count / 2;
            var met = fb.Data.Points[0][selpos];

            GameObject prefab = _prefabs[Random.Range(0, _prefabs.Length)];
            var go = Instantiate(prefab);
            go.name = fb.Data.Data.Id.ToString();
            go.transform.position = met;
            go.transform.SetParent(fb.transform, false);

            var bd = go.AddComponent<FeatureBehaviour>();
            bd.Init(fb.Data);

            var tm = go.GetComponent<IFeaturePropertySettable>();
            if (tm != null)
            {
                tm.Set(fb.Data.Properties);
            }

            TreeModel tree = TreeModel.ParseData(fb.Data.Properties);

            float scale = _scale;
            float runningDiameter = tree.diameter ?? 1.0f;
            while (runningDiameter > 1)
            {
                scale += 0.2f;
                runningDiameter -= 12.0f;
            }
            go.transform.localScale *= scale * Random.Range(.9f, 1.1f);

            float rotationY = Random.Range(0, 360);
            go.transform.localEulerAngles = new Vector3(0, rotationY, 0);
        }
    }
}