using ImageSearchWPF.Command;
using ImageSearchWPF.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System;
using ImageSearchWPF.API;
using ImageSearchWPF.API.Data;
using ImageSearchWPF.API.Interface;
using ImageSearchWPF.Utils;

namespace ImageSearchWPF.ViewModel
{
    public class PhotoViewModel : INotifyPropertyChanged
    {
        IFeedApi _feedApi;

        string _imageSearchKeyword;
        public string ImageSearchKeyword
        {
            get
            {
                return _imageSearchKeyword;
            }
            set
            {
                _imageSearchKeyword = value;
                OnPropertyChanged("ImageSearchKeyword");
                // ((RelayCommand)SearchCommand).RaiseCanExecuteChanged();


            }
        }

        ObservableCollection<Photo> _photoList;
        public ObservableCollection<Photo> PhotoList
        {
            get
            {
                return _photoList;
            }
            set
            {
                _photoList = value;
                //if (_photoList.Count > 0)
                //    IsPhotoListEmpty = false;
                //else
                //    IsPhotoListEmpty = true;
                OnPropertyChanged("PhotoList");
            }
        }

        string _emptyPhotoListMessage;
        public string EmptyPhotoListMessage
        {
            get
            {
                return _emptyPhotoListMessage;
            }
            set
            {
                _emptyPhotoListMessage = value;
                OnPropertyChanged("EmptyPhotoListMessage");

            }
        }

        bool _isPhotoListEmpty;
        public bool IsPhotoListEmpty
        {
            get
            {
                return _isPhotoListEmpty;
            }
            set
            {
                _isPhotoListEmpty = value;
                OnPropertyChanged("IsPhotoListEmpty");

            }
        }


        private ICommand _SubmitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                if (_SubmitCommand == null)
                {
                    _SubmitCommand = new RelayCommand(SubmitExecute, CanSubmitExecute);
                }
                return _SubmitCommand;
            }
        }


        public void SubmitExecute(object obj)
        {
            //Check if search string is null or empty
            if (String.IsNullOrWhiteSpace(ImageSearchKeyword))
            {
                EmptyPhotoListMessage = ConstantsUtility.EmptySearchStringErrorMessage;
                return;
            }

            //Fetch data from api call
            var result = (FlickerFeed)_feedApi.ImageSearch(ImageSearchKeyword);
            PhotoList.Clear();
            if (result != null && result.IsSuccessful)
            {
                foreach (var i in result.Entry)
                {
                    foreach (var link in i.Link)
                    {
                        if (link.Type.Contains(ConstantsUtility.LinkTypeImageString))
                        {
                            var pic = new Photo()
                            {
                                Title = i.Title,
                                Url = link.Href.ToString()
                            };
                            PhotoList.Add(pic);
                        }
                    }
                }
                CheckAndUpdateIsPhotoListEmpty();
            }
            else
            {
                IsPhotoListEmpty = true;
                //We can handle error message based on the business logic scenarios
                if (result != null)
                    EmptyPhotoListMessage = result.ErrorMessage;
                else
                    EmptyPhotoListMessage = ConstantsUtility.ErrorMessageString;
            }
        }

        private bool CanSubmitExecute(object arg)
        {
            return true;
        }

        private void CheckAndUpdateIsPhotoListEmpty()
        {

            if (_photoList.Count > 0)
            {
                IsPhotoListEmpty = false;
                EmptyPhotoListMessage = String.Empty;
            }
            else
            {
                IsPhotoListEmpty = true;
                EmptyPhotoListMessage = ConstantsUtility.NoRecordFoundErrorMessageString;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Constructors
        public PhotoViewModel() : this(new FlickerFeedApiClient())
        {

        }
        public PhotoViewModel(IFeedApi feedApi)
        {
            _photoList = new ObservableCollection<Photo>();
            _feedApi = feedApi;

        }
        #endregion
    }
}
