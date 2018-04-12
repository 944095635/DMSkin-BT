using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DMBT.Models
{
    public class BT : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;



        string name;
        public string Name
        {
            set
            {
                name = value;
                if (PropertyChanged != null)
                { PropertyChanged(this, new PropertyChangedEventArgs("Name")); }
            }
            get { return name; }
        }
        string time;
        public string Time
        {
            set
            {
                time = value;
                if (PropertyChanged != null)
                { PropertyChanged(this, new PropertyChangedEventArgs("Time")); }
            }
            get { return time; }
        }


        string size;
        public string Size
        {
            set
            {
                size = value;
                if (PropertyChanged != null)
                { PropertyChanged(this, new PropertyChangedEventArgs("Size")); }
            }
            get { return size; }
        }

        string point;
        public string Point
        {
            set
            {
                point = value;
                if (PropertyChanged != null)
                { PropertyChanged(this, new PropertyChangedEventArgs("Point")); }
            }
            get { return point; }
        }

        BTType type;
        public BTType Type
        {
            set
            {
                type = value;
                if (PropertyChanged != null)
                { PropertyChanged(this, new PropertyChangedEventArgs("Type")); }
            }
            get { return type; }
        }


        string xunlei;
        public string Xunlei
        {
            set
            {
                xunlei = value;
                if (PropertyChanged != null)
                { PropertyChanged(this, new PropertyChangedEventArgs("Xunlei")); }
            }
            get { return xunlei; }
        }

        string magnet;
        public string Magnet
        {
            set
            {
                magnet = value;
                if (PropertyChanged != null)
                { PropertyChanged(this, new PropertyChangedEventArgs("Magnet")); }
            }
            get { return magnet; }
        }
    }
}
