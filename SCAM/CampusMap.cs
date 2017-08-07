using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Google.Maps.Android.Data.Kml;
using Android.Gms.Maps;
using System.IO;
using Android.Gms.Maps.Model;
using Android.Locations;

namespace SCAM
{
    [Activity(Label = "Campus Map")]
    public class CampusMap : Activity, IOnMapReadyCallback
    {
        public MapFragment _mapFragment;
        public GoogleMap _map;
        public Location _currentLocation;
        public LocationManager _locationManager;

        public void OnMapReady(GoogleMap map)
        {
            _map = map;

            if (_map != null)
            {
                displayMap();
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);
                SetContentView(Resource.Layout.CampusMapLayout);

                //Add the map fragment (the map itself) to the android layout
                _mapFragment = FragmentManager.FindFragmentByTag("map") as MapFragment;
                if (_mapFragment == null)
                {
                    GoogleMapOptions mapOptions = new GoogleMapOptions()
                        .InvokeMapType(GoogleMap.MapTypeSatellite)
                        .InvokeZoomControlsEnabled(false)
                        .InvokeCompassEnabled(true);

                    FragmentTransaction fragTx = FragmentManager.BeginTransaction();
                    _mapFragment = MapFragment.NewInstance(mapOptions);
                    fragTx.Add(Resource.Id.map, _mapFragment, "map");
                    fragTx.Commit();
                }
                _mapFragment.GetMapAsync(this);
            }
            catch (Exception e)
            {
                //Give the user enough time to view the exception
                Console.WriteLine(e.ToString());
                System.Threading.Thread.Sleep(20000);
                throw;
            }

        }

        /// <summary>
        /// The method responible for displaying the map to the activity
        /// </summary>
        protected void displayMap()
        {
            Student student = Student.getStudent();
            List<Course> courses = student.Courses;

            //Stream input = Assets.Open($"ETSU Overlay.kml");

            //Add the building layers to the map
            //KmlLayer etsuLayer = new KmlLayer(_map, input, this);
            List<MarkerOptions> allBuildings = new List<MarkerOptions>();

            foreach (var item in courses)
            {
                MarkerOptions buildingMarker = new MarkerOptions();
                switch (item.buildingName.ToLower())
                {
                    case "nicks":
                        buildingMarker.SetPosition(new LatLng(BuildingLocations.nicksLat, BuildingLocations.nicksLong));
                        buildingMarker.SetTitle("Nicks Hall");
                        allBuildings.Add(buildingMarker);

                        break;

                    case "gilbreath":
                        buildingMarker.SetPosition(new LatLng(BuildingLocations.gilbreathLat, BuildingLocations.gilbreathLong));
                        buildingMarker.SetTitle("Gilbreath Hall");
                        allBuildings.Add(buildingMarker);
                        break;

                    case "roger stout":
                        buildingMarker.SetPosition(new LatLng(BuildingLocations.rogerStoutLat, BuildingLocations.rogerStoutLong));
                        buildingMarker.SetTitle("Roger Stout Hall");
                        allBuildings.Add(buildingMarker);
                        break;

                    case "wilson wallis":
                        buildingMarker.SetPosition(new LatLng(BuildingLocations.wilsonWallisLat, BuildingLocations.wilsonWallisLong));
                        buildingMarker.SetTitle($"Wilson Wallis Hall");
                        allBuildings.Add(buildingMarker);
                        break;

                    case "burleson":
                        buildingMarker.SetPosition(new LatLng(BuildingLocations.burlesonLat, BuildingLocations.burlesonLong));
                        buildingMarker.SetTitle("Burleson Hall");
                        allBuildings.Add(buildingMarker);
                        break;

                    case "brown":
                        buildingMarker.SetPosition(new LatLng(BuildingLocations.brownLat, BuildingLocations.brownLong));
                        buildingMarker.SetTitle("Brown Hall");
                        allBuildings.Add(buildingMarker);
                        break;
                }
            }

            foreach (var item in allBuildings)
            {
                _map.AddMarker(item);
            }

            //input = Assets.Open($"{buildingName}.kml");
            //KmlLayer layer = new KmlLayer(_map, input, this);
            //bool temp = layer.IsLayerOnMap;

            //MarkerOptions buildingMarker = new MarkerOptions();
            //buildingMarker.SetPosition(new LatLng(BuildingLocations.nicksLat, BuildingLocations.nicksLong));
            //buildingMarker.SetTitle($"{courses[0].buildingName}");
            //allBuildings.Add(buildingMarker);


            //Get the devices last known location
            _locationManager = (LocationManager)GetSystemService(LocationService);
            //_currentLocation = _locationManager.GetLastKnownLocation(LocationManager.GpsProvider);

            //Set the default view for ETSU when the user opens the map
            LatLng location = new LatLng(BuildingLocations.nicksLat, BuildingLocations.nicksLong);
            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(location);
            builder.Zoom(15);

            CameraPosition cameraPosition = builder.Build();
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);


            _map.MapType = GoogleMap.MapTypeNormal;
            _map.MyLocationEnabled = true;
            _map.SetIndoorEnabled(true);
            _map.BuildingsEnabled = true;
            _map.MoveCamera(cameraUpdate);

        }


    }

    public class BuildingLocations
    {
        public const double nicksLat = 36.302322;
        public const double nicksLong = -82.367873;
        public const double brownLat = 36.302322;
        public const double brownLong = -82.367873;
        public const double gilbreathLat = 36.303417;
        public const double gilbreathLong = -82.368498;
        public const double wilsonWallisLat = 36.300966;
        public const double wilsonWallisLong = -82.370628;
        public const double rogerStoutLat = 36.303365;
        public const double rogerStoutLong = -82.366165;
        public const double burlesonLat = 36.303957;
        public const double burlesonLong = -82.369679;
    }
}