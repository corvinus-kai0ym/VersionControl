using Simulation_kai0ym.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulation_kai0ym
{
    public partial class Form1 : Form
    {

        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();

        List<int> MaleNr = new List<int>();
        List<int> FemaleNr = new List<int>();

        Random rng = new Random(3568);

        public Form1()
        {
            InitializeComponent();

            Population = GetPopulation(@"C:\Temp\nép.csv");
            BirthProbabilities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");

             

        }

        public void Simulation()
        {
            richTextBox1.Clear();
            MaleNr.Clear();
            FemaleNr.Clear();

            for (int year = 2005; year <= 2024; year++)
            {
                for (int i = 0; i < Population.Count; i++)
                {
                    void SimStep(int year, Person person)
                    {
                        if (!person.IsAlive) return;

                        byte age = (byte)(year - person.BirthYear);

                        double pDeath = (from x in DeathProbabilities
                                         where x.Gender == person.Gender && x.Age == age
                                         select x.P).FirstOrDefault();

                        if (rng.NextDouble() <= pDeath)
                            person.IsAlive = false;

                        if (person.IsAlive && person.Gender == Gender.Female)
                        {
                            double pBirth = (from x in BirthProbabilities
                                             where x.Age == age
                                             select x.P).FirstOrDefault();

                            if (rng.NextDouble() <= pBirth)
                            {
                                Person újszülött = new Person();
                                újszülött.BirthYear = year;
                                újszülött.NbrOfChildren = 0;
                                újszülött.Gender = (Gender)(rng.Next(1, 3));
                                Population.Add(újszülött);
                            }
                        }
                    }
                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                MaleNr.Add(nbrOfMales);
                FemaleNr.Add(nbrOfFemales);
            }
        }

        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> population = new List<Person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }
            }

            return population;
        }

        public List<BirthProbability> GetBirthProbabilities(string csvpath)
        {
            List<BirthProbability> BirthProbabilities = new List<BirthProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    BirthProbabilities.Add(new BirthProbability()
                    {
                        Age = int.Parse(line[0]),
                        NbrOfChildren = int.Parse(line[1]),
                        P= double.Parse(line[2]),
                    });
                }
            }

            return BirthProbabilities;
        }

        public List<DeathProbability> GetDeathProbabilities(string csvpath)
        {
            List<DeathProbability> DeathProbabilities = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    DeathProbabilities.Add(new DeathProbability()
                    {
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[0]),
                        Age = int.Parse(line[1]),
                        P = double.Parse(line[2]),
                    });
                }
            }

            return DeathProbabilities;
        }
        public void DisplayResults()
        {
            for (int year = 2005; year <= numericUpDown1.Value; year++)
            {
                int i = 0;
                string v = "Szimulációs év:" +year+ "\n\t Fiúk:" +MaleNr[i] +"\n\t Lányok:" +FemaleNr[i] +"\n";
                richTextBox1.Text += v;
                i++;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Simulation();
            DisplayResults();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog()==DialogResult.OK)
            {
                textBox1.Text = openFile.FileName;
            }

        }
    }
}
