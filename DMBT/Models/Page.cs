using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DMBT.Models
{
    public class Page : INotifyPropertyChanged
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

        string path;
        public string Path
        {
            set
            {
                path = value;
                if (PropertyChanged != null)
                { PropertyChanged(this, new PropertyChangedEventArgs("Path")); }
            }
            get { return path; }
        }

        bool isChecked;
        public bool IsChecked
        {
            set
            {
                isChecked = value;
                if (PropertyChanged != null)
                { PropertyChanged(this, new PropertyChangedEventArgs("IsChecked")); }
            }
            get { return isChecked; }
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

    }
}
