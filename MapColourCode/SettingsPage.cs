using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals;

namespace RouteColourCode
{
    class AddSettingsPage : IExtendSettingsPages
    {
        #region IExtendSettingsPages Members

        public IList<ISettingsPage> SettingsPages
        {
            get 
            {
                List<ISettingsPage> pageList = new List<ISettingsPage>();
                pageList.Add(new SettingsPage());
                return pageList;
            }
        }

        #endregion
    }

    class SettingsPage: ISettingsPage
    {
        Control SettingsControl = null;
        
        #region ISettingsPage Members

        public Guid Id
        {
            get { return new Guid("E1DEED9E-939C-4DA3-B05B-506FFABD4819");}
        }

        public IList<ISettingsPage> SubPages
        {
            get { return null; }
        }

        #endregion

        #region IDialogPage Members

        public Control CreatePageControl()
        {
            SettingsControl = new SettingsPageControl();
            return SettingsControl;
        }

        public bool HidePage()
        {
            return true;
        }

        public string PageName
        {
            get { return "RouteColourCode"; }
        }

        public void ShowPage(string bookmark)
        {
            SettingsControl.Show();
        }

        public IPageStatus Status
        {
            get { return null; }
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            // Themes not supported yet
        }

        public string Title
        {
            get { return "RouteColourCode"; }
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            // Different cultures not supported
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
