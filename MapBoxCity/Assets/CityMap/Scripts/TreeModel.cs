namespace TreeCity
{
    using System.Collections.Generic;

    public class TreeModel
    {
        private const string TREE_HEIGHT_KEY = "Tree Height";
        private const string DIAMETER_KEY = "Diameter";
        public const int INVALID_INT = -1;

        private TreeModel() { }

        public int diameter;
        public int height;

        public static TreeModel ParseData(Dictionary<string, object> properties)
        {
            object treeHeightObject;
            object diameterObject;
            properties.TryGetValue(TREE_HEIGHT_KEY, out treeHeightObject);
            properties.TryGetValue(DIAMETER_KEY, out diameterObject);

            TreeModel tree = new TreeModel();

            if (!int.TryParse(treeHeightObject.ToString(), out tree.height))
            {
                tree.height = INVALID_INT;
            }
            if (!int.TryParse(diameterObject.ToString(), out tree.diameter))
            {
                tree.diameter = INVALID_INT;
            }

            return tree;
        }
    }
}