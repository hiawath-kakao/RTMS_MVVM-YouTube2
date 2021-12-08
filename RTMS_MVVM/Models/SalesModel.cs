using System;
using System.Collections.Generic;
using System.Text;

namespace RTMS_MVVM.Models
{
    class SalesModel : ModelBase
    {
        private int EA_;
        public int EA
        {
            get { return this.EA_; }
            set
            {
                this.EA_ = value;
                this.OnPropertyChanged("EA");
            }
        }
        private int EA_PRICE_;
        public int EA_PRICE
        {
            get { return this.EA_PRICE_; }
            set
            {
                this.EA_PRICE_ = value;
                this.OnPropertyChanged("EA_PRICE");
            }
        }
        public SalesModel()
        {            
        }
    }
}
