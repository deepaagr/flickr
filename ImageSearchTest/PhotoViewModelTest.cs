using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageSearchWPF.ViewModel;
using Moq;
using ImageSearchWPF.API.Interface;
using ImageSearchWPF.API.Data;
using ImageSearchWPF.Utils;
using System.Collections.Generic;

namespace ImageSearchTest
{
    [TestClass]
    public class PhotoViewModelTest
    {
        [TestMethod]
        public void SubmitExecuteNullAsSearchStringTest()
        {
            var vm = new PhotoViewModel();
            //Execute
            vm.ImageSearchKeyword = null;
            vm.SubmitExecute(null);
            Assert.AreEqual(vm.EmptyPhotoListMessage, ConstantsUtility.EmptySearchStringErrorMessage);
        }
        [TestMethod]
        public void SubmitExecuteEmptyStringAsSearchStringTest()
        {
            var vm = new PhotoViewModel();
            //Execute
            vm.ImageSearchKeyword = String.Empty;
            vm.SubmitExecute(null);
            Assert.AreEqual(vm.EmptyPhotoListMessage, ConstantsUtility.EmptySearchStringErrorMessage);
        }

        [TestMethod]
        public void SubmitExecuteOnlySpacesAsSearchStringTest()
        {
            var vm = new PhotoViewModel();
            //Execute
            vm.ImageSearchKeyword = "   ";
            vm.SubmitExecute(null);
            Assert.AreEqual(vm.EmptyPhotoListMessage, ConstantsUtility.EmptySearchStringErrorMessage);
        }

        [TestMethod]
        public void SubmitExecuteSpacesAndTabAsSearchStringTest()
        {
            var vm = new PhotoViewModel();
            //Execute
            vm.ImageSearchKeyword = " \t ";
            vm.SubmitExecute(null);
            Assert.AreEqual(vm.EmptyPhotoListMessage, ConstantsUtility.EmptySearchStringErrorMessage);
        }

        [TestMethod]
        public void SubmitExecuteNoRecordTest()
        {

            var feedApiMock = new Mock<IFeedApi>(MockBehavior.Strict);
            var vm = new PhotoViewModel(feedApiMock.Object);

            //Setup;

            feedApiMock.Setup(api => api.ImageSearch(It.IsAny<string>())).Returns(() =>
            {
                var result = new FlickerFeed();
                result.IsSuccessful = true;
                result.Entry = new System.Collections.Generic.List<Entry>();
                return result;
            });

            //Execute
            vm.ImageSearchKeyword = "xyz";
            vm.SubmitExecute(null);

            //Assertion
            Assert.IsTrue(vm.IsPhotoListEmpty);
            Assert.AreEqual(vm.EmptyPhotoListMessage, ConstantsUtility.NoRecordFoundErrorMessageString);



        }

        [TestMethod]
        public void SubmitExecuteResultIsSuccessfulFieldFalseTest()
        {

            var feedApiMock = new Mock<IFeedApi>(MockBehavior.Strict);
            var vm = new PhotoViewModel(feedApiMock.Object);

            //Setup;

            feedApiMock.Setup(api => api.ImageSearch(It.IsAny<string>())).Returns(() =>
            {
                var result = new FlickerFeed();
                result.IsSuccessful = false;
                return result;
            });

            //Execute
            vm.ImageSearchKeyword = "xyz";
            vm.SubmitExecute(null);

            //Assertion
            Assert.IsTrue(vm.IsPhotoListEmpty);
        }

        [TestMethod]
        public void SubmitExecuteNullAsResultTest()
        {

            var feedApiMock = new Mock<IFeedApi>(MockBehavior.Strict);
            var vm = new PhotoViewModel(feedApiMock.Object);

            //Setup;

            feedApiMock.Setup(api => api.ImageSearch(It.IsAny<string>())).Returns<IFeedApi>(null);

            //Execute
            vm.ImageSearchKeyword = "xyz";
            vm.SubmitExecute(null);

            //Assertion
            Assert.IsTrue(vm.IsPhotoListEmpty);
            Assert.AreEqual(vm.EmptyPhotoListMessage, ConstantsUtility.ErrorMessageString);
        }

        [TestMethod]
        public void SubmitExecuteValidRecordTest()
        {

            var feedApiMock = new Mock<IFeedApi>(MockBehavior.Strict);
            var vm = new PhotoViewModel(feedApiMock.Object);

            //Setup;
            var result = new FlickerFeed();
            result.IsSuccessful = true;
            result.Entry = new List<Entry>();
            result.Entry.Add(new Entry
            {
                Title = "Lion",
                Link = new List<Link> {
                              new Link{ Type="image/jpeg",
                                  Href = "https://live.staticflickr.com/65535/48394589351_73abef9a4c_b.jpg" },
                              new Link{Type="text/html" ,
                                  Href="https://www.flickr.com/photos/haribokart/48394589351/"}
                                       }
            });
            feedApiMock.Setup(api => api.ImageSearch(It.IsAny<string>())).Returns(result);

            //Execute
            vm.ImageSearchKeyword = "Lion";
            vm.SubmitExecute(null);

            //Assertion
            Assert.IsFalse(vm.IsPhotoListEmpty);
            Assert.AreEqual(vm.EmptyPhotoListMessage, String.Empty);
        }
    }
}
