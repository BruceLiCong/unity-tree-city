namespace Mapbox.Unity.MeshGeneration.Modifiers
{
    using UnityEngine;
    using Mapbox.Unity.MeshGeneration.Components;
    using Mapbox.Unity.MeshGeneration.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [CreateAssetMenu(menuName = "Mapbox/Modifiers/Tree Prefab Modifier")]
    public class TreePrefabModifier : GameObjectModifier
    {
        private const string TREE_HEIGHT_KEY = "Tree Height";
        private const string DIAMETER_KEY = "Diameter";
        private const int INVALID_INT = -1;

        [SerializeField]
        private GameObject _prefab;

        [SerializeField]
        private bool _scaleDownWithWorld = false;

        private void ParseTreeData(Dictionary<string, object> properties)
        {
            object treeHeightObject;
            object diameterObject;

            properties.TryGetValue(TREE_HEIGHT_KEY, out treeHeightObject);
            properties.TryGetValue(DIAMETER_KEY, out diameterObject);

            int treeHeight;
            if (!int.TryParse(treeHeightObject.ToString(), out treeHeight))
            {
                treeHeight = INVALID_INT;
            }

            int diameter;
            if (!int.TryParse(diameterObject.ToString(), out diameter))
            {
                diameter = INVALID_INT;
            }
        }

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

            float scale = UnityEngine.Random.Range(.7f, 1.3f);
            go.transform.localScale *= scale;

            float rotationY = UnityEngine.Random.Range(0, 360);
            go.transform.localEulerAngles = new Vector3(0, rotationY, 0);

            ParseTreeData(fb.Data.Properties);
        }
    }
}