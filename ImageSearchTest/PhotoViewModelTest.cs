using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageSearchWPF.ViewModel;
using Moq;
using ImageSearchWPF.API.Interface;
using ImageSearchWPF.API.Data;
using ImageSearchWPF.Utils;

namespace ImageSearchTest
{
    [TestClass]
    public class PhotoViewModelTest
    {
        [TestMethod]
        public void SubmitExecuteNullAsSearchStringTest()
        {
            var vm = new PhotoViewModel();
            vm.SubmitExecute(null);
            Assert.AreEqual(vm.EmptyPhotoListMessage, ConstantsUtility.EmptySearchStringErrorMessage);
        }
        [TestMethod]
        public void SubmitExecuteEmptyStringAsSearchStringTest()
        {
            var vm = new PhotoViewModel();
            vm.SubmitExecute(String.Empty);
            Assert.AreEqual(vm.EmptyPhotoListMessage, ConstantsUtility.EmptySearchStringErrorMessage);
        }

        [TestMethod]
        public void SubmitExecuteOnlySpacesAsSearchStringTest()
        {
            var vm = new PhotoViewModel();
            vm.SubmitExecute("    ");
            Assert.AreEqual(vm.EmptyPhotoListMessage, ConstantsUtility.EmptySearchStringErrorMessage);
        }

        [TestMethod]
        public void SubmitExecuteSpacesAndTabAsSearchStringTest()
        {
            var vm = new PhotoViewModel();
            vm.SubmitExecute(" \t   ");
            Assert.AreEqual(vm.EmptyPhotoListMessage, ConstantsUtility.EmptySearchStringErrorMessage);
        }

        [TestMethod]
        public void SubmitExecuteNoRecordTest()
        {
           
            var feedApiMock = new Mock<IFeedApi>(MockBehavior.Strict);
            var vm = new PhotoViewModel(feedApiMock.Object);

            //Setup;

            feedApiMock.Setup(api => api.ImageSearch(It.IsAny<string>())).Returns(() => {
                var result = new FlickerFeed();
                result.IsSuccessful = true;
                result.Entry = new System.Collections.Generic.List<Entry>();
                return result;
            });

            //Execute
            vm.SubmitExecute("xyz");

            //Assertion
            Assert.IsTrue(vm.IsPhotoListEmpty);
            Assert.AreEqual(vm.EmptyPhotoListMessage, ConstantsUtility.NoRecordFoundErrorMessageString);

            

        }
    }
}
