using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Media;
using System.Xml.Serialization;

namespace RTMS_MVVM.Models
{
    public abstract class ModelBase: INotifyPropertyChanged
    {
        public string NAME { get; set; }
        private int PRICE_;
        public int PRICE
        {
            get { return this.PRICE_; }
            set
            {
                this.PRICE_ = value;
                this.OnPropertyChanged("PRICE");
            }
        }

        private Brush COLOR_ = Brushes.White;
        [XmlIgnore]
        public Brush COLOR
        {
            get { return this.COLOR_; }
            set
            {
                this.COLOR_ = value;
                this.STR_COLOR = value.ToString();
                this.OnPropertyChanged("COLOR");
            }
        }

        [XmlIgnore]
        private readonly BrushConverter ColorConverter = new BrushConverter();

        // 2021-08-09
        private string STR_COLOR_;
        public string STR_COLOR
        {
            get
            {
                return this.STR_COLOR_;
            }
            set
            {
                this.STR_COLOR_ = value;
                this.COLOR_ = (Brush)this.ColorConverter.ConvertFrom(value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                try
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
                }
                catch
                {
                }
            }
        }
    }
}
