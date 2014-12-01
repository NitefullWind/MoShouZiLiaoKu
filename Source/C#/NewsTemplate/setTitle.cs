using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelMTemplate
{
//    class Box : INotifyPropertyChanged
//    {
//        private string boxName;
//        public string BoxName
//        {
//            get { return boxName; }
//            set
//            {
//                boxName = value;
//                if (PropertyChanged != null)
//                {
//                    RaisePropertyChange("BoxName");
//                }
//            }
//        }
//        public event PropertyChangedEventHandler PropertyChanged;
//        public void RaisePropertyChange(string propertyName)
//        {
//            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
    class setTitle
    {
        public setTitle(string value1)
        {
            // TODO: Complete member initialization
            titleName = value1;
        } 
        private static string titleName;
        public static string getTigle()
        {
            return titleName;
        }
    }
}
