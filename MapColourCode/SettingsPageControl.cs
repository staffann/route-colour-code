using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace RouteColourCode
{
    public partial class SettingsPageControl : UserControl
    {
        public SettingsPageControl()
        {
            InitializeComponent();
            MinSegmentTextBox.Text = MinSegmentLength.ToString();
            SegmentsTextBox.Text = SegmentsCount.ToString();
            MinSpeedTextBox.Text = MinSpeed.ToString();
            FilterWidthTextBox.Text = FilterWidth.ToString();
            ActivateCheckBox.Checked = colouringActivated;
            TrackComboBox.Text = TrackChoice;
        }

        private void MinSegmentTextBox_Validated(object sender, EventArgs e)
        {
            float sl;
            if (float.TryParse(MinSegmentTextBox.Text, out sl))
            {
                minSegmentLength = Math.Max(sl, 1);
            }
            MinSegmentTextBox.Text = MinSegmentLength.ToString();
        }

        private void SegmentsTextBox_Validated(object sender, EventArgs e)
        {
            int segCount;
            if (int.TryParse(SegmentsTextBox.Text, out segCount))
            {
                segmentsCount = Math.Max(segCount, 1);
            }
            SegmentsTextBox.Text = SegmentsCount.ToString();
        }

        private void MinSpeedTextBox_Validated(object sender, EventArgs e)
        {
            float ms;
            if (float.TryParse(MinSpeedTextBox.Text, out ms))
            {
                minSpeed = Math.Max(ms, 0);
            }
            MinSpeedTextBox.Text = MinSpeed.ToString();
        }

        private void FilterWidthTextBox_Validated(object sender, EventArgs e)
        {
            int width;
            if (int.TryParse(FilterWidthTextBox.Text, out width))
            {
                filterWidth = Math.Max(width, 1);
            }
            FilterWidthTextBox.Text = FilterWidth.ToString();
        }

        private void ActivateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            colouringActivated = ActivateCheckBox.Checked;
        }

        private void TrackComboBox_Validated(object sender, EventArgs e)
        {
            trackChoice = TrackComboBox.Text;
        }

        // Colouring activated for not
        private static bool colouringActivated = false;
        public static bool ColouringActivated
        {
            get { return colouringActivated; }
        }
        
        // Minimum length of a segment in meters
        private static float minSegmentLength = 10;
        public static float MinSegmentLength
        {
            get { return minSegmentLength; }
        }

        // No of segments
        private static int segmentsCount = 100;
        public static int SegmentsCount
        {
            get { return segmentsCount; }
        }

        // Min speed to use for colouring
        private static float minSpeed = 0;
        public static float MinSpeed
        {
            get { return minSpeed; }
        }

        private static string trackChoice = "Speed";
        public static string TrackChoice
        {
            get { return trackChoice; }
        }

        private static int filterWidth = 20;
        public static int FilterWidth
        {
            get { return filterWidth; }
        }

        public static void SaveSettings(System.Xml.XmlDocument xmlDoc, System.Xml.XmlElement pluginNode)
        {
            XmlElement element = xmlDoc.CreateElement("Activated");
            XmlText text = xmlDoc.CreateTextNode(ColouringActivated.ToString());
            element.AppendChild(text);
            pluginNode.AppendChild(element);

            element = xmlDoc.CreateElement("MinSegmentLength");
            text = xmlDoc.CreateTextNode(MinSegmentLength.ToString());
            element.AppendChild(text);
            pluginNode.AppendChild(element);

            element = xmlDoc.CreateElement("SegmentsCount");
            text = xmlDoc.CreateTextNode(SegmentsCount.ToString());
            element.AppendChild(text);
            pluginNode.AppendChild(element);

            element = xmlDoc.CreateElement("MinSpeed");
            text = xmlDoc.CreateTextNode(MinSpeed.ToString());
            element.AppendChild(text);
            pluginNode.AppendChild(element);

            element = xmlDoc.CreateElement("FilterWidth");
            text = xmlDoc.CreateTextNode(FilterWidth.ToString());
            element.AppendChild(text);
            pluginNode.AppendChild(element);

            element = xmlDoc.CreateElement("TrackChoice");
            text = xmlDoc.CreateTextNode(TrackChoice);
            element.AppendChild(text);
            pluginNode.AppendChild(element);
        }

        public static void LoadSettings(System.Xml.XmlDocument xmlDoc, System.Xml.XmlElement pluginNode)
        {
            foreach (XmlNode node in pluginNode.ChildNodes)
            {
                switch (node.Name)
                {
                    case "Activated":
                        bool.TryParse(node.ChildNodes[0].Value, out colouringActivated); // Ignore return code - can't do anything about it anyway if we can't read
                        break;
                    case "MinSegmentLength":
                        float.TryParse(node.ChildNodes[0].Value, out minSegmentLength); // Ignore return code - can't do anything about it anyway if we can't read
                        break;
                    case "SegmentsCount":
                        int.TryParse(node.ChildNodes[0].Value, out segmentsCount); // Ignore return code - can't do anything about it anyway if we can't read
                        break;
                    case "MinSpeed":
                        float.TryParse(node.ChildNodes[0].Value, out minSpeed); // Ignore return code - can't do anything about it anyway if we can't read
                        break;
                    case "TrackChoice":
                        trackChoice = node.ChildNodes[0].Value;
                        break;
                    case "FilterWidth":
                        int.TryParse(node.ChildNodes[0].Value, out filterWidth); // Ignore return code - can't do anything about it anyway if we can't read
                        break;
                }

            }
        }
    }
}
