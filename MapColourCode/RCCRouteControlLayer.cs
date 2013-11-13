using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals.Mapping;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.Algorithm;
using ZoneFiveSoftware.Common.Visuals.Util;

namespace RouteColourCode
{
    class RCCRouteControlLayer:IRouteControlLayer
    {
        private IRouteControlLayerProvider provider = null;
        private IRouteControl control = null;
        private IView activeView = null;
        private ISelectionProvider selProvider = null;
        private IList<IActivity> activities = new List<IActivity>();
        private List<IMapOverlay> overlayList = new List<IMapOverlay>();


        public RCCRouteControlLayer(IRouteControlLayerProvider provider, IRouteControl control)
        {
            this.provider = provider;
            this.control = control;

            Plugin.GetApplication().PropertyChanged += new PropertyChangedEventHandler(OnApplicationPropertyChanged);
            OnApplicationPropertyChanged(null, null);
            this.control.MapControlChanged += OnMapControlChanged;
        }
        
        #region IRouteControlLayer Members

        public IRouteControlLayerProvider Provider
        {
            get { throw new NotImplementedException(); }
        }

        public int ZOrder
        {
            get { return 1; }
        }

        #endregion

        void OnApplicationPropertyChanged(object obj, EventArgs args)
        {            
            if (Plugin.GetApplication().ActiveView != activeView)
            {
                //Guid DailyActivityViewGuid = new Guid("a2f041f1-2229-3800-b69d-febacb75af80");
                //Guid ActivityReportsViewGuid = new Guid("a5992d34-3111-355b-8f36-98e521356f85");
                Guid DailyActivityViewGuid = new Guid("1dc82ca0-88aa-45a5-a6c6-c25f56ad1fc3");
                Guid ActivityReportsViewGuid = new Guid("99498256-cf51-11db-9705-005056c00008");

                // Remove any event handler from old view selection provider
                if (activeView != null)
                {
                    if (activeView.Id.Equals(DailyActivityViewGuid)
                        || activeView.Id.Equals(ActivityReportsViewGuid))
                    {
                        selProvider.SelectedItemsChanged -= new EventHandler(OnViewSelectionChanged);
                    }
                }

                // Store new active view
                activeView = Plugin.GetApplication().ActiveView;

                // Add event handler for changed selection
                if (activeView.Id.Equals(DailyActivityViewGuid)
                    || activeView.Id.Equals(ActivityReportsViewGuid))
                {
                    if (activeView.Id.Equals(DailyActivityViewGuid))
                    {
                        IDailyActivityView view = (IDailyActivityView)activeView;
                        selProvider = view.SelectionProvider;
                    }
                    else
                    {
                        IActivityReportsView view = (IActivityReportsView)activeView;
                        selProvider = view.SelectionProvider;
                    }

                    selProvider.SelectedItemsChanged += new EventHandler(OnViewSelectionChanged);
                    OnViewSelectionChanged(null, null);
                }
            }
        }

        void OnViewSelectionChanged(object obj, EventArgs args)
        {
            IList<IActivity> oldActivities = activities;
            activities = new List<IActivity>(CollectionUtils.GetAllContainedItemsOfType<IActivity>(selProvider.SelectedItems));
            bool firstIsEqual = (activities.Count == oldActivities.Count);
            if (activities.Count > 0 && oldActivities.Count > 0 && activities[0] != oldActivities[0])
            {
                firstIsEqual = false;
            }
            if (!firstIsEqual)
            {
                RefreshRoute();
            }
        }

        void OnMapControlChanged(object obj, EventArgs args)
        {
            RefreshRoute();
        }

