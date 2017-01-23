using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinearVisualEncoding.examples.flight_fare
{
    public partial class FlightFareGenerationControl : UserControl
    {
        public class Destination
        {
            public string name;
            public int count;
            public TimeSpan duration;
            public double minFare, maxFare;
            public bool isMainAirport = false;

            public String Description { get {
                    return isMainAirport ? name + "[main]" : name;
                } }

            public Destination(string name, int count, TimeSpan duration, double minFare, double maxFare, bool isMainAirport = false)
            {
                this.name = name;
                this.count = count;
                this.duration = duration;
                this.minFare = minFare;
                this.maxFare = maxFare;
                this.isMainAirport = isMainAirport;
            }
        }

        Destination[] destinations = {
                new Destination("Hobart",2,new TimeSpan(6,30,0), 400, 500),
                new Destination("Launceston",3,new TimeSpan(4, 50,0), 300,500),
                new Destination("Perth",10,new TimeSpan(2,0,0),200,300),
                new Destination("Melbourne",25,new TimeSpan(2,0,0),70,150),
                new Destination("Sydney",20,new TimeSpan(2,0,0),200,300),
                new Destination("Adelaide",0,new TimeSpan(),0,0, isMainAirport:true),
                new Destination("Brisbane",10,new TimeSpan(3,0,0),200,500),
                new Destination("Canberra",3,new TimeSpan(1,30,0), 500, 700),
                new Destination("Gold Coast",2,new TimeSpan(3,0,0),200,400),
                new Destination("Cairns",1,new TimeSpan(3,30,0),250,500),
                new Destination("Darwin",2,new TimeSpan(4,0,0),1000,1200),
                new Destination("Alice Springs",2,new TimeSpan(2,0,0), 300,600) };

        public class Airline
        {
            public string name;
            public float proportion;

            public Airline(string name, float proportion)
            {
                this.name = name;
                this.proportion = proportion;
            }
        }

        Airline[] airlines = new Airline[]
        {
            new Airline("Airline A", 0.10f),
            new Airline("Airline B", 0.85f),
            new Airline("Airline C", 0.05f),
        };

        public void generateTransferGraph(string filename)
        {
            DateTime startTime = new DateTime(2015, 12, 1, 6, 0, 0);
            DateTime finishTime = new DateTime(2015, 12, 2, 1, 0, 0);

            TimeSpan d = finishTime - startTime;

            Random r = new Random();

            string mainAirport = destinations.First( airport => airport.isMainAirport ).name;

            double prOutlierFare = double.Parse(outlierFareProbability_textBox.Text);

            double minExpensiveFare = double.Parse(minExpensiveFare_textBox.Text);
            double maxExpensiveFare = double.Parse(maxExpensiveFare_textBox.Text);
            
            double minCheapFare = double.Parse(minCheapFare_textBox.Text);
            double maxCheapFare = double.Parse(maxCheapFare_textBox.Text);
            
            bool isIncoming = incomingFlights_radioButton.Checked;

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename))
            {
                file.WriteLine("\"source\",\"destination\",\"startTime\",\"finishTime\",\"price\",\"airline\"");

                foreach (var dst in destinations)
                {
                    //we generate the exact number of flights stipulated by the airport
                    for (int i = 0; i < dst.count; i++)
                    {
                        if( dst.isMainAirport)
                        {
                            continue;
                        }

                        //compute the start time is uniformly distributed over the time span of the dataset
                        //the durations of the flights are equal to the airport duration some random variation Uniform( min = 0, max = 15 minutes )
                        //all times are rounded to the nearest 5 minutes
                        var ts = TransferGraph.roundDown(startTime.AddMilliseconds((float)r.NextDouble() * d.TotalMilliseconds), new TimeSpan(0, 5, 0));
                        var tf = TransferGraph.roundUp(ts.AddMinutes((float)r.NextDouble() * 15).Add(dst.duration), new TimeSpan(0, 5, 0));

                        //the flights are divided amongst the airlines according to their proportion of the total flights
                        double ra = r.NextDouble();
                        double accumulatedProbability = 0;

                        string airline = airlines.Last().name;

                        foreach (var a in airlines)
                        {
                            accumulatedProbability += a.proportion;
                            if( ra <= accumulatedProbability)
                            {
                                airline = a.name;
                                break;
                            }
                        }

                        //prices can either be normal or outliers
                        //---if they are normal then the prices are uniformly distributed at a low rate (per route)
                        //---if they are outliers then they have an even change of being a low or high outlier
                        double price;
                        if (r.NextDouble() < prOutlierFare) //outlier fare
                        {
                            if (r.NextDouble() < 0.5)
                            {
                                price = minExpensiveFare + (maxExpensiveFare - minExpensiveFare) * r.NextDouble();
                            }else
                            {
                                price = minCheapFare + (maxCheapFare - minCheapFare) * r.NextDouble();
                            }
                        }
                        else// normal fare
                        {
                            price = dst.minFare + (dst.maxFare - dst.minFare) * r.NextDouble();
                        }


                        file.WriteLine("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\"",
                            isIncoming ? dst.name : mainAirport,
                            isIncoming ? mainAirport : dst.name,
                            ts.ToString("dd/MM/yyyy HH:mm"),
                            tf.ToString("dd/MM/yyyy HH:mm"),
                            price,
                            airline);
                    }
                }
            }
        }

        public FlightFareGenerationControl()
        {
            InitializeComponent();
        }

        private void generate_button_Click(object sender, EventArgs e)
        {
            if( saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            generateTransferGraph(saveFileDialog.FileName);

            System.Diagnostics.Process.Start(saveFileDialog.FileName);
        }

        private void FlightFareGenerationControl_Load(object sender, EventArgs e)
        {
            airports_listBox.DataSource = destinations;
            airports_listBox.DisplayMember = "Description";
        }

        private void airports_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var listbox = sender as ListBox;
            var destination = listbox.SelectedItem as Destination;

            if ( destination == null)
            {
                return;
            }

            isMainAirport_checkBox.Checked = destination.isMainAirport;
            noFlights_textBox.Text = String.Format("{0}", destination.count);
            flightDuration_textBox.Text = String.Format("{0}", destination.duration.TotalHours);

            minFare_textBox.Text = String.Format("{0}", destination.minFare);
            maxFare_textBox.Text = String.Format("{0}", destination.maxFare);
        }

        private void updateAirport_Leave(object sender, EventArgs e)
        {   
            var destination = airports_listBox.SelectedItem as Destination;

            if(isMainAirport_checkBox.Checked)
            {
                foreach (var d in destinations)
                {
                    d.isMainAirport = false;
                }
                destination.isMainAirport = isMainAirport_checkBox.Checked;
                isMainAirport_checkBox.Checked = true;
            }

            destination.count = int.Parse(noFlights_textBox.Text);
            destination.duration = TimeSpan.FromHours(double.Parse(flightDuration_textBox.Text));
            destination.minFare = double.Parse(minFare_textBox.Text);
            destination.maxFare = double.Parse(maxFare_textBox.Text);

            airports_listBox.DataSource = null;
            airports_listBox.DataSource = destinations;
            airports_listBox.DisplayMember = "Description";

        }
    }
}
