using System.Collections.Generic;

namespace rocnikacV4
{
    public class TreeNode
    {
        public string dest { get; set; }

        public string over { get; set; }

        public int rank { get; set; }

        public List<TreeNode> childs { get; set; }

        public TreeNode()
        {
            childs = new List<TreeNode>();
        }

        public TreeNode(Fairway over, Fairway to)
        {
            this.dest = to.Name;
            this.over = over.Name;
            childs = new List<TreeNode>();
        }

        public List<string> allRoutes(TreeNode root)
        {
            List<string> cesty = new List<string>();
            string cesta;

            if (root.childs.Count > 0)
            {
                foreach (TreeNode potomek in root.childs)
                {
                    List<string> vysledek = allRoutes(potomek);
                    foreach (string s in vysledek)
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