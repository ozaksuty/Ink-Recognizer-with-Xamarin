using System.Collections.Generic;

namespace InkRecognizerSample
{
    public class InkServiceRequestModel
    {
        public string language { get; set; }
        public List<Stroke> strokes { get; set; } = new List<Stroke>();
    }
    public class Stroke
    {
        private string _points;
        public int id { get; set; }
        public string points
        {
            get
            {
                return _points;
            }
            set
            {
                _points = value;
            }
        }
    }
}