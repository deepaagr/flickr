using ImageSearchWPF.API.Data;
using RestSharp;
using RestSharp.Deserializers;
using ImageSearchWPF.API.Interface;
using ImageSearchWPF.Utils;

namespace ImageSearchWPF.API
{
    public class FlickerFeedApiClient : IFeedApi
    {
        const string FlickerURL = "https://www.flickr.com/services/feeds/photos_public.gne";
        public IResult ImageSearch(string searchKeyword)
        {
            try
            {
                var client = new RestClient(FlickerURL);
                var request = new RestRequest();
                request.Method = Method.GET;
                request.AddParameter(ConstantsUtility.RequestParameterTagsString, searchKeyword);
                
                // execute the request
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var content = response.Content; // raw content as string

                    //Desearialization
                    DotNetXmlDeserializer deserial = new DotNetXmlDeserializer();
                    var feed = deserial.Deserialize<FlickerFeed>(response);
                    feed.IsSuccessful = true;
                    return feed;
                }
                else
                {
                    var feed = new FlickerFeed();
                    feed.IsSuccessful = false;
                    feed.ErrorMessage = response.ErrorMessage;
                    return feed;
                }
            }
            catch
            {
                return null;
            }
        }

        
    }
}
