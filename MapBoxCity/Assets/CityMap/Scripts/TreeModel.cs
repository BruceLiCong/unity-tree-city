namespace TreeCity
{
    using System.Collections.Generic;

    public class TreeModel
    {
        private const string TREE_HEIGHT_KEY = "Tree Height";
        private const string DIAMETER_KEY = "Diameter";

        private TreeModel() { }

        public float? diameter = null;
        public float? height = null;

        public static TreeModel ParseData(Dictionary<string, object> properties)
        {
            object treeHeightObject;
            object diameterObject;
            properties.TryGetValue(TREE_HEIGHT_KEY, out treeHeightObject);
            properties.TryGetValue(DIAMETER_KEY, out diameterObject);

            TreeModel tree = new TreeModel();

            float val;
            if (float.TryParse(treeHeightObject.ToString(), out val))
            {
                tree.height = val;
            }
            if (float.TryParse(diameterObject.ToString(), out val))
            {
                tree.diameter = val;
            }

            return tree;
        }
    }
}