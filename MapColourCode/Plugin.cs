using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals.Fitness;

namespace RouteColourCode
{
    class Plugin:IPlugin
    {
        #region IPlugin Members

        private static IApplication application;
        public IApplication Application
        {
            set { application = value; }
        }

        public static IApplication GetApplication()
        {
            return application;
        }

        public Guid Id
        {
            get { return new Guid("ebec2a6e-c1a2-4492-9c8b-24875a0f6348"); }
        }

        public string Name
        {
            get { return "RouteColourCode"; }
        }

        public void ReadOptions(System.Xml.XmlDocument xmlDoc, System.Xml.XmlNamespaceManager nsmgr, System.Xml.XmlElement pluginNode)
        {
            SettingsPageControl.LoadSettings(xmlDoc, pluginNode);
        }

        public string Version
        {
            get { return GetType().Assembly.GetName().Version.ToString(3); }
        }

        public void WriteOptions(System.Xml.XmlDocument xmlDoc, System.Xml.XmlElement pluginNode)
        {
            SettingsPageControl.SaveSettings(xmlDoc, pluginNode);
        }

        #endregion
    }
}