        void RefreshRoute()
        {
            if (overlayList.Count > 0)
            {
                control.MapControl.RemoveOverlays(overlayList);
                overlayList.Clear();
            }
            if (SettingsPageControl.ColouringActivated && activities.Count == 1)
            {
                //control.MapControl.ClearOverlays();
                IActivity activity = activities[0];
                if (activity.GPSRoute != null)
                {
                    ActivityInfo actInfo = ActivityInfoCache.Instance.GetInfo(activity);
                    IDistanceDataTrack distanceMetersTrack = actInfo.ActualDistanceMetersTrack;

                    // Find each segments speed as well as the max and min segment speed
                    List<double> resultList = new List<double>();
                    float unusedMinResult = 0, unusedMaxResult = 0;
                    float segmentDistance = distanceMetersTrack.Max / SettingsPageControl.SegmentsCount;
                    segmentDistance = Math.Max(segmentDistance, SettingsPageControl.MinSegmentLength);

                    if (SettingsPageControl.TrackChoice.CompareTo("Speed") == 0)
                    {
                        CreateSpeedSegments(resultList, distanceMetersTrack, segmentDistance, out unusedMinResult, out unusedMaxResult);
                    }
                    else if (SettingsPageControl.TrackChoice.CompareTo("Altitude") == 0)
                    {
                        if (actInfo.HasElevationTrackData)
                        {
                            CreateMeanValueSegments(resultList, distanceMetersTrack, actInfo.SmoothedElevationTrack, segmentDistance, out unusedMinResult, out unusedMaxResult);
                        }
                    }
                    else if (SettingsPageControl.TrackChoice.CompareTo("Heart rate") == 0)
                    {
                        if (actInfo.SmoothedHeartRateTrack != null)
                        {
                            CreateMeanValueSegments(resultList, distanceMetersTrack, actInfo.SmoothedHeartRateTrack, segmentDistance, out unusedMinResult, out unusedMaxResult);
                        }
                    }

                    // Filter the values in the resultList using a FIR filter
                    // Filter from both ends in order to avoid filter lag
                    List<double> filterBufferList = new List<double>();
                    // Initialize the filter buffer
                    for (int i = 0; i < SettingsPageControl.FilterWidth; i++)
                    {
                        if (resultList.Count > 0)
                            filterBufferList.Add(resultList[0]);
                        else
                            filterBufferList.Add(0);
                    }
                    List<double> tempResultList = new List<double>();
                    foreach (double actualResult in resultList)
                    {
                        filterBufferList.RemoveAt(0);
                        filterBufferList.Add(actualResult);
                        double sum = 0;
                        foreach (double filterBufferEntry in filterBufferList)
                        {
                            sum += filterBufferEntry;
                        }
                        tempResultList.Add(sum / filterBufferList.Count);
                    }
                    // Now filter backwards
                    tempResultList.Reverse();
                    filterBufferList.Clear();
                    resultList.Clear();
                    for (int i = 0; i < SettingsPageControl.FilterWidth; i++)
                    {
                        filterBufferList.Add(tempResultList[0]);
                    }
                    foreach (double actualResult in tempResultList)
                    {
                        filterBufferList.RemoveAt(0);
                        filterBufferList.Add(actualResult);
                        double sum = 0;
                        foreach (double filterBufferEntry in filterBufferList)
                        {
                            sum += filterBufferEntry;
                        }
                        resultList.Add(sum / filterBufferList.Count);
                    }
                    resultList.Reverse();

                    // Get max and min values of the filtered results
                    double minResult, maxResult;
                    GetMaxMinSegmentValue(resultList, out minResult, out maxResult);
                    
                    // Create coloured overlays and add them to the map
                    int oldGPSRouteIndex = 0;
                    double distance = 0;

                    foreach (double actualResult in resultList)
                    {
                        distance = distance + segmentDistance;

                        // Colour the segment accordingly
                        double colourWarmth = Math.Max(Math.Min((actualResult - minResult) / (maxResult - minResult), 1), 0);
                        double redFill = 255 * Math.Sin(3.14 / 2 * 2 * Math.Max(colourWarmth - 0.5, 0));
                        double blueFill = 255 * Math.Cos(3.14 / 2 * 2 * Math.Min(colourWarmth, 0.5));
                        double greenFill = 255 * Math.Sin(3.14 * colourWarmth);

                        // A better attempt
                        redFill = 255 * Math.Min(4 * Math.Max(colourWarmth - 0.5, 0), 1);
                        blueFill = 255 * (1 - Math.Min(4 * Math.Max(colourWarmth - 0.25, 0), 1));
                        greenFill = 255 * (Math.Min(4 * colourWarmth, 1) - Math.Min(4 * Math.Max(colourWarmth - 0.75, 0), 1));

                        // An attempt to improve, allowing more of the intermediate colors between blue, green and red
                        //redFill = 255 * Math.Sin(3.14 / 2 * Math.Min(4 * Math.Max(colourWarmth - 0.5, 0), 1));
                        //blueFill = 255 * Math.Sin(3.14 / 2 * (1 - Math.Min(4 * Math.Max(colourWarmth - 0.25, 0), 1)));
                        //greenFill = 255 * Math.Sin(3.14 / 2 * (Math.Min(4 * colourWarmth, 1) - Math.Min(4 * Math.Max(colourWarmth - 0.75, 0), 1)));
                        Color color = System.Drawing.Color.FromArgb((int)redFill, (int)greenFill, (int)blueFill);

                        List<IGPSPoint> gpsPointList = new List<IGPSPoint>();
                        for (int i = oldGPSRouteIndex; i < activity.GPSRoute.Count && distanceMetersTrack[i].Value <= distance; i++)
                        {
                            gpsPointList.Add(activity.GPSRoute[i].Value);
                        }

                        MapPolyline line = new MapPolyline(gpsPointList, Plugin.GetApplication().SystemPreferences.RouteSettings.RouteWidth+2, color);
                        overlayList.Add(line);

                        oldGPSRouteIndex = Math.Max(oldGPSRouteIndex + gpsPointList.Count - 1, 0);
                    }
                    control.MapControl.AddOverlays(overlayList);
                }
                
            }

        }

