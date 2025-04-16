using Consolidated_App.Models.FramworkModels;
using CNET_V7_Domain.Misc.CommonTypes;
using Consolidated_App.AuthNavigation;
namespace Consolidated_App.Common.AuthNavigation
{
    public class NavigatorManager
    {
        static AuthenticationManager _authenticationManager;
        public NavigatorManager(AuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }
        public async Task<List<MenuItem>> PrepareNavigationList(List<NavigatorDTO>? module)
        {
            if (module == null)
            {
                return null;
            }
            var navigationsList = new List<MenuItem>();
            List<string> icons1 = new List<string> { "fa-cubes", "fa-briefcase", "fa-balance-scale", "fa-line-chart" };
            int iconIndex = 0;
            foreach (var item in module)
            {
                string currentIcon;
                if (iconIndex < icons1.Count)
                {
                    currentIcon = icons1[iconIndex];
                }
                else
                {
                    currentIcon = icons1.First();
                }
                var gslParent = new MenuItem()
                {
                    Title = item.name,
                    IconClass = currentIcon,
                    ChildNodes = await geNodes(item.children, item.name)
                };
                navigationsList.Add(gslParent);

                iconIndex++;
            }

            return navigationsList;
        }

        private async Task<List<MenuItem>> geNodes(List<NavigatorDTO> sub_module, string ControllerName)
        {
            if (sub_module == null)
            {
                return null;
            }

            string Controllername2 = ControllerName;
            var nodes = new List<MenuItem>();
            MenuItem menuItem;

            foreach (var item in sub_module)
            {
                var childs = item.children.ToList();

                menuItem = new MenuItem()
                {
                    Title = item.name,
                    IconClass = "fa-dot-circle-o",
                    Url = "/" + ControllerName + "/List/" + item.id,
                    ChildNodes = new List<MenuItem>()
                };

                foreach (var child in childs)
                {
                    var grandchilds = child.children.ToList();

                    var childNode = new MenuItem()
                    {
                        Title = child.name,
                        IconClass = "fa-circle-o",
                        Url = "/" + ControllerName + "/List/" + child.id,
                        ChildNodes = new List<MenuItem>()
                    };

                    foreach (var grandchild in grandchilds)
                    {

                        childNode.ChildNodes.Add(new MenuItem()
                        {
                            Title = grandchild.name,
                            IconClass = "fa-dot-circle-o",
                            Url = "/" + ControllerName + "/List/" + grandchild.id
                        });
                    }

                    menuItem.ChildNodes.Add(childNode);
                }

                nodes.Add(menuItem);
            }

            return nodes;
        }

    }
}
