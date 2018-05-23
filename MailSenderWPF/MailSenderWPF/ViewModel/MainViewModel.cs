using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using MailSenderWPF.Services;
using System;
using System.Windows;
using Common;


namespace MailSenderWPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Email> _emails;
        private IDataAccessService _serviceProxy;
        private Email _emailInfo;

        public MainViewModel()
        {
            _serviceProxy = new DataAccessService();
            Emails = new ObservableCollection<Email>();
            EmailInfo = new Email();
            ReadAllCommand = new RelayCommand(GetName);
            SaveCommand = new RelayCommand<Email>(SaveEmail);
            DeleteCommand = new RelayCommand<Email>(DeleteEmail);
            

        }

        public RelayCommand ReadAllCommand { get; set; }
        public RelayCommand <Email> DeleteCommand { get; set; }
        public RelayCommand<Email> SaveCommand { get; set; }
        public RelayCommand<Email> GetCommand { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Email> Emails
        {
            get { return _emails; }
            set
            {
                _emails = value;
                RaisePropertyChanged(nameof(Emails));
            }
        }

        public Email EmailInfo
        {
            get { return _emailInfo; }
            set
            {
                _emailInfo = value;
                RaisePropertyChanged(nameof(EmailInfo));
            }
        }

        private void GetEmails()
        {
            Emails.Clear();
            foreach (var item in _serviceProxy.GetEmails())
            {
                Emails.Add(item);
            }
        }

        /// <summary>
        /// Метод поиска почты в базе данных по тегу Name
        /// </summary>
        private void GetName()
        {
            Emails.Clear();
            if (Name == null || Name == "")
                foreach (var item in _serviceProxy.GetEmails())
                {
                    Emails.Add(item);
                }
            else
                foreach (var item in _serviceProxy.GetEmails())
                {
                    if (Name.ToLower() == item.Name.ToLower() 
                        || Name.ToLower() == item.Name.Substring(0, Name.Length).ToLower())
                        Emails.Add(item);
                }
        }
        private void SaveEmail(Email email)
        {

            try
            {
                EmailInfo.Id = _serviceProxy.AddEmail(email);
                if (EmailInfo.Id != 0)
                {
                    Emails.Add(EmailInfo);
                    RaisePropertyChanged(nameof(EmailInfo));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Данный Айди нельзя использовать");
            }

        }
        private void DeleteEmail(Email email)
        {

            try
            {
                EmailInfo.Id = _serviceProxy.DeleteEmail(email);
            
                 //Emails.Remove(EmailInfo);
                _serviceProxy.DeleteEmail(email);
                    RaisePropertyChanged(nameof(EmailInfo));
                
            }
            catch (Exception)
            {
                //MessageBox.Show("Данный Айди нельзя использовать");
            }

        }
    }
}