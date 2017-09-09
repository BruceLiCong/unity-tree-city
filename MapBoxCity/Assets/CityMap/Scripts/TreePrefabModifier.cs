using UnityEngine;
using Mapbox.Unity.MeshGeneration.Components;
using Mapbox.Unity.MeshGeneration.Interfaces;
using Mapbox.Unity.MeshGeneration.Modifiers;

namespace TreeCity
{
    [CreateAssetMenu(menuName = "Mapbox/Modifiers/Tree Prefab Modifier")]
    public class TreePrefabModifier : GameObjectModifier
    {
        [SerializeField]
        private GameObject _prefab;

        [SerializeField]
        private bool _scaleDownWithWorld = false;

        public override void Run(FeatureBehaviour fb)
        {
            int selpos = fb.Data.Points[0].Count / 2;
            var met = fb.Data.Points[0][selpos];
            var go = Instantiate(_prefab);
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

            if (!_scaleDownWithWorld)
            {
                go.transform.localScale = Vector3.one / go.transform.lossyScale.x;
            }

            float scale = Random.Range(.7f, 1.3f);
            go.transform.localScale *= scale;

            float rotationY = Random.Range(0, 360);
            go.transform.localEulerAngles = new Vector3(0, rotationY, 0);

            TreeModel.ParseData(fb.Data.Properties);
        }
    }
}