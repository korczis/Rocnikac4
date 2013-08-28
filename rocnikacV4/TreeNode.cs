#region

using System.Collections.Generic;

#endregion

namespace rocnikacV4
{
    public class TreeNode
    {
        public TreeNode()
        {
            childs = new List<TreeNode>();
        }

        public TreeNode(Fairway over, Fairway to)
        {
            dest = to.Name;
            this.over = over.Name;
            childs = new List<TreeNode>();
        }

        public string dest { get; set; }

        public string over { get; set; }

        public int rank { get; set; }

        public List<TreeNode> childs { get; set; }

        public List<string> allRoutes(TreeNode root)
        {
            var cesty = new List<string>();
            string cesta;

            if (root.childs.Count > 0)
            {
                foreach (var potomek in root.childs)
                {
                    List<string> vysledek = allRoutes(potomek);
                    foreach (var s in vysledek)
                    {
                        cesta = root.over + "-" + root.dest + "-" + s;
                        cesty.Add(cesta);
                    }
                }
            }
            else
            {
                cesta = root.over + "-" + root.dest;
                cesty.Add(cesta);
                return cesty;
            }
            return cesty;
        }
    }
}