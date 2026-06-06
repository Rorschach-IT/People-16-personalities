using Microsoft.AspNetCore.Mvc.Rendering;

namespace PeoplePersonalities.Services
{
    public static class ReplaceNavStyle
    {
        public static string IsActive(this IHtmlHelper html,
        string controller,
        string action)
        {
            var routeData = html.ViewContext.RouteData.Values;

            var currentAction = routeData["action"]?.ToString();
            var currentController = routeData["controller"]?.ToString();

            return string.Equals(controller, currentController, StringComparison.OrdinalIgnoreCase)
                   && string.Equals(action, currentAction, StringComparison.OrdinalIgnoreCase)
                ? "active"
                : "";
        }
    }
}
