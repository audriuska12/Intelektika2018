using NeuralNet.NeuralNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace NeuralNet
{
    public partial class Form1 : Form
    {
        List<NormalizedDonor> TrainingDonors;
        List<NormalizedDonor> TestDonors;
        NeuralNetwork nn = new NeuralNetwork(0.9, new int[] { 3, 3, 1 });

        public Form1()
        {
            List<Donor> DonorList = ReadData();
            TrainingDonors = normalizeData(DonorList.GetRange(0,(DonorList.Count*4)/5));
            TestDonors = normalizeData(DonorList.GetRange(1 + (DonorList.Count * 4) / 5, (DonorList.Count - (DonorList.Count * 4) / 5)-1));
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NetworkHelper.ToTreeView(treeView1, nn);
            NetworkHelper.ToPictureBox(pictureBox1, nn, 400, 100);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rng = new Random();
            NormalizedDonor d = TestDonors[rng.Next(TestDonors.Count)];
            double[] ins = { d.Year, d.OperationYear, d.AxillaryNodes };



            nn.Run(new List<double>(ins));

            NetworkHelper.ToTreeView(treeView1, nn);
            NetworkHelper.ToPictureBox(pictureBox1, nn,400, 100);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random rng = new Random();
            for (int i = 0; i < 100000; i++) {
                NormalizedDonor d = TrainingDonors[rng.Next(TrainingDonors.Count)];
                double[] ins = { d.Year, d.OperationYear, d.AxillaryNodes };
                double[] ots = { d.Status };
                nn.Train(new List<double>(ins), new List<double>(ots));
            }
            NetworkHelper.ToTreeView(treeView1, nn);
            NetworkHelper.ToPictureBox(pictureBox1, nn,400, 100);
        }

        private List<Donor> ReadData()
        {
            List<Donor> DonorList = new List<Donor>();

            string[] lines = System.IO.File.ReadAllLines(@"haberman.data.txt");

            foreach( string line in lines)
            {
                string[] input = line.Split(',');
                DonorList.Add(new Donor(int.Parse(input[0]), int.Parse(input[1]), int.Parse(input[2]), int.Parse(input[3])));
            }
            

            return DonorList;
        }

        private List<NormalizedDonor> normalizeData(List<Donor> donors) {
            Donor donorMax = new Donor(donors[0].Year, donors[0].OperationYear, donors[0].AxillaryNodes, donors[0].Status);
            Donor donorMin = new Donor(donors[0].Year, donors[0].OperationYear, donors[0].AxillaryNodes, donors[0].Status);
            foreach (Donor d in donors) {
                if (d.Year > donorMax.Year)
                {
                    donorMax.Year = d.Year;
                }
                else if (d.Year < donorMin.Year) {
                    donorMin.Year = d.Year;
                }
                if (d.OperationYear > donorMax.OperationYear)
                {
                    donorMax.OperationYear = d.OperationYear;
                }
                else if (d.OperationYear < donorMin.OperationYear)
                {
                    donorMin.OperationYear = d.OperationYear;
                }
                if (d.AxillaryNodes > donorMax.AxillaryNodes)
                {
                    donorMax.AxillaryNodes = d.AxillaryNodes;
                }
                else if (d.AxillaryNodes < donorMin.AxillaryNodes)
                {
                    donorMin.AxillaryNodes = d.AxillaryNodes;
                }
                if (d.Status > donorMax.Status)
                {
                    donorMax.Status = d.Status;
                }
                else if (d.Status < donorMin.Status)
                {
                    donorMin.Status = d.Status;
                }
            }
            List<NormalizedDonor> norm = new List<NormalizedDonor>();
            foreach (Donor d in donors) {
                norm.Add(new NormalizedDonor((double)(d.Year - donorMin.Year) / (donorMax.Year - donorMin.Year), (double)(d.OperationYear - donorMin.OperationYear) / (donorMax.OperationYear - donorMin.OperationYear), (double)(d.AxillaryNodes - donorMin.AxillaryNodes) / (donorMax.AxillaryNodes - donorMin.AxillaryNodes), (double)(d.Status - donorMin.Status) / (donorMax.Status - donorMin.Status)));
            }
            return norm;
        }

    }


}