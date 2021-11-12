using porductionLine_kai0ym.Abstractions;
using porductionLine_kai0ym.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace porductionLine_kai0ym
{
    public partial class Form1 : Form
    {
        private Toy _nextToy;
        private List<Abstractions.Toy> _toys = new List<Abstractions.Toy>();

        public Form1(List<Abstractions.Toy> toys)
        {
            _toys = toys;
        }
        private IToyFactory _factory;
        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value;
                DisplayNext();
            }
        }

        public Form1()
        {
            InitializeComponent();
            Factory = new BallFactory();

        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var toy = Factory.CreateNew();
            _toys.Add(toy);
            toy.Left = -toy.Width;
            mainPanel.Controls.Add(toy);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;
            foreach (var toy in _toys)
            {
                toy.MoveToy();
                if (toy.Left > maxPosition)
                    maxPosition = toy.Left;
            }

            if (maxPosition > 1000)
            {
                var oldestToy = _toys[0];
                mainPanel.Controls.Remove(oldestToy);
                _toys.Remove(oldestToy);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Form1 form &&
                   EqualityComparer<List<Abstractions.Toy>>.Default.Equals(_toys, form._toys);
        }

        private void btnSelectCar_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void DisplayNext()
        {
            if (_nextToy != null)
                Controls.Remove(_nextToy);
            _nextToy = Factory.CreateNew();
            _nextToy.Top = lblNext.Top + lblNext.Height + 20;
            _nextToy.Left = lblNext.Left;
            Controls.Add(_nextToy);
        }

        private void btnSelectBall_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory();
        }
    }
}
