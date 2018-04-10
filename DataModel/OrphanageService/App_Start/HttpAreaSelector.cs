using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace OrphanageService
{
    internal class HttpAreaSelector : IHttpControllerSelector
    {
        private const string VersionKey = "version";
        private const string NamespaceKey = "namespace";
        private const string ControllerKey = "controller";
        private readonly HttpConfiguration _configuration;
        private readonly Lazy<Dictionary<string, HttpControllerDescriptor>> _controllers;

        public HttpAreaSelector(HttpConfiguration config)
        {
            _configuration = config;
            _controllers = new Lazy<Dictionary<string, HttpControllerDescriptor>>(InitializeControllerDictionary);
        }

        private Dictionary<string, HttpControllerDescriptor> InitializeControllerDictionary()
        {
            var dictionary = new Dictionary<string, HttpControllerDescriptor>(StringComparer.OrdinalIgnoreCase);
            var assembliesResolver = _configuration.Services.GetAssembliesResolver();
            var controllersResolver = _configuration.Services.GetHttpControllerTypeResolver();
            var controllerTypes = controllersResolver.GetControllerTypes(assembliesResolver);
            foreach (var t in controllerTypes)
            {
                if (t.Namespace == null) continue;
                var segments = t.Namespace.Split(Type.Delimiter);
                var controllerName = t.Name.Remove(t.Name.Length - DefaultHttpControllerSelector.ControllerSuffix.Length);
                var key = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", segments[segments.Length - 2], controllerName);
                if (!dictionary.Keys.Contains(key))
                {
                    dictionary[key] = new HttpControllerDescriptor(_configuration, t.Name, t);
                }
            }
            return dictionary;
        }

        public HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var routeData = request.GetRouteData();
            var subroutes = (IEnumerable<IHttpRouteData>)routeData.Values["MS_SubRoutes"];
            var route = subroutes.First().Route;
            if (route == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            int firstBackslach = route.RouteTemplate.IndexOf('/');
            int secondBackslach = route.RouteTemplate.IndexOf('/', firstBackslach + 1);
            // for the pattern api/family
            if (secondBackslach == -1) secondBackslach = route.RouteTemplate.Length;
            var area = route.RouteTemplate.Substring(4, secondBackslach - firstBackslach - 1);
            if (area == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            string controllerName = null;
            if (route.RouteTemplate.ToLower().Contains("family") && !route.RouteTemplate.ToLower().Contains("familycard"))
            {
                if (route.RouteTemplate.ToLower().Contains("media"))
                    return _controllers.Value["Family.FamMedia"];
                else
                    return _controllers.Value["Family.Families"];
            }
            else
            {
                if (route.RouteTemplate.ToLower().Contains("media"))
                {
                    controllerName = area[0] + "media";
                }
                else
                {
                    controllerName = area + "s";
                }
            }
            var key = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", area, controllerName);
            HttpControllerDescriptor controllerDescriptor;
            if (_controllers.Value.TryGetValue(key, out controllerDescriptor))
                return controllerDescriptor;

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {
            return _controllers.Value;
        }
    }
}