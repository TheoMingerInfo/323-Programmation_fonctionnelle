using Gpx;

namespace Rando
{
    public partial class Rando : Form
    {
        string gpxFile = @"..\..\..\gpx\gemmikandersteg.gpx";
        List<Trackpoint> trackPoints = new();
        private const double LAT_OFFSET = 46.39;
        private const double LONG_OFFSET = 7.61;
        public Rando()
        {
            InitializeComponent();

            if (!File.Exists(gpxFile))
            {
                MessageBox.Show($"Fichier {gpxFile} non trouvé !!");
            }

            StreamReader streamReader = new StreamReader(gpxFile);

            using (GpxReader reader = new GpxReader(streamReader.BaseStream))
            {
                while (reader.Read())
                {
                    switch (reader.ObjectType)
                    {
                        case GpxObjectType.Track:
                            //writer.WriteTrack(reader.Track);
                            var gpxPoints = reader.Track.ToGpxPoints();

                            //TODO convertir les gpxPoints en points
                            //avec un SELECT ;-)
                            var converted = gpxPoints.Select(gpxPoint => new Trackpoint()
                            {
                                Elevation = gpxPoint.Elevation,
                                Latitude = gpxPoint.Latitude,
                                Longitude = gpxPoint.Longitude
                            }).ToList();
                            trackPoints.AddRange(converted.ToList());
                            break;
                    }
                }
            }
        }

        private double GetAllDistance()
        {
            List<GpxPoint> points = new();
            var streamReader = new StreamReader(gpxFile);
            using (GpxReader reader = new GpxReader(streamReader.BaseStream))
            {
                while (reader.Read())
                {
                    if (reader.ObjectType == GpxObjectType.Track)
                    {
                        points = reader.Track.ToGpxPoints().ToList();
                    }
                }
            }
            double distance = 0;
            distance = points.Skip(1).Zip(points, (previous, next) => previous.GetDistanceFrom(next)).Sum();
            distance2 = points.Aggregate(0,)
            return distance;
        }

        private void Rando_Form_Paint(object sender, PaintEventArgs e)
        {
            Pen myPen = new Pen(Color.Red);
            myPen.Width = 2;

            Point[] points = trackPoints.Select(tp => new Point((int)Math.Round((tp.Longitude - LONG_OFFSET) * 10000), this.ClientRectangle.Height - (int)Math.Round((tp.Latitude - LAT_OFFSET) * 10000))).ToArray();

            //Point[] points = new Point[4] { new Point(30,50), new Point(50,10), new Point(80,50), new Point(111,400) };
            this.CreateGraphics().DrawLines(myPen, points);
        }
    }
}
