using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap;
using GMap.NET;
using GMap.NET.WindowsForms;
namespace SASTR_Navigation
{
    public partial class Form1 : Form
    {
        PointLatLng initialPoint,finalPoint;
        MapRoute path;
        GMapOverlay overlayMarker = new GMapOverlay();
        GMapOverlay botPosition = new GMapOverlay();
        GMapOverlay overlayRoute = new GMapOverlay();
        public Form1()
        {
            InitializeComponent();
            initialPoint = new PointLatLng(26.510498, 80.230730);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            areaMap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            areaMap.Position = new PointLatLng(26.5144969, 80.2377076);
            GMap.NET.WindowsForms.Markers.GMarkerGoogle curPos = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(initialPoint, GMap.NET.WindowsForms.Markers.GMarkerGoogleType.blue_pushpin);
            label3.Text = initialPoint.Lat.ToString();
            label4.Text = initialPoint.Lng.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(finalPoint!=null)
            {
                timer1.Enabled = true;
            }
        }

        private void areaMap_MouseClick(object sender, MouseEventArgs e)
        {
            finalPoint = areaMap.FromLocalToLatLng(e.X, e.Y);
            label5.Text = finalPoint.Lng.ToString();
            label6.Text = finalPoint.Lat.ToString();
            GMap.NET.WindowsForms.Markers.GMarkerGoogle endpoint = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(finalPoint, GMap.NET.WindowsForms.Markers.GMarkerGoogleType.blue_pushpin);
            overlayMarker.Markers.Clear();
            overlayMarker.Markers.Add(endpoint);
            areaMap.Overlays.Add(overlayMarker);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GMap.NET.WindowsForms.Markers.GMarkerGoogle curPos = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(initialPoint, GMap.NET.WindowsForms.Markers.GMarkerGoogleType.blue_pushpin);
            path = GMap.NET.MapProviders.GoogleMapProvider.Instance.GetRoute(initialPoint, finalPoint, false, false, 15);
            GMapRoute route = new GMapRoute(path.Points, "My route");
            overlayRoute.Routes.Clear();
            overlayRoute.Routes.Add(route);
            areaMap.Overlays.Add(overlayRoute);
            label10.Text = (path.Distance * 1000).ToString();
            areaMap.Refresh();
            

        }
    }
}