        void CreateSpeedSegments(IList<double> resultList, IDistanceDataTrack distanceMetersTrack, float segmentDistance, out float minResult, out float maxResult)
        {
            // Find each segments speed as well as the max and min segment speed
            float maxSpeed = 0; // m/s 
            float minSpeed = 1000;
            float distance;
            float oldDistance = 0;
            DateTime oldDt = distanceMetersTrack.GetTimeAtDistanceMeters(oldDistance);
            while (oldDistance < distanceMetersTrack.Max)
            {
                distance = oldDistance + segmentDistance;
                if (distance > distanceMetersTrack.Max)
                {
                    distance = distanceMetersTrack.Max;
                }
                DateTime dt = distanceMetersTrack.GetTimeAtDistanceMeters(distance);
                if (!dt.Equals(oldDt))
                {
                    float actualSpeed = (distance - oldDistance) / (float)dt.Subtract(oldDt).TotalSeconds;
                    resultList.Add(actualSpeed);

                    maxSpeed = Math.Max(maxSpeed, actualSpeed);
                    minSpeed = Math.Min(minSpeed, actualSpeed);
                }

                oldDistance = distance;
                oldDt = dt;
            }
            minSpeed = Math.Max(minSpeed, SettingsPageControl.MinSpeed);

            minResult = minSpeed;
            maxResult = maxSpeed;
        }

        void CreateMeanValueSegments(IList<double> resultList, IDistanceDataTrack distanceMetersTrack, INumericTimeDataSeries dataTrack, float segmentDistance, out float minResult, out float maxResult)
        {
            maxResult = 0; // m/s 
            minResult = 1000;

            if (dataTrack != null && dataTrack.Count > 0)
            {
                // Find each segments mean, max and min result
                float distance;
                float oldDistance = 0;
                DateTime oldDt = distanceMetersTrack.GetTimeAtDistanceMeters(oldDistance);
                while (oldDistance < distanceMetersTrack.Max)
                {
                    distance = oldDistance + segmentDistance;
                    if (distance > distanceMetersTrack.Max)
                    {
                        distance = distanceMetersTrack.Max;
                    }
                    DateTime dt = distanceMetersTrack.GetTimeAtDistanceMeters(distance);
                    if (!dt.Equals(oldDt))
                    {
                        float first, last, min, max, avg;
                        //if(oldDt.CompareTo(distanceMetersTrack.StartTime.AddSeconds(distanceMetersTrack[distanceMetersTrack.Count-3].ElapsedSeconds))>0)
                        //{
                        //    oldDt = distanceMetersTrack.StartTime.AddSeconds(distanceMetersTrack[distanceMetersTrack.Count - 3].ElapsedSeconds);
                        //}
                        ZoneFiveSoftware.Common.Data.Algorithm.NumericTimeDataSeries.MinMaxAvgRange(dataTrack,
                                                                                                    oldDt,
                                                                                                    dt,
                                                                                                    new ValueRangeSeries<DateTime>(),
                                                                                                    float.MinValue,
                                                                                                    out first,
                                                                                                    out last,
                                                                                                    out min,
                                                                                                    out max,
                                                                                                    out avg);
                        if (!float.IsNaN(avg))
                        {
                            resultList.Add(avg);

                            maxResult = Math.Max(maxResult, avg);
                            minResult = Math.Min(minResult, avg);
                        }
                        //if (double.IsNaN(avg))
                        //{
                        //    // Only created to have somewhere to put a breakpoint for debugging
                        //    DateTime timetest = distanceMetersTrack.StartTime.AddSeconds(distanceMetersTrack.TotalElapsedSeconds);
                        //    DateTime lastpointTime = distanceMetersTrack.EntryDateTime(distanceMetersTrack[distanceMetersTrack.Count-1]);
                        //    DateTime SecondLastpointTime = distanceMetersTrack.EntryDateTime(distanceMetersTrack[distanceMetersTrack.Count - 2]);
                        //    DateTime ThirdLastpointTime = distanceMetersTrack.EntryDateTime(distanceMetersTrack[distanceMetersTrack.Count - 3]);
                        //    MessageDialog b = new MessageDialog();                            
                        //}
                    }

                    oldDistance = distance;
                    oldDt = dt;
                }
            }
        }

        void GetMaxMinSegmentValue(IList<double> resultList, out double minResult, out double maxResult)
        {
            maxResult = 0; // m/s 
            minResult = 1000;
            foreach (double value in resultList)
            {
                maxResult = Math.Max(maxResult, value);
                minResult = Math.Min(minResult, value);
                if (SettingsPageControl.TrackChoice.CompareTo("Speed") == 0)
                {
                    minResult = Math.Max(minResult, SettingsPageControl.MinSpeed);
                }
            }
        }
    }
}
