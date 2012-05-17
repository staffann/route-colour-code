using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals.Mapping;
using ZoneFiveSoftware.Common.Visuals.Fitness;
namespace RouteColourCode
{
    class RCCExtendRouteControlLayerProviders : IExtendRouteControlLayerProviders
    {
        #region IExtendRouteControlLayerProviders Members

        public IList<IRouteControlLayerProvider> RouteControlLayerProviders
        {
            get
            {
                List<IRouteControlLayerProvider> RouteControlProviders = new List<IRouteControlLayerProvider>();
                RouteControlProviders.Add(new RCCRouteControlLayerProvider());
                return RouteControlProviders; 
            }
        }

        #endregion
    }
    
    class RCCRouteControlLayerProvider:IRouteControlLayerProvider
    {
        #region IRouteControlLayerProvider Members

        public IRouteControlLayer CreateControlLayer(IRouteControl control)
        {
            return new RCCRouteControlLayer(this, control);
        }

        public Guid Id
        {
            get { return new Guid("289864D3-6C9D-4468-A1FB-66EBC4684306"); }
        }

        public string Name
        {
            get { return "RouteColourCode"; }
        }

        #endregion
    }
}
