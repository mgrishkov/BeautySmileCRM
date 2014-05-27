using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySmileCRM.ViewModels
{
    public class AppointmentDetail : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        private int _id;
        private int _appointmentID;
        private int _staffID;
        private int _serviceID;
        private decimal _price;


        public int ID 
        {             
            get { return _id; }
            set 
            { 
                if(value != _id) 
                { 
                    _id = value; 
                    RaisePropertyChanged("ID"); 
                } 
            } 
        }
        public int AppointmentID 
        {  
            get { return _appointmentID; }    
            set 
            { 
                if(value != _appointmentID) 
                { 
                    _appointmentID = value; 
                    RaisePropertyChanged("AppointmentID"); 
                } 
            }
        }
        public int StaffID 
        {        
            get { return _staffID; }          
            set 
            {
                if(value != _staffID) 
                { 
                    _staffID = value; 
                    RaisePropertyChanged("StaffID"); 
                } 
            } 
        }
        public int ServiceID 
        {      
            get { return _serviceID; }        
            set 
            {
                if(value != _serviceID) 
                { 
                    _serviceID = value; 
                    RaisePropertyChanged("ServiceID"); 
                } 
            } 
        }
        public decimal Price 
        {      
            get { return _price; }            
            set 
            { 
                if(value != _price) 
                { 
                    _price = value; 
                    RaisePropertyChanged("Price"); 
                } 
            } 
        }

        public static implicit operator Models.AppointmentDetail(AppointmentDetail s)
        {
            return new Models.AppointmentDetail()
            {
                ID = s.ID,
                AppointmentID = s.AppointmentID,
                Price = s.Price,
                ServiceID = s.ServiceID,
                StaffID = s.StaffID
            };
        }
        public static implicit operator AppointmentDetail(Models.AppointmentDetail s)
        {
            return new AppointmentDetail()
            {
                ID = s.ID,
                AppointmentID = s.AppointmentID,
                Price = s.Price,
                ServiceID = s.ServiceID,
                StaffID = s.StaffID
            };
        }
    }
}
